using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Buildah;

/// <summary>
/// Contains functionality for working with logout command.
/// </summary>
public static partial class BuildahAliases
{
    /// <summary>
    /// Logout from a Buildah registry.
    /// If no server is specified, the Buildah engine default is used.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="server">The server.</param>
    [CakeMethodAlias]
    public static void BuildahLogout(this ICakeContext context, string? server = null) => BuildahLogout(context, new(), server);

    /// <summary>
    /// Logout from a Buildah registry.
    /// If no server is specified, the Buildah engine default is used.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    /// <param name="server">The server.</param>
    [CakeMethodAlias]
    public static void BuildahLogout(this ICakeContext context, BuildahRegistryLogoutSettings? settings, string? server = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (settings == null)
        {
            throw new ArgumentNullException(nameof(settings));
        }

        var runner = new GenericBuildahRunner<BuildahRegistryLogoutSettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
        runner.Run(
            "logout",
            settings,
            server is { } ? new[] { server } : Array.Empty<string>());
    }
}
