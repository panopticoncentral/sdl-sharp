using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Skipped (macros — compile-time only, no exported symbols):
//   SDL_ASSERT_LEVEL, SDL_TriggerBreakpoint, SDL_FUNCTION, SDL_FILE, SDL_ASSERT_FILE, SDL_LINE,
//   SDL_NULL_WHILE_LOOP_CONDITION, SDL_disabled_assert, SDL_enabled_assert, SDL_AssertBreakpoint,
//   SDL_assert, SDL_assert_release, SDL_assert_paranoid, SDL_assert_always.
//   .NET has Debug.Assert / Trace.Assert for application-level assertions.
//
// Skipped (internal): SDL_ReportAssertion — "never call this directly", used by the assert macros.

/// <summary>
/// Possible outcomes from a triggered assertion.
/// </summary>
public enum SDL_AssertState
{
    /// <summary>Retry the assert immediately.</summary>
    SDL_ASSERTION_RETRY = 0,

    /// <summary>Make the debugger trigger a breakpoint.</summary>
    SDL_ASSERTION_BREAK = 1,

    /// <summary>Terminate the program.</summary>
    SDL_ASSERTION_ABORT = 2,

    /// <summary>Ignore the assert.</summary>
    SDL_ASSERTION_IGNORE = 3,

    /// <summary>Ignore the assert from now on.</summary>
    SDL_ASSERTION_ALWAYS_IGNORE = 4,
}

/// <summary>
/// Information about an assertion failure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_AssertData
{
    /// <summary>true if app should always continue when assertion is triggered.</summary>
    [MarshalAs(UnmanagedType.U1)]
    public bool always_ignore;

    /// <summary>Number of times this assertion has been triggered.</summary>
    public uint trigger_count;

    /// <summary>A string of this assert's test code.</summary>
    public byte* condition;

    /// <summary>The source file where this assert lives.</summary>
    public byte* filename;

    /// <summary>The line in filename where this assert lives.</summary>
    public int linenum;

    /// <summary>The name of the function where this assert lives.</summary>
    public byte* function;

    /// <summary>Next item in the linked list.</summary>
    public SDL_AssertData* next;
}

/// <summary>
/// Native bindings for SDL_assert.h — assertion handler and reporting.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Assert
{
    /// <summary>
    /// Set an application-defined assertion handler.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetAssertionHandler")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetAssertionHandler(
        delegate* unmanaged[Cdecl]<SDL_AssertData*, void*, SDL_AssertState> handler,
        void* userdata);

    /// <summary>
    /// Get the default assertion handler.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetDefaultAssertionHandler")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial delegate* unmanaged[Cdecl]<SDL_AssertData*, void*, SDL_AssertState> SDL_GetDefaultAssertionHandler();

    /// <summary>
    /// Get the current assertion handler.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAssertionHandler")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial delegate* unmanaged[Cdecl]<SDL_AssertData*, void*, SDL_AssertState> SDL_GetAssertionHandler(void** puserdata);

    /// <summary>
    /// Get a list of all assertion failures.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAssertionReport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_AssertData* SDL_GetAssertionReport();

    /// <summary>
    /// Clear the list of all assertion failures.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ResetAssertionReport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ResetAssertionReport();
}
