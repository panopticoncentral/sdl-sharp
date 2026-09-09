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

    private ImGuiBackend() { }

    /// <summary>Initializes both backends. Dispose the returned object before its window, device, or context.</summary>
    public static ImGuiBackend Init(Window window, GpuDevice device, GpuTextureFormat colorTargetFormat,
        GpuSampleCount msaaSamples = GpuSampleCount.One)
    {
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

            var backend = new ImGuiBackend();
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
        IGSharp_NewFrame();
    }

    /// <summary>Uploads vertex/index buffers before beginning the render pass.</summary>
    public void PrepareDrawData(DrawData drawData, GpuCommandBuffer commandBuffer)
    {
        ThrowIfDisposed();
        IGSharp_ImplSDLGPU3_PrepareDrawData(drawData.Handle, commandBuffer.Handle);
    }

    /// <summary>Renders prepared draw data inside the active render pass.</summary>
    public void RenderDrawData(DrawData drawData, GpuCommandBuffer commandBuffer, GpuRenderPass renderPass)
    {
        ThrowIfDisposed();
        IGSharp_ImplSDLGPU3_RenderDrawData(drawData.Handle, commandBuffer.Handle, renderPass.Handle);
    }

    /// <summary>Renders prepared draw data with an optional custom pipeline.</summary>
    public void RenderDrawData(DrawData drawData, GpuCommandBuffer commandBuffer,
        GpuRenderPass renderPass, GpuGraphicsPipeline? pipeline)
    {
        ThrowIfDisposed();
        IGSharp_ImplSDLGPU3_RenderDrawDataWithPipeline(drawData.Handle, commandBuffer.Handle,
            renderPass.Handle, pipeline is null ? null : pipeline.Handle);
    }

    /// <summary>Shuts down both backends and unhooks the event filter.</summary>
    public void Dispose()
    {
        lock (Sync)
        {
            if (_disposed) return;
            Application.RawEventFilter -= OnRawEvent;
            IGSharp_ImplSDLGPU3_Shutdown();
            IGSharp_ImplSDL3_Shutdown();
            _disposed = true;
            if (ReferenceEquals(_active, this))
                _active = null;
        }
    }

    private void OnRawEvent(in RawEvent e)
    {
        if (!_disposed)
            IGSharp_ImplSDL3_ProcessEvent((SDL_Event*)e.Pointer);
    }

    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(_disposed, this);
}
