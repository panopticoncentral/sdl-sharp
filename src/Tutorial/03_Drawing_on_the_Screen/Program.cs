using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Input;

using Application app = new(Subsystems.Video);
Size windowSize = new(640, 480);
Rectangle windowRectangle = new(Window.UndefinedWindowLocation, windowSize);
using var window = Window.Create("Drawing on the Screen", windowRectangle, WindowOptions.Shown);

using var sunflowers = Surface.LoadBmp("Sunflowers.bmp");

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
