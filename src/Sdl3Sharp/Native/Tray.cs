using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Surface;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_tray.h - System tray/notification area support.
/// </summary>
/// <remarks>
/// <para>SDL offers a way to add items to the "system tray" (more correctly called
/// the "notification area" on Windows). On platforms that offer this concept,
/// an SDL app can add a tray icon, submenus, checkboxes, and clickable
/// entries, and register a callback that is fired when the user clicks on
/// these pieces.</para>
/// </remarks>
public static unsafe partial class Tray
{
    /// <summary>
    /// An opaque handle representing a toplevel system tray object.
    /// </summary>
    public readonly struct SDL_Tray
    {
    }

    /// <summary>
    /// An opaque handle representing a menu/submenu on a system tray object.
    /// </summary>
    public readonly struct SDL_TrayMenu
    {
    }

    /// <summary>
    /// An opaque handle representing an entry on a system tray object.
    /// </summary>
    public readonly struct SDL_TrayEntry
    {
    }

    /// <summary>
    /// Flags that control the creation of system tray entries.
    /// </summary>
    /// <remarks>
    /// Some of these flags are required; exactly one of them must be specified at
    /// the time a tray entry is created. Other flags are optional; zero or more of
    /// those can be OR'ed together with the required flag.
    /// </remarks>
    [Flags]
    public enum SDL_TrayEntryFlags : uint
    {
        /// <summary>
        /// Make the entry a simple button. Required.
        /// </summary>
        SDL_TRAYENTRY_BUTTON = 0x00000001u,

        /// <summary>
        /// Make the entry a checkbox. Required.
        /// </summary>
        SDL_TRAYENTRY_CHECKBOX = 0x00000002u,

        /// <summary>
        /// Prepare the entry to have a submenu. Required.
        /// </summary>
        SDL_TRAYENTRY_SUBMENU = 0x00000004u,

        /// <summary>
        /// Make the entry disabled. Optional.
        /// </summary>
        SDL_TRAYENTRY_DISABLED = 0x80000000u,

        /// <summary>
        /// Make the entry checked. This is valid only for checkboxes. Optional.
        /// </summary>
        SDL_TRAYENTRY_CHECKED = 0x40000000u
    }

