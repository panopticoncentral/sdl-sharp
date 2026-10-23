using System.Runtime.InteropServices;
using System.Text;
using static SdlSharp.Native.Common;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// A handle to an ImGui font loaded into the font atlas. Obtain via
/// <see cref="FontAtlas"/> methods (e.g. <see cref="FontAtlas.AddFontFromFileTTF"/>)
/// or from <see cref="ImGui.GetFont"/>. The handle is owned by the font atlas — do not dispose.
/// </summary>
public readonly unsafe struct Font
{
    private readonly NativeView<IGSharp_Font> _view;
    internal IGSharp_Font* Handle => _view.Handle;
    internal Lifetime Lifetime => _view.Lifetime!;

    internal Font(IGSharp_Font* handle, Lifetime? lifetime = null, bool? frame = null) => _view = new(handle, lifetime ?? (handle == null ? null : SdlSharp.ImGui.Lifetime.Capture((nint)handle, SdlSharp.ImGui.Lifetime.Capture((nint)IGSharp_Font_GetOwnerAtlas(handle)))), frame ?? false);

    /// <summary>True if this handle refers to a valid font.</summary>
    public bool IsValid => _view.IsValid;

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
        { var pointer = IGSharp_Font_GetFontBaked(Handle, fontSize, density); return new(pointer, Lifetime.Capture((nint)pointer, Lifetime)); }

    /// <summary>
    /// Measures <paramref name="text"/> rendered with this font at <paramref name="size"/> pixels.
    /// <paramref name="maxWidth"/> stops measuring past that width; <paramref name="wrapWidth"/>
    /// &gt; 0 enables word wrapping at that width.
    /// </summary>
    public Vec2 CalcTextSize(float size, string text, float maxWidth = float.MaxValue, float wrapWidth = 0f)
    {
        return CalcTextSize(size, text, out _, maxWidth, wrapWidth);
    }

    /// <summary>Measures text and returns the consumed prefix length in UTF-16 code units.</summary>
    public Vec2 CalcTextSize(float size, string text, out int charactersConsumed, float maxWidth = float.MaxValue, float wrapWidth = 0f)
    {
        ArgumentNullException.ThrowIfNull(text);
        var utf8 = Encoding.UTF8.GetBytes(text);
        var result = CalcTextSize(size, utf8, out var bytesConsumed, maxWidth, wrapWidth);
        charactersConsumed = Encoding.UTF8.GetCharCount(utf8.AsSpan(0, bytesConsumed));
        return result;
    }

    /// <summary>Measures an exact UTF-8 range and returns the consumed prefix length in bytes.</summary>
    public Vec2 CalcTextSize(float size, ReadOnlySpan<byte> text, out int bytesConsumed, float maxWidth = float.MaxValue, float wrapWidth = 0f)
    {
        nuint remaining;
        var value = IGSharp_Font_CalcTextSizeA(Handle, size, maxWidth, wrapWidth, text, &remaining);
        bytesConsumed = checked((int)remaining);
        return new(value.X, value.Y);
    }

    /// <summary>
    /// Returns the index in <paramref name="text"/> at which a line rendered at
    /// <paramref name="size"/> pixels should word-wrap to stay within <paramref name="wrapWidth"/>.
    /// </summary>
    public int CalcWordWrapPosition(float size, string text, float wrapWidth)
    {
        var utf8 = ToUtf8Range(text);
        fixed (byte* p = utf8)
        {
            var wrap = IGSharp_Font_CalcWordWrapPosition(Handle, size, new ReadOnlySpan<byte>(p, utf8.Length), wrapWidth);
            var byteOffset = checked((int)wrap);
            return Encoding.UTF8.GetCharCount(utf8, 0, byteOffset);
        }
    }

    /// <summary>Makes <paramref name="from"/> render as <paramref name="to"/> (e.g. remap a missing character to a substitute).</summary>
    public void AddRemapCodepoint(char from, char to) => IGSharp_Font_AddRemapCodepoint(Handle, from, to);
    /// <summary>Maps a codepoint to a glyph index in a specified font source.</summary>
    public void AddRemapCodepointToGlyphIndex(char codepoint, int sourceIndex, int glyphIndex)
        => IGSharp_Font_AddRemapCodepointToGlyphIndex(Handle, codepoint, sourceIndex, glyphIndex);

    /// <summary>Renders one BMP character, optionally clipping its geometry and UVs to a rectangle.</summary>
    public void RenderCharacter(DrawList list, float size, Vec2 position, uint color, char character, Vec4? clip = null)
    {
        var rectangle = clip.GetValueOrDefault();
        var native = new IGSharp_Vec4(rectangle.X, rectangle.Y, rectangle.Z, rectangle.W);
        IGSharp_Font_RenderChar(Handle, list.MutableHandle, size, new(position.X, position.Y), color, character, clip.HasValue ? &native : null);
    }
    /// <summary>Renders an exact UTF-8 range with explicit clipping and optional wrapping.</summary>
    public void RenderText(DrawList list, float size, Vec2 position, uint color, Vec4 clip, ReadOnlySpan<byte> text, float wrapWidth = 0)
    { fixed (byte* p = text) IGSharp_Font_RenderText(Handle, list.MutableHandle, size, new(position.X, position.Y), color, new(clip.X, clip.Y, clip.Z, clip.W), p, (nuint)text.Length, wrapWidth, 0); }
    /// <summary>Clears baked sizes and glyphs. Existing baked views expire.</summary>
    public void ClearOutputData() { Lifetime.EnsureUnretained(); IGSharp_Font_ClearOutputData(Handle); Lifetime.InvalidateChildren(); }

}
