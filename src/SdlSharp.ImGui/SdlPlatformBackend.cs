using Gamepad = SdlSharp.Input.Gamepad;
using SdlSharp.Graphics;
using SdlSharp.Input;
using SdlSharp.Native;
using static SdlSharp.ImGui.Native;
namespace SdlSharp.ImGui;

/// <summary>Rendering API used by a custom renderer with the SDL platform backend.</summary>
public enum PlatformRenderer
{
    /// <summary>Application-defined renderer.</summary>
    Other,
    /// <summary>Vulkan renderer.</summary>
    Vulkan,
    /// <summary>Metal renderer.</summary>
    Metal,
    /// <summary>Direct3D renderer.</summary>
    Direct3D,
    /// <summary>OpenGL renderer; supply its SDL GL context.</summary>
    OpenGL,
    /// <summary>SDL Renderer; supply its renderer owner.</summary>
    SdlRenderer,
    /// <summary>SDL GPU with an application-managed renderer.</summary>
    SdlGpu
}

/// <summary>Owns only the SDL platform backend for applications supplying their own renderer.</summary>
public sealed unsafe class SdlPlatformBackend : IDisposable
{
    private readonly Context _context;
    private readonly Lifetime _lifetime;
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2213", Justification = "Disposed by the shutdown action registered with the context-owned lifetime.")]
    private readonly PlatformGamepads _gamepads = new();
    private SdlPlatformBackend(Context context, Window window, GlContext? gl, Renderer? renderer)
    {
        _context = context; window.RetainDependent(); gl?.RetainDependent(); renderer?.RetainDependent();
        _lifetime = new(0, context, context.Lifetime, () =>
        {
            Application.RawEventFilter -= OnRawEvent; IGSharp_ImplSDL3_Shutdown(); _gamepads.Dispose();
            renderer?.ReleaseDependent(); gl?.ReleaseDependent(); window.ReleaseDependent();
        });
        Application.RawEventFilter += OnRawEvent;
    }
    /// <summary>Initializes the platform backend and retains the supplied SDL owners until shutdown.</summary>
    public static SdlPlatformBackend Init(Window window, PlatformRenderer kind = PlatformRenderer.Other, GlContext? glContext = null, Renderer? renderer = null)
    {
        ArgumentNullException.ThrowIfNull(window); var context = Context.Current; context.CheckCurrent();
        if (IGSharp_GetIO()->BackendPlatformUserData != null) throw new InvalidOperationException("This context already has a platform backend.");
        window.ValidateDependentOwner(); glContext?.ValidateDependentOwner(); renderer?.ValidateDependentOwner();
        var handle = window.Handle;
        var ok = kind switch
        {
            PlatformRenderer.Other => IGSharp_ImplSDL3_InitForOther(handle),
            PlatformRenderer.Vulkan => IGSharp_ImplSDL3_InitForVulkan(handle),
            PlatformRenderer.Metal => IGSharp_ImplSDL3_InitForMetal(handle),
            PlatformRenderer.Direct3D => IGSharp_ImplSDL3_InitForD3D(handle),
            PlatformRenderer.OpenGL => IGSharp_ImplSDL3_InitForOpenGL(handle, (glContext ?? throw new ArgumentNullException(nameof(glContext))).Handle),
            PlatformRenderer.SdlRenderer => IGSharp_ImplSDL3_InitForSDLRenderer(handle, (renderer ?? throw new ArgumentNullException(nameof(renderer))).Handle),
            PlatformRenderer.SdlGpu => IGSharp_ImplSDL3_InitForSDLGPU(handle),
            _ => throw new ArgumentOutOfRangeException(nameof(kind))
        };
        if (!ok) throw new InvalidOperationException("SDL platform initialization failed.");
        return new(context, window, glContext, renderer);
    }
    /// <summary>Updates platform IO, then starts an ImGui frame. Update the custom renderer first.</summary>
    public void NewFrame() { _lifetime.Check(); IGSharp_ImplSDL3_NewFrame(); _context.NewFrame(); }
    /// <summary>Selects automatic or manually supplied gamepads; manual owners are retained until replacement or shutdown.</summary>
    public void SetGamepadMode(ImplSDL3_GamepadMode mode, params Gamepad[] gamepads) { _lifetime.Check(); _gamepads.Set(mode, gamepads); }
    /// <summary>Controls platform mouse capture.</summary>
    public void SetMouseCaptureMode(ImplSDL3_MouseCaptureMode mode) { _lifetime.Check(); IGSharp_ImplSDL3_SetMouseCaptureMode((int)mode); }
    private void OnRawEvent(in RawEvent e)
    {
        if (!_lifetime.Alive) return;
        using var scope = _context.Enter(); IGSharp_ImplSDL3_ProcessEvent((SDL_Event*)e.Pointer);
    }
    /// <summary>Unhooks events and shuts down the platform backend in its owning context.</summary>
    public void Dispose() { if (!_lifetime.Alive) return; using var scope = _context.Enter(); _lifetime.Dispose(); }
}

internal sealed unsafe class PlatformGamepads : IDisposable
{
    private Gamepad[] _gamepads = [];
    internal void Set(ImplSDL3_GamepadMode mode, Gamepad[] gamepads)
    {
        ArgumentNullException.ThrowIfNull(gamepads);
        if (!Enum.IsDefined(mode)) throw new ArgumentOutOfRangeException(nameof(mode));
        if (mode != ImplSDL3_GamepadMode.Manual && gamepads.Length != 0) throw new ArgumentException("Automatic mode does not accept a manual gamepad list.", nameof(gamepads));
        var pointers = new nint[gamepads.Length];
        for (var i = 0; i < gamepads.Length; i++) { ArgumentNullException.ThrowIfNull(gamepads[i]); gamepads[i].ValidateDependentOwner(); pointers[i] = (nint)gamepads[i].Handle; }
        var retained = gamepads.ToArray(); foreach (var gamepad in retained) gamepad.RetainDependent();
        fixed (nint* pointer = pointers) IGSharp_ImplSDL3_SetGamepadMode((int)mode, (SDL_Gamepad**)pointer, pointers.Length);
        Dispose(); _gamepads = retained;
    }
    public void Dispose() { foreach (var gamepad in _gamepads) gamepad.ReleaseDependent(); _gamepads = []; }
}
