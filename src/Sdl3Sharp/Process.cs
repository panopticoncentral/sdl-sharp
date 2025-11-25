using System.Runtime.InteropServices;
using System.Text;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Process;
using static Sdl3Sharp.Native.Properties;
using static Sdl3Sharp.Native.StdInc;

namespace Sdl3Sharp;

/// <summary>
/// Represents a cross-platform system process.
/// </summary>
/// <remarks>
/// <para>This class provides a cross-platform way to spawn and manage OS-level processes.</para>
/// <para>You can create a new subprocess with <see cref="Create"/> or <see cref="CreateWithProperties"/>
/// and optionally read and write to it using the standard I/O streams.</para>
/// <para>Don't forget to call <see cref="Dispose"/> to clean up, whether the process
/// was killed, terminated on its own, or is still running!</para>
/// </remarks>
public sealed unsafe class Process : IDisposable
{
    private bool _disposed;

    /// <summary>
    /// Gets the underlying SDL_Process pointer.
    /// </summary>
    public SDL_Process* Handle { get; private set; }

    /// <summary>
    /// Gets the properties associated with this process.
    /// </summary>
    public PropertyGroup Properties
    {
        get
        {
            ThrowIfDisposed();
            return new(CheckErrorZero(SDL_GetProcessProperties(Handle)), ownsHandle: false);
        }
    }

    /// <summary>
    /// Gets the process ID.
    /// </summary>
    public long? ProcessId
    {
        get
        {
            ThrowIfDisposed();
            PropertyGroup props = Properties;
            return props.HasProperty(SDL_PROP_PROCESS_PID_NUMBER)
                ? props.GetNumber(SDL_PROP_PROCESS_PID_NUMBER)
                : null;
        }
    }

    /// <summary>
    /// Gets whether the process is running in the background.
    /// </summary>
    public bool IsBackground
    {
        get
        {
            ThrowIfDisposed();
            PropertyGroup props = Properties;
            return props.HasProperty(SDL_PROP_PROCESS_BACKGROUND_BOOLEAN)
                && props.GetBoolean(SDL_PROP_PROCESS_BACKGROUND_BOOLEAN);
        }
    }

    /// <summary>
    /// Gets the IOStream for writing to the process's standard input, if available.
    /// </summary>
    /// <remarks>
    /// This is only available if the process was created with standard input set to <see cref="ProcessIO.App"/>.
    /// </remarks>
    public IOStream? StandardInput
    {
        get
        {
            ThrowIfDisposed();
            Native.IOStream.SDL_IOStream* stream = SDL_GetProcessInput(Handle);
            return stream is not null ? new IOStream(stream, ownsHandle: false) : null;
        }
    }

    /// <summary>
    /// Gets the IOStream for reading from the process's standard output, if available.
    /// </summary>
    /// <remarks>
    /// <para>This is only available if the process was created with standard output set to <see cref="ProcessIO.App"/>.</para>
    /// <para>This stream is non-blocking and may return 0 bytes if no output is available yet.</para>
    /// </remarks>
    public IOStream? StandardOutput
    {
        get
        {
            ThrowIfDisposed();
            Native.IOStream.SDL_IOStream* stream = SDL_GetProcessOutput(Handle);
            return stream is not null ? new IOStream(stream, ownsHandle: false) : null;
        }
    }

    /// <summary>
    /// Gets the IOStream for reading from the process's standard error, if available.
    /// </summary>
    /// <remarks>
    /// <para>This is only available if the process was created with standard error set to <see cref="ProcessIO.App"/>.</para>
    /// <para>This stream is non-blocking and may return 0 bytes if no output is available yet.</para>
    /// </remarks>
    public IOStream? StandardError
    {
        get
        {
            ThrowIfDisposed();
            PropertyGroup props = Properties;
            if (!props.HasProperty(SDL_PROP_PROCESS_STDERR_POINTER))
            {
                return null;
            }

            var streamPtr = (Native.IOStream.SDL_IOStream*)SDL_GetPointerProperty(props.Id, SDL_PROP_PROCESS_STDERR_POINTER, null);
            return streamPtr is not null ? new IOStream(streamPtr, ownsHandle: false) : null;
        }
    }

