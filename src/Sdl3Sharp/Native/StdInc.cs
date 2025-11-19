using System.Runtime.InteropServices;
using static Sdl3Sharp.Native.Common;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_stdinc.h - SDL memory allocation functions.
/// </summary>
public static unsafe partial class StdInc
{
    // Most functions from SDL_stdinc.h are not wrapped as .NET provides equivalent functionality:
    // - Environment variables: Use System.Environment
    // - String functions: Use System.String methods
    // - Character classification: Use System.Char methods
    // - Math functions: Use System.Math
    // - Random functions: Use System.Random
    // - Sorting/searching: Use LINQ or Array.Sort/Array.BinarySearch
    // - Checksums: Use System.Security.Cryptography or third-party libraries
    // - Memory operations: Use System.Buffer or Marshal methods
    // - UTF-8 encoding: Use System.Text.Encoding.UTF8
    // - String parsing: Use int.Parse, double.Parse, etc.

    // Only SDL memory allocation functions are wrapped since they integrate with SDL's memory tracking

    // SDL_malloc_func is not wrapped - callback type for internal use
    // SDL_calloc_func is not wrapped - callback type for internal use
    // SDL_realloc_func is not wrapped - callback type for internal use
    // SDL_free_func is not wrapped - callback type for internal use

    /// <summary>
    /// Allocate uninitialized memory.
    /// </summary>
    /// <param name="size">the size to allocate.</param>
    /// <returns>a pointer to the allocated memory, or NULL if allocation failed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void* SDL_malloc(nuint size);

    /// <summary>
    /// Allocate a zero-initialized array.
    /// </summary>
    /// <param name="nmemb">the number of elements in the array.</param>
    /// <param name="size">the size of each element of the array.</param>
    /// <returns>a pointer to the allocated array, or NULL if allocation failed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void* SDL_calloc(nuint nmemb, nuint size);

    /// <summary>
    /// Change the size of allocated memory.
    /// </summary>
    /// <param name="mem">a pointer to allocated memory to reallocate, or NULL.</param>
    /// <param name="size">the new size of the memory.</param>
    /// <returns>a pointer to the newly allocated memory, or NULL if allocation failed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void* SDL_realloc(void* mem, nuint size);

    /// <summary>
    /// Free allocated memory.
    /// </summary>
    /// <param name="mem">a pointer to allocated memory, or NULL.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_free(void* mem);

    // SDL_GetOriginalMemoryFunctions is not wrapped - requires passing function pointers as out parameters
    // SDL_GetMemoryFunctions is not wrapped - requires passing function pointers as out parameters
    // SDL_SetMemoryFunctions is not wrapped - requires passing function pointers as parameters

    /// <summary>
    /// Allocate memory aligned to a specific alignment.
    /// </summary>
    /// <param name="alignment">the alignment of the memory.</param>
    /// <param name="size">the size to allocate.</param>
    /// <returns>a pointer to the aligned memory, or NULL if allocation failed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void* SDL_aligned_alloc(nuint alignment, nuint size);

    /// <summary>
    /// Free memory allocated by SDL_aligned_alloc().
    /// </summary>
    /// <param name="mem">a pointer previously returned by SDL_aligned_alloc(), or NULL.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_aligned_free(void* mem);

    /// <summary>
    /// Get the number of outstanding (unfreed) allocations.
    /// </summary>
    /// <returns>the number of allocations or -1 if allocation counting is disabled.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int SDL_GetNumAllocations();
}
