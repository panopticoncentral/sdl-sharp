using System;

namespace SdlSharp.Sound
{
    /// <summary>
    /// A mix function.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="userdata">User data.</param>
    public delegate void MixFunctionDelegate(Span<byte> stream, IntPtr userdata);
}
