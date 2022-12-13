using System.Runtime.InteropServices;

namespace SdlSharp.Graphics
{
    /// <summary>
    /// Event arguments for a system window message event.
    /// </summary>
    public sealed unsafe class SystemWindowMessageEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The message.
        /// </summary>
        public SystemWindowMessage Message { get; }

        internal SystemWindowMessageEventArgs(Native.SDL_SysWMEvent wmevent) : base(wmevent.timestamp)
        {
            Message = wmevent.msg == null ? new SystemWindowMessage() : Marshal.PtrToStructure<SystemWindowMessage>((nint)wmevent.msg);
        }
    }
}
