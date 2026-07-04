using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// SDL property type.
/// </summary>
public enum SDL_PropertyType
{
    /// <summary>Invalid or nonexistent property.</summary>
    SDL_PROPERTY_TYPE_INVALID,

    /// <summary>Pointer property.</summary>
    SDL_PROPERTY_TYPE_POINTER,

    /// <summary>String property.</summary>
    SDL_PROPERTY_TYPE_STRING,

    /// <summary>Number (Sint64) property.</summary>
    SDL_PROPERTY_TYPE_NUMBER,

    /// <summary>Float property.</summary>
    SDL_PROPERTY_TYPE_FLOAT,

    /// <summary>Boolean property.</summary>
    SDL_PROPERTY_TYPE_BOOLEAN,
}

/// <summary>
/// SDL_PropertiesID — a unique ID for a group of properties.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct SDL_PropertiesID(uint Value);

/// <summary>
/// Native bindings for SDL_properties.h — generic property system.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Properties
{
    /// <summary>
    /// Get the global SDL properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGlobalProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_GetGlobalProperties();

    /// <summary>
    /// Create a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_CreateProperties();

    /// <summary>
    /// Copy a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CopyProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_CopyProperties(SDL_PropertiesID src, SDL_PropertiesID dst);

    /// <summary>
    /// Lock a group of properties for multi-threaded access.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_LockProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_LockProperties(SDL_PropertiesID props);

    /// <summary>
    /// Unlock a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UnlockProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UnlockProperties(SDL_PropertiesID props);

    // Skipped: SDL_SetPointerPropertyWithCleanup — its cleanup callback ties native
    // lifetime management to managed state; use SetPointerProperty and manage
    // lifetime on the managed side instead.

    /// <summary>
    /// Set a pointer property in a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetPointerProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetPointerProperty(SDL_PropertiesID props, ReadOnlySpan<byte> name, void* value);

    /// <summary>
    /// Set a string property in a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetStringProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetStringProperty(SDL_PropertiesID props, ReadOnlySpan<byte> name, ReadOnlySpan<byte> value);

    /// <summary>
    /// Set an integer property in a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetNumberProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetNumberProperty(SDL_PropertiesID props, ReadOnlySpan<byte> name, long value);

    /// <summary>
    /// Set a floating point property in a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetFloatProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetFloatProperty(SDL_PropertiesID props, ReadOnlySpan<byte> name, float value);

    /// <summary>
    /// Set a boolean property in a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetBooleanProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetBooleanProperty(SDL_PropertiesID props, ReadOnlySpan<byte> name, [MarshalAs(UnmanagedType.U1)] bool value);

    /// <summary>
    /// Return whether a property exists in a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasProperty(SDL_PropertiesID props, ReadOnlySpan<byte> name);

    /// <summary>
    /// Get the type of a property in a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetPropertyType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertyType SDL_GetPropertyType(SDL_PropertiesID props, ReadOnlySpan<byte> name);

    /// <summary>
    /// Get a pointer property from a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetPointerProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void* SDL_GetPointerProperty(SDL_PropertiesID props, ReadOnlySpan<byte> name, void* defaultValue);

    /// <summary>
    /// Get a string property from a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetStringProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetStringProperty(SDL_PropertiesID props, ReadOnlySpan<byte> name, ReadOnlySpan<byte> defaultValue);

    /// <summary>
    /// Get a number property from a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetNumberProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long SDL_GetNumberProperty(SDL_PropertiesID props, ReadOnlySpan<byte> name, long defaultValue);

    /// <summary>
    /// Get a floating point property from a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetFloatProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float SDL_GetFloatProperty(SDL_PropertiesID props, ReadOnlySpan<byte> name, float defaultValue);

    /// <summary>
    /// Get a boolean property from a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetBooleanProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetBooleanProperty(SDL_PropertiesID props, ReadOnlySpan<byte> name, [MarshalAs(UnmanagedType.U1)] bool defaultValue);

    /// <summary>
    /// Clear a property from a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ClearProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ClearProperty(SDL_PropertiesID props, ReadOnlySpan<byte> name);

    /// <summary>
    /// Enumerate the properties contained in a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_EnumerateProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_EnumerateProperties(
        SDL_PropertiesID props,
        delegate* unmanaged[Cdecl]<void*, SDL_PropertiesID, byte*, void> callback,
        void* userdata);

    /// <summary>
    /// Destroy a group of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DestroyProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyProperties(SDL_PropertiesID props);

    /// <summary>Generic property for naming things.</summary>
    public static ReadOnlySpan<byte> SDL_PROP_NAME_STRING => "SDL.name"u8;
}
