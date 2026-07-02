// C# port of imgui_demo.cpp example applications (part B).
//
// Upstream reference: imgui_demo.cpp lines ~9787-11080:
//   - ShowExampleAppAutoResize
//   - ShowExampleAppConstrainedResize
//   - ShowExampleAppSimpleOverlay
//   - ShowExampleAppFullscreen
//   - ShowExampleAppWindowTitles
//   - ShowExampleAppCustomRendering
//   - ShowExampleAppDocuments
//   - ShowExampleAppAssetsBrowser
//
// C++ function-static locals become private static fields (prefixed by app name to
// keep the shared partial class namespace unambiguous). IMGUI_DEMO_MARKER() calls
// are preserved as "// DEMO MARKER: ..." comments.

using System.Runtime.InteropServices;
using SdlSharp.ImGui;

namespace ImGuiDemo;

internal static unsafe partial class DemoWindow
{
    //-----------------------------------------------------------------------------
    // [SECTION] Example App: Auto Resize / ShowExampleAppAutoResize()
    //-----------------------------------------------------------------------------

    static int s_autoresize_lines = 10;

    // Demonstrate creating a window which gets auto-resized according to its content.
    private static void ShowExampleAppAutoResize(ref bool open)
    {
        if (!ImGui.Begin("Example: Auto-resizing window", ref open, WindowFlags.AlwaysAutoResize))
        {
            ImGui.End();
            return;
        }
        // DEMO MARKER: Examples/Auto-resizing window

        ImGui.TextUnformatted(
            "Window will resize every-frame to the size of its content.\n" +
            "Note that you probably don't want to query the window size to\n" +
            "output your content because that would create a feedback loop.");
        ImGui.SliderInt("Number of lines", ref s_autoresize_lines, 1, 20);
        for (int i = 0; i < s_autoresize_lines; i++)
            ImGui.Text($"{new string(' ', i * 4)}This is line {i}"); // Pad with space to extend size horizontally
        ImGui.End();
    }

    //-----------------------------------------------------------------------------
    // [SECTION] Example App: Constrained Resize / ShowExampleAppConstrainedResize()
    //-----------------------------------------------------------------------------

    static readonly string[] s_constrained_test_desc =
    {
        "Between 100x100 and 500x500",
        "At least 100x100",
        "Resize vertical + lock current width",
        "Resize horizontal + lock current height",
        "Width Between 400 and 500",
        "Height at least 400",
        "Custom: Aspect Ratio 16:9",
        "Custom: Always Square",
        "Custom: Fixed Steps (100)",
    };

    // Options
    static bool s_constrained_auto_resize = false;
    static bool s_constrained_window_padding = true;
    static int s_constrained_type = 6; // Aspect Ratio
    static int s_constrained_display_lines = 10;

    // Demonstrate creating a window with custom resize constraints.
    // Note that size constraints currently don't work on a docked window (when in 'docking' branch)
    private static void ShowExampleAppConstrainedResize(ref bool open)
    {
        // Helper functions to demonstrate programmatic constraints (upstream: CustomConstraints struct;
        // aspect_ratio / fixed_step were passed via user data, here captured by the local functions)
        // FIXME: This doesn't take account of decoration size (e.g. title bar), library should make this easier.
        // FIXME: None of the three demos works consistently when resizing from borders.
        float aspect_ratio = 16.0f / 9.0f;
        float fixed_step = 100.0f;
        void CustomConstraintAspectRatio(Vec2 pos, Vec2 currentSize, ref Vec2 desiredSize)
            => desiredSize = desiredSize with { Y = (int)(desiredSize.X / aspect_ratio) };
        void CustomConstraintSquare(Vec2 pos, Vec2 currentSize, ref Vec2 desiredSize)
            => desiredSize = new Vec2(MathF.Max(desiredSize.X, desiredSize.Y), MathF.Max(desiredSize.X, desiredSize.Y));
        void CustomConstraintStep(Vec2 pos, Vec2 currentSize, ref Vec2 desiredSize)
            => desiredSize = new Vec2((int)(desiredSize.X / fixed_step + 0.5f) * fixed_step, (int)(desiredSize.Y / fixed_step + 0.5f) * fixed_step);

        // Submit constraint
        if (s_constrained_type == 0) ImGui.SetNextWindowSizeConstraints(100, 100, 500, 500);                             // Between 100x100 and 500x500
        if (s_constrained_type == 1) ImGui.SetNextWindowSizeConstraints(100, 100, float.MaxValue, float.MaxValue);       // Width > 100, Height > 100
        if (s_constrained_type == 2) ImGui.SetNextWindowSizeConstraints(-1, 0, -1, float.MaxValue);                      // Resize vertical + lock current width
        if (s_constrained_type == 3) ImGui.SetNextWindowSizeConstraints(0, -1, float.MaxValue, -1);                      // Resize horizontal + lock current height
        if (s_constrained_type == 4) ImGui.SetNextWindowSizeConstraints(400, -1, 500, -1);                               // Width Between and 400 and 500
        if (s_constrained_type == 5) ImGui.SetNextWindowSizeConstraints(-1, 400, -1, float.MaxValue);                    // Height at least 400
        if (s_constrained_type == 6) ImGui.SetNextWindowSizeConstraints(new Vec2(0, 0), new Vec2(float.MaxValue, float.MaxValue), CustomConstraintAspectRatio); // Aspect ratio
        if (s_constrained_type == 7) ImGui.SetNextWindowSizeConstraints(new Vec2(0, 0), new Vec2(float.MaxValue, float.MaxValue), CustomConstraintSquare);      // Always Square
        if (s_constrained_type == 8) ImGui.SetNextWindowSizeConstraints(new Vec2(0, 0), new Vec2(float.MaxValue, float.MaxValue), CustomConstraintStep);        // Fixed Step

        // Submit window
        if (!s_constrained_window_padding)
            ImGui.PushStyleVar(StyleVar.WindowPadding, 0.0f, 0.0f);
        var window_flags = s_constrained_auto_resize ? WindowFlags.AlwaysAutoResize : WindowFlags.None;
        bool window_open = ImGui.Begin("Example: Constrained Resize", ref open, window_flags);
        if (!s_constrained_window_padding)
            ImGui.PopStyleVar();
        // DEMO MARKER: Examples/Constrained Resizing window
        if (window_open)
        {
            if (Io.KeyShift)
            {
                // Display a dummy viewport (in your real app you would likely use ImageButton() to display a texture)
                var avail_size = ImGui.GetContentRegionAvail();
                var pos = ImGui.GetCursorScreenPos();
                ImGui.ColorButton("viewport", 0.5f, 0.2f, 0.5f, 1.0f, ColorEditFlags.NoTooltip | ColorEditFlags.NoDragDrop, avail_size.Width, avail_size.Height);
                ImGui.SetCursorScreenPos(pos.X + 10, pos.Y + 10);
                ImGui.Text($"{avail_size.Width:F2} x {avail_size.Height:F2}");
            }
            else
            {
                ImGui.Text("(Hold Shift to display a dummy viewport)");
                if (ImGui.Button("Set 200x200")) { ImGui.SetWindowSize(200, 200); } ImGui.SameLine();
                if (ImGui.Button("Set 500x500")) { ImGui.SetWindowSize(500, 500); } ImGui.SameLine();
                if (ImGui.Button("Set 800x200")) { ImGui.SetWindowSize(800, 200); }
                ImGui.SetNextItemWidth(ImGui.GetFontSize() * 20);
                ImGui.Combo("Constraint", ref s_constrained_type, s_constrained_test_desc);
                ImGui.SetNextItemWidth(ImGui.GetFontSize() * 20);
                ImGui.DragInt("Lines", ref s_constrained_display_lines, 0.2f, 1, 100);
                ImGui.Checkbox("Auto-resize", ref s_constrained_auto_resize);
                ImGui.Checkbox("Window padding", ref s_constrained_window_padding);
                for (int i = 0; i < s_constrained_display_lines; i++)
                    ImGui.Text($"{new string(' ', i * 4)}Hello, sailor! Making this line long enough for the example.");
            }
        }
        ImGui.End();
    }

    //-----------------------------------------------------------------------------
    // [SECTION] Example App: Simple overlay / ShowExampleAppSimpleOverlay()
    //-----------------------------------------------------------------------------

    static int s_overlay_location = 0;

