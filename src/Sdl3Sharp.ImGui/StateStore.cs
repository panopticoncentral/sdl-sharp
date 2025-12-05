using System.Runtime.InteropServices;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Provides stable native memory storage for values that need to persist across frames.
/// </summary>
/// <remarks>
/// <para>
/// ImGui APIs often require pointers to state storage that remain valid and stable
/// across multiple frames (e.g., the <c>p_open</c> parameter for window visibility).
/// This class allocates native memory that is not subject to garbage collection
/// movement, ensuring pointers remain valid.
/// </para>
/// <para>
/// Values are stored by string key and lazily allocated on first access.
/// The same key always returns a reference to the same memory location.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// using var state = new StateStore();
///
/// // In your render loop:
/// ImGui.ShowDemoWindow(state.Get("demo_open", true));
/// </code>
/// </example>
public unsafe sealed class StateStore : IDisposable
{
    private Dictionary<string, nint>? _storage = [];
    private bool _disposed;

    /// <summary>
    /// Gets a reference to a stored value, creating it with a default value if it doesn't exist.
    /// </summary>
    /// <typeparam name="T">The type of value to store. Must be an unmanaged type.</typeparam>
    /// <param name="id">The unique identifier for this value.</param>
    /// <param name="defaultValue">The initial value to use if the key doesn't exist.</param>
    /// <returns>A reference to the stored value.</returns>
    /// <exception cref="ObjectDisposedException">The <see cref="StateStore"/> has been disposed.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="id"/> is null.</exception>
    public StateRef<T> Get<T>(string id, T defaultValue = default) where T : unmanaged
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        ArgumentNullException.ThrowIfNull(id);

        if (!_storage!.TryGetValue(id, out var ptr))
        {
            ptr = (nint)NativeMemory.Alloc((nuint)sizeof(T));
            *(T*)ptr = defaultValue;
            _storage[id] = ptr;
        }

        return new StateRef<T>((T*)ptr);
    }

    /// <summary>
    /// Removes a value from the store and frees its native memory.
    /// </summary>
    /// <param name="id">The unique identifier for the value to remove.</param>
    /// <returns><c>true</c> if the value was found and removed; otherwise, <c>false</c>.</returns>
    /// <exception cref="ObjectDisposedException">The <see cref="StateStore"/> has been disposed.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="id"/> is null.</exception>
    public bool Remove(string id)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        ArgumentNullException.ThrowIfNull(id);

        if (_storage!.Remove(id, out var ptr))
        {
            NativeMemory.Free((void*)ptr);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Removes all values from the store and frees their native memory.
    /// </summary>
    /// <exception cref="ObjectDisposedException">The <see cref="StateStore"/> has been disposed.</exception>
    public void Clear()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        foreach (var ptr in _storage!.Values)
        {
            NativeMemory.Free((void*)ptr);
        }

        _storage.Clear();
    }

    /// <summary>
    /// Releases all native memory allocated by this store.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_storage is not null)
        {
            foreach (var ptr in _storage.Values)
            {
                NativeMemory.Free((void*)ptr);
            }

            _storage = null;
        }

        _disposed = true;
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Finalizer to ensure native memory is freed if Dispose is not called.
    /// </summary>
    ~StateStore()
    {
        Dispose();
    }
}
