using Sdl3Sharp.ImGui.Native;
using System.Runtime.InteropServices;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents specs and pixel storage for a texture used by Dear ImGui.
/// </summary>
/// <remarks>
/// This is primarily useful for core library and backends. End-user/applications typically do not need to interact with this directly.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct TextureData : IDisposable
{
    private ImTextureData _native;

    /// <summary>
    /// Gets the unique identifier for this texture. Unique per atlas.
    /// </summary>
    public readonly int UniqueId => _native.UniqueID;

    /// <summary>
    /// The current status of the texture.
    /// </summary>
    public TextureStatus Status
    {
        readonly get => (TextureStatus)_native.Status;
        set
        {
            fixed (ImTextureData* ptr = &_native)
            {
                ImTextureData.SetStatus(ptr, (ImTextureStatus)value);
            }
        }
    }

    /// <summary>
    /// Gets or sets backend-specific storage for convenience.
    /// </summary>
    public nint BackendUserData
    {
        readonly get => _native.BackendUserData;
        set => _native.BackendUserData = value;
    }

    /// <summary>
    /// Gets the backend-specific texture identifier.
    /// </summary>
    public ImTextureID TexId
    {
        readonly get
        {
            fixed (ImTextureData* ptr = &_native)
            {
                return ImTextureData.GetTexID(ptr);
            }
        }
        set
        {
            fixed (ImTextureData* ptr = &_native)
            {
                ImTextureData.SetTexID(ptr, value);
            }
        }
    }

    /// <summary>
    /// Gets the texture format (RGBA32 or Alpha8).
    /// </summary>
    public readonly TextureFormat Format => (TextureFormat)_native.Format;

    /// <summary>
    /// The texture width in pixels.
    /// </summary>
    public readonly int Width => _native.Width;

    /// <summary>
    /// The texture height in pixels.
    /// </summary>
    public readonly int Height => _native.Height;

    /// <summary>
    /// The bytes per pixel (1 or 4).
    /// </summary>
    public readonly int BytesPerPixel => _native.BytesPerPixel;

    /// <summary>
    /// Gets the total size of the pixel buffer in bytes.
    /// </summary>
    public readonly int SizeInBytes => _native.Width * _native.Height * _native.BytesPerPixel;

    /// <summary>
    /// Gets the pitch (bytes per row) of the texture.
    /// </summary>
    public readonly int Pitch => _native.Width * _native.BytesPerPixel;

    /// <summary>
    /// A pointer to the pixel buffer.
    /// </summary>
    public readonly Span<byte> Pixels
    {
        get
        {
            fixed (ImTextureData* ptr = &_native)
            {
                var pixels = (byte*)ImTextureData.GetPixels(ptr);
                return new Span<byte>(pixels, _native.Width * _native.Height * _native.BytesPerPixel);
            }
        }
    }

    /// <summary>
    /// The number of successive frames where the texture was not used.
    /// </summary>
    public readonly int UnusedFrames => _native.UnusedFrames;

    /// <summary>
    /// The number of contexts using this texture.
    /// </summary>
    public readonly ushort RefCount => _native.RefCount;

    /// <summary>
    /// Whether the texture data uses colors (rather than just white + alpha).
    /// </summary>
    public readonly bool UseColors => _native.UseColors;

    /// <summary>
    /// The bounding box encompassing all past and queued updates.
    /// </summary>
    public readonly TextureRect UsedRect => new(_native.UsedRect);

    /// <summary>
    /// The bounding box encompassing all queued updates.
    /// </summary>
    public readonly TextureRect UpdateRect => new(_native.UpdateRect);

    /// <summary>
    /// Creates the texture with the specified format and dimensions.
    /// </summary>
    /// <param name="format">The texture format.</param>
    /// <param name="width">The texture width.</param>
    /// <param name="height">The texture height.</param>
    public TextureData(TextureFormat format, int width, int height)
    {
        fixed (ImTextureData* ptr = &_native)
        {
            ImTextureData.Create(ptr, (ImTextureFormat)format, width, height);
        }
    }

    internal TextureData(ImTextureData* native)
    {
        _native = *native;
    }

    /// <summary>
    /// Destroys the pixel data.
    /// </summary>
    public readonly void DestroyPixels()
    {
        fixed (ImTextureData* ptr = &_native)
        {
            ImTextureData.DestroyPixels(ptr);
        }
    }

    /// <summary>
    /// Disposes the texture data by destroying the pixel data.
    /// </summary>
    public readonly void Dispose()
    {
        DestroyPixels();
    }

    /// <summary>
    /// Gets a pointer to pixel data at a specific position.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <returns>A pointer to the pixel at the specified position.</returns>
    public readonly Span<byte> GetPixelsAt(int x, int y)
    {
        fixed (ImTextureData* ptr = &_native)
        {
            var pixels = (byte*)ImTextureData.GetPixelsAt(ptr, x, y);
            var size = ((_native.Width * (_native.Height - y)) - x) * _native.BytesPerPixel;
            return new Span<byte>(pixels, size);
        }
    }

    /// <summary>
    /// Gets a texture reference.
    /// </summary>
    /// <returns>The texture reference.</returns>
    public readonly TextureRef GetTexRef()
    {
        fixed (ImTextureData* ptr = &_native)
        {
            return new TextureRef(ImTextureData.GetTexRef(ptr));
        }
    }

    /// <summary>
    /// Casts a pointer to a native ImTextureData to a pointer to a TextureData.
    /// </summary>
    /// <param name="native">The native ImTextureData pointer.</param>
    /// <returns>A pointer to a TextureData.</returns>
    public static TextureData FromNative(ImTextureData* native)
    {
        return new(native);
    }

    /// <summary>
    /// Casts a pointer to a TextureData to a pointer to a native ImTextureData.
    /// </summary>
    /// <param name="textureData">The TextureData pointer.</param>
    /// <returns>A pointer to an ImTextureData.</returns>
    public static ImTextureData* ToNative(TextureData* textureData)
    {
        return (ImTextureData*)textureData;
    }
}
