using Sdl3Sharp.Graphics;
using static Sdl3Sharp.Native.Gpu;
using static Sdl3Sharp.Native.Surface;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Describes parameters for a blit command.
/// </summary>
public struct GpuBlitInfo
{
    /// <summary>
    /// The source region for the blit.
    /// </summary>
    public GpuBlitRegion Source { get; set; }

    /// <summary>
    /// The destination region for the blit.
    /// </summary>
    public GpuBlitRegion Destination { get; set; }

    /// <summary>
    /// What is done with the contents of the destination before the blit.
    /// </summary>
    public GpuLoadOp LoadOp { get; set; }

    /// <summary>
    /// The color to clear the destination region to before the blit.
    /// </summary>
    public ColorF ClearColor { get; set; }

    /// <summary>
    /// The flip mode for the source region.
    /// </summary>
    public FlipMode FlipMode { get; set; }

    /// <summary>
    /// The filter mode used when blitting.
    /// </summary>
    public GpuFilter Filter { get; set; }

    /// <summary>
    /// True cycles the destination texture if it is already bound.
    /// </summary>
    public bool Cycle { get; set; }

    internal unsafe SDL_GPUBlitInfo ToNative()
    {
        return new SDL_GPUBlitInfo
        {
            source = Source.ToNative(),
            destination = Destination.ToNative(),
            load_op = (SDL_GPULoadOp)LoadOp,
            clear_color = ClearColor.ToNative(),
            flip_mode = (SDL_FlipMode)FlipMode,
            filter = (SDL_GPUFilter)Filter,
            cycle = Cycle
        };
    }
}