using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using static Sdl3Sharp.Native.Common;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_properties.h - SDL properties system APIs.
/// </summary>
public static unsafe partial class Properties
{
    /// <summary>
    /// SDL properties ID.
    /// </summary>
    public struct SDL_PropertiesID(uint value)
    {
        /// <summary>
        /// The value of the properties ID.
        /// </summary>
        public uint Value = value;

        /// <summary>
        /// Implicitly convert between SDL_PropertiesID and uint.
        /// </summary>
        public static implicit operator uint(SDL_PropertiesID id)
        {
            return id.Value;
        }

        /// <summary>
        /// Implicitly convert between uint and SDL_PropertiesID.
        /// </summary>
        public static implicit operator SDL_PropertiesID(uint value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// SDL property type.
    /// </summary>
    public enum SDL_PropertyType
    {
        /// <summary>
        /// Invalid or unset property type.
        /// </summary>
        SDL_PROPERTY_TYPE_INVALID,

        /// <summary>
        /// Pointer property type.
        /// </summary>
        SDL_PROPERTY_TYPE_POINTER,

        /// <summary>
        /// String property type.
        /// </summary>
        SDL_PROPERTY_TYPE_STRING,

        /// <summary>
        /// Number (64-bit signed integer) property type.
        /// </summary>
        SDL_PROPERTY_TYPE_NUMBER,

        /// <summary>
        /// Float (single-precision floating point) property type.
        /// </summary>
        SDL_PROPERTY_TYPE_FLOAT,

        /// <summary>
        /// Boolean property type.
        /// </summary>
        SDL_PROPERTY_TYPE_BOOLEAN
    }

    /// <summary>
    /// Get the global SDL properties.
    /// </summary>
    /// <returns>a valid property ID on success or 0 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_GetGlobalProperties();

    /// <summary>
    /// Create a group of properties.
    /// </summary>
    /// <returns>an ID for a new group of properties, or 0 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_CreateProperties();

    /// <summary>
    /// Copy a group of properties.
    /// </summary>
    /// <param name="src">the properties to copy.</param>
    /// <param name="dst">the destination properties.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_CopyProperties(SDL_PropertiesID src, SDL_PropertiesID dst);

    /// <summary>
    /// Lock a group of properties.
    /// </summary>
    /// <param name="props">the properties to lock.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_LockProperties(SDL_PropertiesID props);

    /// <summary>
    /// Unlock a group of properties.
    /// </summary>
    /// <param name="props">the properties to unlock.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_UnlockProperties(SDL_PropertiesID props);

    /// <summary>
    /// Set a pointer property in a group of properties with a cleanup function that is called when the property is deleted.
    /// </summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property, or NULL to delete the property.</param>
    /// <param name="cleanup">the function to call when this property is deleted, or NULL if no cleanup is necessary.</param>
    /// <param name="userdata">an opaque value that is passed to the cleanup function.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetPointerPropertyWithCleanup(SDL_PropertiesID props, [MarshalUsing(typeof(Utf8StringMarshaller))] string name, void* value, delegate* unmanaged[Cdecl]<nuint, void*, void> cleanup, nuint userdata);

    /// <summary>
    /// Set a pointer property in a group of properties.
    /// </summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property, or NULL to delete the property.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetPointerProperty(SDL_PropertiesID props, [MarshalUsing(typeof(Utf8StringMarshaller))] string name, void* value);

    /// <summary>
    /// Set a string property in a group of properties.
    /// </summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property, or NULL to delete the property.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetStringProperty(SDL_PropertiesID props, [MarshalUsing(typeof(Utf8StringMarshaller))] string name, [MarshalUsing(typeof(Utf8StringMarshaller))] string? value);

    /// <summary>
    /// Set an integer property in a group of properties.
    /// </summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetNumberProperty(SDL_PropertiesID props, [MarshalUsing(typeof(Utf8StringMarshaller))] string name, long value);

    /// <summary>
    /// Set a floating point property in a group of properties.
    /// </summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetFloatProperty(SDL_PropertiesID props, [MarshalUsing(typeof(Utf8StringMarshaller))] string name, float value);

    /// <summary>
    /// Set a boolean property in a group of properties.
    /// </summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetBooleanProperty(SDL_PropertiesID props, [MarshalUsing(typeof(Utf8StringMarshaller))] string name, [MarshalAs(UnmanagedType.U1)] bool value);

    /// <summary>
    /// Return whether a property exists in a group of properties.
    /// </summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <returns>true if the property exists, or false if it doesn't.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasProperty(SDL_PropertiesID props, [MarshalUsing(typeof(Utf8StringMarshaller))] string name);

    /// <summary>
    /// Get the type of a property in a group of properties.
    /// </summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <returns>the type of the property, or SDL_PROPERTY_TYPE_INVALID if it is not set.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial SDL_PropertyType SDL_GetPropertyType(SDL_PropertiesID props, [MarshalUsing(typeof(Utf8StringMarshaller))] string name);

    /// <summary>
    /// Get a pointer property from a group of properties.
    /// </summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <param name="default_value">the default value of the property.</param>
    /// <returns>the value of the property, or default_value if it is not set or not a pointer property.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void* SDL_GetPointerProperty(SDL_PropertiesID props, [MarshalUsing(typeof(Utf8StringMarshaller))] string name, void* default_value);

    /// <summary>
    /// Get a string property from a group of properties.
    /// </summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <param name="default_value">the default value of the property.</param>
    /// <returns>the value of the property, or default_value if it is not set or not a string property.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetStringProperty(SDL_PropertiesID props, [MarshalUsing(typeof(Utf8StringMarshaller))] string name, [MarshalUsing(typeof(Utf8StringMarshaller))] string? default_value);

    /// <summary>
    /// Get a number property from a group of properties.
    /// </summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <param name="default_value">the default value of the property.</param>
    /// <returns>the value of the property, or default_value if it is not set or not a number property.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial long SDL_GetNumberProperty(SDL_PropertiesID props, [MarshalUsing(typeof(Utf8StringMarshaller))] string name, long default_value);

    /// <summary>
    /// Get a floating point property from a group of properties.
    /// </summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <param name="default_value">the default value of the property.</param>
    /// <returns>the value of the property, or default_value if it is not set or not a float property.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial float SDL_GetFloatProperty(SDL_PropertiesID props, [MarshalUsing(typeof(Utf8StringMarshaller))] string name, float default_value);

    /// <summary>
    /// Get a boolean property from a group of properties.
    /// </summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <param name="default_value">the default value of the property.</param>
    /// <returns>the value of the property, or default_value if it is not set or not a boolean property.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetBooleanProperty(SDL_PropertiesID props, [MarshalUsing(typeof(Utf8StringMarshaller))] string name, [MarshalAs(UnmanagedType.U1)] bool default_value);

    /// <summary>
    /// Clear a property from a group of properties.
    /// </summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to clear.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ClearProperty(SDL_PropertiesID props, [MarshalUsing(typeof(Utf8StringMarshaller))] string name);

    /// <summary>
    /// Enumerate the properties contained in a group of properties.
    /// </summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="callback">the function to call for each property.</param>
    /// <param name="userdata">an opaque value that is passed to callback.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_EnumerateProperties(SDL_PropertiesID props, delegate* unmanaged[Cdecl]<nuint, SDL_PropertiesID, byte*, void> callback, nuint userdata);

    /// <summary>
    /// Destroy a group of properties.
    /// </summary>
    /// <param name="props">the properties to destroy.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_DestroyProperties(SDL_PropertiesID props);
}