using SdlSharp;
using SdlSharp.Graphics;

using var app = new Application(InitFlags.Video);

var (window, renderer) = Renderer.CreateWindowAndRenderer("SdlSharp Renderer Sample", 800, 600);
using (window)
using (renderer)
{
    for (var i = 0; i < 300; i++)
    {
        Application.PumpEvents();

        renderer.DrawColor = new Color(40, 40, 60);
        renderer.Clear();

        // Draw a filled rectangle
        renderer.DrawColor = Color.Red;
        renderer.FillRect(new FRectangle(100, 100, 200, 150));

        // Draw an outline rectangle
        renderer.DrawColor = Color.Green;
        renderer.DrawRect(new FRectangle(350, 100, 200, 150));

        // Draw a line
        renderer.DrawColor = Color.Blue;
        renderer.DrawLine(100, 350, 700, 450);

        // Draw debug text
        renderer.DrawColor = Color.White;
        renderer.DrawDebugText(10, 10, $"Frame {i}");

        renderer.Present();
        Thread.Sleep(16);
    }
}

Console.WriteLine("Done.");
