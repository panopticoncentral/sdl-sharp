namespace SdlSharp.Input
{
    /// <summary>
    /// A device that supports touch input.
    /// </summary>
    public sealed unsafe class TouchDevice
    {
        private readonly Native.SDL_TouchID _touch;

        /// <summary>
        /// The ID of the touch device;
        /// </summary>
        public long Id => _touch.Value;

        /// <summary>
        /// The touch devices in the system.
        /// </summary>
        public static IReadOnlyList<(string? Name, TouchDevice Device)> Devices => Native.GetIndexedCollection(i =>
            (Native.Utf8ToString(Native.SDL_GetTouchName(i)),
            new TouchDevice(Native.SDL_GetTouchDevice(i))), Native.SDL_GetNumTouchDevices);

        /// <summary>
        /// The active fingers on the device.
        /// </summary>
        public IReadOnlyList<Finger> Fingers => Native.GetIndexedCollection(i =>
            new Finger(Native.SDL_GetTouchFinger(_touch, i)), () => Native.SDL_GetNumTouchFingers(_touch));

        /// <summary>
        /// The device type.
        /// </summary>
        public TouchDeviceType DeviceType => (TouchDeviceType)Native.SDL_GetTouchDeviceType(_touch);

        /// <summary>
        /// An event fired when a finger is put down.
        /// </summary>
        public static event EventHandler<TouchFingerEventArgs>? FingerDown;

        /// <summary>
        /// An event fired when a finger is pulled up.
        /// </summary>
        public static event EventHandler<TouchFingerEventArgs>? FingerUp;

        /// <summary>
        /// An event fired when a finger is moved.
        /// </summary>
        public static event EventHandler<TouchFingerEventArgs>? FingerMotion;

        internal TouchDevice(Native.SDL_TouchID touch)
        {
            _touch = touch;
        }

        internal Native.SDL_TouchID ToNative() => _touch;

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch ((Native.SDL_EventType)e.type)
            {
                case Native.SDL_EventType.SDL_FINGERDOWN:
                case Native.SDL_EventType.SDL_FINGERUP:
                case Native.SDL_EventType.SDL_FINGERMOTION:
                    {
                        var touch = new TouchDevice(e.tfinger.touchId);
                        Finger? finger = null;
                        foreach (var indexedFinger in touch.Fingers)
                        {
                            if (indexedFinger.ToNative()->id == e.tfinger.fingerId)
                            {
                                finger = indexedFinger;
                                break;
                            }
                        }

                        if (finger == null)
                        {
                            throw new InvalidOperationException();
                        }

                        switch ((Native.SDL_EventType)e.type)
                        {
                            case Native.SDL_EventType.SDL_FINGERDOWN:
                                FingerDown?.Invoke(touch, new TouchFingerEventArgs(e.tfinger, finger.Value));
                                break;

                            case Native.SDL_EventType.SDL_FINGERUP:
                                FingerUp?.Invoke(touch, new TouchFingerEventArgs(e.tfinger, finger.Value));
                                break;

                            case Native.SDL_EventType.SDL_FINGERMOTION:
                                FingerMotion?.Invoke(touch, new TouchFingerEventArgs(e.tfinger, finger.Value));
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
