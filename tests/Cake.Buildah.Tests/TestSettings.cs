namespace Cake.Buildah.Tests;

public class TestSettings : AutoToolSettings
{
    public string? String { get; set; }

    public string[]? Strings { get; set; }

    [AutoProperty(AutoArrayType = AutoArrayType.List)]
    public string[]? ListStrings { get; set; }

    public string? Password { get; set; }

    public int? NullableInt { get; set; }

    public long? NullableInt64 { get; set; }

    public ulong? NullableUInt64 { get; set; }

    public ushort? NullableUInt16 { get; set; }

    public bool? NullableBool { get; set; }

    public TimeSpan? NullableTimeSpan { get; set; }

    public bool Bool { get; set; }

    [AutoProperty(Format = "-s {1}")]
    public string? DecoratedString { get; set; }

    [AutoProperty(Format = "-v", OnlyWhenTrue = true)]
    public bool DecoratedBool { get; set; }

    [AutoProperty(Format = "-e {1}")]
    public string[]? DecoratedStrings { get; set; }

    [AutoProperty(PreCommand = true)]
    public string? PreCommandValue { get; set; }

    protected override IEnumerable<string> CollectSecretProperties() =>
        new[]
        {
            nameof(Password),
        };
}
