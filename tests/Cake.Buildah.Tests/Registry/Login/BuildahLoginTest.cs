using Cake.Core.IO;
using NUnit.Framework;

namespace Cake.Buildah.Tests.Registry.Login;

[TestFixture]
public class BuildahLoginTest
{
    [Test]
    public void WhenOnlyPathIsSet_CommandLineIsCorrect()
    {
        var fixture = new BuildahRegistryLoginFixture
        {
            Settings = new(),
            Path = "path"
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo("login path"));
    }
    [Test]
    public void WhenOnlyUsernameIsSet_CommandLineIsCorrect()
    {
        var fixture = new BuildahRegistryLoginFixture
        {
            Settings = new()
                { Username = "Tubo" },
            Path = "path"
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo("login --username \"Tubo\" path"));
    }
    [Test]
    public void WhenOnlyPasswordIsSet_ArgumentIsRedacted()
    {
        var builder = new ProcessArgumentBuilder();
        builder.AppendAll("login", new BuildahRegistryLoginSettings { Password = "Tubo" }, Array.Empty<string>());

        var actual = builder.RenderSafe();

        Assert.That(actual, Is.EqualTo("login --password \"[REDACTED]\""));
    }
}
