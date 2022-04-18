namespace Cake.Buildah;

/// <summary>
/// Represents an agnostic argument.
/// </summary>
public readonly struct BuildahArgument : IEquatable<BuildahArgument>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BuildahArgument"/> struct.
    /// Constructor.
    /// </summary>
    /// <param name="key">the key.</param>
    /// <param name="value">the value.</param>
    /// <param name="quoting">Quoting style.</param>
    public BuildahArgument(string? key, string? value, BuildahArgumentQuoting quoting)
    {
        Key = key;
        Value = value;
        Quoting = quoting;
    }

    /// <summary>
    /// Gets key part of the argument.
    /// </summary>
    public string? Key { get; }

    /// <summary>
    /// Gets value part of the argument.
    /// </summary>
    public string? Value { get; }

    /// <summary>
    /// Gets the required quoting mode.
    /// </summary>
    public BuildahArgumentQuoting Quoting { get; }

    /// <summary>
    /// Compare two instances.
    /// </summary>
    /// <param name="left">An instance of <see cref="BuildahArgument"/>.</param>
    /// <param name="right">Another instance of <see cref="BuildahArgument"/>.</param>
    /// <returns>True if match, false if not.</returns>
    public static bool operator ==(BuildahArgument left, BuildahArgument right) => left.Equals(right);

    /// <summary>
    /// Compare two instances.
    /// </summary>
    /// <param name="left">An instance of <see cref="BuildahArgument"/>.</param>
    /// <param name="right">Another instance of <see cref="BuildahArgument"/>.</param>
    /// <returns>True if not match, false if match.</returns>
    public static bool operator !=(BuildahArgument left, BuildahArgument right) => !(left == right);

    /// <summary>
    /// Compares this instance to another instance of <see cref="BuildahArgument"/>.
    /// </summary>
    /// <param name="obj">the instance of <see cref="BuildahArgument"/> to compare to this instance.</param>
    /// <returns>true if match, false if not.</returns>
    public override bool Equals(object? obj) => obj is BuildahArgument other && Equals(other);

    /// <summary>
    /// Compares this instance to another instance of <see cref="BuildahArgument"/>.
    /// </summary>
    /// <returns>true if match, false if not.</returns>
    public bool Equals() => Equals(default);

    /// <summary>
    /// Compares this instance to another instance of <see cref="BuildahArgument"/>.
    /// </summary>
    /// <param name="other">the other instance of <see cref="BuildahArgument"/> to compare to.</param>
    /// <returns>true if match, false if not.</returns>
    public bool Equals(BuildahArgument other)
    {
        var (key, value, quoting) = other;

        return Key == key && Value == value && Quoting == quoting;
    }

    /// <summary>
    /// Gets the has code.
    /// </summary>
    /// <returns>the hash code for the current instance.</returns>
    public override int GetHashCode() => HashCode.Combine(Key, Value, (int)Quoting);

    /// <summary>
    /// Deconstruction Support.
    /// </summary>
    /// <param name="key">the key.</param>
    /// <param name="value">the value.</param>
    /// <param name="quoting">The Quote style.</param>
    public void Deconstruct(out string? key, out string? value, out BuildahArgumentQuoting quoting)
    {
        key = Key;
        value = Value;
        quoting = Quoting;
    }
}
