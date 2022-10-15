namespace Cake.Buildah;

/// <summary>
/// Settings for Buildah login [OPTIONS] [SERVER].
/// Log in to a Buildah registry.
/// </summary>
public sealed partial class BuildahRegistryLoginSettings : AutoToolSettings
{
    /// <summary>
    /// Gets or sets --username, -u.
    /// Username.
    /// </summary>
    [AutoProperty(Format = "--username {1}", PreCommand = false)]
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets --password, -p.
    /// Password.
    /// </summary>
    [AutoProperty(Format = "--password {1}", PreCommand = false)]
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets --tls-verify.
    /// default: false.
    /// .
    /// </summary>
    [AutoProperty(Format = "--tls-verify={1}", PreCommand = false)]
    public bool? TlsVerify { get; set; }

    /// <summary>
    /// Gets or sets --password-stdin.
    /// default: false.
    /// Take the password from stdin.
    /// </summary>
    public bool? PasswordStdin { get; set; }
}
