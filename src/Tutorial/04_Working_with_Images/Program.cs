using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Input;

using Application app = new(Subsystems.Video, ImageFormats.Jpg);
Size windowSize = new(640, 480);
Rectangle windowRectangle = new(Window.UndefinedWindowLocation, windowSize);
using var window = Window.Create("Working with Images", windowRectangle, WindowOptions.Shown);

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

while (app.DispatchEvents())
{
    if (stretch)
    {
        sunflowers.BlitScaled(window.Surface, null, new(Point.Origin, windowSize));
    }
    else
    {
        sunflowers.Blit(window.Surface);
    }

    window.UpdateSurface();
}
