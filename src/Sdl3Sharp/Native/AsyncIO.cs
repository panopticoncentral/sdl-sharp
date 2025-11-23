using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Common;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_asyncio.h - Asynchronous I/O operations.
/// </summary>
public static unsafe partial class AsyncIO
{
    /// <summary>
    /// Types of asynchronous I/O tasks.
    /// </summary>
    public enum SDL_AsyncIOTaskType
    {
        /// <summary>A read operation.</summary>
        SDL_ASYNCIO_TASK_READ,
        /// <summary>A write operation.</summary>
        SDL_ASYNCIO_TASK_WRITE,
        /// <summary>A close operation.</summary>
        SDL_ASYNCIO_TASK_CLOSE
    }

    /// <summary>
    /// Possible outcomes of an asynchronous I/O task.
    /// </summary>
    public enum SDL_AsyncIOResult
    {
        /// <summary>Request was completed without error.</summary>
        SDL_ASYNCIO_COMPLETE,
        /// <summary>Request failed for some reason; check SDL_GetError()!</summary>
        SDL_ASYNCIO_FAILURE,
        /// <summary>Request was canceled before completing.</summary>
        SDL_ASYNCIO_CANCELED
    }

    /// <summary>
    /// The asynchronous I/O operation structure.
    /// </summary>
    /// <remarks>
    /// This operates as an opaque handle. One can then request read or write operations on it.
    /// </remarks>
    public struct SDL_AsyncIO
    {
    }

    /// <summary>
    /// A queue of completed asynchronous I/O tasks.
    /// </summary>
    /// <remarks>
    /// When starting an asynchronous operation, you specify a queue for the new task.
    /// A queue can be asked later if any tasks in it have completed, allowing an app to
    /// manage multiple pending tasks in one place, in whatever order they complete.
    /// </remarks>
    public struct SDL_AsyncIOQueue
    {
    }

    /// <summary>
    /// Information about a completed asynchronous I/O request.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_AsyncIOOutcome
    {
        /// <summary>What generated this task. This pointer will be invalid if it was closed!</summary>
        public SDL_AsyncIO* asyncio;
        /// <summary>What sort of task was this? Read, write, etc?</summary>
        public SDL_AsyncIOTaskType type;
        /// <summary>The result of the work (success, failure, cancellation).</summary>
        public SDL_AsyncIOResult result;
        /// <summary>Buffer where data was read/written.</summary>
        public void* buffer;
        /// <summary>Offset in the SDL_AsyncIO where data was read/written.</summary>
        public ulong offset;
        /// <summary>Number of bytes the task was to read/write.</summary>
        public ulong bytes_requested;
        /// <summary>Actual number of bytes that were read/written.</summary>
        public ulong bytes_transferred;
        /// <summary>Pointer provided by the app when starting the task.</summary>
        public nuint userdata;
    }

    /// <summary>
    /// Use this function to create a new SDL_AsyncIO object for reading from and/or writing to a named file.
    /// </summary>
    /// <param name="file">A UTF-8 string representing the filename to open.</param>
    /// <param name="mode">An ASCII string representing the mode to be used for opening the file.</param>
    /// <returns>A pointer to the SDL_AsyncIO structure that is created or NULL on failure; call SDL_GetError() for more information.</returns>
    /// <remarks>
    /// <para>The mode string understands the following values:</para>
    /// <list type="bullet">
    /// <item><description>"r": Open a file for reading only. It must exist.</description></item>
    /// <item><description>"w": Open a file for writing only. It will create missing files or truncate existing ones.</description></item>
    /// <item><description>"r+": Open a file for update both reading and writing. The file must exist.</description></item>
    /// <item><description>"w+": Create an empty file for both reading and writing.</description></item>
    /// </list>
    /// <para>There is no "b" mode, as there is only "binary" style I/O, and no "a" mode for appending,
    /// since you specify the position when starting a task.</para>
    /// <para>This call is not asynchronous; it will open the file before returning, under the assumption
    /// that doing so is generally a fast operation. Future reads and writes to the opened file will be async, however.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_AsyncIO* SDL_AsyncIOFromFile([MarshalUsing(typeof(Utf8StringMarshaller))] string file, [MarshalUsing(typeof(Utf8StringMarshaller))] string mode);

    /// <summary>
    /// Use this function to get the size of the data stream in an SDL_AsyncIO.
    /// </summary>
    /// <param name="asyncio">The SDL_AsyncIO to get the size of the data stream from.</param>
    /// <returns>The size of the data stream in the SDL_AsyncIO on success or a negative error code on failure; call SDL_GetError() for more information.</returns>
    /// <remarks>
    /// This call is not asynchronous; it assumes that obtaining this info is a non-blocking operation in most reasonable cases.
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long SDL_GetAsyncIOSize(SDL_AsyncIO* asyncio);

