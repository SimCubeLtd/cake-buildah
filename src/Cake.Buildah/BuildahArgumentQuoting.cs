namespace Cake.Buildah;

/// <summary>
/// Quoting mode.
/// </summary>
public enum BuildahArgumentQuoting
{
    /// <summary>
    /// No quoting.
    /// </summary>
    Unquoted,

    /// <summary>
    /// Quotes.
    /// </summary>
    Quoted,

    /// <summary>
    /// A quoted secret.
    /// </summary>
    QuotedSecret,
}
