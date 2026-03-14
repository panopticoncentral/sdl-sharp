namespace SdlSharp;

/// <summary>
/// SDL log priority levels.
/// </summary>
public enum LogPriority
{
    /// <summary>Trace priority.</summary>
    Trace = (int)Native.SDL_LogPriority.SDL_LOG_PRIORITY_TRACE,
    /// <summary>Verbose priority.</summary>
    Verbose = (int)Native.SDL_LogPriority.SDL_LOG_PRIORITY_VERBOSE,
    /// <summary>Debug priority.</summary>
    Debug = (int)Native.SDL_LogPriority.SDL_LOG_PRIORITY_DEBUG,
    /// <summary>Info priority.</summary>
    Info = (int)Native.SDL_LogPriority.SDL_LOG_PRIORITY_INFO,
    /// <summary>Warning priority.</summary>
    Warn = (int)Native.SDL_LogPriority.SDL_LOG_PRIORITY_WARN,
    /// <summary>Error priority.</summary>
    Error = (int)Native.SDL_LogPriority.SDL_LOG_PRIORITY_ERROR,
    /// <summary>Critical priority.</summary>
    Critical = (int)Native.SDL_LogPriority.SDL_LOG_PRIORITY_CRITICAL,
}
