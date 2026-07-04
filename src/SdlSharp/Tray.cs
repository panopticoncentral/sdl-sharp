using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Tray;

namespace SdlSharp;

/// <summary>
/// A system tray (notification area / menu bar) icon with an optional menu.
/// Tray APIs must be called on the main thread. Menus and entries are owned by
/// the tray: they become invalid when the tray is disposed or an entry is removed.
/// </summary>
public sealed unsafe class Tray : IDisposable
{
    internal Native.SDL_Tray* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private Native.SDL_Tray* _handle;

    internal bool IsDisposed => _handle == null;

    // One GCHandle per entry that has a managed callback, keyed by the native
    // entry pointer. Freed when the entry's callback is replaced/cleared, when
    // the entry is removed, or (for any still registered) at Dispose.
    internal readonly Dictionary<nint, GCHandle> CallbackHandles = new();

    private Tray(Native.SDL_Tray* handle) => _handle = handle;

    /// <summary>
    /// Creates a tray icon.
    /// </summary>
    /// <param name="icon">The icon image, or null for a platform default/blank icon.</param>
    /// <param name="tooltip">Tooltip text, or null.</param>
    public static Tray Create(Graphics.Surface? icon = null, string? tooltip = null) =>
        new(Check(SDL_CreateTray(icon != null ? icon.Handle : null, ToUtf8(tooltip))));

    /// <summary>Replaces the tray icon.</summary>
    /// <param name="icon">The new icon, or null to remove it.</param>
    public void SetIcon(Graphics.Surface? icon) => SDL_SetTrayIcon(Handle, icon != null ? icon.Handle : null);

    /// <summary>Replaces the tooltip text.</summary>
    /// <param name="tooltip">The new tooltip, or null to remove it.</param>
    public void SetTooltip(string? tooltip) => SDL_SetTrayTooltip(Handle, ToUtf8(tooltip));

    /// <summary>Creates the tray's root menu. Call at most once per tray (SDL contract).</summary>
    public TrayMenu CreateMenu() => new(Check(SDL_CreateTrayMenu(Handle)), this);

    /// <summary>Gets the tray's root menu, or null if none was created.</summary>
    public TrayMenu? Menu
    {
        get
        {
            var menu = SDL_GetTrayMenu(Handle);
            return menu == null ? null : new TrayMenu(menu, this);
        }
    }

    /// <summary>
    /// Processes pending tray events. Call this if the app does not run an SDL event
    /// loop; apps that pump events regularly do not need it.
    /// </summary>
    public static void Update() => SDL_UpdateTrays();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static void EntryCallback(void* userdata, Native.SDL_TrayEntry* entry)
    {
        try
        {
            var holder = (TrayEntry.CallbackHolder)GCHandle.FromIntPtr((nint)userdata).Target!;
            holder.Callback(new TrayEntry(entry, holder.Tray));
        }
        catch
        {
            // Exceptions must not cross the native boundary.
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_handle != null)
        {
            SDL_DestroyTray(_handle);
        }

        _handle = null;

        lock (CallbackHandles)
        {
            foreach (var (_, handle) in CallbackHandles)
            {
                handle.Free();
            }

            CallbackHandles.Clear();
        }
    }
}
