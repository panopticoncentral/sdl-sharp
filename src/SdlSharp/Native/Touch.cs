using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// An enum that describes the type of a touch device.
/// </summary>
public enum SDL_TouchDeviceType
{
    SDL_TOUCH_DEVICE_INVALID = -1,
    SDL_TOUCH_DEVICE_DIRECT,
    SDL_TOUCH_DEVICE_INDIRECT_ABSOLUTE,
    SDL_TOUCH_DEVICE_INDIRECT_RELATIVE,
}

/// <summary>
/// Data about a single finger in a multitouch event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_Finger
{
    public ulong id;
    public float x;
    public float y;
    public float pressure;
}

/// <summary>
/// Native bindings for SDL_touch.h.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Touch
{
    public const ulong SDL_MOUSE_TOUCHID = unchecked((ulong)-1);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTouchDevices")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial ulong* SDL_GetTouchDevices(int* count);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTouchDeviceName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetTouchDeviceName(ulong touchID);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTouchDeviceType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_TouchDeviceType SDL_GetTouchDeviceType(ulong touchID);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTouchFingers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Finger** SDL_GetTouchFingers(ulong touchID, int* count);
}
