// src/SdlSharp/Input/TouchDeviceType.cs
namespace SdlSharp.Input;

/// <summary>
/// Types of touch devices.
/// </summary>
public enum TouchDeviceType
{
    Invalid = (int)Native.SDL_TouchDeviceType.SDL_TOUCH_DEVICE_INVALID,
    Direct = (int)Native.SDL_TouchDeviceType.SDL_TOUCH_DEVICE_DIRECT,
    IndirectAbsolute = (int)Native.SDL_TouchDeviceType.SDL_TOUCH_DEVICE_INDIRECT_ABSOLUTE,
    IndirectRelative = (int)Native.SDL_TouchDeviceType.SDL_TOUCH_DEVICE_INDIRECT_RELATIVE,
}
