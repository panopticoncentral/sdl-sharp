using Sdl3Sharp;
using Sdl3Sharp.Graphics;

using Application app = new(Subsystems.Video);
using var window = Window.Create("Hello, World!", new(640, 480), WindowFlags.None);

while (app.DispatchEvents())
{
}
