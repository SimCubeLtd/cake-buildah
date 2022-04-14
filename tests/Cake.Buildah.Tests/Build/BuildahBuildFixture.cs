using Cake.Core;
using Cake.Core.Configuration;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace Cake.Buildah.Tests.Build;

public class BuildahBuildFixture : ToolFixture<BuildahImageBuildSettings>, ICakeContext
{
    public string Path { get; set; } = null!;

    IFileSystem ICakeContext.FileSystem => FileSystem;

    ICakeEnvironment ICakeContext.Environment => Environment;

    public ICakeLog Log => Log;

    ICakeArguments ICakeContext.Arguments => throw new NotImplementedException();

    IProcessRunner ICakeContext.ProcessRunner => ProcessRunner;

    public IRegistry Registry => Registry;

    public ICakeDataResolver Data => throw new NotImplementedException();

    ICakeConfiguration ICakeContext.Configuration => throw new NotImplementedException();

    public BuildahBuildFixture(): base("Buildah") => ProcessRunner.Process.SetStandardOutput(new string[] { });
    protected override void RunTool() => this.BuildahBuild(Settings, Path);
}
