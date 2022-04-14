namespace Cake.Buildah;

/// <summary>
/// Bridge type.
/// </summary>
public static class BuildahBridge
{
    /// <summary>
    /// The bridge network represents the Buildah0 network present in all Buildah installations.
    /// </summary>
    public const string Bridge = "bridge";

    /// <summary>
    /// The none network adds a container to a container-specific network stack.
    /// </summary>
    public const string None = "none";

    /// <summary>
    /// The host network adds a container on the hosts network stack.
    /// </summary>
    public const string Host = "host";
}
