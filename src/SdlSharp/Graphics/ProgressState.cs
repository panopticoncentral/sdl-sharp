namespace SdlSharp.Graphics;

/// <summary>
/// The progress state shown on a window's taskbar icon.
/// </summary>
public enum ProgressState
{
    /// <summary>An invalid progress state indicating an error; check the last SDL error.</summary>
    Invalid = (int)Native.SDL_ProgressState.SDL_PROGRESS_STATE_INVALID,
    /// <summary>No progress bar is shown.</summary>
    None = (int)Native.SDL_ProgressState.SDL_PROGRESS_STATE_NONE,
    /// <summary>The progress bar is shown in an indeterminate state.</summary>
    Indeterminate = (int)Native.SDL_ProgressState.SDL_PROGRESS_STATE_INDETERMINATE,
    /// <summary>The progress bar is shown in a normal state.</summary>
    Normal = (int)Native.SDL_ProgressState.SDL_PROGRESS_STATE_NORMAL,
    /// <summary>The progress bar is shown in a paused state.</summary>
    Paused = (int)Native.SDL_ProgressState.SDL_PROGRESS_STATE_PAUSED,
    /// <summary>The progress bar is shown in a state indicating the application had an error.</summary>
    Error = (int)Native.SDL_ProgressState.SDL_PROGRESS_STATE_ERROR,
}
