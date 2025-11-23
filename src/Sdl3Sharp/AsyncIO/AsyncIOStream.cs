using static Sdl3Sharp.Native.AsyncIO;
using static Sdl3Sharp.Native.Common;

namespace Sdl3Sharp.AsyncIO;

/// <summary>
/// Represents an asynchronous I/O stream for non-blocking file operations.
/// </summary>
/// <remarks>
/// <para>SDL offers a way to perform I/O asynchronously. This allows an app to read
/// or write files without waiting for data to actually transfer; the functions
/// that request I/O never block while the request is fulfilled.</para>
/// <para>Instead, the data moves in the background and the app can check for results
/// at their leisure using an <see cref="AsyncIOQueue"/>.</para>
/// <para>Behind the scenes, SDL will use newer, efficient APIs on platforms that
/// support them: Linux's io_uring and Windows 11's IoRing, for example. If
/// those technologies aren't available, SDL will offload the work to a thread
/// pool that will manage otherwise-synchronous loads without blocking the app.</para>
/// </remarks>
public sealed unsafe class AsyncIOStream
{
    /// <summary>
    /// Gets the underlying SDL_AsyncIO pointer.
    /// </summary>
    public SDL_AsyncIO* Handle { get; private set; }

    /// <summary>
    /// Gets the size of the data stream in bytes.
    /// </summary>
    /// <remarks>
    /// This call is not asynchronous; it assumes that obtaining this info is a non-blocking operation.
    /// </remarks>
    public long Size
    {
        get
        {
            ThrowIfInvalid();
            return CheckErrorNegative(SDL_GetAsyncIOSize(Handle));
        }
    }

    /// <summary>
    /// Opens a file for asynchronous reading and/or writing.
    /// </summary>
    /// <param name="path">The path to the file to open.</param>
    /// <param name="mode">The mode to open the file in.</param>
    /// <returns>A new AsyncIO instance.</returns>
    /// <remarks>
    /// <para>The mode string understands the following values:</para>
    /// <list type="bullet">
    /// <item><description>"r": Open a file for reading only. It must exist.</description></item>
    /// <item><description>"w": Open a file for writing only. It will create missing files or truncate existing ones.</description></item>
    /// <item><description>"r+": Open a file for update both reading and writing. The file must exist.</description></item>
    /// <item><description>"w+": Create an empty file for both reading and writing.</description></item>
    /// </list>
    /// <para>There is no "b" mode (only binary I/O) and no "a" mode (you specify position when starting a task).</para>
    /// <para>This call is not asynchronous; it will open the file before returning.
    /// Future reads and writes to the opened file will be async.</para>
    /// </remarks>
    public static AsyncIOStream FromFile(string path, string mode)
    {
        return new(CheckErrorPointer(SDL_AsyncIOFromFile(path, mode)));
    }

    /// <summary>
    /// Asynchronously loads all data from a file.
    /// </summary>
    /// <param name="path">The path to the file to load.</param>
    /// <param name="queue">The queue to receive the completion notification.</param>
    /// <param name="userData">An app-defined value that will be provided with the task results.</param>
    /// <remarks>
    /// <para>This is a convenience function that handles allocating a buffer, reading the file,
    /// and null-terminating it. Check results via the queue.</para>
    /// <para>The data must be deallocated by calling SDL_free() on the buffer field
    /// of the outcome after completion.</para>
    /// </remarks>
    public static void LoadFileAsync(string path, AsyncIOQueue queue, nuint userData = 0)
    {
        ArgumentNullException.ThrowIfNull(queue);
        _ = CheckErrorBool(SDL_LoadFileAsync(path, queue.Handle, userData));
    }

    /// <summary>
    /// Wraps an existing SDL_AsyncIO pointer.
    /// </summary>
    /// <param name="handle">The SDL_AsyncIO pointer to wrap.</param>
    internal AsyncIOStream(SDL_AsyncIO* handle)
    {
        Handle = handle;
    }

    /// <summary>
    /// Starts an asynchronous read operation.
    /// </summary>
    /// <param name="buffer">The buffer to read data into. Must remain valid until the operation completes.</param>
    /// <param name="offset">The position in the file to start reading from.</param>
    /// <param name="queue">The queue to receive the completion notification.</param>
    /// <param name="userData">An app-defined value that will be provided with the task results.</param>
    /// <remarks>
    /// <para>This function returns immediately; it does not wait for the read to complete.
    /// The work continues in the background.</para>
    /// <para>IMPORTANT: The buffer must remain valid and accessible until the operation completes.
    /// Do not use stack-allocated buffers for async operations.</para>
    /// </remarks>
    public void ReadAsync(Memory<byte> buffer, ulong offset, AsyncIOQueue queue, nuint userData = 0)
    {
        ThrowIfInvalid();
        ArgumentNullException.ThrowIfNull(queue);

        fixed (byte* ptr = buffer.Span)
        {
            _ = CheckErrorBool(SDL_ReadAsyncIO(Handle, ptr, offset, (ulong)buffer.Length, queue.Handle, userData));
        }
    }

    /// <summary>
    /// Starts an asynchronous write operation.
    /// </summary>
    /// <param name="buffer">The buffer containing data to write. Must remain valid until the operation completes.</param>
    /// <param name="offset">The position in the file to start writing to.</param>
    /// <param name="queue">The queue to receive the completion notification.</param>
    /// <param name="userData">An app-defined value that will be provided with the task results.</param>
    /// <remarks>
    /// <para>This function returns immediately; it does not wait for the write to complete.
    /// The work continues in the background.</para>
    /// <para>IMPORTANT: The buffer must remain valid and accessible until the operation completes.
    /// Do not use stack-allocated buffers for async operations.</para>
    /// </remarks>
    public void WriteAsync(ReadOnlyMemory<byte> buffer, ulong offset, AsyncIOQueue queue, nuint userData = 0)
    {
        ThrowIfInvalid();
        ArgumentNullException.ThrowIfNull(queue);

        fixed (byte* ptr = buffer.Span)
        {
            _ = CheckErrorBool(SDL_WriteAsyncIO(Handle, ptr, offset, (ulong)buffer.Length, queue.Handle, userData));
        }
    }

    /// <summary>
    /// Closes the async I/O stream asynchronously.
    /// </summary>
    /// <param name="queue">The queue to receive the completion notification.</param>
    /// <param name="flush">If true, ensures data is synced to disk before the task completes.</param>
    /// <param name="userData">An app-defined value that will be provided with the task results.</param>
    /// <remarks>
    /// <para>Closing a file is also an asynchronous task. If a write failure occurs during
    /// the closing process, the task results will report it.</para>
    /// <para>If flush is false, data may remain in the OS file cache and could be lost
    /// if the system crashes. Set flush to true for critical data like game saves.</para>
    /// <para>This method guarantees the close will happen after any other pending tasks,
    /// so it's safe to open a file, start several operations, close immediately,
    /// then check for all results later.</para>
    /// <para>Once this method returns successfully, this AsyncIO instance is no longer valid.</para>
    /// </remarks>
    public void CloseAsync(AsyncIOQueue queue, bool flush = false, nuint userData = 0)
    {
        ThrowIfInvalid();
        ArgumentNullException.ThrowIfNull(queue);

        _ = CheckErrorBool(SDL_CloseAsyncIO(Handle, flush, queue.Handle, userData));
        Handle = null;
    }

    private void ThrowIfInvalid()
    {
        if (Handle is null)
        {
            throw new InvalidOperationException("The async I/O stream has been closed.");
        }
    }
}
