namespace Sdl3Sharp.AsyncIO;

/// <summary>
/// Possible outcomes of an asynchronous I/O task.
/// </summary>
public enum AsyncIOResult
{
    /// <summary>
    /// Request was completed without error.
    /// </summary>
    Complete,

    /// <summary>
    /// Request failed for some reason.
    /// </summary>
    Failure,

    /// <summary>
    /// Request was canceled before completing.
    /// </summary>
    Canceled
}
