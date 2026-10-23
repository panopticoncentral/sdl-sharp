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
    internal readonly NativeLifetime Lifetime;
    private readonly NativeLifetime? _viewParent;

    /// <summary>
    /// The underlying native SDL_Texture pointer.
    /// </summary>
    internal SDL_Texture* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            Lifetime.Check();
            _viewParent?.Check();
            return _handle;
        }
    }

    private SDL_Texture* _handle;

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

    // --- Create property names: platform-specific native handles ---

    /// <summary>Property: the ID3D11Texture2D associated with the texture, if it was created via SDL_PROP_TEXTURE_CREATE_D3D11_TEXTURE_POINTER (pointer).</summary>
    public const string PropCreateD3D11Texture = Render.SDL_PROP_TEXTURE_CREATE_D3D11_TEXTURE_POINTER;
    /// <summary>Property: the ID3D11Texture2D associated with the U plane of a YUV texture (pointer).</summary>
    public const string PropCreateD3D11TextureU = Render.SDL_PROP_TEXTURE_CREATE_D3D11_TEXTURE_U_POINTER;
    /// <summary>Property: the ID3D11Texture2D associated with the V plane of a YUV texture (pointer).</summary>
    public const string PropCreateD3D11TextureV = Render.SDL_PROP_TEXTURE_CREATE_D3D11_TEXTURE_V_POINTER;
    /// <summary>Property: the ID3D12Resource associated with the texture (pointer).</summary>
    public const string PropCreateD3D12Texture = Render.SDL_PROP_TEXTURE_CREATE_D3D12_TEXTURE_POINTER;
    /// <summary>Property: the ID3D12Resource associated with the U plane of a YUV texture (pointer).</summary>
    public const string PropCreateD3D12TextureU = Render.SDL_PROP_TEXTURE_CREATE_D3D12_TEXTURE_U_POINTER;
    /// <summary>Property: the ID3D12Resource associated with the V plane of a YUV texture (pointer).</summary>
    public const string PropCreateD3D12TextureV = Render.SDL_PROP_TEXTURE_CREATE_D3D12_TEXTURE_V_POINTER;
    /// <summary>Property: the CVPixelBufferRef associated with the texture, if you want to create a texture from an existing pixel buffer (pointer).</summary>
    public const string PropCreateMetalPixelBuffer = Render.SDL_PROP_TEXTURE_CREATE_METAL_PIXELBUFFER_POINTER;
    /// <summary>Property: the OpenGL texture name (number).</summary>
    public const string PropCreateOpenGlTexture = Render.SDL_PROP_TEXTURE_CREATE_OPENGL_TEXTURE_NUMBER;
    /// <summary>Property: the OpenGL texture name for the UV plane of a NV12 texture (number).</summary>
    public const string PropCreateOpenGlTextureUv = Render.SDL_PROP_TEXTURE_CREATE_OPENGL_TEXTURE_UV_NUMBER;
    /// <summary>Property: the OpenGL texture name for the U plane of a YUV texture (number).</summary>
    public const string PropCreateOpenGlTextureU = Render.SDL_PROP_TEXTURE_CREATE_OPENGL_TEXTURE_U_NUMBER;
    /// <summary>Property: the OpenGL texture name for the V plane of a YUV texture (number).</summary>
    public const string PropCreateOpenGlTextureV = Render.SDL_PROP_TEXTURE_CREATE_OPENGL_TEXTURE_V_NUMBER;
    /// <summary>Property: the OpenGLES2 texture name (number).</summary>
    public const string PropCreateOpenGles2Texture = Render.SDL_PROP_TEXTURE_CREATE_OPENGLES2_TEXTURE_NUMBER;
    /// <summary>Property: the OpenGLES2 texture name for the UV plane of a NV12 texture (number).</summary>
    public const string PropCreateOpenGles2TextureUv = Render.SDL_PROP_TEXTURE_CREATE_OPENGLES2_TEXTURE_UV_NUMBER;
    /// <summary>Property: the OpenGLES2 texture name for the U plane of a YUV texture (number).</summary>
    public const string PropCreateOpenGles2TextureU = Render.SDL_PROP_TEXTURE_CREATE_OPENGLES2_TEXTURE_U_NUMBER;
    /// <summary>Property: the OpenGLES2 texture name for the V plane of a YUV texture (number).</summary>
    public const string PropCreateOpenGles2TextureV = Render.SDL_PROP_TEXTURE_CREATE_OPENGLES2_TEXTURE_V_NUMBER;
    /// <summary>Property: the VkImage associated with the texture (number).</summary>
    public const string PropCreateVulkanTexture = Render.SDL_PROP_TEXTURE_CREATE_VULKAN_TEXTURE_NUMBER;
    /// <summary>Property: the VkImageLayout of the VkImage associated with the texture (number).</summary>
    public const string PropCreateVulkanLayout = Render.SDL_PROP_TEXTURE_CREATE_VULKAN_LAYOUT_NUMBER;
    /// <summary>Property: the SDL_GPUTexture associated with the texture (pointer).</summary>
    public const string PropCreateGpuTexture = Render.SDL_PROP_TEXTURE_CREATE_GPU_TEXTURE_POINTER;
    /// <summary>Property: the SDL_GPUTexture associated with the UV plane of a NV12 texture (pointer).</summary>
    public const string PropCreateGpuTextureUv = Render.SDL_PROP_TEXTURE_CREATE_GPU_TEXTURE_UV_POINTER;
    /// <summary>Property: the SDL_GPUTexture associated with the U plane of a YUV texture (pointer).</summary>
    public const string PropCreateGpuTextureU = Render.SDL_PROP_TEXTURE_CREATE_GPU_TEXTURE_U_POINTER;
    /// <summary>Property: the SDL_GPUTexture associated with the V plane of a YUV texture (pointer).</summary>
    public const string PropCreateGpuTextureV = Render.SDL_PROP_TEXTURE_CREATE_GPU_TEXTURE_V_POINTER;

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

    // --- Texture property names: platform-specific native handles ---

    /// <summary>Property: the ID3D11Texture2D associated with the texture (pointer).</summary>
    public const string PropD3D11Texture = Render.SDL_PROP_TEXTURE_D3D11_TEXTURE_POINTER;
    /// <summary>Property: the ID3D11Texture2D associated with the U plane of a YUV texture (pointer).</summary>
    public const string PropD3D11TextureU = Render.SDL_PROP_TEXTURE_D3D11_TEXTURE_U_POINTER;
    /// <summary>Property: the ID3D11Texture2D associated with the V plane of a YUV texture (pointer).</summary>
    public const string PropD3D11TextureV = Render.SDL_PROP_TEXTURE_D3D11_TEXTURE_V_POINTER;
    /// <summary>Property: the ID3D12Resource associated with the texture (pointer).</summary>
    public const string PropD3D12Texture = Render.SDL_PROP_TEXTURE_D3D12_TEXTURE_POINTER;
    /// <summary>Property: the ID3D12Resource associated with the U plane of a YUV texture (pointer).</summary>
    public const string PropD3D12TextureU = Render.SDL_PROP_TEXTURE_D3D12_TEXTURE_U_POINTER;
    /// <summary>Property: the ID3D12Resource associated with the V plane of a YUV texture (pointer).</summary>
    public const string PropD3D12TextureV = Render.SDL_PROP_TEXTURE_D3D12_TEXTURE_V_POINTER;
    /// <summary>Property: the OpenGL texture name (number).</summary>
    public const string PropOpenGlTexture = Render.SDL_PROP_TEXTURE_OPENGL_TEXTURE_NUMBER;
    /// <summary>Property: the OpenGL texture name for the UV plane of a NV12 texture (number).</summary>
    public const string PropOpenGlTextureUv = Render.SDL_PROP_TEXTURE_OPENGL_TEXTURE_UV_NUMBER;
    /// <summary>Property: the OpenGL texture name for the U plane of a YUV texture (number).</summary>
    public const string PropOpenGlTextureU = Render.SDL_PROP_TEXTURE_OPENGL_TEXTURE_U_NUMBER;
    /// <summary>Property: the OpenGL texture name for the V plane of a YUV texture (number).</summary>
    public const string PropOpenGlTextureV = Render.SDL_PROP_TEXTURE_OPENGL_TEXTURE_V_NUMBER;
    /// <summary>Property: the OpenGL texture target (number).</summary>
    public const string PropOpenGlTextureTarget = Render.SDL_PROP_TEXTURE_OPENGL_TEXTURE_TARGET_NUMBER;
    /// <summary>Property: the texture coordinate width of the OpenGL texture (float).</summary>
    public const string PropOpenGlTexW = Render.SDL_PROP_TEXTURE_OPENGL_TEX_W_FLOAT;
    /// <summary>Property: the texture coordinate height of the OpenGL texture (float).</summary>
    public const string PropOpenGlTexH = Render.SDL_PROP_TEXTURE_OPENGL_TEX_H_FLOAT;
    /// <summary>Property: the OpenGLES2 texture name (number).</summary>
    public const string PropOpenGles2Texture = Render.SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_NUMBER;
    /// <summary>Property: the OpenGLES2 texture name for the UV plane of a NV12 texture (number).</summary>
    public const string PropOpenGles2TextureUv = Render.SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_UV_NUMBER;
    /// <summary>Property: the OpenGLES2 texture name for the U plane of a YUV texture (number).</summary>
    public const string PropOpenGles2TextureU = Render.SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_U_NUMBER;
    /// <summary>Property: the OpenGLES2 texture name for the V plane of a YUV texture (number).</summary>
    public const string PropOpenGles2TextureV = Render.SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_V_NUMBER;
    /// <summary>Property: the OpenGLES2 texture target (number).</summary>
    public const string PropOpenGles2TextureTarget = Render.SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_TARGET_NUMBER;
    /// <summary>Property: the VkImage associated with the texture (number).</summary>
    public const string PropVulkanTexture = Render.SDL_PROP_TEXTURE_VULKAN_TEXTURE_NUMBER;
    /// <summary>Property: the SDL_GPUTexture associated with the texture (pointer).</summary>
    public const string PropGpuTexture = Render.SDL_PROP_TEXTURE_GPU_TEXTURE_POINTER;
    /// <summary>Property: the SDL_GPUTexture associated with the UV plane of a NV12 texture (pointer).</summary>
    public const string PropGpuTextureUv = Render.SDL_PROP_TEXTURE_GPU_TEXTURE_UV_POINTER;
    /// <summary>Property: the SDL_GPUTexture associated with the U plane of a YUV texture (pointer).</summary>
    public const string PropGpuTextureU = Render.SDL_PROP_TEXTURE_GPU_TEXTURE_U_POINTER;
    /// <summary>Property: the SDL_GPUTexture associated with the V plane of a YUV texture (pointer).</summary>
    public const string PropGpuTextureV = Render.SDL_PROP_TEXTURE_GPU_TEXTURE_V_POINTER;

    internal Texture(SDL_Texture* handle, bool ownsHandle = true, NativeLifetime? parent = null)
    {
        _handle = handle;
        Lifetime = NativeLifetime.Get(typeof(Texture), (nint)handle, ownsHandle, threadAffine: true, parent: parent);
        _ownsHandle = ownsHandle;
        if (parent == null)
        {
            var renderer = SDL_GetRendererFromTexture(handle);
            if (renderer != null) Lifetime.SetParent(NativeLifetime.Get(typeof(Renderer), (nint)renderer, false, true));
        }
        _viewParent = ownsHandle ? null : parent;
    }

    /// <summary>
    /// Gets the renderer that created this texture. The returned renderer is non-owning.
    /// </summary>
    /// <returns>The renderer that created this texture (non-owning wrapper).</returns>
    public Renderer GetRenderer() => new(Check(SDL_GetRendererFromTexture(Handle)), ownsHandle: false);

    /// <summary>
    /// Gets the properties associated with this texture. The returned group is owned
    /// by SDL; disposing this view does not destroy the native group.
    /// </summary>
    public PropertyGroup Properties =>
        new(CheckId(SDL_GetTextureProperties(Handle)), ownsHandle: false, parent: Lifetime);

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
    /// Gets or sets the additional color value multiplied into texture copy operations, with floating point precision.
    /// The setter ignores the alpha component of <see cref="FColor"/>; the getter always reports alpha as 1.0.
    /// Alpha modulation is set and read separately via <see cref="AlphaModFloat"/>.
    /// </summary>
    public FColor ColorModFloat
    {
        get
        {
            Check(SDL_GetTextureColorModFloat(Handle, out var r, out var g, out var b));
            return new FColor(r, g, b, 1.0f);
        }
        set => Check(SDL_SetTextureColorModFloat(Handle, value.R, value.G, value.B));
    }

    /// <summary>
    /// Gets or sets the additional alpha value multiplied into texture copy operations, with floating point precision.
    /// </summary>
    public float AlphaModFloat
    {
        get
        {
            Check(SDL_GetTextureAlphaModFloat(Handle, out var alpha));
            return alpha;
        }
        set => Check(SDL_SetTextureAlphaModFloat(Handle, value));
    }

    /// <summary>
    /// Gets or sets the blend mode used for texture copy operations.
    /// </summary>
    public BlendMode BlendMode
    {
        get
        {
            Check(SDL_GetTextureBlendMode(Handle, out var mode));
            return (BlendMode)(uint)mode;
        }
        set => Check(SDL_SetTextureBlendMode(Handle, (Native.SDL_BlendMode)(uint)value));
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
    /// Gets or sets the palette used by this texture, for textures created with an indexed
    /// pixel format. The getter returns a non-owning wrapper, or <c>null</c> if the texture
    /// does not use a palette.
    /// </summary>
    public Palette? Palette
    {
        get
        {
            var ptr = SDL_GetTexturePalette(Handle);
            return ptr == null ? null : new Palette(ptr, ownsHandle: false, parent: Lifetime);
        }
        set
        {
            var texture = Handle;
            var replacement = value is { } p ? p.Handle : null;
            if (SDL_GetTexturePalette(texture) != replacement) Lifetime.InvalidateChildren(typeof(Palette));
            Check(SDL_SetTexturePalette(texture, replacement));
        }
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
    /// Updates the given texture rectangle with new pixel data.
    /// </summary>
    /// <param name="rect">The area to update, or <c>null</c> to update the entire texture.</param>
    /// <param name="pixels">The raw pixel data bytes.</param>
    /// <param name="pitch">The number of bytes in a row of pixel data, including padding between lines.</param>
    public void Update(Rectangle? rect, ReadOnlySpan<byte> pixels, int pitch)
    {
        var area = ValidateArea(rect);
        var format = Format;
        if (format is PixelFormat.Yv12 or PixelFormat.Iyuv or PixelFormat.Nv12 or PixelFormat.Nv21 or PixelFormat.P010)
        {
            if ((area.X & 1) != 0 || (area.Y & 1) != 0) throw new ArgumentException("Planar updates require an even X and Y origin.", nameof(rect));
            var sampleBytes = format == PixelFormat.P010 ? 2 : 1;
            var rowBytes = checked(area.W * sampleBytes);
            if (pitch < rowBytes || pitch % sampleBytes != 0) throw new ArgumentOutOfRangeException(nameof(pitch));
            var chromaPitch = format is PixelFormat.Yv12 or PixelFormat.Iyuv ? checked((pitch + 1) / 2) : pitch;
            var chromaRowBytes = checked(((area.W + 1) / 2) * (format is PixelFormat.Yv12 or PixelFormat.Iyuv ? 1 : 2 * sampleBytes));
            if (chromaPitch < chromaRowBytes) throw new ArgumentOutOfRangeException(nameof(pitch));
            var required = checked(pitch * area.H + chromaPitch * ((area.H + 1) / 2) * (format is PixelFormat.Yv12 or PixelFormat.Iyuv ? 2 : 1));
            if (pixels.Length < required) throw new ArgumentException("The buffer must contain the complete padded Y and chroma planes.", nameof(pixels));
        }
        else BufferBounds.Check(pixels.Length, area.H, PackedRowBytes(area.W), pitch, nameof(pixels));
        fixed (byte* pixelsPtr = pixels)
        {
            if (rect is { } r)
            {
                var native = r.ToNative();
                Check(SDL_UpdateTexture(Handle, &native, pixelsPtr, pitch));
            }
            else
            {
                Check(SDL_UpdateTexture(Handle, null, pixelsPtr, pitch));
            }
        }
    }

    /// <summary>
    /// Updates a rectangle within a planar YV12 or IYUV texture with new pixel data.
    /// </summary>
    /// <param name="rect">The area to update, or <c>null</c> to update the entire texture.</param>
    /// <param name="yPlane">The raw pixel data for the Y plane.</param>
    /// <param name="yPitch">The number of bytes between rows of pixel data for the Y plane.</param>
    /// <param name="uPlane">The raw pixel data for the U plane.</param>
    /// <param name="uPitch">The number of bytes between rows of pixel data for the U plane.</param>
    /// <param name="vPlane">The raw pixel data for the V plane.</param>
    /// <param name="vPitch">The number of bytes between rows of pixel data for the V plane.</param>
    public void UpdateYuv(Rectangle? rect, ReadOnlySpan<byte> yPlane, int yPitch,
        ReadOnlySpan<byte> uPlane, int uPitch, ReadOnlySpan<byte> vPlane, int vPitch)
    {
        var area = ValidateArea(rect);
        if ((area.X & 1) != 0 || (area.Y & 1) != 0) throw new ArgumentException("Planar updates require an even X and Y origin.", nameof(rect));
        if (Format is not (PixelFormat.Yv12 or PixelFormat.Iyuv)) throw new ArgumentException("This texture is not planar YUV.");
        BufferBounds.Check(yPlane.Length, area.H, area.W, yPitch, nameof(yPlane));
        BufferBounds.Check(uPlane.Length, (area.H + 1) / 2, (area.W + 1) / 2, uPitch, nameof(uPlane));
        BufferBounds.Check(vPlane.Length, (area.H + 1) / 2, (area.W + 1) / 2, vPitch, nameof(vPlane));
        fixed (byte* yPtr = yPlane)
        fixed (byte* uPtr = uPlane)
        fixed (byte* vPtr = vPlane)
        {
            if (rect is { } r)
            {
                var native = r.ToNative();
                Check(SDL_UpdateYUVTexture(Handle, &native, yPtr, yPitch, uPtr, uPitch, vPtr, vPitch));
            }
            else
            {
                Check(SDL_UpdateYUVTexture(Handle, null, yPtr, yPitch, uPtr, uPitch, vPtr, vPitch));
            }
        }
    }

    /// <summary>
    /// Updates a rectangle within a planar NV12 or NV21 texture with new pixel data.
    /// </summary>
    /// <param name="rect">The area to update, or <c>null</c> to update the entire texture.</param>
    /// <param name="yPlane">The raw pixel data for the Y plane.</param>
    /// <param name="yPitch">The number of bytes between rows of pixel data for the Y plane.</param>
    /// <param name="uvPlane">The raw pixel data for the interleaved UV plane.</param>
    /// <param name="uvPitch">The number of bytes between rows of pixel data for the UV plane.</param>
    public void UpdateNv(Rectangle? rect, ReadOnlySpan<byte> yPlane, int yPitch, ReadOnlySpan<byte> uvPlane, int uvPitch)
    {
        var area = ValidateArea(rect);
        if ((area.X & 1) != 0 || (area.Y & 1) != 0) throw new ArgumentException("Planar updates require an even X and Y origin.", nameof(rect));
        if (Format is not (PixelFormat.Nv12 or PixelFormat.Nv21)) throw new ArgumentException("This texture is not NV12 or NV21.");
        BufferBounds.Check(yPlane.Length, area.H, area.W, yPitch, nameof(yPlane));
        BufferBounds.Check(uvPlane.Length, (area.H + 1) / 2, checked(((area.W + 1) / 2) * 2), uvPitch, nameof(uvPlane));
        fixed (byte* yPtr = yPlane)
        fixed (byte* uvPtr = uvPlane)
        {
            if (rect is { } r)
            {
                var native = r.ToNative();
                Check(SDL_UpdateNVTexture(Handle, &native, yPtr, yPitch, uvPtr, uvPitch));
            }
            else
            {
                Check(SDL_UpdateNVTexture(Handle, null, yPtr, yPitch, uvPtr, uvPitch));
            }
        }
    }

    /// <summary>Locks packed pixels for writing. Dispose the returned lease on this thread to upload the changes.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "The lease is transferred to the returned scope or lifetime cleanup, which releases it exactly once.")]
    public PixelLock Lock(Rectangle? rectangle = null)
    {
        var area = ValidateArea(rectangle);
        if (((uint)Format >> 28) != 1) throw new NotSupportedException("Row mappings require a packed RGB or indexed format; update YUV planes explicitly.");
        var rowBytes = PackedRowBytes(area.W);
        var handle = Handle;
        var lease = Lifetime.RetainExclusive();
        try
        {
            var native = area.ToNative();
            Check(SDL_LockTexture(handle, &native, out var pixels, out var pitch));
            return new PixelLock((nint)pixels, pitch, rowBytes, area.H,
                () => { SDL_UnlockTexture(handle); lease.Dispose(); });
        }
        catch { lease.Dispose(); throw; }
    }

    private Rectangle ValidateArea(Rectangle? rectangle)
    {
        var area = rectangle ?? new Rectangle(0, 0, Width, Height);
        if (area.X < 0 || area.Y < 0 || area.W <= 0 || area.H <= 0 || (long)area.X + area.W > Width || (long)area.Y + area.H > Height)
            throw new ArgumentOutOfRangeException(nameof(rectangle));
        return area;
    }

    private int PackedRowBytes(int width)
    {
        if (Format is PixelFormat.Yuy2 or PixelFormat.Uyvy or PixelFormat.Yvyu) return checked(((width + 1) / 2) * 4);
        if (((uint)Format >> 28) != 1) throw new ArgumentException("This mapping requires a packed texture format.");
        return PixelFormatDetails.Get(Format).GetRowByteCount(width);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && _handle != null)
        {
            Lifetime.Release(() =>
            {
                SDL_DestroyTexture(_handle);
            });
        }
        _handle = null;
    }
    /// <summary>Locks this texture as a borrowed surface until the returned lease is disposed.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "The lease is transferred to the returned scope or lifetime cleanup, which releases it exactly once.")]
    public TextureSurfaceLock LockToSurface(Rectangle? rectangle = null)
    {
        var area = ValidateArea(rectangle).ToNative();
        var handle = Handle;
        var lease = Lifetime.RetainExclusive();
        try
        {
            Check(SDL_LockTextureToSurface(handle, &area, out var surface));
            return new TextureSurfaceLock(new Surface(surface, false, Lifetime), () => { SDL_UnlockTexture(handle); lease.Dispose(); });
        }
        catch { lease.Dispose(); throw; }
    }
}
