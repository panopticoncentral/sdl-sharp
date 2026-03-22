using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static SdlSharp.Native.Assert;

namespace SdlSharp;

/// <summary>
/// Provides access to SDL's assertion handler and assertion report.
/// </summary>
public static unsafe class SdlAssert
{
    private static GCHandle _callbackHandle;

    /// <summary>
    /// Sets a managed callback to handle SDL assertion failures.
    /// </summary>
    /// <param name="handler">
    /// The handler to invoke when an SDL assertion fails, or null to reset to the default handler.
    /// The handler receives an <see cref="AssertionData"/> describing the failure and returns an
    /// <see cref="AssertState"/> indicating how SDL should respond.
    /// </param>
    public static void SetAssertionHandler(Func<AssertionData, AssertState>? handler)
    {
        if (_callbackHandle.IsAllocated)
            _callbackHandle.Free();

        if (handler == null)
        {
            SDL_SetAssertionHandler(null, null);
            return;
        }

        _callbackHandle = GCHandle.Alloc(handler);
        SDL_SetAssertionHandler(&NativeAssertionHandler, (void*)(nint)_callbackHandle);
    }

    /// <summary>
    /// Gets all assertion failures since the last call to <see cref="ResetAssertionReport"/>
    /// or the start of the program.
    /// </summary>
    /// <returns>A list of all failed assertions.</returns>
    public static IReadOnlyList<AssertionData> GetAssertionReport()
    {
        var list = new List<AssertionData>();
        var item = SDL_GetAssertionReport();

        while (item != null)
        {
            list.Add(MarshalAssertData(item));
            item = item->next;
        }

        return list;
    }

    /// <summary>
    /// Clears the list of all assertion failures.
    /// </summary>
    public static void ResetAssertionReport() => SDL_ResetAssertionReport();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static Native.SDL_AssertState NativeAssertionHandler(Native.SDL_AssertData* data, void* userdata)
    {
        var handle = GCHandle.FromIntPtr((nint)userdata);
        var handler = (Func<AssertionData, AssertState>)handle.Target!;
        var result = handler(MarshalAssertData(data));
        return (Native.SDL_AssertState)result;
    }

    private static AssertionData MarshalAssertData(Native.SDL_AssertData* data) =>
        new(
            AlwaysIgnore: data->always_ignore,
            TriggerCount: data->trigger_count,
            Condition: Marshal.PtrToStringUTF8((nint)data->condition),
            Filename: Marshal.PtrToStringUTF8((nint)data->filename),
            LineNumber: data->linenum,
            Function: Marshal.PtrToStringUTF8((nint)data->function));
}
