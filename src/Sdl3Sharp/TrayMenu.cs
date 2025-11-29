using static Sdl3Sharp.Native.Tray;

namespace Sdl3Sharp;

/// <summary>
/// Represents a menu or submenu on a system tray object.
/// </summary>
/// <remarks>
/// A menu does not need to be destroyed; it will be destroyed with the tray.
/// </remarks>
public sealed unsafe class TrayMenu
{
    /// <summary>
    /// Gets the underlying SDL_TrayMenu pointer.
    /// </summary>
    public SDL_TrayMenu* Handle { get; }

    /// <summary>
    /// Gets the entries in this menu, in order.
    /// </summary>
    /// <remarks>
    /// The returned array becomes invalid when any function that inserts or deletes
    /// entries in the menu is called.
    /// </remarks>
    public TrayEntry[] Entries
    {
        get
        {
            int count;
            SDL_TrayEntry** entries = SDL_GetTrayEntries(Handle, &count);

            if (entries == null || count == 0)
            {
                return [];
            }

            var result = new TrayEntry[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = new TrayEntry(entries[i]);
            }

            return result;
        }
    }

    /// <summary>
    /// Gets the parent entry if this menu is a submenu.
    /// </summary>
    /// <remarks>
    /// Either this property or <see cref="ParentTray"/> will return non-null
    /// for any given menu.
    /// </remarks>
    public TrayEntry? ParentEntry
    {
        get
        {
            SDL_TrayEntry* entry = SDL_GetTrayMenuParentEntry(Handle);
            return entry != null ? new TrayEntry(entry) : null;
        }
    }

    /// <summary>
    /// Gets the parent tray if this is a first-level menu.
    /// </summary>
    /// <remarks>
    /// Either this property or <see cref="ParentEntry"/> will return non-null
    /// for any given menu.
    /// </remarks>
    public Tray? ParentTray
    {
        get
        {
            SDL_Tray* tray = SDL_GetTrayMenuParentTray(Handle);
            return tray != null ? new Tray(tray) : null;
        }
    }

    /// <summary>
    /// Initializes a new instance wrapping an existing SDL_TrayMenu pointer.
    /// </summary>
    /// <param name="handle">The SDL_TrayMenu pointer.</param>
    internal TrayMenu(SDL_TrayMenu* handle)
    {
        Handle = handle;
    }

    /// <summary>
    /// Inserts a new entry at a given position.
    /// </summary>
    /// <param name="position">The desired position for the new entry. Entries at or following
    /// this place will be moved. If position is -1, the entry is appended.</param>
    /// <param name="label">The text to be displayed on the entry, or null for a separator.</param>
    /// <param name="flags">A combination of flags, some of which are mandatory.</param>
    /// <returns>The newly created entry, or null if position is out of bounds.</returns>
    public TrayEntry? InsertEntryAt(int position, string? label, TrayEntryFlags flags)
    {
        SDL_TrayEntry* entry = SDL_InsertTrayEntryAt(Handle, position, label, (SDL_TrayEntryFlags)flags);
        return entry != null ? new TrayEntry(entry) : null;
    }

    /// <summary>
    /// Appends a new button entry to the menu.
    /// </summary>
    /// <param name="label">The text to be displayed on the entry.</param>
    /// <param name="callback">An optional callback to invoke when the entry is clicked.</param>
    /// <returns>The newly created entry.</returns>
    public TrayEntry AddButton(string label, Action<TrayEntry>? callback = null)
    {
        TrayEntry entry = InsertEntryAt(-1, label, TrayEntryFlags.Button)!;
        if (callback != null)
        {
            entry.SetCallback(callback);
        }

        return entry;
    }

    /// <summary>
    /// Appends a new checkbox entry to the menu.
    /// </summary>
    /// <param name="label">The text to be displayed on the entry.</param>
    /// <param name="isChecked">Whether the checkbox should be initially checked.</param>
    /// <param name="callback">An optional callback to invoke when the entry is clicked.</param>
    /// <returns>The newly created entry.</returns>
    public TrayEntry AddCheckbox(string label, bool isChecked = false, Action<TrayEntry>? callback = null)
    {
        TrayEntryFlags flags = TrayEntryFlags.Checkbox;
        if (isChecked)
        {
            flags |= TrayEntryFlags.Checked;
        }

        TrayEntry entry = InsertEntryAt(-1, label, flags)!;
        if (callback != null)
        {
            entry.SetCallback(callback);
        }

        return entry;
    }

    /// <summary>
    /// Appends a new submenu entry to the menu.
    /// </summary>
    /// <param name="label">The text to be displayed on the entry.</param>
    /// <returns>The newly created submenu.</returns>
    public TrayMenu AddSubmenu(string label)
    {
        TrayEntry entry = InsertEntryAt(-1, label, TrayEntryFlags.Submenu)!;
        return entry.CreateSubmenu();
    }

    /// <summary>
    /// Appends a separator to the menu.
    /// </summary>
    /// <returns>The newly created separator entry.</returns>
    public TrayEntry AddSeparator()
    {
        return InsertEntryAt(-1, null, 0)!;
    }

    /// <summary>
    /// Removes an entry from the menu.
    /// </summary>
    /// <param name="entry">The entry to remove.</param>
    public void RemoveEntry(TrayEntry entry)
    {
        TrayEntry.RemoveCallback(entry.Handle);
        SDL_RemoveTrayEntry(entry.Handle);
    }
}
