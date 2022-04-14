namespace Cake.Buildah;

/// <summary>
/// Settings for Buildah volume ls.
/// </summary>
public sealed class BuildahImageLsSettings : AutoToolSettings
{
    /// <summary>
    /// Gets or sets a value indicating whether show all images (default hides intermediate images).
    /// </summary>
    public bool All { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether show digests.
    /// </summary>
    public bool Digests { get; set; }

    /// <summary>
    /// Gets or sets pretty-print volumes using a Go template.
    /// </summary>
    public string? Format { get; set; }

    /// <summary>
    /// Gets or sets provide filter values (e.g. ‘dangling=true’).
    /// </summary>
    public string? Filter { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether don't truncate output.
    /// </summary>
    public bool NoTrunc { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether only display volume names.
    /// </summary>
    public bool Quiet { get; set; }
}
