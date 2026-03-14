// AudioInfo sample — enumerates audio drivers, playback/recording devices, and queries formats.
// Exercises: AudioDevice.NumAudioDrivers, GetAudioDriver, CurrentAudioDriver,
//            GetPlaybackDevices, GetRecordingDevices, Open, GetFormat, Pause/Resume,
//            IsPaused, Gain, Dispose.

using SdlSharp;
using SdlSharp.Audio;

using var app = new Application(InitFlags.Audio);

// --- Audio drivers ---
Console.WriteLine("=== Audio Drivers ===");
var numDrivers = AudioDevice.NumAudioDrivers;
Console.WriteLine($"  Built-in drivers: {numDrivers}");
for (var i = 0; i < numDrivers; i++)
    Console.WriteLine($"    [{i}] {AudioDevice.GetAudioDriver(i)}");
Console.WriteLine($"  Current driver: {AudioDevice.CurrentAudioDriver}");

// --- Playback devices ---
Console.WriteLine();
Console.WriteLine("=== Playback Devices ===");
var playbackDevices = AudioDevice.GetPlaybackDevices();
if (playbackDevices.Length == 0)
{
    Console.WriteLine("  (none found)");
}
else
{
    foreach (var (id, name) in playbackDevices)
        Console.WriteLine($"  [{id}] {name}");
}

// --- Recording devices ---
Console.WriteLine();
Console.WriteLine("=== Recording Devices ===");
var recordingDevices = AudioDevice.GetRecordingDevices();
if (recordingDevices.Length == 0)
{
    Console.WriteLine("  (none found)");
}
else
{
    foreach (var (id, name) in recordingDevices)
        Console.WriteLine($"  [{id}] {name}");
}

// --- Open default playback device and inspect ---
Console.WriteLine();
Console.WriteLine("=== Default Playback Device ===");
using (var device = AudioDevice.OpenPlayback())
{
    var spec = device.GetFormat(out var sampleFrames);
    Console.WriteLine($"  Format: {spec.Format}");
    Console.WriteLine($"  Channels: {spec.Channels}");
    Console.WriteLine($"  Frequency: {spec.Frequency} Hz");
    Console.WriteLine($"  Buffer size: {sampleFrames} sample frames");

    Console.WriteLine($"  Paused: {device.IsPaused}");
    Console.WriteLine($"  Gain: {device.Gain}");

    device.Pause();
    Console.WriteLine($"  After Pause — Paused: {device.IsPaused}");

    device.Resume();
    Console.WriteLine($"  After Resume — Paused: {device.IsPaused}");

    device.Gain = 0.5f;
    Console.WriteLine($"  After Gain=0.5 — Gain: {device.Gain}");
}

// --- Open with specific format ---
Console.WriteLine();
Console.WriteLine("=== Open with Specific Format ===");
var requestedSpec = new AudioSpec(AudioFormat.S16LE, 2, 44100);
using (var device = AudioDevice.OpenPlayback(requestedSpec))
{
    var spec = device.GetFormat(out _);
    Console.WriteLine($"  Requested: {requestedSpec.Format}, {requestedSpec.Channels}ch, {requestedSpec.Frequency}Hz");
    Console.WriteLine($"  Got:       {spec.Format}, {spec.Channels}ch, {spec.Frequency}Hz");
}

Console.WriteLine();
Console.WriteLine("Done.");
