using System.Runtime.InteropServices;

using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Video;

namespace SdlSharp.Graphics;

/// <summary>
/// Represents an SDL display.
/// </summary>
public readonly record struct Display(uint Id)
{
    /// <summary>
    /// Gets all currently connected displays.
    /// </summary>
    /// <returns>An array of displays.</returns>
    public static unsafe Display[] GetAll()
    {
        var ptr = SDL_GetDisplays(out var count);
        if (ptr == null)
        {
            throw new SdlException();
        }

        try
        {
            var displays = new Display[count];
            for (var i = 0; i < count; i++)
            {
                displays[i] = new Display(ptr[i].Value);
            }

            return displays;
        }
        finally
        {
            SDL_free(ptr);
        }
    }

    /// <summary>
    /// Gets the primary display.
    /// </summary>
    public static Display Primary
    {
        get
        {
            var id = SDL_GetPrimaryDisplay();
            return new Display(CheckId(id.Value));
        }
    }

    /// <summary>
    /// Gets the name of this display.
    /// </summary>
    public unsafe string? Name =>
        Marshal.PtrToStringUTF8((nint)SDL_GetDisplayName(new SDL_DisplayID(Id)));

    /// <summary>
    /// Gets the desktop area represented by this display.
    /// </summary>
    public Rectangle Bounds
    {
        get
        {
            Check(SDL_GetDisplayBounds(new SDL_DisplayID(Id), out var rect));
            return Rectangle.FromNative(rect);
        }
    }

    /// <summary>
    /// Gets the usable desktop area represented by this display.
    /// </summary>
    public Rectangle UsableBounds
    {
        get
        {
            Check(SDL_GetDisplayUsableBounds(new SDL_DisplayID(Id), out var rect));
            return Rectangle.FromNative(rect);
        }
    }

    /// <summary>
    /// Gets the content scale of this display.
    /// </summary>
    public float ContentScale => SDL_GetDisplayContentScale(new SDL_DisplayID(Id));

    /// <summary>
    /// Gets information about the desktop's display mode.
    /// </summary>
    public unsafe DisplayMode DesktopDisplayMode
    {
        get
        {
            var ptr = Check(SDL_GetDesktopDisplayMode(new SDL_DisplayID(Id)));
            return DisplayMode.FromNative(ptr);
        }
    }

    /// <summary>
    /// Gets information about the current display mode.
    /// </summary>
    public unsafe DisplayMode CurrentDisplayMode
    {
        get
        {
            var ptr = Check(SDL_GetCurrentDisplayMode(new SDL_DisplayID(Id)));
            return DisplayMode.FromNative(ptr);
        }
    }
}
