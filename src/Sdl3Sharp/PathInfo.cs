namespace Sdl3Sharp;

/// <summary>
/// Information about a path on the filesystem.
/// </summary>
public readonly struct PathInfo
{
    /// <summary>
    /// Gets the path type.
    /// </summary>
    public PathType Type { get; init; }

    /// <summary>
    /// Gets the file size in bytes.
    /// </summary>
    public ulong Size { get; init; }

    /// <summary>
    /// Gets the time when the path was created.
    /// </summary>
    public DateTimeOffset CreateTime { get; init; }

    /// <summary>
    /// Gets the last time the path was modified.
    /// </summary>
    public DateTimeOffset ModifyTime { get; init; }

    /// <summary>
    /// Gets the last time the path was read.
    /// </summary>
    public DateTimeOffset AccessTime { get; init; }

    internal PathInfo(Native.Filesystem.SDL_PathInfo info)
    {
        Type = (PathType)info.type;
        Size = info.size;
        CreateTime = DateTimeOffset.FromUnixTimeSeconds(info.create_time / 1000000000);
        ModifyTime = DateTimeOffset.FromUnixTimeSeconds(info.modify_time / 1000000000);
        AccessTime = DateTimeOffset.FromUnixTimeSeconds(info.access_time / 1000000000);
    }
}
