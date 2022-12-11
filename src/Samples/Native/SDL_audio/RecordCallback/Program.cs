using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SdlSharp;

namespace Samples
{
    public static class Program
    {
        private static byte[]? s_recordingBuffer;
        private static int s_currentOffset;
        private static int s_currentLength;

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        private static unsafe void RecordCallback(nint _, byte* buffer, int len)
        {
            new Span<byte>(buffer, len).CopyTo(new(s_recordingBuffer, s_currentOffset, s_recordingBuffer!.Length - s_currentOffset));
            s_currentOffset += len;
        }

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        private static unsafe void PlayCallback(nint _, byte* buffer, int len)
        {
            new Span<byte>(s_recordingBuffer, s_currentOffset, Math.Min(s_currentLength - s_currentOffset, len)).CopyTo(new Span<byte>(buffer, len));
            s_currentOffset += Math.Min(len, s_currentLength - s_currentOffset);
        }

        public static unsafe void Main()
        {
            _ = Native.CheckError(Native.SDL_Init(Native.SDL_INIT_AUDIO));

            var quit = false;
            while (!quit)
            {
                var command = Console.ReadLine()?.Trim();

                switch (command)
                {
                    case "g":
                        {
                            var requestedSpec = new Native.SDL_AudioSpec(0, new(0), 0, 0, &RecordCallback);
                            Native.SDL_AudioSpec actualSpec;

                            var deviceId = Native.CheckValid(Native.SDL_OpenAudioDevice(null, Native.BoolToInt(true), &requestedSpec, &actualSpec, (int)Native.SDL_AUDIO_ALLOW_ANY_CHANGE));

                            const int RecordingSeconds = 5;
                            var bytesPerSecond = actualSpec.freq * actualSpec.channels * (Native.SDL_AUDIO_BITSIZE(actualSpec.format) / 8);
                            s_recordingBuffer = new byte[(RecordingSeconds + 1) * bytesPerSecond];
                            s_currentOffset = 0;

                            Console.WriteLine("Recording...");
                            Native.SDL_PauseAudioDevice(deviceId, Native.BoolToInt(false));
                            for (var i = 0; i < RecordingSeconds * 2; i++)
                            {
                                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                            }
                            Native.SDL_PauseAudioDevice(deviceId, Native.BoolToInt(true));
                            Thread.Sleep(TimeSpan.FromSeconds(0.5));
                            Console.WriteLine("Stopped recording...");

                            s_currentLength = s_currentOffset;

                            Native.SDL_CloseAudioDevice(deviceId);
                        }
                        break;

                    case "p":
                        {
                            var requestedSpec = new Native.SDL_AudioSpec(0, new(0), 0, 0, &PlayCallback);
                            Native.SDL_AudioSpec actualSpec;

                            var deviceId = Native.CheckValid(Native.SDL_OpenAudioDevice(null, Native.BoolToInt(false), &requestedSpec, &actualSpec, (int)Native.SDL_AUDIO_ALLOW_ANY_CHANGE));

                            s_currentOffset = 0;

                            Console.WriteLine("Playing...");
                            Native.SDL_PauseAudioDevice(deviceId, Native.BoolToInt(false));

                            while (s_currentOffset < s_currentLength)
                            {
                                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                            }

                            Console.WriteLine("Stopped playing...");

                            Native.SDL_CloseAudioDevice(deviceId);
                        }
                        break;

                    case "q":
                        quit = true;
                        break;
                }
            }

            Native.SDL_Quit();
        }
    }
}
