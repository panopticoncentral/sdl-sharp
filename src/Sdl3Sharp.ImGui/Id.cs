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
        return new Id(ImGui_GetIDPtr(ptrId));
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
