// src/SdlSharp/Input/PenDevice.cs
using static SdlSharp.Native.Pen;

namespace SdlSharp.Input;

/// <summary>
/// Static methods for querying pen devices.
/// </summary>
public static class PenDevice
{
    /// <summary>Gets the type of a pen device.</summary>
    public static PenDeviceType GetType(uint id) =>
        (PenDeviceType)SDL_GetPenDeviceType(id);
}
