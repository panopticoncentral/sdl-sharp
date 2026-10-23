using SdlSharp.Graphics.Gpu;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Render;
namespace SdlSharp.Graphics;

/// <summary>Custom fragment shading for the SDL GPU renderer. Dispose before its renderer and all referenced GPU resources.</summary>
public sealed unsafe class GpuRenderState : IDisposable
{
    private Native.SDL_GPURenderState* _handle;
    internal readonly Renderer Renderer;
    private readonly NativeLifetime _lifetime;
    private readonly List<IDisposable> _leases;
    internal Native.SDL_GPURenderState* Handle { get { _lifetime.Check(); return _handle; } }
    private GpuRenderState(Renderer renderer, Native.SDL_GPURenderState* handle, List<IDisposable> leases)
    {
        Renderer = new Renderer(renderer.Handle, ownsHandle: false); _handle = handle; _leases = leases;
        _lifetime = NativeLifetime.Get(typeof(GpuRenderState), (nint)handle, true, threadAffine: true);
    }
    /// <summary>Creates custom render state and retains the shader and additional resources until disposal.</summary>
    public static GpuRenderState Create(Renderer renderer, GpuShader fragmentShader, ReadOnlySpan<GpuTextureSamplerBinding> samplers = default,
        ReadOnlySpan<GpuTexture> storageTextures = default, ReadOnlySpan<GpuBuffer> storageBuffers = default, PropertyGroup? properties = null)
    {
        using var device = renderer.GetGpuDevice();
        var leases = new List<IDisposable>();
        void Retain(NativeLifetime lifetime, GpuDevice resourceDevice)
        {
            if (!ReferenceEquals(device.Lifetime, resourceDevice.Lifetime)) throw new ArgumentException("All render-state resources must belong to the renderer's GPU device.");
            leases.Add(lifetime.Retain());
        }
        try
        {
            leases.Add(renderer.Lifetime.Retain());
            Retain(fragmentShader.Lifetime, fragmentShader.Device);
            var bindings = new Native.SDL_GPUTextureSamplerBinding[samplers.Length];
            for (var i = 0; i < samplers.Length; i++)
            {
                Retain(samplers[i].Texture.Lifetime, samplers[i].Texture.Device);
                Retain(samplers[i].Sampler.Lifetime, samplers[i].Sampler.Device);
                if (samplers[i].Texture.OwnerCommand != null) throw new ArgumentException("Swapchain textures cannot be retained by render state.");
                bindings[i] = new() { texture = samplers[i].Texture.Handle, sampler = samplers[i].Sampler.Handle };
            }
            var textures = new nint[storageTextures.Length];
            for (var i = 0; i < textures.Length; i++)
            {
                Retain(storageTextures[i].Lifetime, storageTextures[i].Device);
                if (storageTextures[i].OwnerCommand != null) throw new ArgumentException("Swapchain textures cannot be retained by render state.");
                textures[i] = (nint)storageTextures[i].Handle;
            }
            var buffers = new nint[storageBuffers.Length];
            for (var i = 0; i < buffers.Length; i++) { Retain(storageBuffers[i].Lifetime, storageBuffers[i].Device); buffers[i] = (nint)storageBuffers[i].Handle; }
            fixed (Native.SDL_GPUTextureSamplerBinding* b = bindings)
            fixed (nint* t = textures)
            fixed (nint* p = buffers)
            {
                var info = new Native.SDL_GPURenderStateCreateInfo
                {
                    fragment_shader = fragmentShader.Handle, num_sampler_bindings = bindings.Length, sampler_bindings = b,
                    num_storage_textures = textures.Length, storage_textures = (Native.SDL_GPUTexture**)t,
                    num_storage_buffers = buffers.Length, storage_buffers = (Native.SDL_GPUBuffer**)p, props = properties?.Id ?? default
                };
                return new(renderer, Check(SDL_CreateGPURenderState(renderer.Handle, &info)), leases);
            }
        }
        catch { foreach (var lease in leases) lease.Dispose(); throw; }
    }
    /// <summary>Copies fragment uniform data for a slot.</summary>
    public void SetFragmentUniforms(uint slot, ReadOnlySpan<byte> data)
    {
        fixed (byte* p = data) Check(SDL_SetGPURenderStateFragmentUniforms(Handle, slot, p, (uint)data.Length));
    }
    /// <summary>Flushes queued draws, clears this state if active, and releases it and its dependencies.</summary>
    public void Dispose()
    {
        if (_handle == null) return;
        _lifetime.EnsureCanDestroy();
        Renderer.Flush();
        if (ReferenceEquals(Renderer.Lifetime.Attachment, this)) Renderer.SetGpuRenderState(null);
        _lifetime.Release(() => SDL_DestroyGPURenderState(_handle));
        _handle = null;
        foreach (var lease in _leases) lease.Dispose();
        _leases.Clear();
        Renderer.Dispose();
    }
}
