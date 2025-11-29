namespace Sdl3Sharp;

/// <summary>
/// Flags for path matching.
/// </summary>
[Flags]
public enum GlobFlags : uint
{
    /// <summary>No flags set.</summary>
    None = 0,
    /// <summary>Case-insensitive matching.</summary>
    CaseInsensitive = (1u << 0)
}
