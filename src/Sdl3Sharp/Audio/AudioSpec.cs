namespace Sdl3Sharp.Audio;

/// <summary>
/// Specifies the format of audio data including sample format, channel count, and sample rate.
/// </summary>
/// <param name="Format">The audio sample format.</param>
/// <param name="Channels">The number of audio channels (1 for mono, 2 for stereo, etc.).</param>
/// <param name="Frequency">The sample rate in Hz (samples per second).</param>
public readonly record struct AudioSpec(AudioFormat Format, int Channels, int Frequency)
{
    /// <summary>
    /// Gets the size in bytes of a single audio frame (all channels combined).
    /// </summary>
    public int FrameSize => Format.ByteSize() * Channels;

    /// <summary>
    /// Calculates the duration in milliseconds for a given number of bytes.
    /// </summary>
    /// <param name="bytes">The number of bytes of audio data.</param>
    /// <returns>The duration in milliseconds.</returns>
    public double BytesToMilliseconds(int bytes)
    {
        if (FrameSize == 0 || Frequency == 0)
        {
            return 0;
        }

        var frames = bytes / FrameSize;
        return frames * 1000.0 / Frequency;
    }

    /// <summary>
    /// Calculates the number of bytes needed for a given duration in milliseconds.
    /// </summary>
    /// <param name="milliseconds">The duration in milliseconds.</param>
    /// <returns>The number of bytes needed.</returns>
    public int MillisecondsToBytes(double milliseconds)
    {
        var frames = (int)(milliseconds * Frequency / 1000.0);
        return frames * FrameSize;
    }

    /// <summary>
    /// Creates a common stereo audio spec with 16-bit signed samples.
    /// </summary>
    /// <param name="frequency">The sample rate in Hz (default: 44100).</param>
    /// <returns>A new AudioSpec for stereo 16-bit audio.</returns>
    public static AudioSpec Stereo16(int frequency = 44100) => new(AudioFormat.S16, 2, frequency);

    /// <summary>
    /// Creates a common stereo audio spec with 32-bit floating point samples.
    /// </summary>
    /// <param name="frequency">The sample rate in Hz (default: 44100).</param>
    /// <returns>A new AudioSpec for stereo float audio.</returns>
    public static AudioSpec StereoFloat(int frequency = 44100) => new(AudioFormat.F32, 2, frequency);

    /// <summary>
    /// Creates a common mono audio spec with 16-bit signed samples.
    /// </summary>
    /// <param name="frequency">The sample rate in Hz (default: 44100).</param>
    /// <returns>A new AudioSpec for mono 16-bit audio.</returns>
    public static AudioSpec Mono16(int frequency = 44100) => new(AudioFormat.S16, 1, frequency);

    /// <summary>
    /// Creates a common mono audio spec with 32-bit floating point samples.
    /// </summary>
    /// <param name="frequency">The sample rate in Hz (default: 44100).</param>
    /// <returns>A new AudioSpec for mono float audio.</returns>
    public static AudioSpec MonoFloat(int frequency = 44100) => new(AudioFormat.F32, 1, frequency);
}
