using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// Pen axis indices.
/// </summary>
public enum SDL_PenAxis
{
    SDL_PEN_AXIS_PRESSURE,
    SDL_PEN_AXIS_XTILT,
    SDL_PEN_AXIS_YTILT,
    SDL_PEN_AXIS_DISTANCE,
    SDL_PEN_AXIS_ROTATION,
    SDL_PEN_AXIS_SLIDER,
    SDL_PEN_AXIS_TANGENTIAL_PRESSURE,
    SDL_PEN_AXIS_COUNT,
}

/// <summary>
/// An enum that describes the type of a pen device.
/// </summary>
public enum SDL_PenDeviceType
{
    SDL_PEN_DEVICE_TYPE_INVALID = -1,
    SDL_PEN_DEVICE_TYPE_UNKNOWN,
    SDL_PEN_DEVICE_TYPE_DIRECT,
    SDL_PEN_DEVICE_TYPE_INDIRECT,
}

/// <summary>
/// Native bindings for SDL_pen.h.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Pen
{
    public const uint SDL_PEN_INPUT_DOWN = 1u << 0;
    public const uint SDL_PEN_INPUT_BUTTON_1 = 1u << 1;
    public const uint SDL_PEN_INPUT_BUTTON_2 = 1u << 2;
    public const uint SDL_PEN_INPUT_BUTTON_3 = 1u << 3;
    public const uint SDL_PEN_INPUT_BUTTON_4 = 1u << 4;
    public const uint SDL_PEN_INPUT_BUTTON_5 = 1u << 5;
    public const uint SDL_PEN_INPUT_ERASER_TIP = 1u << 30;
    public const uint SDL_PEN_INPUT_IN_PROXIMITY = 1u << 31;

    public const ulong SDL_PEN_TOUCHID = unchecked((ulong)-2);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetPenDeviceType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PenDeviceType SDL_GetPenDeviceType(uint instance_id);
}
