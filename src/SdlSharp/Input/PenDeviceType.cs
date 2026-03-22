// src/SdlSharp/Input/PenDeviceType.cs
namespace SdlSharp.Input;

/// <summary>
/// Types of pen devices.
/// </summary>
public enum PenDeviceType
{
    Invalid = (int)Native.SDL_PenDeviceType.SDL_PEN_DEVICE_TYPE_INVALID,
    Unknown = (int)Native.SDL_PenDeviceType.SDL_PEN_DEVICE_TYPE_UNKNOWN,
    Direct = (int)Native.SDL_PenDeviceType.SDL_PEN_DEVICE_TYPE_DIRECT,
    Indirect = (int)Native.SDL_PenDeviceType.SDL_PEN_DEVICE_TYPE_INDIRECT,
}
