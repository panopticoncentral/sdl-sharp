using Sdl3Sharp.ImGui.Native;
using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Identifier for ImGui.
/// </summary>
public unsafe readonly record struct Id
{
    internal readonly ImGuiID Value { get; }

    internal Id(ImGuiID value)
    {
        Value = value;
    }

    /// <summary>
    /// Pushes a string into the ID stack (will hash string).
    /// </summary>
    /// <param name="strId">The string ID to push.</param>
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
    public static void Push(ReadOnlySpan<byte> strId)
    {
        fixed (byte* ptr = strId)
        {
            ImGui_PushID(ptr);
        }
    }

    /// <summary>
    /// Pushes a pointer into the ID stack (will hash pointer).
    /// </summary>
    /// <param name="ptrId">The pointer value to push as an ID.</param>
    public static void Push(nint ptrId)
    {
        ImGui_PushIDPtr((void *)ptrId);
    }

    /// <summary>
    /// Pushes an integer into the ID stack (will hash integer).
    /// </summary>
    /// <param name="intId">The integer ID to push.</param>
    public static void Push(int intId)
    {
        ImGui_PushIDInt(intId);
    }

    /// <summary>
    /// Pops from the ID stack.
    /// </summary>
    public static void Pop()
    {
        ImGui_PopID();
    }

    /// <summary>
    /// Calculates a unique ID (hash of whole ID stack + given parameter).
    /// </summary>
    /// <param name="strId">The string to include in the hash.</param>
    /// <returns>The calculated unique ID.</returns>
    /// <remarks>
    /// Use this if you want to query into ImGuiStorage yourself.
    /// </remarks>
    public static Id Get(ReadOnlySpan<byte> strId)
    {
        fixed (byte* ptr = strId)
        {
            return new Id(ImGui_GetID(ptr));
        }
    }

    /// <summary>
    /// Calculates a unique ID from a pointer (hash of whole ID stack + given parameter).
    /// </summary>
    /// <param name="ptrId">The pointer value to include in the hash.</param>
    /// <returns>The calculated unique ID.</returns>
    public static Id Get(nint ptrId)
    {
        return new Id(ImGui_GetIDPtr((void*)ptrId));
    }

    /// <summary>
    /// Calculates a unique ID from an integer (hash of whole ID stack + given parameter).
    /// </summary>
    /// <param name="intId">The integer to include in the hash.</param>
    /// <returns>The calculated unique ID.</returns>
    public static Id Get(int intId)
    {
        return new Id(ImGui_GetIDInt(intId));
    }
}
