using static SdlSharp.Native.Metal;
namespace SdlSharp.Graphics;
/// <summary>An owned Metal view that retains its window until disposed on the creating thread.</summary>
public sealed unsafe class MetalView : IDisposable
{
    private nint _handle;
    private readonly NativeLifetime _lifetime;
    private readonly IDisposable _windowLease;
    private MetalView(Window window)
    {
        var handle = window.Handle;
        _windowLease = window.Lifetime.Retain();
        try
        {
            _handle = SDL_Metal_CreateView(handle);
            if (_handle == 0) throw new SdlException();
            _lifetime = NativeLifetime.Get(typeof(MetalView), _handle, true, threadAffine: true);
        }
        catch { _windowLease.Dispose(); throw; }
    }
    /// <summary>Creates a Metal view for a window created with Metal support.</summary>
    public static MetalView Create(Window window) => new(window);
    /// <summary>The borrowed CAMetalLayer, valid until this view is disposed.</summary>
    public nint Layer { get { _lifetime.Check(); return SDL_Metal_GetLayer(_handle); } }
    /// <summary>Destroys the view and releases the window.</summary>
    public void Dispose()
    {
        if (_handle == 0) return;
        _lifetime.Release(() => SDL_Metal_DestroyView(_handle));
        _handle = 0; _windowLease.Dispose();
    }
}
