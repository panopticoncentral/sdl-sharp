namespace Sdl3Sharp;

/// <summary>
/// The predefined log categories.
/// </summary>
/// <remarks>
/// By default the application and GPU categories are enabled at the INFO
/// level, the assert category is enabled at the WARN level, test is enabled at
/// the VERBOSE level and all other categories are enabled at the ERROR level.
/// </remarks>
public enum LogCategory
{
    /// <summary>
    /// Application log category.
    /// </summary>
    Application = 0,

    /// <summary>
    /// Error log category.
    /// </summary>
    Error = 1,

    /// <summary>
    /// Assert log category.
    /// </summary>
    Assert = 2,

    /// <summary>
    /// System log category.
    /// </summary>
    System = 3,

    /// <summary>
    /// Audio log category.
    /// </summary>
    Audio = 4,

    /// <summary>
    /// Video log category.
    /// </summary>
    Video = 5,

    /// <summary>
    /// Render log category.
    /// </summary>
    Render = 6,

    /// <summary>
    /// Input log category.
    /// </summary>
    Input = 7,

    /// <summary>
    /// Test log category.
    /// </summary>
    Test = 8,

    /// <summary>
    /// GPU log category.
    /// </summary>
    Gpu = 9,

    /// <summary>
    /// Reserved for future SDL library use.
    /// </summary>
    Reserved2 = 10,

    /// <summary>
    /// Reserved for future SDL library use.
    /// </summary>
    Reserved3 = 11,

    /// <summary>
    /// Reserved for future SDL library use.
    /// </summary>
    Reserved4 = 12,

    /// <summary>
    /// Reserved for future SDL library use.
    /// </summary>
    Reserved5 = 13,

    /// <summary>
    /// Reserved for future SDL library use.
    /// </summary>
    Reserved6 = 14,

    /// <summary>
    /// Reserved for future SDL library use.
    /// </summary>
    Reserved7 = 15,

    /// <summary>
    /// Reserved for future SDL library use.
    /// </summary>
    Reserved8 = 16,

    /// <summary>
    /// Reserved for future SDL library use.
    /// </summary>
    Reserved9 = 17,

    /// <summary>
    /// Reserved for future SDL library use.
    /// </summary>
    Reserved10 = 18,

    /// <summary>
    /// Starting point for application-defined log categories.
    /// </summary>
    Custom = 19
}
