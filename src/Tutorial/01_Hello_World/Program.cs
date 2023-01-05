using SdlSharp;
using SdlSharp.Graphics;

using var app = new Application(Subsystems.Video);
Size windowSize = new(640, 480);
Rectangle windowRectangle = new(Window.UndefinedWindowLocation, windowSize);
using var window = Window.Create("Hello, World!", windowRectangle, WindowOptions.Shown);

while (app.DispatchEvents())
{
}
