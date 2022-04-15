using NUnit.Framework;

namespace Cake.Buildah.Tests;

[TestFixture]
public class GetArgumentFromAutoProperty : ArgumentsBuilderExtensionTest
{
    [Test]
    public void WhenGivenValue_FormatsProperly()
    {
        var attribute = ArgumentsBuilderExtension.GetAutoPropertyAttributeOrNull(DecoratedStringProperty);
        var actual =
            ArgumentsBuilderExtension.GetArgumentFromAutoProperty(attribute, DecoratedStringProperty, "SIGNAL");

        Assert.That(actual, Is.EqualTo("-s SIGNAL"));
    }

    [Test]
    public void WhenOnlyWhenTrueValue_AndIsFalse_ReturnsEmptyString()
    {
        var attribute = ArgumentsBuilderExtension.GetAutoPropertyAttributeOrNull(DecoratedBoolProperty);
        var actual = ArgumentsBuilderExtension.GetArgumentFromAutoProperty(attribute, DecoratedBoolProperty, false);

        Assert.That(actual, Is.Empty);
    }

    [Test]
    public void WhenOnlyWhenTrueValue_AndIsTrue_FormatsProperly()
    {
        var attribute = ArgumentsBuilderExtension.GetAutoPropertyAttributeOrNull(DecoratedBoolProperty);
        var actual = ArgumentsBuilderExtension.GetArgumentFromAutoProperty(attribute, DecoratedBoolProperty, true);

        Assert.That(actual, Is.EqualTo("-v"));
    }

    [Test]
    public void WhenDecoratedStrings_FormatsProperly()
    {
        var attribute = ArgumentsBuilderExtension.GetAutoPropertyAttributeOrNull(DecoratedStringsProperty);
        var actual = ArgumentsBuilderExtension.GetArgumentFromAutoProperty(
            attribute,
            DecoratedStringsProperty,
            new[]
            {
                "One=1",
                "Two=2",
            });

        Assert.That(actual, Is.EqualTo("-e One=1 -e Two=2"));
    }
}
