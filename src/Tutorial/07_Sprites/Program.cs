using System;

using SdlSharp;
using SdlSharp.Graphics;

namespace Tutorial
{
    internal class Program
    {
        private static void Main()
        {
            using var app = new Application(Subsystems.Video, ImageFormats.Png);
            Size windowSize = (640, 480);
            Rectangle windowRectangle = (Window.UndefinedWindowLocation, windowSize);
            using var window = Window.Create("Sprites", windowRectangle, WindowFlags.Shown);
            using var renderer = Renderer.Create(window, -1, RendererFlags.Accelerated | RendererFlags.PresentVSync);

            using var floor = Image.Load("Floor.png", renderer);
            using var player0 = Image.Load("Player0.png", renderer);
            using var player1 = Image.Load("Player1.png", renderer);
            Size spriteSize = (16, 16);

            Point upperLeftFloor = (7, 15);
            Point upperRightFloor = (9, 15);
            Point lowerLeftFloor = (7, 17);
            Point lowerRightFloor = (9, 17);

            var last = Timer.Ticks;
            var current = 0;
            while (app.DispatchEvent())
            {
                if (Timer.Ticks - last > 500)
                {
                    last = Timer.Ticks;
                    current = (current + 1) % 2;
                }

                renderer.DrawColor = Colors.White;
                renderer.Clear();

                renderer.Copy(floor, ((upperLeftFloor.X * 16, upperLeftFloor.Y * 16), spriteSize), (Point.Origin, spriteSize * 4));
                renderer.Copy(floor, ((upperRightFloor.X * 16, upperRightFloor.Y * 16), spriteSize), ((spriteSize.Width * 4, 0), spriteSize * 4));
                renderer.Copy(floor, ((lowerLeftFloor.X * 16, lowerLeftFloor.Y * 16), spriteSize), ((0, spriteSize.Height * 4), spriteSize * 4));
                renderer.Copy(floor, ((lowerRightFloor.X * 16, lowerRightFloor.Y * 16), spriteSize), ((spriteSize.Width * 4, spriteSize.Height * 4), spriteSize * 4));

                var currentPlayer = current == 0 ? player0 : player1;
                renderer.Copy(currentPlayer, (Point.Origin, spriteSize), (Point.Origin, spriteSize * 4));

                currentPlayer.ColorMod = (0x8F, 0x8F, 0x8F);
                renderer.Copy(currentPlayer, (Point.Origin, spriteSize), ((spriteSize.Width * 4, 0), spriteSize * 4));
                currentPlayer.ColorMod = (0xFF, 0xFF, 0xFF);

                currentPlayer.AlphaMod = 0x3F;
                renderer.Copy(currentPlayer, (Point.Origin, spriteSize), ((0, spriteSize.Height * 4), spriteSize * 4));
                currentPlayer.AlphaMod = 0xFF;

                renderer.Copy(currentPlayer, (Point.Origin, spriteSize), ((spriteSize.Width * 4, spriteSize.Height * 4), spriteSize * 4), 90, null, RendererFlip.None);

                renderer.Present();
            }
        }
    }
}
