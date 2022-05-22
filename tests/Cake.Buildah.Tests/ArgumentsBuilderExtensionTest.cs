using System.Reflection;
using Cake.Core.IO;
using NUnit.Framework;

namespace Cake.Buildah.Tests;

public class ArgumentsBuilderExtensionTest
{
    public static PropertyInfo? StringProperty => GetProperty(nameof(TestSettings.String));

    public static PropertyInfo? PasswordProperty => GetProperty(nameof(TestSettings.Password));

    public static PropertyInfo? StringsProperty => GetProperty(nameof(TestSettings.Strings));

    public static PropertyInfo? ListStringsProperty => GetProperty(nameof(TestSettings.ListStrings));

    public static PropertyInfo? NullableIntProperty => GetProperty(nameof(TestSettings.NullableInt));

    public static PropertyInfo? NullableInt64Property => GetProperty(nameof(TestSettings.NullableInt64));

    public static PropertyInfo? NullableUInt64Property => GetProperty(nameof(TestSettings.NullableUInt64));

    public static PropertyInfo? NullableUInt16Property => GetProperty(nameof(TestSettings.NullableUInt16));

    public static PropertyInfo? NullableBoolProperty => GetProperty(nameof(TestSettings.NullableBool));

    public static PropertyInfo? NullableTimeSpanProperty => GetProperty(nameof(TestSettings.NullableTimeSpan));

    public static PropertyInfo? BoolProperty => GetProperty(nameof(TestSettings.Bool));

    public static PropertyInfo? DecoratedStringProperty => GetProperty(nameof(TestSettings.DecoratedString));

    public static PropertyInfo? DecoratedBoolProperty => GetProperty(nameof(TestSettings.DecoratedBool));

    public static PropertyInfo? DecoratedStringsProperty => GetProperty(nameof(TestSettings.DecoratedStrings));

    public static PropertyInfo? PreCommandValueProperty => GetProperty(nameof(TestSettings.PreCommandValue));

    public static PropertyInfo? GetProperty(string name) => typeof(TestSettings).GetProperty(name, BindingFlags.Public | BindingFlags.Instance);

    [TestFixture]
    public class GetArgumentFromBoolProperty
    {
        [Test]
        public void WhenTrue_FormatsProperly()
        {
            var actual = ArgumentsBuilderExtension.GetArgumentFromBoolProperty(BoolProperty, true);

            Assert.That(actual, Is.EqualTo("--bool"));
        }

        [Test]
        public void WhenFalse_NullIsReturned()
        {
            var actual = ArgumentsBuilderExtension.GetArgumentFromBoolProperty(BoolProperty, false);

            Assert.That(actual, Is.Null);
        }
    }

    [TestFixture]
    public class GetArgumentFromStringProperty
    {
        [Test]
        public void WhenGivenStringProperty_FormatsProperly()
        {
            var actual =
                ArgumentsBuilderExtension.GetArgumentFromStringProperty(StringProperty, "tubo")!.Value;

            Assert.That(actual.Key, Is.EqualTo("--string"));
            Assert.That(actual.Value, Is.EqualTo("tubo"));
            Assert.That(actual.Quoting, Is.EqualTo(BuildahArgumentQuoting.Quoted));
        }

        [Test]
        public void WhenGivenNull_NullIsReturned()
        {
            var actual = ArgumentsBuilderExtension.GetArgumentFromStringProperty(StringProperty, null);

            Assert.That(actual, Is.Null);
        }
    }

    [TestFixture]
    public class GetArgumentFromPasswordProperty
    {
        [Test]
        public void WhenGivenStringProperty_FormatsProperly()
        {
            var actual =
                ArgumentsBuilderExtension.GetArgumentFromStringProperty(StringProperty, "tubo")!.Value;

            Assert.That(actual.Key, Is.EqualTo("--string"));
            Assert.That(actual.Value, Is.EqualTo("tubo"));
            Assert.That(actual.Quoting, Is.EqualTo(BuildahArgumentQuoting.Quoted));
        }
    }

