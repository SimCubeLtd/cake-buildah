using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Buildah;

/// <summary>
/// Contains functionality for working with pull command.
/// </summary>
public static partial class BuildahAliases
{
    /// <summary>
    /// Pull an image or a repository from the registry.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="imageReference">The image reference.</param>
    [CakeMethodAlias]
    public static void BuildahPull(this ICakeContext context, string imageReference) => BuildahPull(context, new(), imageReference);

    /// <summary>
    /// Pull an image or a repository from the registry with given <paramref name="settings"/>.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    /// <param name="imageReference">The image reference.</param>
    [CakeMethodAlias]
    public static void BuildahPull(this ICakeContext context, BuildahImagePullSettings? settings, string imageReference)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (string.IsNullOrEmpty(imageReference))
        {
            throw new ArgumentNullException(nameof(imageReference));
        }

        var runner = new GenericBuildahRunner<BuildahImagePullSettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
        runner.Run(
            "pull",
            settings ?? new BuildahImagePullSettings(),
            new[]
            {
                imageReference,
            });
    }
}
