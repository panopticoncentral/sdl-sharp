using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Input;

using var app = new Application(InitFlags.Video);

var (window, renderer) = Renderer.CreateWindowAndRenderer("SdlSharp Renderer Sample", 800, 600);
using (window)
using (renderer)
{
    var running = true;
    var frame = 0;

    Application.Quit += _ => running = false;
    Application.KeyDown += e =>
    {
        if (e.Key == Keycode.Escape)
            running = false;
    };

    while (running)
    {
        Application.DispatchEvents();

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
        renderer.DrawDebugText(10, 10, $"Frame {frame++} — Press ESC to quit");

        renderer.Present();
        Thread.Sleep(16);
    }
}

Console.WriteLine("Done.");
