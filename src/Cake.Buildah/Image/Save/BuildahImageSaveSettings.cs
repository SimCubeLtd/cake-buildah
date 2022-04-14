namespace Cake.Buildah;

/// <summary>
/// Settings for Buildah save [OPTIONS] IMAGE [IMAGE...].
/// Save one or more images to a tar archive (streamed to STDOUT by default).
/// </summary>
public sealed class BuildahImageSaveSettings : AutoToolSettings
{
    /// <summary>
    /// Gets or sets --output, -o.
    /// Write to a file, instead of STDOUT.
    /// </summary>
    public string? Output { get; set; }
}
