using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Video;

namespace SdlSharp.Graphics;

/// <summary>
/// A managed wrapper around an SDL OpenGL context (SDL_GLContext).
/// </summary>
public sealed unsafe class GlContext : IDisposable
{
    private readonly bool _ownsHandle;

    /// <summary>
    /// The underlying native SDL_GLContext pointer.
    /// </summary>
    internal SDL_GLContextState* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private SDL_GLContextState* _handle;

    internal GlContext(SDL_GLContextState* handle, bool ownsHandle = true)
    {
        _handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Makes this context the current OpenGL context for the given window. This is only
    /// needed when switching contexts or windows — <see cref="Window.CreateGlContext"/>
    /// already makes the new context current.
    /// </summary>
    /// <param name="window">The window to make this context current on.</param>
    /// <seealso cref="Window.CreateGlContext"/>
    /// <seealso cref="Window.GlSwap"/>
    public void MakeCurrent(Window window) => Check(SDL_GL_MakeCurrent(window.Handle, Handle));

    /// <summary>
    /// Gets the currently active OpenGL context, as a non-owning wrapper, or
    /// <c>null</c> if no context is current. Each access returns a new non-owning
    /// wrapper around the same underlying SDL context; the wrappers are not equal
    /// to each other.
    /// </summary>
    public static GlContext? Current
    {
        get
        {
            var handle = SDL_GL_GetCurrentContext();
            return handle == null ? null : new GlContext(handle, ownsHandle: false);
        }
    }

    /// <summary>
    /// Gets the window whose OpenGL context is current, as a non-owning wrapper, or
    /// <c>null</c> if no context is current. Each access returns a new non-owning
    /// wrapper around the same underlying SDL window; the wrappers are not equal
    /// to each other.
    /// </summary>
    public static Window? CurrentWindow
    {
        get
        {
            var handle = SDL_GL_GetCurrentWindow();
            return handle == null ? null : new Window(handle, ownsHandle: false);
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && _handle != null)
        {
            Check(SDL_GL_DestroyContext(_handle));
        }
        _handle = null;
    }
}
