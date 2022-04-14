namespace Cake.Buildah;

/// <summary>
/// Settings for Buildah load [OPTIONS].
/// Load an image from a tar archive or STDIN.
/// </summary>
public sealed class BuildahImageLoadSettings : AutoToolSettings
{
    /// <summary>
    /// Gets or sets --input, -i.
    /// Read from tar archive file, instead of STDIN.
    /// </summary>
    public string? Input { get; set; }

    /// <summary>
    /// Gets or sets --quiet, -q.
    /// default: false.
    /// Suppress the load output.
    /// </summary>
    public bool? Quiet { get; set; }
}
