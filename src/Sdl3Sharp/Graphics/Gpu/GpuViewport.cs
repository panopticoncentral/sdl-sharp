using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Describes a viewport for rendering.
/// </summary>
public struct GpuViewport
{
    /// <summary>
    /// The left offset of the viewport.
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// The top offset of the viewport.
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// The width of the viewport.
    /// </summary>
    public float Width { get; set; }

    /// <summary>
    /// The height of the viewport.
    /// </summary>
    public float Height { get; set; }

    /// <summary>
    /// The minimum depth of the viewport.
    /// </summary>
    public float MinDepth { get; set; }

    /// <summary>
    /// The maximum depth of the viewport.
    /// </summary>
    public float MaxDepth { get; set; }

    internal SDL_GPUViewport ToNative()
    {
        return new SDL_GPUViewport
        {
            x = X,
            y = Y,
            w = Width,
            h = Height,
            min_depth = MinDepth,
            max_depth = MaxDepth
        };
    }
}