    [TestFixture]
    public class GetArgumentFromStringArrayProperty
    {
        [Test]
        public void WhenGivenStringArrayProperty_FormatsProperly()
        {
            var actual = ArgumentsBuilderExtension.GetArgumentFromStringArrayProperty(
                StringsProperty,
                new[]
                {
                    "tubo1",
                    "tubo2",
                });

            CollectionAssert.AreEqual(
                actual,
                new BuildahArgument[]
                {
                    new("--strings", "tubo1", BuildahArgumentQuoting.Quoted),
                    new("--strings", "tubo2", BuildahArgumentQuoting.Quoted),
                });
        }

        [Test]
        public void WhenGivenNull_EmptyArrayReturned()
        {
            var actual =
                ArgumentsBuilderExtension.GetArgumentFromStringArrayProperty(StringsProperty, null);

            Assert.That(actual, Is.Empty);
        }
    }

    [TestFixture]
    public class GetArgumentFromStringArrayListProperty
    {
        [Test]
        public void WhenGivenStringArrayProperty_FormatsProperly()
        {
            var actual = ArgumentsBuilderExtension.GetArgumentFromStringArrayListProperty(
                ListStringsProperty,
                new[]
                {
                    "tubo1",
                    "tubo2",
                })!.Value;

            Assert.That(actual.Key, Is.EqualTo("--list-strings"));
            Assert.That(actual.Value, Is.EqualTo("tubo1,tubo2"));
            Assert.That(actual.Quoting, Is.EqualTo(BuildahArgumentQuoting.Quoted));
        }

        [Test]
        public void WhenGivenNull_EmptyArrayReturned()
        {
            var actual =
                ArgumentsBuilderExtension.GetArgumentFromStringArrayProperty(
                    ListStringsProperty,
                    null);

            Assert.That(actual, Is.Empty);
        }
    }

    [TestFixture]
    public class GetArgumentFromDictionaryProperty
    {
        [Test]
        public void WhenGivenStringArrayProperty_FormatsProperly()
        {
            var actual = ArgumentsBuilderExtension.GetArgumentFromDictionaryProperty(
                StringsProperty,
                new Dictionary<string, string>
                {
                    { "t1", "v1" },
                    { "t2", "v2" },
                });

            CollectionAssert.AreEqual(
                actual,
                new BuildahArgument[]
                {
                    new("--strings", "t1=v1", BuildahArgumentQuoting.Quoted),
                    new("--strings", "t2=v2", BuildahArgumentQuoting.Quoted),
                });
        }

        [Test]
        public void WhenGivenNull_EmptyArrayReturned()
        {
            var actual =
                ArgumentsBuilderExtension.GetArgumentFromDictionaryProperty(StringsProperty, null);

            Assert.That(actual, Is.Empty);
        }
    }

    [TestFixture]
    public class GetArgumentFromNullableIntProperty
    {
        [Test]
        public void WhenGivenValue_FormatsProperly()
        {
            var actual = ArgumentsBuilderExtension.GetArgumentFromNullableIntProperty(NullableIntProperty, 5);

            Assert.That(actual, Is.EqualTo("--nullable-int 5"));
        }

        [Test]
        public void WhenGivenNull_NullIsReturned()
        {
            var actual = ArgumentsBuilderExtension.GetArgumentFromNullableIntProperty(NullableIntProperty, null);

            Assert.That(actual, Is.Null);
        }
    }

    [TestFixture]
    public class GetArgumentFromNullableInt64Property
    {
        [Test]
        public void WhenGivenValue_FormatsProperly()
        {
            var actual = ArgumentsBuilderExtension.GetArgumentFromNullableInt64Property(NullableInt64Property, 5);

            Assert.That(actual, Is.EqualTo("--nullable-int64 5"));
        }

        [Test]
        public void WhenGivenNull_NullIsReturned()
        {
            var actual = ArgumentsBuilderExtension.GetArgumentFromNullableIntProperty(NullableInt64Property, null);

            Assert.That(actual, Is.Null);
        }
    }

