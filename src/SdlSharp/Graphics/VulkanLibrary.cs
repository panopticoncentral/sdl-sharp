using System.Runtime.InteropServices;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Vulkan;
namespace SdlSharp.Graphics;
/// <summary>A reference to SDL's Vulkan loader. Dispose surfaces before the loader. Caller-owned Vulkan instances must outlive their surfaces.</summary>
public sealed unsafe class VulkanLibrary : IDisposable
{
    private readonly NativeLifetime _lifetime;
    private bool _disposed;
    private VulkanLibrary(string? path)
    {
        Check(SDL_Vulkan_LoadLibrary(ToUtf8(path)));
        _lifetime = NativeLifetime.Get(typeof(VulkanLibrary), 1, true, threadAffine: true);
    }
    /// <summary>Loads the Vulkan library, optionally from an explicit path.</summary>
    public static VulkanLibrary Load(string? path = null) => new(path);
    private void CheckAlive() { ObjectDisposedException.ThrowIf(_disposed, this); _lifetime.Check(); }
    /// <summary>The vkGetInstanceProcAddr function address, valid while this loader remains alive.</summary>
    public nint GetInstanceProcAddress { get { CheckAlive(); return SDL_Vulkan_GetVkGetInstanceProcAddr(); } }
    /// <summary>Gets copies of the required Vulkan instance extension names.</summary>
    public string[] GetInstanceExtensions()
    {
        CheckAlive(); uint count;
        var extensions = SDL_Vulkan_GetInstanceExtensions(&count);
        if (extensions == null) throw new SdlException();
        var result = new string[checked((int)count)];
        for (var i = 0; i < result.Length; i++) result[i] = Marshal.PtrToStringUTF8((nint)extensions[i])!;
        return result;
    }
    /// <summary>Checks presentation support for a caller-owned instance and physical device.</summary>
    public bool GetPresentationSupport(nint instance, nint physicalDevice, uint queueFamilyIndex)
    {
        CheckAlive(); return SDL_Vulkan_GetPresentationSupport(instance, physicalDevice, queueFamilyIndex);
    }
    /// <summary>Creates a surface with the default Vulkan allocator and retains this loader and the window.</summary>
    public VulkanSurface CreateSurface(Window window, nint instance)
    {
        CheckAlive();
        return new VulkanSurface(this, _lifetime, window, instance);
    }
    /// <summary>Releases this loader reference.</summary>
    public void Dispose()
    {
        if (_disposed) return;
        _lifetime.Release(() => SDL_Vulkan_UnloadLibrary()); _disposed = true;
    }
}
/// <summary>An owned Vulkan surface. Keep the caller-owned instance alive until this object is disposed.</summary>
public sealed unsafe class VulkanSurface : IDisposable
{
    private ulong _handle;
    private readonly nint _instance;
    private readonly SdlScope _release;
    private readonly int _thread = Environment.CurrentManagedThreadId;
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "The lease is transferred to the returned scope or lifetime cleanup, which releases it exactly once.")]
    internal VulkanSurface(VulkanLibrary library, NativeLifetime lifetime, Window window, nint instance)
    {
        _ = library; var nativeWindow = window.Handle;
        if (instance == 0) throw new ArgumentException("A Vulkan instance is required.", nameof(instance));
        var loaderLease = lifetime.Retain();
        var windowLease = window.Lifetime.Retain();
        try
        {
            ulong surface;
            Check(SDL_Vulkan_CreateSurface(nativeWindow, instance, 0, &surface));
            _handle = surface; _instance = instance;
            _release = new SdlScope(() => { SDL_Vulkan_DestroySurface(_instance, _handle, 0); _handle = 0; windowLease.Dispose(); loaderLease.Dispose(); });
        }
        catch { windowLease.Dispose(); loaderLease.Dispose(); throw; }
    }
    /// <summary>The Vulkan surface handle, valid until disposal.</summary>
    public ulong Handle
    {
        get { ObjectDisposedException.ThrowIf(_handle == 0, this); if (_thread != Environment.CurrentManagedThreadId) throw new InvalidOperationException("Use this surface on its creating thread."); return _handle; }
    }
    /// <summary>Destroys the surface and releases its dependencies.</summary>
    public void Dispose() => _release.Dispose();
}
