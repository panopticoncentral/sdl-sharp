namespace SdlSharp;

/// <summary>
/// Possible outcomes from a triggered SDL assertion.
/// </summary>
public enum AssertState
{
    /// <summary>Retry the assert immediately.</summary>
    Retry = (int)Native.SDL_AssertState.SDL_ASSERTION_RETRY,

    /// <summary>Make the debugger trigger a breakpoint.</summary>
    Break = (int)Native.SDL_AssertState.SDL_ASSERTION_BREAK,

    /// <summary>Terminate the program.</summary>
    Abort = (int)Native.SDL_AssertState.SDL_ASSERTION_ABORT,

    /// <summary>Ignore the assert.</summary>
    Ignore = (int)Native.SDL_AssertState.SDL_ASSERTION_IGNORE,

    /// <summary>Ignore the assert from now on.</summary>
    AlwaysIgnore = (int)Native.SDL_AssertState.SDL_ASSERTION_ALWAYS_IGNORE,
}
