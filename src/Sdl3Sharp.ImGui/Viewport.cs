using Sdl3Sharp.ImGui.Native;
using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents an ImGui viewport.
/// </summary>
/// <remarks>
/// <para>
/// Currently represents the Platform Window created by the application which is hosting Dear ImGui windows.
/// In 'docking' branch with multi-viewport enabled, we extend this concept to have multiple active viewports.
/// In the future we will extend this concept further to also represent Platform Monitor and support a
/// "no main platform window" operation mode.
/// </para>
/// <para>
/// About Main Area vs Work Area:
/// <list type="bullet">
/// <item><description>Main Area = entire viewport.</description></item>
/// <item><description>Work Area = entire viewport minus sections used by main menu bars (for platform windows),
/// or by task bar (for platform monitor).</description></item>
/// </list>
/// Windows are generally trying to stay within the Work Area of their host viewport.
/// </para>
/// </remarks>
public unsafe sealed class Viewport
{
    internal ImGuiViewport* Native { get; }

    internal Viewport(ImGuiViewport* native)
    {
        Native = native;
    }

    /// <summary>
    /// Gets the unique identifier for the viewport.
    /// </summary>
    public Id ID => new(Native->ID);

    /// <summary>
    /// Gets or sets the viewport flags.
    /// </summary>
    /// <seealso cref="ViewportFlags"/>
    public ViewportFlags Flags
    {
        get => (ViewportFlags)Native->Flags;
        set => Native->Flags = (ImGuiViewportFlags)value;
    }

    /// <summary>
    /// Gets or sets the position of the viewport (Main Area).
    /// </summary>
    /// <remarks>
    /// Dear ImGui coordinates are the same as OS desktop/native coordinates.
    /// </remarks>
    public Point Position
    {
        get => new(Native->Pos);
        set => Native->Pos = value.Value;
    }

    /// <summary>
    /// Gets or sets the size of the viewport (Main Area).
    /// </summary>
    public Vec2 Size
    {
        get => new(Native->Size);
        set => Native->Size = value.Value;
    }

    /// <summary>
    /// Gets the density of the viewport for Retina display.
    /// </summary>
    /// <remarks>
    /// Always (1, 1) on Windows, may be (2, 2) etc on macOS/iOS. This will affect font rasterizer density.
    /// </remarks>
    public Vec2 FramebufferScale => new(Native->FramebufferScale);

    /// <summary>
    /// Gets or sets the position of the viewport minus task bars, menus bars, status bars (Work Area).
    /// </summary>
    /// <remarks>
    /// This value is always greater than or equal to <see cref="Pos"/>.
    /// </remarks>
    public Point WorkPosition
    {
        get => new(Native->WorkPos);
        set => Native->WorkPos = value.Value;
    }

    /// <summary>
    /// Gets or sets the size of the viewport minus task bars, menu bars, status bars (Work Area).
    /// </summary>
    /// <remarks>
    /// This value is always less than or equal to <see cref="Size"/>.
    /// </remarks>
    public Size WorkSize
    {
        get => new(Native->WorkSize);
        set => Native->WorkSize = value.Value;
    }

    /// <summary>
    /// Gets the higher-level, platform window handle (e.g., HWND, GLFWWindow*, SDL_Window*).
    /// </summary>
    public nint PlatformHandle => (nint)Native->PlatformHandle;

    /// <summary>
    /// Gets the lower-level, platform-native window handle.
    /// </summary>
    /// <remarks>
    /// Under Win32 this is expected to be a HWND, unused for other platforms.
    /// </remarks>
    public nint PlatformHandleRaw => (nint)Native->PlatformHandleRaw;

    /// <summary>
    /// Gets the center of the viewport (Main Area).
    /// </summary>
    /// <returns>The center point of the viewport.</returns>
    public Point Center => new(ImGuiViewport.GetCenter(Native));

    /// <summary>
    /// Gets the center of the Work Area.
    /// </summary>
    /// <returns>The center point of the Work Area.</returns>
    public Point WorkCenter => new(ImGuiViewport.GetWorkCenter(Native));

    /// <summary>
    /// Gets the background draw list for the current viewport.
    /// </summary>
    /// <returns>The background draw list.</returns>
    /// <remarks>
    /// This draw list will be the first rendering one. Useful to quickly draw shapes/text behind dear ImGui contents.
    /// </remarks>
    public static DrawList? BackgroundDrawList
    {
        get
        {
            ImDrawList* drawList = ImGui_GetBackgroundDrawList();
            return drawList != null ? new DrawList(drawList) : null;
        }
    }

    /// <summary>
    /// Gets the foreground draw list for the current viewport.
    /// </summary>
    /// <returns>The foreground draw list.</returns>
    /// <remarks>
    /// This draw list will be the last rendered one. Useful to quickly draw shapes/text over dear ImGui contents.
    /// </remarks>
    public static DrawList? ForegroundDrawList
    {
        get
        {
            ImDrawList* drawList = ImGui_GetForegroundDrawList();
            return drawList != null ? new DrawList(drawList) : null;
        }
    }
}
