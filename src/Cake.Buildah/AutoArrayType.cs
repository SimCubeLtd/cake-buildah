namespace Cake.Buildah;

/// <summary>
/// Array representation in command line.
/// </summary>
public enum AutoArrayType
{
    /// <summary>
    /// A key-value pair for each value.
    /// </summary>
    /// <example>--property=value1 --property=value2.</example>
    MultipleInstances,

    /// <summary>
    /// A single property with multiple values.
    /// </summary>
    /// <example>--property=value1,value2.</example>
    List,
}
