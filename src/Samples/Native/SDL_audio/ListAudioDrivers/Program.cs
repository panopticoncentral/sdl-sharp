using SdlSharp;

unsafe
{
    var audioDriverCount = Native.SDL_GetNumAudioDrivers();

    for (var index = 0; index < audioDriverCount; index++)
    {
        var namePointer = Native.SDL_GetAudioDriver(index);
        var name = new StaticUtf8String(namePointer);
        Console.WriteLine(name.ToString());
    }

    _ = Console.ReadLine();
}
