using SdlSharp;

namespace Samples
{
    public static class Program
    {
        public static unsafe void Main()
        {
            _ = Native.CheckError(Native.SDL_Init(Native.SDL_INIT_AUDIO));

            Native.SDL_AudioSpec wavSpec;
            byte* buffer;
            uint length;
            _ = Native.CheckPointer(Native.SDL_LoadWAV("Fanfare60.wav", &wavSpec, &buffer, &length));

            Native.SDL_AudioSpec deviceSpec;
            var deviceId = Native.CheckValid(Native.SDL_OpenAudioDevice(null, Native.BoolToInt(false), &wavSpec, &deviceSpec, (int)Native.SDL_AUDIO_ALLOW_ANY_CHANGE));

            Native.SDL_AudioCVT audioConvert;
            if (!Native.CheckErrorBool(Native.SDL_BuildAudioCVT(&audioConvert, wavSpec.format, wavSpec.channels, wavSpec.freq, deviceSpec.format, deviceSpec.channels, deviceSpec.freq)))
            {
                Console.WriteLine("Failed to convert!");
                return;
            }

            var resultBuffer = new byte[length * audioConvert.len_mult];
            new Span<byte>(buffer, (int)length).CopyTo(resultBuffer);

            fixed (byte* resultBufferPointer = resultBuffer)
            {
                audioConvert.buf = resultBufferPointer;
                audioConvert.len = (int)length;
                _ = Native.CheckError(Native.SDL_ConvertAudio(&audioConvert));

                var resultLength = audioConvert.len_cvt;

                Native.SDL_FreeWAV(buffer);

                _ = Native.CheckError(Native.SDL_QueueAudio(deviceId, resultBufferPointer, (uint)resultLength));
            }

            var quit = false;
            while (!quit)
            {
                var command = Console.ReadLine()?.Trim();

                switch (command)
                {
                    case "g":
                        Native.SDL_PauseAudioDevice(deviceId, Native.BoolToInt(false));
                        break;

                    case "p":
                        Native.SDL_PauseAudioDevice(deviceId, Native.BoolToInt(true));
                        break;

                    case "q":
                        quit = true;
                        break;
                }
            }

            Native.SDL_CloseAudioDevice(deviceId);

            Native.SDL_Quit();
        }
    }
}
