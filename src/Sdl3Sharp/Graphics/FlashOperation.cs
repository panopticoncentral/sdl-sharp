namespace Sdl3Sharp.Graphics;

/// <summary>
/// Window flash operation.
/// </summary>
public enum FlashOperation
{
    /// <summary>Cancel any window flash state.</summary>
    Cancel,
    /// <summary>Flash the window briefly to get attention.</summary>
    Briefly,
    /// <summary>Flash the window until it gets focus.</summary>
    UntilFocused
}
