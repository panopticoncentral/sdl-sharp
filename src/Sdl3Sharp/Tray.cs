using Sdl3Sharp.Graphics;

using static Sdl3Sharp.Native.Tray;

namespace Sdl3Sharp;

/// <summary>
/// Represents a system tray icon.
/// </summary>
/// <remarks>
/// <para>SDL offers a way to add items to the "system tray" (more correctly called
/// the "notification area" on Windows). On platforms that offer this concept,
/// an SDL app can add a tray icon, submenus, checkboxes, and clickable
/// entries, and register a callback that is fired when the user clicks on
/// these pieces.</para>
/// <para>Many platforms advise not using a system tray unless persistence is a
/// necessary feature. Avoid needlessly creating a tray icon, as the user may
/// feel like it clutters their interface.</para>
/// <para>Using tray icons requires the video subsystem.</para>
/// </remarks>
public sealed unsafe class Tray : IDisposable
{
    private bool _disposed;
    private readonly bool _ownsHandle;

    /// <summary>
    /// Gets the underlying SDL_Tray pointer.
    /// </summary>
    public SDL_Tray* Handle { get; private set; }

    /// <summary>
    /// Initializes a new instance wrapping an existing SDL_Tray pointer.
    /// </summary>
    /// <param name="handle">The SDL_Tray pointer.</param>
    internal Tray(SDL_Tray* handle)
    {
        Handle = handle;
        _ownsHandle = false;
    }

    private Tray(SDL_Tray* handle, bool ownsHandle)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Creates a new system tray icon.
    /// </summary>
    /// <param name="icon">A surface to be used as icon. May be null.</param>
    /// <param name="tooltip">A tooltip to be displayed when the mouse hovers the icon.
    /// Not supported on all platforms. May be null.</param>
    /// <returns>The newly created system tray icon.</returns>
    public static Tray Create(Surface? icon = null, string? tooltip = null)
    {
        SDL_Tray* tray = SDL_CreateTray(
            icon is not null ? icon.Handle : null,
            tooltip);

        return new Tray(tray, ownsHandle: true);
    }

    /// <summary>
    /// Updates the tray icon's icon.
    /// </summary>
    /// <param name="icon">The new icon. May be null.</param>
    public void SetIcon(Surface? icon)
    {
        ThrowIfDisposed();
        SDL_SetTrayIcon(Handle, icon is not null ? icon.Handle : null);
    }

    /// <summary>
    /// Updates the tray icon's tooltip.
    /// </summary>
    /// <param name="tooltip">The new tooltip. May be null.</param>
    public void SetTooltip(string? tooltip)
    {
        ThrowIfDisposed();
        SDL_SetTrayTooltip(Handle, tooltip);
    }

    /// <summary>
    /// Creates a menu for this tray.
    /// </summary>
    /// <remarks>
    /// <para>This should be called at most once per tray icon.</para>
    /// <para>A menu does not need to be destroyed; it will be destroyed with the tray.</para>
    /// </remarks>
    /// <returns>The newly created menu.</returns>
    public TrayMenu CreateMenu()
    {
        ThrowIfDisposed();
        return new TrayMenu(SDL_CreateTrayMenu(Handle));
    }

    /// <summary>
    /// Gets a previously created tray menu.
    /// </summary>
    /// <remarks>
    /// You should have called <see cref="CreateMenu"/> on this tray first.
    /// </remarks>
    /// <returns>The menu associated with this tray, or null if none exists.</returns>
    public TrayMenu? GetMenu()
    {
        ThrowIfDisposed();
        SDL_TrayMenu* menu = SDL_GetTrayMenu(Handle);
        return menu != null ? new TrayMenu(menu) : null;
    }

    /// <summary>
    /// Updates all trays.
    /// </summary>
    /// <remarks>
    /// This is called automatically by the event loop and is only needed if you're
    /// using trays but aren't handling SDL events.
    /// </remarks>
    public static void UpdateAll()
    {
        SDL_UpdateTrays();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_ownsHandle && Handle != null)
        {
            // Clean up all callback registrations for entries in this tray
            CleanupCallbacks();
            SDL_DestroyTray(Handle);
        }

        Handle = null;
        _disposed = true;
    }

    private void CleanupCallbacks()
    {
        // Recursively clean up callbacks for all entries in the tray's menus
        SDL_TrayMenu* menu = SDL_GetTrayMenu(Handle);
        if (menu != null)
        {
            CleanupMenuCallbacks(menu);
        }
    }

    private static void CleanupMenuCallbacks(SDL_TrayMenu* menu)
    {
        int count;
        SDL_TrayEntry** entries = SDL_GetTrayEntries(menu, &count);

        if (entries == null)
        {
            return;
        }

        for (var i = 0; i < count; i++)
        {
            SDL_TrayEntry* entry = entries[i];
            TrayEntry.RemoveCallback(entry);

            // Check for submenu
            SDL_TrayMenu* submenu = SDL_GetTraySubmenu(entry);
            if (submenu != null)
            {
                CleanupMenuCallbacks(submenu);
            }
        }
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
