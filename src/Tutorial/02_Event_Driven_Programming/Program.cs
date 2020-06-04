using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Input;

namespace Tutorial
{
    internal class Program
    {
        private static void Main()
        {
            using var app = new Application(Subsystems.Video);
            Size windowSize = (640, 480);
            Rectangle windowRectangle = (Window.UndefinedWindowLocation, windowSize);
            using var window = Window.Create("Hello, world!", windowRectangle, WindowFlags.Shown);

            Keyboard.KeyDown += (s, e) =>
            {
                switch (e.Keycode)
                {
                    case Keycode.Up:
                        app.ShowMessageBox(MessageBoxFlags.Information, "Key Press", "Up!", window);
                        break;
                    case Keycode.Down:
                        app.ShowMessageBox(MessageBoxFlags.Information, "Key Press", "Down!", window);
                        break;
                    case Keycode.Left:
                        app.ShowMessageBox(MessageBoxFlags.Information, "Key Press", "Left!", window);
                        break;
                    case Keycode.Right:
                        app.ShowMessageBox(MessageBoxFlags.Information, "Key Press", "Right!", window);
                        break;
                }
            };

            Mouse.ButtonDown += (s, e) => app.ShowMessageBox(MessageBoxFlags.Information, "Mouse Button", "Down!", window);
            Mouse.ButtonUp += (s, e) => app.ShowMessageBox(MessageBoxFlags.Information, "Mouse Button", "Up!", window);

            while (app.DispatchEvent())
            {
            }
        }
    }
}