    // Demonstrate creating a simple static window with no decoration
    // + a context-menu to choose which corner of the screen to use.
    private static void ShowExampleAppSimpleOverlay(ref bool open)
    {
        var window_flags = WindowFlags.NoDecoration | WindowFlags.AlwaysAutoResize | WindowFlags.NoSavedSettings | WindowFlags.NoFocusOnAppearing | WindowFlags.NoNav;
        if (s_overlay_location >= 0)
        {
            const float PAD = 10.0f;
            var viewport = ImGui.GetMainViewport();
            Vec2 work_pos = viewport.WorkPos; // Use work area to avoid menu-bar/task-bar, if any!
            Vec2 work_size = viewport.WorkSize;
            float window_pos_x = (s_overlay_location & 1) != 0 ? (work_pos.X + work_size.X - PAD) : (work_pos.X + PAD);
            float window_pos_y = (s_overlay_location & 2) != 0 ? (work_pos.Y + work_size.Y - PAD) : (work_pos.Y + PAD);
            float window_pos_pivot_x = (s_overlay_location & 1) != 0 ? 1.0f : 0.0f;
            float window_pos_pivot_y = (s_overlay_location & 2) != 0 ? 1.0f : 0.0f;
            ImGui.SetNextWindowPos(window_pos_x, window_pos_y, Cond.Always, window_pos_pivot_x, window_pos_pivot_y);
            window_flags |= WindowFlags.NoMove;
        }
        else if (s_overlay_location == -2)
        {
            // Center window
            var center = ImGui.GetMainViewport().Center;
            ImGui.SetNextWindowPos(center.X, center.Y, Cond.Always, 0.5f, 0.5f);
            window_flags |= WindowFlags.NoMove;
        }
        ImGui.SetNextWindowBgAlpha(0.35f); // Transparent background
        if (ImGui.Begin("Example: Simple overlay", ref open, window_flags))
        {
            // DEMO MARKER: Examples/Simple overlay -- Scroll up to the beginning of this function to see overlay flags
            ImGui.Text("Simple overlay\n(right-click to change position)");
            ImGui.Separator();
            if (ImGui.IsMousePosValid())
                ImGui.Text($"Mouse Position: ({Io.MousePos.X:F1},{Io.MousePos.Y:F1})");
            else
                ImGui.Text("Mouse Position: <invalid>");
            if (ImGui.BeginPopupContextWindow())
            {
                if (ImGui.MenuItem("Custom", null, s_overlay_location == -1)) s_overlay_location = -1;
                if (ImGui.MenuItem("Center", null, s_overlay_location == -2)) s_overlay_location = -2;
                if (ImGui.MenuItem("Top-left", null, s_overlay_location == 0)) s_overlay_location = 0;
                if (ImGui.MenuItem("Top-right", null, s_overlay_location == 1)) s_overlay_location = 1;
                if (ImGui.MenuItem("Bottom-left", null, s_overlay_location == 2)) s_overlay_location = 2;
                if (ImGui.MenuItem("Bottom-right", null, s_overlay_location == 3)) s_overlay_location = 3;
                if (ImGui.MenuItem("Close")) open = false;
                ImGui.EndPopup();
            }
        }
        ImGui.End();
    }

    //-----------------------------------------------------------------------------
    // [SECTION] Example App: Fullscreen window / ShowExampleAppFullscreen()
    //-----------------------------------------------------------------------------

    static bool s_fullscreen_use_work_area = true;
    static int s_fullscreen_flags = (int)(WindowFlags.NoDecoration | WindowFlags.NoMove | WindowFlags.NoSavedSettings);

    // Demonstrate creating a window covering the entire screen/viewport
    private static void ShowExampleAppFullscreen(ref bool open)
    {
        // We demonstrate using the full viewport area or the work area (without menu-bars, task-bars etc.)
        // Based on your use case you may want one or the other.
        var viewport = ImGui.GetMainViewport();
        Vec2 pos = s_fullscreen_use_work_area ? viewport.WorkPos : viewport.Pos;
        Vec2 size = s_fullscreen_use_work_area ? viewport.WorkSize : viewport.Size;
        ImGui.SetNextWindowPos(pos.X, pos.Y);
        ImGui.SetNextWindowSize(size.X, size.Y);

        if (ImGui.Begin("Example: Fullscreen window", ref open, (WindowFlags)s_fullscreen_flags))
        {
            // DEMO MARKER: Examples/Fullscreen window
            ImGui.Checkbox("Use work area instead of main area", ref s_fullscreen_use_work_area);
            ImGui.SameLine();
            HelpMarker("Main Area = entire viewport,\nWork Area = entire viewport minus sections used by the main menu bars, task bars etc.\n\nEnable the main-menu bar in Examples menu to see the difference.");

            ImGui.CheckboxFlags("ImGuiWindowFlags_NoBackground", ref s_fullscreen_flags, (int)WindowFlags.NoBackground);
            ImGui.CheckboxFlags("ImGuiWindowFlags_NoDecoration", ref s_fullscreen_flags, (int)WindowFlags.NoDecoration);
            ImGui.Indent();
            ImGui.CheckboxFlags("ImGuiWindowFlags_NoTitleBar", ref s_fullscreen_flags, (int)WindowFlags.NoTitleBar);
            ImGui.CheckboxFlags("ImGuiWindowFlags_NoCollapse", ref s_fullscreen_flags, (int)WindowFlags.NoCollapse);
            ImGui.CheckboxFlags("ImGuiWindowFlags_NoScrollbar", ref s_fullscreen_flags, (int)WindowFlags.NoScrollbar);
            ImGui.Unindent();

            if (ImGui.Button("Close this window"))
                open = false;
        }
        ImGui.End();
    }

    //-----------------------------------------------------------------------------
    // [SECTION] Example App: Manipulating Window Titles / ShowExampleAppWindowTitles()
    //-----------------------------------------------------------------------------

    // Demonstrate the use of "##" and "###" in identifiers to manipulate ID generation.
    // This applies to all regular items as well.
    // Read FAQ section "How can I have multiple widgets with the same label?" for details.
    private static void ShowExampleAppWindowTitles(ref bool open)
    {
        _ = open; // Upstream ShowExampleAppWindowTitles(bool*) ignores p_open.
        var viewport = ImGui.GetMainViewport();
        Vec2 base_pos = viewport.Pos;

        // By default, Windows are uniquely identified by their title.
        // You can use the "##" and "###" markers to manipulate the display/ID.

        // Using "##" to display same title but have unique identifier.
        ImGui.SetNextWindowPos(base_pos.X + 100, base_pos.Y + 100, Cond.FirstUseEver);
        ImGui.Begin("Same title as another window##1");
        // DEMO MARKER: Examples/Manipulating window titles##1
        ImGui.Text("This is window 1.\nMy title is the same as window 2, but my identifier is unique.");
        ImGui.End();

        ImGui.SetNextWindowPos(base_pos.X + 100, base_pos.Y + 200, Cond.FirstUseEver);
        ImGui.Begin("Same title as another window##2");
        // DEMO MARKER: Examples/Manipulating window titles##2
        ImGui.Text("This is window 2.\nMy title is the same as window 1, but my identifier is unique.");
        ImGui.End();

        // Using "###" to display a changing title but keep a static identifier "AnimatedTitle"
        char spinner = "|/-\\"[(int)(ImGui.GetTime() / 0.25) & 3];
        string buf = $"Animated title {spinner} {ImGui.GetFrameCount()}###AnimatedTitle";
        ImGui.SetNextWindowPos(base_pos.X + 100, base_pos.Y + 300, Cond.FirstUseEver);
        ImGui.Begin(buf);
        // DEMO MARKER: Examples/Manipulating window titles##3
        ImGui.Text("This window has a changing title.");
        ImGui.End();
    }

    //-----------------------------------------------------------------------------
    // [SECTION] Example App: Custom Rendering using ImDrawList API / ShowExampleAppCustomRendering()
    //-----------------------------------------------------------------------------

    // "Primitives" tab state
    static float s_customrendering_sz = 36.0f;
    static float s_customrendering_thickness = 3.0f;
    static int s_customrendering_ngon_sides = 6;
    static bool s_customrendering_circle_segments_override = false;
    static int s_customrendering_circle_segments_override_v = 12;
    static bool s_customrendering_curve_segments_override = false;
    static int s_customrendering_curve_segments_override_v = 8;
    static readonly float[] s_customrendering_colf = { 1.0f, 1.0f, 0.4f, 1.0f };

    // "Canvas" tab state
    static readonly List<Vec2> s_customrendering_points = new();
    static Vec2 s_customrendering_scrolling = new(0.0f, 0.0f);
    static bool s_customrendering_opt_enable_grid = true;
    static bool s_customrendering_opt_enable_context_menu = true;
    static bool s_customrendering_adding_line = false;

    // "BG/FG draw lists" tab state
    static bool s_customrendering_draw_bg = true;
    static bool s_customrendering_draw_fg = true;

    // Add a |_| looking shape
    static readonly Vec2[] s_customrendering_concave_pos_norms =
    {
        new(0.0f, 0.0f), new(0.3f, 0.0f), new(0.3f, 0.7f), new(0.7f, 0.7f), new(0.7f, 0.0f), new(1.0f, 0.0f), new(1.0f, 1.0f), new(0.0f, 1.0f),
    };

    private static void PathConcaveShape(DrawList draw_list, float x, float y, float sz)
    {
        foreach (var p in s_customrendering_concave_pos_norms)
            draw_list.PathLineTo(new Vec2(x + 0.5f + (int)(sz * p.X), y + 0.5f + (int)(sz * p.Y)));
    }

