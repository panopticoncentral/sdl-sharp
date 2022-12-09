using SdlSharp;

_ = Native.CheckError(Native.SDL_Init(Native.SDL_INIT_TIMER));

if (Native.SDL_WasInit(0) != Native.SDL_INIT_TIMER)
{
    throw new InvalidOperationException();
}

_ = Native.CheckError(Native.SDL_InitSubSystem(Native.SDL_INIT_HAPTIC));

if (Native.SDL_WasInit(0) != (Native.SDL_INIT_TIMER | Native.SDL_INIT_HAPTIC))
{
    throw new InvalidOperationException();
}

Native.SDL_QuitSubSystem(Native.SDL_INIT_HAPTIC);

if (Native.SDL_WasInit(0) != Native.SDL_INIT_TIMER)
{
    throw new InvalidOperationException();
}

Native.SDL_Quit();
