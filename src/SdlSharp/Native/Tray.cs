using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>Opaque handle for a system tray icon.</summary>
public struct SDL_Tray;

/// <summary>Opaque handle for a tray menu.</summary>
public struct SDL_TrayMenu;

/// <summary>Opaque handle for a tray menu entry.</summary>
public struct SDL_TrayEntry;

/// <summary>
/// Native bindings for SDL_tray.h — system tray icons and menus.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Tray
{
    public const uint SDL_TRAYENTRY_BUTTON = 0x00000001u;
    public const uint SDL_TRAYENTRY_CHECKBOX = 0x00000002u;
    public const uint SDL_TRAYENTRY_SUBMENU = 0x00000004u;
    public const uint SDL_TRAYENTRY_DISABLED = 0x80000000u;
    public const uint SDL_TRAYENTRY_CHECKED = 0x40000000u;

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateTray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Tray* SDL_CreateTray(SDL_Surface* icon, ReadOnlySpan<byte> tooltip);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTrayIcon")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetTrayIcon(SDL_Tray* tray, SDL_Surface* icon);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTrayTooltip")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetTrayTooltip(SDL_Tray* tray, ReadOnlySpan<byte> tooltip);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateTrayMenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_TrayMenu* SDL_CreateTrayMenu(SDL_Tray* tray);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateTraySubmenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_TrayMenu* SDL_CreateTraySubmenu(SDL_TrayEntry* entry);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTrayMenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_TrayMenu* SDL_GetTrayMenu(SDL_Tray* tray);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTraySubmenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_TrayMenu* SDL_GetTraySubmenu(SDL_TrayEntry* entry);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTrayEntries")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_TrayEntry** SDL_GetTrayEntries(SDL_TrayMenu* menu, out int count);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RemoveTrayEntry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_RemoveTrayEntry(SDL_TrayEntry* entry);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_InsertTrayEntryAt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_TrayEntry* SDL_InsertTrayEntryAt(SDL_TrayMenu* menu, int pos, ReadOnlySpan<byte> label, uint flags);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTrayEntryLabel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetTrayEntryLabel(SDL_TrayEntry* entry, ReadOnlySpan<byte> label);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTrayEntryLabel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetTrayEntryLabel(SDL_TrayEntry* entry);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTrayEntryChecked")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetTrayEntryChecked(SDL_TrayEntry* entry, [MarshalAs(UnmanagedType.U1)] bool @checked);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTrayEntryChecked")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetTrayEntryChecked(SDL_TrayEntry* entry);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTrayEntryEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetTrayEntryEnabled(SDL_TrayEntry* entry, [MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTrayEntryEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetTrayEntryEnabled(SDL_TrayEntry* entry);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTrayEntryCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetTrayEntryCallback(SDL_TrayEntry* entry, delegate* unmanaged[Cdecl]<void*, SDL_TrayEntry*, void> callback, void* userdata);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ClickTrayEntry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_ClickTrayEntry(SDL_TrayEntry* entry);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DestroyTray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DestroyTray(SDL_Tray* tray);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTrayEntryParent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_TrayMenu* SDL_GetTrayEntryParent(SDL_TrayEntry* entry);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTrayMenuParentEntry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_TrayEntry* SDL_GetTrayMenuParentEntry(SDL_TrayMenu* menu);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTrayMenuParentTray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Tray* SDL_GetTrayMenuParentTray(SDL_TrayMenu* menu);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UpdateTrays")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UpdateTrays();
}
