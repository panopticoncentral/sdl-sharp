namespace SdlSharp.Graphics;

/// <summary>
/// How the logical size is mapped to the output.
/// </summary>
public enum LogicalPresentation
{
    /// <summary>There is no logical size in effect.</summary>
    Disabled = (int)Native.SDL_RendererLogicalPresentation.SDL_LOGICAL_PRESENTATION_DISABLED,
    /// <summary>The rendered content is stretched to the output resolution.</summary>
    Stretch = (int)Native.SDL_RendererLogicalPresentation.SDL_LOGICAL_PRESENTATION_STRETCH,
    /// <summary>The rendered content is fit to the largest dimension and letterboxed.</summary>
    Letterbox = (int)Native.SDL_RendererLogicalPresentation.SDL_LOGICAL_PRESENTATION_LETTERBOX,
    /// <summary>The rendered content is fit to the smallest dimension and extends beyond bounds.</summary>
    Overscan = (int)Native.SDL_RendererLogicalPresentation.SDL_LOGICAL_PRESENTATION_OVERSCAN,
    /// <summary>The rendered content is scaled up by integer multiples.</summary>
    IntegerScale = (int)Native.SDL_RendererLogicalPresentation.SDL_LOGICAL_PRESENTATION_INTEGER_SCALE,
}
