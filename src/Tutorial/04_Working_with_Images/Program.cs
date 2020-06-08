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
            using var window = Window.Create("Working with Images", windowRectangle, WindowFlags.Shown);

            using var sunflowers = Image.Load("Sunflowers.jpg", window.Surface);

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
                if (stretch)
                {
                    sunflowers.BlitScaled(window.Surface, null, (Point.Origin, windowSize));
                }
                else
                {
                    sunflowers.Blit(window.Surface);
                }

                window.UpdateSurface();
            }
        }
    }
}
