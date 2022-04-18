using System.Diagnostics.CodeAnalysis;

namespace Cake.Buildah;

/// <summary>
/// Settings for Buildah build [OPTIONS] PATH | URL | -.
/// Build an image from a Dockerfile.
/// </summary>
[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Its fine. Arrays will be small.")]
public sealed class BuildahImageBuildSettings : AutoToolSettings
{
    /// <summary>
    /// Gets or sets --build-arg.
    /// Set build-time variables.
    /// </summary>
    public string[]? BuildArg { get; set; }

    /// <summary>
    /// Gets or sets --cache-from.
    /// default:
    /// Images to consider as cache sources.
    /// </summary>
    public string[]? CacheFrom { get; set; }

    /// <summary>
    /// Gets or sets --disable-content-trust.
    /// default: true.
    /// Skip image verification.
    /// </summary>
    [AutoProperty(Format = Constants.BoolWithTrueDefaultFormat)]
    public bool? DisableContentTrust { get; set; }

    /// <summary>
    /// Gets or sets --file, -f.
    /// Name of the Buildahfile (Default is &#39;PATH/Dockerfile&#39;).
    /// </summary>
    public string? File { get; set; }

    /// <summary>
    /// Gets or sets --no-cache.
    /// default: false.
    /// Do not use cache when building the image.
    /// </summary>
    public bool? NoCache { get; set; }

    /// <summary>
    /// Gets or sets --platform.
    /// Set platform if server is multi-platform capable.
    /// </summary>
    public string? Platform { get; set; }

    /// <summary>
    /// Gets or sets --tag, -t.
    /// Name and optionally a tag in the &#39;name:tag&#39; format.
    /// </summary>
    public string[]? Tag { get; set; }
}
