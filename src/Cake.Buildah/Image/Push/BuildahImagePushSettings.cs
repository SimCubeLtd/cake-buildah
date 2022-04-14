namespace Cake.Buildah;

/// <summary>
/// Settings for Buildah push [OPTIONS] NAME[:TAG].
/// Push an image or a repository to a registry.
/// </summary>
public sealed class BuildahImagePushSettings : AutoToolSettings
{
    /// <summary>
    /// Gets or sets --disable-content-trust.
    /// default: true.
    /// Skip image signing.
    /// </summary>
    [AutoProperty(Format = Constants.BoolWithTrueDefaultFormat)]
    public bool? DisableContentTrust { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether push all tagged images in the repository.
    /// </summary>
    public bool AllTags { get; set; }
}
