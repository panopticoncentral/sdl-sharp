namespace Sdl3Sharp;

/// <summary>
/// Types of filesystem entries.
/// </summary>
public enum PathType
{
    /// <summary>Path does not exist.</summary>
    None,
    /// <summary>A normal file.</summary>
    File,
    /// <summary>A directory.</summary>
    Directory,
    /// <summary>Something completely different like a device node (not a symlink, those are always followed).</summary>
    Other
}
