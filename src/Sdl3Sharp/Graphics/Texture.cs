using static Sdl3Sharp.Native.BlendMode;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Rect;
using static Sdl3Sharp.Native.Render;
using static Sdl3Sharp.Native.Surface;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Represents an SDL texture - an efficient driver-specific representation of pixel data.
/// </summary>
public sealed unsafe class Texture : IDisposable
{
    private bool _disposed;
    private readonly bool _ownsHandle;

    /// <summary>
    /// Gets the underlying SDL_Texture pointer.
    /// </summary>
    public SDL_Texture* Handle { get; private set; }

    /// <summary>
    /// Creates a new texture wrapping an existing SDL_Texture pointer.
    /// </summary>
    /// <param name="handle">The SDL_Texture pointer.</param>
    /// <param name="ownsHandle">Whether this instance owns the handle and should free it on disposal.</param>
    internal Texture(SDL_Texture* handle, bool ownsHandle)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Gets the properties associated with this texture.
    /// </summary>
    public PropertyGroup Properties
    {
        get
        {
            ThrowIfDisposed();
            return new(CheckErrorZero(SDL_GetTextureProperties(Handle)), ownsHandle: false);
        }
    }

    /// <summary>
    /// Gets the renderer that created this texture.
    /// </summary>
    public Renderer? GetRenderer()
    {
        ThrowIfDisposed();
        SDL_Renderer* renderer = SDL_GetRendererFromTexture(Handle);
        return renderer != null ? new Renderer(renderer, ownsHandle: false) : null;
    }

    /// <summary>
    /// Gets the pixel format of the texture.
    /// </summary>
    public PixelFormat Format
    {
        get
        {
            ThrowIfDisposed();
            return new(Handle->format);
        }
    }

    /// <summary>
    /// Gets the width of the texture in pixels.
    /// </summary>
    public int Width
    {
        get
        {
            ThrowIfDisposed();
            return Handle->w;
        }
    }

    /// <summary>
    /// Gets the height of the texture in pixels.
    /// </summary>
    public int Height
    {
        get
        {
            ThrowIfDisposed();
            return Handle->h;
        }
    }

    /// <summary>
    /// Gets the size of the texture.
    /// </summary>
    public Size Size
    {
        get
        {
            ThrowIfDisposed();
            return new(Handle->w, Handle->h);
        }
    }

    /// <summary>
    /// Gets the size of the texture as floating point values.
    /// </summary>
    public SizeF SizeF
    {
        get
        {
            ThrowIfDisposed();
            float w, h;
            _ = CheckErrorBool(SDL_GetTextureSize(Handle, &w, &h));
            return new(w, h);
        }
    }

