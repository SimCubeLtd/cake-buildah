namespace Cake.Buildah;

/// <summary>
/// Settings for Buildah login [OPTIONS] [SERVER].
/// Log in to a Buildah registry.
/// </summary>
public sealed partial class BuildahRegistryLoginSettings : AutoToolSettings
{
    /// <summary>
    /// Gets or sets --password, -p.
    /// Password.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets --password-stdin.
    /// default: false.
    /// Take the password from stdin.
    /// </summary>
    public bool? PasswordStdin { get; set; }

    /// <summary>
    /// Gets or sets --username, -u.
    /// Username.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Adds <see cref="Password"/> to secret properties.
    /// </summary>
    /// <returns>A collection of secret properties.</returns>
    protected override IEnumerable<string> CollectSecretProperties() => new[]
    {
        nameof(Password),
    };
}
