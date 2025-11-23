namespace Sdl3Sharp.AsyncIO;

/// <summary>
/// Information about a completed asynchronous I/O request.
/// </summary>
/// <param name="TaskType">The type of task that was performed.</param>
/// <param name="Result">The result of the operation.</param>
/// <param name="Offset">The offset in the file where data was read/written.</param>
/// <param name="BytesRequested">The number of bytes the task was requested to read/write.</param>
/// <param name="BytesTransferred">The actual number of bytes that were read/written.</param>
/// <param name="UserData">The user-provided data associated with this task.</param>
public readonly record struct AsyncIOOutcome(
    AsyncIOTaskType TaskType,
    AsyncIOResult Result,
    ulong Offset,
    ulong BytesRequested,
    ulong BytesTransferred,
    nuint UserData);
