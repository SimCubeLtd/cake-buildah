using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Buildah;

/// <summary>
/// Generic Buildah Runner.
/// </summary>
/// <typeparam name="TSettings">The type of the settings instance.</typeparam>
public class GenericBuildahRunner<TSettings> : BuildahTool<TSettings>
    where TSettings : AutoToolSettings, new()
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GenericBuildahRunner{TSettings}"/> class.
    /// </summary>
    /// <param name="fileSystem">Represents a file system.</param>
    /// <param name="environment">Represents the environment.</param>
    /// <param name="processRunner">Represents a process runner.</param>
    /// <param name="tools">Represents a tool locator.</param>
    public GenericBuildahRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
        : base(fileSystem, environment, processRunner, tools)
    {
    }

    /// <summary>
    /// Runs given <paramref name="command"/> using given <paramref name=" settings"/> and <paramref name="additional"/>.
    /// </summary>
    /// <param name="command">the command to run.</param>
    /// <param name="settings">The settings.</param>
    /// <param name="additional">additional arguments.</param>
    public void Run(string command, TSettings? settings, string[] additional)
    {
        if (string.IsNullOrEmpty(command))
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (settings == null)
        {
            throw new ArgumentNullException(nameof(settings));
        }

        if (additional == null)
        {
            throw new ArgumentNullException(nameof(additional));
        }

        Run(settings, GenericBuildahRunner<TSettings>.GetArguments(command, settings, additional));
    }

    /// <summary>
    /// Runs a command and returns a result based on processed output.
    /// </summary>
    /// <typeparam name="T">The type of the results.</typeparam>
    /// <param name="command">the command to run.</param>
    /// <param name="settings">The settings.</param>
    /// <param name="processOutput">the output returned from the execution.</param>
    /// <param name="arguments">extra arguments.</param>
    /// <returns>an Array of T.</returns>
    public T[] RunWithResult<T>(
        string command,
        TSettings? settings,
        Func<IEnumerable<string>, T[]> processOutput,
        params string[] arguments)
    {
        if (string.IsNullOrEmpty(command))
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (settings == null)
        {
            throw new ArgumentNullException(nameof(settings));
        }

        if (processOutput == null)
        {
            throw new ArgumentNullException(nameof(processOutput));
        }

        var result = Array.Empty<T>();
        var processSettings = new ProcessSettings
        {
            RedirectStandardOutput = true,
        };

        Run(
            settings,
            GetArguments(command, settings, arguments),
            processSettings,
            proc =>
            {
                result = processOutput(proc.GetStandardOutput());
            });

        return result;
    }

    private static ProcessArgumentBuilder GetArguments(string command, TSettings? settings, string[] additional)
    {
        var builder = new ProcessArgumentBuilder();
        builder.AppendAll(command, settings, additional);
        return builder;
    }
}
