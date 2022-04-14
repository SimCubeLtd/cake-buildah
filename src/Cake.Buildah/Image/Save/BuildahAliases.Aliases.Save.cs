using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Buildah;

/// <summary>
/// Contains functionality for working with save command.
/// </summary>
public static partial class BuildahAliases
{
    /// <summary>
    /// Save one or more images to a tar archive (streamed to STDOUT by default).
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    /// <param name="images">The list of images.</param>
    [CakeMethodAlias]
    public static void BuildahSave(this ICakeContext context, BuildahImageSaveSettings? settings, params string[] images)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (images == null || images.Length == 0)
        {
            throw new ArgumentNullException(nameof(images));
        }

        var runner = new GenericBuildahRunner<BuildahImageSaveSettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
        runner.Run("save", settings ?? new BuildahImageSaveSettings(), images);
    }
}
