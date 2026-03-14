using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Input;

using var app = new Application(InitFlags.Video);

var (window, renderer) = Renderer.CreateWindowAndRenderer("SdlSharp Input Demo", 800, 600);
using (window)
using (renderer)
{
    var running = true;
    var lastKey = "None";
    var lastKeyState = "";
    float mouseX = 0, mouseY = 0;
    var mouseButtons = "";
    var lastEvent = "";
    var keyPressCount = 0;

    Application.Quit += _ => running = false;

    Application.KeyDown += e =>
    {
        if (e.Key == Keycode.Escape)
        {
            running = false;
            return;
        }
        keyPressCount++;
        lastKey = $"{Keyboard.GetKeyName(e.Key)} (scancode: {e.Scancode})";
        lastKeyState = "DOWN";
        lastEvent = $"KeyDown: {lastKey}";
    };

    Application.KeyUp += e =>
    {
        lastKeyState = "UP";
        lastEvent = $"KeyUp: {Keyboard.GetKeyName(e.Key)}";
    };

    Application.MouseMotion += e =>
    {
        mouseX = e.X;
        mouseY = e.Y;
    };

    Application.MouseButtonDown += e =>
    {
        mouseButtons = $"{e.Button} DOWN at ({e.X:F0}, {e.Y:F0})";
        lastEvent = $"MouseButtonDown: {e.Button} clicks={e.Clicks}";
    };

    Application.MouseButtonUp += e =>
    {
        mouseButtons = $"{e.Button} UP";
        lastEvent = $"MouseButtonUp: {e.Button}";
    };

    Application.MouseWheel += e =>
    {
        lastEvent = $"MouseWheel: x={e.X:F1} y={e.Y:F1}";
    };

    while (running)
    {
        Application.DispatchEvents();

        renderer.DrawColor = new Color(30, 30, 50);
        renderer.Clear();

        renderer.DrawColor = Color.White;
        renderer.DrawDebugText(10, 10, "=== SdlSharp Input Demo ===");
        renderer.DrawDebugText(10, 30, "Press ESC to quit");

        renderer.DrawColor = new Color(200, 200, 100);
        renderer.DrawDebugText(10, 60, $"Last key: {lastKey} ({lastKeyState})");
        renderer.DrawDebugText(10, 80, $"Key presses: {keyPressCount}");
        renderer.DrawDebugText(10, 100, $"Modifiers: {Keyboard.ModState}");

        renderer.DrawColor = new Color(100, 200, 200);
        renderer.DrawDebugText(10, 130, $"Mouse: ({mouseX:F0}, {mouseY:F0})");
        renderer.DrawDebugText(10, 150, $"Mouse buttons: {mouseButtons}");

        renderer.DrawColor = new Color(200, 100, 200);
        renderer.DrawDebugText(10, 180, $"Last event: {lastEvent}");

        // Draw a crosshair at the mouse position
        renderer.DrawColor = new Color(255, 255, 0);
        renderer.DrawLine(mouseX - 10, mouseY, mouseX + 10, mouseY);
        renderer.DrawLine(mouseX, mouseY - 10, mouseX, mouseY + 10);

        // Show which keys are currently held via polling
        renderer.DrawColor = new Color(100, 255, 100);
        renderer.DrawDebugText(10, 220, "Held keys (polled):");
        var y = 240;
        if (Keyboard.IsKeyPressed(Scancode.W)) { renderer.DrawDebugText(20, y, "W"); y += 16; }
        if (Keyboard.IsKeyPressed(Scancode.A)) { renderer.DrawDebugText(20, y, "A"); y += 16; }
        if (Keyboard.IsKeyPressed(Scancode.S)) { renderer.DrawDebugText(20, y, "S"); y += 16; }
        if (Keyboard.IsKeyPressed(Scancode.D)) { renderer.DrawDebugText(20, y, "D"); y += 16; }
        if (Keyboard.IsKeyPressed(Scancode.Space)) { renderer.DrawDebugText(20, y, "SPACE"); y += 16; }
        if (Keyboard.IsKeyPressed(Scancode.LShift)) { renderer.DrawDebugText(20, y, "LSHIFT"); }

        renderer.Present();
        Thread.Sleep(16);
    }
}

Console.WriteLine("Done.");
