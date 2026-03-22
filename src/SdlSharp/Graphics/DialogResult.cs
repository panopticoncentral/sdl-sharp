namespace SdlSharp.Graphics;

/// <summary>
/// Result from a file dialog operation.
/// </summary>
/// <param name="Files">Selected file paths, or null if cancelled.</param>
/// <param name="FilterIndex">Index of the filter the user selected.</param>
public readonly record struct DialogResult(string[]? Files, int FilterIndex);
