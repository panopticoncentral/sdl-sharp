namespace SdlSharp.IO;

/// <summary>The kind of entry in a storage container.</summary>
public enum StoragePathType
{
    /// <summary>No entry exists.</summary>
    None = Native.SDL_PathType.SDL_PATHTYPE_NONE,
    /// <summary>A regular file.</summary>
    File = Native.SDL_PathType.SDL_PATHTYPE_FILE,
    /// <summary>A directory.</summary>
    Directory = Native.SDL_PathType.SDL_PATHTYPE_DIRECTORY,
    /// <summary>Another kind of entry, such as a device.</summary>
    Other = Native.SDL_PathType.SDL_PATHTYPE_OTHER
}

/// <summary>Copied storage metadata. Timestamp precision is limited to .NET's 100-nanosecond ticks; null represents SDL's zero/unspecified timestamp.</summary>
/// <param name="Type">Entry kind.</param>
/// <param name="Size">Size in bytes.</param>
/// <param name="CreationTime">Creation time as an unambiguous instant.</param>
/// <param name="LastWriteTime">Last modification time.</param>
/// <param name="LastAccessTime">Last access time.</param>
public readonly record struct StoragePathInfo(StoragePathType Type, ulong Size,
    DateTimeOffset? CreationTime = null, DateTimeOffset? LastWriteTime = null, DateTimeOffset? LastAccessTime = null)
{
    internal static StoragePathInfo FromNative(Native.SDL_PathInfo value) => new((StoragePathType)value.type,
        value.size, FromTime(value.create_time), FromTime(value.modify_time),
        FromTime(value.access_time));
    private static DateTimeOffset? FromTime(long time) => time == 0 ? null : SdlDateTime.ToDateTimeOffset(time);
    private static long ToTime(DateTimeOffset? time) => time.HasValue ? SdlDateTime.FromDateTimeOffset(time.Value) : 0;
    internal Native.SDL_PathInfo ToNative() => new()
    {
        type = (Native.SDL_PathType)Type, size = Size,
        create_time = ToTime(CreationTime),
        modify_time = ToTime(LastWriteTime),
        access_time = ToTime(LastAccessTime)
    };
}
