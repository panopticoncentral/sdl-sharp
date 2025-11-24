using Sdl3Sharp.Graphics;
using static Sdl3Sharp.Native.Camera;
using static Sdl3Sharp.Native.Surface;

namespace Sdl3Sharp.Input;

/// <summary>
/// Represents a frame of video acquired from a camera.
/// This frame must be released back to the camera using <see cref="Dispose"/> or the using statement.
/// Do not hold onto frames for too long, as the camera has a limited buffer.
/// </summary>
public sealed unsafe class CameraFrame : IDisposable
{
    private bool _disposed;
    private readonly SDL_Camera* _camera;

    /// <summary>
    /// Gets the underlying SDL_Surface pointer for the frame.
    /// </summary>
    public SDL_Surface* Handle { get; private set; }

    /// <summary>
    /// Gets the timestamp of when this frame was captured, in nanoseconds.
    /// </summary>
    public ulong TimestampNanoseconds { get; }

    /// <summary>
    /// Gets the size of the frame in pixels.
    /// </summary>
    public Size Size
    {
        get
        {
            ThrowIfDisposed();
            return new(Handle->w, Handle->h);
        }
    }

    /// <summary>
    /// Gets the pitch (distance in bytes between rows of pixels).
    /// </summary>
    public int Pitch
    {
        get
        {
            ThrowIfDisposed();
            return Handle->pitch;
        }
    }

    /// <summary>
    /// Gets the pixel format of the frame.
    /// </summary>
    public PixelFormat Format
    {
        get
        {
            ThrowIfDisposed();
            return new(Handle->format);
        }
    }

    /// <summary>
    /// Initializes a new instance wrapping an acquired camera frame.
    /// </summary>
    /// <param name="camera">The camera that provided this frame.</param>
    /// <param name="handle">The SDL_Surface pointer for the frame.</param>
    /// <param name="timestampNs">The timestamp of the frame in nanoseconds.</param>
    internal CameraFrame(SDL_Camera* camera, SDL_Surface* handle, ulong timestampNs)
    {
        _camera = camera;
        Handle = handle;
        TimestampNanoseconds = timestampNs;
    }

    /// <summary>
    /// Gets a span of the frame's pixels.
    /// </summary>
    /// <typeparam name="T">The type of pixel data.</typeparam>
    /// <returns>A span over the pixel data.</returns>
    public Span<T> GetPixels<T>() where T : unmanaged
    {
        ThrowIfDisposed();
        var size = Handle->pitch * Handle->h / sizeof(T);
        return new Span<T>(Handle->pixels, size);
    }

    /// <summary>
    /// Creates a copy of this frame as a new Surface that you can keep after releasing the frame.
    /// </summary>
    /// <returns>A new Surface containing a copy of the frame data.</returns>
    public Surface ToSurface()
    {
        ThrowIfDisposed();
        return new Surface(Native.Common.CheckErrorPointer(SDL_DuplicateSurface(Handle)), ownsHandle: true);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (Handle != null && _camera != null)
        {
            SDL_ReleaseCameraFrame(_camera, Handle);
            Handle = null;
        }

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
