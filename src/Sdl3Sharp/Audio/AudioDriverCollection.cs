using System.Collections;

using NativeAudio = Sdl3Sharp.Native.Audio;

using static Sdl3Sharp.Native.Common;

namespace Sdl3Sharp.Audio;

/// <summary>
/// Represents a read-only collection of audio drivers compiled into SDL.
/// </summary>
public sealed class AudioDriverCollection : IReadOnlyList<string>
{
    /// <summary>
    /// Gets the singleton instance of the audio driver collection.
    /// </summary>
    public static AudioDriverCollection Instance { get; } = new();

    private AudioDriverCollection()
    {
    }

    /// <summary>
    /// Gets the number of audio drivers.
    /// </summary>
    public int Count => NativeAudio.SDL_GetNumAudioDrivers();

    /// <summary>
    /// Gets the name of the audio driver at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the audio driver.</param>
    /// <returns>The name of the audio driver.</returns>
    public string this[int index] => CheckErrorNull(NativeAudio.SDL_GetAudioDriver(index));

    /// <summary>
    /// Returns an enumerator that iterates through the audio driver collection.
    /// </summary>
    public IEnumerator<string> GetEnumerator()
    {
        var count = Count;
        for (var i = 0; i < count; i++)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
