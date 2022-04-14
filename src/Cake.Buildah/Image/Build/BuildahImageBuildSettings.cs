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
    /// Gets or sets --add-host.
    /// Add a custom host-to-IP mapping (host:ip).
    /// </summary>
    public string[]? AddHost { get; set; }

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
    /// Gets or sets --cgroup-parent.
    /// Optional parent cgroup for the container.
    /// </summary>
    public string? CgroupParent { get; set; }

    /// <summary>
    /// Gets or sets --compress.
    /// default: false.
    /// Compress the build context using gzip.
    /// </summary>
    public bool? Compress { get; set; }

    /// <summary>
    /// Gets or sets --cpu-period.
    /// default: 0.
    /// Limit the CPU CFS (Completely Fair Scheduler) period.
    /// </summary>
    public long? CpuPeriod { get; set; }

    /// <summary>
    /// Gets or sets --cpu-quota.
    /// default: 0.
    /// Limit the CPU CFS (Completely Fair Scheduler) quota.
    /// </summary>
    public long? CpuQuota { get; set; }

    /// <summary>
    /// Gets or sets --cpuset-cpus.
    /// CPUs in which to allow execution (0-3, 0,1).
    /// </summary>
    public string? CpusetCpus { get; set; }

    /// <summary>
    /// Gets or sets --cpuset-mems.
    /// MEMs in which to allow execution (0-3, 0,1).
    /// </summary>
    public string? CpusetMems { get; set; }

    /// <summary>
    /// Gets or sets --cpu-shares, -c.
    /// default: 0.
    /// CPU shares (relative weight).
    /// </summary>
    public long? CpuShares { get; set; }

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
    /// Gets or sets --force-rm.
    /// default: false.
    /// Always remove intermediate containers.
    /// </summary>
    public bool? ForceRm { get; set; }

    /// <summary>
    /// Gets or sets --iidfile.
    /// Write the image ID to the file.
    /// </summary>
    public string? Iidfile { get; set; }

    /// <summary>
    /// Gets or sets --isolation.
    /// Container isolation technology.
    /// </summary>
    public string? Isolation { get; set; }

    /// <summary>
    /// Gets or sets --label.
    /// Set metadata for an image.
    /// </summary>
    public string[]? Label { get; set; }

    /// <summary>
    /// Gets or sets --memory, -m.
    /// Memory limit.
    /// </summary>
    public string? Memory { get; set; }

    /// <summary>
    /// Gets or sets --memory-swap.
    /// Swap limit equal to memory plus swap: &#39;-1&#39; to enable unlimited swap.
    /// </summary>
    public string? MemorySwap { get; set; }

    /// <summary>
    /// Gets or sets --network.
    /// default: default.
    /// Set the networking mode for the RUN instructions during build.
    /// </summary>
    /// <remarks>
    /// Version: 1.25.
    /// </remarks>
    public string? Network { get; set; }

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
    /// Gets or sets --pull.
    /// default: false.
    /// Always attempt to pull a newer version of the image.
    /// </summary>
    public bool? Pull { get; set; }

    /// <summary>
    /// Gets or sets --quiet, -q.
    /// default: false.
    /// Suppress the build output and print image ID on success.
    /// </summary>
    public bool? Quiet { get; set; }

    /// <summary>
    /// Gets or sets --rm.
    /// default: true.
    /// Remove intermediate containers after a successful build.
    /// </summary>
    [AutoProperty(Format = Constants.BoolWithTrueDefaultFormat)]
    public bool? Rm { get; set; }

    /// <summary>
    /// Gets or sets --security-opt.
    /// default:.
    /// Security options.
    /// </summary>
    public string[]? SecurityOpt { get; set; }

    /// <summary>
    /// Gets or sets --shm-size.
    /// Size of /dev/shm.
    /// </summary>
    public string? ShmSize { get; set; }

    /// <summary>
    /// Gets or sets --squash.
    /// default: false.
    /// Squash newly built layers into a single new layer.
    /// </summary>
    /// <remarks>
    /// Experimental.
    /// Version: 1.25.
    /// </remarks>
    public bool? Squash { get; set; }

    /// <summary>
    /// Gets or sets --stream.
    /// default: false.
    /// Stream attaches to server to negotiate build context.
    /// </summary>
    /// <remarks>
    /// Experimental.
    /// Version: 1.31.
    /// </remarks>
    public bool? Stream { get; set; }

    /// <summary>
    /// Gets or sets --tag, -t.
    /// Name and optionally a tag in the &#39;name:tag&#39; format.
    /// </summary>
    public string[]? Tag { get; set; }

    /// <summary>
    /// Gets or sets --target.
    /// Set the target build stage to build.
    /// </summary>
    public string? Target { get; set; }

    /// <summary>
    /// Gets or sets --ulimit.
    /// Ulimit options.
    /// </summary>
    public string[]? Ulimit { get; set; }
}
