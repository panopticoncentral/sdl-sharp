namespace SdlSharp;

/// <summary>
/// Priority levels for hint values (higher priorities override lower ones).
/// </summary>
public enum HintPriority
{
    /// <summary>Low priority; used for default values.</summary>
    Default = (int)Native.SDL_HintPriority.SDL_HINT_DEFAULT,
    /// <summary>Normal priority.</summary>
    Normal = (int)Native.SDL_HintPriority.SDL_HINT_NORMAL,
    /// <summary>High priority; overrides normal-priority values.</summary>
    Override = (int)Native.SDL_HintPriority.SDL_HINT_OVERRIDE,
}
