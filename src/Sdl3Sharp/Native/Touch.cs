using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Mouse;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_touch.h - Touch input and gesture management.
/// </summary>
public static unsafe partial class Touch
{
    /// <summary>
    /// A unique ID for a touch device.
    /// This ID is valid for the time the device is connected to the system,
    /// and is never reused for the lifetime of the application.
    /// The value 0 is an invalid ID.
    /// </summary>
    /// <param name="value">The touch device ID value.</param>
    public readonly struct SDL_TouchID(ulong value)
    {
        /// <summary>The underlying touch device ID value.</summary>
        public readonly ulong Value = value;

        /// <summary>Implicitly converts an SDL_TouchID to ulong.</summary>
        /// <param name="id">The touch device ID to convert.</param>
        public static implicit operator ulong(SDL_TouchID id)
        {
            return id.Value;
        }

        /// <summary>Implicitly converts a ulong to SDL_TouchID.</summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator SDL_TouchID(ulong value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// A unique ID for a single finger on a touch device.
    /// This ID is valid for the time the finger (stylus, etc) is touching and will
    /// be unique for all fingers currently in contact, so this ID tracks the
    /// lifetime of a single continuous touch. This value may represent an index, a
    /// pointer, or some other unique ID, depending on the platform.
    /// The value 0 is an invalid ID.
    /// </summary>
    /// <param name="value">The finger ID value.</param>
    public readonly struct SDL_FingerID(ulong value)
    {
        /// <summary>The underlying finger ID value.</summary>
        public readonly ulong Value = value;

        /// <summary>Implicitly converts an SDL_FingerID to ulong.</summary>
        /// <param name="id">The finger ID to convert.</param>
        public static implicit operator ulong(SDL_FingerID id)
        {
            return id.Value;
        }

        /// <summary>Implicitly converts a ulong to SDL_FingerID.</summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator SDL_FingerID(ulong value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// An enum that describes the type of a touch device.
    /// </summary>
    public enum SDL_TouchDeviceType
    {
        /// <summary>Invalid touch device type.</summary>
        SDL_TOUCH_DEVICE_INVALID = -1,
        /// <summary>Touch screen with window-relative coordinates.</summary>
        SDL_TOUCH_DEVICE_DIRECT,
        /// <summary>Trackpad with absolute device coordinates.</summary>
        SDL_TOUCH_DEVICE_INDIRECT_ABSOLUTE,
        /// <summary>Trackpad with screen cursor-relative coordinates.</summary>
        SDL_TOUCH_DEVICE_INDIRECT_RELATIVE
    }

    /// <summary>
    /// Data about a single finger in a multitouch event.
    /// Each touch event is a collection of fingers that are simultaneously in
    /// contact with the touch device (so a "touch" can be a "multitouch," in
    /// reality), and this struct reports details of the specific fingers.
    /// </summary>
    public struct SDL_Finger
    {
        /// <summary>The finger ID.</summary>
        public SDL_FingerID id;
        /// <summary>The x-axis location of the touch event, normalized (0...1).</summary>
        public float x;
        /// <summary>The y-axis location of the touch event, normalized (0...1).</summary>
        public float y;
        /// <summary>The quantity of pressure applied, normalized (0...1).</summary>
        public float pressure;
    }

    /// <summary>
    /// The SDL_MouseID for mouse events simulated with touch input.
    /// </summary>
    public static readonly SDL_MouseID SDL_TOUCH_MOUSEID = unchecked((uint)-1);

    /// <summary>
    /// The SDL_TouchID for touch events simulated with mouse input.
    /// </summary>
    public static readonly SDL_TouchID SDL_MOUSE_TOUCHID = unchecked((ulong)-1);

    /// <summary>
    /// Get a list of registered touch devices.
    /// On some platforms SDL first sees the touch device if it was actually used.
    /// Therefore the returned list might be empty, although devices are available.
    /// After using all devices at least once the number will be correct.
    /// </summary>
    /// <param name="count">A pointer filled in with the number of devices returned, may be NULL.</param>
    /// <returns>A 0 terminated array of touch device IDs or NULL on failure; call SDL_GetError() for more information. This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_TouchID* SDL_GetTouchDevices(int* count);

    /// <summary>
    /// Get the touch device name as reported from the driver.
    /// </summary>
    /// <param name="touchID">The touch device instance ID.</param>
    /// <returns>Touch device name, or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetTouchDeviceName(SDL_TouchID touchID);

    /// <summary>
    /// Get the type of the given touch device.
    /// </summary>
    /// <param name="touchID">The ID of a touch device.</param>
    /// <returns>Touch device type.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_TouchDeviceType SDL_GetTouchDeviceType(SDL_TouchID touchID);

    /// <summary>
    /// Get a list of active fingers for a given touch device.
    /// </summary>
    /// <param name="touchID">The ID of a touch device.</param>
    /// <param name="count">A pointer filled in with the number of fingers returned, can be NULL.</param>
    /// <returns>A NULL terminated array of SDL_Finger pointers or NULL on failure; call SDL_GetError() for more information. This is a single allocation that should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Finger** SDL_GetTouchFingers(SDL_TouchID touchID, int* count);
}
