using System.Runtime.InteropServices;
using static Sdl3Sharp.Native.Common;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_rect.h - Rectangle and 2D point helper functions.
/// </summary>
public static unsafe partial class Rect
{
    /// <summary>
    /// The structure that defines a point (using integers).
    /// </summary>
    /// <param name="x">The x coordinate of the point.</param>
    /// <param name="y">The y coordinate of the point.</param>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Point(int x, int y)
    {
        /// <summary>
        /// The x coordinate of the point.
        /// </summary>
        public int x = x;

        /// <summary>
        /// The y coordinate of the point.
        /// </summary>
        public int y = y;
    }

    /// <summary>
    /// The structure that defines a point (using floating point values).
    /// </summary>
    /// <param name="x">The x coordinate of the point.</param>
    /// <param name="y">The y coordinate of the point.</param>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_FPoint(float x, float y)
    {
        /// <summary>
        /// The x coordinate of the point.
        /// </summary>
        public float x = x;

        /// <summary>
        /// The y coordinate of the point.
        /// </summary>
        public float y = y;
    }

    /// <summary>
    /// A rectangle, with the origin at the upper left (using integers).
    /// </summary>
    /// <param name="x">The x coordinate of the rectangle.</param>
    /// <param name="y">The y coordinate of the rectangle.</param>
    /// <param name="w">The width of the rectangle.</param>
    /// <param name="h">The height of the rectangle.</param>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Rect(int x, int y, int w, int h)
    {
        /// <summary>
        /// The x coordinate of the rectangle.
        /// </summary>
        public int x = x;

        /// <summary>
        /// The y coordinate of the rectangle.
        /// </summary>
        public int y = y;

        /// <summary>
        /// The width of the rectangle.
        /// </summary>
        public int w = w;

        /// <summary>
        /// The height of the rectangle.
        /// </summary>
        public int h = h;
    }

    /// <summary>
    /// A rectangle, with the origin at the upper left (using floating point values).
    /// </summary>
    /// <param name="x">The x coordinate of the rectangle.</param>
    /// <param name="y">The y coordinate of the rectangle.</param>
    /// <param name="w">The width of the rectangle.</param>
    /// <param name="h">The height of the rectangle.</param>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_FRect(float x, float y, float w, float h)
    {
        /// <summary>
        /// The x coordinate of the rectangle.
        /// </summary>
        public float x = x;

        /// <summary>
        /// The y coordinate of the rectangle.
        /// </summary>
        public float y = y;

        /// <summary>
        /// The width of the rectangle.
        /// </summary>
        public float w = w;

        /// <summary>
        /// The height of the rectangle.
        /// </summary>
        public float h = h;
    }

    /// <summary>
    /// Convert an SDL_Rect to SDL_FRect.
    /// </summary>
    /// <param name="rect">a pointer to an SDL_Rect.</param>
    /// <param name="frect">a pointer filled in with the floating point representation of rect.</param>
    /// <remarks>This function is available since SDL 3.2.0.</remarks>
    public static void SDL_RectToFRect(SDL_Rect* rect, SDL_FRect* frect)
    {
        frect->x = rect->x;
        frect->y = rect->y;
        frect->w = rect->w;
        frect->h = rect->h;
    }

    /// <summary>
    /// Determine whether a point resides inside a rectangle.
    /// </summary>
    /// <param name="p">the point to test.</param>
    /// <param name="r">the rectangle to test.</param>
    /// <returns>true if p is contained by r, false otherwise.</returns>
    /// <remarks>
    /// <para>A point is considered part of a rectangle if both p and r are not NULL,
    /// and p's x and y coordinates are &gt;= to the rectangle's top left corner,
    /// and &lt; the rectangle's x+w and y+h. So a 1x1 rectangle considers point (0,0)
    /// as "inside" and (0,1) as not.</para>
    /// <para>This function is available since SDL 3.2.0.</para>
    /// </remarks>
    public static bool SDL_PointInRect(SDL_Point* p, SDL_Rect* r)
    {
        return (p != null) && (r != null) && (p->x >= r->x) && (p->x < (r->x + r->w)) &&
               (p->y >= r->y) && (p->y < (r->y + r->h));
    }

