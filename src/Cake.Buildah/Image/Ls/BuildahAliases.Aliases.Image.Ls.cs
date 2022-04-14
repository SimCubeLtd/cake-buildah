using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Buildah;

/// <summary>
/// Contains functionality for working with Buildah image ls command.
/// </summary>
public static partial class BuildahAliases
{
    /// <summary>
    /// List images given <paramref name="settings"/>.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    /// <returns>Output text.</returns>
    /// <remarks>Return value are the lines from stdout. This method will redirect stdout and it won't be available for capture.</remarks>
    [CakeMethodAlias]
    public static IEnumerable<string> BuildahImageLs(this ICakeContext context, BuildahImageLsSettings? settings)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var runner = new GenericBuildahRunner<BuildahImageLsSettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
        return runner.RunWithResult("image ls", settings ?? new BuildahImageLsSettings(), r => r.ToArray());
    }
}
