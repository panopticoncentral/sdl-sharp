using SdlSharp;
using SdlSharp.Graphics;

using Application app = new (Subsystems.Video);
Size windowSize = (640, 480);
Rectangle windowRectangle = (Window.UndefinedWindowLocation, windowSize);
using var window = Window.Create("Hello, world!", windowRectangle, WindowFlags.Shown);

while (app.DispatchEvent())
{
}
