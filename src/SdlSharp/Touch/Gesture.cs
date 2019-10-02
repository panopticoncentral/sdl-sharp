using System;

namespace SdlSharp.Touch
{
    /// <summary>
    /// A touch gesture.
    /// </summary>
    public unsafe sealed class Gesture : NativeStaticIndexBase<Native.SDL_GestureID, Gesture>
    {
        /// <summary>
        /// An event fired when a multi-finger gesture was made.
        /// </summary>
        public static event EventHandler<MultiGestureEventArgs>? MultiGesture;

        /// <summary>
        /// An event fired when a dollar gesture was made.
        /// </summary>
        public static event EventHandler<DollarGestureEventArgs>? DollarGesture;

        /// <summary>
        /// An event fired when a dollar gesture was recorded.
        /// </summary>
        public static event EventHandler<DollarGestureEventArgs>? DollarRecord;

        /// <summary>
        /// Records a gesture on a touch device.
        /// </summary>
        /// <param name="device">The device. If <c>null</c>, then records on all devices.</param>
        /// <returns><c>true</c> if the recording started, <c>false</c> otherwise.</returns>
        public static bool RecordGesture(TouchDevice? device) =>
            Native.SDL_RecordGesture(device == null ? new Native.SDL_TouchID(-1) : device.Index);

        /// <summary>
        /// Loads a dollar template.
        /// </summary>
        /// <param name="device">The touch device.</param>
        /// <param name="rwops">The storage.</param>
        public static void LoadDollarTemplates(TouchDevice device, RWOps rwops) =>
            Native.CheckError(Native.SDL_LoadDollarTemplates(device.Index, rwops.Pointer));

        /// <summary>
        /// Save all the dollar templates.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        public static void SaveAllDollarTemplates(RWOps rwops) =>
            Native.CheckError(Native.SDL_SaveAllDollarTemplates(rwops.Pointer));

        /// <summary>
        /// Save a dollar template.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        public void SaveDollarTemplate(RWOps rwops) =>
            Native.CheckError(Native.SDL_SaveDollarTemplate(Index, rwops.Pointer));

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch (e.Type)
            {
                case Native.SDL_EventType.MultiGesture:
                    {
                        MultiGesture?.Invoke(null, new MultiGestureEventArgs(e.Mgesture));
                        break;
                    }

                case Native.SDL_EventType.DollarGesture:
                    {
                        DollarGesture?.Invoke(null, new DollarGestureEventArgs(e.Dgesture));
                        break;
                    }

                case Native.SDL_EventType.DollarRecord:
                    {
                        DollarRecord?.Invoke(null, new DollarGestureEventArgs(e.Dgesture));
                        break;
                    }

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