    /// <summary>
    /// Determine whether a rectangle has no area.
    /// </summary>
    /// <param name="r">the rectangle to test.</param>
    /// <returns>true if the rectangle is "empty", false otherwise.</returns>
    /// <remarks>
    /// <para>A rectangle is considered "empty" for this function if r is NULL, or if
    /// r's width and/or height are &lt;= 0.</para>
    /// <para>This function is available since SDL 3.2.0.</para>
    /// </remarks>
    public static bool SDL_RectEmpty(SDL_Rect* r)
    {
        return (r == null) || (r->w <= 0) || (r->h <= 0);
    }

    /// <summary>
    /// Determine whether two rectangles are equal.
    /// </summary>
    /// <param name="a">the first rectangle to test.</param>
    /// <param name="b">the second rectangle to test.</param>
    /// <returns>true if the rectangles are equal, false otherwise.</returns>
    /// <remarks>
    /// <para>Rectangles are considered equal if both are not NULL and each of their x,
    /// y, width and height match.</para>
    /// <para>This function is available since SDL 3.2.0.</para>
    /// </remarks>
    public static bool SDL_RectsEqual(SDL_Rect* a, SDL_Rect* b)
    {
        return (a != null) && (b != null) && (a->x == b->x) && (a->y == b->y) &&
               (a->w == b->w) && (a->h == b->h);
    }

    /// <summary>
    /// Determine whether two rectangles intersect.
    /// </summary>
    /// <param name="A">an SDL_Rect structure representing the first rectangle.</param>
    /// <param name="B">an SDL_Rect structure representing the second rectangle.</param>
    /// <returns>true if there is an intersection, false otherwise.</returns>
    /// <remarks>
    /// <para>If either pointer is NULL the function will return false.</para>
    /// <para>This function is available since SDL 3.2.0.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasRectIntersection(SDL_Rect* A, SDL_Rect* B);

    /// <summary>
    /// Calculate the intersection of two rectangles.
    /// </summary>
    /// <param name="A">an SDL_Rect structure representing the first rectangle.</param>
    /// <param name="B">an SDL_Rect structure representing the second rectangle.</param>
    /// <param name="result">an SDL_Rect structure filled in with the intersection of rectangles A and B.</param>
    /// <returns>true if there is an intersection, false otherwise.</returns>
    /// <remarks>
    /// <para>If result is NULL then this function will return false.</para>
    /// <para>This function is available since SDL 3.2.0.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRectIntersection(SDL_Rect* A, SDL_Rect* B, SDL_Rect* result);

    /// <summary>
    /// Calculate the union of two rectangles.
    /// </summary>
    /// <param name="A">an SDL_Rect structure representing the first rectangle.</param>
    /// <param name="B">an SDL_Rect structure representing the second rectangle.</param>
    /// <param name="result">an SDL_Rect structure filled in with the union of rectangles A and B.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    /// <remarks>This function is available since SDL 3.2.0.</remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRectUnion(SDL_Rect* A, SDL_Rect* B, SDL_Rect* result);

    /// <summary>
    /// Calculate a minimal rectangle enclosing a set of points.
    /// </summary>
    /// <param name="points">an array of SDL_Point structures representing points to be enclosed.</param>
    /// <param name="count">the number of structures in the points array.</param>
    /// <param name="clip">an SDL_Rect used for clipping or NULL to enclose all points.</param>
    /// <param name="result">an SDL_Rect structure filled in with the minimal enclosing rectangle.</param>
    /// <returns>true if any points were enclosed or false if all the points were outside of the clipping rectangle.</returns>
    /// <remarks>
    /// <para>If clip is not NULL then only points inside of the clipping rectangle are considered.</para>
    /// <para>This function is available since SDL 3.2.0.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRectEnclosingPoints(SDL_Point* points, int count, SDL_Rect* clip, SDL_Rect* result);

