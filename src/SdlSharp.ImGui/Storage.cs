using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Key/value storage mapping ImGui IDs (<see cref="uint"/>) to int, bool, float, or
/// pointer values. Create your own instance with <see cref="Storage()"/> (dispose when done),
/// or obtain a non-owning view of the current window's state storage via
/// <see cref="ImGui.GetStateStorage"/> — disposing a view is a no-op.
/// </summary>
public sealed unsafe class Storage : IDisposable
{
    private IGSharp_Storage* _handle;

    internal IGSharp_Storage* Handle => _handle;
    private readonly bool _ownsHandle;

    /// <summary>Creates a new, empty storage owned by this wrapper.</summary>
    public Storage()
    {
        _handle = IGSharp_Storage_New();
        _ownsHandle = true;
    }

    /// <summary>Wraps an ImGui-owned storage (e.g. window state storage) without taking ownership.</summary>
    internal Storage(Native.IGSharp_Storage* handle)
    {
        _handle = handle;
        _ownsHandle = false;
    }

    /// <summary>Gets the int value for <paramref name="key"/>, or <paramref name="defaultValue"/> if not present.</summary>
    public int GetInt(uint key, int defaultValue = 0)
    {
        ThrowIfDisposed();
        return IGSharp_Storage_GetInt(_handle, key, defaultValue);
    }

    /// <summary>Sets the int value for <paramref name="key"/>.</summary>
    public void SetInt(uint key, int value)
    {
        ThrowIfDisposed();
        IGSharp_Storage_SetInt(_handle, key, value);
    }

    /// <summary>Gets the bool value for <paramref name="key"/>, or <paramref name="defaultValue"/> if not present.</summary>
    public bool GetBool(uint key, bool defaultValue = false)
    {
        ThrowIfDisposed();
        return IGSharp_Storage_GetBool(_handle, key, defaultValue);
    }

    /// <summary>Sets the bool value for <paramref name="key"/>.</summary>
    public void SetBool(uint key, bool value)
    {
        ThrowIfDisposed();
        IGSharp_Storage_SetBool(_handle, key, value);
    }

    /// <summary>Gets the float value for <paramref name="key"/>, or <paramref name="defaultValue"/> if not present.</summary>
    public float GetFloat(uint key, float defaultValue = 0f)
    {
        ThrowIfDisposed();
        return IGSharp_Storage_GetFloat(_handle, key, defaultValue);
    }

    /// <summary>Sets the float value for <paramref name="key"/>.</summary>
    public void SetFloat(uint key, float value)
    {
        ThrowIfDisposed();
        IGSharp_Storage_SetFloat(_handle, key, value);
    }

    /// <summary>Gets the pointer value for <paramref name="key"/>, or 0 if not present.</summary>
    public nint GetPointer(uint key)
    {
        ThrowIfDisposed();
        return (nint)IGSharp_Storage_GetVoidPtr(_handle, key);
    }

    /// <summary>Sets the pointer value for <paramref name="key"/>.</summary>
    public void SetPointer(uint key, nint value)
    {
        ThrowIfDisposed();
        IGSharp_Storage_SetVoidPtr(_handle, key, (void*)value);
    }

    /// <summary>Sets all int entries to <paramref name="value"/> (e.g. to open/close all tree nodes).</summary>
    public void SetAllInt(int value)
    {
        ThrowIfDisposed();
        IGSharp_Storage_SetAllInt(_handle, value);
    }

    /// <summary>Sorts entries by key so lookups stay O(log N) after bulk insertion.</summary>
    public void BuildSortByKey()
    {
        ThrowIfDisposed();
        IGSharp_Storage_BuildSortByKey(_handle);
    }

    /// <summary>Removes all entries.</summary>
    public void Clear()
    {
        ThrowIfDisposed();
        IGSharp_Storage_Clear(_handle);
    }

    /// <summary>Releases the unmanaged storage if owned; no-op for non-owned views.</summary>
    public void Dispose()
    {
        if (_ownsHandle && _handle != null)
        {
            IGSharp_Storage_Delete(_handle);
            _handle = null;
        }
    }

    private void ThrowIfDisposed()
    {
        if (_handle == null) throw new ObjectDisposedException(nameof(Storage));
    }
}
