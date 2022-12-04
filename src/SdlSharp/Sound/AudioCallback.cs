namespace SdlSharp.Sound
{
    /// <summary>
    /// The callback for an audio device.
    /// </summary>
    /// <param name="stream">The audio stream.</param>
    public delegate void AudioCallback(Span<byte> stream);
}
