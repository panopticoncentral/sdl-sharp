namespace SdlSharp
{
    /// <summary>
    /// Categories of log messages.
    /// </summary>
    public enum LogCategory
    {
        /// <summary>
        /// Application
        /// </summary>
        Application = Native.SDL_LogCategory.SDL_LOG_CATEGORY_APPLICATION,

        /// <summary>
        /// Error
        /// </summary>
        Error = Native.SDL_LogCategory.SDL_LOG_CATEGORY_ERROR,

        /// <summary>
        /// Assert
        /// </summary>
        Assert = Native.SDL_LogCategory.SDL_LOG_CATEGORY_ASSERT,

        /// <summary>
        /// System
        /// </summary>
        System = Native.SDL_LogCategory.SDL_LOG_CATEGORY_SYSTEM,

        /// <summary>
        /// Audio
        /// </summary>
        Audio = Native.SDL_LogCategory.SDL_LOG_CATEGORY_AUDIO,

        /// <summary>
        /// Video
        /// </summary>
        Video = Native.SDL_LogCategory.SDL_LOG_CATEGORY_VIDEO,

        /// <summary>
        /// Render
        /// </summary>
        Render = Native.SDL_LogCategory.SDL_LOG_CATEGORY_RENDER,

        /// <summary>
        /// Input
        /// </summary>
        Input = Native.SDL_LogCategory.SDL_LOG_CATEGORY_INPUT,

        /// <summary>
        /// Test
        /// </summary>
        Test = Native.SDL_LogCategory.SDL_LOG_CATEGORY_TEST,

        /// <summary>
        /// Custom
        /// </summary>
        Custom = Native.SDL_LogCategory.SDL_LOG_CATEGORY_CUSTOM
    }
}
