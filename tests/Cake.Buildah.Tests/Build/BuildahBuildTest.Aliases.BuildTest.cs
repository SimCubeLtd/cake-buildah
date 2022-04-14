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
            Settings = new(),
            Path = "path"
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo("bud \"path\""));
    }
    [Test]
    public void WhenRmFlagIsSet_CommandLineIsCorrect()
    {
        var fixture = new BuildahBuildFixture
        {
            Settings = new()
                {  Rm = true },
            Path = "path"
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo("bud --rm=True \"path\""));
    }

    [Test]
    public void WhenRmFlagIsSetToFalse_CommandLineDoesNotHaveRm()
    {
        var fixture = new BuildahBuildFixture
        {
            Settings = new()
                { Rm = false },
            Path = "path"
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo("bud --rm=False \"path\""));
    }

    [Test]
    public void WhenForceRmFlagIsSetToFalse_CommandLineDoesNotHaveForceRm()
    {
        var fixture = new BuildahBuildFixture
        {
            Settings = new()
                { ForceRm = false },
            Path = "path"
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo("bud \"path\""));
    }

    [Test]
    public void WhenPullFlagIsSetToFalse_CommandLineDoesNotHavePull()
    {
        var fixture = new BuildahBuildFixture
        {
            Settings = new()
                { Pull = false },
            Path = "path"
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo("bud \"path\""));
    }

    [Test]
    public void WhenPullFlagIsSetToTrue_CommandLineDoesHavePull()
    {
        var fixture = new BuildahBuildFixture
        {
            Settings = new()
                { Pull = true },
            Path = "path"
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo("bud --pull \"path\""));
    }
    [Test]
    public void WhenPathHasSpaces_ArgumentIsQuoted()
    {
        var fixture = new BuildahBuildFixture
        {
            Settings = new(),
            Path = @"C:\Some where"
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo(@"bud ""C:\Some where"""));
    }
    [Test]
    public void WhenPathHasSingleQuote_ArgumentIsQuoted()
    {
        var fixture = new BuildahBuildFixture
        {
            Settings = new(),
            Path = "\""
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
            Settings = new(),
            Path = path
        };

        var actual = fixture.Run();

        Assert.That(actual.Args, Is.EqualTo($"bud {path}"));
    }
}
