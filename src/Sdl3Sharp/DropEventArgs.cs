using System.Runtime.InteropServices;
using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp;

/// <summary>
/// Event arguments for drop events.
/// </summary>
public sealed unsafe class DropEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the window ID that the drop was performed on.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the X coordinate of the drop position, relative to the window.
    /// </summary>
    public float X { get; }

    /// <summary>
    /// Gets the Y coordinate of the drop position, relative to the window.
    /// </summary>
    public float Y { get; }

    /// <summary>
    /// Gets the source application that sent this drop event, or null if not available.
    /// </summary>
    public string? Source { get; }

    /// <summary>
    /// Gets the dropped data (file path or text), or null for begin/complete events.
    /// </summary>
    public string? Data { get; }

    internal DropEventArgs(SDL_DropEvent e) : base(e.timestamp)
    {
        WindowId = e.windowID;
        X = e.x;
        Y = e.y;
        Source = e.source != null ? Marshal.PtrToStringUTF8((nint)e.source) : null;
        Data = e.data != null ? Marshal.PtrToStringUTF8((nint)e.data) : null;
    }
}
