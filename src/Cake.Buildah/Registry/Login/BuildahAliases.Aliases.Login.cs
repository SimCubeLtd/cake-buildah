using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Buildah;

/// <summary>
/// Contains functionality for working with login command.
/// </summary>
public static partial class BuildahAliases
{
    /// <summary>
    /// Register or log in to a Buildah registry.
    /// If no server is specified, the Buildah engine default is used.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="username">The username.</param>
    /// <param name="password">The password.</param>
    /// <param name="tlsVerify">Perform tls verification. Set to false for Http Registry Servers.</param>
    /// <param name="server">The server.</param>
    [CakeMethodAlias]
    public static void BuildahLogin(this ICakeContext context, string username, string password, bool tlsVerify = false, string? server = null)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentNullException(nameof(username));
        }

        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException(nameof(password));
        }

        BuildahLogin(
            context,
            new()
            {
                Username = username,
                Password = password,
                TlsVerify = tlsVerify,
            },
            server);
    }

    /// <summary>
    /// Register or log in to a Buildah registry.
    /// If no server is specified, the Buildah engine default is used.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    /// <param name="server">The server.</param>
    [CakeMethodAlias]
    public static void BuildahLogin(this ICakeContext context, BuildahRegistryLoginSettings? settings, string? server = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (settings == null)
        {
            throw new ArgumentNullException(nameof(settings));
        }

        var runner = new GenericBuildahRunner<BuildahRegistryLoginSettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
        runner.Run(
            "login",
            settings,
            server is { } ? new[] { server } : Array.Empty<string>());
    }
}
