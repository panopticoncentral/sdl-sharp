using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_cpuinfo.h - CPU feature detection for SDL.
/// </summary>
public static partial class CpuInfo
{
    /// <summary>
    /// A guess for the cacheline size used for padding.
    /// Most x86 processors have a 64 byte cache line. The 64-bit PowerPC
    /// processors have a 128 byte cache line. The larger value is used to be
    /// generally safe.
    /// </summary>
    public const int SDL_CACHELINE_SIZE = 128;

    /// <summary>
    /// Get the number of logical CPU cores available.
    /// </summary>
    /// <returns>The total number of logical CPU cores. On CPUs that include
    /// technologies such as hyperthreading, the number of logical cores
    /// may be more than the number of physical cores.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumLogicalCPUCores();

    /// <summary>
    /// Determine the L1 cache line size of the CPU.
    /// This is useful for determining multi-threaded structure padding or SIMD
    /// prefetch sizes.
    /// </summary>
    /// <returns>The L1 cache line size of the CPU, in bytes.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetCPUCacheLineSize();

    /// <summary>
    /// Determine whether the CPU has AltiVec features.
    /// This always returns false on CPUs that aren't using PowerPC instruction sets.
    /// </summary>
    /// <returns>True if the CPU has AltiVec features or false if not.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasAltiVec();

    /// <summary>
    /// Determine whether the CPU has MMX features.
    /// This always returns false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    /// <returns>True if the CPU has MMX features or false if not.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasMMX();

    /// <summary>
    /// Determine whether the CPU has SSE features.
    /// This always returns false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    /// <returns>True if the CPU has SSE features or false if not.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasSSE();

    /// <summary>
    /// Determine whether the CPU has SSE2 features.
    /// This always returns false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    /// <returns>True if the CPU has SSE2 features or false if not.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasSSE2();

    /// <summary>
    /// Determine whether the CPU has SSE3 features.
    /// This always returns false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    /// <returns>True if the CPU has SSE3 features or false if not.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasSSE3();

    /// <summary>
    /// Determine whether the CPU has SSE4.1 features.
    /// This always returns false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    /// <returns>True if the CPU has SSE4.1 features or false if not.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasSSE41();

    /// <summary>
    /// Determine whether the CPU has SSE4.2 features.
    /// This always returns false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    /// <returns>True if the CPU has SSE4.2 features or false if not.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasSSE42();

    /// <summary>
    /// Determine whether the CPU has AVX features.
    /// This always returns false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    /// <returns>True if the CPU has AVX features or false if not.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasAVX();

    /// <summary>
    /// Determine whether the CPU has AVX2 features.
    /// This always returns false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    /// <returns>True if the CPU has AVX2 features or false if not.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasAVX2();

    /// <summary>
    /// Determine whether the CPU has AVX-512F (foundation) features.
    /// This always returns false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    /// <returns>True if the CPU has AVX-512F features or false if not.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasAVX512F();

    /// <summary>
    /// Determine whether the CPU has ARM SIMD (ARMv6) features.
    /// This is different from ARM NEON, which is a different instruction set.
    /// This always returns false on CPUs that aren't using ARM instruction sets.
    /// </summary>
    /// <returns>True if the CPU has ARM SIMD features or false if not.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasARMSIMD();

    /// <summary>
    /// Determine whether the CPU has NEON (ARM SIMD) features.
    /// This always returns false on CPUs that aren't using ARM instruction sets.
    /// </summary>
    /// <returns>True if the CPU has ARM NEON features or false if not.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasNEON();

    /// <summary>
    /// Determine whether the CPU has LSX (LOONGARCH SIMD) features.
    /// This always returns false on CPUs that aren't using LOONGARCH instruction sets.
    /// </summary>
    /// <returns>True if the CPU has LOONGARCH LSX features or false if not.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasLSX();

    /// <summary>
    /// Determine whether the CPU has LASX (LOONGARCH SIMD) features.
    /// This always returns false on CPUs that aren't using LOONGARCH instruction sets.
    /// </summary>
    /// <returns>True if the CPU has LOONGARCH LASX features or false if not.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasLASX();

    /// <summary>
    /// Get the amount of RAM configured in the system.
    /// </summary>
    /// <returns>The amount of RAM configured in the system in MiB.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetSystemRAM();

    /// <summary>
    /// Report the alignment this system needs for SIMD allocations.
    /// This will return the minimum number of bytes to which a pointer must be
    /// aligned to be compatible with SIMD instructions on the current machine.
    /// For example, if the machine supports SSE only, it will return 16, but if it
    /// supports AVX-512F, it'll return 64 (etc). This only reports values for
    /// instruction sets SDL knows about, so if your SDL build doesn't have
    /// SDL_HasAVX512F(), then it might return 16 for the SSE support it sees and
    /// not 64 for the AVX-512 instructions that exist but SDL doesn't know about.
    /// </summary>
    /// <returns>The alignment in bytes needed for available, known SIMD instructions.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint SDL_GetSIMDAlignment();
}
