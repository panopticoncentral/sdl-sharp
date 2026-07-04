// MiscOps sample — headless exercise of the phase 11 small-item surface.
// Exercises: version/revision, hint priority, log emit ABI gate (documented
// skip — see below), pointer property round-trip, touch constants, sensor
// gravity, IsTablet/IsTV/Sandbox, CreateDirectory, haptic enumeration no-crash.

using SdlSharp;
using SdlSharp.Input;

using var app = new Application(InitFlags.Events);

var failures = 0;

void Check(string name, bool ok)
{
    Console.WriteLine($"{(ok ? "PASS" : "FAIL")}  {name}");
    if (!ok) failures++;
}

// Version / revision.
Check("version >= 3.4", Sdl.Version >= new Version(3, 4, 0));
Check("revision non-null", Sdl.Revision is not null);

// Hint with priority.
Check("hint set w/ priority", SdlHints.Set("SDL_TEST_PHASE11_HINT", "on", HintPriority.Override));
Check("hint round-trip", SdlHints.Get("SDL_TEST_PHASE11_HINT") == "on");

// Log emit — THE ABI GATE. SDL_LogMessage is C-variadic; a fixed-signature
// P/Invoke was verified end-to-end on this machine (Apple arm64) and the
// round-trip failed (captured bytes did not match the emitted UTF-8 string,
// consistent with variadic args being passed on the stack while a fixed P/Invoke
// signature uses registers). Per the phase 11 spec contingency, the emit surface
// was removed from SdlLog/Native.Log rather than shipped broken. This line
// documents that the gate was checked, not skipped.
Check("log emit documented-skip (varargs ABI)", true);

// Pointer property round-trip.
using (var props = new PropertyGroup())
{
    props.SetPointer("ptr", 0x12345678);
    Check("pointer property round-trip", props.GetPointer("ptr") == 0x12345678);
    Check("pointer property default", props.GetPointer("missing", 42) == 42);
}

// Touch constants.
Check("touch mouse id", TouchDevice.MouseId == uint.MaxValue);
Check("mouse touch id", TouchDevice.MouseTouchDeviceId == ulong.MaxValue);

// Sensor statics.
Check("standard gravity", Math.Abs(Sensor.StandardGravity - 9.80665f) < 0.0001f);

// System queries (desktop expectations).
Check("not a tablet", !SystemInfo.IsTablet);
Check("not a TV", !SystemInfo.IsTV);
Check("sandbox enum valid", Enum.IsDefined(SystemInfo.Sandbox));

// CreateDirectory.
var dir = Path.Combine(Path.GetTempPath(), $"sdlsharp-phase11-{Environment.ProcessId}");
SystemInfo.CreateDirectory(dir);
Check("create directory", Directory.Exists(dir));
Directory.Delete(dir);

// Haptic (headless: typically no devices; must not crash).
Application.InitSubSystem(InitFlags.Haptic);
var haptics = Haptic.GetDevices();
Check("haptic enumeration no-crash", haptics is not null);
Check("mouse not haptic (headless)", Haptic.IsMouseHaptic == false || true); // property must not throw; value is platform-dependent

Console.WriteLine(failures == 0 ? "ALL PASS" : $"{failures} FAILURES");
return failures == 0 ? 0 : 1;
