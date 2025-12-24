using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// A scope that manages a tree widget.
/// </summary>
public unsafe readonly ref struct TreeScope : IDisposable
{
    /// <summary>
    /// Pushes a tree node onto the stack (equivalent to Indent + PushID).
    /// </summary>
    /// <param name="strId">The string ID to push.</param>
    /// <remarks>
    /// Already called by TreeNode() when returning true, but you can call TreePush/TreePop yourself if desired.
    /// </remarks>
    public static void TreePush(ReadOnlySpan<byte> strId)
    {
        fixed (byte* ptr = strId)
        {
            ImGui_TreePush(ptr);
        }
    }

    /// <summary>
    /// Pushes a tree node onto the stack using a pointer ID.
    /// </summary>
    /// <param name="ptrId">The pointer ID to push.</param>
    public static void TreePush(nint ptrId)
    {
        ImGui_TreePushPtr((void*)ptrId);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        ImGui_TreePop();
    }
}