    /// <summary>
    /// Start an async read.
    /// </summary>
    /// <param name="asyncio">A pointer to an SDL_AsyncIO structure.</param>
    /// <param name="ptr">A pointer to a buffer to read data into.</param>
    /// <param name="offset">The position to start reading in the data source.</param>
    /// <param name="size">The number of bytes to read from the data source.</param>
    /// <param name="queue">A queue to add the new SDL_AsyncIO to.</param>
    /// <param name="userdata">An app-defined value that will be provided with the task results.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    /// <remarks>
    /// <para>This function reads up to size bytes from offset position in the data source to the area pointed at by ptr.
    /// This function may read less bytes than requested.</para>
    /// <para>This function returns as quickly as possible; it does not wait for the read to complete.
    /// On a successful return, this work will continue in the background. If the work begins, even failure is asynchronous:
    /// a failing return value from this function only means the work couldn't start at all.</para>
    /// <para>ptr must remain available until the work is done, and may be accessed by the system at any time until then.
    /// Do not allocate it on the stack, as this might take longer than the life of the calling function to complete!</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadAsyncIO(SDL_AsyncIO* asyncio, void* ptr, ulong offset, ulong size, SDL_AsyncIOQueue* queue, nuint userdata);

    /// <summary>
    /// Start an async write.
    /// </summary>
    /// <param name="asyncio">A pointer to an SDL_AsyncIO structure.</param>
    /// <param name="ptr">A pointer to a buffer to write data from.</param>
    /// <param name="offset">The position to start writing to the data source.</param>
    /// <param name="size">The number of bytes to write to the data source.</param>
    /// <param name="queue">A queue to add the new SDL_AsyncIO to.</param>
    /// <param name="userdata">An app-defined value that will be provided with the task results.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    /// <remarks>
    /// <para>This function writes size bytes from offset position in the data source to the area pointed at by ptr.</para>
    /// <para>This function returns as quickly as possible; it does not wait for the write to complete.
    /// On a successful return, this work will continue in the background. If the work begins, even failure is asynchronous:
    /// a failing return value from this function only means the work couldn't start at all.</para>
    /// <para>ptr must remain available until the work is done, and may be accessed by the system at any time until then.
    /// Do not allocate it on the stack, as this might take longer than the life of the calling function to complete!</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteAsyncIO(SDL_AsyncIO* asyncio, void* ptr, ulong offset, ulong size, SDL_AsyncIOQueue* queue, nuint userdata);

    /// <summary>
    /// Close and free any allocated resources for an async I/O object.
    /// </summary>
    /// <param name="asyncio">A pointer to an SDL_AsyncIO structure to close.</param>
    /// <param name="flush">true if data should sync to disk before the task completes.</param>
    /// <param name="queue">A queue to add the new SDL_AsyncIO to.</param>
    /// <param name="userdata">An app-defined value that will be provided with the task results.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    /// <remarks>
    /// <para>Closing a file is also an asynchronous task! If a write failure were to happen during the closing process,
    /// for example, the task results will report it as usual.</para>
    /// <para>Closing a file that has been written to does not guarantee the data has made it to physical media;
    /// it may remain in the operating system's file cache, for later writing to disk. This means that a successfully-closed
    /// file can be lost if the system crashes or loses power in this small window. To prevent this, call this function
    /// with the flush parameter set to true. This will make the operation take longer, and perhaps increase system load
    /// in general, but a successful result guarantees that the data has made it to physical storage.</para>
    /// <para>This function guarantees that the close will happen after any other pending tasks to asyncio, so it's safe
    /// to open a file, start several operations, close the file immediately, then check for all results later.
    /// This function will not block until the tasks have completed.</para>
    /// <para>Once this function returns true, asyncio is no longer valid, regardless of any future outcomes.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_CloseAsyncIO(SDL_AsyncIO* asyncio, [MarshalAs(UnmanagedType.U1)] bool flush, SDL_AsyncIOQueue* queue, nuint userdata);

    /// <summary>
    /// Create a task queue for tracking multiple I/O operations.
    /// </summary>
    /// <returns>A new task queue object or NULL if there was an error; call SDL_GetError() for more information.</returns>
    /// <remarks>
    /// Async I/O operations are assigned to a queue when started. The queue can be checked for completed tasks thereafter.
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_AsyncIOQueue* SDL_CreateAsyncIOQueue();

    /// <summary>
    /// Destroy a previously-created async I/O task queue.
    /// </summary>
    /// <param name="queue">The task queue to destroy.</param>
    /// <remarks>
    /// <para>If there are still tasks pending for this queue, this call will block until those tasks are finished.
    /// All those tasks will be deallocated. Their results will be lost to the app.</para>
    /// <para>Any pending reads from SDL_LoadFileAsync() that are still in this queue will have their buffers
    /// deallocated by this function, to prevent a memory leak.</para>
    /// <para>Once this function is called, the queue is no longer valid and should not be used, including by
    /// other threads that might access it while destruction is blocking on pending tasks.</para>
    /// <para>Do not destroy a queue that still has threads waiting on it through SDL_WaitAsyncIOResult().
    /// You can call SDL_SignalAsyncIOQueue() first to unblock those threads, and take measures
    /// (such as SDL_WaitThread()) to make sure they have finished their wait and won't wait on the queue again.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyAsyncIOQueue(SDL_AsyncIOQueue* queue);

