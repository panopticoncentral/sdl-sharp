using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Draw command list.
/// </summary>
/// <remarks>
/// <para>
/// This is the low-level list of polygons that ImGui functions fill. At the end of the frame,
/// all command lists are passed to your RenderDrawListFn function for rendering.
/// </para>
/// <para>
/// Each dear imgui window contains its own ImDrawList. You can use ImGui::GetWindowDrawList() to
/// access the current window draw list and draw custom primitives.
/// </para>
/// <para>
/// You can interleave normal ImGui calls and adding primitives to the current draw list.
/// </para>
/// <para>
/// In single viewport mode, top-left is == GetMainViewport()->Pos (generally 0,0),
/// bottom-right is == GetMainViewport()->Pos+Size (generally io.DisplaySize).
/// </para>
/// <para>
/// You are totally free to apply whatever transformation matrix you want to the data
/// (depending on the use of the transformation you may want to apply it to ClipRect as well!).
/// </para>
/// <para>
/// Important: Primitives are always added to the list and not culled (culling is done at higher-level by ImGui functions).
/// If you use this API a lot, consider coarse culling your drawn objects.
/// </para>
/// </remarks>
public unsafe readonly struct DrawList
{
    internal DrawList(ImDrawList* native)
    {
        Native = native;
    }

    internal readonly ImDrawList* Native { get; }

    /// <summary>
    /// Gets or sets the draw list flags.
    /// </summary>
    /// <remarks>
    /// You may poke into these to adjust anti-aliasing settings per-primitive.
    /// </remarks>
    public DrawListFlags Flags
    {
        get => (DrawListFlags)Native->Flags;
        set => Native->Flags = (ImDrawListFlags)value;
    }

    /// <summary>
    /// Pushes a clip rectangle for subsequent draw calls.
    /// </summary>
    /// <param name="rect">The clip rectangle.</param>
    /// <param name="intersectWithCurrentClipRect">If true, intersects with the current clip rectangle.</param>
    /// <remarks>
    /// This is passed down to your render function but not used for CPU-side coarse clipping.
    /// Prefer using higher-level ImGui::PushClipRect() to affect logic (hit-testing and widget culling).
    /// </remarks>
    public void PushClipRect(Rect rect, bool intersectWithCurrentClipRect = false)
    {
        ImDrawList.PushClipRect(Native, rect.UpperLeft.Value, rect.LowerRight.Value, intersectWithCurrentClipRect);
    }

    /// <summary>
    /// Pushes a full-screen clip rectangle.
    /// </summary>
    public void PushClipRectFullScreen()
    {
        ImDrawList.PushClipRectFullScreen(Native);
    }

    /// <summary>
    /// Pops the last clip rectangle.
    /// </summary>
    public void PopClipRect()
    {
        ImDrawList.PopClipRect(Native);
    }

    /// <summary>
    /// Pushes a texture onto the texture stack.
    /// </summary>
    /// <param name="textureRef">The texture reference to push.</param>
    public void PushTexture(TextureRef textureRef)
    {
        ImDrawList.PushTexture(Native, textureRef.Native);
    }

    /// <summary>
    /// Pops the last texture from the texture stack.
    /// </summary>
    public void PopTexture()
    {
        ImDrawList.PopTexture(Native);
    }

    /// <summary>
    /// Gets the minimum corner of the current clip rectangle.
    /// </summary>
    /// <returns>The minimum corner point.</returns>
    public Rect GetClipRect()
    {
        return new Rect(new(ImDrawList.GetClipRectMin(Native)), new(ImDrawList.GetClipRectMax(Native)));
    }

    /// <summary>
    /// Adds a line between two points.
    /// </summary>
    /// <param name="p1">The starting point.</param>
    /// <param name="p2">The ending point.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <param name="thickness">The line thickness (default 1.0f).</param>
    /// <remarks>
    /// For filled shapes, use clockwise winding order. The anti-aliasing fringe depends on it.
    /// Counter-clockwise shapes will have "inward" anti-aliasing.
    /// </remarks>
    public void AddLine(Point p1, Point p2, uint color, float thickness = 1.0f)
    {
        ImDrawList.AddLine(Native, p1.Value, p2.Value, color, thickness);
    }

    /// <summary>
    /// Adds a rectangle outline.
    /// </summary>
    /// <param name="rect">The rectangle.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <param name="rounding">Corner rounding radius (default 0.0f).</param>
    /// <param name="flags">Draw flags for corner rounding control.</param>
    /// <param name="thickness">The line thickness (default 1.0f).</param>
    public void AddRect(Rect rect, uint color, float rounding = 0.0f, DrawFlags flags = DrawFlags.None, float thickness = 1.0f)
    {
        ImDrawList.AddRect(Native, rect.UpperLeft.Value, rect.LowerRight.Value, color, rounding, flags.ToNative(), thickness);
    }

    /// <summary>
    /// Adds a filled rectangle.
    /// </summary>
    /// <param name="rect">The rectangle.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <param name="rounding">Corner rounding radius (default 0.0f).</param>
    /// <param name="flags">Draw flags for corner rounding control.</param>
    public void AddRectFilled(Rect rect, uint color, float rounding = 0.0f, DrawFlags flags = DrawFlags.None)
    {
        ImDrawList.AddRectFilled(Native, rect.UpperLeft.Value, rect.LowerRight.Value, color, rounding, flags.ToNative());
    }

    /// <summary>
    /// Adds a filled rectangle with different colors for each corner.
    /// </summary>
    /// <param name="rect">The rectangle.</param>
    /// <param name="colUpperLeft">The color at the upper-left corner.</param>
    /// <param name="colUpperRight">The color at the upper-right corner.</param>
    /// <param name="colBottomRight">The color at the bottom-right corner.</param>
    /// <param name="colBottomLeft">The color at the bottom-left corner.</param>
    public void AddRectFilledMultiColor(Rect rect, uint colUpperLeft, uint colUpperRight, uint colBottomRight, uint colBottomLeft)
    {
        ImDrawList.AddRectFilledMultiColor(Native, rect.UpperLeft.Value, rect.LowerRight.Value, colUpperLeft, colUpperRight, colBottomRight, colBottomLeft);
    }

    /// <summary>
    /// Adds a quadrilateral outline.
    /// </summary>
    /// <param name="p1">The first corner.</param>
    /// <param name="p2">The second corner.</param>
    /// <param name="p3">The third corner.</param>
    /// <param name="p4">The fourth corner.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <param name="thickness">The line thickness (default 1.0f).</param>
    public void AddQuad(Point p1, Point p2, Point p3, Point p4, uint color, float thickness = 1.0f)
    {
        ImDrawList.AddQuad(Native, p1.Value, p2.Value, p3.Value, p4.Value, color, thickness);
    }

    /// <summary>
    /// Adds a filled quadrilateral.
    /// </summary>
    /// <param name="p1">The first corner.</param>
    /// <param name="p2">The second corner.</param>
    /// <param name="p3">The third corner.</param>
    /// <param name="p4">The fourth corner.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    public void AddQuadFilled(Point p1, Point p2, Point p3, Point p4, uint color)
    {
        ImDrawList.AddQuadFilled(Native, p1.Value, p2.Value, p3.Value, p4.Value, color);
    }

    /// <summary>
    /// Adds a triangle outline.
    /// </summary>
    /// <param name="p1">The first vertex.</param>
    /// <param name="p2">The second vertex.</param>
    /// <param name="p3">The third vertex.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <param name="thickness">The line thickness (default 1.0f).</param>
    public void AddTriangle(Point p1, Point p2, Point p3, uint color, float thickness = 1.0f)
    {
        ImDrawList.AddTriangle(Native, p1.Value, p2.Value, p3.Value, color, thickness);
    }

    /// <summary>
    /// Adds a filled triangle.
    /// </summary>
    /// <param name="p1">The first vertex.</param>
    /// <param name="p2">The second vertex.</param>
    /// <param name="p3">The third vertex.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    public void AddTriangleFilled(Point p1, Point p2, Point p3, uint color)
    {
        ImDrawList.AddTriangleFilled(Native, p1.Value, p2.Value, p3.Value, color);
    }

    /// <summary>
    /// Adds a circle outline.
    /// </summary>
    /// <param name="center">The center point.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <param name="numSegments">Number of segments (0 for automatic tessellation).</param>
    /// <param name="thickness">The line thickness (default 1.0f).</param>
    /// <remarks>
    /// Use numSegments == 0 to automatically calculate tessellation (preferred).
    /// In future versions textures will provide cheaper and higher-quality circles.
    /// </remarks>
    public void AddCircle(Point center, float radius, uint color, int numSegments = 0, float thickness = 1.0f)
    {
        ImDrawList.AddCircle(Native, center.Value, radius, color, numSegments, thickness);
    }

    /// <summary>
    /// Adds a filled circle.
    /// </summary>
    /// <param name="center">The center point.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <param name="numSegments">Number of segments (0 for automatic tessellation).</param>
    public void AddCircleFilled(Point center, float radius, uint color, int numSegments = 0)
    {
        ImDrawList.AddCircleFilled(Native, center.Value, radius, color, numSegments);
    }

    /// <summary>
    /// Adds an n-gon outline (regular polygon).
    /// </summary>
    /// <param name="center">The center point.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <param name="numSegments">Number of segments (sides of the polygon).</param>
    /// <param name="thickness">The line thickness (default 1.0f).</param>
    /// <remarks>
    /// Use AddNgon/AddNgonFilled functions if you need to guarantee a specific number of sides.
    /// </remarks>
    public void AddNgon(Point center, float radius, uint color, int numSegments, float thickness = 1.0f)
    {
        ImDrawList.AddNgon(Native, center.Value, radius, color, numSegments, thickness);
    }

    /// <summary>
    /// Adds a filled n-gon (regular polygon).
    /// </summary>
    /// <param name="center">The center point.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <param name="numSegments">Number of segments (sides of the polygon).</param>
    public void AddNgonFilled(Point center, float radius, uint color, int numSegments)
    {
        ImDrawList.AddNgonFilled(Native, center.Value, radius, color, numSegments);
    }

    /// <summary>
    /// Adds an ellipse outline.
    /// </summary>
    /// <param name="center">The center point.</param>
    /// <param name="radius">The radius in X and Y directions.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <param name="rotation">Rotation angle in radians (default 0.0f).</param>
    /// <param name="numSegments">Number of segments (0 for automatic tessellation).</param>
    /// <param name="thickness">The line thickness (default 1.0f).</param>
    public void AddEllipse(Point center, Vec2 radius, uint color, float rotation = 0.0f, int numSegments = 0, float thickness = 1.0f)
    {
        ImDrawList.AddEllipse(Native, center.Value, radius.Value, color, rotation, numSegments, thickness);
    }

    /// <summary>
    /// Adds a filled ellipse.
    /// </summary>
    /// <param name="center">The center point.</param>
    /// <param name="radius">The radius in X and Y directions.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <param name="rotation">Rotation angle in radians (default 0.0f).</param>
    /// <param name="numSegments">Number of segments (0 for automatic tessellation).</param>
    public void AddEllipseFilled(Point center, Vec2 radius, uint color, float rotation = 0.0f, int numSegments = 0)
    {
        ImDrawList.AddEllipseFilled(Native, center.Value, radius.Value, color, rotation, numSegments);
    }

    /// <summary>
    /// Adds text at the specified position.
    /// </summary>
    /// <param name="pos">The position.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <param name="text">The text to render.</param>
    public void AddText(Point pos, uint color, Span<byte> text)
    {
        fixed (byte* textPtr = text)
        {
            ImDrawList.AddText(Native, pos.Value, color, textPtr);
        }
    }

    /// <summary>
    /// Adds text at the specified position using a specific font.
    /// </summary>
    /// <param name="font">The font to use.</param>
    /// <param name="fontSize">The font size.</param>
    /// <param name="pos">The position.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <param name="text">The text to render.</param>
    /// <param name="wrapWidth">Text wrap width (0.0f for no wrapping).</param>
    /// <param name="cpuFineClipRect">Optional CPU-side fine clipping rectangle.</param>
    public void AddText(Font font, float fontSize, Point pos, uint color, Span<byte> text, float wrapWidth = 0.0f, Rect? cpuFineClipRect = null)
    {
        fixed (byte* textPtr = text)
        {
            if (cpuFineClipRect.HasValue)
            {
                ImVec4 clipRect = cpuFineClipRect.Value.Value;
                ImDrawList.AddText(Native, font.Native, fontSize, pos.Value, color, textPtr, null, wrapWidth, &clipRect);
            }
            else
            {
                ImDrawList.AddText(Native, font.Native, fontSize, pos.Value, color, textPtr, null, wrapWidth, null);
            }
        }
    }

    /// <summary>
    /// Adds a cubic Bezier curve (4 control points).
    /// </summary>
    /// <param name="p1">The first control point (start).</param>
    /// <param name="p2">The second control point.</param>
    /// <param name="p3">The third control point.</param>
    /// <param name="p4">The fourth control point (end).</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <param name="thickness">The line thickness.</param>
    /// <param name="numSegments">Number of segments (0 for automatic tessellation).</param>
    public void AddBezierCubic(Point p1, Point p2, Point p3, Point p4, uint color, float thickness, int numSegments = 0)
    {
        ImDrawList.AddBezierCubic(Native, p1.Value, p2.Value, p3.Value, p4.Value, color, thickness, numSegments);
    }

    /// <summary>
    /// Adds a quadratic Bezier curve (3 control points).
    /// </summary>
    /// <param name="p1">The first control point (start).</param>
    /// <param name="p2">The second control point.</param>
    /// <param name="p3">The third control point (end).</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <param name="thickness">The line thickness.</param>
    /// <param name="numSegments">Number of segments (0 for automatic tessellation).</param>
    public void AddBezierQuadratic(Point p1, Point p2, Point p3, uint color, float thickness, int numSegments = 0)
    {
        ImDrawList.AddBezierQuadratic(Native, p1.Value, p2.Value, p3.Value, color, thickness, numSegments);
    }

    /// <summary>
    /// Adds a polyline (connected line segments).
    /// </summary>
    /// <param name="points">The array of points.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <param name="flags">Draw flags (use Closed to close the polyline).</param>
    /// <param name="thickness">The line thickness.</param>
    /// <remarks>
    /// Only simple polygons are supported (no self-intersections, no holes).
    /// </remarks>
    public void AddPolyline(ReadOnlySpan<Point> points, uint color, DrawFlags flags, float thickness)
    {
        if (points.Length == 0)
        {
            return;
        }

        Span<ImVec2> nativePoints = stackalloc ImVec2[points.Length];
        for (var i = 0; i < points.Length; i++)
        {
            nativePoints[i] = points[i].Value;
        }

        fixed (ImVec2* pointsPtr = nativePoints)
        {
            ImDrawList.AddPolyline(Native, pointsPtr, points.Length, color, flags.ToNative(), thickness);
        }
    }

    /// <summary>
    /// Adds a filled convex polygon.
    /// </summary>
    /// <param name="points">The array of points defining the polygon vertices.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <remarks>
    /// Filled shapes must always use clockwise winding order.
    /// </remarks>
    public void AddConvexPolyFilled(ReadOnlySpan<Point> points, uint color)
    {
        if (points.Length == 0)
        {
            return;
        }

        Span<ImVec2> nativePoints = stackalloc ImVec2[points.Length];
        for (var i = 0; i < points.Length; i++)
        {
            nativePoints[i] = points[i].Value;
        }

        fixed (ImVec2* pointsPtr = nativePoints)
        {
            ImDrawList.AddConvexPolyFilled(Native, pointsPtr, points.Length, color);
        }
    }

    /// <summary>
    /// Adds a filled concave polygon.
    /// </summary>
    /// <param name="points">The array of points defining the polygon vertices.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    /// <remarks>
    /// Concave polygon fill is more expensive than convex one: it has O(N^2) complexity.
    /// Provided as a convenience for the user but not used by the main library.
    /// </remarks>
    public void AddConcavePolyFilled(ReadOnlySpan<Point> points, uint color)
    {
        if (points.Length == 0)
        {
            return;
        }

        Span<ImVec2> nativePoints = stackalloc ImVec2[points.Length];
        for (var i = 0; i < points.Length; i++)
        {
            nativePoints[i] = points[i].Value;
        }

        fixed (ImVec2* pointsPtr = nativePoints)
        {
            ImDrawList.AddConcavePolyFilled(Native, pointsPtr, points.Length, color);
        }
    }

    /// <summary>
    /// Adds an image.
    /// </summary>
    /// <param name="textureRef">The texture reference.</param>
    /// <param name="rect">The rectangle defining the image area.</param>
    /// <param name="uvMin">The UV coordinates for the upper-left corner (default 0,0).</param>
    /// <param name="uvMax">The UV coordinates for the lower-right corner (default 1,1).</param>
    /// <param name="color">The color tint as a 32-bit packed RGBA value (default white).</param>
    /// <remarks>
    /// Read FAQ to understand what TextureRef is.
    /// Using (0,0)->(1,1) texture coordinates will generally display the entire texture.
    /// </remarks>
    public void AddImage(TextureRef textureRef, Rect rect, Vec2? uvMin = null, Vec2? uvMax = null, uint color = 0xFFFFFFFF)
    {
        Vec2 uv0 = uvMin ?? new Vec2(0, 0);
        Vec2 uv1 = uvMax ?? new Vec2(1, 1);
        ImDrawList.AddImage(Native, textureRef.Native, rect.UpperLeft.Value, rect.LowerRight.Value, uv0.Value, uv1.Value, color);
    }

    /// <summary>
    /// Adds an image as a quad with arbitrary corner positions.
    /// </summary>
    /// <param name="textureRef">The texture reference.</param>
    /// <param name="p1">The first corner.</param>
    /// <param name="p2">The second corner.</param>
    /// <param name="p3">The third corner.</param>
    /// <param name="p4">The fourth corner.</param>
    /// <param name="uv1">UV for first corner (default 0,0).</param>
    /// <param name="uv2">UV for second corner (default 1,0).</param>
    /// <param name="uv3">UV for third corner (default 1,1).</param>
    /// <param name="uv4">UV for fourth corner (default 0,1).</param>
    /// <param name="color">The color tint as a 32-bit packed RGBA value (default white).</param>
    public void AddImageQuad(TextureRef textureRef, Point p1, Point p2, Point p3, Point p4,
        Vec2? uv1 = null, Vec2? uv2 = null, Vec2? uv3 = null, Vec2? uv4 = null, uint color = 0xFFFFFFFF)
    {
        Vec2 uvA = uv1 ?? new Vec2(0, 0);
        Vec2 uvB = uv2 ?? new Vec2(1, 0);
        Vec2 uvC = uv3 ?? new Vec2(1, 1);
        Vec2 uvD = uv4 ?? new Vec2(0, 1);
        ImDrawList.AddImageQuad(Native, textureRef.Native, p1.Value, p2.Value, p3.Value, p4.Value,
            uvA.Value, uvB.Value, uvC.Value, uvD.Value, color);
    }

    /// <summary>
    /// Adds a rounded image.
    /// </summary>
    /// <param name="textureRef">The texture reference.</param>
    /// <param name="pMin">The upper-left corner.</param>
    /// <param name="pMax">The lower-right corner.</param>
    /// <param name="uvMin">The UV coordinates for the upper-left corner.</param>
    /// <param name="uvMax">The UV coordinates for the lower-right corner.</param>
    /// <param name="color">The color tint as a 32-bit packed RGBA value.</param>
    /// <param name="rounding">Corner rounding radius.</param>
    /// <param name="flags">Draw flags for corner rounding control.</param>
    public void AddImageRounded(TextureRef textureRef, Point pMin, Point pMax, Vec2 uvMin, Vec2 uvMax,
        uint color, float rounding, DrawFlags flags = DrawFlags.None)
    {
        ImDrawList.AddImageRounded(Native, textureRef.Native, pMin.Value, pMax.Value, uvMin.Value, uvMax.Value,
            color, rounding, flags.ToNative());
    }

    /// <summary>
    /// Clears the current path.
    /// </summary>
    /// <remarks>
    /// Important: filled shapes must always use clockwise winding order! The anti-aliasing fringe depends on it.
    /// </remarks>
    public void PathClear()
    {
        ImDrawList.PathClear(Native);
    }

    /// <summary>
    /// Adds a point to the current path.
    /// </summary>
    /// <param name="pos">The position to add.</param>
    public void PathLineTo(Point pos)
    {
        ImDrawList.PathLineTo(Native, pos.Value);
    }

    /// <summary>
    /// Adds a point to the current path, merging duplicates.
    /// </summary>
    /// <param name="pos">The position to add.</param>
    public void PathLineToMergeDuplicate(Point pos)
    {
        ImDrawList.PathLineToMergeDuplicate(Native, pos.Value);
    }

    /// <summary>
    /// Fills the current path as a convex polygon.
    /// </summary>
    /// <param name="color">The fill color as a 32-bit packed RGBA value.</param>
    public void PathFillConvex(uint color)
    {
        ImDrawList.PathFillConvex(Native, color);
    }

    /// <summary>
    /// Fills the current path as a concave polygon.
    /// </summary>
    /// <param name="color">The fill color as a 32-bit packed RGBA value.</param>
    public void PathFillConcave(uint color)
    {
        ImDrawList.PathFillConcave(Native, color);
    }

    /// <summary>
    /// Strokes the current path.
    /// </summary>
    /// <param name="color">The stroke color as a 32-bit packed RGBA value.</param>
    /// <param name="flags">Draw flags (use Closed to close the path).</param>
    /// <param name="thickness">The line thickness.</param>
    public void PathStroke(uint color, DrawFlags flags, float thickness)
    {
        ImDrawList.PathStroke(Native, color, flags.ToNative(), thickness);
    }

    /// <summary>
    /// Adds an arc to the current path.
    /// </summary>
    /// <param name="center">The center of the arc.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="aMin">The starting angle in radians.</param>
    /// <param name="aMax">The ending angle in radians.</param>
    /// <param name="numSegments">Number of segments.</param>
    public void PathArcTo(Point center, float radius, float aMin, float aMax, int numSegments)
    {
        ImDrawList.PathArcTo(Native, center.Value, radius, aMin, aMax, numSegments);
    }

    /// <summary>
    /// Adds an arc to the current path using precomputed angles for a 12-step circle.
    /// </summary>
    /// <param name="center">The center of the arc.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="aMinOf12">The starting angle as a multiple of (2*PI/12).</param>
    /// <param name="aMaxOf12">The ending angle as a multiple of (2*PI/12).</param>
    public void PathArcToFast(Point center, float radius, int aMinOf12, int aMaxOf12)
    {
        ImDrawList.PathArcToFast(Native, center.Value, radius, aMinOf12, aMaxOf12);
    }

    /// <summary>
    /// Adds an elliptical arc to the current path.
    /// </summary>
    /// <param name="center">The center of the ellipse.</param>
    /// <param name="radius">The radius in X and Y directions.</param>
    /// <param name="rotation">Rotation angle in radians.</param>
    /// <param name="aMin">The starting angle in radians.</param>
    /// <param name="aMax">The ending angle in radians.</param>
    /// <param name="numSegments">Number of segments (0 for automatic).</param>
    public void PathEllipticalArcTo(Point center, Vec2 radius, float rotation, float aMin, float aMax, int numSegments = 0)
    {
        ImDrawList.PathEllipticalArcTo(Native, center.Value, radius.Value, rotation, aMin, aMax, numSegments);
    }

    /// <summary>
    /// Adds a cubic Bezier curve to the current path (4 control points).
    /// </summary>
    /// <param name="p2">The second control point.</param>
    /// <param name="p3">The third control point.</param>
    /// <param name="p4">The fourth control point (end).</param>
    /// <param name="numSegments">Number of segments (0 for automatic).</param>
    /// <remarks>
    /// The first control point is the current path position.
    /// </remarks>
    public void PathBezierCubicCurveTo(Point p2, Point p3, Point p4, int numSegments = 0)
    {
        ImDrawList.PathBezierCubicCurveTo(Native, p2.Value, p3.Value, p4.Value, numSegments);
    }

    /// <summary>
    /// Adds a quadratic Bezier curve to the current path (3 control points).
    /// </summary>
    /// <param name="p2">The second control point.</param>
    /// <param name="p3">The third control point (end).</param>
    /// <param name="numSegments">Number of segments (0 for automatic).</param>
    /// <remarks>
    /// The first control point is the current path position.
    /// </remarks>
    public void PathBezierQuadraticCurveTo(Point p2, Point p3, int numSegments = 0)
    {
        ImDrawList.PathBezierQuadraticCurveTo(Native, p2.Value, p3.Value, numSegments);
    }

    /// <summary>
    /// Adds a rectangle to the current path.
    /// </summary>
    /// <param name="rect">The rectangle defining the area.</param>
    /// <param name="rounding">Corner rounding radius.</param>
    /// <param name="flags">Draw flags for corner rounding control.</param>
    public void PathRect(Rect rect, float rounding = 0.0f, DrawFlags flags = DrawFlags.None)
    {
        ImDrawList.PathRect(Native, rect.UpperLeft.Value, rect.LowerRight.Value, rounding, flags.ToNative());
    }

    /// <summary>
    /// Forces creation of a new draw call.
    /// </summary>
    /// <remarks>
    /// This is useful if you need to forcefully create a new draw call (to allow for dependent rendering / blending).
    /// Otherwise primitives are merged into the same draw-call as much as possible.
    /// </remarks>
    public void AddDrawCmd()
    {
        ImDrawList.AddDrawCmd(Native);
    }

    /// <summary>
    /// Creates a clone of the CmdBuffer/IdxBuffer/VtxBuffer.
    /// </summary>
    /// <returns>A cloned DrawList.</returns>
    /// <remarks>
    /// For multi-threaded rendering, consider using imgui_threaded_rendering from https://github.com/ocornut/imgui_club instead.
    /// </remarks>
    public DrawList CloneOutput()
    {
        return new(ImDrawList.CloneOutput(Native));
    }

    /// <summary>
    /// Splits the draw list into multiple channels.
    /// </summary>
    /// <param name="count">Number of channels to create.</param>
    /// <remarks>
    /// <para>
    /// Use to split render into layers. By switching channels you can render out-of-order
    /// (e.g. submit FG primitives before BG primitives).
    /// </para>
    /// <para>
    /// Use to minimize draw calls (e.g. if going back-and-forth between multiple clipping rectangles,
    /// prefer to append into separate channels then merge at the end).
    /// </para>
    /// <para>
    /// This API shouldn't have been in ImDrawList in the first place!
    /// Prefer using your own persistent instance of ImDrawListSplitter as you can stack them.
    /// Using the DrawList.Channels* methods you cannot stack a split over another.
    /// </para>
    /// </remarks>
    public void ChannelsSplit(int count)
    {
        ImDrawList.ChannelsSplit(Native, count);
    }

    /// <summary>
    /// Merges all channels back into the main draw list.
    /// </summary>
    public void ChannelsMerge()
    {
        ImDrawList.ChannelsMerge(Native);
    }

    /// <summary>
    /// Sets the current channel.
    /// </summary>
    /// <param name="channelIndex">The channel index to switch to.</param>
    public void ChannelsSetCurrent(int channelIndex)
    {
        ImDrawList.ChannelsSetCurrent(Native, channelIndex);
    }

    /// <summary>
    /// Reserves space for primitives.
    /// </summary>
    /// <param name="idxCount">Number of indices to reserve.</param>
    /// <param name="vtxCount">Number of vertices to reserve.</param>
    /// <remarks>
    /// All primitives need to be reserved via PrimReserve() beforehand.
    /// We render triangles (three vertices).
    /// </remarks>
    public void PrimReserve(int idxCount, int vtxCount)
    {
        ImDrawList.PrimReserve(Native, idxCount, vtxCount);
    }

    /// <summary>
    /// Unreserves previously reserved space.
    /// </summary>
    /// <param name="idxCount">Number of indices to unreserve.</param>
    /// <param name="vtxCount">Number of vertices to unreserve.</param>
    public void PrimUnreserve(int idxCount, int vtxCount)
    {
        ImDrawList.PrimUnreserve(Native, idxCount, vtxCount);
    }

    /// <summary>
    /// Writes an axis-aligned rectangle (composed of two triangles).
    /// </summary>
    /// <param name="rect">The rectangle defining the area.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    public void PrimRect(Rect rect, uint color)
    {
        ImDrawList.PrimRect(Native, rect.UpperLeft.Value, rect.LowerRight.Value, color);
    }

    /// <summary>
    /// Writes an axis-aligned rectangle with UV coordinates.
    /// </summary>
    /// <param name="rect">The rectangle defining the area.</param>
    /// <param name="uvA">UV for upper-left corner.</param>
    /// <param name="uvB">UV for lower-right corner.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    public void PrimRectUV(Rect rect, Vec2 uvA, Vec2 uvB, uint color)
    {
        ImDrawList.PrimRectUV(Native, rect.UpperLeft.Value, rect.LowerRight.Value, uvA.Value, uvB.Value, color);
    }

    /// <summary>
    /// Writes a quad with UV coordinates.
    /// </summary>
    /// <param name="a">The first corner.</param>
    /// <param name="b">The second corner.</param>
    /// <param name="c">The third corner.</param>
    /// <param name="d">The fourth corner.</param>
    /// <param name="uvA">UV for first corner.</param>
    /// <param name="uvB">UV for second corner.</param>
    /// <param name="uvC">UV for third corner.</param>
    /// <param name="uvD">UV for fourth corner.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    public void PrimQuadUV(Point a, Point b, Point c, Point d, Vec2 uvA, Vec2 uvB, Vec2 uvC, Vec2 uvD, uint color)
    {
        ImDrawList.PrimQuadUV(Native, a.Value, b.Value, c.Value, d.Value, uvA.Value, uvB.Value, uvC.Value, uvD.Value, color);
    }

    /// <summary>
    /// Writes a single vertex.
    /// </summary>
    /// <param name="pos">The position.</param>
    /// <param name="uv">The UV coordinates.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    public void PrimWriteVtx(Point pos, Vec2 uv, uint color)
    {
        ImDrawList.PrimWriteVtx(Native, pos.Value, uv.Value, color);
    }

    /// <summary>
    /// Writes a single index.
    /// </summary>
    /// <param name="idx">The index value.</param>
    public void PrimWriteIdx(ushort idx)
    {
        ImDrawList.PrimWriteIdx(Native, idx);
    }

    /// <summary>
    /// Writes a vertex with a unique index.
    /// </summary>
    /// <param name="pos">The position.</param>
    /// <param name="uv">The UV coordinates.</param>
    /// <param name="color">The color as a 32-bit packed RGBA value.</param>
    public void PrimVtx(Point pos, Vec2 uv, uint color)
    {
        ImDrawList.PrimVtx(Native, pos.Value, uv.Value, color);
    }
}
