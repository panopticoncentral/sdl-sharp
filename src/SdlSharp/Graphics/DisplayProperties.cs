using static SdlSharp.Native.Video;

namespace SdlSharp.Graphics;

/// <summary>
/// Well-known property names for <see cref="Display.Properties"/>.
/// </summary>
public static class DisplayProperties
{
    /// <summary>True if the display has HDR headroom above the SDR white point. Informational/diagnostic only.</summary>
    public const string HdrEnabled = SDL_PROP_DISPLAY_HDR_ENABLED_BOOLEAN;

    /// <summary>(KMS/DRM) The panel orientation for the display, in degrees of clockwise rotation.</summary>
    public const string KmsdrmPanelOrientation = SDL_PROP_DISPLAY_KMSDRM_PANEL_ORIENTATION_NUMBER;

    /// <summary>(Wayland) The wl_output associated with the display.</summary>
    public const string WaylandWlOutput = SDL_PROP_DISPLAY_WAYLAND_WL_OUTPUT_POINTER;

    /// <summary>(Windows) The monitor handle (HMONITOR) associated with the display.</summary>
    public const string WindowsHmonitor = SDL_PROP_DISPLAY_WINDOWS_HMONITOR_POINTER;
}
