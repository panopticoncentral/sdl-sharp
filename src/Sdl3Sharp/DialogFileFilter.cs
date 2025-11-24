namespace Sdl3Sharp;

/// <summary>
/// Represents a filter entry for file dialogs.
/// </summary>
/// <param name="Name">A user-readable label for the filter (for example, "Office document").</param>
/// <param name="Pattern">A semicolon-separated list of file extensions (for example, "doc;docx").
/// File extensions may only contain alphanumeric characters, hyphens, underscores and periods.
/// Alternatively, the whole string can be a single asterisk ("*"), which serves as an "All files" filter.</param>
public readonly record struct DialogFileFilter(string Name, string Pattern);
