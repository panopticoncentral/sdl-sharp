namespace Sdl3Sharp;

/// <summary>
/// Specifies the type of file dialog to display.
/// </summary>
public enum FileDialogType
{
    /// <summary>
    /// A dialog for opening an existing file.
    /// </summary>
    OpenFile,

    /// <summary>
    /// A dialog for saving a file (may or may not already exist).
    /// </summary>
    SaveFile,

    /// <summary>
    /// A dialog for selecting a folder.
    /// </summary>
    OpenFolder
}
