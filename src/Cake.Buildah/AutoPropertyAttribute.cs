namespace Cake.Buildah;

/// <summary>
/// Auto Property Attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class AutoPropertyAttribute : Attribute
{
    /// <summary>
    /// Gets or sets format of the output, i.e. "-s {1}"
    /// where {0} is property name and {1} is value.
    /// </summary>
    public string? Format { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether outputs only when given value is true.
    /// </summary>
    public bool OnlyWhenTrue { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether whether it appears before command.
    /// </summary>
    public bool PreCommand { get; set; }

    /// <summary>
    /// Gets or sets array representation in command line.
    /// </summary>
    /// <remarks>Default is <see>
    ///         <cref xml:space="preserve"> AutoArrayType.MultipleInstances</cref>
    ///     </see>
    ///     .</remarks>
    public AutoArrayType AutoArrayType { get; set; } = AutoArrayType.MultipleInstances;
}
