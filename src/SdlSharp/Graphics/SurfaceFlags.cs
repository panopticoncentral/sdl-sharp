namespace SdlSharp.Graphics;

/// <summary>
/// Flags describing a <see cref="Surface"/>'s internal state and memory layout.
/// </summary>
[Flags]
public enum SurfaceFlags : uint
{
    /// <summary>Surface uses preallocated pixel memory.</summary>
    Preallocated = (uint)Native.SDL_SurfaceFlags.SDL_SURFACE_PREALLOCATED,
    /// <summary>Surface needs to be locked to access pixels.</summary>
    LockNeeded = (uint)Native.SDL_SurfaceFlags.SDL_SURFACE_LOCK_NEEDED,
    /// <summary>Surface is currently locked.</summary>
    Locked = (uint)Native.SDL_SurfaceFlags.SDL_SURFACE_LOCKED,
    /// <summary>Surface uses pixel memory allocated with aligned allocation for SIMD access.</summary>
    SimdAligned = (uint)Native.SDL_SurfaceFlags.SDL_SURFACE_SIMD_ALIGNED,
}
