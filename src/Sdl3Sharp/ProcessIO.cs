namespace Sdl3Sharp;

/// <summary>
/// Describes where standard I/O should be directed when creating a process.
/// </summary>
public enum ProcessIO
{
    /// <summary>
    /// The I/O stream is inherited from the application.
    /// </summary>
    Inherited = Native.Process.SDL_ProcessIO.SDL_PROCESS_STDIO_INHERITED,

    /// <summary>
    /// The I/O stream is ignored (connected to NUL: on Windows or /dev/null on POSIX).
    /// </summary>
    Null = Native.Process.SDL_ProcessIO.SDL_PROCESS_STDIO_NULL,

    /// <summary>
    /// The I/O stream is connected to a new IOStream that the application can read or write.
    /// </summary>
    App = Native.Process.SDL_ProcessIO.SDL_PROCESS_STDIO_APP,

    /// <summary>
    /// The I/O stream is redirected to an existing IOStream.
    /// </summary>
    Redirect = Native.Process.SDL_ProcessIO.SDL_PROCESS_STDIO_REDIRECT
}
