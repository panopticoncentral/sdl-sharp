using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Render;

namespace SdlSharp.Graphics;

/// <summary>
/// A managed wrapper around an SDL texture (SDL_Texture).
/// </summary>
public sealed unsafe class Texture : IDisposable
{
    private readonly bool _ownsHandle;

    /// <summary>
    /// The underlying native SDL_Texture pointer.
    /// </summary>
    internal SDL_Texture* Handle { get; private set; }

    // --- Create property names ---

    /// <summary>Property: the colorspace for the texture (number).</summary>
    public const string PropCreateColorspace = Render.SDL_PROP_TEXTURE_CREATE_COLORSPACE_NUMBER;
    /// <summary>Property: the pixel format for the texture (number).</summary>
    public const string PropCreateFormat = Render.SDL_PROP_TEXTURE_CREATE_FORMAT_NUMBER;
    /// <summary>Property: the access pattern for the texture (number).</summary>
    public const string PropCreateAccess = Render.SDL_PROP_TEXTURE_CREATE_ACCESS_NUMBER;
    /// <summary>Property: the width of the texture (number).</summary>
    public const string PropCreateWidth = Render.SDL_PROP_TEXTURE_CREATE_WIDTH_NUMBER;
    /// <summary>Property: the height of the texture (number).</summary>
    public const string PropCreateHeight = Render.SDL_PROP_TEXTURE_CREATE_HEIGHT_NUMBER;
    /// <summary>Property: the palette for the texture (pointer).</summary>
    public const string PropCreatePalette = Render.SDL_PROP_TEXTURE_CREATE_PALETTE_POINTER;
    /// <summary>Property: the SDR white point for the texture (float).</summary>
    public const string PropCreateSdrWhitePoint = Render.SDL_PROP_TEXTURE_CREATE_SDR_WHITE_POINT_FLOAT;
    /// <summary>Property: the HDR headroom for the texture (float).</summary>
    public const string PropCreateHdrHeadroom = Render.SDL_PROP_TEXTURE_CREATE_HDR_HEADROOM_FLOAT;

    // --- Texture property names ---

    /// <summary>Property: the colorspace of the texture (number).</summary>
    public const string PropColorspace = Render.SDL_PROP_TEXTURE_COLORSPACE_NUMBER;
    /// <summary>Property: the pixel format of the texture (number).</summary>
    public const string PropFormat = Render.SDL_PROP_TEXTURE_FORMAT_NUMBER;
    /// <summary>Property: the access pattern of the texture (number).</summary>
    public const string PropAccess = Render.SDL_PROP_TEXTURE_ACCESS_NUMBER;
    /// <summary>Property: the width of the texture (number).</summary>
    public const string PropWidth = Render.SDL_PROP_TEXTURE_WIDTH_NUMBER;
    /// <summary>Property: the height of the texture (number).</summary>
    public const string PropHeight = Render.SDL_PROP_TEXTURE_HEIGHT_NUMBER;
    /// <summary>Property: the SDR white point of the texture (float).</summary>
    public const string PropSdrWhitePoint = Render.SDL_PROP_TEXTURE_SDR_WHITE_POINT_FLOAT;
    /// <summary>Property: the HDR headroom of the texture (float).</summary>
    public const string PropHdrHeadroom = Render.SDL_PROP_TEXTURE_HDR_HEADROOM_FLOAT;

    internal Texture(SDL_Texture* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Gets the width of the texture in pixels.
    /// </summary>
    public int Width => Handle->w;

    /// <summary>
    /// Gets the height of the texture in pixels.
    /// </summary>
    public int Height => Handle->h;

    /// <summary>
    /// Gets the pixel format of the texture.
    /// </summary>
    public PixelFormat Format => (PixelFormat)Handle->format;

    /// <summary>
    /// Gets the size of the texture as floating point values.
    /// </summary>
    public (float W, float H) Size
    {
        get
        {
            Check(SDL_GetTextureSize(Handle, out var w, out var h));
            return (w, h);
        }
    }

    /// <summary>
    /// Gets or sets the additional color value multiplied into texture copy operations.
    /// </summary>
    public Color ColorMod
    {
        get
        {
            Check(SDL_GetTextureColorMod(Handle, out var r, out var g, out var b));
            return new Color(r, g, b);
        }
        set => Check(SDL_SetTextureColorMod(Handle, value.R, value.G, value.B));
    }

    /// <summary>
    /// Gets or sets the additional alpha value multiplied into texture copy operations.
    /// </summary>
    public byte AlphaMod
    {
        get
        {
            Check(SDL_GetTextureAlphaMod(Handle, out var alpha));
            return alpha;
        }
        set => Check(SDL_SetTextureAlphaMod(Handle, value));
    }

    /// <summary>
    /// Gets or sets the blend mode used for texture copy operations.
    /// </summary>
    public BlendMode BlendMode
    {
        get
        {
            Check(SDL_GetTextureBlendMode(Handle, out var mode));
            return BlendMode.FromNative(mode);
        }
        set => Check(SDL_SetTextureBlendMode(Handle, value.ToNative()));
    }

    /// <summary>
    /// Gets or sets the scale mode used for texture scale operations.
    /// </summary>
    public ScaleMode ScaleMode
    {
        get
        {
            Check(SDL_GetTextureScaleMode(Handle, out var mode));
            return (ScaleMode)mode;
        }
        set => Check(SDL_SetTextureScaleMode(Handle, (SDL_ScaleMode)value));
    }

    /// <summary>
    /// Updates the given texture rectangle with new pixel data.
    /// </summary>
    /// <param name="rect">The area to update, or <c>null</c> to update the entire texture.</param>
    /// <param name="pixels">The raw pixel data.</param>
    /// <param name="pitch">The number of bytes in a row of pixel data, including padding between lines.</param>
    public void Update(Rectangle? rect, nint pixels, int pitch)
    {
        if (rect is { } r)
        {
            var native = r.ToNative();
            Check(SDL_UpdateTexture(Handle, &native, (void*)pixels, pitch));
        }
        else
        {
            Check(SDL_UpdateTexture(Handle, null, (void*)pixels, pitch));
        }
    }

    /// <summary>
    /// Locks a portion of the texture for write-only pixel access.
    /// </summary>
    /// <param name="rect">The area to lock, or <c>null</c> for the entire texture.</param>
    /// <param name="pixels">On return, the pointer to the locked pixels.</param>
    /// <param name="pitch">On return, the pitch of the locked pixels.</param>
    public void Lock(Rectangle? rect, out nint pixels, out int pitch)
    {
        if (rect is { } r)
        {
            var native = r.ToNative();
            Check(SDL_LockTexture(Handle, &native, out var p, out pitch));
            pixels = (nint)p;
        }
        else
        {
            Check(SDL_LockTexture(Handle, null, out var p, out pitch));
            pixels = (nint)p;
        }
    }

    /// <summary>
    /// Unlocks the texture, uploading any changes to video memory.
    /// </summary>
    public void Unlock() => SDL_UnlockTexture(Handle);

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && Handle != null)
        {
            SDL_DestroyTexture(Handle);
            Handle = null;
        }
    }
}
