using System;

namespace SdlSharp.Touch
{
    /// <summary>
    /// Capabilities that a haptic device can support.
    /// </summary>
    [Flags]
    public enum HapticCapabilities : uint
    {
        /// <summary>
        /// No capabilities.
        /// </summary>
        None,

        /// <summary>
        /// Device supports setting the global gain.
        /// </summary>
        Gain = 1u << 12,

        /// <summary>
        /// Device supports setting autocenter.
        /// </summary>
        Autocenter = 1u << 13,

        /// <summary>
        /// Device supports querying effect status.
        /// </summary>
        Status = 1u << 14,

        /// <summary>
        /// Devices supports being paused.
        /// </summary>
        Pause = 1u << 15,
        
        /// <summary>
        /// All capabilities.
        /// </summary>
        All = Gain | Autocenter | Status | Pause
    }
}
