using SdlSharp;
using SdlSharp.Graphics;

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

            while (app.DispatchEvent())
            {
            }
        }
    }
}
