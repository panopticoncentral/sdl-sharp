using System.Linq;

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

            while (app.DispatchEvent())
            {
                renderer.DrawColor = Colors.White;
                renderer.Clear();

                renderer.DrawColor = Colors.Red;
                renderer.FillRectangle(((windowSize.Width / 4, windowSize.Height / 4), (windowSize.Width / 2, windowSize.Height / 2)));

                renderer.DrawColor = Colors.Green;
                renderer.DrawRectangle(((windowSize.Width / 6, windowSize.Height / 6), (windowSize.Width * 2 / 3, windowSize.Height * 2 / 3)));

                renderer.DrawColor = Colors.Blue;
                renderer.DrawLine((0, windowSize.Height / 2), (windowSize.Width, windowSize.Height / 2));

                renderer.DrawColor = Colors.Yellow;
                for (var i = 0; i < windowSize.Height; i += 4)
                {
                    renderer.DrawPoint((windowSize.Width / 2, i));
                }

                renderer.Present();
            }
        }
    }
}
