namespace SdlSharp;

/// <summary>
/// The type of an SDL property value.
/// </summary>
public enum PropertyType
{
    /// <summary>Invalid or nonexistent property.</summary>
    Invalid = Native.SDL_PropertyType.SDL_PROPERTY_TYPE_INVALID,

    /// <summary>Pointer property.</summary>
    Pointer = Native.SDL_PropertyType.SDL_PROPERTY_TYPE_POINTER,

    /// <summary>String property.</summary>
    String = Native.SDL_PropertyType.SDL_PROPERTY_TYPE_STRING,

    /// <summary>Number (long) property.</summary>
    Number = Native.SDL_PropertyType.SDL_PROPERTY_TYPE_NUMBER,

    /// <summary>Float property.</summary>
    Float = Native.SDL_PropertyType.SDL_PROPERTY_TYPE_FLOAT,

    /// <summary>Boolean property.</summary>
    Boolean = Native.SDL_PropertyType.SDL_PROPERTY_TYPE_BOOLEAN,
}
