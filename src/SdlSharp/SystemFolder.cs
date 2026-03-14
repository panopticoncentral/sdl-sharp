namespace SdlSharp;

/// <summary>
/// Known system folder types.
/// </summary>
public enum SystemFolder
{
    /// <summary>The user's home directory.</summary>
    Home = (int)Native.SDL_Folder.SDL_FOLDER_HOME,
    /// <summary>The desktop folder.</summary>
    Desktop = (int)Native.SDL_Folder.SDL_FOLDER_DESKTOP,
    /// <summary>User document files.</summary>
    Documents = (int)Native.SDL_Folder.SDL_FOLDER_DOCUMENTS,
    /// <summary>Standard folder for downloads.</summary>
    Downloads = (int)Native.SDL_Folder.SDL_FOLDER_DOWNLOADS,
    /// <summary>Music files.</summary>
    Music = (int)Native.SDL_Folder.SDL_FOLDER_MUSIC,
    /// <summary>Image files.</summary>
    Pictures = (int)Native.SDL_Folder.SDL_FOLDER_PICTURES,
    /// <summary>Files shared with other users.</summary>
    PublicShare = (int)Native.SDL_Folder.SDL_FOLDER_PUBLICSHARE,
    /// <summary>Save files for games.</summary>
    SavedGames = (int)Native.SDL_Folder.SDL_FOLDER_SAVEDGAMES,
    /// <summary>Application screenshots.</summary>
    Screenshots = (int)Native.SDL_Folder.SDL_FOLDER_SCREENSHOTS,
    /// <summary>Template files.</summary>
    Templates = (int)Native.SDL_Folder.SDL_FOLDER_TEMPLATES,
    /// <summary>Video files.</summary>
    Videos = (int)Native.SDL_Folder.SDL_FOLDER_VIDEOS,
}
