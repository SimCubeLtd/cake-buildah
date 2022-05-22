using Cake.Core;
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
            Settings = new BuildahRegistryLoginSettings(),
            Path = "path",
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo("login path"));
    }

    [Test]
    public void WhenOnlyUsernameIsSet_CommandLineIsCorrect()
    {
        var fixture = new BuildahRegistryLoginFixture
        {
            Settings = new BuildahRegistryLoginSettings
            {
                Username = "Tubo",
            },
            Path = "path",
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo("login --username Tubo path"));
    }

    [Test]
    public void WhenUsernameAndTlsVerifyIsTrueIsSet_CommandLineIsCorrect()
    {
        var fixture = new BuildahRegistryLoginFixture
        {
            Settings = new BuildahRegistryLoginSettings
            {
                Username = "Tubo",
                TlsVerify = true,
            },
            Path = "path",
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo("login --username Tubo --tls-verify=True path"));
    }

    [Test]
    public void WhenUsernameAndPasswordIsSet_CommandLineIsCorrect()
    {
        var fixture = new BuildahRegistryLoginFixture
        {
            Settings = new BuildahRegistryLoginSettings
            {
                Username = "Tubo",
                Password = "Password",
            },
            Path = "path",
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo("login --username Tubo --password Password path"));
    }

    [Test]
    public void WhenUsernameAndPasswordAndTlsVerifyIsSet_CommandLineIsCorrect()
    {
        var fixture = new BuildahRegistryLoginFixture
        {
            Settings = new BuildahRegistryLoginSettings
            {
                Username = "Tubo",
                Password = "Password",
                TlsVerify = true,
            },
            Path = "path",
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo("login --username Tubo --password Password --tls-verify=True path"));
    }

    [Test]
    public void WhenUsernameAndPasswordAndTlsVerifyIsFalseIsSet_CommandLineIsCorrect()
    {
        var fixture = new BuildahRegistryLoginFixture
        {
            Settings = new BuildahRegistryLoginSettings
            {
                Username = "Tubo",
                Password = "Password",
                TlsVerify = false,
            },
            Path = "path",
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo("login --username Tubo --password Password --tls-verify=False path"));
    }
}
