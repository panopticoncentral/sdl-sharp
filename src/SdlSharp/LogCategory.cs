namespace SdlSharp;

/// <summary>
/// SDL log categories.
/// </summary>
public enum LogCategory
{
    /// <summary>Application log category.</summary>
    Application = (int)Native.SDL_LogCategory.SDL_LOG_CATEGORY_APPLICATION,
    /// <summary>Error log category.</summary>
    Error = (int)Native.SDL_LogCategory.SDL_LOG_CATEGORY_ERROR,
    /// <summary>Assert log category.</summary>
    Assert = (int)Native.SDL_LogCategory.SDL_LOG_CATEGORY_ASSERT,
    /// <summary>System log category.</summary>
    System = (int)Native.SDL_LogCategory.SDL_LOG_CATEGORY_SYSTEM,
    /// <summary>Audio log category.</summary>
    Audio = (int)Native.SDL_LogCategory.SDL_LOG_CATEGORY_AUDIO,
    /// <summary>Video log category.</summary>
    Video = (int)Native.SDL_LogCategory.SDL_LOG_CATEGORY_VIDEO,
    /// <summary>Render log category.</summary>
    Render = (int)Native.SDL_LogCategory.SDL_LOG_CATEGORY_RENDER,
    /// <summary>Input log category.</summary>
    Input = (int)Native.SDL_LogCategory.SDL_LOG_CATEGORY_INPUT,
    /// <summary>Test log category.</summary>
    Test = (int)Native.SDL_LogCategory.SDL_LOG_CATEGORY_TEST,
    /// <summary>GPU log category.</summary>
    Gpu = (int)Native.SDL_LogCategory.SDL_LOG_CATEGORY_GPU,
    /// <summary>Start of custom log categories.</summary>
    Custom = (int)Native.SDL_LogCategory.SDL_LOG_CATEGORY_CUSTOM,
}
