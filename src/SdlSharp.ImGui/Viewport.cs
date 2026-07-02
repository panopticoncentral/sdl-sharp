using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// A handle to an ImGui viewport — represents the area where ImGui windows are rendered.
/// Obtain via <see cref="ImGui.GetMainViewport"/>. The handle is only valid for the current frame.
/// </summary>
public readonly unsafe struct Viewport
{
    internal readonly IGSharp_Viewport* Handle;

    internal Viewport(IGSharp_Viewport* handle) => Handle = handle;

    /// <summary>True if this handle refers to a valid viewport.</summary>
    public bool IsValid => Handle != null;

    /// <summary>Main Area: position of the viewport (the OS window client area).</summary>
    public Vec2 Pos
    {
        get
        {
            var v = IGSharp_Viewport_GetPos(Handle);
            return new Vec2(v.X, v.Y);
        }
    }

    /// <summary>Main Area: size of the viewport.</summary>
    public Vec2 Size
    {
        get
        {
            var v = IGSharp_Viewport_GetSize(Handle);
            return new Vec2(v.X, v.Y);
        }
    }

    /// <summary>Work Area: position of the viewport minus task bars, menu bars, status bars.</summary>
    public Vec2 WorkPos
    {
        get
        {
            var v = IGSharp_Viewport_GetWorkPos(Handle);
            return new Vec2(v.X, v.Y);
        }
    }

    /// <summary>Work Area: size of the viewport minus task bars, menu bars, status bars.</summary>
    public Vec2 WorkSize
    {
        get
        {
            var v = IGSharp_Viewport_GetWorkSize(Handle);
            return new Vec2(v.X, v.Y);
        }
    }
}
