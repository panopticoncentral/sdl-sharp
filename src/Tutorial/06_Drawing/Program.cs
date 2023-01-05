using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Input;

using Application app = new(Subsystems.Video, ImageFormats.Jpg, hints: new[] { (Hint.RenderScaleQuality, "1") });
Size windowSize = new(640, 480);
Rectangle windowRectangle = new(Window.UndefinedWindowLocation, windowSize);
using var window = Window.Create("Drawing", windowRectangle, WindowOptions.Shown);
using var renderer = Renderer.Create(window, -1, RendererOptions.Accelerated);

Rectangle? viewport = null;

Keyboard.KeyDown += (s, e) => viewport = e.Keycode switch
{
    Keycode.Number0 => null,
    Keycode.Number1 => new(Point.Origin, new(windowSize.Width / 2, windowSize.Height / 2)),
    Keycode.Number2 => new(new(windowSize.Width / 2, 0), new(windowSize.Width / 2, windowSize.Height / 2)),
    Keycode.Number3 => new(new(0, windowSize.Height / 2), new(windowSize.Width, windowSize.Height / 2)),
    _ => viewport
};

while (app.DispatchEvents())
{
    renderer.DrawColor = Colors.White;
    renderer.Clear();

    renderer.Viewport = viewport;
    var size = viewport?.Size ?? windowSize;

    renderer.DrawColor = Colors.Red;
    renderer.FillRectangle(new(new(size.Width / 4, size.Height / 4), new(size.Width / 2, size.Height / 2)));

    renderer.DrawColor = Colors.Green;
    renderer.DrawRectangle(new(new(size.Width / 6, size.Height / 6), new(size.Width * 2 / 3, size.Height * 2 / 3)));

    renderer.DrawColor = Colors.Blue;
    renderer.DrawLine(new(new(0, size.Height / 2), new(size.Width, size.Height / 2)));

    renderer.DrawColor = Colors.Yellow;
    for (var i = 0; i < size.Height; i += 4)
    {
        renderer.DrawPoint(new(size.Width / 2, i));
    }

    renderer.Viewport = null;

    renderer.Present();
}