    /// <summary>
    /// Gets or sets the color modulation for this texture.
    /// </summary>
    public Color ColorMod
    {
        get
        {
            ThrowIfDisposed();
            byte r, g, b;
            _ = CheckErrorBool(SDL_GetTextureColorMod(Handle, &r, &g, &b));
            return new(r, g, b);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetTextureColorMod(Handle, value.Red, value.Green, value.Blue));
        }
    }

    /// <summary>
    /// Gets or sets the color modulation for this texture as floating point values.
    /// </summary>
    public ColorF ColorModFloat
    {
        get
        {
            ThrowIfDisposed();
            float r, g, b;
            _ = CheckErrorBool(SDL_GetTextureColorModFloat(Handle, &r, &g, &b));
            return new(r, g, b, 1.0f);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetTextureColorModFloat(Handle, value.Red, value.Green, value.Blue));
        }
    }

    /// <summary>
    /// Gets or sets the alpha modulation for this texture.
    /// </summary>
    public byte AlphaMod
    {
        get
        {
            ThrowIfDisposed();
            byte alpha;
            _ = CheckErrorBool(SDL_GetTextureAlphaMod(Handle, &alpha));
            return alpha;
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetTextureAlphaMod(Handle, value));
        }
    }

    /// <summary>
    /// Gets or sets the alpha modulation for this texture as a floating point value.
    /// </summary>
    public float AlphaModFloat
    {
        get
        {
            ThrowIfDisposed();
            float alpha;
            _ = CheckErrorBool(SDL_GetTextureAlphaModFloat(Handle, &alpha));
            return alpha;
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetTextureAlphaModFloat(Handle, value));
        }
    }

    /// <summary>
    /// Gets or sets the blend mode for this texture.
    /// </summary>
    public BlendMode BlendMode
    {
        get
        {
            ThrowIfDisposed();
            SDL_BlendMode mode;
            _ = CheckErrorBool(SDL_GetTextureBlendMode(Handle, &mode));
            return new BlendMode(mode);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetTextureBlendMode(Handle, value.ToNative()));
        }
    }

    /// <summary>
    /// Gets or sets the scale mode for this texture.
    /// </summary>
    public ScaleMode ScaleMode
    {
        get
        {
            ThrowIfDisposed();
            SDL_ScaleMode mode;
            _ = CheckErrorBool(SDL_GetTextureScaleMode(Handle, &mode));
            return (ScaleMode)mode;
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetTextureScaleMode(Handle, (SDL_ScaleMode)value));
        }
    }

    /// <summary>
    /// Updates the texture with new pixel data.
    /// </summary>
    /// <param name="rect">The area to update, or null to update the entire texture.</param>
    /// <param name="pixels">The raw pixel data.</param>
    /// <param name="pitch">The number of bytes in a row of pixel data.</param>
    public void Update(Rectangle? rect, void* pixels, int pitch)
    {
        ThrowIfDisposed();
        SDL_Rect nativeRect;
        _ = CheckErrorBool(SDL_UpdateTexture(Handle, Rectangle.ToNative(rect, &nativeRect), pixels, pitch));
    }

    /// <summary>
    /// Updates the texture with new pixel data from a span.
    /// </summary>
    /// <typeparam name="T">The type of pixel data.</typeparam>
    /// <param name="rect">The area to update, or null to update the entire texture.</param>
    /// <param name="pixels">The pixel data.</param>
    /// <param name="pitch">The number of bytes in a row of pixel data.</param>
    public void Update<T>(Rectangle? rect, ReadOnlySpan<T> pixels, int pitch) where T : unmanaged
    {
        ThrowIfDisposed();
        SDL_Rect nativeRect;
        fixed (T* pixelsPtr = pixels)
        {
            _ = CheckErrorBool(SDL_UpdateTexture(Handle, Rectangle.ToNative(rect, &nativeRect), pixelsPtr, pitch));
        }
    }

    /// <summary>
    /// Updates a YUV texture with new pixel data.
    /// </summary>
    /// <param name="rect">The area to update, or null to update the entire texture.</param>
    /// <param name="yPlane">The Y plane data.</param>
    /// <param name="yPitch">The pitch of the Y plane.</param>
    /// <param name="uPlane">The U plane data.</param>
    /// <param name="uPitch">The pitch of the U plane.</param>
    /// <param name="vPlane">The V plane data.</param>
    /// <param name="vPitch">The pitch of the V plane.</param>
    public void UpdateYuv(Rectangle? rect, byte* yPlane, int yPitch, byte* uPlane, int uPitch, byte* vPlane, int vPitch)
    {
        ThrowIfDisposed();
        SDL_Rect nativeRect;
        _ = CheckErrorBool(SDL_UpdateYUVTexture(Handle, Rectangle.ToNative(rect, &nativeRect), yPlane, yPitch, uPlane, uPitch, vPlane, vPitch));
    }

    /// <summary>
    /// Updates an NV12/NV21 texture with new pixel data.
    /// </summary>
    /// <param name="rect">The area to update, or null to update the entire texture.</param>
    /// <param name="yPlane">The Y plane data.</param>
    /// <param name="yPitch">The pitch of the Y plane.</param>
    /// <param name="uvPlane">The UV plane data.</param>
    /// <param name="uvPitch">The pitch of the UV plane.</param>
    public void UpdateNv(Rectangle? rect, byte* yPlane, int yPitch, byte* uvPlane, int uvPitch)
    {
        ThrowIfDisposed();
        SDL_Rect nativeRect;
        _ = CheckErrorBool(SDL_UpdateNVTexture(Handle, Rectangle.ToNative(rect, &nativeRect), yPlane, yPitch, uvPlane, uvPitch));
    }

    /// <summary>
    /// Locks a portion of the texture for write-only pixel access.
    /// </summary>
    /// <param name="rect">The area to lock, or null to lock the entire texture.</param>
    /// <param name="pixels">Receives a pointer to the locked pixels.</param>
    /// <param name="pitch">Receives the pitch of the locked pixels.</param>
    public void Lock(Rectangle? rect, out void* pixels, out int pitch)
    {
        ThrowIfDisposed();
        SDL_Rect nativeRect;
        void* p;
        int pt;
        _ = CheckErrorBool(SDL_LockTexture(Handle, Rectangle.ToNative(rect, &nativeRect), &p, &pt));
        pixels = p;
        pitch = pt;
    }

    /// <summary>
    /// Locks a portion of the texture for write-only pixel access and exposes it as a surface.
    /// </summary>
    /// <param name="rect">The area to lock, or null to lock the entire texture.</param>
    /// <returns>A surface representing the locked area. Do not free this surface; it will be freed when the texture is unlocked.</returns>
    public Surface LockToSurface(Rectangle? rect)
    {
        ThrowIfDisposed();
        SDL_Rect nativeRect;
        SDL_Surface* surface;
        _ = CheckErrorBool(SDL_LockTextureToSurface(Handle, Rectangle.ToNative(rect, &nativeRect), &surface));
        return new Surface(surface, ownsHandle: false);
    }

    /// <summary>
    /// Unlocks the texture and uploads changes to video memory.
    /// </summary>
    public void Unlock()
    {
        ThrowIfDisposed();
        SDL_UnlockTexture(Handle);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_ownsHandle && Handle != null)
        {
            SDL_DestroyTexture(Handle);
        }

        Handle = null;
        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
