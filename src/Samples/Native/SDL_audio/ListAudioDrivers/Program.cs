using SdlSharp;

unsafe
{
    var audioDriverCount = Native.SDL_GetNumAudioDrivers();

    Console.WriteLine("Available audio drivers:");
    for (var index = 0; index < audioDriverCount; index++)
    {
        Console.WriteLine(Native.Utf8ToString(Native.SDL_GetAudioDriver(index)));
    }

    _ = Native.CheckError(Native.SDL_Init(Native.SDL_INIT_AUDIO));
    Console.WriteLine();
    Console.WriteLine("Current audio driver:");
    Console.WriteLine(Native.Utf8ToString(Native.SDL_GetCurrentAudioDriver()));
    Native.SDL_Quit();

    _ = Console.ReadLine();
}
