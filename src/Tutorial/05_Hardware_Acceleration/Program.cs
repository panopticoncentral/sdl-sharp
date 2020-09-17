using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Input;

using Application app = new (Subsystems.Video, ImageFormats.Jpg, hints: new[] { (Hint.RenderScaleQuality, "1") });
Size windowSize = (640, 480);
Rectangle windowRectangle = (Window.UndefinedWindowLocation, windowSize);
using var window = Window.Create("Hardware Acceleration", windowRectangle, WindowFlags.Shown);
using var renderer = Renderer.Create(window, -1, RendererFlags.Accelerated);
using var sunflowers = Image.Load("Sunflowers.jpg", renderer);

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
    renderer.Copy(sunflowers, null, (Point.Origin, stretch ? windowSize : sunflowers.Size));
    renderer.Present();
}
