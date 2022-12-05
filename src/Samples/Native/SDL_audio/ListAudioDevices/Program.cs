using SdlSharp;

unsafe
{
    static void PrintAudioDevices(bool isCapture)
    {
        var audioDeviceCount = Native.SDL_GetNumAudioDevices(Native.BoolToInt(isCapture));

        for (var index = 0; index < audioDeviceCount; index++)
        {
            var name = Native.Utf8ToString(Native.SDL_GetAudioDeviceName(index, Native.BoolToInt(isCapture)));
            Native.SDL_AudioSpec spec;
            _ = Native.CheckError(Native.SDL_GetAudioDeviceSpec(index, Native.BoolToInt(isCapture), &spec));

            Console.WriteLine($"{name}:");
            Console.WriteLine($"\tFrequency: {spec.freq}");
            Console.WriteLine($"\tChannels: {spec.channels}");
            Console.WriteLine($"\tSilence: {spec.silence}");
            Console.WriteLine($"\tSamples: {spec.samples}");
            Console.WriteLine($"\tSize: {spec.size}");
        }
    }

    _ = Native.CheckError(Native.SDL_Init(Native.SDL_INIT_AUDIO));

    Console.WriteLine("Non-capture devices:");
    PrintAudioDevices(false);

    Console.WriteLine();
    Console.WriteLine("Capture devices:");
    PrintAudioDevices(true);

    Native.SDL_Quit();

    _ = Console.ReadLine();
}
