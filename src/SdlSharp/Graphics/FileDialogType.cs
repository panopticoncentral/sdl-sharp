namespace SdlSharp.Graphics;

/// <summary>
/// File dialog type.
/// </summary>
public enum FileDialogType
{
    /// <summary>Dialog for opening one or more files.</summary>
    OpenFile = (int)Native.SDL_FileDialogType.SDL_FILEDIALOG_OPENFILE,
    /// <summary>Dialog for choosing a save file location.</summary>
    SaveFile = (int)Native.SDL_FileDialogType.SDL_FILEDIALOG_SAVEFILE,
    /// <summary>Dialog for opening a folder.</summary>
    OpenFolder = (int)Native.SDL_FileDialogType.SDL_FILEDIALOG_OPENFOLDER,
}
