using static Sdl3Sharp.Native.CpuInfo;

namespace Sdl3Sharp;

/// <summary>
/// Provides information about the system's CPU and SIMD capabilities.
/// </summary>
public static class CpuInfo
{
    /// <summary>
    /// A guess for the cacheline size used for padding.
    /// Most x86 processors have a 64 byte cache line. The 64-bit PowerPC
    /// processors have a 128 byte cache line. The larger value is used to be
    /// generally safe.
    /// </summary>
    public const int CacheLineSize = SDL_CACHELINE_SIZE;

    /// <summary>
    /// Gets the number of logical CPU cores available.
    /// On CPUs that include technologies such as hyperthreading, the number of logical cores
    /// may be more than the number of physical cores.
    /// </summary>
    public static int LogicalCoreCount => SDL_GetNumLogicalCPUCores();

    /// <summary>
    /// Gets the L1 cache line size of the CPU in bytes.
    /// This is useful for determining multi-threaded structure padding or SIMD prefetch sizes.
    /// </summary>
    public static int CacheLineSizeActual => SDL_GetCPUCacheLineSize();

    /// <summary>
    /// Gets the amount of RAM configured in the system in MiB.
    /// </summary>
    public static int SystemRamMiB => SDL_GetSystemRAM();

    /// <summary>
    /// Gets the alignment this system needs for SIMD allocations.
    /// This is the minimum number of bytes to which a pointer must be aligned to be compatible
    /// with SIMD instructions on the current machine.
    /// </summary>
    public static nuint SimdAlignment => SDL_GetSIMDAlignment();

    /// <summary>
    /// Gets a value indicating whether the CPU has AltiVec features.
    /// This is always false on CPUs that aren't using PowerPC instruction sets.
    /// </summary>
    public static bool HasAltiVec => SDL_HasAltiVec();

    /// <summary>
    /// Gets a value indicating whether the CPU has MMX features.
    /// This is always false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    public static bool HasMmx => SDL_HasMMX();

    /// <summary>
    /// Gets a value indicating whether the CPU has SSE features.
    /// This is always false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    public static bool HasSse => SDL_HasSSE();

    /// <summary>
    /// Gets a value indicating whether the CPU has SSE2 features.
    /// This is always false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    public static bool HasSse2 => SDL_HasSSE2();

    /// <summary>
    /// Gets a value indicating whether the CPU has SSE3 features.
    /// This is always false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    public static bool HasSse3 => SDL_HasSSE3();

    /// <summary>
    /// Gets a value indicating whether the CPU has SSE4.1 features.
    /// This is always false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    public static bool HasSse41 => SDL_HasSSE41();

    /// <summary>
    /// Gets a value indicating whether the CPU has SSE4.2 features.
    /// This is always false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    public static bool HasSse42 => SDL_HasSSE42();

    /// <summary>
    /// Gets a value indicating whether the CPU has AVX features.
    /// This is always false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    public static bool HasAvx => SDL_HasAVX();

    /// <summary>
    /// Gets a value indicating whether the CPU has AVX2 features.
    /// This is always false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    public static bool HasAvx2 => SDL_HasAVX2();

    /// <summary>
    /// Gets a value indicating whether the CPU has AVX-512F (foundation) features.
    /// This is always false on CPUs that aren't using Intel instruction sets.
    /// </summary>
    public static bool HasAvx512F => SDL_HasAVX512F();

    /// <summary>
    /// Gets a value indicating whether the CPU has ARM SIMD (ARMv6) features.
    /// This is different from ARM NEON, which is a different instruction set.
    /// This is always false on CPUs that aren't using ARM instruction sets.
    /// </summary>
    public static bool HasArmSimd => SDL_HasARMSIMD();

    /// <summary>
    /// Gets a value indicating whether the CPU has NEON (ARM SIMD) features.
    /// This is always false on CPUs that aren't using ARM instruction sets.
    /// </summary>
    public static bool HasNeon => SDL_HasNEON();

    /// <summary>
    /// Gets a value indicating whether the CPU has LSX (LOONGARCH SIMD) features.
    /// This is always false on CPUs that aren't using LOONGARCH instruction sets.
    /// </summary>
    public static bool HasLsx => SDL_HasLSX();

    /// <summary>
    /// Gets a value indicating whether the CPU has LASX (LOONGARCH SIMD) features.
    /// This is always false on CPUs that aren't using LOONGARCH instruction sets.
    /// </summary>
    public static bool HasLasx => SDL_HasLASX();
}
