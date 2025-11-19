namespace Sdl3Sharp;

/// <summary>
/// Represents the status of an SDL I/O stream operation.
/// </summary>
public enum IOStatus
{
    /// <summary>Everything is ready (no errors and not EOF).</summary>
    Ready = 0,

    /// <summary>Read or write I/O error.</summary>
    Error = 1,

    /// <summary>End of file.</summary>
    EndOfFile = 2,

    /// <summary>Non-blocking I/O, not ready.</summary>
    NotReady = 3,

    /// <summary>Tried to write a read-only buffer.</summary>
    ReadOnly = 4,

    /// <summary>Tried to read a write-only buffer.</summary>
    WriteOnly = 5
}
