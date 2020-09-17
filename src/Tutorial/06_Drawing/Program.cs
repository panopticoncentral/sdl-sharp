using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Input;

using Application app = new (Subsystems.Video, ImageFormats.Jpg, hints: new[] { (Hint.RenderScaleQuality, "1") });
Size windowSize = (640, 480);
Rectangle windowRectangle = (Window.UndefinedWindowLocation, windowSize);
using var window = Window.Create("Drawing", windowRectangle, WindowFlags.Shown);
using var renderer = Renderer.Create(window, -1, RendererFlags.Accelerated);

Rectangle? viewport = null;

Keyboard.KeyDown += (s, e) =>
{
    switch (e.Keycode)
    {
        case Keycode.Number0:
            viewport = null;
            break;

        case Keycode.Number1:
            viewport = (Point.Origin, (windowSize.Width / 2, windowSize.Height / 2));
            break;

        case Keycode.Number2:
            viewport = ((windowSize.Width / 2, 0), (windowSize.Width / 2, windowSize.Height / 2));
            break;

        case Keycode.Number3:
            viewport = ((0, windowSize.Height / 2), (windowSize.Width, windowSize.Height / 2));
            break;
    }
};

while (app.DispatchEvent())
{
    renderer.DrawColor = Colors.White;
    renderer.Clear();

    renderer.Viewport = viewport;
    var size = viewport?.Size ?? windowSize;

    renderer.DrawColor = Colors.Red;
    renderer.FillRectangle(((size.Width / 4, size.Height / 4), (size.Width / 2, size.Height / 2)));

    renderer.DrawColor = Colors.Green;
    renderer.DrawRectangle(((size.Width / 6, size.Height / 6), (size.Width * 2 / 3, size.Height * 2 / 3)));

    renderer.DrawColor = Colors.Blue;
    renderer.DrawLine((0, size.Height / 2), (size.Width, size.Height / 2));

    renderer.DrawColor = Colors.Yellow;
    for (var i = 0; i < size.Height; i += 4)
    {
        renderer.DrawPoint((size.Width / 2, i));
    }

    renderer.Viewport = null;

    renderer.Present();
}