    /// <summary>
    /// Create an icon to be placed in the operating system's tray, or equivalent.
    /// </summary>
    /// <remarks>
    /// <para>Many platforms advise not using a system tray unless persistence is a
    /// necessary feature. Avoid needlessly creating a tray icon, as the user may
    /// feel like it clutters their interface.</para>
    /// <para>Using tray icons require the video subsystem.</para>
    /// </remarks>
    /// <param name="icon">A surface to be used as icon. May be NULL.</param>
    /// <param name="tooltip">A tooltip to be displayed when the mouse hovers the icon in
    /// UTF-8 encoding. Not supported on all platforms. May be NULL.</param>
    /// <returns>The newly created system tray icon.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Tray* SDL_CreateTray(
        SDL_Surface* icon,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string? tooltip);

    /// <summary>
    /// Updates the system tray icon's icon.
    /// </summary>
    /// <param name="tray">The tray icon to be updated.</param>
    /// <param name="icon">The new icon. May be NULL.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetTrayIcon(SDL_Tray* tray, SDL_Surface* icon);

    /// <summary>
    /// Updates the system tray icon's tooltip.
    /// </summary>
    /// <param name="tray">The tray icon to be updated.</param>
    /// <param name="tooltip">The new tooltip in UTF-8 encoding. May be NULL.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetTrayTooltip(
        SDL_Tray* tray,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string? tooltip);

    /// <summary>
    /// Create a menu for a system tray.
    /// </summary>
    /// <remarks>
    /// <para>This should be called at most once per tray icon.</para>
    /// <para>This function does the same thing as <see cref="SDL_CreateTraySubmenu"/>, except that
    /// it takes a <see cref="SDL_Tray"/> instead of a <see cref="SDL_TrayEntry"/>.</para>
    /// <para>A menu does not need to be destroyed; it will be destroyed with the tray.</para>
    /// </remarks>
    /// <param name="tray">The tray to bind the menu to.</param>
    /// <returns>The newly created menu.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_TrayMenu* SDL_CreateTrayMenu(SDL_Tray* tray);

    /// <summary>
    /// Create a submenu for a system tray entry.
    /// </summary>
    /// <remarks>
    /// <para>This should be called at most once per tray entry.</para>
    /// <para>This function does the same thing as <see cref="SDL_CreateTrayMenu"/>, except that it
    /// takes a <see cref="SDL_TrayEntry"/> instead of a <see cref="SDL_Tray"/>.</para>
    /// <para>A menu does not need to be destroyed; it will be destroyed with the tray.</para>
    /// </remarks>
    /// <param name="entry">The tray entry to bind the menu to.</param>
    /// <returns>The newly created menu.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_TrayMenu* SDL_CreateTraySubmenu(SDL_TrayEntry* entry);

    /// <summary>
    /// Gets a previously created tray menu.
    /// </summary>
    /// <remarks>
    /// <para>You should have called <see cref="SDL_CreateTrayMenu"/> on the tray object. This
    /// function allows you to fetch it again later.</para>
    /// <para>This function does the same thing as <see cref="SDL_GetTraySubmenu"/>, except that it
    /// takes a <see cref="SDL_Tray"/> instead of a <see cref="SDL_TrayEntry"/>.</para>
    /// <para>A menu does not need to be destroyed; it will be destroyed with the tray.</para>
    /// </remarks>
    /// <param name="tray">The tray entry to bind the menu to.</param>
    /// <returns>The menu associated with the tray.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_TrayMenu* SDL_GetTrayMenu(SDL_Tray* tray);

    /// <summary>
    /// Gets a previously created tray entry submenu.
    /// </summary>
    /// <remarks>
    /// <para>You should have called <see cref="SDL_CreateTraySubmenu"/> on the entry object. This
    /// function allows you to fetch it again later.</para>
    /// <para>This function does the same thing as <see cref="SDL_GetTrayMenu"/>, except that it
    /// takes a <see cref="SDL_TrayEntry"/> instead of a <see cref="SDL_Tray"/>.</para>
    /// <para>A menu does not need to be destroyed; it will be destroyed with the tray.</para>
    /// </remarks>
    /// <param name="entry">The tray entry to bind the menu to.</param>
    /// <returns>The submenu associated with the entry.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_TrayMenu* SDL_GetTraySubmenu(SDL_TrayEntry* entry);

    /// <summary>
    /// Returns a list of entries in the menu, in order.
    /// </summary>
    /// <param name="menu">The menu to get entries from.</param>
    /// <param name="count">An optional pointer to obtain the number of entries in the menu.</param>
    /// <returns>A NULL-terminated list of entries within the given menu. The
    /// pointer becomes invalid when any function that inserts or deletes
    /// entries in the menu is called.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_TrayEntry** SDL_GetTrayEntries(SDL_TrayMenu* menu, int* count);

    /// <summary>
    /// Removes a tray entry.
    /// </summary>
    /// <param name="entry">The entry to be deleted.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_RemoveTrayEntry(SDL_TrayEntry* entry);

    /// <summary>
    /// Insert a tray entry at a given position.
    /// </summary>
    /// <remarks>
    /// <para>If label is NULL, the entry will be a separator. Many functions won't work
    /// for an entry that is a separator.</para>
    /// <para>An entry does not need to be destroyed; it will be destroyed with the tray.</para>
    /// </remarks>
    /// <param name="menu">The menu to append the entry to.</param>
    /// <param name="pos">The desired position for the new entry. Entries at or following
    /// this place will be moved. If pos is -1, the entry is appended.</param>
    /// <param name="label">The text to be displayed on the entry, in UTF-8 encoding, or
    /// NULL for a separator.</param>
    /// <param name="flags">A combination of flags, some of which are mandatory.</param>
    /// <returns>The newly created entry, or NULL if pos is out of bounds.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_TrayEntry* SDL_InsertTrayEntryAt(
        SDL_TrayMenu* menu,
        int pos,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string? label,
        SDL_TrayEntryFlags flags);

    /// <summary>
    /// Sets the label of an entry.
    /// </summary>
    /// <remarks>
    /// <para>An entry cannot change between a separator and an ordinary entry; that is,
    /// it is not possible to set a non-NULL label on an entry that has a NULL
    /// label (separators), or to set a NULL label to an entry that has a non-NULL
    /// label. The function will silently fail if that happens.</para>
    /// </remarks>
    /// <param name="entry">The entry to be updated.</param>
    /// <param name="label">The new label for the entry in UTF-8 encoding.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetTrayEntryLabel(
        SDL_TrayEntry* entry,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string? label);

    /// <summary>
    /// Gets the label of an entry.
    /// </summary>
    /// <remarks>
    /// If the returned value is NULL, the entry is a separator.
    /// </remarks>
    /// <param name="entry">The entry to be read.</param>
    /// <returns>The label of the entry in UTF-8 encoding.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetTrayEntryLabel(SDL_TrayEntry* entry);

    /// <summary>
    /// Sets whether or not an entry is checked.
    /// </summary>
    /// <remarks>
    /// The entry must have been created with the <see cref="SDL_TrayEntryFlags.SDL_TRAYENTRY_CHECKBOX"/> flag.
    /// </remarks>
    /// <param name="entry">The entry to be updated.</param>
    /// <param name="checked">True if the entry should be checked; false otherwise.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetTrayEntryChecked(
        SDL_TrayEntry* entry,
        [MarshalAs(UnmanagedType.U1)] bool @checked);

    /// <summary>
    /// Gets whether or not an entry is checked.
    /// </summary>
    /// <remarks>
    /// The entry must have been created with the <see cref="SDL_TrayEntryFlags.SDL_TRAYENTRY_CHECKBOX"/> flag.
    /// </remarks>
    /// <param name="entry">The entry to be read.</param>
    /// <returns>True if the entry is checked; false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetTrayEntryChecked(SDL_TrayEntry* entry);

    /// <summary>
    /// Sets whether or not an entry is enabled.
    /// </summary>
    /// <param name="entry">The entry to be updated.</param>
    /// <param name="enabled">True if the entry should be enabled; false otherwise.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetTrayEntryEnabled(
        SDL_TrayEntry* entry,
        [MarshalAs(UnmanagedType.U1)] bool enabled);

    /// <summary>
    /// Gets whether or not an entry is enabled.
    /// </summary>
    /// <param name="entry">The entry to be read.</param>
    /// <returns>True if the entry is enabled; false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetTrayEntryEnabled(SDL_TrayEntry* entry);

    /// <summary>
    /// Sets a callback to be invoked when the entry is selected.
    /// </summary>
    /// <param name="entry">The entry to be updated.</param>
    /// <param name="callback">A callback to be invoked when the entry is selected.</param>
    /// <param name="userdata">An optional pointer to pass extra data to the callback when
    /// it will be invoked.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetTrayEntryCallback(
        SDL_TrayEntry* entry,
        delegate* unmanaged[Cdecl]<nuint, SDL_TrayEntry*, void> callback,
        nuint userdata);

    /// <summary>
    /// Simulate a click on a tray entry.
    /// </summary>
    /// <param name="entry">The entry to activate.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ClickTrayEntry(SDL_TrayEntry* entry);

    /// <summary>
    /// Destroys a tray object.
    /// </summary>
    /// <remarks>
    /// This also destroys all associated menus and entries.
    /// </remarks>
    /// <param name="tray">The tray icon to be destroyed.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyTray(SDL_Tray* tray);

    /// <summary>
    /// Gets the menu containing a certain tray entry.
    /// </summary>
    /// <param name="entry">The entry for which to get the parent menu.</param>
    /// <returns>The parent menu.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_TrayMenu* SDL_GetTrayEntryParent(SDL_TrayEntry* entry);

    /// <summary>
    /// Gets the entry for which the menu is a submenu, if the current menu is a submenu.
    /// </summary>
    /// <remarks>
    /// Either this function or <see cref="SDL_GetTrayMenuParentTray"/> will return non-NULL
    /// for any given menu.
    /// </remarks>
    /// <param name="menu">The menu for which to get the parent entry.</param>
    /// <returns>The parent entry, or NULL if this menu is not a submenu.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_TrayEntry* SDL_GetTrayMenuParentEntry(SDL_TrayMenu* menu);

    /// <summary>
    /// Gets the tray for which this menu is the first-level menu, if the current
    /// menu isn't a submenu.
    /// </summary>
    /// <remarks>
    /// Either this function or <see cref="SDL_GetTrayMenuParentEntry"/> will return non-NULL
    /// for any given menu.
    /// </remarks>
    /// <param name="menu">The menu for which to get the parent tray.</param>
    /// <returns>The parent tray, or NULL if this menu is a submenu.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Tray* SDL_GetTrayMenuParentTray(SDL_TrayMenu* menu);

    /// <summary>
    /// Update the trays.
    /// </summary>
    /// <remarks>
    /// This is called automatically by the event loop and is only needed if you're
    /// using trays but aren't handling SDL events.
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UpdateTrays();
}
