using Gamepad = SdlSharp.Input.Gamepad;
using SdlSharp.Graphics;
using SdlSharp.Graphics.Gpu;
using SdlSharp.Input;
using SdlSharp.Native;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>Owns the Dear ImGui SDL3 platform and SDL_GPU renderer backends.</summary>
public sealed unsafe class ImGuiBackend : IDisposable
{
    private static readonly object Sync = new();
    private static ImGuiBackend? _active;
    private bool _disposed;
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2213", Justification = "Disposed by Shutdown through the context-owned lifetime.")]
    private readonly PlatformGamepads _gamepads = new();

    private readonly Context _context;
    private readonly Window _window;
    private readonly GpuDevice _device;
    private readonly Lifetime _lifetime;
    private ImGuiBackend(Context context, Window window, GpuDevice device)
    {
        _context = context; _window = window; _device = device;
        window.RetainDependent(); device.RetainDependent();
        _lifetime = new(0, context, context.Lifetime, Shutdown);
    }
    private void Shutdown()
    {
        Application.RawEventFilter -= OnRawEvent;
        IGSharp_ImplSDLGPU3_Shutdown(); IGSharp_ImplSDL3_Shutdown();
        _gamepads.Dispose();
        _window.ReleaseDependent(); _device.ReleaseDependent(); _disposed = true;
        if (ReferenceEquals(_active, this)) _active = null;
    }

    /// <summary>Initializes both backends. Dispose the returned object before its window, device, or context.</summary>
    public static ImGuiBackend Init(Window window, GpuDevice device, GpuTextureFormat colorTargetFormat,
        GpuSampleCount msaaSamples = GpuSampleCount.One)
    {
        ArgumentNullException.ThrowIfNull(window); ArgumentNullException.ThrowIfNull(device);
        var context = Context.Current; context.CheckCurrent();
        window.ValidateDependentOwner(); device.ValidateDependentOwner();
        if (IGSharp_GetIO()->BackendPlatformUserData != null || IGSharp_GetIO()->BackendRendererUserData != null) throw new InvalidOperationException("This context already has a backend.");
        lock (Sync)
        {
            if (_active is not null)
                throw new InvalidOperationException("An ImGui SDL/GPU backend is already initialized.");

            if (!IGSharp_ImplSDL3_InitForSDLGPU(window.Handle))
                throw new InvalidOperationException("Failed to initialize the Dear ImGui SDL3 platform backend.");

            if (!IGSharp_ImplSDLGPU3_Init(device.Handle, (int)colorTargetFormat, (int)msaaSamples))
            {
                IGSharp_ImplSDL3_Shutdown();
                throw new InvalidOperationException("Failed to initialize the Dear ImGui SDL_GPU renderer backend.");
            }

            var backend = new ImGuiBackend(context, window, device);
            Application.RawEventFilter += backend.OnRawEvent;
            _active = backend;
            return backend;
        }
    }

    /// <summary>Starts a new ImGui frame.</summary>
    public void NewFrame()
    {
        ThrowIfDisposed();
        IGSharp_ImplSDLGPU3_NewFrame();
        IGSharp_ImplSDL3_NewFrame();
        _context.NewFrame();
    }

    /// <summary>Uploads vertex/index buffers before beginning the render pass.</summary>
    public void PrepareDrawData(DrawData drawData, GpuCommandBuffer commandBuffer)
    {
        ThrowIfDisposed();
        ValidatePreparation(_device, commandBuffer);
        RetainSubmission(drawData, commandBuffer);
        using var retention = TextureRetention.DeferDestruction();
        IGSharp_ImplSDLGPU3_PrepareDrawData(drawData.Handle, commandBuffer.Handle);
    }

    /// <summary>Renders prepared draw data inside the active render pass.</summary>
    public void RenderDrawData(DrawData drawData, GpuCommandBuffer commandBuffer, GpuRenderPass renderPass)
    {
        RenderDrawData(drawData, commandBuffer, renderPass, null);
    }

