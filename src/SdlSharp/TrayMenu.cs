using static SdlSharp.Native.Common;
using static SdlSharp.Native.Tray;

namespace SdlSharp;

/// <summary>
/// A menu attached to a <see cref="Tray"/> or to a submenu entry. This is a view over
/// tray-owned state: it is valid only while the owning tray lives. Wrappers are not
/// equal to each other.
/// </summary>
public sealed unsafe class TrayMenu
{
    private readonly Native.SDL_TrayMenu* _menu;
    private readonly Tray _tray;

    internal TrayMenu(Native.SDL_TrayMenu* menu, Tray tray)
    {
        _menu = menu;
        _tray = tray;
    }

    private Native.SDL_TrayMenu* Menu
    {
        get
        {
            ObjectDisposedException.ThrowIf(_tray.IsDisposed, _tray);
            return _menu;
        }
    }

    /// <summary>
    /// Inserts an entry into the menu.
    /// </summary>
    /// <param name="index">Position to insert at, or -1 to append.</param>
    /// <param name="label">The entry label, or null for a separator.</param>
    /// <param name="flags">Entry kind and initial state (ignored for separators).</param>
    /// <returns>The new entry, or null when a separator was inserted and SDL returned none.</returns>
    public TrayEntry? InsertEntry(int index, string? label, TrayEntryFlags flags = TrayEntryFlags.Button)
    {
        var entry = SDL_InsertTrayEntryAt(Menu, index, ToUtf8(label), (uint)flags);
        if (entry == null && label != null)
        {
            throw new SdlException();
        }

        return entry == null ? null : new TrayEntry(entry, _tray);
    }

    /// <summary>Gets a snapshot of the menu's entries.</summary>
    public TrayEntry[] Entries
    {
        get
        {
            var entries = SDL_GetTrayEntries(Menu, out var count);
            if (entries == null || count <= 0) return [];

            var result = new TrayEntry[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = new TrayEntry(entries[i], _tray);
            }

            return result;
        }
    }

    /// <summary>Gets the entry this menu is a submenu of, or null for a tray root menu.</summary>
    public TrayEntry? ParentEntry
    {
        get
        {
            var entry = SDL_GetTrayMenuParentEntry(Menu);
            return entry == null ? null : new TrayEntry(entry, _tray);
        }
    }

    /// <summary>Gets the tray this menu ultimately belongs to.</summary>
    public Tray ParentTray => _tray;
}
