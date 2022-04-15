using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Buildah;

/// <summary>
/// Arguments builder.
/// </summary>
[SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "We want these lower.")]
public static class ArgumentsBuilderExtension
{
    /// <summary>
    /// Appends all arguments from <paramref name="settings"/> and <paramref name="arguments"/>.
    /// </summary>
    /// <typeparam name="TSettings">The type of the settings.</typeparam>
    /// <param name="builder">The builder instance.</param>
    /// <param name="command">The command.</param>
    /// <param name="settings">The settings.</param>
    /// <param name="arguments">Extra Arguments.</param>
    public static void AppendAll<TSettings>(this ProcessArgumentBuilder builder, string command, TSettings? settings, string[] arguments)
        where TSettings : AutoToolSettings, new()
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        if (arguments == null)
        {
            throw new ArgumentNullException(nameof(arguments));
        }

        if (string.IsNullOrEmpty(command))
        {
            throw new ArgumentNullException(nameof(command));
        }

        settings ??= new();

        AppendArguments(builder, settings, true);
        builder.Append(command);
        AppendArguments(builder, settings, false);

        foreach (var argument in arguments)
        {
            builder.Append(argument);
        }
    }

    /// <summary>
    /// Appends pre or post command arguments.
    /// </summary>
    /// <typeparam name="TSettings">The type of the settings.</typeparam>
    /// <param name="builder">The builder instance.</param>
    /// <param name="settings">The settings.</param>
    /// <param name="preCommand">The pre command.</param>
    public static void AppendArguments<TSettings>(ProcessArgumentBuilder builder, TSettings settings, bool preCommand)
        where TSettings : AutoToolSettings, new()
    {
        foreach (var property in typeof(TSettings).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var isSecret = IsPropertyValueSecret(property, settings);
            var query = from a in GetArgumentFromProperty(property, settings, preCommand, isSecret) where a.HasValue select a.Value;

            foreach (var (key, value, quoting) in query)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    builder.Append(key);
                }

                if (!string.IsNullOrEmpty(value))
                {
                    switch (quoting)
                    {
                        case BuildahArgumentQuoting.Unquoted:
                            builder.Append(value);
                            break;
                        case BuildahArgumentQuoting.QuotedSecret:
                            builder.AppendQuotedSecret(value);
                            break;
                        case BuildahArgumentQuoting.Quoted:
                        default:
                            builder.AppendQuoted(value);
                            break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Gets and processes <paramref name="property"/> value from <paramref name="settings"/>.
    /// </summary>
    /// <typeparam name="TSettings">The type of Settings.</typeparam>
    /// <param name="property">The property to handle.</param>
    /// <param name="settings">The settings.</param>
    /// <param name="preCommand">Pre or post command.</param>
    /// <param name="isSecret">declares if it is secret.</param>
    /// <returns>A collection of <see cref="BuildahArgument"/>.</returns>
    public static IEnumerable<BuildahArgument?> GetArgumentFromProperty<TSettings>(PropertyInfo? property, TSettings settings, bool preCommand, bool isSecret)
        where TSettings : AutoToolSettings, new()
    {
        if (property is null)
        {
            yield return default(BuildahArgument);
        }

        var autoPropertyAttribute = GetAutoPropertyAttributeOrNull(property);
        if (autoPropertyAttribute?.Format != null)
        {
            if (autoPropertyAttribute.PreCommand == preCommand)
            {
                yield return new BuildahArgument(
                    null,
                    GetArgumentFromAutoProperty(autoPropertyAttribute, property, property?.GetValue(settings)),
                    BuildahArgumentQuoting.Unquoted);
            }
        }
        else if ((!preCommand && autoPropertyAttribute is not { PreCommand: true }) || (autoPropertyAttribute?.PreCommand != null && preCommand))
        {
            if (property?.PropertyType == typeof(bool))
            {
                yield return new BuildahArgument(null, GetArgumentFromBoolProperty(property, (bool)(property.GetValue(settings) ?? false)), BuildahArgumentQuoting.Unquoted);
            }
            else if (property?.PropertyType == typeof(bool?))
            {
                yield return new BuildahArgument(null, GetArgumentFromNullableBoolProperty(property, (bool?)property.GetValue(settings)), BuildahArgumentQuoting.Unquoted);
            }
            else if (property?.PropertyType == typeof(int?))
            {
                yield return new BuildahArgument(null, GetArgumentFromNullableIntProperty(property, (int?)property.GetValue(settings)), BuildahArgumentQuoting.Unquoted);
            }
            else if (property?.PropertyType == typeof(long?))
            {
                yield return new BuildahArgument(null, GetArgumentFromNullableInt64Property(property, (long?)property.GetValue(settings)), BuildahArgumentQuoting.Unquoted);
            }
            else if (property?.PropertyType == typeof(ulong?))
            {
                yield return new BuildahArgument(
                    null,
                    GetArgumentFromNullableUInt64Property(property, (ulong?)property.GetValue(settings)),
                    BuildahArgumentQuoting.Unquoted);
            }
            else if (property?.PropertyType == typeof(ushort?))
            {
                yield return new BuildahArgument(
                    null,
                    GetArgumentFromNullableUInt16Property(property, (ushort?)property.GetValue(settings)),
                    BuildahArgumentQuoting.Unquoted);
            }
            else if (property?.PropertyType == typeof(string))
            {
                yield return GetArgumentFromStringProperty(property, (string)property.GetValue(settings)!, isSecret);
            }
            else if (property?.PropertyType == typeof(TimeSpan?))
            {
                yield return new BuildahArgument(
                    null,
                    GetArgumentFromNullableTimeSpanProperty(property, (TimeSpan?)property.GetValue(settings)),
                    BuildahArgumentQuoting.Unquoted);
            }
            else if (property?.PropertyType == typeof(string[]))
            {
                foreach (var arg in GetArgumentFromStringArrayProperty(property, (string[])property.GetValue(settings)!, isSecret))
                {
                    yield return arg;
                }
            }
        }
    }

    /// <summary>
    /// Checks out whether given <paramref name="property"/> is a secret.
    /// </summary>
    /// <typeparam name="TSettings">The type of settings.</typeparam>
    /// <param name="property">The property.</param>
    /// <param name="settings">The settings.</param>
    /// <returns>True if secret, false if not.</returns>
    public static bool IsPropertyValueSecret<TSettings>(PropertyInfo? property, TSettings? settings)
        where TSettings : AutoToolSettings
    {
        if (property is null || settings is null)
        {
            return false;
        }

        return settings.SecretProperties?.Contains(property.Name) ?? false;
    }

    /// <summary>
    /// Uses format specified in attribute to format the argument.
    /// </summary>
    /// <param name="attribute">The attribute.</param>
    /// <param name="property">the property.</param>
    /// <param name="value">the value.</param>
    /// <returns>a string, or null.</returns>
    public static string? GetArgumentFromAutoProperty(AutoPropertyAttribute? attribute, PropertyInfo? property, object? value)
    {
        if (attribute?.Format is null)
        {
            return null;
        }

        if (value == null)
        {
            return null;
        }

        if (property is null)
        {
            return null;
        }

        var result = string.Format(CultureInfo.InvariantCulture, attribute.Format!, GetPropertyName(property.Name), value);
        if (attribute.OnlyWhenTrue)
        {
            var boolValue = (bool)value;
            return boolValue ? result : string.Empty;
        }

        if (property.PropertyType == typeof(string[]))
        {
            var strings = (string[])value;
            result = string.Join(" ", strings.Select(s => string.Format(CultureInfo.InvariantCulture, attribute.Format, GetPropertyName(property.Name), s)));
        }

        return result;
    }

    /// <summary>
    /// Retrieve <see cref="AutoPropertyAttribute"/> from property or null if there isn't one.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>an instance of <see cref="AutoPropertyAttribute"/>.</returns>
    public static AutoPropertyAttribute? GetAutoPropertyAttributeOrNull(PropertyInfo? property) => property?.GetCustomAttribute<AutoPropertyAttribute>();

    /// <summary>
    /// Get an argument from a bool property.
    /// </summary>
    /// <param name="property">the property.</param>
    /// <param name="value">the value.</param>
    /// <returns>a nullable string.</returns>
    public static string? GetArgumentFromBoolProperty(PropertyInfo? property, bool value)
    {
        if (property is null)
        {
            return null;
        }

        return value ? $"--{GetPropertyName(property.Name)}" : null;
    }

    /// <summary>
    /// Get Argument From NullableIntProperty.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <param name="value">the value.</param>
    /// <returns>a nullable string.</returns>
    public static string? GetArgumentFromNullableIntProperty(PropertyInfo? property, int? value)
    {
        if (property is null)
        {
            return null;
        }

        return value.HasValue ? $"--{GetPropertyName(property.Name)} {value.Value}" : null;
    }

    /// <summary>
    /// Get Argument From NullableInt64Property.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <param name="value">the value.</param>
    /// <returns>a nullable string.</returns>
    public static string? GetArgumentFromNullableInt64Property(PropertyInfo? property, long? value)
    {
        if (property is null)
        {
            return null;
        }

        return value.HasValue ? $"--{GetPropertyName(property.Name)} {value.Value}" : null;
    }

    /// <summary>
    /// Get an argument from an unsigned long.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <param name="value">the value.</param>
    /// <returns>a nullable string.</returns>
    public static string? GetArgumentFromNullableUInt64Property(PropertyInfo? property, ulong? value)
    {
        if (property is null)
        {
            return null;
        }

        return value.HasValue ? $"--{GetPropertyName(property.Name)} {value.Value}" : null;
    }

    /// <summary>
    /// Get an argument from an unsigned short.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <param name="value">the value.</param>
    /// <returns>a nullable string.</returns>
    public static string? GetArgumentFromNullableUInt16Property(PropertyInfo? property, ushort? value)
    {
        if (property is null)
        {
            return null;
        }

        return value.HasValue ? $"--{GetPropertyName(property.Name)} {value.Value}" : null;
    }

    /// <summary>
    /// Get an argument from a nullable bool.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <param name="value">the value.</param>
    /// <returns>a nullable string.</returns>
    public static string? GetArgumentFromNullableBoolProperty(PropertyInfo? property, bool? value)
    {
        if (property is null)
        {
            return null;
        }

        if (value ?? false)
        {
            return $"--{GetPropertyName(property.Name)}";
        }

        return null;
    }

    /// <summary>
    /// Get argument from dictionary.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <param name="values">the values.</param>
    /// <param name="isSecret">is it secret?.</param>
    /// <returns>a nullable list of <see cref="BuildahArgument"/>.</returns>
    public static IEnumerable<BuildahArgument?> GetArgumentFromDictionaryProperty(PropertyInfo? property, Dictionary<string, string>? values, bool isSecret)
    {
        if (property is null)
        {
            yield return null;
        }

        if (values != null)
        {
            foreach (var (key, value) in values)
            {
                yield return GetArgumentFromStringProperty(property, $"{key}={value}", isSecret);
            }
        }
    }

    /// <summary>
    /// Get argument from string array.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <param name="values">the values.</param>
    /// <param name="isSecret">is it secret?.</param>
    /// <returns>a nullable list of <see cref="BuildahArgument"/>.</returns>
    public static IEnumerable<BuildahArgument?> GetArgumentFromStringArrayProperty(PropertyInfo? property, string?[]? values, bool isSecret)
    {
        if (property is null)
        {
            yield return null;
        }

        if (values != null)
        {
            foreach (var value in values)
            {
                yield return GetArgumentFromStringProperty(property, value, isSecret);
            }
        }
    }

    /// <summary>
    /// Get argument from String Array List.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <param name="values">the values.</param>
    /// <param name="isSecret">is it secret?.</param>
    /// <returns>a nullable list of <see cref="BuildahArgument"/>.</returns>
    public static BuildahArgument? GetArgumentFromStringArrayListProperty(PropertyInfo? property, string[]? values, bool isSecret)
    {
        if (property is null)
        {
            return null;
        }

        if (values is null)
        {
            return null;
        }

        if (values.Length > 0)
        {
            return GetArgumentFromStringProperty(property, string.Join(",", values), isSecret);
        }

        return null;
    }

    /// <summary>
    /// Get argument from string.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <param name="value">the value.</param>
    /// <param name="isSecret">is it secret?.</param>
    /// <returns>a nullable list of <see cref="BuildahArgument"/>.</returns>
    public static BuildahArgument? GetArgumentFromStringProperty(PropertyInfo? property, string? value, bool isSecret)
    {
        if (property is null)
        {
            return null;
        }

        if (!string.IsNullOrEmpty(value))
        {
            return new BuildahArgument($"--{GetPropertyName(property.Name)}", value, isSecret ? BuildahArgumentQuoting.QuotedSecret : BuildahArgumentQuoting.Quoted);
        }

        return null;
    }

    /// <summary>
    /// Get argument from nullable timespan.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <param name="value">the value.</param>
    /// <returns>a nullable .</returns>
    public static string? GetArgumentFromNullableTimeSpanProperty(PropertyInfo? property, TimeSpan? value)
    {
        if (property is null)
        {
            return null;
        }

        return value.HasValue ? $"--{GetPropertyName(property.Name)} {ConvertTimeSpan(value.Value)}" : null;
    }

    /// <summary>
    /// Convert timespan to string.
    /// </summary>
    /// <param name="source">The source timespan.</param>
    /// <returns>a string representing the timespan.</returns>
    public static string ConvertTimeSpan(TimeSpan source) => $"{Math.Floor(source.TotalHours)}h{source.Minutes}m{source.Seconds}s";

    /// <summary>
    /// Converts property name to Buildah arguments format.
    /// </summary>
    /// <param name="name">the property name.</param>
    /// <returns>a nullable string.</returns>
    /// <example>NoForce -> no-force.</example>
    public static string? GetPropertyName(string name)
    {
        string? result = null;
        if (!string.IsNullOrEmpty(name))
        {
            if (name.Length > 0)
            {
                result = name[..1].ToLower();
            }

            if (name.Length > 1)
            {
                foreach (var c in name[1..])
                {
                    if (char.IsUpper(c))
                    {
                        result += "-" + char.ToLower(c);
                    }
                    else
                    {
                        result += c;
                    }
                }
            }
        }

        return result;
    }
}
