using System.Runtime.InteropServices;

namespace SdlSharp
{
    /// <summary>
    /// A callback when a hint is changed.
    /// </summary>
    /// <param name="userdata">The user data for the callback.</param>
    /// <param name="name">The name of the hint.</param>
    /// <param name="oldValue">The old value of the hint.</param>
    /// <param name="newValue">The new value of the hint.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public delegate void HintCallback(nint userdata, string name, string oldValue, string newValue);
}
