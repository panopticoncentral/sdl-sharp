namespace SdlSharp.Gui;

/// <summary>
/// Condition for SetNextWindow*, SetWindow*, SetNextItem* functions.
/// Treat as a regular enum — do NOT combine multiple values using binary operators.
/// </summary>
public enum Cond
{
    /// <summary>No condition (always set the variable), same as <see cref="Always"/>.</summary>
    None = 0,
    /// <summary>No condition (always set the variable), same as <see cref="None"/>.</summary>
    Always = 1 << 0,
    /// <summary>Set the variable once per runtime session (only the first call will succeed).</summary>
    Once = 1 << 1,
    /// <summary>Set the variable if the object/window has no persistently saved data (no entry in .ini file).</summary>
    FirstUseEver = 1 << 2,
    /// <summary>Set the variable if the object/window is appearing after being hidden/inactive.</summary>
    Appearing = 1 << 3,
}
