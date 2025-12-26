using Sdl3Sharp;
using Sdl3Sharp.Demo;
using Sdl3Sharp.Graphics;
using ImGui = Sdl3Sharp.ImGui;

// Initialize SDL
using Application app = new(Subsystems.Video);

// Create window and renderer
(Window? window, Renderer? renderer) = Renderer.CreateWindowAndRenderer("ImGui SDL Renderer Demo", new(1280, 720), WindowFlags.Resizable);

// Create ImGui context
var context = ImGui.Context.Create();
ImGui.Context.Style.FontSizeBase = 24.0f;
ImGui.Context.Current = context;

// Initialize ImGui backends
ImGui.Backends.SDL3Backend.InitForSDLRenderer(window, renderer);
ImGui.Backends.SDLRenderer3Backend.Init(renderer);

var quit = false;
Application.Quitting += (sender, e) => quit = true;

// Demo state
ImGui.StateRef<bool> showDemoWindow = ImGui.StateStore.Instance.Create(true);
ImGui.StateRef<bool> showManagedDemoWindow = ImGui.StateStore.Instance.Create(true);

// Handle live resize: render during window resize on Windows
void DoFrame()
{
    // Start the ImGui frame
    ImGui.Backends.SDL3Backend.NewFrame();
    ImGui.Backends.SDLRenderer3Backend.NewFrame();
    ImGui.Context.NewFrame();

    // Show the ImGui demo window
    ImGui.Context.ShowDemoWindow(showDemoWindow);

    // Show the managed ImGui demo window
    ImGuiDemoWindow.ShowDemoWindow(showManagedDemoWindow);

    // Rendering
    ImGui.Context.Render();

    renderer.DrawColor = new Color(45, 55, 60, 255);
    renderer.Clear();
    ImGui.Backends.SDLRenderer3Backend.RenderDrawData(renderer);
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
        _ = ImGui.Backends.SDL3Backend.ProcessEvent(e.Value);
        EventQueue.DispatchEvent(e.Value);
    }

    DoFrame();
}

// Cleanup
EventQueue.RemoveWatch(watchHandle);
ImGui.Backends.SDLRenderer3Backend.Shutdown();
ImGui.Backends.SDL3Backend.Shutdown();

renderer.Dispose();
window.Dispose();

ImGui.Context.Destroy(context);