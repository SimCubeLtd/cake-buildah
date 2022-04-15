using System.Collections;

namespace Cake.Buildah.Tests;

public class BuildahArgumentComparer : IComparer
{
    public int Compare(object? x, object? y)
    {
        if (x is not BuildahArgument(var key, var value, var quoting))
        {
            throw new ArgumentException(nameof(x));
        }

        if (y is not BuildahArgument(var s, var value1, var buildahArgumentQuoting))
        {
            throw new ArgumentNullException(nameof(y));
        }

        var keyCompare = CompareStrings(key, s);
        if (keyCompare != 0)
        {
            return keyCompare;
        }

        var valueCompare = CompareStrings(value, value1);
        if (valueCompare != 0)
        {
            return valueCompare;
        }

        return quoting.CompareTo(buildahArgumentQuoting);
    }

    private static int CompareStrings(string? a, string? b)
    {
        if (a == b)
        {
            return 0;
        }

        if (a == null)
        {
            return 1;
        }

        if (b == null)
        {
            return -1;
        }

        return string.Compare(a, b, StringComparison.Ordinal);
    }
}
