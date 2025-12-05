using System.Runtime.InteropServices;
using Sdl3Sharp.ImGui.Native;

using ImGuiNative = Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a Dear ImGui context that manages the state for a single ImGui instance.
/// </summary>
public unsafe sealed class Context : IDisposable
{
    private bool _ownsPointer;

    internal Context(ImGuiContext* native, bool ownsPointer)
    {
        Native = native;
        _ownsPointer = ownsPointer;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Context"/> struct by creating a new ImGui context.
    /// </summary>
    /// <param name="sharedFontAtlas">Optional shared font atlas. If null, a new font atlas is created.</param>
    /// <remarks>
    /// The created context will be destroyed when <see cref="Dispose"/> is called.
    /// </remarks>
    public static Context CreateContext(FontAtlas? sharedFontAtlas = null)
    {
        return new(ImGuiNative.CreateContext(sharedFontAtlas == null ? null : sharedFontAtlas.Native), true);
    }

    internal ImGuiContext* Native { get; private set; }

    /// <summary>
    /// Gets the current ImGui context.
    /// </summary>
    /// <returns>A <see cref="Context"/> wrapping the current context, or a default context if none is set.</returns>
    public static Context Current
    {
        get => new(ImGuiNative.GetCurrentContext(), false);
        set => ImGuiNative.SetCurrentContext(value.Native);
    }

    /// <summary>
    /// Gets the IO configuration and state for this context.
    /// </summary>
    /// <remarks>
    /// This context must be current before calling this method.
    /// </remarks>
    public static IO IO => new(ImGuiNative.GetIO());

    // For the moment, we are not supporting the platform IO since Windows is handled.

    /// <summary>
    /// Gets the style configuration for this context.
    /// </summary>
    /// <remarks>
    /// This context must be current before calling this method.
    /// </remarks>
    public static Style Style => new(ImGuiNative.GetStyle());

    /// <summary>
    /// Starts a new Dear ImGui frame.
    /// </summary>
    /// <remarks>
    /// You can submit any command from this point until <see cref="Render"/> or <see cref="EndFrame"/>.
    /// This context must be current before calling this method.
    /// </remarks>
    public static void NewFrame()
    {
        ImGuiNative.NewFrame();
    }

    /// <summary>
    /// Ends the Dear ImGui frame without rendering.
    /// </summary>
    /// <remarks>
    /// This is automatically called by <see cref="Render"/>. If you don't need to render data
    /// (skipping rendering), you may call EndFrame() without Render(), but you'll have wasted CPU already.
    /// If you don't need to render, it's better to not create any windows and not call NewFrame() at all.
    /// This context must be current before calling this method.
    /// </remarks>
    public static void EndFrame()
    {
        ImGuiNative.EndFrame();
    }

    /// <summary>
    /// Ends the Dear ImGui frame and finalizes the draw data.
    /// </summary>
    /// <remarks>
    /// After calling this, you can get the draw data using <see cref="GetDrawData"/>.
    /// This context must be current before calling this method.
    /// </remarks>
    public static void Render()
    {
        ImGuiNative.Render();
    }

    // For now, we don't expose draw data since it's mainly used by the backend.

    /// <summary>
    /// Gets the compiled ImGui version string.
    /// </summary>
    /// <returns>The version string, e.g., "1.92.0".</returns>
    public static string Version
    {
        get
        {
            var versionPtr = ImGuiNative.GetVersion();
            return Marshal.PtrToStringUTF8((nint)versionPtr) ?? string.Empty;
        }
    }

    /// <summary>
    /// Applies the dark color style (default).
    /// </summary>
    public static void StyleColorsDark()
    {
        ImGuiNative.StyleColorsDark(null);
    }

    /// <summary>
    /// Applies the light color style.
    /// </summary>
    public static void StyleColorsLight()
    {
        ImGuiNative.StyleColorsLight(null);
    }

    /// <summary>
    /// Applies the classic ImGui color style.
    /// </summary>
    public static void StyleColorsClassic()
    {
        ImGuiNative.StyleColorsClassic(null);
    }

    /// <summary>
    /// Shows the ImGui demo window.
    /// </summary>
    /// <param name="open">Optional pointer to a bool that controls whether the window is open.</param>
    /// <remarks>
    /// Demonstrates most ImGui features. Call this to learn about the library!
    /// This context must be current before calling this method.
    /// </remarks>
    public static void ShowDemoWindow(StateRef<bool>? open = null)
    {
        ImGuiNative.ShowDemoWindow(open == null ? null : open.Value.Ptr);
    }

    /// <summary>
    /// Shows the ImGui metrics/debugger window.
    /// </summary>
    /// <param name="open">Optional pointer to a bool that controls whether the window is open.</param>
    /// <remarks>
    /// Displays Dear ImGui internals: windows, draw commands, various internal state, etc.
    /// This context must be current before calling this method.
    /// </remarks>
    public static void ShowMetricsWindow(StateRef<bool>? open = null)
    {
        ImGuiNative.ShowMetricsWindow(open == null ? null : open.Value.Ptr);
    }

    /// <summary>
    /// Shows the ImGui debug log window.
    /// </summary>
    /// <param name="open">Optional pointer to a bool that controls whether the window is open.</param>
    /// <remarks>
    /// Displays a simplified log of important Dear ImGui events.
    /// This context must be current before calling this method.
    /// </remarks>
    public static void ShowDebugLogWindow(StateRef<bool>? open = null)
    {
        ImGuiNative.ShowDebugLogWindow(open == null ? null : open.Value.Ptr);
    }

    /// <summary>
    /// Shows the ImGui about window.
    /// </summary>
    /// <param name="open">Optional pointer to a bool that controls whether the window is open.</param>
    /// <remarks>
    /// Displays Dear ImGui version, credits, and build/system information.
    /// This context must be current before calling this method.
    /// </remarks>
    public static void ShowAboutWindow(StateRef<bool>? open = null)
    {
        ImGuiNative.ShowAboutWindow(open == null ? null : open.Value.Ptr);
    }

    /// <summary>
    /// Disposes the context, destroying it if this instance owns the pointer.
    /// </summary>
    /// <remarks>
    /// If the context was created using the constructor with optional font atlas, this will destroy the context.
    /// If the context was created by wrapping an existing pointer, this does nothing.
    /// </remarks>
    public void Dispose()
    {
        if (_ownsPointer && Native != null)
        {
            ImGuiNative.DestroyContext(Native);
            Native = null;
            _ownsPointer = false;
        }
    }
}
