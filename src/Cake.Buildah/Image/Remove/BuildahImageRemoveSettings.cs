namespace Cake.Buildah;

/// <summary>
/// Settings for Buildah rmi [OPTIONS] IMAGE [IMAGE...].
/// Remove one or more images.
/// </summary>
public sealed partial class BuildahImageRemoveSettings : AutoToolSettings
{
    /// <summary>
    /// Gets or sets --force, -f.
    /// default: false.
    /// Force removal of the image.
    /// </summary>
    public bool? Force { get; set; }

    /// <summary>
    /// Gets or sets --no-prune.
    /// default: false.
    /// Do not delete untagged parents.
    /// </summary>
    public bool? NoPrune { get; set; }
}
