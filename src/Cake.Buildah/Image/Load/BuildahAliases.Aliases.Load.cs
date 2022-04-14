using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Buildah;

/// <summary>
/// Contains functionality for working with load command.
/// </summary>
public static partial class BuildahAliases
{
    /// <summary>
    /// Load an image from a tar archive or STDIN.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    [CakeMethodAlias]
    public static void BuildahLoad(this ICakeContext context, BuildahImageLoadSettings? settings)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var runner = new GenericBuildahRunner<BuildahImageLoadSettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
        runner.Run("load", settings ?? new BuildahImageLoadSettings(), Array.Empty<string>());
    }
}
