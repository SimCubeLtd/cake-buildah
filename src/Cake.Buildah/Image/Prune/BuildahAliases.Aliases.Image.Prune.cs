using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Buildah;

/// <summary>
/// Contains functionality for working with Buildah image prune command.
/// </summary>
public static partial class BuildahAliases
{
    /// <summary>
    /// Remove unused images.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>A list of strings.</returns>
    [CakeMethodAlias]
    public static IEnumerable<string> BuildahImagePrune(this ICakeContext context) => BuildahImagePrune(context, null);

    /// <summary>
    /// Remove unused images given <paramref name="settings"/>.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    /// <returns>Output text.</returns>
    /// <remarks>Return value are the lines from stdout. This method will redirect stdout and it won't be available for capture.</remarks>
    [CakeMethodAlias]
    public static IEnumerable<string> BuildahImagePrune(this ICakeContext context, BuildahImagePruneSettings? settings)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var runner = new GenericBuildahRunner<BuildahImagePruneSettings>(
            context.FileSystem,
            context.Environment,
            context.ProcessRunner,
            context.Tools);
        return runner.RunWithResult("rm", settings ?? new BuildahImagePruneSettings(), r => r.ToArray());
    }
}
