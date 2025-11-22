using static Sdl3Sharp.Native.Pen;

namespace Sdl3Sharp.Input;

/// <summary>
/// Pen input flags, as reported by various pen events' pen_state field.
/// </summary>
[Flags]
public enum PenInputFlags : uint
{
    /// <summary>No pen input flags set.</summary>
    None = SDL_PenInputFlags.None,
    /// <summary>Pen is pressed down.</summary>
    Down = SDL_PenInputFlags.SDL_PEN_INPUT_DOWN,
    /// <summary>Button 1 is pressed.</summary>
    Button1 = SDL_PenInputFlags.SDL_PEN_INPUT_BUTTON_1,
    /// <summary>Button 2 is pressed.</summary>
    Button2 = SDL_PenInputFlags.SDL_PEN_INPUT_BUTTON_2,
    /// <summary>Button 3 is pressed.</summary>
    Button3 = SDL_PenInputFlags.SDL_PEN_INPUT_BUTTON_3,
    /// <summary>Button 4 is pressed.</summary>
    Button4 = SDL_PenInputFlags.SDL_PEN_INPUT_BUTTON_4,
    /// <summary>Button 5 is pressed.</summary>
    Button5 = SDL_PenInputFlags.SDL_PEN_INPUT_BUTTON_5,
    /// <summary>Eraser tip is used.</summary>
    EraserTip = SDL_PenInputFlags.SDL_PEN_INPUT_ERASER_TIP
}
