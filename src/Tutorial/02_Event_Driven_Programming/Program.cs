using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Input;

using Application app = new (Subsystems.Video);
Size windowSize = (640, 480);
Rectangle windowRectangle = (Window.UndefinedWindowLocation, windowSize);
using var window = Window.Create("Event Driven Programming", windowRectangle, WindowFlags.Shown);

Keyboard.KeyDown += (s, e) => Application.ShowMessageBox(MessageBoxFlags.Information, "Key Press", e.Keycode switch
{
    Keycode.Up => "Up!",
    Keycode.Down => "Down!",
    Keycode.Left => "Left!",
    Keycode.Right => "Right!",
    _ => "Other!"
}, window);

Mouse.ButtonDown += (s, e) => Application.ShowMessageBox(MessageBoxFlags.Information, "Mouse Button", "Down!", window);
Mouse.ButtonUp += (s, e) => Application.ShowMessageBox(MessageBoxFlags.Information, "Mouse Button", "Up!", window);

while (app.DispatchEvent())
{
}