    /// <summary>Renders prepared draw data with an optional custom pipeline.</summary>
    public void RenderDrawData(DrawData drawData, GpuCommandBuffer commandBuffer,
        GpuRenderPass renderPass, GpuGraphicsPipeline? pipeline)
    {
        ThrowIfDisposed();
        ValidateRendering(_device, commandBuffer, renderPass, pipeline);
        RetainSubmission(drawData, commandBuffer);
        using var retention = TextureRetention.DeferDestruction();
        IGSharp_ImplSDLGPU3_RenderDrawData(drawData.Handle, commandBuffer.Handle, renderPass.Handle, pipeline is null ? null : pipeline.Handle);
    }

    internal static void ValidatePreparation(GpuDevice device, GpuCommandBuffer command)
    {
        ArgumentNullException.ThrowIfNull(command);
        command.CheckDevice(device); command.CheckIdle();
    }
    internal static void ValidateRendering(GpuDevice device, GpuCommandBuffer command, GpuRenderPass pass, GpuGraphicsPipeline? pipeline)
    {
        ArgumentNullException.ThrowIfNull(command); ArgumentNullException.ThrowIfNull(pass);
        command.CheckDevice(device); pass.CheckCommand(command);
        if (pipeline != null) _ = command.Use(pipeline);
    }
    internal static void RetainTextures(DrawData data, GpuCommandBuffer command)
    {
        for (var i = 0; i < data.CmdListsCount; i++)
            foreach (var texture in data.GetCmdList(i).Lifetime.GpuTextures.Values)
            {
                texture.Check(); command.RetainResource(texture.Lifetime, texture.Device);
            }
    }
    private void RetainSubmission(DrawData data, GpuCommandBuffer command)
    {
        RetainTextures(data, command);
        command.RetainDependency(_lifetime, _lifetime.Retain);
    }

    /// <summary>Shuts down both backends and unhooks the event filter.</summary>
    public void Dispose()
    {
        lock (Sync)
        {
            if (_disposed) return;
            using var scope = _context.Enter();
            _lifetime.Dispose();
        }
    }

    private void OnRawEvent(in RawEvent e)
    {
        if (_disposed) return;
        using var scope = _context.Enter();
        IGSharp_ImplSDL3_ProcessEvent((SDL_Event*)e.Pointer);
    }

    private void ThrowIfDisposed() { ObjectDisposedException.ThrowIf(_disposed, this); _context.CheckCurrent(); _ = _window.Handle; _ = _device.Handle; }
    /// <summary>Creates renderer device objects after device recreation.</summary>
    public void CreateDeviceObjects() { ThrowIfDisposed(); IGSharp_ImplSDLGPU3_CreateDeviceObjects(); }
    /// <summary>Releases renderer device objects before device recreation.</summary>
    public void DestroyDeviceObjects() { ThrowIfDisposed(); _lifetime.EnsureUnretained(); IGSharp_ImplSDLGPU3_DestroyDeviceObjects(); }
    /// <summary>Processes a pending texture upload or destruction.</summary>
    public void UpdateTexture(TextureData texture) { ThrowIfDisposed(); if (texture.Status == TextureStatus.WantDestroy && texture.IsRetainedForRendering) return; IGSharp_ImplSDLGPU3_UpdateTexture(texture.Handle); }
    /// <summary>Controls platform mouse capture.</summary>
    public void SetMouseCaptureMode(ImplSDL3_MouseCaptureMode mode) { ThrowIfDisposed(); IGSharp_ImplSDL3_SetMouseCaptureMode((int)mode); }
    /// <summary>Selects automatic or manually supplied gamepads, retaining manual owners until replacement or shutdown.</summary>
    public void SetGamepadMode(ImplSDL3_GamepadMode mode, params Gamepad[] gamepads) { ThrowIfDisposed(); _gamepads.Set(mode, gamepads); }
    internal static IDisposable? RetainCurrent(Lifetime owner) => _active is { _disposed: false } backend && backend._context.IsCurrent ? backend._lifetime.RetainFor(owner) : null;
    /// <summary>The SDL GPU device retained by this backend.</summary>
    public GpuDevice Device { get { ThrowIfDisposed(); return _device; } }
}
