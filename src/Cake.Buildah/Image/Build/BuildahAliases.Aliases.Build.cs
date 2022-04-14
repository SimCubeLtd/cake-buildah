using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Buildah;

/// <summary>
/// Contains functionality for working with build command.
/// </summary>
public static partial class BuildahAliases
{
    /// <summary>
    /// Builds an image using default settings.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="path">The path.</param>
    [CakeMethodAlias]
    public static void BuildahBuild(this ICakeContext context, string path) => BuildahBuild(context, new(), path);

    /// <summary>
    /// Builds an image given <paramref name="settings"/>.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    /// <param name="path">The path.</param>
    [CakeMethodAlias]
    public static void BuildahBuild(this ICakeContext context, BuildahImageBuildSettings? settings, string path)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(nameof(path));
        }

        var runner = new GenericBuildahRunner<BuildahImageBuildSettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);

        // quote path if not already quoted
        string quotedPath;
        if (!string.IsNullOrEmpty(path))
        {
            var trimmed = path.Trim();
            if (trimmed.Length > 1 && trimmed.StartsWith("\"", StringComparison.OrdinalIgnoreCase) && trimmed.EndsWith("\"", StringComparison.OrdinalIgnoreCase))
            {
                quotedPath = path;
            }
            else
            {
                quotedPath = $"\"{path}\"";
            }
        }
        else
        {
            quotedPath = path;
        }

        runner.Run(
            "bud",
            settings ?? new BuildahImageBuildSettings(),
            new[]
            {
                quotedPath,
            });
    }
}
