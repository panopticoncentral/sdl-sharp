namespace Sdl3Sharp.AsyncIO;

/// <summary>
/// Types of asynchronous I/O tasks.
/// </summary>
public enum AsyncIOTaskType
{
    /// <summary>
    /// A read operation.
    /// </summary>
    Read,

    /// <summary>
    /// A write operation.
    /// </summary>
    Write,

    /// <summary>
    /// A close operation.
    /// </summary>
    Close
}
