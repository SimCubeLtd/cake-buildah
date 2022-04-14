using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Buildah;

/// <summary>
/// Contains functionality for working with tag command.
/// </summary>
public static partial class BuildahAliases
{
    /// <summary>
    /// Tag an image into a repository.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="imageReference">The image reference.</param>
    /// <param name="registryReference">The registry reference.</param>
    [CakeMethodAlias]
    public static void BuildahTag(this ICakeContext context, string imageReference, string registryReference)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (string.IsNullOrEmpty(imageReference))
        {
            throw new ArgumentNullException(nameof(imageReference));
        }

        if (string.IsNullOrEmpty(registryReference))
        {
            throw new ArgumentNullException(nameof(registryReference));
        }

        var runner = new GenericBuildahRunner<BuildahImageTagSettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
        runner.Run(
            "tag",
            new(),
            new[]
            {
                imageReference,
                registryReference,
            });
    }
}
