using Sdl3Sharp.Graphics;
using Sdl3Sharp.ImGui.Native.Backends;
using static Sdl3Sharp.Native.Events;

using SdlWindow = Sdl3Sharp.Graphics.Window;

namespace Sdl3Sharp.ImGui.Backends;

/// <summary>
/// Provides SDL3 platform backend initialization and event processing for Dear ImGui.
/// </summary>
/// <remarks>
/// This backend handles window and input management (keyboard, mouse, gamepad) for ImGui
/// when using SDL3. You must initialize this backend before using ImGui with SDL3.
/// </remarks>
public static unsafe class SDL3Backend
{
    /// <summary>
    /// Initializes the SDL3 platform backend for use with SDL_Renderer.
    /// </summary>
    /// <param name="window">The SDL window to use for input.</param>
    /// <param name="renderer">The SDL renderer to use.</param>
    /// <returns><c>true</c> if initialization was successful; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// Call this after creating your ImGui context and before the main loop.
    /// </remarks>
    public static bool InitForSDLRenderer(SdlWindow window, Renderer renderer)
    {
        ArgumentNullException.ThrowIfNull(window);
        ArgumentNullException.ThrowIfNull(renderer);
        return ImGuiSdl3.InitForSDLRenderer(window.Handle, renderer.Handle);
    }

    /// <summary>
    /// Initializes the SDL3 platform backend for use with SDL_GPU.
    /// </summary>
    /// <param name="window">The SDL window to use for input.</param>
    /// <returns><c>true</c> if initialization was successful; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// Call this after creating your ImGui context and before the main loop.
    /// </remarks>
    public static bool InitForSDLGPU(SdlWindow window)
    {
        ArgumentNullException.ThrowIfNull(window);
        return ImGuiSdl3.InitForSDLGPU(window.Handle);
    }

    /// <summary>
    /// Shuts down the SDL3 platform backend.
    /// </summary>
    /// <remarks>
    /// Call this before destroying your ImGui context during application shutdown.
    /// </remarks>
    public static void Shutdown()
    {
        ImGuiSdl3.Shutdown();
    }

    /// <summary>
    /// Starts a new ImGui frame for the SDL3 platform backend.
    /// </summary>
    /// <remarks>
    /// Call this at the beginning of each frame, before calling <see cref="Context.NewFrame"/>.
    /// </remarks>
    public static void NewFrame()
    {
        ImGuiSdl3.NewFrame();
    }

    /// <summary>
    /// Processes an SDL event for ImGui input handling.
    /// </summary>
    /// <param name="sdlEvent">The SDL event to process.</param>
    /// <returns><c>true</c> if ImGui consumed the event and it should not be processed further; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// Call this for each SDL event in your event loop. When this returns <c>true</c>,
    /// ImGui has consumed the event (e.g., a mouse click on an ImGui widget) and your
    /// application should generally not process it further.
    /// </remarks>
    public static bool ProcessEvent(Event sdlEvent)
    {
        SDL_Event nativeEvent = sdlEvent.Native;
        return ImGuiSdl3.ProcessEvent(&nativeEvent);
    }

    /// <summary>
    /// Sets the gamepad input mode for ImGui.
    /// </summary>
    /// <param name="mode">The gamepad mode to use.</param>
    /// <remarks>
    /// Gamepad selection automatically starts in <see cref="GamepadMode.AutoFirst"/> mode,
    /// picking the first available SDL_Gamepad. Use this method to override this behavior.
    /// </remarks>
    public static void SetGamepadMode(ImGuiSdl3GamepadMode mode)
    {
        ImGuiSdl3.SetGamepadModeEx(mode, 0, 0);
    }

    /// <summary>
    /// Sets the gamepad input mode for ImGui with manual gamepad specification.
    /// </summary>
    /// <param name="mode">The gamepad mode to use.</param>
    /// <param name="gamepads">An array of gamepad handles to use when in manual mode.</param>
    /// <remarks>
    /// When using <see cref="GamepadMode.Manual"/>, the caller is responsible for opening
    /// and closing gamepads. The <paramref name="gamepads"/> array contains pointers to
    /// SDL_Gamepad objects that ImGui should use for input.
    /// </remarks>
    public static void SetGamepadMode(ImGuiSdl3GamepadMode mode, nint[] gamepads)
    {
        ArgumentNullException.ThrowIfNull(gamepads);
        fixed (nint* gamepadsPtr = gamepads)
        {
            ImGuiSdl3.SetGamepadModeEx(mode, (nint)gamepadsPtr, gamepads.Length);
        }
    }
}
