using static SdlSharp.Native.Common;
using static SdlSharp.Native.Surface;
namespace SdlSharp.Graphics;
/// <summary>Bounds-checked conversion of packed pixel buffers.</summary>
public static unsafe class PixelConverter
{
    private static void Validate(int width, int height, PixelFormat sourceFormat, int sourceLength, int sourcePitch, PixelFormat destinationFormat, int destinationLength, int destinationPitch)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);
        if (((uint)sourceFormat >> 28) != 1 || ((uint)destinationFormat >> 28) != 1) throw new ArgumentException("Use surfaces for planar or compressed formats.");
        var sourceBytes = PixelFormatDetails.Get(sourceFormat).BytesPerPixel;
        var destinationBytes = PixelFormatDetails.Get(destinationFormat).BytesPerPixel;
        if (sourceBytes == 0 || destinationBytes == 0) throw new ArgumentException("Sub-byte formats require a surface conversion.");
        BufferBounds.Check(sourceLength, height, checked(width * sourceBytes), sourcePitch, "source");
        BufferBounds.Check(destinationLength, height, checked(width * destinationBytes), destinationPitch, "destination");
    }
    /// <summary>Converts packed pixels. Buffers may include inter-row padding; the final row need only contain its pixel bytes.</summary>
    public static void Convert(int width, int height, PixelFormat sourceFormat, ReadOnlySpan<byte> source, int sourcePitch, PixelFormat destinationFormat, Span<byte> destination, int destinationPitch)
    {
        Validate(width, height, sourceFormat, source.Length, sourcePitch, destinationFormat, destination.Length, destinationPitch);
        if (source.Overlaps(destination)) throw new ArgumentException("Source and destination must not overlap.");
        fixed (byte* src = source) fixed (byte* dst = destination)
            Check(SDL_ConvertPixels(width, height, (Native.SDL_PixelFormat)sourceFormat, src, sourcePitch, (Native.SDL_PixelFormat)destinationFormat, dst, destinationPitch));
    }
    /// <summary>Converts packed pixel format and colorspace, using optional SDL color-conversion properties.</summary>
    public static void Convert(int width, int height, PixelFormat sourceFormat, Colorspace sourceColorspace, ReadOnlySpan<byte> source, int sourcePitch,
        PixelFormat destinationFormat, Colorspace destinationColorspace, Span<byte> destination, int destinationPitch, PropertyGroup? sourceProperties = null, PropertyGroup? destinationProperties = null)
    {
        Validate(width, height, sourceFormat, source.Length, sourcePitch, destinationFormat, destination.Length, destinationPitch);
        if (source.Overlaps(destination)) throw new ArgumentException("Source and destination must not overlap.");
        fixed (byte* src = source) fixed (byte* dst = destination)
            Check(SDL_ConvertPixelsAndColorspace(width, height, (Native.SDL_PixelFormat)sourceFormat, (Native.SDL_Colorspace)sourceColorspace, sourceProperties?.Id ?? default, src, sourcePitch,
                (Native.SDL_PixelFormat)destinationFormat, (Native.SDL_Colorspace)destinationColorspace, destinationProperties?.Id ?? default, dst, destinationPitch));
    }
    /// <summary>Premultiplies alpha in packed pixels. Buffers may be identical for in-place conversion when formats and pitches match.</summary>
    public static void PremultiplyAlpha(int width, int height, PixelFormat sourceFormat, ReadOnlySpan<byte> source, int sourcePitch, PixelFormat destinationFormat, Span<byte> destination, int destinationPitch, bool linear = false)
    {
        Validate(width, height, sourceFormat, source.Length, sourcePitch, destinationFormat, destination.Length, destinationPitch);
        if (source.Overlaps(destination, out var offset) && (offset != 0 || sourceFormat != destinationFormat || sourcePitch != destinationPitch)) throw new ArgumentException("Overlapping buffers must have identical layout and start address.");
        fixed (byte* src = source) fixed (byte* dst = destination)
            Check(SDL_PremultiplyAlpha(width, height, (Native.SDL_PixelFormat)sourceFormat, src, sourcePitch, (Native.SDL_PixelFormat)destinationFormat, dst, destinationPitch, linear));
    }
}
