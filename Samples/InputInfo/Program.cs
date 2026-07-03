// InputInfo sample — exercises the phase 8 input surface without requiring hardware.
// Exercises: Gamepad.HasGamepad, device enumeration, mapping database, SdlGuid round-trip,
// Joystick/Gamepad events-enabled toggles, axis/button string round-trips.

using SdlSharp;
using SdlSharp.Input;

using var app = new Application(InitFlags.Joystick | InitFlags.Gamepad);

Console.WriteLine($"Has gamepad: {Gamepad.HasGamepad}");
Console.WriteLine($"Has joystick: {Joystick.HasJoystick}");
Console.WriteLine($"Gamepads: {Gamepad.GetDevices().Length}");
Console.WriteLine($"Joysticks: {Joystick.GetDevices().Length}");

// Mapping database: add a syntactically valid mapping for a fake device.
var added = Gamepad.AddMapping(
    "00000000f00dcafe0000000000000000,Phase8 Test Pad,a:b0,b:b1,back:b6,start:b7,leftshoulder:b4,rightshoulder:b5,dpup:h0.1,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,leftx:a0,lefty:a1");
Console.WriteLine($"Mapping added as new: {added}");

// SdlGuid round-trip through SDL's parser/formatter.
var guid = SdlGuid.Parse("00000000f00dcafe0000000000000000");
Console.WriteLine($"Guid round-trip: {guid} (zero: {guid.IsZero})");
var mapping = Gamepad.GetMappingForGuid(guid);
Console.WriteLine($"Mapping lookup: {(mapping != null ? "found" : "missing")}");

// String round-trips.
Console.WriteLine($"Axis leftx -> {Gamepad.GetAxisFromString("leftx")} -> {Gamepad.GetStringForAxis(GamepadAxis.LeftX)}");
Console.WriteLine($"Button a -> {Gamepad.GetButtonFromString("a")} -> {Gamepad.GetStringForButton(GamepadButton.South)}");

// Events-enabled toggles.
Gamepad.SetEventsEnabled(true);
Joystick.SetEventsEnabled(true);
Console.WriteLine($"Events enabled: gamepad={Gamepad.EventsEnabled} joystick={Joystick.EventsEnabled}");

Console.WriteLine("Done.");
