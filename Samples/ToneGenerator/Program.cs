// ToneGenerator sample — generates a sine wave and plays it through an audio stream.
// Exercises: AudioStream.OpenDevice, PutData, ResumeDevice, PauseDevice, IsDevicePaused,
//            Gain, FrequencyRatio, Available, Queued, Flush, Clear, Dispose,
//            AudioSpec, AudioFormat.

using SdlSharp;
using SdlSharp.Audio;

using var app = new Application(InitFlags.Audio);

var spec = new AudioSpec(AudioFormat.F32LE, 1, 44100);

// OpenDevice creates a device + stream in one call (starts paused)
using var stream = AudioStream.OpenDevice(playback: true, spec);

Console.WriteLine("=== Tone Generator ===");
Console.WriteLine($"  Device paused: {stream.IsDevicePaused}");

// Generate 2 seconds of 440 Hz sine wave (A4)
const float frequency = 440.0f;
const int sampleRate = 44100;
const int durationMs = 2000;
var totalSamples = sampleRate * durationMs / 1000;
var buffer = new byte[totalSamples * sizeof(float)];

FillSineWave(buffer, frequency, sampleRate);

// Queue the audio data
stream.PutData(buffer);
Console.WriteLine($"  Queued: {stream.Queued} bytes");
Console.WriteLine($"  Available: {stream.Available} bytes");

// Set gain to 50%
stream.Gain = 0.5f;
Console.WriteLine($"  Gain: {stream.Gain}");

// Resume to start playback
stream.ResumeDevice();
Console.WriteLine($"  Device paused: {stream.IsDevicePaused}");
Console.WriteLine("  Playing 440 Hz tone for 2 seconds...");
Thread.Sleep(2000);

// Now demonstrate frequency ratio — play same tone at 2x speed (sounds like 880 Hz, 1 second)
Console.WriteLine();
Console.WriteLine("  Changing frequency ratio to 2.0 (double speed/pitch)...");
stream.FrequencyRatio = 2.0f;
Console.WriteLine($"  FrequencyRatio: {stream.FrequencyRatio}");

stream.Clear();
stream.PutData(buffer);
Thread.Sleep(1000);

// Demonstrate half speed (sounds like 220 Hz, plays for 4 seconds but we cut it short)
Console.WriteLine("  Changing frequency ratio to 0.5 (half speed/pitch)...");
stream.FrequencyRatio = 0.5f;

stream.Clear();
stream.PutData(buffer);
Thread.Sleep(2000);

// Demonstrate gain changes
Console.WriteLine();
Console.WriteLine("  Fading out over 1 second...");
stream.FrequencyRatio = 1.0f;
stream.Clear();
stream.PutData(buffer);
for (var i = 10; i >= 0; i--)
{
    stream.Gain = i / 10.0f;
    Thread.Sleep(100);
}

// Pause and clean up
stream.PauseDevice();
stream.Flush();
Console.WriteLine($"  Device paused: {stream.IsDevicePaused}");
Console.WriteLine();
Console.WriteLine("Done.");

static void FillSineWave(Span<byte> buffer, float frequency, int sampleRate)
{
    var floats = System.Runtime.InteropServices.MemoryMarshal.Cast<byte, float>(buffer);
    for (var i = 0; i < floats.Length; i++)
        floats[i] = MathF.Sin(2.0f * MathF.PI * frequency * i / sampleRate);
}
