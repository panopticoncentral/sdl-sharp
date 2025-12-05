namespace Sdl3Sharp.ImGui;

/// <summary>
/// A reference to a value stored in a <see cref="StateStore"/>.
/// </summary>
/// <remarks>
/// This type provides type-safe access to native memory that remains stable
/// across frames, suitable for use with ImGui APIs that require persistent
/// pointers to state storage.
/// </remarks>
/// <typeparam name="T">The type of value stored.</typeparam>
public readonly unsafe struct StateRef<T> where T : unmanaged
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StateRef{T}"/> struct.
    /// </summary>
    /// <param name="ptr">A pointer to the stored value.</param>
    internal StateRef(T* ptr)
    {
        Ptr = ptr;
    }

    /// <summary>
    /// Gets or sets the stored value.
    /// </summary>
    public T Value
    {
        get => *Ptr;
        set => *Ptr = value;
    }

    /// <summary>
    /// Gets the pointer to the stored value.
    /// </summary>
    /// <remarks>
    /// This pointer remains valid as long as the owning <see cref="StateStore"/>
    /// has not been disposed.
    /// </remarks>
    internal T* Ptr { get; }
}