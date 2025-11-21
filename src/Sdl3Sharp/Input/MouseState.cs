using Sdl3Sharp.Graphics;

namespace Sdl3Sharp.Input;

/// <summary>
/// Represents the current state of the mouse.
/// </summary>
/// <param name="Position">The position of the mouse cursor.</param>
/// <param name="Buttons">The currently pressed mouse buttons.</param>
public readonly record struct MouseState(PointF Position, MousePressedButtons Buttons)
{
    /// <summary>
    /// Gets whether the left mouse button is pressed.
    /// </summary>
    public bool IsLeftButtonPressed => Buttons.HasFlag(MousePressedButtons.Left);

    /// <summary>
    /// Gets whether the middle mouse button is pressed.
    /// </summary>
    public bool IsMiddleButtonPressed => Buttons.HasFlag(MousePressedButtons.Middle);

    /// <summary>
    /// Gets whether the right mouse button is pressed.
    /// </summary>
    public bool IsRightButtonPressed => Buttons.HasFlag(MousePressedButtons.Right);

    /// <summary>
    /// Gets whether a specific button is pressed.
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns>True if the button is pressed.</returns>
    public bool IsButtonPressed(MouseButton button)
    {
        var mask = (MousePressedButtons)(1u << ((int)button - 1));
        return Buttons.HasFlag(mask);
    }
}
