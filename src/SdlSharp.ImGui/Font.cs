using System.Runtime.InteropServices;
using System.Text;
using static SdlSharp.Native.Common;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Flags for a <see cref="Font"/>.
/// </summary>
[Flags]
public enum FontFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>
    /// Disable erroring/asserting when the <c>FontAtlas.AddFont*</c> call has a missing file or
    /// bad data; check <see cref="Font.IsValid"/>/<see cref="Font.IsLoaded"/> on the result instead.
    /// </summary>
    NoLoadError = 1 << 1,
    /// <summary>[Internal] Disable loading new glyphs.</summary>
    NoLoadGlyphs = 1 << 2,
    /// <summary>[Internal] Disable loading new baked sizes and garbage-collecting current ones.</summary>
    LockBakedSizes = 1 << 3,
}

/// <summary>
/// A handle to an ImGui font loaded into the font atlas. Obtain via
/// <see cref="FontAtlas"/> methods (e.g. <see cref="FontAtlas.AddFontFromFileTTF"/>)
/// or from <see cref="ImGui.GetFont"/>. The handle is owned by the font atlas — do not dispose.
/// </summary>
public readonly unsafe struct Font
{
    internal readonly IGSharp_Font* Handle;

    internal Font(IGSharp_Font* handle) => Handle = handle;

    /// <summary>True if this handle refers to a valid font.</summary>
    public bool IsValid => Handle != null;

    /// <summary>True if the font is fully loaded and usable.</summary>
    public bool IsLoaded => IGSharp_Font_IsLoaded(Handle);

    /// <summary>Display name for debugging (typically derived from the source filename).</summary>
    public string DebugName => Marshal.PtrToStringUTF8((nint)IGSharp_Font_GetDebugName(Handle)) ?? string.Empty;

    /// <summary>The <see cref="FontAtlas"/> this font was loaded into.</summary>
    public FontAtlas OwnerAtlas => new(IGSharp_Font_GetOwnerAtlas(Handle));

    /// <summary>Font behavior flags.</summary>
    public FontFlags Flags
    {
        get => (FontFlags)IGSharp_Font_GetFlags(Handle);
        set => IGSharp_Font_SetFlags(Handle, (int)value);
    }

    /// <summary>Character drawn in place of glyphs missing from the font. (char)0 = auto (U+FFFD or '?').</summary>
    public char FallbackChar
    {
        get => (char)IGSharp_Font_GetFallbackChar(Handle);
        set => IGSharp_Font_SetFallbackChar(Handle, value);
    }

    /// <summary>Character used for the "..." ellipsis when text is clipped.</summary>
    public char EllipsisChar
    {
        get => (char)IGSharp_Font_GetEllipsisChar(Handle);
        set => IGSharp_Font_SetEllipsisChar(Handle, value);
    }

    /// <summary>Whether the ellipsis character is automatically baked when missing from the font.</summary>
    public bool EllipsisAutoBake
    {
        get => IGSharp_Font_GetEllipsisAutoBake(Handle);
        set => IGSharp_Font_SetEllipsisAutoBake(Handle, value);
    }

    /// <summary>
    /// The size passed to <c>FontAtlas.AddFont*</c>, kept for backward compatibility. With the
    /// dynamic font system fonts render at any size; prefer passing an explicit size to
    /// <see cref="ImGui.PushFont"/>.
    /// </summary>
    public float LegacySize
    {
        get => IGSharp_Font_GetLegacySize(Handle);
        set => IGSharp_Font_SetLegacySize(Handle, value);
    }

    /// <summary>True if the font's sources contain a glyph for <paramref name="c"/> (may load it to find out).</summary>
    public bool IsGlyphInFont(char c) => IGSharp_Font_IsGlyphInFont(Handle, c);

    /// <summary>
    /// True if no glyph in the inclusive codepoint range [<paramref name="first"/>,
    /// <paramref name="last"/>] is present in the font's sources. Useful to detect missing coverage.
    /// </summary>
    public bool IsGlyphRangeUnused(uint first, uint last) => IGSharp_Font_IsGlyphRangeUnused(Handle, first, last);

    /// <summary>
    /// Gets (baking on demand) this font's data for a given size and rasterizer density.
    /// Pass <paramref name="density"/> = -1 to use the current context's font density.
    /// The returned view is transient — see <see cref="FontBaked"/>.
    /// </summary>
    public FontBaked GetFontBaked(float fontSize, float density = -1f)
        => new(IGSharp_Font_GetFontBaked(Handle, fontSize, density));

    /// <summary>
    /// Measures <paramref name="text"/> rendered with this font at <paramref name="size"/> pixels.
    /// <paramref name="maxWidth"/> stops measuring past that width; <paramref name="wrapWidth"/>
    /// &gt; 0 enables word wrapping at that width.
    /// </summary>
    public Vec2 CalcTextSize(float size, string text, float maxWidth = float.MaxValue, float wrapWidth = 0f)
    {
        var v = IGSharp_Font_CalcTextSizeA(Handle, size, maxWidth, wrapWidth, ToUtf8(text), null, null);
        return new Vec2(v.X, v.Y);
    }

    /// <summary>
    /// Returns the index in <paramref name="text"/> at which a line rendered at
    /// <paramref name="size"/> pixels should word-wrap to stay within <paramref name="wrapWidth"/>.
    /// </summary>
    public int CalcWordWrapPosition(float size, string text, float wrapWidth)
    {
        var utf8 = ToUtf8(text)!;
        fixed (byte* p = utf8)
        {
            var wrap = IGSharp_Font_CalcWordWrapPosition(Handle, size, new ReadOnlySpan<byte>(p, utf8.Length), null, wrapWidth);
            var byteOffset = (int)(wrap - p);
            return Encoding.UTF8.GetCharCount(utf8, 0, byteOffset);
        }
    }

    /// <summary>Makes <paramref name="from"/> render as <paramref name="to"/> (e.g. remap a missing character to a substitute).</summary>
    public void AddRemapChar(char from, char to) => IGSharp_Font_AddRemapChar(Handle, from, to);
}
