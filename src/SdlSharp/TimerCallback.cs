using System.Runtime.InteropServices;

namespace SdlSharp
{
    /// <summary>
    /// A callback from a timer.
    /// </summary>
    /// <param name="interval">The delay before the callback was called.</param>
    /// <param name="param">The user data.</param>
    /// <returns>The next timer interval (or <c>0</c> to cancel the timer).</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint TimerCallback(uint interval, nint param);
}
