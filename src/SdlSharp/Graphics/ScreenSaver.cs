using static SdlSharp.Native.Common;
using static SdlSharp.Native.Video;

namespace SdlSharp.Graphics;

/// <summary>
/// Controls whether the operating system's screen saver may activate while the
/// application is running. SDL disables it by default while video is initialized.
/// </summary>
public static class ScreenSaver
{
    /// <summary>
    /// Gets or sets whether the screen saver is allowed to activate.
    /// </summary>
    public static bool Enabled
    {
        get => SDL_ScreenSaverEnabled();
        set => Check(value ? SDL_EnableScreenSaver() : SDL_DisableScreenSaver());
    }
}
