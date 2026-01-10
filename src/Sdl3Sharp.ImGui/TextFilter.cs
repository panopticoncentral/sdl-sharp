using System.Text;
using Sdl3Sharp.ImGui.Native;
using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Helper to parse and apply text filters. In format "aaaaa[,bbbb][,ccccc]".
/// </summary>
/// <remarks>
/// <para>
/// This type wraps the native ImGuiTextFilter struct and manages its lifetime.
/// Call <see cref="Dispose"/> when done to free native resources.
/// </para>
/// <para>
/// Filter format: "aaaaa[,bbbb][,ccccc]" where:
/// <list type="bullet">
/// <item><description>Filters are comma-separated</description></item>
/// <item><description>Prefix with "-" to exclude (e.g., "-hide_this")</description></item>
/// </list>
/// </para>
/// </remarks>
public unsafe struct TextFilter : IDisposable
{
    private ImGuiTextFilter* _native;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextFilter"/> struct with an optional default filter.
    /// </summary>
    /// <param name="defaultFilter">Optional initial filter string.</param>
    public TextFilter(string? defaultFilter = null)
    {
        // Allocate memory for the native struct using ImGui's allocator
        _native = (ImGuiTextFilter*)ImGui_MemAlloc((nuint)sizeof(ImGuiTextFilter));

        // Zero-initialize the struct
        new Span<byte>(_native, sizeof(ImGuiTextFilter)).Clear();

        // If a default filter was provided, copy it to InputBuf and build
        if (!string.IsNullOrEmpty(defaultFilter))
        {
            var bytes = Encoding.UTF8.GetBytes(defaultFilter);
            var copyLen = Math.Min(bytes.Length, 255); // Leave room for null terminator
            fixed (byte* src = bytes)
            {
                Buffer.MemoryCopy(src, _native->InputBuf, 256, copyLen);
            }
            _native->InputBuf[copyLen] = 0; // Null terminator

            Build();
        }
    }

    /// <summary>
    /// Draws the filter input field with an optional label.
    /// </summary>
    /// <param name="label">The label for the input field.</param>
    /// <param name="width">The width of the input field (0 for auto).</param>
    /// <returns>True if the filter was modified.</returns>
    /// <remarks>
    /// This is a helper that calls InputText+Build internally.
    /// </remarks>
    public bool Draw(ReadOnlySpan<byte> label = default, float width = 0.0f)
    {
        ThrowIfDisposed();

        if (label.IsEmpty)
        {
            return ImGuiTextFilter.Draw(_native, null, width);
        }

        fixed (byte* labelPtr = label)
        {
            return ImGuiTextFilter.Draw(_native, labelPtr, width);
        }
    }

    /// <summary>
    /// Draws the filter input field with a string label.
    /// </summary>
    /// <param name="label">The label for the input field.</param>
    /// <param name="width">The width of the input field (0 for auto).</param>
    /// <returns>True if the filter was modified.</returns>
    public bool Draw(string label, float width = 0.0f)
    {
        ThrowIfDisposed();

        fixed (byte* labelPtr = Encoding.UTF8.GetBytes(label + '\0'))
        {
            return ImGuiTextFilter.Draw(_native, labelPtr, width);
        }
    }

    /// <summary>
    /// Tests if a given text passes the filter.
    /// </summary>
    /// <param name="text">The text to test against the filter.</param>
    /// <returns>True if the text passes the filter; otherwise, false.</returns>
    public bool PassFilter(ReadOnlySpan<byte> text)
    {
        ThrowIfDisposed();

        if (text.IsEmpty)
        {
            return ImGuiTextFilter.PassFilter(_native, null, null);
        }

        fixed (byte* textPtr = text)
        {
            // Find the null terminator or use the end of the span
            byte* textEnd = textPtr + text.Length;
            if (text[^1] == 0)
            {
                textEnd--; // Don't include null terminator
            }

            return ImGuiTextFilter.PassFilter(_native, textPtr, textEnd);
        }
    }

    /// <summary>
    /// Tests if a given string passes the filter.
    /// </summary>
    /// <param name="text">The text to test against the filter.</param>
    /// <returns>True if the text passes the filter; otherwise, false.</returns>
    public bool PassFilter(string text)
    {
        ThrowIfDisposed();

        if (string.IsNullOrEmpty(text))
        {
            return ImGuiTextFilter.PassFilter(_native, null, null);
        }

        var bytes = Encoding.UTF8.GetBytes(text);
        fixed (byte* textPtr = bytes)
        {
            return ImGuiTextFilter.PassFilter(_native, textPtr, textPtr + bytes.Length);
        }
    }

    /// <summary>
    /// Rebuilds the filter from the input buffer contents.
    /// </summary>
    /// <remarks>
    /// This is automatically called by <see cref="Draw"/> when the input changes.
    /// Call this manually if you modify the input buffer directly.
    /// </remarks>
    public void Build()
    {
        ThrowIfDisposed();
        ImGuiTextFilter.Build(_native);
    }

    /// <summary>
    /// Clears the filter.
    /// </summary>
    public void Clear()
    {
        ThrowIfDisposed();
        ImGuiTextFilter.Clear(_native);
    }

    /// <summary>
    /// Gets a value indicating whether the filter is active (not empty).
    /// </summary>
    public readonly bool IsActive
    {
        get
        {
            ThrowIfDisposed();
            return ImGuiTextFilter.IsActive(_native);
        }
    }

    /// <summary>
    /// Gets or sets the filter text.
    /// </summary>
    public string FilterText
    {
        readonly get
        {
            ThrowIfDisposed();
            return Encoding.UTF8.GetString(new ReadOnlySpan<byte>(_native->InputBuf, GetInputBufLength()));
        }
        set
        {
            ThrowIfDisposed();

            // Clear the buffer first
            new Span<byte>(_native->InputBuf, 256).Clear();

            if (!string.IsNullOrEmpty(value))
            {
                var bytes = Encoding.UTF8.GetBytes(value);
                var copyLen = Math.Min(bytes.Length, 255);
                fixed (byte* src = bytes)
                {
                    Buffer.MemoryCopy(src, _native->InputBuf, 256, copyLen);
                }
            }

            Build();
        }
    }

    /// <summary>
    /// Releases the native resources used by this filter.
    /// </summary>
    public void Dispose()
    {
        if (_native != null)
        {
            // Clear the filters vector if it has data
            if (_native->Filters.Data != null)
            {
                ImGui_MemFree(_native->Filters.Data);
            }

            ImGui_MemFree(_native);
            _native = null;
        }
    }

    private readonly int GetInputBufLength()
    {
        for (var i = 0; i < 256; i++)
        {
            if (_native->InputBuf[i] == 0)
            {
                return i;
            }
        }

        return 256;
    }

    private readonly void ThrowIfDisposed()
    {
        if (_native == null)
        {
            throw new ObjectDisposedException(nameof(TextFilter));
        }
    }
}
