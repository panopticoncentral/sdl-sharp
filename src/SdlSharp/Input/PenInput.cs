// src/SdlSharp/Input/PenInput.cs
namespace SdlSharp.Input;

/// <summary>
/// Pen input flags.
/// </summary>
[Flags]
public enum PenInput : uint
{
    Down = Native.Pen.SDL_PEN_INPUT_DOWN,
    Button1 = Native.Pen.SDL_PEN_INPUT_BUTTON_1,
    Button2 = Native.Pen.SDL_PEN_INPUT_BUTTON_2,
    Button3 = Native.Pen.SDL_PEN_INPUT_BUTTON_3,
    Button4 = Native.Pen.SDL_PEN_INPUT_BUTTON_4,
    Button5 = Native.Pen.SDL_PEN_INPUT_BUTTON_5,
    EraserTip = Native.Pen.SDL_PEN_INPUT_ERASER_TIP,
    InProximity = Native.Pen.SDL_PEN_INPUT_IN_PROXIMITY,
}
