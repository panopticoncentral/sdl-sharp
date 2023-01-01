namespace SdlSharp.Sound
{
    /// <summary>
    /// A mix function.
    /// </summary>
    /// <param name="stream">The stream.</param>
    public delegate void MixHookCallback(Span<byte> stream);
}
