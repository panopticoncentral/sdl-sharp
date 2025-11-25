using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static Sdl3Sharp.Native.Tray;

namespace Sdl3Sharp;

/// <summary>
/// Represents an entry on a system tray menu.
/// </summary>
/// <remarks>
/// <para>An entry does not need to be destroyed; it will be destroyed with the tray.</para>
/// <para>If the entry was created with a null label, it is a separator. Many operations
/// won't work for separator entries.</para>
/// </remarks>
public sealed unsafe class TrayEntry
{
    private static readonly ConcurrentDictionary<nint, Action<TrayEntry>> s_callbacks = new();

    /// <summary>
    /// Gets the underlying SDL_TrayEntry pointer.
    /// </summary>
    public SDL_TrayEntry* Handle { get; }

    /// <summary>
    /// Gets or sets the label of the entry.
    /// </summary>
    /// <remarks>
    /// <para>An entry cannot change between a separator and an ordinary entry; that is,
    /// it is not possible to set a non-null label on an entry that has a null
    /// label (separators), or to set a null label to an entry that has a non-null
    /// label. The operation will silently fail if that happens.</para>
    /// </remarks>
    public string? Label
    {
        get => SDL_GetTrayEntryLabel(Handle);
        set => SDL_SetTrayEntryLabel(Handle, value);
    }

    /// <summary>
    /// Gets or sets whether the entry is checked.
    /// </summary>
    /// <remarks>
    /// The entry must have been created with the <see cref="TrayEntryFlags.Checkbox"/> flag.
    /// </remarks>
    public bool IsChecked
    {
        get => SDL_GetTrayEntryChecked(Handle);
        set => SDL_SetTrayEntryChecked(Handle, value);
    }

    /// <summary>
    /// Gets or sets whether the entry is enabled.
    /// </summary>
    public bool IsEnabled
    {
        get => SDL_GetTrayEntryEnabled(Handle);
        set => SDL_SetTrayEntryEnabled(Handle, value);
    }

    /// <summary>
    /// Gets whether this entry is a separator.
    /// </summary>
    public bool IsSeparator => Label is null;

    /// <summary>
    /// Gets the parent menu containing this entry.
    /// </summary>
    public TrayMenu Parent => new(SDL_GetTrayEntryParent(Handle));

    /// <summary>
    /// Initializes a new instance wrapping an existing SDL_TrayEntry pointer.
    /// </summary>
    /// <param name="handle">The SDL_TrayEntry pointer.</param>
    internal TrayEntry(SDL_TrayEntry* handle)
    {
        Handle = handle;
    }

    /// <summary>
    /// Creates a submenu for this entry.
    /// </summary>
    /// <remarks>
    /// <para>This should be called at most once per tray entry.</para>
    /// <para>The entry must have been created with the <see cref="TrayEntryFlags.Submenu"/> flag.</para>
    /// <para>A menu does not need to be destroyed; it will be destroyed with the tray.</para>
    /// </remarks>
    /// <returns>The newly created submenu.</returns>
    public TrayMenu CreateSubmenu()
    {
        return new TrayMenu(SDL_CreateTraySubmenu(Handle));
    }

    /// <summary>
    /// Gets a previously created submenu for this entry.
    /// </summary>
    /// <remarks>
    /// You should have called <see cref="CreateSubmenu"/> on this entry first.
    /// </remarks>
    /// <returns>The submenu associated with this entry, or null if none exists.</returns>
    public TrayMenu? GetSubmenu()
    {
        SDL_TrayMenu* menu = SDL_GetTraySubmenu(Handle);
        return menu != null ? new TrayMenu(menu) : null;
    }

    /// <summary>
    /// Sets a callback to be invoked when this entry is selected.
    /// </summary>
    /// <param name="callback">The callback to invoke, or null to remove the callback.</param>
    public void SetCallback(Action<TrayEntry>? callback)
    {
        var key = (nint)Handle;

        if (callback is null)
        {
            s_callbacks.TryRemove(key, out _);
            SDL_SetTrayEntryCallback(Handle, null, 0);
        }
        else
        {
            s_callbacks[key] = callback;
            SDL_SetTrayEntryCallback(Handle, &NativeCallback, (nuint)key);
        }
    }

    /// <summary>
    /// Simulates a click on this entry.
    /// </summary>
    public void Click()
    {
        SDL_ClickTrayEntry(Handle);
    }

    /// <summary>
    /// Removes the callback registration for an entry. Called when the tray is destroyed.
    /// </summary>
    /// <param name="handle">The entry handle.</param>
    internal static void RemoveCallback(SDL_TrayEntry* handle)
    {
        s_callbacks.TryRemove((nint)handle, out _);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static void NativeCallback(nuint userdata, SDL_TrayEntry* entry)
    {
        if (s_callbacks.TryGetValue((nint)entry, out var callback))
        {
            callback(new TrayEntry(entry));
        }
    }
}
