namespace SdlSharp.Input;

/// <summary>
/// The direction of a mouse wheel event.
/// </summary>
public enum MouseWheelDirection
{
    /// <summary>The scroll direction is normal.</summary>
    Normal = (int)Native.SDL_MouseWheelDirection.SDL_MOUSEWHEEL_NORMAL,
    /// <summary>The scroll direction is flipped ("natural" scrolling).</summary>
    Flipped = (int)Native.SDL_MouseWheelDirection.SDL_MOUSEWHEEL_FLIPPED,
}
