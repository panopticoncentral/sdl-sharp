namespace SdlSharp.Sound
{
    /// <summary>
    /// An callback for audio data.
    /// </summary>
    /// <param name="data">The data.</param>
    public delegate void AudioCallback(Span<byte> data);
}
