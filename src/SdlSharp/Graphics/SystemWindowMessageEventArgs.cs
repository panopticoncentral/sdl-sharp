using System;
using System.Runtime.InteropServices;

namespace SdlSharp.Graphics
{
    /// <summary>
    /// Event arguments for a system window message event.
    /// </summary>
    public unsafe sealed class SystemWindowMessageEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The message.
        /// </summary>
        public SystemWindowMessage Message { get; }

        internal SystemWindowMessageEventArgs(Native.SDL_SysWMEvent wmevent) : base(wmevent.Timestamp)
        {
            Message = wmevent.Msg == null ? new SystemWindowMessage() : Marshal.PtrToStructure<SystemWindowMessage>((IntPtr)wmevent.Msg);
        }
    }
}
