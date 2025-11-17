using static Sdl3Sharp.Native.Pixels;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Colorspace definitions.
/// </summary>
public readonly record struct Colorspace(SDL_Colorspace Value)
{
    /// <summary>Unknown colorspace.</summary>
    public static readonly Colorspace Unknown = new(SDL_Colorspace.SDL_COLORSPACE_UNKNOWN);

    /// <summary>sRGB colorspace (gamma corrected, default for SDL rendering and 8-bit RGB). Equivalent to DXGI_COLOR_SPACE_RGB_FULL_G22_NONE_P709.</summary>
    public static readonly Colorspace SRGB = new(SDL_Colorspace.SDL_COLORSPACE_SRGB);

    /// <summary>Linear sRGB colorspace (default for floating point surfaces). Equivalent to DXGI_COLOR_SPACE_RGB_FULL_G10_NONE_P709.</summary>
    public static readonly Colorspace SRGBLinear = new(SDL_Colorspace.SDL_COLORSPACE_SRGB_LINEAR);

    /// <summary>HDR10 colorspace (non-linear HDR, default for 10-bit surfaces). Equivalent to DXGI_COLOR_SPACE_RGB_FULL_G2084_NONE_P2020.</summary>
    public static readonly Colorspace HDR10 = new(SDL_Colorspace.SDL_COLORSPACE_HDR10);

    /// <summary>JPEG colorspace. Equivalent to DXGI_COLOR_SPACE_YCBCR_FULL_G22_NONE_P709_X601.</summary>
    public static readonly Colorspace JPEG = new(SDL_Colorspace.SDL_COLORSPACE_JPEG);

    /// <summary>BT.601 colorspace with limited range. Equivalent to DXGI_COLOR_SPACE_YCBCR_STUDIO_G22_LEFT_P601.</summary>
    public static readonly Colorspace BT601Limited = new(SDL_Colorspace.SDL_COLORSPACE_BT601_LIMITED);

    /// <summary>BT.601 colorspace with full range.</summary>
    public static readonly Colorspace BT601Full = new(SDL_Colorspace.SDL_COLORSPACE_BT601_FULL);

    /// <summary>BT.709 colorspace with limited range. Equivalent to DXGI_COLOR_SPACE_YCBCR_STUDIO_G22_LEFT_P709.</summary>
    public static readonly Colorspace BT709Limited = new(SDL_Colorspace.SDL_COLORSPACE_BT709_LIMITED);

    /// <summary>BT.709 colorspace with full range.</summary>
    public static readonly Colorspace BT709Full = new(SDL_Colorspace.SDL_COLORSPACE_BT709_FULL);

    /// <summary>BT.2020 colorspace with limited range. Equivalent to DXGI_COLOR_SPACE_YCBCR_STUDIO_G22_LEFT_P2020.</summary>
    public static readonly Colorspace BT2020Limited = new(SDL_Colorspace.SDL_COLORSPACE_BT2020_LIMITED);

    /// <summary>BT.2020 colorspace with full range. Equivalent to DXGI_COLOR_SPACE_YCBCR_FULL_G22_LEFT_P2020.</summary>
    public static readonly Colorspace BT2020Full = new(SDL_Colorspace.SDL_COLORSPACE_BT2020_FULL);

    /// <summary>Default colorspace for RGB surfaces (sRGB).</summary>
    public static readonly Colorspace RgbDefault = new(SDL_Colorspace.SDL_COLORSPACE_RGB_DEFAULT);

    /// <summary>Default colorspace for YUV surfaces (JPEG).</summary>
    public static readonly Colorspace YuvDefault = new(SDL_Colorspace.SDL_COLORSPACE_YUV_DEFAULT);

    /// <summary>
    /// Creates a custom colorspace.
    /// </summary>
    /// <param name="type">The color type.</param>
    /// <param name="range">The color range.</param>
    /// <param name="primaries">The color primaries.</param>
    /// <param name="transfer">The transfer characteristics.</param>
    /// <param name="matrix">The matrix coefficients.</param>
    /// <param name="chroma">The chroma sample location.</param>
    /// <returns>A custom colorspace.</returns>
    public Colorspace(ColorType type, ColorRange range, ColorPrimaries primaries, TransferCharacteristics transfer, MatrixCoefficients matrix, ChromaLocation chroma)
        : this((SDL_Colorspace)SDL_DEFINE_COLORSPACE(
            (SDL_ColorType)type,
            (SDL_ColorRange)range,
            (SDL_ColorPrimaries)primaries,
            (SDL_TransferCharacteristics)transfer,
            (SDL_MatrixCoefficients)matrix,
            (SDL_ChromaLocation)chroma))
    {
    }

    /// <summary>
    /// Gets the color type.
    /// </summary>
    public ColorType Type => (ColorType)SDL_COLORSPACETYPE(Value);

    /// <summary>
    /// Gets the color range.
    /// </summary>
    public ColorRange Range => (ColorRange)SDL_COLORSPACERANGE(Value);

    /// <summary>
    /// Gets the chroma sample location.
    /// </summary>
    public ChromaLocation ChromaLocation => (ChromaLocation)SDL_COLORSPACECHROMA(Value);

    /// <summary>
    /// Gets the color primaries.
    /// </summary>
    public ColorPrimaries Primaries => (ColorPrimaries)SDL_COLORSPACEPRIMARIES(Value);

    /// <summary>
    /// Gets the transfer characteristics.
    /// </summary>
    public TransferCharacteristics Transfer => (TransferCharacteristics)SDL_COLORSPACETRANSFER(Value);

    /// <summary>
    /// Gets the matrix coefficients.
    /// </summary>
    public MatrixCoefficients Matrix => (MatrixCoefficients)SDL_COLORSPACEMATRIX(Value);

    /// <summary>
    /// Gets a value indicating whether this colorspace uses BT601 (or BT470BG) matrix coefficients.
    /// </summary>
    public bool IsMatrixBT601 => SDL_ISCOLORSPACE_MATRIX_BT601(Value);

    /// <summary>
    /// Gets a value indicating whether this colorspace uses BT709 matrix coefficients.
    /// </summary>
    public bool IsMatrixBT709 => SDL_ISCOLORSPACE_MATRIX_BT709(Value);

    /// <summary>
    /// Gets a value indicating whether this colorspace uses BT2020_NCL matrix coefficients.
    /// </summary>
    public bool IsMatrixBT2020NCL => SDL_ISCOLORSPACE_MATRIX_BT2020_NCL(Value);

    /// <summary>
    /// Gets a value indicating whether this colorspace has a limited range.
    /// </summary>
    public bool IsLimitedRange => SDL_ISCOLORSPACE_LIMITED_RANGE(Value);

    /// <summary>
    /// Gets a value indicating whether this colorspace has a full range.
    /// </summary>
    public bool IsFullRange => SDL_ISCOLORSPACE_FULL_RANGE(Value);
}
