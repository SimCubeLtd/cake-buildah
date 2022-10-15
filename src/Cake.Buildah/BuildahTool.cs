using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Buildah;

/// <summary>
/// Base class for all Buildah related tools.
/// </summary>
/// <typeparam name="TSettings">The settings type.</typeparam>
public abstract class BuildahTool<TSettings> : Tool<TSettings>
    where TSettings : ToolSettings
{
    private readonly ICakeEnvironment _environment;

    /// <summary>
    /// Initializes a new instance of the <see cref="BuildahTool{TSettings}"/> class.
    /// </summary>
    /// <param name="fileSystem">The file system.</param>
    /// <param name="environment">The _environment.</param>
    /// <param name="processRunner">The process runner.</param>
    /// <param name="tools">The tools.</param>
    protected BuildahTool(
        IFileSystem fileSystem,
        ICakeEnvironment environment,
        IProcessRunner processRunner,
        IToolLocator tools)
        : base(fileSystem, environment, processRunner, tools) => _environment = environment;

    /// <summary>
    /// Gets the name of the tool.
    /// </summary>
    /// <returns>The name of the tool.</returns>
    protected override string GetToolName() => "buildah";

    /// <summary>
    /// Gets the possible names of the tool executable.
    /// </summary>
    /// <returns>The tool executable name.</returns>
    protected override IEnumerable<string> GetToolExecutableNames()
    {
        if (_environment.Platform.IsUnix())
        {
            return new[]
            {
                "buildah",
            };
        }

        return Array.Empty<string>();
    }
}
