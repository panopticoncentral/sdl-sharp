using System.Collections;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Video;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Represents a read-only collection of video drivers compiled into SDL.
/// </summary>
public sealed class VideoDriverCollection : IReadOnlyList<string>
{
    /// <summary>
    /// Gets the singleton instance of the video driver collection.
    /// </summary>
    public static VideoDriverCollection Instance { get; } = new();

    private VideoDriverCollection()
    {
    }

    /// <summary>
    /// Gets the number of video drivers.
    /// </summary>
    public int Count => SDL_GetNumVideoDrivers();

    /// <summary>
    /// Gets the name of the video driver at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the video driver.</param>
    /// <returns>The name of the video driver, or null if the index is out of range.</returns>
    public string this[int index] => CheckErrorNull(SDL_GetVideoDriver(index));

    /// <summary>
    /// Returns an enumerator that iterates through the video driver collection.
    /// </summary>
    public IEnumerator<string> GetEnumerator()
    {
        var count = Count;
        for (var i = 0; i < count; i++)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
