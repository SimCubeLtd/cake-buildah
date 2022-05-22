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
}
