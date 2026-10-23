using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Tray;

namespace SdlSharp;

/// <summary>
/// An entry in a tray menu. This is a view over tray-owned state: it is valid only
/// while the owning tray lives and the entry has not been removed. Wrappers are not
/// equal to each other.
/// </summary>
public sealed unsafe class TrayEntry
{
    internal sealed record CallbackHolder(Tray Tray, Action<TrayEntry> Callback);

    private readonly Native.SDL_TrayEntry* _entry;
    private readonly Tray _tray;
    internal readonly NativeLifetime Lifetime;

    internal TrayEntry(Native.SDL_TrayEntry* entry, Tray tray, NativeLifetime? parent = null)
    {
        _entry = entry;
        _tray = tray;
        Lifetime = NativeLifetime.Get(typeof(TrayEntry), (nint)entry, false, threadAffine: true, parent: parent ?? tray.Lifetime);
    }

    private Native.SDL_TrayEntry* Entry
    {
        get
        {
            Lifetime.Check();
            return _entry;
        }
    }

    /// <summary>Gets or sets the entry label. Null denotes a separator.</summary>
    public string? Label
    {
        get => Marshal.PtrToStringUTF8((nint)SDL_GetTrayEntryLabel(Entry));
        set => SDL_SetTrayEntryLabel(Entry, ToUtf8(value));
    }

    /// <summary>Gets or sets whether a checkbox entry is checked.</summary>
    public bool IsChecked
    {
        get => SDL_GetTrayEntryChecked(Entry);
        set => SDL_SetTrayEntryChecked(Entry, value);
    }

    /// <summary>Gets or sets whether the entry can be selected.</summary>
    public bool IsEnabled
    {
        get => SDL_GetTrayEntryEnabled(Entry);
        set => SDL_SetTrayEntryEnabled(Entry, value);
    }

    /// <summary>Creates a submenu for this entry (the entry must have been created with <see cref="TrayEntryFlags.Submenu"/>).</summary>
    public TrayMenu CreateSubmenu() => new(Check(SDL_CreateTraySubmenu(Entry)), _tray, Lifetime);

    /// <summary>Gets this entry's submenu, or null if it has none.</summary>
    public TrayMenu? Submenu
    {
        get
        {
            var menu = SDL_GetTraySubmenu(Entry);
            return menu == null ? null : new TrayMenu(menu, _tray, Lifetime);
        }
    }

    /// <summary>Gets the menu containing this entry.</summary>
    public TrayMenu Parent => new(Check(SDL_GetTrayEntryParent(Entry)), _tray);

    /// <summary>Simulates a click on this entry (fires its callback).</summary>
    public void Click() => SDL_ClickTrayEntry(Entry);

    /// <summary>
    /// Sets or clears (<c>null</c>) the callback invoked when this entry is selected.
    /// Setting replaces any previous callback.
    /// </summary>
    /// <param name="callback">The callback, or null to clear.</param>
    public void SetCallback(Action<TrayEntry>? callback)
    {
        CallbackErrors.EnsureOutsideCallback();
        var key = (nint)Entry;

        lock (_tray.CallbackHandles)
        {
            _tray.CallbackHandles.Remove(key, out var previous);

            if (callback == null)
            {
                SDL_SetTrayEntryCallback(Entry, null, null);
            }
            else
            {
                var handle = CallbackHandle.Alloc(new CallbackHolder(_tray, callback));
                SDL_SetTrayEntryCallback(Entry, &Tray.EntryCallback, (void*)CallbackHandle.ToIntPtr(handle));
                _tray.CallbackHandles[key] = handle;
                Lifetime.AddCleanup(() => { handle.Free(); _tray.CallbackHandles.Remove(key); });
            }

            if (previous.IsAllocated)
            {
                previous.Free();
            }
        }
    }

    /// <summary>
    /// Removes this entry (and any submenu under it) from its menu. The wrapper is
    /// invalid afterward. Callback handles of a removed submenu's descendants are
    /// released when the tray is disposed.
    /// </summary>
    public void Remove()
    {
        Lifetime.EnsureCanDestroy();
        SDL_RemoveTrayEntry(Entry);
        Lifetime.Invalidate();
    }
}
