using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for ImDrawList functions.
/// </summary>
/// <remarks>
/// Legacy: bit 0 must always correspond to Closed to be backward compatible with old API using a bool.
/// Bits 1..3 must be unused.
/// </remarks>
[Flags]
public enum DrawFlags
{
    /// <summary>
    /// No flags.
    /// </summary>
    None = 0,

    /// <summary>
    /// PathStroke(), AddPolyline(): specify that shape should be closed.
    /// </summary>
    /// <remarks>
    /// Important: this is always == 1 for legacy reason.
    /// </remarks>
    Closed = 1 << 0,

    /// <summary>
    /// AddRect(), AddRectFilled(), PathRect(): enable rounding top-left corner only (when rounding > 0.0f).
    /// </summary>
    RoundCornersTopLeft = 1 << 4,

    /// <summary>
    /// AddRect(), AddRectFilled(), PathRect(): enable rounding top-right corner only (when rounding > 0.0f).
    /// </summary>
    RoundCornersTopRight = 1 << 5,

    /// <summary>
    /// AddRect(), AddRectFilled(), PathRect(): enable rounding bottom-left corner only (when rounding > 0.0f).
    /// </summary>
    RoundCornersBottomLeft = 1 << 6,

    /// <summary>
    /// AddRect(), AddRectFilled(), PathRect(): enable rounding bottom-right corner only (when rounding > 0.0f).
    /// </summary>
    RoundCornersBottomRight = 1 << 7,

    /// <summary>
    /// AddRect(), AddRectFilled(), PathRect(): disable rounding on all corners (when rounding > 0.0f).
    /// </summary>
    /// <remarks>
    /// This is NOT zero, NOT an implicit flag!
    /// </remarks>
    RoundCornersNone = 1 << 8,

    /// <summary>
    /// AddRect(), AddRectFilled(), PathRect(): enable rounding on top corners only.
    /// </summary>
    RoundCornersTop = RoundCornersTopLeft | RoundCornersTopRight,

    /// <summary>
    /// AddRect(), AddRectFilled(), PathRect(): enable rounding on bottom corners only.
    /// </summary>
    RoundCornersBottom = RoundCornersBottomLeft | RoundCornersBottomRight,

    /// <summary>
    /// AddRect(), AddRectFilled(), PathRect(): enable rounding on left corners only.
    /// </summary>
    RoundCornersLeft = RoundCornersTopLeft | RoundCornersBottomLeft,

    /// <summary>
    /// AddRect(), AddRectFilled(), PathRect(): enable rounding on right corners only.
    /// </summary>
    RoundCornersRight = RoundCornersTopRight | RoundCornersBottomRight,

    /// <summary>
    /// AddRect(), AddRectFilled(), PathRect(): enable rounding on all corners.
    /// </summary>
    RoundCornersAll = RoundCornersTopLeft | RoundCornersTopRight | RoundCornersBottomLeft | RoundCornersBottomRight
}

/// <summary>
/// Extension methods for converting between DrawFlags and ImDrawFlags.
/// </summary>
internal static class DrawFlagsExtensions
{
    /// <summary>
    /// Converts DrawFlags to ImDrawFlags.
    /// </summary>
    internal static ImDrawFlags ToNative(this DrawFlags flags) => (ImDrawFlags)flags;

    /// <summary>
    /// Converts ImDrawFlags to DrawFlags.
    /// </summary>
    internal static DrawFlags ToManaged(this ImDrawFlags flags) => (DrawFlags)flags;
}
