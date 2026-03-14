using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// Native bindings for SDL_cpuinfo.h — CPU feature detection.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class CpuInfo
{
    /// <summary>Get the number of logical CPU cores available.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetNumLogicalCPUCores")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumLogicalCPUCores();

    /// <summary>Determine the L1 cache line size of the CPU, in bytes.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCPUCacheLineSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetCPUCacheLineSize();

    /// <summary>Determine whether the CPU has AltiVec features.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasAltiVec")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasAltiVec();

    /// <summary>Determine whether the CPU has MMX features.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasMMX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasMMX();

    /// <summary>Determine whether the CPU has SSE features.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasSSE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasSSE();

    /// <summary>Determine whether the CPU has SSE2 features.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasSSE2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasSSE2();

    /// <summary>Determine whether the CPU has SSE3 features.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasSSE3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasSSE3();

    /// <summary>Determine whether the CPU has SSE4.1 features.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasSSE41")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasSSE41();

    /// <summary>Determine whether the CPU has SSE4.2 features.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasSSE42")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasSSE42();

    /// <summary>Determine whether the CPU has AVX features.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasAVX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasAVX();

    /// <summary>Determine whether the CPU has AVX2 features.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasAVX2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasAVX2();

    /// <summary>Determine whether the CPU has AVX-512F features.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasAVX512F")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasAVX512F();

    /// <summary>Determine whether the CPU has ARM SIMD (ARMv6) features.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasARMSIMD")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasARMSIMD();

    /// <summary>Determine whether the CPU has NEON features.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasNEON")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasNEON();

    /// <summary>Determine whether the CPU has LSX (Loongson SIMD Extension) features.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasLSX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasLSX();

    /// <summary>Determine whether the CPU has LASX (Loongson Advanced SIMD Extension) features.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasLASX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasLASX();

    /// <summary>Get the amount of RAM configured in the system, in MB.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSystemRAM")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetSystemRAM();

    /// <summary>Report the alignment this system needs for SIMD allocations.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSIMDAlignment")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint SDL_GetSIMDAlignment();

    /// <summary>Get the system's page size.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSystemPageSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetSystemPageSize();
}
