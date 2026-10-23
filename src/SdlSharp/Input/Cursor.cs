using SdlSharp.Graphics;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Mouse;
// Aliases instead of `using SdlSharp.Native;`, which would make `Surface` ambiguous (CS0104)
// between SdlSharp.Graphics.Surface and the SdlSharp.Native.Surface binding class.
using SDL_Cursor = SdlSharp.Native.SDL_Cursor;
using SDL_SystemCursor = SdlSharp.Native.SDL_SystemCursor;

namespace SdlSharp.Input;

/// <summary>
/// A managed wrapper around an SDL mouse cursor (SDL_Cursor).
/// </summary>
public sealed unsafe class Cursor : IDisposable
{
    private readonly bool _ownsHandle;
    internal readonly NativeLifetime Lifetime;
    private readonly NativeLifetime? _viewParent;

    /// <summary>
    /// The underlying native SDL_Cursor pointer.
    /// </summary>
    internal SDL_Cursor* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            Lifetime.Check();
            _viewParent?.Check();
            return _handle;
        }
    }

    private SDL_Cursor* _handle;

    internal Cursor(SDL_Cursor* handle, bool ownsHandle = true, NativeLifetime? parent = null)
    {
        _handle = handle;
        Lifetime = NativeLifetime.Get(typeof(Cursor), (nint)handle, ownsHandle, threadAffine: true, parent: parent);
        _ownsHandle = ownsHandle;
        _viewParent = ownsHandle ? null : parent;
    }

    /// <summary>
    /// Creates a standard system cursor.
    /// </summary>
    /// <param name="id">The system cursor shape.</param>
    /// <returns>A new cursor. Dispose it when no longer needed.</returns>
    public static Cursor CreateSystem(SystemCursor id) =>
        new(Check(SDL_CreateSystemCursor((SDL_SystemCursor)id)));

    /// <summary>
    /// Creates a color cursor from a surface.
    /// </summary>
    /// <param name="surface">The cursor image.</param>
    /// <param name="hotX">The X position of the cursor hot spot.</param>
    /// <param name="hotY">The Y position of the cursor hot spot.</param>
    /// <returns>A new cursor. Dispose it when no longer needed.</returns>
    public static Cursor CreateColor(Surface surface, int hotX, int hotY) =>
        new(Check(SDL_CreateColorCursor(surface.Handle, hotX, hotY)));

    /// <summary>
    /// Gets or sets the active cursor. The getter returns a non-owning wrapper
    /// (or null when no cursor is set). Setting null forces a cursor redraw,
    /// mirroring SDL_SetCursor(NULL). Setting null does not hide the cursor —
    /// use <see cref="Mouse.HideCursor"/> for that.
    /// </summary>
    /// <seealso cref="Mouse.HideCursor"/>
    public static Cursor? Current
    {
        get
        {
            var handle = SDL_GetCursor();
            return handle == null ? null : new Cursor(handle, ownsHandle: false);
        }
        set => Check(SDL_SetCursor(value == null ? null : value.Handle));
    }

    /// <summary>
    /// Gets the default cursor as a non-owning wrapper.
    /// </summary>
    public static Cursor Default => new(Check(SDL_GetDefaultCursor()), ownsHandle: false);

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && _handle != null)
        {
            Lifetime.Release(() =>
            {
                SDL_DestroyCursor(_handle);
            });
        }
        _handle = null;
    }
    /// <summary>Creates a monochrome cursor. Width must be a multiple of eight and each buffer must contain width × height / 8 bytes.</summary>
    public static Cursor CreateBitmap(ReadOnlySpan<byte> data, ReadOnlySpan<byte> mask, int width, int height, int hotX, int hotY)
    {
        if (width <= 0 || width % 8 != 0 || height <= 0) throw new ArgumentOutOfRangeException(nameof(width));
        var length = checked(width / 8 * height);
        if (data.Length < length || mask.Length < length) throw new ArgumentException("Cursor bitmap or mask is too short.");
        fixed (byte* d = data) fixed (byte* m = mask) return new(Check(SDL_CreateCursor(d, m, width, height, hotX, hotY)));
    }
    /// <summary>Creates an animated cursor, copying the supplied frame surfaces.</summary>
    public static Cursor CreateAnimated(ReadOnlySpan<CursorFrame> frames, int hotX, int hotY)
    {
        if (frames.IsEmpty) throw new ArgumentException("At least one frame is required.", nameof(frames));
        var native = new Native.SDL_CursorFrameInfo[frames.Length];
        for (var i = 0; i < frames.Length; i++) native[i] = new() { surface = frames[i].Surface.Handle, duration = frames[i].DurationMilliseconds };
        fixed (Native.SDL_CursorFrameInfo* p = native) return new(Check(SDL_CreateAnimatedCursor(p, frames.Length, hotX, hotY)));
    }
}