    /// <summary>
    /// Creates a new process.
    /// </summary>
    /// <param name="args">The path to the executable and arguments for the new process.</param>
    /// <param name="pipeStdio">True to create pipes to the process's standard input and output, false otherwise.</param>
    /// <returns>A new Process instance.</returns>
    /// <remarks>
    /// <para>The first element in <paramref name="args"/> should be the path to the executable.</para>
    /// <para>Setting <paramref name="pipeStdio"/> to true is equivalent to setting standard input and
    /// standard output to <see cref="ProcessIO.App"/>.</para>
    /// </remarks>
    public static Process Create(string[] args, bool pipeStdio = false)
    {
        if (args is null || args.Length == 0)
        {
            throw new ArgumentException("Arguments array must contain at least one element (the executable path)", nameof(args));
        }

        var argPointers = new byte*[args.Length + 1];
        var allocatedStrings = new List<IntPtr>();

        try
        {
            for (var i = 0; i < args.Length; i++)
            {
                var bytes = Encoding.UTF8.GetBytes(args[i] + '\0');
                var ptr = (byte*)Marshal.AllocHGlobal(bytes.Length);
                Marshal.Copy(bytes, 0, (IntPtr)ptr, bytes.Length);
                argPointers[i] = ptr;
                allocatedStrings.Add((IntPtr)ptr);
            }

            argPointers[args.Length] = null;

            fixed (byte** argsPtr = argPointers)
            {
                SDL_Process* handle = CheckErrorPointer(SDL_CreateProcess(argsPtr, pipeStdio));
                return new Process(handle);
            }
        }
        finally
        {
            foreach (var ptr in allocatedStrings)
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }

    /// <summary>
    /// Creates a new process with the specified properties.
    /// </summary>
    /// <param name="args">The path to the executable and arguments for the new process.</param>
    /// <param name="stdin">Where standard input for the process comes from.</param>
    /// <param name="stdout">Where standard output for the process goes to.</param>
    /// <param name="stderr">Where standard error for the process goes to.</param>
    /// <param name="background">True if the process should run in the background.</param>
    /// <param name="stderrToStdout">True if error output should be redirected to standard output.</param>
    /// <returns>A new Process instance.</returns>
    public static Process CreateWithProperties(
        string[] args,
        ProcessIO stdin = ProcessIO.Null,
        ProcessIO stdout = ProcessIO.Inherited,
        ProcessIO stderr = ProcessIO.Inherited,
        bool background = false,
        bool stderrToStdout = false)
    {
        if (args is null || args.Length == 0)
        {
            throw new ArgumentException("Arguments array must contain at least one element (the executable path)", nameof(args));
        }

        var props = new PropertyGroup();
        var argPointers = new byte*[args.Length + 1];
        var allocatedStrings = new List<IntPtr>();

        try
        {
            for (var i = 0; i < args.Length; i++)
            {
                var bytes = Encoding.UTF8.GetBytes(args[i] + '\0');
                var ptr = (byte*)Marshal.AllocHGlobal(bytes.Length);
                Marshal.Copy(bytes, 0, (IntPtr)ptr, bytes.Length);
                argPointers[i] = ptr;
                allocatedStrings.Add((IntPtr)ptr);
            }

            argPointers[args.Length] = null;

            fixed (byte** argsPtr = argPointers)
            {
                _ = CheckErrorBool(SDL_SetPointerProperty(props.Id, SDL_PROP_PROCESS_CREATE_ARGS_POINTER, argsPtr));
            }

            props.SetNumber(SDL_PROP_PROCESS_CREATE_STDIN_NUMBER, (int)stdin);
            props.SetNumber(SDL_PROP_PROCESS_CREATE_STDOUT_NUMBER, (int)stdout);
            props.SetNumber(SDL_PROP_PROCESS_CREATE_STDERR_NUMBER, (int)stderr);

            if (background)
            {
                props.SetBoolean(SDL_PROP_PROCESS_CREATE_BACKGROUND_BOOLEAN, true);
            }

            if (stderrToStdout)
            {
                props.SetBoolean(SDL_PROP_PROCESS_CREATE_STDERR_TO_STDOUT_BOOLEAN, true);
            }

            SDL_Process* handle = CheckErrorPointer(SDL_CreateProcessWithProperties(props.Id));
            return new Process(handle);
        }
        finally
        {
            foreach (var ptr in allocatedStrings)
            {
                Marshal.FreeHGlobal(ptr);
            }

            props.Dispose();
        }
    }

    /// <summary>
    /// Wraps an existing SDL_Process pointer.
    /// </summary>
    /// <param name="handle">The SDL_Process pointer to wrap.</param>
    internal Process(SDL_Process* handle)
    {
        Handle = handle;
    }

    /// <summary>
    /// Reads all the output from the process.
    /// </summary>
    /// <param name="exitCode">When this method returns, contains the process exit code if the process has exited.</param>
    /// <returns>The output data from the process.</returns>
    /// <remarks>
    /// <para>This method blocks until the process is complete, capturing all output.</para>
    /// <para>The process must have been created with I/O enabled (standard output set to <see cref="ProcessIO.App"/>).</para>
    /// </remarks>
    public byte[] ReadAllOutput(out int exitCode)
    {
        ThrowIfDisposed();

        nuint dataSize;
        int exit;
        var data = CheckErrorPointer(SDL_ReadProcess(Handle, &dataSize, &exit));
        exitCode = exit;

        try
        {
            var result = new byte[dataSize];
            new Span<byte>(data, (int)dataSize).CopyTo(result);
            return result;
        }
        finally
        {
            SDL_free(data);
        }
    }

    /// <summary>
    /// Reads all the output from the process as a UTF-8 string.
    /// </summary>
    /// <param name="exitCode">When this method returns, contains the process exit code if the process has exited.</param>
    /// <returns>The output from the process as a string.</returns>
    /// <remarks>
    /// <para>This method blocks until the process is complete, capturing all output.</para>
    /// <para>The process must have been created with I/O enabled (standard output set to <see cref="ProcessIO.App"/>).</para>
    /// </remarks>
    public string ReadAllOutputAsString(out int exitCode)
    {
        var data = ReadAllOutput(out exitCode);
        return Encoding.UTF8.GetString(data);
    }

    /// <summary>
    /// Stops the process.
    /// </summary>
    /// <param name="force">True to terminate the process immediately, false to try to stop the process gracefully.</param>
    /// <remarks>
    /// In general you should try to stop the process gracefully first as terminating a process may
    /// leave it with half-written data or in some other unstable state.
    /// </remarks>
    public void Kill(bool force = false)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_KillProcess(Handle, force));
    }

    /// <summary>
    /// Waits for the process to finish.
    /// </summary>
    /// <param name="block">If true, block until the process finishes; otherwise, check the process status without blocking.</param>
    /// <param name="exitCode">When this method returns true, contains the process exit code.</param>
    /// <returns>True if the process has exited, false if it is still running.</returns>
    /// <remarks>
    /// <para>This method can be called multiple times to get the status of a process.</para>
    /// <para>The exit code will be the exit code of the process if it terminates normally,
    /// a negative signal if it terminated due to a signal, or -255 otherwise.</para>
    /// <para>If you create a process with standard output piped to the application, you should
    /// read all of the process output before calling this method with blocking enabled, otherwise
    /// the process might block indefinitely waiting for output to be read.</para>
    /// </remarks>
    public bool Wait(bool block, out int exitCode)
    {
        ThrowIfDisposed();
        int exit;
        var hasExited = SDL_WaitProcess(Handle, block, &exit);
        exitCode = exit;
        return hasExited;
    }

    /// <summary>
    /// Waits for the process to finish, blocking until it exits.
    /// </summary>
    /// <returns>The exit code of the process.</returns>
    /// <remarks>
    /// <para>The exit code will be the exit code of the process if it terminates normally,
    /// a negative signal if it terminated due to a signal, or -255 otherwise.</para>
    /// <para>If you create a process with standard output piped to the application, you should
    /// read all of the process output before calling this method, otherwise the process might
    /// block indefinitely waiting for output to be read.</para>
    /// </remarks>
    public int WaitForExit()
    {
        _ = Wait(true, out var exitCode);
        return exitCode;
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
            SDL_DestroyProcess(Handle);
            Handle = null;
        }

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
