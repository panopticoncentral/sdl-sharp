using Sdl3Sharp.Graphics;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Mouse;

namespace Sdl3Sharp.Input;

/// <summary>
/// Represents an SDL cursor.
/// </summary>
public sealed unsafe class Cursor : IDisposable
{
    private bool _disposed;
    private readonly bool _ownsHandle;

    /// <summary>
    /// Gets the underlying SDL_Cursor pointer.
    /// </summary>
    public SDL_Cursor* Handle { get; private set; }

    /// <summary>
    /// Creates a cursor using the specified bitmap data and mask (in MSB format).
    /// The cursor width must be a multiple of 8 bits.
    /// </summary>
    /// <param name="data">The color value for each pixel of the cursor.</param>
    /// <param name="mask">The mask value for each pixel of the cursor.</param>
    /// <param name="size">The size of the cursor (must be a multiple of 8).</param>
    /// <param name="hot">The hot spot of the cursor.</param>
    public Cursor(ReadOnlySpan<byte> data, ReadOnlySpan<byte> mask, Size size, Point hot)
    {
        fixed (byte* dataPtr = data)
        fixed (byte* maskPtr = mask)
        {
            Handle = CheckErrorPointer(SDL_CreateCursor(dataPtr, maskPtr, size.Width, size.Height, hot.X, hot.Y));
        }

        _ownsHandle = true;
    }

    /// <summary>
    /// Creates a color cursor from a surface.
    /// </summary>
    /// <param name="surface">The surface representing the cursor image.</param>
    /// <param name="hot">The hot spot of the cursor.</param>
    public Cursor(Surface surface, Point hot)
    {
        ArgumentNullException.ThrowIfNull(surface);
        Handle = CheckErrorPointer(SDL_CreateColorCursor(surface.Handle, hot.X, hot.Y));
        _ownsHandle = true;
    }

    /// <summary>
    /// Creates a system cursor.
    /// </summary>
    /// <param name="cursor">The system cursor type.</param>
    public Cursor(SystemCursor cursor)
    {
        Handle = CheckErrorPointer(SDL_CreateSystemCursor((SDL_SystemCursor)cursor));
        _ownsHandle = true;
    }

    internal Cursor(SDL_Cursor* handle, bool ownsHandle)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Gets the current active cursor.
    /// </summary>
    /// <returns>The active cursor, or null if there is no mouse.</returns>
    public static Cursor? GetCurrent()
    {
        SDL_Cursor* cursor = SDL_GetCursor();
        return cursor != null ? new Cursor(cursor, ownsHandle: false) : null;
    }

    /// <summary>
    /// Gets the default cursor.
    /// </summary>
    /// <returns>The default cursor.</returns>
    public static Cursor GetDefault()
    {
        return new Cursor(CheckErrorPointer(SDL_GetDefaultCursor()), ownsHandle: false);
    }

    /// <summary>
    /// Sets this cursor as the active cursor.
    /// </summary>
    public void SetActive()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetCursor(Handle));
    }

    /// <summary>
    /// Shows the cursor.
    /// </summary>
    public static void Show()
    {
        _ = CheckErrorBool(SDL_ShowCursor());
    }

    /// <summary>
    /// Hides the cursor.
    /// </summary>
    public static void Hide()
    {
        _ = CheckErrorBool(SDL_HideCursor());
    }

    /// <summary>
    /// Gets whether the cursor is currently visible.
    /// </summary>
    public static bool IsVisible => SDL_CursorVisible();

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    /// <summary>
    /// Disposes this cursor.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_ownsHandle && Handle != null)
        {
            SDL_DestroyCursor(Handle);
        }

        Handle = null;
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
