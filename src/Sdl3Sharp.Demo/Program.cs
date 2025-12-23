using Sdl3Sharp;
using Sdl3Sharp.Graphics;
using ImGui = Sdl3Sharp.ImGui;
using Sdl3Sharp.ImGui.Backends;

using Sdl3WindowFlags = Sdl3Sharp.Graphics.WindowFlags;

// Initialize SDL
using Application app = new(Subsystems.Video);

// Create window and renderer
(Window? window, Renderer? renderer) = Renderer.CreateWindowAndRenderer("ImGui SDL Renderer Demo", new(1280, 720), Sdl3WindowFlags.Resizable);

// Create ImGui context
using ImGui.Context context = new();
ImGui.Context.Style.FontSizeBase = 24.0f;
ImGui.Context.Current = context;

// Initialize ImGui backends
SDL3Backend.InitForSDLRenderer(window, renderer);
SDLRenderer3Backend.Init(renderer);

// Create a state store for persistent ImGui state
using var state = new ImGui.StateStore();

var quit = false;
Application.Quitting += (sender, e) => quit = true;

// Demo state
ImGui.StateRef<bool> showDemoWindow = state.Create(true);

// Handle live resize: render during window resize on Windows
void DoFrame()
{
    // Start the ImGui frame
    SDL3Backend.NewFrame();
    SDLRenderer3Backend.NewFrame();
    ImGui.Context.NewFrame();

    // Show the ImGui demo window
    ImGui.Context.ShowDemoWindow(showDemoWindow);

    // Rendering
    ImGui.Context.Render();

    renderer.DrawColor = new Color(45, 55, 60, 255);
    renderer.Clear();
    SDLRenderer3Backend.RenderDrawData(renderer);
    renderer.Present();
}

EventWatchHandle watchHandle = EventQueue.AddWatch(e =>
{
    if (e.Type == EventType.WindowPixelSizeChanged)
    {
        DoFrame();
    }
});

while (!quit)
{
    // Process events
    Event? e;
    while ((e = EventQueue.Poll()) != null)
    {
        _ = SDL3Backend.ProcessEvent(e.Value);
        EventQueue.DispatchEvent(e.Value);
    }

    DoFrame();
}

// Cleanup
EventQueue.RemoveWatch(watchHandle);
SDLRenderer3Backend.Shutdown();
SDL3Backend.Shutdown();

renderer.Dispose();
window.Dispose();