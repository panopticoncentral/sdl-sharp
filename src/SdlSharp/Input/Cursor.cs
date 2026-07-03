using SdlSharp.Graphics;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Mouse;
using SDL_Cursor = SdlSharp.Native.SDL_Cursor;
using SDL_SystemCursor = SdlSharp.Native.SDL_SystemCursor;

namespace SdlSharp.Input;

/// <summary>
/// A managed wrapper around an SDL mouse cursor (SDL_Cursor).
/// </summary>
public sealed unsafe class Cursor : IDisposable
{
    private readonly bool _ownsHandle;

    /// <summary>
    /// The underlying native SDL_Cursor pointer.
    /// </summary>
    internal SDL_Cursor* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private SDL_Cursor* _handle;

    internal Cursor(SDL_Cursor* handle, bool ownsHandle = true)
    {
        _handle = handle;
        _ownsHandle = ownsHandle;
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
    /// mirroring SDL_SetCursor(NULL).
    /// </summary>
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
            SDL_DestroyCursor(_handle);
        }
        _handle = null;
    }
}