    [TestFixture]
    public class GetArgumentFromNullableUInt64Property
    {
        [Test]
        public void WhenGivenValue_FormatsProperly()
        {
            var actual = ArgumentsBuilderExtension.GetArgumentFromNullableUInt64Property(NullableUInt64Property, 5);

            Assert.That(actual, Is.EqualTo("--nullable-u-int64 5"));
        }

        [Test]
        public void WhenGivenNull_NullIsReturned()
        {
            var actual = ArgumentsBuilderExtension.GetArgumentFromNullableIntProperty(NullableUInt64Property, null);

            Assert.That(actual, Is.Null);
        }
    }

    [TestFixture]
    public class GetArgumentFromNullableUInt16Property
    {
        [Test]
        public void WhenGivenValue_FormatsProperly()
        {
            var actual = ArgumentsBuilderExtension.GetArgumentFromNullableUInt16Property(NullableUInt16Property, 5);

            Assert.That(actual, Is.EqualTo("--nullable-u-int16 5"));
        }

        [Test]
        public void WhenGivenNull_NullIsReturned()
        {
            var actual = ArgumentsBuilderExtension.GetArgumentFromNullableIntProperty(NullableUInt16Property, null);

            Assert.That(actual, Is.Null);
        }
    }

    [TestFixture]
    public class GetArgumentFromNullableBoolProperty
    {
        [Test]
        public void WhenGivenValueIsTrue_FormatsProperly()
        {
            var actual = ArgumentsBuilderExtension.GetArgumentFromNullableBoolProperty(NullableBoolProperty, true);

            Assert.That(actual, Is.EqualTo("--nullable-bool"));
        }

        [Test]
        public void WhenGivenValueIsFalse_NullIsReturned()
        {
            var actual = ArgumentsBuilderExtension.GetArgumentFromNullableBoolProperty(NullableBoolProperty, false);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void WhenGivenNull_NullIsReturned()
        {
            var actual = ArgumentsBuilderExtension.GetArgumentFromNullableBoolProperty(NullableBoolProperty, null);

            Assert.That(actual, Is.Null);
        }
    }

    [TestFixture]
    public class GetArgumentFromNullableTimeSpanProperty
    {
        [Test]
        public void WhenGivenValue_FormatsProperly()
        {
            var actual =
                ArgumentsBuilderExtension.GetArgumentFromNullableTimeSpanProperty(
                    NullableTimeSpanProperty,
                    new TimeSpan(734, 18, 4));

            Assert.That(actual, Is.EqualTo("--nullable-time-span 734h18m4s"));
        }

        [Test]
        public void WhenGivenNull_NullIsReturned()
        {
            var actual =
                ArgumentsBuilderExtension.GetArgumentFromNullableTimeSpanProperty(NullableTimeSpanProperty, null);

            Assert.That(actual, Is.Null);
        }
    }

    [TestFixture]
    public class GetPropertyName
    {
        [TestCase("Name", ExpectedResult = "name")]
        [TestCase("NameExtended", ExpectedResult = "name-extended")]
        public string? WhenInput_ReturnsCorrectlyFormatted(string name) => ArgumentsBuilderExtension.GetPropertyName(name);
    }

    [TestFixture]
    public class AppendAll
    {
        [Test]
        public void WhenStringInput_AddsAsArgument()
        {
            var input = new TestSettings
            {
                String = "tubo",
            };

            var builder = new ProcessArgumentBuilder();
            builder.AppendAll(
                "cmd",
                input,
                new[]
                {
                    "arg1",
                });
            var actual = builder.Render();

            Assert.That(actual, Is.EqualTo("cmd --string \"tubo\" arg1"));
        }
    }

    [TestFixture]
    public class ConvertTimeSpan
    {
        [Test]
        public void WhenGivenInput_ConvertsProperly()
        {
            var actual = ArgumentsBuilderExtension.ConvertTimeSpan(new(734, 18, 4));

            Assert.That(actual, Is.EqualTo("734h18m4s"));
        }
    }
}
