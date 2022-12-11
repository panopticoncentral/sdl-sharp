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

            var audioStream = Native.CheckPointer(Native.SDL_NewAudioStream(wavSpec.format, wavSpec.channels, wavSpec.freq, deviceSpec.format, deviceSpec.channels, deviceSpec.freq));
            _ = Native.CheckError(Native.SDL_AudioStreamPut(audioStream, buffer, (int)length));
            _ = Native.CheckError(Native.SDL_AudioStreamFlush(audioStream));
            var resultLength = Native.SDL_AudioStreamAvailable(audioStream);
            var resultBuffer = Native.SDL_malloc((nuint)resultLength);
            _ = Native.CheckError(Native.SDL_AudioStreamGet(audioStream, resultBuffer, resultLength));
            Native.SDL_FreeAudioStream(audioStream);
            Native.SDL_FreeWAV(buffer);

            _ = Native.CheckError(Native.SDL_QueueAudio(deviceId, resultBuffer, (uint)resultLength));

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
            Native.SDL_free(resultBuffer);

            Native.SDL_Quit();
        }
    }
}
