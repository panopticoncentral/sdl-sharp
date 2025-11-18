using System.Runtime.InteropServices;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Properties;

namespace Sdl3Sharp;

/// <summary>
/// Represents a group of SDL properties.
/// </summary>
public sealed unsafe class PropertyGroup : IDisposable
{
    private SDL_PropertiesID _id;
    private bool _disposed;
    private readonly bool _ownsHandle;

    /// <summary>
    /// Gets the global SDL properties.
    /// </summary>
    public static PropertyGroup Global => new(SDL_GetGlobalProperties(), ownsHandle: false);

    /// <summary>
    /// Gets the underlying SDL_PropertiesID.
    /// </summary>
    public SDL_PropertiesID Id => _id;

    /// <summary>
    /// Gets whether this property group is valid (non-zero ID).
    /// </summary>
    public bool IsValid => _id.Value != 0;

    /// <summary>
    /// Gets a collection of all property names in this group.
    /// </summary>
    public IReadOnlyCollection<string> Keys
    {
        get
        {
            ThrowIfDisposed();

            var names = new List<string>();
            var handle = GCHandle.Alloc(names);
            try
            {
                // Fix: wrap the pointer usage in an unsafe block
                unsafe
                {
                    _ = CheckErrorBool(SDL_EnumerateProperties(_id, &EnumerateCallback, (nuint)GCHandle.ToIntPtr(handle)));
                }
            }
            finally
            {
                handle.Free();
            }

            return names;

            [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            static unsafe void EnumerateCallback(nuint userdata, SDL_PropertiesID props, byte* name)
            {
                var handle = GCHandle.FromIntPtr((nint)userdata);
                var names = (List<string>)handle.Target!;
                var nameStr = Marshal.PtrToStringUTF8((nint)name);
                if (nameStr is not null)
                {
                    names.Add(nameStr);
                }
            }
        }
    }

    /// <summary>
    /// Creates a new property group.
    /// </summary>
    public PropertyGroup()
    {
        _id = CheckErrorZero(SDL_CreateProperties());
        _ownsHandle = true;
    }

    /// <summary>
    /// Wraps an existing SDL_PropertiesID.
    /// </summary>
    /// <param name="id">The SDL_PropertiesID to wrap.</param>
    /// <param name="ownsHandle">Whether this instance should destroy the properties when disposed.</param>
    internal PropertyGroup(SDL_PropertiesID id, bool ownsHandle = true)
    {
        _id = id;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Copies all properties from this group to another group.
    /// </summary>
    /// <param name="destination">The destination property group.</param>
    public void CopyTo(PropertyGroup destination)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_CopyProperties(_id, destination._id));
    }

    /// <summary>
    /// Locks the property group for thread-safe access.
    /// </summary>
    public void Lock()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_LockProperties(_id));
    }

    /// <summary>
    /// Unlocks the property group.
    /// </summary>
    public void Unlock()
    {
        ThrowIfDisposed();
        SDL_UnlockProperties(_id);
    }

    // Pointer property methods are not exposed in the managed wrapper because:
    // - Storing managed objects as pointers requires pinning, which fragments the GC heap
    // - void* loses all type information, making it unsafe and error-prone
    // - Lifetime management is problematic - no guarantee the pointer remains valid
    // - The low-level SDL_SetPointerProperty/SDL_GetPointerProperty APIs remain available
    //   in Sdl3Sharp.Native.Properties for advanced scenarios requiring direct interop

    /// <summary>
    /// Sets a string property.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="value">The value to set, or null to delete the property.</param>
    public void SetString(string name, string? value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetStringProperty(_id, name, value));
    }

    /// <summary>
    /// Sets a number property.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="value">The value to set.</param>
    public void SetNumber(string name, long value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetNumberProperty(_id, name, value));
    }

    /// <summary>
    /// Sets a float property.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="value">The value to set.</param>
    public void SetFloat(string name, float value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetFloatProperty(_id, name, value));
    }

    /// <summary>
    /// Sets a boolean property.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="value">The value to set.</param>
    public void SetBoolean(string name, bool value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetBooleanProperty(_id, name, value));
    }

    /// <summary>
    /// Checks whether a property exists.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <returns>True if the property exists, false otherwise.</returns>
    public bool HasProperty(string name)
    {
        ThrowIfDisposed();
        return SDL_HasProperty(_id, name);
    }

    /// <summary>
    /// Gets the type of a property.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <returns>The type of the property, or Invalid if not set.</returns>
    public PropertyType GetPropertyType(string name)
    {
        ThrowIfDisposed();
        return (PropertyType)SDL_GetPropertyType(_id, name);
    }

    /// <summary>
    /// Gets a string property.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="defaultValue">The default value if the property is not set.</param>
    /// <returns>The value of the property, or defaultValue if not set or not a string property.</returns>
    public string? GetString(string name, string? defaultValue = null)
    {
        ThrowIfDisposed();
        return SDL_GetStringProperty(_id, name, defaultValue);
    }

    /// <summary>
    /// Gets a number property.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="defaultValue">The default value if the property is not set.</param>
    /// <returns>The value of the property, or defaultValue if not set or not a number property.</returns>
    public long GetNumber(string name, long defaultValue = 0)
    {
        ThrowIfDisposed();
        return SDL_GetNumberProperty(_id, name, defaultValue);
    }

    /// <summary>
    /// Gets a float property.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="defaultValue">The default value if the property is not set.</param>
    /// <returns>The value of the property, or defaultValue if not set or not a float property.</returns>
    public float GetFloat(string name, float defaultValue = 0.0f)
    {
        ThrowIfDisposed();
        return SDL_GetFloatProperty(_id, name, defaultValue);
    }

    /// <summary>
    /// Gets a boolean property.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="defaultValue">The default value if the property is not set.</param>
    /// <returns>The value of the property, or defaultValue if not set or not a boolean property.</returns>
    public bool GetBoolean(string name, bool defaultValue = false)
    {
        ThrowIfDisposed();
        return SDL_GetBooleanProperty(_id, name, defaultValue);
    }

    /// <summary>
    /// Clears a property from the group.
    /// </summary>
    /// <param name="name">The name of the property to clear.</param>
    public void ClearProperty(string name)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_ClearProperty(_id, name));
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_ownsHandle && _id.Value != 0)
        {
            SDL_DestroyProperties(_id);
            _id = 0;
        }

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}