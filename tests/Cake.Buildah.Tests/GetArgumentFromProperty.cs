using NUnit.Framework;

namespace Cake.Buildah.Tests;

[TestFixture]
public class GetArgumentFromProperty : ArgumentsBuilderExtensionTest
{
    [Test]
    public void WhenPreCommand_DoesNotAppearInNormalCommands()
    {
        var input = new TestSettings
        {
            PreCommandValue = "preCommand",
        };
        var actual =
            ArgumentsBuilderExtension.GetArgumentFromProperty(
                PreCommandValueProperty,
                input,
                false);

        Assert.That(actual.Count(), Is.Zero);
    }

    [Test]
    public void WhenPreCommand_ItAppearsInPreCommands()
    {
        var input = new TestSettings
        {
            PreCommandValue = "preCommand",
        };
        var actual =
            ArgumentsBuilderExtension.GetArgumentFromProperty(
                PreCommandValueProperty,
                input,
                true);

        Assert.That(actual.Count(), Is.EqualTo(1));
    }
}
