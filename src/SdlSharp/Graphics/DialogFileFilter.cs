namespace SdlSharp.Graphics;

/// <summary>
/// A file filter for dialog operations.
/// </summary>
/// <param name="Name">Human-readable label (e.g. "Image files").</param>
/// <param name="Pattern">Semicolon-separated extensions (e.g. "png;jpg;bmp").</param>
public readonly record struct DialogFileFilter(string Name, string Pattern);
