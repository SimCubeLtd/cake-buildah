using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Buildah;

/// <summary>
/// Contains functionality for working with rmi command.
/// </summary>
public static partial class BuildahAliases
{
    /// <summary>
    /// Removes an array of <paramref name="images"/> using default settings.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="images">The list of images.</param>
    [CakeMethodAlias]
    public static void BuildahRmi(this ICakeContext context, params string[] images) => BuildahRemove(context, new(), images);

    /// <summary>
    /// Removes an array of <paramref name="images"/> using the give <paramref name="settings"/>.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    /// <param name="images">The list of images.</param>
    [CakeMethodAlias]
    public static void BuildahRemove(this ICakeContext context, BuildahImageRemoveSettings? settings, params string[] images)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (images == null || images.Length == 0)
        {
            throw new ArgumentNullException(nameof(images));
        }

        var runner = new GenericBuildahRunner<BuildahImageRemoveSettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
        runner.Run("rmi", settings ?? new BuildahImageRemoveSettings(), images);
    }
}
