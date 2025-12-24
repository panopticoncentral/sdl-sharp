using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// A scope that manages an ID stack entry. Dispose to pop the ID.
/// </summary>
/// <remarks>
/// Use this with a using statement or declaration to automatically pop the ID from the stack.
/// </remarks>
public unsafe readonly ref struct IdScope : IDisposable
{
    /// <summary>
    /// Pushes a string into the ID stack (will hash string).
    /// </summary>
    /// <param name="strId">The string ID to push.</param>
    /// <returns>An <see cref="IdScope"/> that automatically pops the ID when disposed.</returns>
    /// <remarks>
    /// <para>
    /// Read the FAQ for more details about how IDs are handled in Dear ImGui.
    /// </para>
    /// <para>
    /// IDs are hashes of the entire ID stack. If you are creating widgets in a loop,
    /// you most likely want to push a unique identifier (e.g., object pointer, loop index)
    /// to uniquely differentiate them.
    /// </para>
    /// <para>
    /// You can also use the "Label##foobar" syntax within widget labels to distinguish them from each other.
    /// </para>
    /// </remarks>
    public static IdScope Push(ReadOnlySpan<byte> strId)
    {
        fixed (byte* ptr = strId)
        {
            ImGui_PushID(ptr);
        }

        return new IdScope();
    }

    /// <summary>
    /// Pushes a pointer into the ID stack (will hash pointer).
    /// </summary>
    /// <param name="ptrId">The pointer value to push as an ID.</param>
    /// <returns>An <see cref="IdScope"/> that automatically pops the ID when disposed.</returns>
    public static IdScope Push(nint ptrId)
    {
        ImGui_PushIDPtr((void*)ptrId);
        return new IdScope();
    }

    /// <summary>
    /// Pushes an integer into the ID stack (will hash integer).
    /// </summary>
    /// <param name="intId">The integer ID to push.</param>
    /// <returns>An <see cref="IdScope"/> that automatically pops the ID when disposed.</returns>
    public static IdScope Push(int intId)
    {
        ImGui_PushIDInt(intId);
        return new IdScope();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        ImGui_PopID();
    }
}
