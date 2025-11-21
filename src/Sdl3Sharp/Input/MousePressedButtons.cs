namespace Sdl3Sharp.Input;

/// <summary>
/// A bitmask of pressed mouse buttons.
/// </summary>
[Flags]
public enum MousePressedButtons : uint
{
    /// <summary>No buttons pressed.</summary>
    None = 0,
    /// <summary>Left mouse button.</summary>
    Left = 1u << 0,
    /// <summary>Middle mouse button.</summary>
    Middle = 1u << 1,
    /// <summary>Right mouse button.</summary>
    Right = 1u << 2,
    /// <summary>Side mouse button 1 (X1).</summary>
    X1 = 1u << 3,
    /// <summary>Side mouse button 2 (X2).</summary>
    X2 = 1u << 4
}
