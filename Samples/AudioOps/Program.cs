// AudioOps sample — headless exercise of the phase 10 audio surface API.
// Exercises: format introspection, FrameSize, silence values, Convert, Mix,
// stream put callback, Lock scope, channel maps, device access, postmix callback.

using SdlSharp;
using SdlSharp.Audio;

Environment.SetEnvironmentVariable("SDL_AUDIO_DRIVER", "dummy");

using var app = new Application(InitFlags.Audio);

var failures = 0;

void Check(string name, bool ok)
{
    Console.WriteLine($"{(ok ? "PASS" : "FAIL")}  {name}");
    if (!ok) failures++;
}

// Format introspection.
Check("S16 bit size", AudioFormat.S16.GetBitSize() == 16);
Check("S16 byte size", AudioFormat.S16.GetByteSize() == 2);
Check("S16 is signed", AudioFormat.S16.IsSigned());
Check("S16 is int", AudioFormat.S16.IsInt());
Check("F32 is float", AudioFormat.F32.IsFloat());
Check("U8 is unsigned", AudioFormat.U8.IsUnsigned());
Check("S16 is little-endian", AudioFormat.S16.IsLittleEndian());
Check("format name non-empty", !string.IsNullOrEmpty(AudioFormat.S16.GetName()));

// FrameSize.
var stereoS16 = new AudioSpec(AudioFormat.S16, 2, 48000);
Check("stereo S16 frame size", stereoS16.FrameSize == 4);

// Silence values.
Check("U8 silence is 128", AudioFormat.U8.GetSilenceValue() == 128);
Check("S16 silence is 0", AudioFormat.S16.GetSilenceValue() == 0);

// Convert: 4 mono S16 samples -> stereo F32.
var srcSpec = new AudioSpec(AudioFormat.S16, 1, 48000);
var dstSpec = new AudioSpec(AudioFormat.F32, 2, 48000);
var srcBytes = new byte[4 * 2]; // 4 samples * 2 bytes
var converted = AudioSamples.Convert(srcSpec, srcBytes, dstSpec);
// 4 frames * 2 channels * 4 bytes = 32 bytes expected.
Check("convert output length", converted.Length == 32);

// Mix: silence into silence stays silence.
var dst = new byte[8];
var add = new byte[8];
AudioSamples.Mix(dst, add, AudioFormat.S16, 1.0f);
Check("mix silence stays zero", Array.TrueForAll(dst, b => b == 0));

// Stream put callback fires on PutData.
using var stream = AudioStream.Create(srcSpec, srcSpec);
var putCallbackFired = false;
stream.SetPutCallback((s, additional, total) => putCallbackFired = true);
stream.PutData(new byte[16]);
Check("put callback fired", putCallbackFired);

// Lock scope round-trips.
var locked = false;
using (stream.Lock())
{
    locked = true;
}
Check("lock scope", locked);

// Channel map default is null.
Check("input channel map default null", stream.GetInputChannelMap() is null);

// Clearing the callback works.
stream.SetPutCallback(null);
Check("put callback cleared", true);

// Device from OpenDevice is non-null; postmix set/clear.
using var deviceStream = AudioStream.OpenDevice(playback: true);
var device = deviceStream.Device;
Check("stream device non-null", device is not null);
if (device is not null)
{
    device.SetPostmixCallback((in AudioSpec spec, Span<float> buffer) => { });
    device.SetPostmixCallback(null);
    Check("postmix set/clear", true);
    Check("device is playback", device.IsPlayback);
}

Console.WriteLine(failures == 0 ? "ALL PASS" : $"{failures} FAILURES");
return failures == 0 ? 0 : 1;
