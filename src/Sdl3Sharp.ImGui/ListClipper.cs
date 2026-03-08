using System.Runtime.InteropServices;
using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Helper for manually clipping large lists of items.
/// </summary>
/// <remarks>
/// <para>
/// If you have lots evenly spaced items and you have random access to the list, you can perform coarse
/// clipping based on visibility to only submit items that are in view.
/// </para>
/// <para>
/// The clipper calculates the range of visible items and advances the cursor to compensate for the non-visible items we have skipped.
/// This allows you to easily scale using lists with tens of thousands of items without a problem.
/// </para>
/// <para>
/// Usage:
/// <code>
/// using var clipper = new ListClipper();
/// clipper.Begin(1000);         // We have 1000 elements, evenly spaced.
/// while (clipper.Step())
///     for (int i = clipper.DisplayStart; i &lt; clipper.DisplayEnd; i++)
///         Widgets.Text($"line number {i}".ToUtf8());
/// </code>
/// </para>
/// </remarks>
public unsafe struct ListClipper : IDisposable
{
    private ImGuiListClipper _native;
    private bool _disposed;

    /// <summary>
    /// Gets the first item to display (updated by each call to Step()).
    /// </summary>
    public readonly int DisplayStart => _native.DisplayStart;

    /// <summary>
    /// Gets the end of items to display (exclusive).
    /// </summary>
    public readonly int DisplayEnd => _native.DisplayEnd;

    /// <summary>
    /// Begins the clipper with the specified item count.
    /// </summary>
    /// <param name="itemsCount">The total number of items.</param>
    /// <param name="itemsHeight">The height of each item. Use -1.0f to be calculated automatically on first step.</param>
    public void Begin(int itemsCount, float itemsHeight = -1.0f)
    {
        fixed (ImGuiListClipper* ptr = &_native)
        {
            ImGuiListClipper.Begin(ptr, itemsCount, itemsHeight);
        }
    }

    /// <summary>
    /// Call until it returns false. The DisplayStart/DisplayEnd fields will be set and you can process/draw those items.
    /// </summary>
    /// <returns>True if there are more items to process.</returns>
    public bool Step()
    {
        fixed (ImGuiListClipper* ptr = &_native)
        {
            return ImGuiListClipper.Step(ptr);
        }
    }

    /// <summary>
    /// Call before first Step() to ensure a specific item is not clipped, regardless of visibility.
    /// </summary>
    /// <param name="itemIndex">The index of the item to include.</param>
    public void IncludeItemByIndex(int itemIndex)
    {
        fixed (ImGuiListClipper* ptr = &_native)
        {
            ImGuiListClipper.IncludeItemByIndex(ptr, itemIndex);
        }
    }

    /// <summary>
    /// Call before first Step() to ensure a range of items is not clipped, regardless of visibility.
    /// </summary>
    /// <param name="itemBegin">The first item index (inclusive).</param>
    /// <param name="itemEnd">The last item index (exclusive).</param>
    public void IncludeItemsByIndex(int itemBegin, int itemEnd)
    {
        fixed (ImGuiListClipper* ptr = &_native)
        {
            ImGuiListClipper.IncludeItemsByIndex(ptr, itemBegin, itemEnd);
        }
    }

    /// <summary>
    /// Ends the clipper. Automatically called on the last call of Step() that returns false.
    /// </summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            fixed (ImGuiListClipper* ptr = &_native)
            {
                ImGuiListClipper.End(ptr);
            }

            _disposed = true;
        }
    }
}
