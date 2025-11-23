using static Sdl3Sharp.Native.AsyncIO;
using static Sdl3Sharp.Native.Common;

namespace Sdl3Sharp.AsyncIO;

/// <summary>
/// A queue for tracking multiple asynchronous I/O operations.
/// </summary>
/// <remarks>
/// <para>Async I/O operations are assigned to a queue when started. The queue can be
/// checked for completed tasks thereafter, allowing an app to manage multiple
/// pending tasks in one place, in whatever order they complete.</para>
/// <para>One async I/O queue can be shared by multiple threads, or one thread can
/// have more than one queue.</para>
/// </remarks>
public sealed unsafe class AsyncIOQueue : IDisposable
{
    private bool _disposed;

    /// <summary>
    /// Gets the underlying SDL_AsyncIOQueue pointer.
    /// </summary>
    public SDL_AsyncIOQueue* Handle { get; private set; }

    /// <summary>
    /// Creates a new async I/O queue.
    /// </summary>
    /// <returns>A new AsyncIOQueue instance.</returns>
    public static AsyncIOQueue Create()
    {
        return new AsyncIOQueue(CheckErrorPointer(SDL_CreateAsyncIOQueue()));
    }

    /// <summary>
    /// Wraps an existing SDL_AsyncIOQueue pointer.
    /// </summary>
    /// <param name="handle">The SDL_AsyncIOQueue pointer to wrap.</param>
    internal AsyncIOQueue(SDL_AsyncIOQueue* handle)
    {
        Handle = handle;
    }

    /// <summary>
    /// Tries to get a completed task from the queue without blocking.
    /// </summary>
    /// <param name="outcome">When this method returns true, contains information about the completed task.</param>
    /// <returns>true if a task has completed; false otherwise.</returns>
    /// <remarks>
    /// This function does not block. If no task in the queue has finished, it returns false immediately.
    /// It is safe for multiple threads to call this method on the same queue at once;
    /// a completed task will only go to one of the threads.
    /// </remarks>
    public bool TryGetResult(out AsyncIOOutcome outcome)
    {
        ThrowIfDisposed();

        SDL_AsyncIOOutcome nativeOutcome;
        if (SDL_GetAsyncIOResult(Handle, &nativeOutcome))
        {
            outcome = new AsyncIOOutcome(
                (AsyncIOTaskType)nativeOutcome.type,
                (AsyncIOResult)nativeOutcome.result,
                nativeOutcome.offset,
                nativeOutcome.bytes_requested,
                nativeOutcome.bytes_transferred,
                nativeOutcome.userdata);
            return true;
        }

        outcome = default;
        return false;
    }

    /// <summary>
    /// Waits for a task to complete in the queue.
    /// </summary>
    /// <param name="outcome">When this method returns true, contains information about the completed task.</param>
    /// <param name="timeout">The maximum time to wait, or <see cref="Timeout.InfiniteTimeSpan"/> to wait indefinitely.</param>
    /// <returns>true if a task has completed; false if the timeout expired or the queue was signaled.</returns>
    /// <remarks>
    /// <para>This method puts the calling thread to sleep until a task assigned to the queue has finished,
    /// or until the timeout expires.</para>
    /// <para>It is safe for multiple threads to call this method on the same queue at once;
    /// a completed task will only go to one of the threads.</para>
    /// <para>This method may return false if there was a system error, the OS inadvertently awoke multiple threads,
    /// or if <see cref="Signal"/> was called to wake up all waiting threads without a finished task.</para>
    /// </remarks>
    public bool WaitForResult(out AsyncIOOutcome outcome, TimeSpan timeout)
    {
        ThrowIfDisposed();

        var timeoutMs = timeout == Timeout.InfiniteTimeSpan ? -1 : (int)timeout.TotalMilliseconds;

        SDL_AsyncIOOutcome nativeOutcome;
        if (SDL_WaitAsyncIOResult(Handle, &nativeOutcome, timeoutMs))
        {
            outcome = new AsyncIOOutcome(
                (AsyncIOTaskType)nativeOutcome.type,
                (AsyncIOResult)nativeOutcome.result,
                nativeOutcome.offset,
                nativeOutcome.bytes_requested,
                nativeOutcome.bytes_transferred,
                nativeOutcome.userdata);
            return true;
        }

        outcome = default;
        return false;
    }

    /// <summary>
    /// Waits indefinitely for a task to complete in the queue.
    /// </summary>
    /// <param name="outcome">When this method returns true, contains information about the completed task.</param>
    /// <returns>true if a task has completed; false if the queue was signaled without a finished task.</returns>
    public bool WaitForResult(out AsyncIOOutcome outcome)
    {
        return WaitForResult(out outcome, Timeout.InfiniteTimeSpan);
    }

    /// <summary>
    /// Wakes up any threads that are blocking in <see cref="WaitForResult(out AsyncIOOutcome, TimeSpan)"/>.
    /// </summary>
    /// <remarks>
    /// <para>This will unblock any threads that are sleeping in a call to WaitForResult for this queue,
    /// and cause them to return from that method.</para>
    /// <para>This can be useful when destroying a queue to make sure nothing is touching it indefinitely.
    /// In this case, once this call completes, the caller should take measures to make sure any
    /// previously-blocked threads have returned from their wait and will not touch the queue again.</para>
    /// </remarks>
    public void Signal()
    {
        ThrowIfDisposed();
        SDL_SignalAsyncIOQueue(Handle);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (Handle is not null)
        {
            SDL_DestroyAsyncIOQueue(Handle);
            Handle = null;
        }

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
