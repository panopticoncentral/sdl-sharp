using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// A 128-bit device GUID (bus type, vendor, product encoding — not an RFC 4122 UUID).
/// Stored as two ulongs for blittability; layout-identical to the C Uint8 data[16].
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GUID
{
    public ulong data0;
    public ulong data1;
}

/// <summary>
/// Native bindings for SDL_guid.h.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Guid
{
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GUIDToString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_GUIDToString(SDL_GUID guid, byte* pszGUID, int cbGUID);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_StringToGUID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GUID SDL_StringToGUID(ReadOnlySpan<byte> pchGUID);
}
