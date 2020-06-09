using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Input;

namespace Tutorial
{
    internal class Program
    {
        private static void Main()
        {
            using var app = new Application(Subsystems.Video, ImageFormats.Jpg);
            Size windowSize = (640, 480);
            Rectangle windowRectangle = (Window.UndefinedWindowLocation, windowSize);
            using var window = Window.Create("Drawing", windowRectangle, WindowFlags.Shown);
            using var renderer = Renderer.Create(window, -1, RendererFlags.Accelerated);

            var stretch = false;

            Keyboard.KeyDown += (s, e) =>
            {
                switch (e.Keycode)
                {
                    case Keycode.s:
                        stretch = !stretch;
                        break;
                }
            };

            while (app.DispatchEvent())
            {
                renderer.DrawColor = Colors.White;
                renderer.Clear();
                renderer.Present();
            }
        }
    }
}
