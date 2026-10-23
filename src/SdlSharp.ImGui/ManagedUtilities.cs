using System.Text;
using static SdlSharp.ImGui.Native;
namespace SdlSharp.ImGui;

/// <summary>Builds immutable pairs of inclusive BMP glyph ranges for FontConfig.</summary>
public sealed unsafe class GlyphRangesBuilder : IDisposable
{
    private readonly IGSharp_FontGlyphRangesBuilder* _handle;
    private readonly Lifetime _lifetime;
    private IGSharp_FontGlyphRangesBuilder* Handle { get { _lifetime.Check(); return _handle; } }
    /// <summary>Creates an empty builder.</summary>
    public GlyphRangesBuilder()
    {
        _handle = IGSharp_FontGlyphRangesBuilder_Create(); var pointer = (nint)_handle;
        _lifetime = Lifetime.Own(pointer, () => IGSharp_FontGlyphRangesBuilder_Destroy((IGSharp_FontGlyphRangesBuilder*)pointer), threadIndependent: true);
    }
    /// <summary>Adds one BMP character.</summary>
    public void Add(char character) { using var lease = _lifetime.Borrow(); IGSharp_FontGlyphRangesBuilder_AddChar(Handle, character); }
    /// <summary>Adds all characters in UTF-8 text.</summary>
    public void AddText(ReadOnlySpan<byte> utf8)
    { using var lease = _lifetime.Borrow(); fixed (byte* pointer = utf8) IGSharp_FontGlyphRangesBuilder_AddText(Handle, pointer, (nuint)utf8.Length); }
    /// <summary>Adds all characters in a string.</summary>
    public void AddText(string text) { ArgumentNullException.ThrowIfNull(text); AddText(Encoding.UTF8.GetBytes(text)); }
    /// <summary>Tests whether the builder includes a character.</summary>
    public bool Contains(char character) { using var lease = _lifetime.Borrow(); return IGSharp_FontGlyphRangesBuilder_GetBit(Handle, character); }
    /// <summary>Adds pairs of inclusive, nonzero BMP codepoints.</summary>
    public void AddRanges(ReadOnlySpan<ushort> ranges)
    {
        using var lease = _lifetime.Borrow();
        if (ranges.IsEmpty) return;
        var pointer = FontConfig.CopyRanges(ranges);
        try { IGSharp_FontGlyphRangesBuilder_AddRanges(Handle, pointer); } finally { IGSharp_MemFree(pointer); }
    }
    /// <summary>Copies sorted range pairs; the native terminator is omitted.</summary>
    public ushort[] Build()
    {
        using var lease = _lifetime.Borrow();
        var count = IGSharp_FontGlyphRangesBuilder_BuildRanges(Handle, null, 0);
        var ranges = new ushort[count];
        fixed (ushort* pointer = ranges) IGSharp_FontGlyphRangesBuilder_BuildRanges(Handle, pointer, count);
        return count == 0 ? [] : ranges[..^1];
    }
    /// <summary>Clears all characters.</summary>
    public void Clear() { using var lease = _lifetime.Borrow(); IGSharp_FontGlyphRangesBuilder_Clear(Handle); }
    /// <summary>Releases the builder.</summary>
    public void Dispose() => _lifetime.Dispose();
}

/// <summary>A frame-bound scope that balances an ImGui Begin/End or Push/Pop pair.</summary>
public sealed class UiScope : IDisposable
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2213", Justification = "Borrowed context; disposing a UI scope must not destroy its context.")]
    private readonly Context _context = Context.Current;
    private readonly int _frame = ImGui.GetFrameCount();
    private Action? _end;
    internal UiScope(bool visible, Action? end) { Visible = visible; _end = end; }
    /// <summary>Whether contents should be submitted.</summary>
    public bool Visible { get; }
    /// <summary>Balances the scope once. Dispose on the same context and frame.</summary>
    public void Dispose()
    {
        if (_end == null) return;
        _context.CheckCurrent();
        if (ImGui.GetFrameCount() != _frame) throw new InvalidOperationException("The UI scope outlived its frame.");
        _end(); _end = null;
    }
}

public static unsafe partial class ImGui
{
    /// <summary>Begins a window. Always dispose the scope, even when Visible is false.</summary>
    public static UiScope Window(string title, WindowFlags flags = WindowFlags.None) => new(Begin(title, flags), End);
    /// <summary>Begins a child. Always dispose the scope, even when Visible is false.</summary>
    public static UiScope Child(string id, Vec2 size, ChildFlags flags = ChildFlags.None) => new(BeginChild(id, size.X, size.Y, flags), EndChild);
    /// <summary>Begins a table; disposal calls EndTable only when BeginTable succeeded.</summary>
    public static UiScope Table(string id, int columns, TableFlags flags = TableFlags.None)
    { var visible = BeginTable(id, columns, flags); return new(visible, visible ? EndTable : null); }
    /// <summary>Pushes an ID until scope disposal.</summary>
    public static UiScope IdScope(string id) { PushID(id); return new(true, PopID); }
    /// <summary>Pushes a font until scope disposal.</summary>
    public static UiScope FontScope(Font font, float size = 0) { PushFont(font, size); return new(true, PopFont); }
    /// <summary>Overrides a style color until scope disposal.</summary>
    public static UiScope ColorScope(Col color, uint value) { PushStyleColor(color, value); return new(true, () => PopStyleColor()); }
    /// <summary>Overrides a scalar style variable until scope disposal.</summary>
    public static UiScope StyleScope(StyleVar variable, float value) { PushStyleVar(variable, value); return new(true, () => PopStyleVar()); }
    /// <summary>Overrides a vector style variable until scope disposal.</summary>
    public static UiScope StyleScope(StyleVar variable, Vec2 value) { PushStyleVar(variable, value.X, value.Y); return new(true, () => PopStyleVar()); }
    /// <summary>Pushes a clip rectangle until scope disposal.</summary>
    public static UiScope ClipScope(Vec2 min, Vec2 max, bool intersect = true) { PushClipRect(min, max, intersect); return new(true, PopClipRect); }
    /// <summary>Hashes an exact UTF-8 range without allocating or requiring a terminator.</summary>
    public static uint GetID(ReadOnlySpan<byte> utf8) { Context.Current.CheckCurrent(); fixed (byte* p = utf8) return IGSharp_GetIDStrRange(p, (nuint)utf8.Length); }
    /// <summary>Pushes an exact UTF-8 ID range.</summary>
    public static void PushID(ReadOnlySpan<byte> utf8) { Context.Current.CheckCurrent(); fixed (byte* p = utf8) IGSharp_PushIDStrRange(p, (nuint)utf8.Length); }
}

/// <summary>Allows an action to run once per frame of its creating context without a native allocation.</summary>
public sealed class FrameGate
{
    private readonly Context _context = Context.Current;
    private int _frame = -1;
    /// <summary>The last frame admitted by TryEnter, or -1 before first use.</summary>
    public int LastFrame => _frame;
    /// <summary>Returns true once per frame; validates the owning context and thread.</summary>
    public bool TryEnter()
    {
        _context.CheckCurrent(); var frame = ImGui.GetFrameCount();
        if (_frame == frame) return false;
        _frame = frame; return true;
    }
}
