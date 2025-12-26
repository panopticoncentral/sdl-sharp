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
public unsafe sealed class StateStore : IDisposable
{
    private readonly List<nint> _storage = [];
    private bool _disposed;

    public static StateStore Instance = new();

    private StateStore()
    {
    }

    /// <summary>
    /// Creates a reference to a stored value.
    /// </summary>
    /// <typeparam name="T">The type of value to store. Must be an unmanaged type.</typeparam>
    /// <param name="defaultValue">The initial value to use.</param>
    /// <returns>A reference to the stored value.</returns>
    public StateRef<T> Create<T>(T defaultValue = default) where T : unmanaged
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        var ptr = (nint)NativeMemory.Alloc((nuint)sizeof(T));
        *(T*)ptr = defaultValue;
        _storage.Add(ptr);

        return new StateRef<T>((T*)ptr);
    }

    /// <summary>
    /// Creates a reference to a stored value.
    /// </summary>
    /// <typeparam name="T">The type of value to store. Must be an unmanaged type.</typeparam>
    /// <param name="defaultValue">The initial value to use.</param>
    /// <returns>A reference to the stored value.</returns>
    public StateArrayRef<T> CreateArray<T>(int length) where T : unmanaged
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        var ptr = (nint)NativeMemory.Alloc((nuint)sizeof(T) * (uint)length);
        for (var i = 0; i < length; i++)
        {
            *(T*)(ptr + sizeof(T) * i) = default;
        }
        _storage.Add(ptr);

        return new StateArrayRef<T>((T*)ptr, length);
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
            foreach (var ptr in _storage)
            {
                NativeMemory.Free((void*)ptr);
            }

            _storage.Clear();
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
