namespace SdlSharp.Touch
{
    /// <summary>
    /// A device that supports touch input.
    /// </summary>
    public sealed unsafe class TouchDevice : NativeStaticIndexBase<Native.SDL_TouchID, TouchDevice>
    {
        private static ItemCollection<TouchDevice>? s_devices;

        private ItemCollection<Finger>? _fingers;

        /// <summary>
        /// The touch devices in the system.
        /// </summary>
        public static IReadOnlyList<TouchDevice> Devices => s_devices ??= new ItemCollection<TouchDevice>(
            index => IndexToInstance(Native.SDL_GetTouchDevice(index)),
            Native.SDL_GetNumTouchDevices);

        /// <summary>
        /// The active fingers on the device.
        /// </summary>
        public IReadOnlyList<Finger> Fingers => _fingers ??= new ItemCollection<Finger>(
            index => Finger.PointerToInstanceNotNull(Native.SDL_GetTouchFinger(Index, index)),
            () => Native.SDL_GetNumTouchFingers(Index));


        /// <summary>
        /// The device type.
        /// </summary>
        public TouchDeviceType DeviceType => Native.SDL_GetTouchDeviceType(Index);

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
            switch ((Native.SDL_EventType)e.type)
            {
                case Native.SDL_EventType.SDL_FINGERDOWN:
                case Native.SDL_EventType.SDL_FINGERUP:
                case Native.SDL_EventType.SDL_FINGERMOTION:
                    {
                        var touch = IndexToInstance(e.tfinger.touchId);
                        Finger? finger = null;
                        foreach (var indexedFinger in touch.Fingers)
                        {
                            if (indexedFinger.Id.Id == e.tfinger.fingerId.Id)
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
                                touch.FingerDown?.Invoke(touch, new TouchFingerEventArgs(e.tfinger, finger));
                                break;

                            case Native.SDL_EventType.SDL_FINGERUP:
                                touch.FingerUp?.Invoke(touch, new TouchFingerEventArgs(e.tfinger, finger));
                                break;

                            case Native.SDL_EventType.SDL_FINGERMOTION:
                                touch.FingerMotion?.Invoke(touch, new TouchFingerEventArgs(e.tfinger, finger));
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
