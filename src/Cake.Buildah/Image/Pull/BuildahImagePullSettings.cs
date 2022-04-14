namespace Cake.Buildah;

/// <summary>
/// Settings for Buildah pull [OPTIONS] NAME[:TAG|@DIGEST].
/// Pull an image or a repository from a registry.
/// </summary>
public sealed class BuildahImagePullSettings : AutoToolSettings
{
    /// <summary>
    /// Gets or sets --all-tags, -a.
    /// default: false.
    /// Download all tagged images in the repository.
    /// </summary>
    public bool? AllTags { get; set; }

    /// <summary>
    /// Gets or sets --disable-content-trust.
    /// default: true.
    /// Skip image verification.
    /// </summary>
    [AutoProperty(Format = Constants.BoolWithTrueDefaultFormat)]
    public bool? DisableContentTrust { get; set; }

    /// <summary>
    /// Gets or sets --platform.
    /// Set platform if server is multi-platform capable.
    /// </summary>
    public string? Platform { get; set; }
}
