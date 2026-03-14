using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Input;

using var app = new Application(InitFlags.Video);

const int width = 800;
const int height = 600;

var (window, renderer) = Renderer.CreateWindowAndRenderer("SdlSharp Paint", width, height);
using (window)
using (renderer)
{
    // Use a texture as a persistent canvas
    using var canvas = renderer.CreateTexture(PixelFormat.Rgba8888, TextureAccess.Target, width, height);

    // Clear canvas to white
    renderer.Target = canvas;
    renderer.DrawColor = Color.White;
    renderer.Clear();
    renderer.Target = null;

    var running = true;
    var drawing = false;
    float lastX = 0, lastY = 0;
    var brushColor = Color.Black;
    var brushSize = 3;

    Color[] palette =
    [
        Color.Black,
        Color.Red,
        new(255, 100, 0),
        new Color(255, 255, 0),
        Color.Green,
        new(0, 150, 255),
        Color.Blue,
        new(150, 0, 255),
        Color.White,
    ];
    var selectedColor = 0;

    Application.Quit += _ => running = false;

    Application.KeyDown += e =>
    {
        switch (e.Key)
        {
            case Keycode.Escape:
                running = false;
                break;
            case Keycode.C:
                // Clear canvas
                renderer.Target = canvas;
                renderer.DrawColor = Color.White;
                renderer.Clear();
                renderer.Target = null;
                break;
            case Keycode.Num1: selectedColor = 0; break;
            case Keycode.Num2: selectedColor = 1; break;
            case Keycode.Num3: selectedColor = 2; break;
            case Keycode.Num4: selectedColor = 3; break;
            case Keycode.Num5: selectedColor = 4; break;
            case Keycode.Num6: selectedColor = 5; break;
            case Keycode.Num7: selectedColor = 6; break;
            case Keycode.Num8: selectedColor = 7; break;
            case Keycode.Num9: selectedColor = 8; break;
        }
        brushColor = palette[selectedColor];
    };

    Application.MouseButtonDown += e =>
    {
        if (e.Button == MouseButton.Left)
        {
            drawing = true;
            lastX = e.X;
            lastY = e.Y;

            // Draw a dot at click position
            renderer.Target = canvas;
            renderer.DrawColor = brushColor;
            renderer.FillRect(new FRectangle(e.X - brushSize, e.Y - brushSize, brushSize * 2, brushSize * 2));
            renderer.Target = null;
        }
    };

    Application.MouseButtonUp += e =>
    {
        if (e.Button == MouseButton.Left)
            drawing = false;
    };

    Application.MouseMotion += e =>
    {
        if (!drawing) return;

        // Draw a line from last position to current
        renderer.Target = canvas;
        renderer.DrawColor = brushColor;

        // Interpolate between last and current to fill gaps
        var dx = e.X - lastX;
        var dy = e.Y - lastY;
        var dist = MathF.Sqrt(dx * dx + dy * dy);
        var steps = (int)MathF.Max(dist / brushSize, 1);

        for (var i = 0; i <= steps; i++)
        {
            var t = steps > 0 ? (float)i / steps : 0;
            var px = lastX + dx * t;
            var py = lastY + dy * t;
            renderer.FillRect(new FRectangle(px - brushSize, py - brushSize, brushSize * 2, brushSize * 2));
        }

        renderer.Target = null;
        lastX = e.X;
        lastY = e.Y;
    };

    Application.MouseWheel += e =>
    {
        brushSize = Math.Clamp(brushSize + (int)e.Y, 1, 20);
    };

    while (running)
    {
        Application.DispatchEvents();

        // Draw canvas to screen
        renderer.DrawColor = new Color(200, 200, 200);
        renderer.Clear();
        renderer.RenderTexture(canvas, null, null);

        // Draw color palette bar at top
        for (var i = 0; i < palette.Length; i++)
        {
            var rect = new FRectangle(10 + i * 30, 5, 25, 25);
            renderer.DrawColor = palette[i];
            renderer.FillRect(rect);

            // Highlight selected
            if (i == selectedColor)
            {
                renderer.DrawColor = new Color(255, 255, 0);
                renderer.DrawRect(rect);
            }
        }

        // Draw brush size indicator
        renderer.DrawColor = Color.Black;
        renderer.DrawDebugText(300, 10, $"Brush: {brushSize}  C=clear  1-9=color  Wheel=size  ESC=quit");

        renderer.Present();
        Thread.Sleep(16);
    }
}

Console.WriteLine("Done.");
