using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static Sdl3Sharp.Native.Common;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_process.h - Process control support.
/// </summary>
public static unsafe partial class Process
{
    /// <summary>
    /// An opaque handle representing a system process.
    /// </summary>
    public struct SDL_Process
    {
    }

    /// <summary>
    /// Description of where standard I/O should be directed when creating a process.
    /// </summary>
    public enum SDL_ProcessIO
    {
        /// <summary>The I/O stream is inherited from the application.</summary>
        SDL_PROCESS_STDIO_INHERITED,
        /// <summary>The I/O stream is ignored.</summary>
        SDL_PROCESS_STDIO_NULL,
        /// <summary>The I/O stream is connected to a new SDL_IOStream that the application can read or write.</summary>
        SDL_PROCESS_STDIO_APP,
        /// <summary>The I/O stream is redirected to an existing SDL_IOStream.</summary>
        SDL_PROCESS_STDIO_REDIRECT
    }

    /// <summary>
    /// Property name for the array of strings containing the program to run, any arguments, and a NULL pointer.
    /// </summary>
    public const string SDL_PROP_PROCESS_CREATE_ARGS_POINTER = "SDL.process.create.args";

    /// <summary>
    /// Property name for an SDL_Environment pointer that will be the entire environment for the process.
    /// </summary>
    public const string SDL_PROP_PROCESS_CREATE_ENVIRONMENT_POINTER = "SDL.process.create.environment";

    /// <summary>
    /// Property name for an SDL_ProcessIO value describing where standard input for the process comes from.
    /// </summary>
    public const string SDL_PROP_PROCESS_CREATE_STDIN_NUMBER = "SDL.process.create.stdin_option";

    /// <summary>
    /// Property name for an SDL_IOStream pointer used for standard input when SDL_PROP_PROCESS_CREATE_STDIN_NUMBER is set to SDL_PROCESS_STDIO_REDIRECT.
    /// </summary>
    public const string SDL_PROP_PROCESS_CREATE_STDIN_POINTER = "SDL.process.create.stdin_source";

    /// <summary>
    /// Property name for an SDL_ProcessIO value describing where standard output for the process goes to.
    /// </summary>
    public const string SDL_PROP_PROCESS_CREATE_STDOUT_NUMBER = "SDL.process.create.stdout_option";

    /// <summary>
    /// Property name for an SDL_IOStream pointer used for standard output when SDL_PROP_PROCESS_CREATE_STDOUT_NUMBER is set to SDL_PROCESS_STDIO_REDIRECT.
    /// </summary>
    public const string SDL_PROP_PROCESS_CREATE_STDOUT_POINTER = "SDL.process.create.stdout_source";

    /// <summary>
    /// Property name for an SDL_ProcessIO value describing where standard error for the process goes to.
    /// </summary>
    public const string SDL_PROP_PROCESS_CREATE_STDERR_NUMBER = "SDL.process.create.stderr_option";

    /// <summary>
    /// Property name for an SDL_IOStream pointer used for standard error when SDL_PROP_PROCESS_CREATE_STDERR_NUMBER is set to SDL_PROCESS_STDIO_REDIRECT.
    /// </summary>
    public const string SDL_PROP_PROCESS_CREATE_STDERR_POINTER = "SDL.process.create.stderr_source";

    /// <summary>
    /// Property name for a boolean indicating if the error output of the process should be redirected into the standard output of the process.
    /// </summary>
    public const string SDL_PROP_PROCESS_CREATE_STDERR_TO_STDOUT_BOOLEAN = "SDL.process.create.stderr_to_stdout";

    /// <summary>
    /// Property name for a boolean indicating if the process should run in the background.
    /// </summary>
    public const string SDL_PROP_PROCESS_CREATE_BACKGROUND_BOOLEAN = "SDL.process.create.background";

    /// <summary>
    /// Property name for the process ID of the process.
    /// </summary>
    public const string SDL_PROP_PROCESS_PID_NUMBER = "SDL.process.pid";

