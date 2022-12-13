namespace SdlSharp
{
    /// <summary>
    /// Information about the system CPU.
    /// </summary>
    public static class CpuInfo
    {
        /// <summary>
        /// The number of CPUs.
        /// </summary>
        public static int CpuCount => Native.SDL_GetCPUCount();

        /// <summary>
        /// The cache line size.
        /// </summary>
        public static int CpuCacheLineSize => Native.SDL_GetCPUCacheLineSize();

        /// <summary>
        /// Whether the CPU supports RDTSC.
        /// </summary>
        public static bool HasRdtsc => Native.SDL_HasRDTSC();

        /// <summary>
        /// Whether the CPU supports AltiVec
        /// </summary>
        public static bool HasAltiVec => Native.SDL_HasAltiVec();

        /// <summary>
        /// Whether the CPU supports MMX.
        /// </summary>
        public static bool HasMmx => Native.SDL_HasMMX();

        /// <summary>
        /// Whether the CPU supports 3DNow.
        /// </summary>
        public static bool Has3dNow => Native.SDL_Has3DNow();

        /// <summary>
        /// Whether the CPU supports SSE.
        /// </summary>
        public static bool HasSse => Native.SDL_HasSSE();

        /// <summary>
        /// Whether the CPU supports SSE2.
        /// </summary>
        public static bool HasSse2 => Native.SDL_HasSSE2();

        /// <summary>
        /// Whether the CPU supports SSE3.
        /// </summary>
        public static bool HasSse3 => Native.SDL_HasSSE3();

        /// <summary>
        /// Whether the CPU supports SSE4.1.
        /// </summary>
        public static bool HasSse41 => Native.SDL_HasSSE41();

        /// <summary>
        /// Whether the CPU supports SSE4.2.
        /// </summary>
        public static bool HasSse42 => Native.SDL_HasSSE42();

        /// <summary>
        /// Whether the CPU supports AVX.
        /// </summary>
        public static bool HasAvx => Native.SDL_HasAVX();

        /// <summary>
        /// Whether the CPU supports AVX2.
        /// </summary>
        public static bool HasAvx2 => Native.SDL_HasAVX2();

        /// <summary>
        /// Whether the CPU supports AVX512F.
        /// </summary>
        public static bool HasAvx512F => Native.SDL_HasAVX512F();

        /// <summary>
        /// Whether the CPU supports ARM SIMD (ARMv6).
        /// </summary>
        public static bool HasARMSIMD => Native.SDL_HasARMSIMD();

        /// <summary>
        /// Whether the CPU supports NEON.
        /// </summary>
        public static bool HasNeon => Native.SDL_HasNEON();

        /// <summary>
        /// Whether the CPU supports LSX.
        /// </summary>
        public static bool HasLsx => Native.SDL_HasLSX();

        /// <summary>
        /// Whether the CPU supports LASX.
        /// </summary>
        public static bool HasLasx => Native.SDL_HasLASX();

        /// <summary>
        /// The amount of system RAM.
        /// </summary>
        public static int SystemRam => Native.SDL_GetSystemRAM();
    }
}
