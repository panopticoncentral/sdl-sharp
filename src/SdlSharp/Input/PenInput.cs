namespace SdlSharp.Input;

/// <summary>
/// Pen input flags.
/// </summary>
[Flags]
public enum PenInput : uint
{
    /// <summary>Pen tip is touching the surface.</summary>
    Down = Native.Pen.SDL_PEN_INPUT_DOWN,
    /// <summary>Pen barrel button 1 is pressed.</summary>
    Button1 = Native.Pen.SDL_PEN_INPUT_BUTTON_1,
    /// <summary>Pen barrel button 2 is pressed.</summary>
    Button2 = Native.Pen.SDL_PEN_INPUT_BUTTON_2,
    /// <summary>Pen barrel button 3 is pressed.</summary>
    Button3 = Native.Pen.SDL_PEN_INPUT_BUTTON_3,
    /// <summary>Pen barrel button 4 is pressed.</summary>
    Button4 = Native.Pen.SDL_PEN_INPUT_BUTTON_4,
    /// <summary>Pen barrel button 5 is pressed.</summary>
    Button5 = Native.Pen.SDL_PEN_INPUT_BUTTON_5,
    /// <summary>Eraser tip is touching the surface.</summary>
    EraserTip = Native.Pen.SDL_PEN_INPUT_ERASER_TIP,
    /// <summary>Pen is within detection range of the tablet.</summary>
    InProximity = Native.Pen.SDL_PEN_INPUT_IN_PROXIMITY,
}
