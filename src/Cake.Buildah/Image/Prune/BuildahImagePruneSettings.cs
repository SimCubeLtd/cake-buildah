namespace Cake.Buildah;

/// <summary>
/// Settings for Buildah prune [OPTIONS].
/// Remove unused images.
/// </summary>
public sealed class BuildahImagePruneSettings : AutoToolSettings
{
    /// <summary>
    /// Gets or sets --all, -a.
    /// default: false.
    /// Remove all unused images, not just dangling ones.
    /// </summary>
    public bool? All { get; set; }

    /// <summary>
    /// Gets or sets --filter.
    /// Provide filter values (e.g. &#39;until=&lt;timestamp&gt;&#39;).
    /// </summary>
    public string? Filter { get; set; }

    /// <summary>
    /// Gets or sets --force, -f.
    /// default: false.
    /// Do not prompt for confirmation.
    /// </summary>
    public bool? Force { get; set; }
}
