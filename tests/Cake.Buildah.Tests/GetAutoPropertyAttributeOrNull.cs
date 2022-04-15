using NUnit.Framework;

namespace Cake.Buildah.Tests;

[TestFixture]
public class GetAutoPropertyAttributeOrNull : ArgumentsBuilderExtensionTest
{
    [Test]
    public void WhenDecorated_ReturnsAutoPropertyAttribute()
    {
        var actual = ArgumentsBuilderExtension.GetAutoPropertyAttributeOrNull(DecoratedStringProperty);

        Assert.That(actual?.Format, Is.EqualTo("-s {1}"));
    }

    [Test]
    public void WhenNotDecorated_ReturnsNull()
    {
        var actual = ArgumentsBuilderExtension.GetAutoPropertyAttributeOrNull(StringProperty);

        Assert.That(actual, Is.Null);
    }
}
