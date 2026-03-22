// GamepadDemo sample — enumerates joystick and gamepad devices and prints info.
// Exercises: Joystick.GetDevices, GetName, GetType, Joystick.Open,
//            Gamepad.IsGamepad, Gamepad.GetDevices, Gamepad.GetName, Gamepad.GetType,
//            Gamepad.Open, JoystickType, GamepadType, JoystickConnectionState.
// Does not require any connected device; enumerates whatever is available.

using SdlSharp;
using SdlSharp.Input;

using var app = new Application(InitFlags.Gamepad);

// --- Joystick enumeration ---
Console.WriteLine("=== Joystick Devices ===");
var joystickIds = Joystick.GetDevices();
if (joystickIds.Length == 0)
{
    Console.WriteLine("  (no joysticks connected)");
}
else
{
    Console.WriteLine($"  Joysticks found: {joystickIds.Length}");
    foreach (var id in joystickIds)
    {
        var name = Joystick.GetName(id) ?? "(unknown)";
        var type = Joystick.GetType(id);
        var isGamepad = Gamepad.IsGamepad(id);
        Console.WriteLine($"  [{id}] {name}  type={type}  isGamepad={isGamepad}");
    }
}

// --- Gamepad enumeration ---
Console.WriteLine();
Console.WriteLine("=== Gamepad Devices ===");
var gamepadIds = Gamepad.GetDevices();
if (gamepadIds.Length == 0)
{
    Console.WriteLine("  (no gamepads connected)");
}
else
{
    Console.WriteLine($"  Gamepads found: {gamepadIds.Length}");
    foreach (var id in gamepadIds)
    {
        var name = Gamepad.GetName(id) ?? "(unknown)";
        var type = Gamepad.GetType(id);
        Console.WriteLine($"  [{id}] {name}  type={type}");
    }
}

// --- Open first available gamepad and inspect ---
Console.WriteLine();
Console.WriteLine("=== First Gamepad Detail ===");
if (gamepadIds.Length == 0)
{
    Console.WriteLine("  (no gamepad to open)");
}
else
{
    using var gamepad = Gamepad.Open(gamepadIds[0]);
    Console.WriteLine($"  Name:              {gamepad.Name}");
    Console.WriteLine($"  Type:              {gamepad.Type}");
    Console.WriteLine($"  Connection state:  {gamepad.ConnectionState}");
    Console.WriteLine($"  Left stick X:      {gamepad.GetAxis(GamepadAxis.LeftX)}");
    Console.WriteLine($"  Left stick Y:      {gamepad.GetAxis(GamepadAxis.LeftY)}");
    Console.WriteLine($"  South button:      {gamepad.GetButton(GamepadButton.South)}");
}

Console.WriteLine();
Console.WriteLine("Done.");
