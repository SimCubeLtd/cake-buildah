using NUnit.Framework;

namespace Cake.Buildah.Tests.Build;

[TestFixture]
public class BuildahBuildTest
{
    [Test]
    public void WhenOnlyPathIsSet_CommandLineIsCorrect()
    {
        var fixture = new BuildahBuildFixture
        {
            Settings = new BuildahImageBuildSettings(),
            Path = "path",
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo("bud \"path\""));
    }

    [Test]
    public void WhenPathHasSpaces_ArgumentIsQuoted()
    {
        var fixture = new BuildahBuildFixture
        {
            Settings = new BuildahImageBuildSettings(),
            Path = @"C:\Some where",
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo(@"bud ""C:\Some where"""));
    }

    [Test]
    public void WhenPathHasSingleQuote_ArgumentIsQuoted()
    {
        var fixture = new BuildahBuildFixture
        {
            Settings = new BuildahImageBuildSettings(),
            Path = "\"",
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo(@"bud """""""));
    }

    [TestCase("\"test\"")]
    [TestCase(" \"test\"")]
    [TestCase("\"test\" ")]
    [TestCase(" \"test\" ")]
    public void WhenPathIsQuoted_ArgumentIsNotDoubleQuoted(string path)
    {
        var fixture = new BuildahBuildFixture
        {
            Settings = new BuildahImageBuildSettings(),
            Path = path,
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo($"bud {path}"));
    }
}
