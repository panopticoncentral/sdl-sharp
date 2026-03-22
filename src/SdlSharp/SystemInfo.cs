using System.Runtime.InteropServices;

using static SdlSharp.Native.CpuInfo;
using static SdlSharp.Native.FileSystem;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Misc;

namespace SdlSharp;

/// <summary>
/// Provides information about the system (CPU, memory, paths).
/// </summary>
public static unsafe class SystemInfo
{
    /// <summary>Gets the number of logical CPU cores available.</summary>
    public static int LogicalCpuCores => SDL_GetNumLogicalCPUCores();

    /// <summary>Gets the L1 cache line size of the CPU, in bytes.</summary>
    public static int CpuCacheLineSize => SDL_GetCPUCacheLineSize();

    /// <summary>Gets the amount of RAM configured in the system, in MB.</summary>
    public static int SystemRam => SDL_GetSystemRAM();

    /// <summary>Gets the system's page size.</summary>
    public static int SystemPageSize => SDL_GetSystemPageSize();

    /// <summary>Gets the SIMD alignment requirement.</summary>
    public static nuint SimdAlignment => SDL_GetSIMDAlignment();

    /// <summary>Whether the CPU has AltiVec features.</summary>
    public static bool HasAltiVec => SDL_HasAltiVec();
    /// <summary>Whether the CPU has MMX features.</summary>
    public static bool HasMmx => SDL_HasMMX();
    /// <summary>Whether the CPU has SSE features.</summary>
    public static bool HasSse => SDL_HasSSE();
    /// <summary>Whether the CPU has SSE2 features.</summary>
    public static bool HasSse2 => SDL_HasSSE2();
    /// <summary>Whether the CPU has SSE3 features.</summary>
    public static bool HasSse3 => SDL_HasSSE3();
    /// <summary>Whether the CPU has SSE4.1 features.</summary>
    public static bool HasSse41 => SDL_HasSSE41();
    /// <summary>Whether the CPU has SSE4.2 features.</summary>
    public static bool HasSse42 => SDL_HasSSE42();
    /// <summary>Whether the CPU has AVX features.</summary>
    public static bool HasAvx => SDL_HasAVX();
    /// <summary>Whether the CPU has AVX2 features.</summary>
    public static bool HasAvx2 => SDL_HasAVX2();
    /// <summary>Whether the CPU has AVX-512F features.</summary>
    public static bool HasAvx512F => SDL_HasAVX512F();
    /// <summary>Whether the CPU has ARM SIMD features.</summary>
    public static bool HasArmsimd => SDL_HasARMSIMD();
    /// <summary>Whether the CPU has NEON features.</summary>
    public static bool HasNeon => SDL_HasNEON();
    /// <summary>Whether the CPU has LSX (Loongson SIMD Extension) features.</summary>
    public static bool HasLsx => SDL_HasLSX();
    /// <summary>Whether the CPU has LASX (Loongson Advanced SIMD Extension) features.</summary>
    public static bool HasLasx => SDL_HasLASX();

    /// <summary>
    /// Gets the directory where the application was run from.
    /// </summary>
    public static string? BasePath =>
        Marshal.PtrToStringUTF8((nint)SDL_GetBasePath());

    /// <summary>
    /// Gets the user-and-app-specific path where files can be written.
    /// </summary>
    /// <param name="org">The organization name.</param>
    /// <param name="app">The application name.</param>
    public static string? GetPrefPath(string org, string app)
    {
        var ptr = SDL_GetPrefPath(ToUtf8(org), ToUtf8(app));
        if (ptr == null) return null;
        try
        {
            return Marshal.PtrToStringUTF8((nint)ptr);
        }
        finally
        {
            SDL_free(ptr);
        }
    }

    /// <summary>
    /// Gets a well-known system folder path.
    /// </summary>
    /// <param name="folder">The folder type to look up.</param>
    public static string? GetUserFolder(SystemFolder folder) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetUserFolder((Native.SDL_Folder)folder));

    /// <summary>
    /// Gets the current working directory.
    /// </summary>
    public static string? GetCurrentDirectory()
    {
        var ptr = SDL_GetCurrentDirectory();
        if (ptr == null) return null;
        try
        {
            return Marshal.PtrToStringUTF8((nint)ptr);
        }
        finally
        {
            SDL_free(ptr);
        }
    }

    /// <summary>
    /// Opens a URL/URI in the browser or other appropriate external application.
    /// </summary>
    /// <param name="url">The URL to open.</param>
    public static void OpenUrl(string url) =>
        Check(SDL_OpenURL(ToUtf8(url)));
}
