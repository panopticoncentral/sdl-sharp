namespace SdlSharp.Graphics;

/// <summary>
/// Result from a file dialog operation.
/// </summary>
/// <param name="Files">Selected file paths, or null if cancelled.</param>
/// <param name="FilterIndex">Index of the filter the user selected.</param>
public readonly record struct DialogResult(string[]? Files, int FilterIndex)
{
    /// <summary>The native or callback error, if the dialog failed.</summary>
    public Exception? Error { get; init; }
    /// <summary>Whether the user dismissed the dialog without selecting a file.</summary>
    public bool IsCanceled => Error == null && (Files == null || Files.Length == 0);
}
