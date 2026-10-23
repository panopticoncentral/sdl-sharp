using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Properties;

namespace SdlSharp;

/// <summary>
/// A managed wrapper around an SDL property group (SDL_PropertiesID).
/// Names beginning with "SdlSharp." are reserved for ownership metadata and cannot be mutated through this API.
/// </summary>
public sealed unsafe class PropertyGroup : IDisposable
{
    private readonly bool _ownsHandle;
    internal readonly NativeLifetime Lifetime;
    private readonly NativeLifetime? _viewParent;

    /// <summary>
    /// The underlying SDL properties ID.
    /// </summary>
    internal SDL_PropertiesID Id
    {
        get
        {
            ObjectDisposedException.ThrowIf(_id.Value == 0, this);
            Lifetime.Check();
            _viewParent?.Check();
            return _id;
        }
    }

    private SDL_PropertiesID _id;

    internal PropertyGroup(SDL_PropertiesID id, bool ownsHandle = true, NativeLifetime? parent = null)
    {
        _id = id;
        Lifetime = NativeLifetime.Get(typeof(PropertyGroup), (nint)id.Value, ownsHandle, threadAffine: false, parent: parent);
        _ownsHandle = ownsHandle;
        _viewParent = ownsHandle ? null : parent;
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
    /// Copies public properties to another group. Cleanup registrations and reserved ownership metadata are excluded.
    /// </summary>
    public void CopyTo(PropertyGroup destination)
    {
        ArgumentNullException.ThrowIfNull(destination);
        var source = Id; var target = destination.Id;
        // Snapshot with SDL's cleanup-property exclusion, then remove internal keys
        // before copying. Even keys installed by a native producer cannot overwrite
        // a destination's ownership metadata through this managed operation.
        using var snapshot = new PropertyGroup();
        Check(SDL_CopyProperties(source, snapshot.Id));
        foreach (var name in snapshot.GetNamesCore(includeInternal: true))
            if (IsInternalName(name)) Check(SDL_ClearProperty(snapshot.Id, ToUtf8(name)));
        Check(SDL_CopyProperties(snapshot.Id, target));
    }
    private static bool IsInternalName(string name) => name.StartsWith("SdlSharp.", StringComparison.Ordinal);
    private static byte[] WritableName(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        if (name.Contains('\0') || IsInternalName(name))
            throw new ArgumentException("Property names cannot contain null characters or use the reserved SdlSharp. prefix.", nameof(name));
        return ToUtf8(name)!;
    }

    /// <summary>
    /// Locks the property group for multi-threaded access.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "The lease is transferred to the returned scope or lifetime cleanup, which releases it exactly once.")]
    public SdlScope Lock()
    {
        var id = Id;
        var lease = Lifetime.Retain();
        try
        {
            Check(SDL_LockProperties(id));
            return new SdlScope(() => { SDL_UnlockProperties(id); lease.Dispose(); });
        }
        catch { lease.Dispose(); throw; }
    }

    /// <summary>
    /// Sets a string property.
    /// </summary>
    public void SetString(string name, string? value) =>
        Check(SDL_SetStringProperty(Id, WritableName(name), ToUtf8(value)));

    /// <summary>
    /// Sets a number (long) property.
    /// </summary>
    public void SetNumber(string name, long value) =>
        Check(SDL_SetNumberProperty(Id, WritableName(name), value));

    /// <summary>
    /// Sets a float property.
    /// </summary>
    public void SetFloat(string name, float value) =>
        Check(SDL_SetFloatProperty(Id, WritableName(name), value));

    /// <summary>
    /// Sets a boolean property.
    /// </summary>
    public void SetBoolean(string name, bool value) =>
        Check(SDL_SetBooleanProperty(Id, WritableName(name), value));

    /// <summary>
    /// Sets a pointer-valued property. The caller owns whatever the pointer references.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="value">The pointer value, or 0 to clear.</param>
    public void SetPointer(string name, nint value) =>
        Check(SDL_SetPointerProperty(Id, WritableName(name), (void*)value));

    /// <summary>
    /// Returns whether a property exists.
    /// </summary>
    public bool Has(string name) => SDL_HasProperty(Id, ToUtf8(name));

    /// <summary>
    /// Gets the type of a property.
    /// </summary>
    public PropertyType GetPropertyType(string name) => (PropertyType)SDL_GetPropertyType(Id, ToUtf8(name));

    /// <summary>
    /// Gets a pointer-valued property.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="defaultValue">Value returned when the property is unset.</param>
    public nint GetPointer(string name, nint defaultValue = 0) =>
        (nint)SDL_GetPointerProperty(Id, ToUtf8(name), (void*)defaultValue);

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
    public void Clear(string name) => Check(SDL_ClearProperty(Id, WritableName(name)));

    /// <summary>
    /// Enumerates public property names, excluding reserved ownership metadata.
    /// </summary>
    public IReadOnlyList<string> GetNames() => GetNamesCore(includeInternal: false);
    private IReadOnlyList<string> GetNamesCore(bool includeInternal)
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

        return includeInternal ? names : names.Where(name => !IsInternalName(name)).ToArray();

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        static void Callback(void* userdata, SDL_PropertiesID props, byte* name)
        {
            try
            {
            var list = (List<string>)GCHandle.FromIntPtr((nint)userdata).Target!;
            var str = Marshal.PtrToStringUTF8((nint)name);
            if (str != null)
            {
                list.Add(str);
            }
            }
            catch (Exception error) { CallbackErrors.Capture(error); }

        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && _id.Value != 0)
        {
            Lifetime.Release(() =>
            {
                SDL_DestroyProperties(_id);
            });
        }
        _id = default;
    }
    /// <summary>Transfers a pointer with a cleanup action to SDL. Cleanup runs once when the value is replaced, cleared, or destroyed, including native setup failure. Rejected arguments do not transfer ownership.</summary>
    public void SetPointer(string name, nint value, Action<nint> cleanup)
        => SetPointerCore(WritableName(name), value, cleanup);
    internal void SetOwnershipPointer(string name, nint value, Action<nint> cleanup)
        => SetPointerCore(ToUtf8(name)!, value, cleanup);
    private void SetPointerCore(byte[] name, nint value, Action<nint> cleanup)
    {
        ArgumentNullException.ThrowIfNull(cleanup);
        var id = Id;
        var registration = CallbackHandle.Alloc(cleanup);
        if (!SDL_SetPointerPropertyWithCleanup(id, name, (void*)value, &CleanupPointer, (void*)(nint)registration))
        {
            // SDL invokes cleanup even when setting the property fails.
            registration.Free();
            throw new SdlException();
        }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void CleanupPointer(void* data, void* value)
    {
        var registration = CallbackHandle.FromIntPtr((nint)data);
        try
        {
            using var scope = CallbackErrors.Enter();
            if (registration.Target is Action<nint> cleanup) cleanup((nint)value);
        }
        catch (Exception error) { CallbackErrors.Capture(error); }
        finally { registration.Free(); }
    }
}
