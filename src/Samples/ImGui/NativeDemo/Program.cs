using Sdl3Sharp;
using Sdl3Sharp.Graphics;

using var app = new Application(Subsystems.Video);
Size windowSize = new(640, 480);
using Window window = new("Hello, World!", windowSize, WindowFlags.None);

while (true)
{
    EventQueue.Pump();
}
