using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU texture (SDL_GPUTexture).
/// GPU textures hold image data on the GPU for sampling, rendering, or storage.
/// </summary>
public sealed unsafe class GpuTexture : IDisposable
{
    private readonly GpuDevice _device;
    private readonly Native.SDL_GPUDevice* _devicePointer;
    private Native.SDL_GPUDevice* DeviceHandle { get { _device.Lifetime.Check(); return _devicePointer; } }
    internal GpuDevice Device => _device;
    internal GpuCommandBuffer? OwnerCommand { get; set; }
    private readonly bool _ownsHandle;
    internal readonly NativeLifetime Lifetime;
    private readonly NativeLifetime? _viewParent;

    /// <summary>
    /// The underlying native SDL_GPUTexture pointer.
    /// </summary>
    internal SDL_GPUTexture* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            Lifetime.Check();
            _viewParent?.Check();
            return _handle;
        }
    }

    private SDL_GPUTexture* _handle;

    /// <summary>
    /// Native handle value (pointer to the underlying SDL_GPUTexture).
    /// Use this when passing the texture to a backend-aware API (for example ImGui image primitives,
    /// which accept the SDL_GPU texture pointer as an <see cref="ulong"/> <c>ImTextureID</c>).
    /// </summary>
    public nuint NativeHandle => (nuint)Handle;

    internal GpuTexture(GpuDevice device, SDL_GPUTexture* handle, bool ownsHandle = true, NativeLifetime? parent = null)
    {
        _device = new GpuDevice(device.Handle, ownsHandle: false);
        _devicePointer = device.Handle;
        _handle = handle;
        Lifetime = NativeLifetime.Get(typeof(GpuTexture), (nint)handle, ownsHandle, threadAffine: false, parent: parent ?? device.Lifetime);
        _ownsHandle = ownsHandle;
        _viewParent = ownsHandle ? null : parent;
    }

    /// <summary>
    /// Sets a debug name for this texture, visible in GPU debugging tools.
    /// </summary>
    /// <param name="name">The debug name.</param>
    public void SetName(string name) => SDL_SetGPUTextureName(DeviceHandle, Handle, ToUtf8(name));

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && _handle != null && _device.Lifetime.IsAlive)
        {
            Lifetime.Release(() =>
            {
                SDL_ReleaseGPUTexture(DeviceHandle, _handle);
            });
        }
        _handle = null;
        _device.Dispose();
    }
}
