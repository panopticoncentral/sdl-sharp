// src/SdlSharp/Input/TouchDevice.cs
using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Touch;

namespace SdlSharp.Input;

/// <summary>
/// Static methods for querying touch devices.
/// </summary>
public static unsafe class TouchDevice
{
    /// <summary>
    /// The mouse instance ID carried by mouse events that SDL synthesized from touch input.
    /// </summary>
    public const uint MouseId = uint.MaxValue;

    /// <summary>
    /// The touch device ID carried by touch events that SDL synthesized from mouse input.
    /// </summary>
    public const ulong MouseTouchDeviceId = ulong.MaxValue;

    /// <summary>Gets the IDs of all connected touch devices.</summary>
    /// <exception cref="SdlException">The touch device list could not be retrieved.</exception>
    public static ulong[] GetDevices()
    {
        int count;
        var ids = SDL_GetTouchDevices(&count);
        if (ids == null) throw new SdlException();
        try
        {
            var result = new ulong[count];
            for (var i = 0; i < count; i++)
                result[i] = ids[i];
            return result;
        }
        finally
        {
            SDL_free(ids);
        }
    }

    /// <summary>Gets the name of a touch device.</summary>
    public static string? GetName(ulong id) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetTouchDeviceName(id));

    /// <summary>Gets the type of a touch device.</summary>
    public static TouchDeviceType GetType(ulong id) =>
        (TouchDeviceType)SDL_GetTouchDeviceType(id);

    /// <summary>Gets all active fingers on a touch device.</summary>
    /// <exception cref="SdlException">The finger list could not be retrieved.</exception>
    public static Finger[] GetFingers(ulong id)
    {
        int count;
        var fingers = SDL_GetTouchFingers(id, &count);
        if (fingers == null) throw new SdlException();
        try
        {
            var result = new Finger[count];
            for (var i = 0; i < count; i++)
            {
                var f = fingers[i];
                result[i] = new Finger((long)f->id, f->x, f->y, f->pressure);
            }
            return result;
        }
        finally
        {
            SDL_free(fingers);
        }
    }
}
