using System;
using SdlSharp;
using SdlSharp.Graphics;

namespace _01_Hello_World
{
    class Program
    {
        static void Main(string[] args)
        {
            using var app = new Application(Subsystems.Video);
            Size windowSize = (640, 480);
            Rectangle windowRectangle = (Window.UndefinedWindowLocation, windowSize);
            using var window = Window.Create("Hello, world!", windowRectangle, WindowFlags.Shown);

            while (app.DispatchEvent())
            {
                window.UpdateSurface();
            }
        }
    }
}