    /// <summary>
    /// Calculate the intersection of a rectangle and line segment.
    /// </summary>
    /// <param name="rect">an SDL_Rect structure representing the rectangle to intersect.</param>
    /// <param name="X1">a pointer to the starting X-coordinate of the line.</param>
    /// <param name="Y1">a pointer to the starting Y-coordinate of the line.</param>
    /// <param name="X2">a pointer to the ending X-coordinate of the line.</param>
    /// <param name="Y2">a pointer to the ending Y-coordinate of the line.</param>
    /// <returns>true if there is an intersection, false otherwise.</returns>
    /// <remarks>
    /// <para>This function is used to clip a line segment to a rectangle. A line segment
    /// contained entirely within the rectangle or that does not intersect will
    /// remain unchanged. A line segment that crosses the rectangle at either or
    /// both ends will be clipped to the boundary of the rectangle and the new
    /// coordinates saved in X1, Y1, X2, and/or Y2 as necessary.</para>
    /// <para>This function is available since SDL 3.2.0.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRectAndLineIntersection(SDL_Rect* rect, int* X1, int* Y1, int* X2, int* Y2);

    /// <summary>
    /// Determine whether a point resides inside a floating point rectangle.
    /// </summary>
    /// <param name="p">the point to test.</param>
    /// <param name="r">the rectangle to test.</param>
    /// <returns>true if p is contained by r, false otherwise.</returns>
    /// <remarks>
    /// <para>A point is considered part of a rectangle if both p and r are not NULL,
    /// and p's x and y coordinates are &gt;= to the rectangle's top left corner,
    /// and &lt;= the rectangle's x+w and y+h. So a 1x1 rectangle considers point
    /// (0,0) and (0,1) as "inside" and (0,2) as not.</para>
    /// <para>This function is available since SDL 3.2.0.</para>
    /// </remarks>
    public static bool SDL_PointInRectFloat(SDL_FPoint* p, SDL_FRect* r)
    {
        return (p != null) && (r != null) && (p->x >= r->x) && (p->x <= (r->x + r->w)) &&
               (p->y >= r->y) && (p->y <= (r->y + r->h));
    }

    /// <summary>
    /// Determine whether a floating point rectangle can contain any point.
    /// </summary>
    /// <param name="r">the rectangle to test.</param>
    /// <returns>true if the rectangle is "empty", false otherwise.</returns>
    /// <remarks>
    /// <para>A rectangle is considered "empty" for this function if r is NULL, or if
    /// r's width and/or height are &lt; 0.0f.</para>
    /// <para>This function is available since SDL 3.2.0.</para>
    /// </remarks>
    public static bool SDL_RectEmptyFloat(SDL_FRect* r)
    {
        return (r == null) || (r->w < 0.0f) || (r->h < 0.0f);
    }

    /// <summary>
    /// Determine whether two floating point rectangles are equal, within some given epsilon.
    /// </summary>
    /// <param name="a">the first rectangle to test.</param>
    /// <param name="b">the second rectangle to test.</param>
    /// <param name="epsilon">the epsilon value for comparison.</param>
    /// <returns>true if the rectangles are equal, false otherwise.</returns>
    /// <remarks>
    /// <para>Rectangles are considered equal if both are not NULL and each of their x,
    /// y, width and height are within epsilon of each other. If you don't know
    /// what value to use for epsilon, you should call the SDL_RectsEqualFloat
    /// function instead.</para>
    /// <para>This function is available since SDL 3.2.0.</para>
    /// </remarks>
    public static bool SDL_RectsEqualEpsilon(SDL_FRect* a, SDL_FRect* b, float epsilon)
    {
        return (a != null) && (b != null) && ((a == b) ||
               ((MathF.Abs(a->x - b->x) <= epsilon) &&
                (MathF.Abs(a->y - b->y) <= epsilon) &&
                (MathF.Abs(a->w - b->w) <= epsilon) &&
                (MathF.Abs(a->h - b->h) <= epsilon)));
    }

    /// <summary>
    /// Determine whether two floating point rectangles are equal, within a default epsilon.
    /// </summary>
    /// <param name="a">the first rectangle to test.</param>
    /// <param name="b">the second rectangle to test.</param>
    /// <returns>true if the rectangles are equal, false otherwise.</returns>
    /// <remarks>
    /// <para>Rectangles are considered equal if both are not NULL and each of their x,
    /// y, width and height are within SDL_FLT_EPSILON of each other. This is often
    /// a reasonable way to compare two floating point rectangles and deal with the
    /// slight precision variations in floating point calculations that tend to pop up.</para>
    /// <para>This function is available since SDL 3.2.0.</para>
    /// </remarks>
    public static bool SDL_RectsEqualFloat(SDL_FRect* a, SDL_FRect* b)
    {
        return SDL_RectsEqualEpsilon(a, b, float.Epsilon);
    }

