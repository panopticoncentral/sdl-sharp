using static Sdl3Sharp.Native.Version;

namespace Sdl3Sharp;

/// <summary>
/// A version number for a component.
/// </summary>
public readonly record struct Version(int Value)
{
    /// <summary>
    /// Initializes a new instance of the Version class with the specified major, minor, and micro version numbers.
    /// </summary>
    /// <param name="major">The major version number. Must be a non-negative integer.</param>
    /// <param name="minor">The minor version number. Must be a non-negative integer.</param>
    /// <param name="micro">The micro (patch) version number. Must be a non-negative integer.</param>
    public Version(int major, int minor, int micro)
        : this(SDL_VERSIONNUM(major, minor, micro))
    {
    }

    /// <summary>
    /// The major version.
    /// </summary>
    public int Major => SDL_VERSIONNUM_MAJOR(Value);

    /// <summary>
    /// The minor version.
    /// </summary>
    public int Minor => SDL_VERSIONNUM_MINOR(Value);

    /// <summary>
    /// The micro version.
    /// </summary>
    public int Micro => SDL_VERSIONNUM_MICRO(Value);

    /// <summary>
    /// Converts the version to a string.
    /// </summary>
    /// <returns>The version as a string.</returns>
    public override string ToString()
    {
        return $"{Major}.{Minor}.{Micro}";
    }

    /// <summary>
    /// Compares two versions for less than.
    /// </summary>
    /// <param name="left">The left version.</param>
    /// <param name="right">The right version.</param>
    /// <returns>true if the left version is less than the right version; otherwise, false.</returns>
    public static bool operator <(Version left, Version right)
    {
        return left.Value < right.Value;
    }

    /// <summary>
    /// Compares two versions for greater than.
    /// </summary>
    /// <param name="left">The left version.</param>
    /// <param name="right">The right version.</param>
    /// <returns>true if the left version is greater than the right version; otherwise, false.</returns>
    public static bool operator >(Version left, Version right)
    {
        return left.Value > right.Value;
    }

    /// <summary>
    /// Compares two versions for less than or equal to.
    /// </summary>
    /// <param name="left">The left version.</param>
    /// <param name="right">The right version.</param>
    /// <returns>true if the left version is less than or equal to the right version; otherwise, false.</returns>
    public static bool operator <=(Version left, Version right)
    {
        return left.Value <= right.Value;
    }

    /// <summary>
    /// Compares two versions for greater than or equal to.
    /// </summary>
    /// <param name="left">The left version.</param>
    /// <param name="right">The right version.</param>
    /// <returns>true if the left version is greater than or equal to the right version; otherwise, false.</returns>
    public static bool operator >=(Version left, Version right)
    {
        return left.Value >= right.Value;
    }
}
