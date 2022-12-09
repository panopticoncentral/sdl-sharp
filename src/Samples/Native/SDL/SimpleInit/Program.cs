using SdlSharp;

_ = Native.CheckError(Native.SDL_Init(0));
Native.SDL_Quit();
