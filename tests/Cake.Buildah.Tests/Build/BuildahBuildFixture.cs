using Cake.Core;
using Cake.Core.Configuration;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace Cake.Buildah.Tests.Build;

public class BuildahBuildFixture : ToolFixture<BuildahImageBuildSettings>, ICakeContext
{
    public BuildahBuildFixture()
        : base("buildah") => ProcessRunner.Process.SetStandardOutput(new string[] { });

    public string Path { get; set; } = null!;

    IFileSystem ICakeContext.FileSystem => FileSystem;

    ICakeEnvironment ICakeContext.Environment => Environment;

    public ICakeLog Log => new NullLog();

    ICakeArguments ICakeContext.Arguments => throw new NotImplementedException();

    IProcessRunner ICakeContext.ProcessRunner => ProcessRunner;

    public IRegistry Registry => throw new NotImplementedException();

    public ICakeDataResolver Data => throw new NotImplementedException();

    ICakeConfiguration ICakeContext.Configuration => throw new NotImplementedException();

    protected override void RunTool() => this.BuildahBuild(Settings, Path);
}
