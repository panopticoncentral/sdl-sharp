using System;

namespace SdlSharp.Sound
{
    /// <summary>
    /// The callback for an audio device.
    /// </summary>
    /// <param name="stream">The audio stream.</param>
    /// <param name="userdata">The user data specified when the device was opened.</param>
    public delegate void AudioCallback(Span<byte> stream, IntPtr userdata);
}