    /// <summary>
    /// Query an async I/O task queue for completed tasks.
    /// </summary>
    /// <param name="queue">The async I/O task queue to query.</param>
    /// <param name="outcome">Details of a finished task will be written here. May not be NULL.</param>
    /// <returns>true if a task has completed, false otherwise.</returns>
    /// <remarks>
    /// <para>If a task assigned to this queue has finished, this will return true and fill in outcome with the details
    /// of the task. If no task in the queue has finished, this function will return false. This function does not block.</para>
    /// <para>If a task has completed, this function will free its resources and the task pointer will no longer be valid.
    /// The task will be removed from the queue.</para>
    /// <para>It is safe for multiple threads to call this function on the same queue at once;
    /// a completed task will only go to one of the threads.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetAsyncIOResult(SDL_AsyncIOQueue* queue, SDL_AsyncIOOutcome* outcome);

    /// <summary>
    /// Block until an async I/O task queue has a completed task.
    /// </summary>
    /// <param name="queue">The async I/O task queue to wait on.</param>
    /// <param name="outcome">Details of a finished task will be written here. May not be NULL.</param>
    /// <param name="timeoutMS">The maximum time to wait, in milliseconds, or -1 to wait indefinitely.</param>
    /// <returns>true if task has completed, false otherwise.</returns>
    /// <remarks>
    /// <para>This function puts the calling thread to sleep until there a task assigned to the queue that has finished.</para>
    /// <para>If a task assigned to the queue has finished, this will return true and fill in outcome with the details
    /// of the task. If no task in the queue has finished, this function will return false.</para>
    /// <para>If a task has completed, this function will free its resources and the task pointer will no longer be valid.
    /// The task will be removed from the queue.</para>
    /// <para>It is safe for multiple threads to call this function on the same queue at once;
    /// a completed task will only go to one of the threads.</para>
    /// <para>Note that by the nature of various platforms, more than one waiting thread may wake to handle a single task,
    /// but only one will obtain it, so timeoutMS is a maximum wait time, and this function may return false sooner.</para>
    /// <para>This function may return false if there was a system error, the OS inadvertently awoke multiple threads,
    /// or if SDL_SignalAsyncIOQueue() was called to wake up all waiting threads without a finished task.</para>
    /// <para>A timeout can be used to specify a maximum wait time, but rather than polling, it is possible to have
    /// a timeout of -1 to wait forever, and use SDL_SignalAsyncIOQueue() to wake up the waiting threads later.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WaitAsyncIOResult(SDL_AsyncIOQueue* queue, SDL_AsyncIOOutcome* outcome, int timeoutMS);

    /// <summary>
    /// Wake up any threads that are blocking in SDL_WaitAsyncIOResult().
    /// </summary>
    /// <param name="queue">The async I/O task queue to signal.</param>
    /// <remarks>
    /// <para>This will unblock any threads that are sleeping in a call to SDL_WaitAsyncIOResult for the specified queue,
    /// and cause them to return from that function.</para>
    /// <para>This can be useful when destroying a queue to make sure nothing is touching it indefinitely.
    /// In this case, once this call completes, the caller should take measures to make sure any previously-blocked
    /// threads have returned from their wait and will not touch the queue again (perhaps by setting a flag to tell
    /// the threads to terminate and then using SDL_WaitThread() to make sure they've done so).</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SignalAsyncIOQueue(SDL_AsyncIOQueue* queue);

    /// <summary>
    /// Load all the data from a file path, asynchronously.
    /// </summary>
    /// <param name="file">The path to read all available data from.</param>
    /// <param name="queue">A queue to add the new SDL_AsyncIO to.</param>
    /// <param name="userdata">An app-defined value that will be provided with the task results.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    /// <remarks>
    /// <para>This function returns as quickly as possible; it does not wait for the read to complete.
    /// On a successful return, this work will continue in the background. If the work begins, even failure is asynchronous:
    /// a failing return value from this function only means the work couldn't start at all.</para>
    /// <para>The data is allocated with a zero byte at the end (null terminated) for convenience.
    /// This extra byte is not included in SDL_AsyncIOOutcome's bytes_transferred value.</para>
    /// <para>This function will allocate the buffer to contain the file. It must be deallocated by calling
    /// SDL_free() on SDL_AsyncIOOutcome's buffer field after completion.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_LoadFileAsync([MarshalUsing(typeof(Utf8StringMarshaller))] string file, SDL_AsyncIOQueue* queue, nuint userdata);
}
