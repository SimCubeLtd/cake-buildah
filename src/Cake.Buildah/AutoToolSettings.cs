using System.Collections.ObjectModel;
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
    public ReadOnlyCollection<string>? SecretProperties { get; set; }

    /// <summary>
    /// Sets the Collect of secret properties.
    /// </summary>
    /// <param name="properties">The properties to pass through.</param>
    public void SetSecretProperties(IEnumerable<string> properties) =>
        SecretProperties = properties.ToList().AsReadOnly();
}
