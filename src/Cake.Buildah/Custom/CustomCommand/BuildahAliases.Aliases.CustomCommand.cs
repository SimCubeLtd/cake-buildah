using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Buildah;

/// <summary>
/// Contains functionality for running any custom command which are not yet implemented.
/// </summary>
public static partial class BuildahAliases
{
    /// <summary>
    /// Run a custom Buildah command.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="command">The custom command.</param>
    /// <returns>A list of strings.</returns>
    [CakeMethodAlias]
    public static IEnumerable<string> BuildahCustomCommand(this ICakeContext context, string command)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        return BuildahCustomCommand(context, new(), command);
    }

    /// <summary>
    /// Run a custom Buildah command.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    /// <param name="command">The custom command.</param>
    /// <returns>A list of strings.</returns>
    [CakeMethodAlias]
    public static IEnumerable<string> BuildahCustomCommand(this ICakeContext context, BuildahCustomCommandSettings? settings, string command)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (settings == null)
        {
            throw new ArgumentNullException(nameof(settings));
        }

        if (string.IsNullOrEmpty(command))
        {
            throw new ArgumentNullException(nameof(command));
        }

        var runner = new GenericBuildahRunner<BuildahCustomCommandSettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);

        var commandName = command;
        var commandArguments = string.Empty;

        var space = command.IndexOf(" ", StringComparison.Ordinal);
        if (space > -1)
        {
            commandName = command[..space];
            commandArguments = command[space..];
        }

        return runner.RunWithResult(
            commandName,
            settings,
            r => r.ToArray(),
            commandArguments);
    }
}
