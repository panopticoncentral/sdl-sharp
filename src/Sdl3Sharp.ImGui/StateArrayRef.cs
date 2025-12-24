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
public readonly unsafe struct StateArrayRef<T> where T : unmanaged
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StateArrayRef{T}"/> struct.
    /// </summary>
    /// <param name="ptr">A pointer to the stored value.</param>
    internal StateArrayRef(T* ptr, int length)
    {
        Ptr = ptr;
        Length = length;
    }

    /// <summary>
    /// Gets or sets the stored value.
    /// </summary>
    public Span<T> Value
    {
        get => new(Ptr, Length);
        set
        {
            if (value.Length != Length)
            {
                throw new ArgumentException("Invalid span length.", nameof(value));
            }

            for (var i = 0; i < Length; i++)
            {
                Ptr[i] = value[i];
            }
        }
    }

    internal T* Ptr { get; }
    internal int Length { get; }

    public static implicit operator Span<T>(StateArrayRef<T> stateRef)
    {
        return stateRef.Value;
    }
}