using Sdl3Sharp;
using Sdl3Sharp.Graphics;

using Application app = new(Subsystems.Video);
using var window = Window.Create("Hello, World!", new(640, 480), WindowFlags.None);

var quit = false;
Application.Quitting += (sender, e) => quit = true;

while (!quit)
{
    while (true)
    {
        Event? e = EventQueue.Poll();

        if (e == null)
        {
            break;
        }

        EventQueue.DispatchEvent(e.Value);
    }
}
