namespace Sdl3Sharp;

/// <summary>
/// The type of the OS-provided default folder for a specific purpose.
/// </summary>
public enum Folder
{
    /// <summary>The folder which contains all of the current user's data, preferences, and documents. It usually contains most of the other folders. If a requested folder does not exist, the home folder can be considered a safe fallback to store a user's documents.</summary>
    Home,
    /// <summary>The folder of files that are displayed on the desktop. Note that the existence of a desktop folder does not guarantee that the system does show icons on its desktop; certain GNU/Linux distros with a graphical environment may not have desktop icons.</summary>
    Desktop,
    /// <summary>User document files, possibly application-specific. This is a good place to save a user's projects.</summary>
    Documents,
    /// <summary>Standard folder for user files downloaded from the internet.</summary>
    Downloads,
    /// <summary>Music files that can be played using a standard music player (mp3, ogg...).</summary>
    Music,
    /// <summary>Image files that can be displayed using a standard viewer (png, jpg...).</summary>
    Pictures,
    /// <summary>Files that are meant to be shared with other users on the same computer.</summary>
    PublicShare,
    /// <summary>Save files for games.</summary>
    SavedGames,
    /// <summary>Application screenshots.</summary>
    Screenshots,
    /// <summary>Template files to be used when the user requests the desktop environment to create a new file in a certain folder, such as "New Text File.txt". Any file in the Templates folder can be used as a starting point for a new file.</summary>
    Templates,
    /// <summary>Video files that can be played using a standard video player (mp4, webm...).</summary>
    Videos
}