    // Demonstrate using the low-level ImDrawList to draw custom shapes.
    private static void ShowExampleAppCustomRendering(ref bool open)
    {
        // IM_COL32(r,g,b,a) equivalent: packed ABGR (a<<24 | b<<16 | g<<8 | r)
        static uint Col32(byte r, byte g, byte b, byte a) => ((uint)a << 24) | ((uint)b << 16) | ((uint)g << 8) | r;

        if (!ImGui.Begin("Example: Custom rendering", ref open))
        {
            ImGui.End();
            return;
        }
        // DEMO MARKER: Examples/Custom rendering

        // Tip: If you do a lot of custom rendering, you probably want to use your own geometrical types and benefit of
        // overloaded operators, etc. Define IM_VEC2_CLASS_EXTRA in imconfig.h to create implicit conversions between your
        // types and ImVec2/ImVec4. Dear ImGui defines overloaded operators but they are internal to imgui.cpp and not
        // exposed outside (to avoid messing with your types) In this example we are not using the maths operators!

        if (ImGui.BeginTabBar("##TabBar"))
        {
            if (ImGui.BeginTabItem("Primitives"))
            {
                // DEMO MARKER: Examples/Custom rendering/Primitives
                ImGui.PushItemWidth(-ImGui.GetFontSize() * 15);
                DrawList draw_list = ImGui.GetWindowDrawList();

                // Draw gradients
                // (note that those are currently exacerbating our sRGB/Linear issues)
                // Calling ImGui::GetColorU32() multiplies the given colors by the current Style Alpha, but you may pass the IM_COL32() directly as well..
                ImGui.Text("Gradients");
                Vec2 gradient_size = new(ImGui.CalcItemWidth(), ImGui.GetFrameHeight());
                {
                    var cursor = ImGui.GetCursorScreenPos();
                    Vec2 p0 = new(cursor.X, cursor.Y);
                    Vec2 p1 = new(p0.X + gradient_size.X, p0.Y + gradient_size.Y);
                    uint col_a = ImGui.GetColorU32(Col32(0, 0, 0, 255));
                    uint col_b = ImGui.GetColorU32(Col32(255, 255, 255, 255));
                    draw_list.AddRectFilledMultiColor(p0, p1, col_a, col_b, col_b, col_a);
                    ImGui.InvisibleButton("##gradient1", gradient_size.X, gradient_size.Y);
                }
                {
                    var cursor = ImGui.GetCursorScreenPos();
                    Vec2 p0 = new(cursor.X, cursor.Y);
                    Vec2 p1 = new(p0.X + gradient_size.X, p0.Y + gradient_size.Y);
                    uint col_a = ImGui.GetColorU32(Col32(0, 255, 0, 255));
                    uint col_b = ImGui.GetColorU32(Col32(255, 0, 0, 255));
                    draw_list.AddRectFilledMultiColor(p0, p1, col_a, col_b, col_b, col_a);
                    ImGui.InvisibleButton("##gradient2", gradient_size.X, gradient_size.Y);
                }

                // Draw a bunch of primitives
                ImGui.Text("All primitives");
                ImGui.DragFloat("Size", ref s_customrendering_sz, 0.2f, 2.0f, 100.0f, "%.0f");
                ImGui.DragFloat("Thickness", ref s_customrendering_thickness, 0.05f, 1.0f, 8.0f, "%.02f");
                ImGui.SliderInt("N-gon sides", ref s_customrendering_ngon_sides, 3, 12);
                ImGui.Checkbox("##circlesegmentoverride", ref s_customrendering_circle_segments_override);
                ImGui.SameLine(0.0f, Style.ItemInnerSpacing.X);
                s_customrendering_circle_segments_override |= ImGui.SliderInt("Circle segments override", ref s_customrendering_circle_segments_override_v, 3, 40);
                ImGui.Checkbox("##curvessegmentoverride", ref s_customrendering_curve_segments_override);
                ImGui.SameLine(0.0f, Style.ItemInnerSpacing.X);
                s_customrendering_curve_segments_override |= ImGui.SliderInt("Curves segments override", ref s_customrendering_curve_segments_override_v, 3, 40);
                ImGui.ColorEdit4("Color", s_customrendering_colf);

                var cursor_p = ImGui.GetCursorScreenPos();
                Vec2 p = new(cursor_p.X, cursor_p.Y);
                uint col = ImGui.ColorConvertFloat4ToU32(s_customrendering_colf[0], s_customrendering_colf[1], s_customrendering_colf[2], s_customrendering_colf[3]);
                float sz = s_customrendering_sz;
                const float spacing = 10.0f;
                const DrawFlags corners_tl_br = DrawFlags.RoundCornersTopLeft | DrawFlags.RoundCornersBottomRight;
                float rounding = sz / 5.0f;
                int circle_segments = s_customrendering_circle_segments_override ? s_customrendering_circle_segments_override_v : 0;
                int curve_segments = s_customrendering_curve_segments_override ? s_customrendering_curve_segments_override_v : 0;
                Vec2[] cp3 = { new(0.0f, sz * 0.6f), new(sz * 0.5f, -sz * 0.4f), new(sz, sz) }; // Control points for curves
                Vec2[] cp4 = { new(0.0f, 0.0f), new(sz * 1.3f, sz * 0.3f), new(sz - sz * 1.3f, sz - sz * 0.3f), new(sz, sz) };

                float x = p.X + 4.0f;
                float y = p.Y + 4.0f;
                for (int n = 0; n < 2; n++)
                {
                    // First line uses a thickness of 1.0f, second line uses the configurable thickness
                    float th = (n == 0) ? 1.0f : s_customrendering_thickness;
                    draw_list.AddNgon(new Vec2(x + sz * 0.5f, y + sz * 0.5f), sz * 0.5f, col, s_customrendering_ngon_sides, th); x += sz + spacing;  // N-gon
                    draw_list.AddCircle(new Vec2(x + sz * 0.5f, y + sz * 0.5f), sz * 0.5f, col, circle_segments, th); x += sz + spacing;             // Circle
                    draw_list.AddEllipse(new Vec2(x + sz * 0.5f, y + sz * 0.5f), new Vec2(sz * 0.5f, sz * 0.3f), col, -0.3f, circle_segments, th); x += sz + spacing; // Ellipse
                    draw_list.AddRect(new Vec2(x, y), new Vec2(x + sz, y + sz), col, 0.0f, DrawFlags.None, th); x += sz + spacing;                   // Square
                    draw_list.AddRect(new Vec2(x, y), new Vec2(x + sz, y + sz), col, rounding, DrawFlags.None, th); x += sz + spacing;               // Square with all rounded corners
                    draw_list.AddRect(new Vec2(x, y), new Vec2(x + sz, y + sz), col, rounding, corners_tl_br, th); x += sz + spacing;                // Square with two rounded corners
                    draw_list.AddTriangle(new Vec2(x + sz * 0.5f, y), new Vec2(x + sz, y + sz - 0.5f), new Vec2(x, y + sz - 0.5f), col, th); x += sz + spacing; // Triangle
                    //draw_list.AddTriangle(new Vec2(x + sz * 0.2f, y), new Vec2(x, y + sz - 0.5f), new Vec2(x + sz * 0.4f, y + sz - 0.5f), col, th); x += sz * 0.4f + spacing; // Thin triangle
                    PathConcaveShape(draw_list, x, y, sz); draw_list.PathStroke(col, DrawFlags.Closed, th); x += sz + spacing;                       // Concave Shape
                    //draw_list.AddPolyline(concave_shape, col, DrawFlags.Closed, th);
                    draw_list.AddLine(new Vec2(x, y), new Vec2(x + sz, y), col, th); x += sz + spacing;                                              // Horizontal line (note: drawing a filled rectangle will be faster!)
                    draw_list.AddLine(new Vec2(x, y), new Vec2(x, y + sz), col, th); x += spacing;                                                   // Vertical line (note: drawing a filled rectangle will be faster!)
                    draw_list.AddLine(new Vec2(x, y), new Vec2(x + sz, y + sz), col, th); x += sz + spacing;                                         // Diagonal line

                    // Path
                    draw_list.PathArcTo(new Vec2(x + sz * 0.5f, y + sz * 0.5f), sz * 0.5f, 3.141592f, 3.141592f * -0.5f);
                    draw_list.PathStroke(col, DrawFlags.None, th);
                    x += sz + spacing;

                    // Quadratic Bezier Curve (3 control points)
                    draw_list.AddBezierQuadratic(new Vec2(x + cp3[0].X, y + cp3[0].Y), new Vec2(x + cp3[1].X, y + cp3[1].Y), new Vec2(x + cp3[2].X, y + cp3[2].Y), col, th, curve_segments);
                    x += sz + spacing;

                    // Cubic Bezier Curve (4 control points)
                    draw_list.AddBezierCubic(new Vec2(x + cp4[0].X, y + cp4[0].Y), new Vec2(x + cp4[1].X, y + cp4[1].Y), new Vec2(x + cp4[2].X, y + cp4[2].Y), new Vec2(x + cp4[3].X, y + cp4[3].Y), col, th, curve_segments);

                    x = p.X + 4;
                    y += sz + spacing;
                }

                // Filled shapes
                draw_list.AddNgonFilled(new Vec2(x + sz * 0.5f, y + sz * 0.5f), sz * 0.5f, col, s_customrendering_ngon_sides); x += sz + spacing;    // N-gon
                draw_list.AddCircleFilled(new Vec2(x + sz * 0.5f, y + sz * 0.5f), sz * 0.5f, col, circle_segments); x += sz + spacing;               // Circle
                draw_list.AddEllipseFilled(new Vec2(x + sz * 0.5f, y + sz * 0.5f), new Vec2(sz * 0.5f, sz * 0.3f), col, -0.3f, circle_segments); x += sz + spacing; // Ellipse
                draw_list.AddRectFilled(new Vec2(x, y), new Vec2(x + sz, y + sz), col); x += sz + spacing;                                           // Square
                draw_list.AddRectFilled(new Vec2(x, y), new Vec2(x + sz, y + sz), col, 10.0f); x += sz + spacing;                                    // Square with all rounded corners
                draw_list.AddRectFilled(new Vec2(x, y), new Vec2(x + sz, y + sz), col, 10.0f, corners_tl_br); x += sz + spacing;                     // Square with two rounded corners
                draw_list.AddTriangleFilled(new Vec2(x + sz * 0.5f, y), new Vec2(x + sz, y + sz - 0.5f), new Vec2(x, y + sz - 0.5f), col); x += sz + spacing; // Triangle
                //draw_list.AddTriangleFilled(new Vec2(x + sz * 0.2f, y), new Vec2(x, y + sz - 0.5f), new Vec2(x + sz * 0.4f, y + sz - 0.5f), col); x += sz * 0.4f + spacing; // Thin triangle
                PathConcaveShape(draw_list, x, y, sz); draw_list.PathFillConcave(col); x += sz + spacing;                                            // Concave shape
                draw_list.AddRectFilled(new Vec2(x, y), new Vec2(x + sz, y + s_customrendering_thickness), col); x += sz + spacing;                  // Horizontal line (faster than AddLine, but only handle integer thickness)
                draw_list.AddRectFilled(new Vec2(x, y), new Vec2(x + s_customrendering_thickness, y + sz), col); x += spacing * 2.0f;                // Vertical line (faster than AddLine, but only handle integer thickness)
                draw_list.AddRectFilled(new Vec2(x, y), new Vec2(x + 1, y + 1), col); x += sz;                                                       // Pixel (faster than AddLine)

                // Path
                draw_list.PathArcTo(new Vec2(x + sz * 0.5f, y + sz * 0.5f), sz * 0.5f, 3.141592f * -0.5f, 3.141592f);
                draw_list.PathFillConvex(col);
                x += sz + spacing;

                // Quadratic Bezier Curve (3 control points)
                draw_list.PathLineTo(new Vec2(x + cp3[0].X, y + cp3[0].Y));
                draw_list.PathBezierQuadraticCurveTo(new Vec2(x + cp3[1].X, y + cp3[1].Y), new Vec2(x + cp3[2].X, y + cp3[2].Y), curve_segments);
                draw_list.PathFillConvex(col);
                x += sz + spacing;

                draw_list.AddRectFilledMultiColor(new Vec2(x, y), new Vec2(x + sz, y + sz), Col32(0, 0, 0, 255), Col32(255, 0, 0, 255), Col32(255, 255, 0, 255), Col32(0, 255, 0, 255));
                x += sz + spacing;

                ImGui.Dummy((sz + spacing) * 13.2f, (sz + spacing) * 3.0f);
                ImGui.PopItemWidth();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Canvas"))
            {
                // DEMO MARKER: Examples/Custom rendering/Canvas
                ImGui.Checkbox("Enable grid", ref s_customrendering_opt_enable_grid);
                ImGui.Checkbox("Enable context menu", ref s_customrendering_opt_enable_context_menu);
                ImGui.Text("Mouse Left: drag to add lines,\nMouse Right: drag to scroll, click for context menu.");

                // Typically you would use a BeginChild()/EndChild() pair to benefit from a clipping region + own scrolling.
                // Here we demonstrate that this can be replaced by simple offsetting + custom drawing + PushClipRect/PopClipRect() calls.
                // To use a child window instead we could use, e.g:
                //      ImGui.PushStyleVar(StyleVar.WindowPadding, 0, 0);                // Disable padding
                //      ImGui.PushStyleColor(Col.ChildBg, IM_COL32(50, 50, 50, 255));    // Set a background color
                //      ImGui.BeginChild("canvas", 0.0f, 0.0f, ChildFlags.Borders, WindowFlags.NoMove);
                //      ImGui.PopStyleColor();
                //      ImGui.PopStyleVar();
                //      [...]
                //      ImGui.EndChild();

                // Using InvisibleButton() as a convenience 1) it will advance the layout cursor and 2) allows us to use IsItemHovered()/IsItemActive()
                var canvas_cursor = ImGui.GetCursorScreenPos();
                Vec2 canvas_p0 = new(canvas_cursor.X, canvas_cursor.Y);    // ImDrawList API uses screen coordinates!
                var avail = ImGui.GetContentRegionAvail();                 // Resize canvas to what's available
                Vec2 canvas_sz = new(avail.Width, avail.Height);
                if (canvas_sz.X < 50.0f) canvas_sz = canvas_sz with { X = 50.0f };
                if (canvas_sz.Y < 50.0f) canvas_sz = canvas_sz with { Y = 50.0f };
                Vec2 canvas_p1 = new(canvas_p0.X + canvas_sz.X, canvas_p0.Y + canvas_sz.Y);

                // Draw border and background color
                DrawList draw_list = ImGui.GetWindowDrawList();
                draw_list.AddRectFilled(canvas_p0, canvas_p1, Col32(50, 50, 50, 255));
                draw_list.AddRect(canvas_p0, canvas_p1, Col32(255, 255, 255, 255));

                // This will catch our interactions
                ImGui.InvisibleButton("canvas", canvas_sz.X, canvas_sz.Y, ButtonFlags.MouseButtonLeft | ButtonFlags.MouseButtonRight);
                bool is_hovered = ImGui.IsItemHovered(); // Hovered
                bool is_active = ImGui.IsItemActive();   // Held
                Vec2 origin = new(canvas_p0.X + s_customrendering_scrolling.X, canvas_p0.Y + s_customrendering_scrolling.Y); // Lock scrolled origin
                Vec2 mouse_pos_in_canvas = new(Io.MousePos.X - origin.X, Io.MousePos.Y - origin.Y);

                // Add first and second point
                if (is_hovered && !s_customrendering_adding_line && ImGui.IsMouseClicked(MouseButton.Left))
                {
                    s_customrendering_points.Add(mouse_pos_in_canvas);
                    s_customrendering_points.Add(mouse_pos_in_canvas);
                    s_customrendering_adding_line = true;
                }
                if (s_customrendering_adding_line)
                {
                    s_customrendering_points[^1] = mouse_pos_in_canvas;
                    if (!ImGui.IsMouseDown(MouseButton.Left))
                        s_customrendering_adding_line = false;
                }

                // Pan (we use a zero mouse threshold when there's no context menu)
                // You may decide to make that threshold dynamic based on whether the mouse is hovering something etc.
                float mouse_threshold_for_pan = s_customrendering_opt_enable_context_menu ? -1.0f : 0.0f;
                if (is_active && ImGui.IsMouseDragging(MouseButton.Right, mouse_threshold_for_pan))
                {
                    s_customrendering_scrolling = new Vec2(
                        s_customrendering_scrolling.X + Io.MouseDelta.X,
                        s_customrendering_scrolling.Y + Io.MouseDelta.Y);
                }

                // Context menu (under default mouse threshold)
                Vec2 drag_delta = ImGui.GetMouseDragDelta(MouseButton.Right);
                if (s_customrendering_opt_enable_context_menu && drag_delta.X == 0.0f && drag_delta.Y == 0.0f)
                    ImGui.OpenPopupOnItemClick("context", PopupFlags.MouseButtonRight);
                if (ImGui.BeginPopup("context"))
                {
                    if (s_customrendering_adding_line)
                        s_customrendering_points.RemoveRange(s_customrendering_points.Count - 2, 2);
                    s_customrendering_adding_line = false;
                    if (ImGui.MenuItem("Remove one", null, false, s_customrendering_points.Count > 0)) { s_customrendering_points.RemoveRange(s_customrendering_points.Count - 2, 2); }
                    if (ImGui.MenuItem("Remove all", null, false, s_customrendering_points.Count > 0)) { s_customrendering_points.Clear(); }
                    ImGui.EndPopup();
                }

                // Draw grid + all lines in the canvas
                draw_list.PushClipRect(canvas_p0, canvas_p1, true);
                if (s_customrendering_opt_enable_grid)
                {
                    const float GRID_STEP = 64.0f;
                    for (float gx = s_customrendering_scrolling.X % GRID_STEP; gx < canvas_sz.X; gx += GRID_STEP)
                        draw_list.AddLine(new Vec2(canvas_p0.X + gx, canvas_p0.Y), new Vec2(canvas_p0.X + gx, canvas_p1.Y), Col32(200, 200, 200, 40));
                    for (float gy = s_customrendering_scrolling.Y % GRID_STEP; gy < canvas_sz.Y; gy += GRID_STEP)
                        draw_list.AddLine(new Vec2(canvas_p0.X, canvas_p0.Y + gy), new Vec2(canvas_p1.X, canvas_p0.Y + gy), Col32(200, 200, 200, 40));
                }
                for (int n = 0; n < s_customrendering_points.Count; n += 2)
                    draw_list.AddLine(
                        new Vec2(origin.X + s_customrendering_points[n].X, origin.Y + s_customrendering_points[n].Y),
                        new Vec2(origin.X + s_customrendering_points[n + 1].X, origin.Y + s_customrendering_points[n + 1].Y),
                        Col32(255, 255, 0, 255), 2.0f);
                draw_list.PopClipRect();

                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("BG/FG draw lists"))
            {
                // DEMO MARKER: Examples/Custom rendering/BG & FG draw lists
                ImGui.Checkbox("Draw in Background draw list", ref s_customrendering_draw_bg);
                ImGui.SameLine(); HelpMarker("The Background draw list will be rendered below every Dear ImGui windows.");
                ImGui.Checkbox("Draw in Foreground draw list", ref s_customrendering_draw_fg);
                ImGui.SameLine(); HelpMarker("The Foreground draw list will be rendered over every Dear ImGui windows.");
                var window_pos = ImGui.GetWindowPos();
                var window_size = ImGui.GetWindowSize();
                Vec2 window_center = new(window_pos.X + window_size.Width * 0.5f, window_pos.Y + window_size.Height * 0.5f);
                if (s_customrendering_draw_bg)
                    ImGui.GetBackgroundDrawList().AddCircle(window_center, window_size.Width * 0.6f, Col32(255, 0, 0, 200), 0, 10 + 4);
                if (s_customrendering_draw_fg)
                    ImGui.GetForegroundDrawList().AddCircle(window_center, window_size.Height * 0.6f, Col32(0, 255, 0, 200), 0, 10);
                ImGui.EndTabItem();
            }

            // Demonstrate out-of-order rendering via channels splitting
            // We use functions in ImDrawList as each draw list contains a convenience splitter,
            // but you can also instantiate your own ImDrawListSplitter if you need to nest them.
            if (ImGui.BeginTabItem("Draw Channels"))
            {
                // DEMO MARKER: Examples/Custom rendering/Draw Channels
                DrawList draw_list = ImGui.GetWindowDrawList();
                {
                    ImGui.Text("Blue shape is drawn first: appears in back");
                    ImGui.Text("Red shape is drawn after: appears in front");
                    var cursor = ImGui.GetCursorScreenPos();
                    Vec2 p0 = new(cursor.X, cursor.Y);
                    draw_list.AddRectFilled(new Vec2(p0.X, p0.Y), new Vec2(p0.X + 50, p0.Y + 50), Col32(0, 0, 255, 255)); // Blue
                    draw_list.AddRectFilled(new Vec2(p0.X + 25, p0.Y + 25), new Vec2(p0.X + 75, p0.Y + 75), Col32(255, 0, 0, 255)); // Red
                    ImGui.Dummy(75, 75);
                }
                ImGui.Separator();
                {
                    ImGui.Text("Blue shape is drawn first, into channel 1: appears in front");
                    ImGui.Text("Red shape is drawn after, into channel 0: appears in back");
                    var cursor = ImGui.GetCursorScreenPos();
                    Vec2 p1 = new(cursor.X, cursor.Y);

                    // Create 2 channels and draw a Blue shape THEN a Red shape.
                    // You can create any number of channels. Tables API use 1 channel per column in order to better batch draw calls.
                    draw_list.ChannelsSplit(2);
                    draw_list.ChannelsSetCurrent(1);
                    draw_list.AddRectFilled(new Vec2(p1.X, p1.Y), new Vec2(p1.X + 50, p1.Y + 50), Col32(0, 0, 255, 255)); // Blue
                    draw_list.ChannelsSetCurrent(0);
                    draw_list.AddRectFilled(new Vec2(p1.X + 25, p1.Y + 25), new Vec2(p1.X + 75, p1.Y + 75), Col32(255, 0, 0, 255)); // Red

                    // Flatten/reorder channels. Red shape is in channel 0 and it appears below the Blue shape in channel 1.
                    // This works by copying draw indices only (vertices are not copied).
                    draw_list.ChannelsMerge();
                    ImGui.Dummy(75, 75);
                    ImGui.Text("After reordering, contents of channel 0 appears below channel 1.");
                }
                ImGui.EndTabItem();
            }

            ImGui.EndTabBar();
        }

        ImGui.End();
    }

    //-----------------------------------------------------------------------------
    // [SECTION] Example App: Documents Handling / ShowExampleAppDocuments()
    //-----------------------------------------------------------------------------

    // Simplified structure to mimic a Document model
    private sealed class MyDocument
    {
        public string Name;             // Document title
        public int UID;                 // Unique ID (necessary as we can change title)
        public bool Open;               // Set when open (we keep an array of all available documents to simplify demo code!)
        public bool OpenPrev;           // Copy of Open from last update.
        public bool Dirty;              // Set when the document has been modified
        public readonly float[] Color;  // An arbitrary variable associated to the document

        public MyDocument(int uid, string name, bool open = true, float r = 1.0f, float g = 1.0f, float b = 1.0f, float a = 1.0f)
        {
            UID = uid;
            Name = name;
            Open = OpenPrev = open;
            Dirty = false;
            Color = new[] { r, g, b, a };
        }

        public void DoOpen() => Open = true;
        public void DoForceClose() { Open = false; Dirty = false; }
        public void DoSave() => Dirty = false;
    }

    private sealed class ExampleAppDocuments
    {
        public readonly List<MyDocument> Documents = new()
        {
            new MyDocument(0, "Lettuce",             true,  0.4f, 0.8f, 0.4f, 1.0f),
            new MyDocument(1, "Eggplant",            true,  0.8f, 0.5f, 1.0f, 1.0f),
            new MyDocument(2, "Carrot",              true,  1.0f, 0.8f, 0.5f, 1.0f),
            new MyDocument(3, "Tomato",              false, 1.0f, 0.3f, 0.4f, 1.0f),
            new MyDocument(4, "A Rather Long Title", false, 0.4f, 0.8f, 0.8f, 1.0f),
            new MyDocument(5, "Some Document",       false, 0.8f, 0.8f, 1.0f, 1.0f),
        };
        public readonly List<MyDocument> CloseQueue = new();
        public MyDocument? RenamingDoc = null;
        public bool RenamingStarted = false;

        // As we allow to change document name, we append a never-changing document ID so tabs are stable
        public static string GetTabName(MyDocument doc) => $"{doc.Name}###doc{doc.UID}";

        // Display placeholder contents for the Document
        public void DisplayDocContents(MyDocument doc)
        {
            ImGui.PushID(doc.UID);
            ImGui.Text($"Document \"{doc.Name}\"");
            ImGui.PushStyleColor(Col.Text, doc.Color[0], doc.Color[1], doc.Color[2], doc.Color[3]);
            ImGui.TextWrapped("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
            ImGui.PopStyleColor();

            ImGui.SetNextItemShortcut(Key.ModCtrl | Key.R, InputFlags.Tooltip);
            if (ImGui.Button("Rename.."))
            {
                RenamingDoc = doc;
                RenamingStarted = true;
            }
            ImGui.SameLine();

            ImGui.SetNextItemShortcut(Key.ModCtrl | Key.M, InputFlags.Tooltip);
            if (ImGui.Button("Modify"))
                doc.Dirty = true;

            ImGui.SameLine();
            ImGui.SetNextItemShortcut(Key.ModCtrl | Key.S, InputFlags.Tooltip);
            if (ImGui.Button("Save"))
                doc.DoSave();

            ImGui.SameLine();
            ImGui.SetNextItemShortcut(Key.ModCtrl | Key.W, InputFlags.Tooltip);
            if (ImGui.Button("Close"))
                CloseQueue.Add(doc);
            ImGui.ColorEdit3("color", ref doc.Color[0], ref doc.Color[1], ref doc.Color[2]); // Useful to test drag and drop and hold-dragged-to-open-tab behavior.
            ImGui.PopID();
        }

        // Display context menu for the Document
        public void DisplayDocContextMenu(MyDocument doc)
        {
            if (!ImGui.BeginPopupContextItem())
                return;

            if (ImGui.MenuItem($"Save {doc.Name}", "Ctrl+S", false, doc.Open))
                doc.DoSave();
            if (ImGui.MenuItem("Rename...", "Ctrl+R", false, doc.Open))
                RenamingDoc = doc;
            if (ImGui.MenuItem("Close", "Ctrl+W", false, doc.Open))
                CloseQueue.Add(doc);
            ImGui.EndPopup();
        }

        // [Optional] Notify the system of Tabs/Windows closure that happened outside the regular tab interface.
        // If a tab has been closed programmatically (aka closed from another source such as the Checkbox() in the demo,
        // as opposed to clicking on the regular tab closing button) and stops being submitted, it will take a frame for
        // the tab bar to notice its absence. During this frame there will be a gap in the tab bar, and if the tab that has
        // disappeared was the selected one, the tab bar will report no selected tab during the frame. This will effectively
        // give the impression of a flicker for one frame.
        // We call SetTabItemClosed() to manually notify the Tab Bar or Docking system of removed tabs to avoid this glitch.
        // Note that this completely optional, and only affect tab bars with the ImGuiTabBarFlags_Reorderable flag.
        public void NotifyOfDocumentsClosedElsewhere()
        {
            foreach (var doc in Documents)
            {
                if (!doc.Open && doc.OpenPrev)
                    ImGui.SetTabItemClosed(doc.Name);
                doc.OpenPrev = doc.Open;
            }
        }
    }

    static readonly ExampleAppDocuments s_documents_app = new();

    // Options
    static bool s_documents_opt_reorderable = true;
    static TabBarFlags s_documents_opt_fitting_flags = TabBarFlags.FittingPolicyMixed; // == ImGuiTabBarFlags_FittingPolicyDefault_

    private static void ShowExampleAppDocuments(ref bool open)
    {
        var app = s_documents_app;

        bool window_contents_visible = ImGui.Begin("Example: Documents", ref open, WindowFlags.MenuBar);
        if (!window_contents_visible)
        {
            ImGui.End();
            return;
        }
        // DEMO MARKER: Examples/Documents

        // Menu
        if (ImGui.BeginMenuBar())
        {
            if (ImGui.BeginMenu("File"))
            {
                int open_count = 0;
                foreach (var doc in app.Documents)
                    open_count += doc.Open ? 1 : 0;

                if (ImGui.BeginMenu("Open", open_count < app.Documents.Count))
                {
                    foreach (var doc in app.Documents)
                        if (!doc.Open && ImGui.MenuItem(doc.Name))
                            doc.DoOpen();
                    ImGui.EndMenu();
                }
                if (ImGui.MenuItem("Close All Documents", null, false, open_count > 0))
                    foreach (var doc in app.Documents)
                        app.CloseQueue.Add(doc);
                if (ImGui.MenuItem("Exit"))
                    open = false;
                ImGui.EndMenu();
            }
            ImGui.EndMenuBar();
        }

        // [Debug] List documents with one checkbox for each
        for (int doc_n = 0; doc_n < app.Documents.Count; doc_n++)
        {
            var doc = app.Documents[doc_n];
            if (doc_n > 0)
                ImGui.SameLine();
            ImGui.PushID(doc.UID);
            if (ImGui.Checkbox(doc.Name, ref doc.Open))
                if (!doc.Open)
                    doc.DoForceClose();
            ImGui.PopID();
        }

        ImGui.Separator();

        // About the ImGuiWindowFlags_UnsavedDocument / ImGuiTabItemFlags_UnsavedDocument flags.
        // They have multiple effects:
        // - Display a dot next to the title.
        // - Tab is selected when clicking the X close button.
        // - Closure is not assumed (will wait for user to stop submitting the tab).
        //   Otherwise closure is assumed when pressing the X, so if you keep submitting the tab may reappear at end of tab bar.
        //   We need to assume closure by default otherwise waiting for "lack of submission" on the next frame would leave an empty
        //   hole for one-frame, both in the tab-bar and in tab-contents when closing a tab/window.
        //   The rarely used SetTabItemClosed() function is a way to notify of programmatic closure to avoid the one-frame hole.

        // Submit Tab Bar and Tabs
        {
            var tab_bar_flags = s_documents_opt_fitting_flags | (s_documents_opt_reorderable ? TabBarFlags.Reorderable : TabBarFlags.None);
            tab_bar_flags |= TabBarFlags.DrawSelectedOverline;
            if (ImGui.BeginTabBar("##tabs", tab_bar_flags))
            {
                if (s_documents_opt_reorderable)
                    app.NotifyOfDocumentsClosedElsewhere();

                // [DEBUG] Stress tests
                //if ((ImGui.GetFrameCount() % 30) == 0) docs[1].Open ^= 1;            // [DEBUG] Automatically show/hide a tab. Test various interactions e.g. dragging with this on.
                //if (Io.KeyCtrl) ImGui.SetTabItemSelected(docs[1].Name);              // [DEBUG] Test SetTabItemSelected(), probably not very useful as-is anyway..

                // Submit Tabs
                foreach (var doc in app.Documents)
                {
                    if (!doc.Open)
                        continue;

                    // As we allow to change document name, we append a never-changing document id so tabs are stable
                    string doc_name = ExampleAppDocuments.GetTabName(doc);
                    var tab_flags = doc.Dirty ? TabItemFlags.UnsavedDocument : TabItemFlags.None;
                    bool visible = ImGui.BeginTabItem(doc_name, ref doc.Open, tab_flags);

                    // Cancel attempt to close when unsaved add to save queue so we can display a popup.
                    if (!doc.Open && doc.Dirty)
                    {
                        doc.Open = true;
                        app.CloseQueue.Add(doc);
                    }

                    app.DisplayDocContextMenu(doc);
                    if (visible)
                    {
                        app.DisplayDocContents(doc);
                        ImGui.EndTabItem();
                    }
                }

                ImGui.EndTabBar();
            }
        }

        // Display renaming UI
        if (app.RenamingDoc != null)
        {
            if (app.RenamingStarted)
                ImGui.OpenPopup("Rename");
            if (ImGui.BeginPopup("Rename"))
            {
                var renaming_doc = app.RenamingDoc;
                ImGui.SetNextItemWidth(ImGui.GetFontSize() * 30);
                if (ImGui.InputText("###Name", ref renaming_doc.Name, InputTextFlags.EnterReturnsTrue))
                {
                    ImGui.CloseCurrentPopup();
                    app.RenamingDoc = null;
                }
                if (app.RenamingStarted)
                    ImGui.SetKeyboardFocusHere(-1);
                ImGui.EndPopup();
            }
            else
            {
                app.RenamingDoc = null;
            }
            app.RenamingStarted = false;
        }

        // Display closing confirmation UI
        if (app.CloseQueue.Count > 0)
        {
            int close_queue_unsaved_documents = 0;
            for (int n = 0; n < app.CloseQueue.Count; n++)
                if (app.CloseQueue[n].Dirty)
                    close_queue_unsaved_documents++;

            if (close_queue_unsaved_documents == 0)
            {
                // Close documents when all are unsaved
                for (int n = 0; n < app.CloseQueue.Count; n++)
                    app.CloseQueue[n].DoForceClose();
                app.CloseQueue.Clear();
            }
            else
            {
                if (!ImGui.IsPopupOpen("Save?"))
                    ImGui.OpenPopup("Save?");
                if (ImGui.BeginPopupModal("Save?", WindowFlags.AlwaysAutoResize))
                {
                    ImGui.Text("Save change to the following items?");
                    float item_height = ImGui.GetTextLineHeightWithSpacing();
                    if (ImGui.BeginChild(ImGui.GetID("frame"), -float.Epsilon /* -FLT_MIN */, 6.25f * item_height, ChildFlags.FrameStyle))
                        foreach (var doc in app.CloseQueue)
                            if (doc.Dirty)
                                ImGui.Text(doc.Name);
                    ImGui.EndChild();

                    float button_size_x = ImGui.GetFontSize() * 7.0f;
                    if (ImGui.Button("Yes", button_size_x, 0.0f))
                    {
                        foreach (var doc in app.CloseQueue)
                        {
                            if (doc.Dirty)
                                doc.DoSave();
                            doc.DoForceClose();
                        }
                        app.CloseQueue.Clear();
                        ImGui.CloseCurrentPopup();
                    }
                    ImGui.SameLine();
                    if (ImGui.Button("No", button_size_x, 0.0f))
                    {
                        foreach (var doc in app.CloseQueue)
                            doc.DoForceClose();
                        app.CloseQueue.Clear();
                        ImGui.CloseCurrentPopup();
                    }
                    ImGui.SameLine();
                    if (ImGui.Button("Cancel", button_size_x, 0.0f))
                    {
                        app.CloseQueue.Clear();
                        ImGui.CloseCurrentPopup();
                    }
                    ImGui.EndPopup();
                }
            }
        }

        ImGui.End();
    }

    //-----------------------------------------------------------------------------
    // [SECTION] Example App: Assets Browser / ShowExampleAppAssetsBrowser()
    //-----------------------------------------------------------------------------

    private sealed class ExampleAsset
    {
        public uint ID;
        public int Type;

        public ExampleAsset(uint id, int type) { ID = id; Type = type; }

        public static void SortWithSortSpecs(TableSortSpecs sort_specs, List<ExampleAsset> items)
        {
            if (items.Count > 1)
                items.Sort((a, b) => CompareWithSortSpecs(sort_specs, a, b));
        }

        // Compare function used by List<T>.Sort() (upstream: qsort + s_current_sort_specs)
        private static int CompareWithSortSpecs(TableSortSpecs sort_specs, ExampleAsset a, ExampleAsset b)
        {
            for (int n = 0; n < sort_specs.SpecsCount; n++)
            {
                var sort_spec = sort_specs.GetSpec(n);
                int delta = 0;
                if (sort_spec.ColumnIndex == 0)
                    delta = (int)a.ID - (int)b.ID;
                else if (sort_spec.ColumnIndex == 1)
                    delta = a.Type - b.Type;
                if (delta > 0)
                    return (sort_spec.SortDirection == SortDirection.Ascending) ? +1 : -1;
                if (delta < 0)
                    return (sort_spec.SortDirection == SortDirection.Ascending) ? -1 : +1;
            }
            return (int)a.ID - (int)b.ID;
        }
    }

    private sealed class ExampleAssetsBrowser
    {
        // Options
        public bool ShowTypeOverlay = true;
        public bool AllowSorting = true;
        public bool AllowBoxSelect = true;                  // Will set ImGuiMultiSelectFlags_BoxSelect2d
        public bool AllowBoxSelectInsideSelection = false;  // Will set ImGuiMultiSelectFlags_SelectOnClickAlways
        public bool AllowDragUnselected = false;            // Will set ImGuiMultiSelectFlags_SelectOnClickRelease
        public float IconSize = 32.0f;
        public int IconSpacing = 10;
        public int IconHitSpacing = 4;          // Increase hit-spacing if you want to make it possible to clear or box-select from gaps. Some spacing is required to able to amend with Shift+box-select. Value is small in Explorer.
        public bool StretchSpacing = true;

        // State
        public readonly List<ExampleAsset> Items = new();       // Our items
        public readonly SelectionBasicStorage Selection = new(); // Our selection (ImGuiSelectionBasicStorage + deletion helpers below, ported from upstream ExampleSelectionWithDeletion)
        public uint NextItemId = 0;             // Unique identifier when creating new items
        public bool RequestDelete = false;      // Deferred deletion request
        public bool RequestSort = false;        // Deferred sort request
        public float ZoomWheelAccum = 0.0f;     // Mouse wheel accumulator to handle smooth wheels better

        // Calculated sizes for layout, output of UpdateLayoutSizes(). Could be locals but our code is simpler this way.
        public Vec2 LayoutItemSize;
        public Vec2 LayoutItemStep;             // == LayoutItemSize + LayoutItemSpacing
        public float LayoutItemSpacing = 0.0f;
        public float LayoutSelectableSpacing = 0.0f;
        public float LayoutOuterPadding = 0.0f;
        public int LayoutColumnCount = 0;
        public int LayoutLineCount = 0;

        // Functions
        public ExampleAssetsBrowser()
        {
            // Use custom selection adapter: store ID in selection (recommended).
            // (Upstream assigns Selection.AdapterIndexToStorageId each frame in Draw(); the wrapper
            // registers a managed delegate once, which is equivalent as it captures 'this'.)
            Selection.SetIndexToStorageIdAdapter(idx => Items[idx].ID);
            AddItems(10000);
        }

        public void AddItems(int count)
        {
            if (Items.Count == 0)
                NextItemId = 0;
            Items.Capacity = Math.Max(Items.Capacity, Items.Count + count);
            for (int n = 0; n < count; n++, NextItemId++)
                Items.Add(new ExampleAsset(NextItemId, (NextItemId % 20) < 15 ? 0 : (NextItemId % 20) < 18 ? 1 : 2));
            RequestSort = true;
        }

        public void ClearItems()
        {
            Items.Clear();
            Selection.Clear();
        }

        // --- Deletion helpers, ported from upstream ExampleSelectionWithDeletion ---

        // Find which item should be Focused after deletion.
        // Call _before_ item submission. Return an index in the before-deletion item list, your item loop should call SetKeyboardFocusHere() on it.
        // The subsequent ApplyDeletionPostLoop() code will use it to apply Selection.
        // - We cannot provide this logic in core Dear ImGui because we don't have access to selection data.
        // - Important: Deletion only works if the underlying ImGuiID for your items are stable: aka not depend on their index, but on e.g. item id/ptr.
        public int ApplyDeletionPreLoop(MultiSelectIO ms_io, int items_count)
        {
            if (Selection.Size == 0)
                return -1;

            // If focused item is not selected...
            int focused_idx = (int)ms_io.NavIdItem;  // Index of currently focused item
            if (ms_io.NavIdSelected == false)  // This is merely a shortcut, == Contains(adapter->IndexToStorage(items, focused_idx))
            {
                ms_io.RangeSrcReset = true;    // Request to recover RangeSrc from NavId next frame. Would be ok to reset even when NavIdSelected==true, but it would take an extra frame to recover RangeSrc when deleting a selected item.
                return focused_idx;            // Request to focus same item after deletion.
            }

            // If focused item is selected: land on first unselected item after focused item.
            for (int idx = focused_idx + 1; idx < items_count; idx++)
                if (!Selection.Contains(Selection.GetStorageIdFromIndex(idx)))
                    return idx;

            // If focused item is selected: otherwise return last unselected item before focused item.
            for (int idx = Math.Min(focused_idx, items_count) - 1; idx >= 0; idx--)
                if (!Selection.Contains(Selection.GetStorageIdFromIndex(idx)))
                    return idx;

            return -1;
        }

        // Rewrite item list (delete items) + update selection.
        // - Call after EndMultiSelect()
        // - We cannot provide this logic in core Dear ImGui because we don't have access to your items, nor to selection data.
        public void ApplyDeletionPostLoop(MultiSelectIO ms_io, int item_curr_idx_to_select)
        {
            // Rewrite item list (delete items) + convert old selection index (before deletion) to new selection index (after selection).
            // If NavId was not part of selection, we will stay on same item.
            var new_items = new List<ExampleAsset>(Items.Count - Selection.Size);
            int item_next_idx_to_select = -1;
            for (int idx = 0; idx < Items.Count; idx++)
            {
                if (!Selection.Contains(Selection.GetStorageIdFromIndex(idx)))
                    new_items.Add(Items[idx]);
                if (item_curr_idx_to_select == idx)
                    item_next_idx_to_select = new_items.Count - 1;
            }
            Items.Clear();
            Items.AddRange(new_items);

            // Update selection
            Selection.Clear();
            if (item_next_idx_to_select != -1 && ms_io.NavIdSelected)
                Selection.SetItemSelected(Selection.GetStorageIdFromIndex(item_next_idx_to_select), true);
        }

        // Logic would be written in the main code BeginChild() and outputting to local variables.
        // We extracted it into a function so we can call it easily from multiple places.
        public void UpdateLayoutSizes(float avail_width)
        {
            // Layout: when not stretching: allow extending into right-most spacing.
            LayoutItemSpacing = IconSpacing;
            if (StretchSpacing == false)
                avail_width += MathF.Floor(LayoutItemSpacing * 0.5f);

            // Layout: calculate number of icon per line and number of lines
            LayoutItemSize = new Vec2(MathF.Floor(IconSize), MathF.Floor(IconSize));
            LayoutColumnCount = Math.Max((int)(avail_width / (LayoutItemSize.X + LayoutItemSpacing)), 1);
            LayoutLineCount = (Items.Count + LayoutColumnCount - 1) / LayoutColumnCount;

            // Layout: when stretching: allocate remaining space to more spacing. Round before division, so item_spacing may be non-integer.
            if (StretchSpacing && LayoutColumnCount > 1)
                LayoutItemSpacing = MathF.Floor(avail_width - LayoutItemSize.X * LayoutColumnCount) / LayoutColumnCount;

            LayoutItemStep = new Vec2(LayoutItemSize.X + LayoutItemSpacing, LayoutItemSize.Y + LayoutItemSpacing);
            LayoutSelectableSpacing = Math.Max(MathF.Floor(LayoutItemSpacing) - IconHitSpacing, 0.0f);
            LayoutOuterPadding = MathF.Floor(LayoutItemSpacing * 0.5f);
        }

        public void Draw(string title, ref bool open)
        {
            // IM_COL32(r,g,b,a) equivalent: packed ABGR (a<<24 | b<<16 | g<<8 | r)
            static uint Col32(byte r, byte g, byte b, byte a) => ((uint)a << 24) | ((uint)b << 16) | ((uint)g << 8) | r;

            ImGui.SetNextWindowSize(IconSize * 25, IconSize * 15, Cond.FirstUseEver);
            if (!ImGui.Begin(title, ref open, WindowFlags.MenuBar))
            {
                ImGui.End();
                return;
            }
            // DEMO MARKER: Examples/Assets Browser

            // Menu bar
            if (ImGui.BeginMenuBar())
            {
                if (ImGui.BeginMenu("File"))
                {
                    if (ImGui.MenuItem("Add 10000 items"))
                        AddItems(10000);
                    if (ImGui.MenuItem("Clear items"))
                        ClearItems();
                    ImGui.Separator();
                    if (ImGui.MenuItem("Close"))
                        open = false;
                    ImGui.EndMenu();
                }
                if (ImGui.BeginMenu("Edit"))
                {
                    if (ImGui.MenuItem("Delete", "Del", false, Selection.Size > 0))
                        RequestDelete = true;
                    ImGui.EndMenu();
                }
                if (ImGui.BeginMenu("Options"))
                {
                    ImGui.PushItemWidth(ImGui.GetFontSize() * 10);

                    ImGui.SeparatorText("Contents");
                    ImGui.Checkbox("Show Type Overlay", ref ShowTypeOverlay);
                    ImGui.Checkbox("Allow Sorting", ref AllowSorting);

                    ImGui.SeparatorText("Selection Behavior");
                    ImGui.Checkbox("Allow box-selection", ref AllowBoxSelect);
                    if (ImGui.Checkbox("Allow box-selection from selected items", ref AllowBoxSelectInsideSelection) && AllowBoxSelectInsideSelection)
                        AllowDragUnselected = false;
                    if (ImGui.Checkbox("Allow dragging unselected item", ref AllowDragUnselected) && AllowDragUnselected)
                        AllowBoxSelectInsideSelection = false;

                    ImGui.SeparatorText("Layout");
                    ImGui.SliderFloat("Icon Size", ref IconSize, 16.0f, 128.0f, "%.0f");
                    ImGui.SameLine(); HelpMarker("Use Ctrl+Wheel to zoom");
                    ImGui.SliderInt("Icon Spacing", ref IconSpacing, 0, 32);
                    ImGui.SliderInt("Icon Hit Spacing", ref IconHitSpacing, 0, 32);
                    ImGui.Checkbox("Stretch Spacing", ref StretchSpacing);
                    ImGui.PopItemWidth();
                    ImGui.EndMenu();
                }
                ImGui.EndMenuBar();
            }

            // Show a table with ONLY one header row to showcase the idea/possibility of using this to provide a sorting UI
            if (AllowSorting)
            {
                ImGui.PushStyleVar(StyleVar.ItemSpacing, 0, 0);
                var table_flags_for_sort_specs = TableFlags.Sortable | TableFlags.SortMulti | TableFlags.SizingFixedFit | TableFlags.Borders;
                if (ImGui.BeginTable("for_sort_specs_only", 2, table_flags_for_sort_specs, 0.0f, ImGui.GetFrameHeight()))
                {
                    ImGui.TableSetupColumn("Index");
                    ImGui.TableSetupColumn("Type");
                    ImGui.TableHeadersRow();
                    var sort_specs = ImGui.TableGetSortSpecs();
                    if (sort_specs.IsValid && (sort_specs.SpecsDirty || RequestSort))
                    {
                        ExampleAsset.SortWithSortSpecs(sort_specs, Items);
                        sort_specs.SpecsDirty = false;
                        RequestSort = false;
                    }
                    ImGui.EndTable();
                }
                ImGui.PopStyleVar();
            }

            ImGui.SetNextWindowContentSize(0.0f, LayoutOuterPadding + LayoutLineCount * (LayoutItemSize.Y + LayoutItemSpacing));
            if (ImGui.BeginChild("Assets", 0.0f, -ImGui.GetTextLineHeightWithSpacing(), ChildFlags.Borders, WindowFlags.NoMove))
            {
                DrawList draw_list = ImGui.GetWindowDrawList();

                float avail_width = ImGui.GetContentRegionAvail().Width;
                UpdateLayoutSizes(avail_width);

                // Calculate and store start position.
                var cursor = ImGui.GetCursorScreenPos();
                Vec2 start_pos = new(cursor.X + LayoutOuterPadding, cursor.Y + LayoutOuterPadding);
                ImGui.SetCursorScreenPos(start_pos.X, start_pos.Y);

                // Multi-select
                var ms_flags = MultiSelectFlags.ClearOnEscape | MultiSelectFlags.ClearOnClickVoid;

                // - Enable box-select (in 2D mode, so that changing box-select rectangle X1/X2 boundaries will affect clipped items)
                if (AllowBoxSelect)
                    ms_flags |= MultiSelectFlags.BoxSelect2d;

                // - Selection mode
                if (AllowDragUnselected)
                    ms_flags |= MultiSelectFlags.SelectOnClickRelease; // Rarely used: Allows dragging an unselected item without selecting it (rarely used)
                else if (AllowBoxSelectInsideSelection)
                    ms_flags |= MultiSelectFlags.SelectOnClickAlways; // Rarely used: Prevents Drag and Drop from being used on multiple-selection, but allows e.g. BoxSelect to always reselect even when clicking inside an existing selection.

                // - Enable keyboard wrapping on X axis
                ms_flags |= MultiSelectFlags.NavWrapX;

                var ms_io = ImGui.BeginMultiSelect(ms_flags, Selection.Size, Items.Count);

                // Use custom selection adapter: store ID in selection (recommended)
                // (Adapter registered once in the constructor — see SetIndexToStorageIdAdapter.)
                Selection.ApplyRequests(ms_io);

                bool want_delete = (ImGui.Shortcut(Key.Delete, InputFlags.Repeat) && (Selection.Size > 0)) || RequestDelete;
                int item_curr_idx_to_focus = want_delete ? ApplyDeletionPreLoop(ms_io, Items.Count) : -1;
                RequestDelete = false;

                // Push LayoutSelectableSpacing (which is LayoutItemSpacing minus hit-spacing, if we decide to have hit gaps between items)
                // Altering style ItemSpacing may seem unnecessary as we position every items using SetCursorScreenPos()...
                // But it is necessary for two reasons:
                // - Selectables uses it by default to visually fill the space between two items.
                // - The vertical spacing would be measured by Clipper to calculate line height if we didn't provide it explicitly (here we do).
                ImGui.PushStyleVar(StyleVar.ItemSpacing, LayoutSelectableSpacing, LayoutSelectableSpacing);

                // Rendering parameters
                ReadOnlySpan<uint> icon_type_overlay_colors = stackalloc uint[] { 0, Col32(200, 70, 70, 255), Col32(70, 170, 70, 255) };
                uint icon_bg_color = ImGui.GetColorU32(Col32(35, 35, 35, 220));
                Vec2 icon_type_overlay_size = new(4.0f, 4.0f);
                bool display_label = LayoutItemSize.X >= ImGui.CalcTextSize("999").Width;

                int column_count = LayoutColumnCount;
                using var clipper = new ListClipper();
                clipper.Begin(LayoutLineCount, LayoutItemStep.Y);
                if (item_curr_idx_to_focus != -1)
                    clipper.IncludeItemsByIndex(item_curr_idx_to_focus / column_count, item_curr_idx_to_focus / column_count + 1); // Ensure focused item line is not clipped.
                if (ms_io.RangeSrcItem != -1)
                    clipper.IncludeItemsByIndex((int)ms_io.RangeSrcItem / column_count, (int)ms_io.RangeSrcItem / column_count + 1); // Ensure RangeSrc item line is not clipped.
                while (clipper.Step())
                {
                    for (int line_idx = clipper.DisplayStart; line_idx < clipper.DisplayEnd; line_idx++)
                    {
                        int item_min_idx_for_current_line = line_idx * column_count;
                        int item_max_idx_for_current_line = Math.Min((line_idx + 1) * column_count, Items.Count);
                        for (int item_idx = item_min_idx_for_current_line; item_idx < item_max_idx_for_current_line; ++item_idx)
                        {
                            var item_data = Items[item_idx];
                            ImGui.PushID((int)item_data.ID);

                            // Position item
                            Vec2 pos = new(start_pos.X + (item_idx % column_count) * LayoutItemStep.X, start_pos.Y + line_idx * LayoutItemStep.Y);
                            ImGui.SetCursorScreenPos(pos.X, pos.Y);

                            ImGui.SetNextItemSelectionUserData(item_idx);
                            bool item_is_selected = Selection.Contains(item_data.ID);
                            bool item_is_visible = ImGui.IsRectVisible(LayoutItemSize);
                            ImGui.Selectable("", item_is_selected, SelectableFlags.None, LayoutItemSize.X, LayoutItemSize.Y);

                            // Update our selection state immediately (without waiting for EndMultiSelect() requests)
                            // because we use this to alter the color of our text/icon.
                            if (ImGui.IsItemToggledSelection())
                                item_is_selected = !item_is_selected;

                            // Focus (for after deletion)
                            if (item_curr_idx_to_focus == item_idx)
                                ImGui.SetKeyboardFocusHere(-1);

                            // Drag and drop
                            if (ImGui.BeginDragDropSource())
                            {
                                // Create payload with full selection OR single unselected item.
                                // (the later is only possible when using ImGuiMultiSelectFlags_SelectOnClickRelease)
                                if (!ImGui.GetDragDropPayload().IsValid)
                                {
                                    var payload_items = new List<uint>();
                                    if (!item_is_selected)
                                        payload_items.Add(item_data.ID);
                                    else
                                        foreach (uint id in Selection.SelectedItems)
                                            payload_items.Add(id);
                                    ImGui.SetDragDropPayload("ASSETS_BROWSER_ITEMS", MemoryMarshal.AsBytes(CollectionsMarshal.AsSpan(payload_items)));
                                }

                                // Display payload content in tooltip, by extracting it from the payload data
                                // (we could read from selection, but it is more correct and reusable to read from payload)
                                var payload = ImGui.GetDragDropPayload();
                                int payload_count = payload.Data.Length / sizeof(uint);
                                ImGui.Text($"{payload_count} assets");

                                ImGui.EndDragDropSource();
                            }

                            // Render icon (a real app would likely display an image/thumbnail here)
                            // Because we use ImGuiMultiSelectFlags_BoxSelect2d, clipping vertical may occasionally be larger, so we coarse-clip our rendering as well.
                            if (item_is_visible)
                            {
                                Vec2 box_min = new(pos.X - 1, pos.Y - 1);
                                Vec2 box_max = new(box_min.X + LayoutItemSize.X + 2, box_min.Y + LayoutItemSize.Y + 2); // Dubious
                                draw_list.AddRectFilled(box_min, box_max, icon_bg_color); // Background color
                                if (ShowTypeOverlay && item_data.Type != 0)
                                {
                                    uint type_col = icon_type_overlay_colors[item_data.Type % icon_type_overlay_colors.Length];
                                    draw_list.AddRectFilled(new Vec2(box_max.X - 2 - icon_type_overlay_size.X, box_min.Y + 2), new Vec2(box_max.X - 2, box_min.Y + 2 + icon_type_overlay_size.Y), type_col);
                                }
                                if (display_label)
                                {
                                    uint label_col = ImGui.GetColorU32(item_is_selected ? Col.Text : Col.TextDisabled);
                                    draw_list.AddText(new Vec2(box_min.X, box_max.Y - ImGui.GetFontSize()), label_col, item_data.ID.ToString());
                                }
                            }

                            ImGui.PopID();
                        }
                    }
                }
                clipper.End();
                ImGui.PopStyleVar(); // ImGuiStyleVar_ItemSpacing

                // Context menu
                if (ImGui.BeginPopupContextWindow())
                {
                    ImGui.Text($"Selection: {Selection.Size} items");
                    ImGui.Separator();
                    if (ImGui.MenuItem("Delete", "Del", false, Selection.Size > 0))
                        RequestDelete = true;
                    ImGui.EndPopup();
                }

                ms_io = ImGui.EndMultiSelect();
                Selection.ApplyRequests(ms_io);
                if (want_delete)
                    ApplyDeletionPostLoop(ms_io, item_curr_idx_to_focus);

                // Zooming with Ctrl+Wheel
                if (ImGui.IsWindowAppearing())
                    ZoomWheelAccum = 0.0f;
                if (ImGui.IsWindowHovered() && Io.MouseWheel != 0.0f && ImGui.IsKeyDown(Key.ModCtrl) && ImGui.IsAnyItemActive() == false)
                {
                    ZoomWheelAccum += Io.MouseWheel;
                    if (MathF.Abs(ZoomWheelAccum) >= 1.0f)
                    {
                        // Calculate hovered item index from mouse location
                        // FIXME: Locking aiming on 'hovered_item_idx' (with a cool-down timer) would ensure zoom keeps on it.
                        float hovered_item_nx = (Io.MousePos.X - start_pos.X + LayoutItemSpacing * 0.5f) / LayoutItemStep.X;
                        float hovered_item_ny = (Io.MousePos.Y - start_pos.Y + LayoutItemSpacing * 0.5f) / LayoutItemStep.Y;
                        int hovered_item_idx = ((int)hovered_item_ny * LayoutColumnCount) + (int)hovered_item_nx;
                        //ImGui.SetTooltip($"{hovered_item_nx},{hovered_item_ny} -> item {hovered_item_idx}"); // Move those 4 lines in block above for easy debugging

                        // Zoom
                        IconSize *= MathF.Pow(1.1f, (int)ZoomWheelAccum);
                        IconSize = Math.Clamp(IconSize, 16.0f, 128.0f);
                        ZoomWheelAccum -= (int)ZoomWheelAccum;
                        UpdateLayoutSizes(avail_width);

                        // Manipulate scroll to that we will land at the same Y location of currently hovered item.
                        // - Calculate next frame position of item under mouse
                        // - Set new scroll position to be used in next ImGui::BeginChild() call.
                        float hovered_item_rel_pos_y = ((float)(hovered_item_idx / LayoutColumnCount) + hovered_item_ny % 1.0f) * LayoutItemStep.Y;
                        hovered_item_rel_pos_y += Style.WindowPadding.Y;
                        float mouse_local_y = Io.MousePos.Y - ImGui.GetWindowPos().Y;
                        ImGui.SetScrollY(hovered_item_rel_pos_y - mouse_local_y);
                    }
                }
            }
            ImGui.EndChild();

            ImGui.Text($"Selected: {Selection.Size}/{Items.Count} items");
            ImGui.End();
        }
    }

    static readonly ExampleAssetsBrowser s_assets_browser = new();

    private static void ShowExampleAppAssetsBrowser(ref bool open)
    {
        // DEMO MARKER: Examples/Assets Browser
        s_assets_browser.Draw("Example: Assets Browser", ref open);
    }
}
