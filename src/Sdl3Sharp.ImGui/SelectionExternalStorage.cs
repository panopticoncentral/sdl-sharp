using System.Runtime.InteropServices;
using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Helper for applying multi-selection requests to external storage (like an array of bools for checkboxes).
/// </summary>
/// <remarks>
/// <para>
/// This is useful when you want multi-selection behavior applied to external data
/// (e.g., an array of checkboxes) rather than using ImGuiSelectionBasicStorage.
/// </para>
/// <para>
/// Usage with checkboxes:
/// <code>
/// bool[] items = new bool[20];
/// var storage = new SelectionExternalStorage(items, (idx, selected) => items[idx] = selected);
/// var msIo = Widgets.BeginMultiSelect(flags, -1, items.Length);
/// storage.ApplyRequests(msIo);
/// for (int n = 0; n &lt; items.Length; n++)
/// {
///     Widgets.SetNextItemSelectionUserData(n);
///     Widgets.Checkbox($"Item {n}".ToUtf8(), ref items[n]);
/// }
/// msIo = Widgets.EndMultiSelect();
/// storage.ApplyRequests(msIo);
/// </code>
/// </para>
/// </remarks>
public unsafe sealed class SelectionExternalStorage : IDisposable
{
    private ImGuiSelectionExternalStorage* _native;
    private readonly Action<int, bool> _setItemSelected;
    private GCHandle _delegateHandle;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectionExternalStorage"/> class.
    /// </summary>
    /// <param name="userData">Optional user data pointer (can be used to pass context to the callback).</param>
    /// <param name="setItemSelected">Callback to set the selected state of an item at the given index.</param>
    public SelectionExternalStorage(nint userData, Action<int, bool> setItemSelected)
    {
        _setItemSelected = setItemSelected ?? throw new ArgumentNullException(nameof(setItemSelected));
        _native = (ImGuiSelectionExternalStorage*)NativeMemory.AllocZeroed((nuint)sizeof(ImGuiSelectionExternalStorage));
        _native->UserData = (void*)userData;

        // We need to use a static method with UnmanagedCallersOnly, and pass the delegate through UserData
        // But since we need the Action, we'll use a different approach - store the delegate handle
        _delegateHandle = GCHandle.Alloc(this);
        _native->UserData = (void*)GCHandle.ToIntPtr(_delegateHandle);
        _native->AdapterSetItemSelected = &StaticSetItemSelected;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectionExternalStorage"/> class for use with a boolean array.
    /// </summary>
    /// <param name="setItemSelected">Callback to set the selected state of an item at the given index.</param>
    public SelectionExternalStorage(Action<int, bool> setItemSelected)
        : this(0, setItemSelected)
    {
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static void StaticSetItemSelected(ImGuiSelectionExternalStorage* self, int idx, byte selected)
    {
        var handle = GCHandle.FromIntPtr((nint)self->UserData);
        var storage = (SelectionExternalStorage)handle.Target!;
        storage._setItemSelected(idx, selected != 0);
    }

    /// <summary>
    /// Applies selection requests from BeginMultiSelect() and EndMultiSelect().
    /// </summary>
    /// <param name="msIo">The MultiSelectIO containing selection requests.</param>
    public void ApplyRequests(MultiSelectIO msIo)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        ImGuiSelectionExternalStorage.ApplyRequests(_native, msIo.Native);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (!_disposed)
        {
            if (_delegateHandle.IsAllocated)
            {
                _delegateHandle.Free();
            }

            if (_native != null)
            {
                NativeMemory.Free(_native);
                _native = null;
            }

            _disposed = true;
        }
    }
}
