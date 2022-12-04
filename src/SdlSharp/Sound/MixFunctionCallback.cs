namespace SdlSharp.Sound
{
    /// <summary>
    /// A mix function.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="userdata">User data.</param>
    public delegate void MixFunctionCallback(Span<byte> stream, nint userdata);
}
