using System;
using System.Collections.Generic;

namespace SdlSharp.Touch
{
    /// <summary>
    /// A device that supports touch input.
    /// </summary>
    public unsafe sealed class TouchDevice : NativeStaticIndexBase<Native.SDL_TouchID, TouchDevice>
    {
        private static ItemCollection<TouchDevice>? s_devices;

        private ItemCollection<Finger>? _fingers;

        /// <summary>
        /// The touch devices in the system.
        /// </summary>
        public static IReadOnlyList<TouchDevice> Devices => s_devices ?? (s_devices = new ItemCollection<TouchDevice>(
            index => IndexToInstance(Native.SDL_GetTouchDevice(index)),
            Native.SDL_GetNumTouchDevices));

        /// <summary>
        /// The active fingers on the device.
        /// </summary>
        public IReadOnlyList<Finger> Fingers => _fingers ?? (_fingers = new ItemCollection<Finger>(
            index => Finger.PointerToInstanceNotNull(Native.SDL_GetTouchFinger(Index, index)),
            () => Native.SDL_GetNumTouchFingers(Index)));

        /// <summary>
        /// An event fired when a finger is put down.
        /// </summary>
        public event EventHandler<TouchFingerEventArgs>? FingerDown;

        /// <summary>
        /// An event fired when a finger is pulled up.
        /// </summary>
        public event EventHandler<TouchFingerEventArgs>? FingerUp;

        /// <summary>
        /// An event fired when a finger is moved.
        /// </summary>
        public event EventHandler<TouchFingerEventArgs>? FingerMotion;

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch (e.Type)
            {
                case Native.SDL_EventType.FingerDown:
                case Native.SDL_EventType.FingerUp:
                case Native.SDL_EventType.FingerMotion:
                    {
                        var touch = IndexToInstance(e.Tfinger.TouchId);
                        Finger? finger = null;
                        foreach (var indexedFinger in touch.Fingers)
                        {
                            if (indexedFinger.Id.Id == e.Tfinger.FingerId.Id)
                            {
                                finger = indexedFinger;
                                break;
                            }
                        }

                        if (finger == null)
                        {
                            throw new InvalidOperationException();
                        }

                        switch (e.Type)
                        {
                            case Native.SDL_EventType.FingerDown:
                                touch.FingerDown?.Invoke(touch, new TouchFingerEventArgs(e.Tfinger, finger));
                                break;

                            case Native.SDL_EventType.FingerUp:
                                touch.FingerUp?.Invoke(touch, new TouchFingerEventArgs(e.Tfinger, finger));
                                break;

                            case Native.SDL_EventType.FingerMotion:
                                touch.FingerMotion?.Invoke(touch, new TouchFingerEventArgs(e.Tfinger, finger));
                                break;
                        }
                        break;
                    }

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
