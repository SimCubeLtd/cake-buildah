using System.Diagnostics.CodeAnalysis;
using Cake.Core.Tooling;

namespace Cake.Buildah;

/// <summary>
/// Base class for tooling that is used for auto generation of command line arguments.
/// </summary>
[SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "I'm choosing to.")]
[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "I'm choosing to.")]
[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "I'm choosing to.")]
public abstract class AutoToolSettings : ToolSettings
{
    /// <summary>
    /// Gets or sets values of these properties shouldn't be displayed in the output.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public string[] SecretProperties;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoToolSettings"/> class.
    /// </summary>
    [SuppressMessage("Usage", "CA2214:Do not call overridable methods in constructors", Justification = "We are allowing this.")]
    protected AutoToolSettings() => SecretProperties = CollectSecretProperties().ToArray();

    /// <summary>
    /// Collects secret properties.
    /// </summary>
    /// <returns>a Array of strings.</returns>
    protected virtual IEnumerable<string> CollectSecretProperties() => Array.Empty<string>();
}
