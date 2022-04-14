using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Buildah;

/// <summary>
/// Contains functionality for working with push command.
/// </summary>
public static partial class BuildahAliases
{
    /// <summary>
    /// Push an image or a repository to the registry.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="imageReference">The image reference.</param>
    [CakeMethodAlias]
    public static void BuildahPush(this ICakeContext context, string imageReference) => BuildahPush(context, new(), imageReference);

    /// <summary>
    /// Push an image or a repository to the registry with given <paramref name="settings"/>.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    /// <param name="imageReference">The image reference.</param>
    [CakeMethodAlias]
    public static void BuildahPush(this ICakeContext context, BuildahImagePushSettings? settings, string imageReference)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (string.IsNullOrEmpty(imageReference))
        {
            throw new ArgumentNullException(nameof(imageReference));
        }

        var runner = new GenericBuildahRunner<BuildahImagePushSettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
        runner.Run(
            "push",
            settings ?? new BuildahImagePushSettings(),
            new[]
            {
                imageReference,
            });
    }
}
