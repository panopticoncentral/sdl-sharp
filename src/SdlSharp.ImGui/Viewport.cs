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

    /// <summary>Unique identifier of the viewport.</summary>
    public uint Id => IGSharp_Viewport_GetID(Handle);

    /// <summary>Viewport flags (platform window / monitor / app-owned).</summary>
    public ViewportFlags Flags => (ViewportFlags)IGSharp_Viewport_GetFlags(Handle);

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

    /// <summary>Ratio of framebuffer pixels to viewport units (for Retina / HiDPI displays).</summary>
    public Vec2 FramebufferScale
    {
        get
        {
            var v = IGSharp_Viewport_GetFramebufferScale(Handle);
            return new Vec2(v.X, v.Y);
        }
    }

    /// <summary>Center of the main area (helper: Pos + Size * 0.5).</summary>
    public Vec2 Center
    {
        get
        {
            var v = IGSharp_Viewport_GetCenter(Handle);
            return new Vec2(v.X, v.Y);
        }
    }

    /// <summary>Center of the work area (helper: WorkPos + WorkSize * 0.5).</summary>
    public Vec2 WorkCenter
    {
        get
        {
            var v = IGSharp_Viewport_GetWorkCenter(Handle);
            return new Vec2(v.X, v.Y);
        }
    }

    /// <summary>
    /// Platform/backend-specific handle for the viewport (e.g. an <c>SDL_Window*</c>).
    /// Owned by the backend; treat as opaque.
    /// </summary>
    public IntPtr PlatformHandle
    {
        get => (IntPtr)IGSharp_Viewport_GetPlatformHandle(Handle);
        set => IGSharp_Viewport_SetPlatformHandle(Handle, (void*)value);
    }

    /// <summary>
    /// Lower-level platform-native handle (e.g. an <c>HWND</c> / <c>NSWindow*</c>),
    /// when the backend exposes one in addition to <see cref="PlatformHandle"/>.
    /// </summary>
    public IntPtr PlatformHandleRaw
    {
        get => (IntPtr)IGSharp_Viewport_GetPlatformHandleRaw(Handle);
        set => IGSharp_Viewport_SetPlatformHandleRaw(Handle, (void*)value);
    }
}
