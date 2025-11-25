namespace Sdl3Sharp;

/// <summary>
/// Represents a button in a message box.
/// </summary>
/// <param name="Text">The text displayed on the button.</param>
/// <param name="ButtonId">The ID returned when this button is clicked.</param>
/// <param name="Flags">Flags controlling the button's behavior.</param>
public readonly record struct MessageBoxButton(string Text, int ButtonId, MessageBoxButtonFlags Flags = MessageBoxButtonFlags.None);
