namespace SdlSharp.Audio;

/// <summary>
/// Audio format specification: format, channels, and sample rate.
/// </summary>
/// <param name="Format">Audio data format.</param>
/// <param name="Channels">Number of channels (1 = mono, 2 = stereo, etc.).</param>
/// <param name="Frequency">Sample rate in sample frames per second.</param>
public readonly record struct AudioSpec(AudioFormat Format, int Channels, int Frequency);
