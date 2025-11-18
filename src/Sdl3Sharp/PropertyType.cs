using static Sdl3Sharp.Native.Properties;

namespace Sdl3Sharp;

/// <summary>
/// SDL property type.
/// </summary>
public enum PropertyType
{
    /// <summary>
    /// Invalid or unset property type.
    /// </summary>
    Invalid = SDL_PropertyType.SDL_PROPERTY_TYPE_INVALID,

    /// <summary>
    /// Pointer property type.
    /// </summary>
    Pointer = SDL_PropertyType.SDL_PROPERTY_TYPE_POINTER,

    /// <summary>
    /// String property type.
    /// </summary>
    String = SDL_PropertyType.SDL_PROPERTY_TYPE_STRING,

    /// <summary>
    /// Number (64-bit signed integer) property type.
    /// </summary>
    Number = SDL_PropertyType.SDL_PROPERTY_TYPE_NUMBER,

    /// <summary>
    /// Float (single-precision floating point) property type.
    /// </summary>
    Float = SDL_PropertyType.SDL_PROPERTY_TYPE_FLOAT,

    /// <summary>
    /// Boolean property type.
    /// </summary>
    Boolean = SDL_PropertyType.SDL_PROPERTY_TYPE_BOOLEAN
}