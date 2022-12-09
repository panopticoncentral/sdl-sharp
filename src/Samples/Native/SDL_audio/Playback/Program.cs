using SdlSharp;

_ = Native.CheckError(Native.SDL_Init(Native.SDL_INIT_AUDIO));

unsafe
{
    Native.SDL_AudioSpec wavSpec;
    byte* buffer;
    uint length;
    _ = Native.CheckPointer(Native.SDL_LoadWAV("Fanfare60.wav", &wavSpec, &buffer, &length));

    Native.SDL_AudioSpec deviceSpec;
    var deviceId = Native.CheckValid(Native.SDL_OpenAudioDevice(null, Native.BoolToInt(false), &wavSpec, &deviceSpec, 0));

    _ = Native.CheckError(Native.SDL_QueueAudio(deviceId, buffer, length));

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

    Native.SDL_FreeWAV(buffer);
}

Native.SDL_Quit();
