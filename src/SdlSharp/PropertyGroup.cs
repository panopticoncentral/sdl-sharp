using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Properties;

namespace SdlSharp;

/// <summary>
/// A managed wrapper around an SDL property group (SDL_PropertiesID).
/// </summary>
public sealed unsafe class PropertyGroup : IDisposable
{
    private readonly bool _ownsHandle;

    /// <summary>
    /// The underlying SDL properties ID.
    /// </summary>
    internal SDL_PropertiesID Id
    {
        get
        {
            ObjectDisposedException.ThrowIf(_id.Value == 0, this);
            return _id;
        }
    }

    private SDL_PropertiesID _id;

    internal PropertyGroup(SDL_PropertiesID id, bool ownsHandle = true)
    {
        _id = id;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Creates a new property group.
    /// </summary>
    public PropertyGroup() : this(CheckId(SDL_CreateProperties()), ownsHandle: true)
    {
    }

    /// <summary>
    /// Gets the global SDL properties.
    /// </summary>
    public static PropertyGroup Global => new(CheckId(SDL_GetGlobalProperties()), ownsHandle: false);

    /// <summary>
    /// Copies all properties from this group to another.
    /// </summary>
    public void CopyTo(PropertyGroup destination) =>
        Check(SDL_CopyProperties(Id, destination.Id));

    /// <summary>
    /// Locks the property group for multi-threaded access.
    /// </summary>
    public void Lock() => Check(SDL_LockProperties(Id));

    /// <summary>
    /// Unlocks the property group.
    /// </summary>
    public void Unlock() => SDL_UnlockProperties(Id);

    /// <summary>
    /// Sets a string property.
    /// </summary>
    public void SetString(string name, string? value) =>
        Check(SDL_SetStringProperty(Id, ToUtf8(name), ToUtf8(value)));

    /// <summary>
    /// Sets a number (long) property.
    /// </summary>
    public void SetNumber(string name, long value) =>
        Check(SDL_SetNumberProperty(Id, ToUtf8(name), value));

    /// <summary>
    /// Sets a float property.
    /// </summary>
    public void SetFloat(string name, float value) =>
        Check(SDL_SetFloatProperty(Id, ToUtf8(name), value));

    /// <summary>
    /// Sets a boolean property.
    /// </summary>
    public void SetBoolean(string name, bool value) =>
        Check(SDL_SetBooleanProperty(Id, ToUtf8(name), value));

    /// <summary>
    /// Returns whether a property exists.
    /// </summary>
    public bool Has(string name) => SDL_HasProperty(Id, ToUtf8(name));

    /// <summary>
    /// Gets the type of a property.
    /// </summary>
    public PropertyType GetPropertyType(string name) => (PropertyType)SDL_GetPropertyType(Id, ToUtf8(name));

    /// <summary>
    /// Gets a string property.
    /// </summary>
    public string? GetString(string name, string? defaultValue = null) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetStringProperty(Id, ToUtf8(name), ToUtf8(defaultValue)));

    /// <summary>
    /// Gets a number (long) property.
    /// </summary>
    public long GetNumber(string name, long defaultValue = 0) =>
        SDL_GetNumberProperty(Id, ToUtf8(name), defaultValue);

    /// <summary>
    /// Gets a float property.
    /// </summary>
    public float GetFloat(string name, float defaultValue = 0f) =>
        SDL_GetFloatProperty(Id, ToUtf8(name), defaultValue);

    /// <summary>
    /// Gets a boolean property.
    /// </summary>
    public bool GetBoolean(string name, bool defaultValue = false) =>
        SDL_GetBooleanProperty(Id, ToUtf8(name), defaultValue);

    /// <summary>
    /// Clears a property.
    /// </summary>
    public void Clear(string name) => Check(SDL_ClearProperty(Id, ToUtf8(name)));

    /// <summary>
    /// Enumerates all property names in this group.
    /// </summary>
    public IReadOnlyList<string> GetNames()
    {
        var names = new List<string>();

        var handle = GCHandle.Alloc(names);
        try
        {
            Check(SDL_EnumerateProperties(Id, &Callback, (void*)(nint)handle));
        }
        finally
        {
            handle.Free();
        }

        return names;

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static void Callback(void* userdata, SDL_PropertiesID props, byte* name)
        {
            var list = (List<string>)GCHandle.FromIntPtr((nint)userdata).Target!;
            var str = Marshal.PtrToStringUTF8((nint)name);
            if (str != null)
            {
                list.Add(str);
            }
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && _id.Value != 0)
        {
            SDL_DestroyProperties(_id);
        }
        _id = default;
    }
}
