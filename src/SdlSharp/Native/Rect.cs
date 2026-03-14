using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// SDL_FORCE_INLINE functions are not exported from the shared library and cannot be P/Invoked.
// Skipped: SDL_RectToFRect, SDL_PointInRect, SDL_RectEmpty, SDL_RectsEqual,
//          SDL_PointInRectFloat, SDL_RectEmptyFloat, SDL_RectsEqualFloat, SDL_RectsEqualEpsilon.
// These are reimplemented as C# methods on the high-level value types in SdlSharp.Graphics.

/// <summary>
/// A point (using integers).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_Point
{
    /// <summary>X coordinate.</summary>
    public int x;

    /// <summary>Y coordinate.</summary>
    public int y;
}

/// <summary>
/// A point (using floating point values).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_FPoint
{
    /// <summary>X coordinate.</summary>
    public float x;

    /// <summary>Y coordinate.</summary>
    public float y;
}

/// <summary>
/// A rectangle, with the origin at the upper left (using integers).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_Rect
{
    /// <summary>X position of the upper left corner.</summary>
    public int x;

    /// <summary>Y position of the upper left corner.</summary>
    public int y;

    /// <summary>Width.</summary>
    public int w;

    /// <summary>Height.</summary>
    public int h;
}

/// <summary>
/// A rectangle (using floating point values).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_FRect
{
    /// <summary>X position of the upper left corner.</summary>
    public float x;

    /// <summary>Y position of the upper left corner.</summary>
    public float y;

    /// <summary>Width.</summary>
    public float w;

    /// <summary>Height.</summary>
    public float h;
}

/// <summary>
/// Native bindings for SDL_rect.h — rectangle and point functions.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Rect
{
    /// <summary>
    /// Determine whether two rectangles intersect.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasRectIntersection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasRectIntersection(in SDL_Rect A, in SDL_Rect B);

    /// <summary>
    /// Calculate the intersection of two rectangles.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRectIntersection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRectIntersection(in SDL_Rect A, in SDL_Rect B, out SDL_Rect result);

    /// <summary>
    /// Calculate the union of two rectangles.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRectUnion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRectUnion(in SDL_Rect A, in SDL_Rect B, out SDL_Rect result);

    /// <summary>
    /// Calculate a minimal rectangle enclosing a set of points.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRectEnclosingPoints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetRectEnclosingPoints(SDL_Point* points, int count, in SDL_Rect clip, out SDL_Rect result);

    /// <summary>
    /// Calculate a minimal rectangle enclosing a set of points (nullable clip variant).
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRectEnclosingPoints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetRectEnclosingPoints(SDL_Point* points, int count, SDL_Rect* clip, out SDL_Rect result);

    /// <summary>
    /// Calculate the intersection of a rectangle and line segment.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRectAndLineIntersection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRectAndLineIntersection(in SDL_Rect rect, ref int X1, ref int Y1, ref int X2, ref int Y2);

    // Float variants

    /// <summary>
    /// Determine whether two floating point rectangles intersect.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasRectIntersectionFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasRectIntersectionFloat(in SDL_FRect A, in SDL_FRect B);

    /// <summary>
    /// Calculate the intersection of two floating point rectangles.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRectIntersectionFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRectIntersectionFloat(in SDL_FRect A, in SDL_FRect B, out SDL_FRect result);

    /// <summary>
    /// Calculate the union of two floating point rectangles.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRectUnionFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRectUnionFloat(in SDL_FRect A, in SDL_FRect B, out SDL_FRect result);

    /// <summary>
    /// Calculate a minimal rectangle enclosing a set of floating point points.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRectEnclosingPointsFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetRectEnclosingPointsFloat(SDL_FPoint* points, int count, in SDL_FRect clip, out SDL_FRect result);

    /// <summary>
    /// Calculate a minimal rectangle enclosing a set of floating point points (nullable clip variant).
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRectEnclosingPointsFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetRectEnclosingPointsFloat(SDL_FPoint* points, int count, SDL_FRect* clip, out SDL_FRect result);

    /// <summary>
    /// Calculate the intersection of a floating point rectangle and line segment.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRectAndLineIntersectionFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRectAndLineIntersectionFloat(in SDL_FRect rect, ref float X1, ref float Y1, ref float X2, ref float Y2);
}