    /// <summary>
    /// Determine whether two rectangles intersect with float precision.
    /// </summary>
    /// <param name="A">an SDL_FRect structure representing the first rectangle.</param>
    /// <param name="B">an SDL_FRect structure representing the second rectangle.</param>
    /// <returns>true if there is an intersection, false otherwise.</returns>
    /// <remarks>
    /// <para>If either pointer is NULL the function will return false.</para>
    /// <para>This function is available since SDL 3.2.0.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasRectIntersectionFloat(SDL_FRect* A, SDL_FRect* B);

    /// <summary>
    /// Calculate the intersection of two rectangles with float precision.
    /// </summary>
    /// <param name="A">an SDL_FRect structure representing the first rectangle.</param>
    /// <param name="B">an SDL_FRect structure representing the second rectangle.</param>
    /// <param name="result">an SDL_FRect structure filled in with the intersection of rectangles A and B.</param>
    /// <returns>true if there is an intersection, false otherwise.</returns>
    /// <remarks>
    /// <para>If result is NULL then this function will return false.</para>
    /// <para>This function is available since SDL 3.2.0.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRectIntersectionFloat(SDL_FRect* A, SDL_FRect* B, SDL_FRect* result);

    /// <summary>
    /// Calculate the union of two rectangles with float precision.
    /// </summary>
    /// <param name="A">an SDL_FRect structure representing the first rectangle.</param>
    /// <param name="B">an SDL_FRect structure representing the second rectangle.</param>
    /// <param name="result">an SDL_FRect structure filled in with the union of rectangles A and B.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    /// <remarks>This function is available since SDL 3.2.0.</remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRectUnionFloat(SDL_FRect* A, SDL_FRect* B, SDL_FRect* result);

    /// <summary>
    /// Calculate a minimal rectangle enclosing a set of points with float precision.
    /// </summary>
    /// <param name="points">an array of SDL_FPoint structures representing points to be enclosed.</param>
    /// <param name="count">the number of structures in the points array.</param>
    /// <param name="clip">an SDL_FRect used for clipping or NULL to enclose all points.</param>
    /// <param name="result">an SDL_FRect structure filled in with the minimal enclosing rectangle.</param>
    /// <returns>true if any points were enclosed or false if all the points were outside of the clipping rectangle.</returns>
    /// <remarks>
    /// <para>If clip is not NULL then only points inside of the clipping rectangle are considered.</para>
    /// <para>This function is available since SDL 3.2.0.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRectEnclosingPointsFloat(SDL_FPoint* points, int count, SDL_FRect* clip, SDL_FRect* result);

    /// <summary>
    /// Calculate the intersection of a rectangle and line segment with float precision.
    /// </summary>
    /// <param name="rect">an SDL_FRect structure representing the rectangle to intersect.</param>
    /// <param name="X1">a pointer to the starting X-coordinate of the line.</param>
    /// <param name="Y1">a pointer to the starting Y-coordinate of the line.</param>
    /// <param name="X2">a pointer to the ending X-coordinate of the line.</param>
    /// <param name="Y2">a pointer to the ending Y-coordinate of the line.</param>
    /// <returns>true if there is an intersection, false otherwise.</returns>
    /// <remarks>
    /// <para>This function is used to clip a line segment to a rectangle. A line segment
    /// contained entirely within the rectangle or that does not intersect will
    /// remain unchanged. A line segment that crosses the rectangle at either or
    /// both ends will be clipped to the boundary of the rectangle and the new
    /// coordinates saved in X1, Y1, X2, and/or Y2 as necessary.</para>
    /// <para>This function is available since SDL 3.2.0.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRectAndLineIntersectionFloat(SDL_FRect* rect, float* X1, float* Y1, float* X2, float* Y2);
}
