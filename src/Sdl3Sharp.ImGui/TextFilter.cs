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
    private ImGuiTextFilter _native;
    private bool _isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextFilter"/> struct with an optional default filter.
    /// </summary>
    public TextFilter()
    {
        _native = default;
        _isDisposed = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TextFilter"/> struct with an optional default filter.
    /// </summary>
    /// <param name="defaultFilter">Optional initial filter string.</param>
    public TextFilter(ReadOnlySpan<byte> defaultFilter)
    {
        _native = default;
        _isDisposed = false;

        fixed (byte* bytes = defaultFilter)
        {
            // If a default filter was provided, copy it to InputBuf and build
            var copyLen = Math.Min(defaultFilter.Length, 256);
            fixed (ImGuiTextFilter* nativePtr = &_native)
            {
                Buffer.MemoryCopy(bytes, nativePtr->InputBuf, 256, copyLen);
            }

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

        fixed (ImGuiTextFilter* nativePtr = &_native)
        {
            if (label.IsEmpty)
            {
                return ImGuiTextFilter.Draw(nativePtr, null, width);
            }

            fixed (byte* labelPtr = label)
            {
                return ImGuiTextFilter.Draw(nativePtr, labelPtr, width);
            }
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

        fixed (ImGuiTextFilter* nativePtr = &_native)
        {
            if (text.IsEmpty)
            {
                return ImGuiTextFilter.PassFilter(nativePtr, null, null);
            }

            fixed (byte* textPtr = text)
            {
                // Find the null terminator or use the end of the span
                var textEnd = textPtr + text.Length;
                if (text[^1] == 0)
                {
                    textEnd--; // Don't include null terminator
                }

                return ImGuiTextFilter.PassFilter(nativePtr, textPtr, textEnd);
            }
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

        fixed (ImGuiTextFilter* nativePtr = &_native)
        {
            if (string.IsNullOrEmpty(text))
            {
                return ImGuiTextFilter.PassFilter(nativePtr, null, null);
            }

            var bytes = text.ToUtf8();
            fixed (byte* textPtr = bytes)
            {
                return ImGuiTextFilter.PassFilter(nativePtr, textPtr, textPtr + bytes.Length);
            }
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

        fixed (ImGuiTextFilter* nativePtr = &_native)
        {
            ImGuiTextFilter.Build(nativePtr);
        }
    }

    /// <summary>
    /// Clears the filter.
    /// </summary>
    public void Clear()
    {
        ThrowIfDisposed();

        fixed (ImGuiTextFilter* nativePtr = &_native)
        {
            ImGuiTextFilter.Clear(nativePtr);
        }
    }

    /// <summary>
    /// Gets a value indicating whether the filter is active (not empty).
    /// </summary>
    public readonly bool IsActive
    {
        get
        {
            ThrowIfDisposed();

            fixed (ImGuiTextFilter* nativePtr = &_native)
            {
                return ImGuiTextFilter.IsActive(nativePtr);
            }
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

            fixed (ImGuiTextFilter* nativePtr = &_native)
            {
                return Encoding.UTF8.GetString(new ReadOnlySpan<byte>(nativePtr->InputBuf, GetInputBufLength()));
            }
        }
        set
        {
            ThrowIfDisposed();

            fixed (ImGuiTextFilter* nativePtr = &_native)
            {
                // Clear the buffer first
                new Span<byte>(nativePtr->InputBuf, 256).Clear();

                if (!string.IsNullOrEmpty(value))
                {
                    var bytes = value.ToUtf8();
                    var copyLen = Math.Min(bytes.Length, 255);
                    fixed (byte* src = bytes)
                    {
                        Buffer.MemoryCopy(src, nativePtr->InputBuf, 256, copyLen);
                    }
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
        if (!_isDisposed)
        {
            fixed (ImGuiTextFilter* nativePtr = &_native)
            {
                // Clear the filters vector if it has data
                if (nativePtr->Filters.Data != null)
                {
                    ImGui_MemFree(nativePtr->Filters.Data);
                    nativePtr->Filters.Data = null;
                }
            }

            _isDisposed = true;
        }
    }

    private readonly int GetInputBufLength()
    {
        fixed (ImGuiTextFilter* nativePtr = &_native)
        {
            for (var i = 0; i < 256; i++)
            {
                if (nativePtr->InputBuf[i] == 0)
                {
                    return i;
                }
            }

            return 256;
        }
    }

    private readonly void ThrowIfDisposed()
    {
        if (_isDisposed)
        {
            throw new ObjectDisposedException(nameof(TextFilter));
        }
    }
}