    /// <summary>
    /// Property name for an SDL_IOStream that can be used to write input to the process.
    /// </summary>
    public const string SDL_PROP_PROCESS_STDIN_POINTER = "SDL.process.stdin";

    /// <summary>
    /// Property name for a non-blocking SDL_IOStream that can be used to read output from the process.
    /// </summary>
    public const string SDL_PROP_PROCESS_STDOUT_POINTER = "SDL.process.stdout";

    /// <summary>
    /// Property name for a non-blocking SDL_IOStream that can be used to read error output from the process.
    /// </summary>
    public const string SDL_PROP_PROCESS_STDERR_POINTER = "SDL.process.stderr";

    /// <summary>
    /// Property name for a boolean indicating if the process is running in the background.
    /// </summary>
    public const string SDL_PROP_PROCESS_BACKGROUND_BOOLEAN = "SDL.process.background";

    /// <summary>
    /// Create a new process.
    /// </summary>
    /// <param name="args">the path and arguments for the new process.</param>
    /// <param name="pipe_stdio">true to create pipes to the process's standard input and from the process's standard output, false for the process to have no input and inherit the application's standard output.</param>
    /// <returns>the newly created and running process, or NULL if the process couldn't be created.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Process* SDL_CreateProcess(byte** args, [MarshalAs(UnmanagedType.U1)] bool pipe_stdio);

    /// <summary>
    /// Create a new process with the specified properties.
    /// </summary>
    /// <param name="props">the properties to use.</param>
    /// <returns>the newly created and running process, or NULL if the process couldn't be created.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Process* SDL_CreateProcessWithProperties(uint props);

    /// <summary>
    /// Get the properties associated with a process.
    /// </summary>
    /// <param name="process">the process to query.</param>
    /// <returns>a valid property ID on success or 0 on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_GetProcessProperties(SDL_Process* process);

    /// <summary>
    /// Read all the output from a process.
    /// </summary>
    /// <param name="process">The process to read.</param>
    /// <param name="datasize">a pointer filled in with the number of bytes read, may be NULL.</param>
    /// <param name="exitcode">a pointer filled in with the process exit code if the process has exited, may be NULL.</param>
    /// <returns>the data or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* SDL_ReadProcess(SDL_Process* process, nuint* datasize, int* exitcode);

    /// <summary>
    /// Get the SDL_IOStream associated with process standard input.
    /// </summary>
    /// <param name="process">The process to get the input stream for.</param>
    /// <returns>the input stream or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IOStream.SDL_IOStream* SDL_GetProcessInput(SDL_Process* process);

    /// <summary>
    /// Get the SDL_IOStream associated with process standard output.
    /// </summary>
    /// <param name="process">The process to get the output stream for.</param>
    /// <returns>the output stream or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IOStream.SDL_IOStream* SDL_GetProcessOutput(SDL_Process* process);

    /// <summary>
    /// Stop a process.
    /// </summary>
    /// <param name="process">The process to stop.</param>
    /// <param name="force">true to terminate the process immediately, false to try to stop the process gracefully.</param>
    /// <returns>true on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_KillProcess(SDL_Process* process, [MarshalAs(UnmanagedType.U1)] bool force);

    /// <summary>
    /// Wait for a process to finish.
    /// </summary>
    /// <param name="process">The process to wait for.</param>
    /// <param name="block">If true, block until the process finishes; otherwise, report on the process' status.</param>
    /// <param name="exitcode">a pointer filled in with the process exit code if the process has exited, may be NULL.</param>
    /// <returns>true if the process exited, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WaitProcess(SDL_Process* process, [MarshalAs(UnmanagedType.U1)] bool block, int* exitcode);

    /// <summary>
    /// Destroy a previously created process object.
    /// </summary>
    /// <param name="process">The process object to destroy.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyProcess(SDL_Process* process);
}
