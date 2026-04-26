namespace SdlSharp.Gui;

/// <summary>Flags for <see cref="DrawList"/> functions.</summary>
[Flags]
public enum DrawFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>PathStroke / AddPolyline: specify that shape should be closed.</summary>
    Closed = 1 << 0,
    /// <summary>AddRect / AddRectFilled / PathRect: enable rounding top-left corner only.</summary>
    RoundCornersTopLeft = 1 << 4,
    /// <summary>AddRect / AddRectFilled / PathRect: enable rounding top-right corner only.</summary>
    RoundCornersTopRight = 1 << 5,
    /// <summary>AddRect / AddRectFilled / PathRect: enable rounding bottom-left corner only.</summary>
    RoundCornersBottomLeft = 1 << 6,
    /// <summary>AddRect / AddRectFilled / PathRect: enable rounding bottom-right corner only.</summary>
    RoundCornersBottomRight = 1 << 7,
    /// <summary>Disable rounding on all corners (not zero — an explicit flag).</summary>
    RoundCornersNone = 1 << 8,
    /// <summary>Round top corners (both left and right).</summary>
    RoundCornersTop = RoundCornersTopLeft | RoundCornersTopRight,
    /// <summary>Round bottom corners (both left and right).</summary>
    RoundCornersBottom = RoundCornersBottomLeft | RoundCornersBottomRight,
    /// <summary>Round left corners (both top and bottom).</summary>
    RoundCornersLeft = RoundCornersBottomLeft | RoundCornersTopLeft,
    /// <summary>Round right corners (both top and bottom).</summary>
    RoundCornersRight = RoundCornersBottomRight | RoundCornersTopRight,
    /// <summary>Round all corners (default when none specified).</summary>
    RoundCornersAll = RoundCornersTopLeft | RoundCornersTopRight | RoundCornersBottomLeft | RoundCornersBottomRight,
}
