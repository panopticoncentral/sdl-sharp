# Phase 9 Graphics Completion Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Complete the video (48/114), surface (27/65), and render (70/102) surfaces per `docs/superpowers/specs/2026-07-03-graphics-completion-design.md`, including GlContext, the window-create property constants, PNG support, and the hit-test callback.

**Architecture:** Native bindings appended to the three existing `Native/*.cs` files with per-file accounting; managed members follow the phase 7–8 conventions (guarded handles, non-owning wrappers, Check discipline, doc conventions). New files: `Native/` gains nothing new; `Graphics/` gains `GlContext.cs`, `Gl.cs`, `GlEnums.cs`, `ScreenSaver.cs`, `WindowProperties.cs`, `DisplayProperties.cs`, `SurfaceProperties.cs`, `SurfaceFlags.cs`, `SystemTheme.cs`, `DisplayOrientation.cs`, `ProgressState.cs`, `HitTestResult.cs`; `Samples/SurfaceOps/` is the new runtime gate.

**Tech Stack:** C# 13 / .NET 10, LibraryImport P/Invoke.

**Header truth:** `/Users/paulv/Projects/SDL/include/SDL3/SDL_{video,surface,render}.h`. The audit (`audit/sdl3-coverage-2026-07.json`) guides but the header wins (phase 8 precedent). Binding boilerplate recipe, typed-ID conventions, doc conventions (no-throw sentinels, throw-on-unsupported, -1/null both-direction semantics), and gates (zero-warning `dotnet build SdlSharp.slnx` per task; commit per task; NO Co-Authored-By) all as in the phase 8 plan.

**Pre-verified header facts (from plan-time inspection — re-verify while implementing):**
- `SDL_RotateSurface(SDL_Surface*, float angle)` → new `SDL_Surface*` (float degrees).
- `SDL_BlitSurfaceTiled(src, const SDL_Rect*, dst, const SDL_Rect*)`;
  `SDL_BlitSurfaceTiledWithScale(src, srcrect, float scale, SDL_ScaleMode, dst, dstrect)`;
  `SDL_BlitSurface9Grid(src, srcrect, int left_w, int right_w, int top_h, int bottom_h, float scale, SDL_ScaleMode, dst, dstrect)`.
- `SDL_SurfaceFlags` = `Uint32` typedef + 4 `#define` bits (PREALLOCATED/LOCK_NEEDED/LOCKED/SIMD_ALIGNED).
- 38 `SDL_PROP_WINDOW_CREATE_*` + 37 post-create `SDL_PROP_WINDOW_*` names (75 total `SDL_PROP_WINDOW_` defines).
- Enums: `SDL_SystemTheme` (video.h:110), `SDL_DisplayOrientation` (:159), `SDL_ProgressState` (:340), `SDL_GLAttr` (:470), `SDL_HitTestResult` (:2828), callback `SDL_HitTest` (:2852: `SDL_HitTestResult (SDLCALL*)(SDL_Window*, const SDL_Point*, void*)` — verify the exact param list).
- Already bound in Native/Render.cs (do NOT re-bind): RenderGeometryRaw, RenderDebugText, RenderTexture9Grid/Tiled/Affine/Rotated, RenderCoordinatesFrom/ToWindow, render scale pair, vsync pair, texture address mode pair.
- `Texture.cs` exposes property constants as `public const string PropCreate* = Render.SDL_PROP_TEXTURE_CREATE_*;` — EXTEND THIS PATTERN (constants live on Texture/Renderer, not new classes) for the render property completion. Window/Display/Surface get NEW const classes (WindowProperties/DisplayProperties/SurfaceProperties) since no precedent exists there.
- `Surface.cs` naming precedent: `LoadBmp(string file)` / `SaveBmp(string file)` → new members are `Load`, `LoadPng`, `SavePng`.
- Managed callback precedent: `Graphics/FileDialog.cs` (GCHandle.Alloc + UnmanagedCallersOnly trampoline + GCHandle.FromIntPtr in callback).

---

### Task 1: Native video batch A — window management, displays, property constants, enums

**Files:**
- Modify: `src/SdlSharp/Native/Video.cs`

- [ ] **Step 1.1: Add the native enums** (before the class, original C names, members verified against header): `SDL_SystemTheme`, `SDL_DisplayOrientation`, `SDL_ProgressState`, `SDL_HitTestResult`, and `SDL_FlashOperation` only if missing (it exists — verify).

- [ ] **Step 1.2: Bind the missing non-GL functions.** 48 are bound (EntryPoint grep). Derive the exact missing set: extract the header's 114 function names, subtract bound, subtract the GL/EGL set (Task 2) and the skip set below. Bind everything remaining — expect approximately (verify each signature in the header; boilerplate recipe; typed `SDL_WindowID`/`SDL_DisplayID` per file convention):

SDL_GetSystemTheme, SDL_GetFullscreenDisplayModes, SDL_GetClosestFullscreenDisplayMode, SDL_SetWindowFullscreenMode, SDL_GetWindowFullscreenMode, SDL_GetWindows, SDL_GetDisplayProperties, SDL_GetNaturalDisplayOrientation, SDL_GetCurrentDisplayOrientation, SDL_GetDisplayForPoint, SDL_GetDisplayForRect, SDL_SetWindowAlwaysOnTop, SDL_SetWindowAspectRatio, SDL_GetWindowAspectRatio, SDL_SyncWindow, SDL_SetWindowMouseGrab, SDL_GetWindowMouseGrab, SDL_SetWindowKeyboardGrab, SDL_GetWindowKeyboardGrab, SDL_SetWindowMouseRect, SDL_GetWindowMouseRect, SDL_CreatePopupWindow, SDL_GetWindowParent, SDL_SetWindowParent, SDL_SetWindowModal, SDL_SetWindowFocusable, SDL_SetWindowHitTest, SDL_ScreenSaverEnabled, SDL_EnableScreenSaver, SDL_DisableScreenSaver, SDL_GetWindowSafeArea, SDL_GetWindowPixelFormat, SDL_SetWindowProgressState, SDL_GetWindowProgressState, SDL_SetWindowProgressValue, SDL_GetWindowProgressValue, SDL_SetWindowShape, SDL_ShowWindowSystemMenu, SDL_SetWindowSurfaceVSync, SDL_GetWindowSurfaceVSync, SDL_WindowHasSurface, SDL_GetGrabbedWindow, SDL_GetWindowBordersSize.

`SDL_SetWindowHitTest` binds as `public static unsafe partial bool SDL_SetWindowHitTest(SDL_Window* window, delegate* unmanaged[Cdecl]<SDL_Window*, SDL_Point*, void*, SDL_HitTestResult> callback, void* callback_data);` — match how other function-pointer params are declared in this codebase (grep `delegate* unmanaged` in Native/ for the precedent, e.g. Properties/Assert).

- [ ] **Step 1.3: Add the 75 `SDL_PROP_WINDOW_*` const strings** (38 CREATE + 37 post-create), byte-exact vs header, `public const string` form, documented, grouped with a comment separating the two families. Also the two `SDL_WINDOW_SURFACE_VSYNC_*` int constants if defined (verify).

- [ ] **Step 1.4: Skip comment + accounting.** Video header skips (this file, after Task 2 completes the picture): EGL group (`SDL_EGL_*` — platform), `SDL_GetWindowICCProfile` (raw blob, niche). State partial accounting: bound-after-A + GL-set-size + skips ≈ 114 (final arithmetic lands in Task 2).

- [ ] **Step 1.5: Build (`dotnet build SdlSharp.slnx` → 0/0) + commit**

```bash
git add src/SdlSharp/Native/Video.cs
git commit -m "Bind window management, display, and property surface of SDL_video.h"
```

---

### Task 2: Native video batch B — OpenGL

**Files:**
- Modify: `src/SdlSharp/Native/Video.cs`

- [ ] **Step 2.1: Add GL types**: `public struct SDL_GLContext;` opaque (verify: header typedefs it as a pointer to `SDL_GLContextState` — an opaque pointer struct marker is the file convention), native `SDL_GLAttr` enum (all members from header :470), and the `SDL_GLProfile`/`SDL_GLContextFlag`/`SDL_GLContextReleaseFlag`/`SDL_GLContextResetNotification` groups in whatever form the header defines them (typedef'd Uint32 + defines → const uint groups or [Flags] enums; match the SurfaceFlags treatment decided in Task 6 — enums preferred where they're closed sets).

- [ ] **Step 2.2: Bind the GL functions** (verify each; ReadOnlySpan<byte> for the two string params): SDL_GL_LoadLibrary, SDL_GL_GetProcAddress (returns `SDL_FunctionPointer`/void* — bind as `nint` or `void*` per header; check how the codebase binds function-pointer returns), SDL_GL_UnloadLibrary, SDL_GL_ExtensionSupported, SDL_GL_ResetAttributes, SDL_GL_SetAttribute, SDL_GL_GetAttribute, SDL_GL_CreateContext, SDL_GL_MakeCurrent, SDL_GL_GetCurrentWindow, SDL_GL_GetCurrentContext, SDL_GL_SwapWindow, SDL_GL_SetSwapInterval, SDL_GL_GetSwapInterval, SDL_GL_DestroyContext.

- [ ] **Step 2.3: FINAL video accounting**: bound (Task 1 + Task 2 totals) + skipped (EGL functions enumerated by name, ICC profile) = 114. State the arithmetic; set-diff proof.

- [ ] **Step 2.4: Build + commit**

```bash
git add src/SdlSharp/Native/Video.cs
git commit -m "Bind SDL_video.h OpenGL context and attribute functions"
```

---

### Task 3: Managed display/fullscreen/theme/screensaver/properties

**Files:**
- Create: `src/SdlSharp/Graphics/SystemTheme.cs`, `src/SdlSharp/Graphics/DisplayOrientation.cs`, `src/SdlSharp/Graphics/ScreenSaver.cs`, `src/SdlSharp/Graphics/WindowProperties.cs`, `src/SdlSharp/Graphics/DisplayProperties.cs`
- Modify: `src/SdlSharp/Graphics/Display.cs`, `src/SdlSharp/Graphics/DisplayMode.cs`, `src/SdlSharp/Application.cs`

- [ ] **Step 3.1: Public enums** SystemTheme (Unknown/Light/Dark) and DisplayOrientation (Unknown/Landscape/LandscapeFlipped/Portrait/PortraitFlipped) — mirror native, cast style per convention.

- [ ] **Step 3.2: `DisplayMode.ToNative()`** — internal, inverse of FromNative (all 8 fields; the native struct's `internal` pointer field if any stays default — READ the native SDL_DisplayMode struct first; it has a `SDL_DisplayModeData *internal` pointer — set null/default and note that SetWindowFullscreenMode modes should originate from GetFullscreenModes for exclusive-mode matching, per doc).

- [ ] **Step 3.3: Display additions** (follow Display.cs's existing shape — read it first):

```csharp
/// <summary>Gets the fullscreen display modes for this display.</summary>
public DisplayMode[] GetFullscreenModes()   // SDL_GetFullscreenDisplayModes: SDL_DisplayMode** + int* count; copy elements via DisplayMode.FromNative(modes[i]), then SDL_free the array (verify header: single SDL_free of the returned array)
/// <summary>Gets the closest fullscreen mode to the requested size/rate, ...</summary>
public DisplayMode GetClosestFullscreenMode(int w, int h, float refreshRate = 0f, bool includeHighDensityModes = false)  // out-param native (SDL_DisplayMode* result) → Check + FromNative — VERIFY signature form in header
/// <summary>Gets the current orientation of this display.</summary>
public DisplayOrientation Orientation => ...
/// <summary>Gets the natural orientation of this display.</summary>
public DisplayOrientation NaturalOrientation => ...
/// <summary>Gets the properties of this display (names in <see cref="DisplayProperties"/>). Owned by SDL.</summary>
public PropertyGroup Properties => new(SDL_GetDisplayProperties(...), ownsHandle: false);
/// <summary>Gets the display containing the given point.</summary>
public static Display GetForPoint(Point point)   // SDL_Point* param — Point has ToNative? verify; CheckId on returned display id per Display.cs conventions
/// <summary>Gets the display best matching the given rectangle.</summary>
public static Display GetForRect(Rectangle rect)
```

- [ ] **Step 3.4: `ScreenSaver.cs`** (full code):

```csharp
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Video;

namespace SdlSharp.Graphics;

/// <summary>
/// Controls whether the operating system's screen saver may activate while the
/// application is running. SDL disables it by default while video is initialized.
/// </summary>
public static class ScreenSaver
{
    /// <summary>
    /// Gets or sets whether the screen saver is allowed to activate.
    /// </summary>
    public static bool Enabled
    {
        get => SDL_ScreenSaverEnabled();
        set => Check(value ? SDL_EnableScreenSaver() : SDL_DisableScreenSaver());
    }
}
```

- [ ] **Step 3.5: `Application.SystemTheme`** static property → `(SystemTheme)SDL_GetSystemTheme()` with doc.

- [ ] **Step 3.6: `WindowProperties.cs` / `DisplayProperties.cs`** — const classes referencing the native consts; WindowProperties documents the Create*/query split (Create-prefixed members for SDL_PROP_WINDOW_CREATE_*, bare names for post-create queries; 75 members total, each one-line documented from the header's descriptions). DisplayProperties: the HDR/KMSDRM names present in the header (verify list).

- [ ] **Step 3.7: Build + commit**

```bash
git add src/SdlSharp/Graphics/ src/SdlSharp/Application.cs
git commit -m "Managed display modes, orientation, theme, screensaver, window properties"
```

---

### Task 4: Managed window management + hit test

**Files:**
- Create: `src/SdlSharp/Graphics/ProgressState.cs`, `src/SdlSharp/Graphics/HitTestResult.cs`
- Modify: `src/SdlSharp/Graphics/Window.cs`

- [ ] **Step 4.1: Enums** ProgressState (mirror native) and HitTestResult (Normal, Draggable, ResizeTopLeft, ResizeTop, ResizeTopRight, ResizeRight, ResizeBottomRight, ResizeBottom, ResizeBottomLeft, ResizeLeft — verify order against header :2828).

- [ ] **Step 4.2: Window members** (doc conventions apply: null-clears both directions, throw-on-unsupported where SDL reports false, Sync's blocking note):

```csharp
public static Window[] GetWindows()                      // non-owning wrappers; SDL_free the array
public static Window? FromId(uint id)                    // non-owning; null when not found (SDL_GetWindowFromID already bound)
public static Window? GrabbedWindow                      // SDL_GetGrabbedWindow, non-owning, null when none
public WindowFlags Flags                                 // (WindowFlags)SDL_GetWindowFlags(Handle) — verify WindowFlags enum covers current header bits; add missing members
public void SetFullscreenMode(DisplayMode? mode)         // null → SDL_SetWindowFullscreenMode(Handle, null) borderless; else stack native + pointer; Check; doc: modes should come from Display.GetFullscreenModes; applied on next SetFullscreen(true) or immediately when fullscreen (verify header wording)
public DisplayMode? GetFullscreenMode()                  // null pointer → null (borderless/windowed)
public bool MouseGrabbed { get; set; }                   // Check on set
public bool KeyboardGrabbed { get; set; }
public Rectangle? MouseConfinementRect { get; set; }     // get: SDL_GetWindowMouseRect null → null; set: null clears (SDL_SetWindowMouseRect(Handle, null))
public void SetAlwaysOnTop(bool onTop)
public void SetAspectRatio(float minAspect, float maxAspect)   // Check
public void GetAspectRatio(out float minAspect, out float maxAspect)
public void Sync()                                       // Check; doc: blocks until pending window state is applied
public bool HasSurface                                   // SDL_WindowHasSurface
public void GetBordersSize(out int top, out int left, out int bottom, out int right)  // doc: may fail on some platforms → SdlException
public Window CreatePopup(int offsetX, int offsetY, int w, int h, WindowFlags flags)  // owning wrapper; doc popup constraints (parent-relative, needs POPUP flag semantics — verify header: SDL_CreatePopupWindow(parent, ...))
public Window? Parent                                    // non-owning; null when none
public void SetParent(Window? parent)                    // null clears; Check
public void SetModal(bool modal)                         // Check; doc: requires parent
public void SetFocusable(bool focusable)
public void ShowSystemMenu(int x, int y)                 // Check
public void SetShape(Surface shape)                      // Check; doc transparency requirements per header
public PixelFormat PixelFormat                           // SDL_GetWindowPixelFormat
public Rectangle SafeArea                                // SDL_GetWindowSafeArea out rect; Check
public ProgressState ProgressState { get; set; }         // Check on set
public float ProgressValue { get; set; }                 // 0..1; Check on set
public bool SurfaceVSync... // SDL_SetWindowSurfaceVSync(int)/Get — expose as int property SurfaceVSyncInterval with the adaptive (-1)/disabled (0) doc, constants per header
```

(The exact member bodies follow the file's existing one-liner Check style; each fully documented. Where a listed native turns out to be already-exposed — e.g. Show/Hide exist — skip silently and note in the report.)

Non-owning prerequisite: `GetWindows`/`FromId`/`GrabbedWindow`/`Parent` construct non-owning `Window` wrappers — verify `Window`'s internal ctor already has the `bool ownsHandle = true` parameter (CLAUDE.md's example shows it; the file may predate it). If absent, retrofit per the Cursor pattern (Dispose gates `SDL_DestroyWindow` on `_ownsHandle`, always nulls) and note it. Task 5's `GlContext.CurrentWindow` depends on the same ctor.

- [ ] **Step 4.3: Hit test** (full pattern — adapt only if FileDialog precedent differs):

```csharp
/// <summary>
/// Handles hit testing for custom window dragging and resizing.
/// </summary>
/// <param name="window">The window being tested.</param>
/// <param name="area">The point being tested, in window coordinates.</param>
/// <returns>The hit test result for the point.</returns>
public delegate HitTestResult HitTestHandler(Window window, Point area);

private GCHandle _hitTestHandle;   // freed on replace/clear/Dispose

/// <summary>
/// Sets or clears (null) the hit-test callback used for custom window dragging
/// and resizing regions. Exceptions thrown by the handler are swallowed and
/// treated as <see cref="HitTestResult.Normal"/> (they must not cross the
/// native boundary).
/// </summary>
public void SetHitTest(HitTestHandler? handler)
{
    if (_hitTestHandle.IsAllocated)
    {
        _hitTestHandle.Free();
        _hitTestHandle = default;
    }
    if (handler == null)
    {
        Check(SDL_SetWindowHitTest(Handle, null, null));
        return;
    }
    var holder = new HitTestHolder(this, handler);
    _hitTestHandle = GCHandle.Alloc(holder);
    Check(SDL_SetWindowHitTest(Handle, &HitTestCallback, (void*)GCHandle.ToIntPtr(_hitTestHandle)));
}

private sealed record HitTestHolder(Window Window, HitTestHandler Handler);

[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
private static SDL_HitTestResult HitTestCallback(SDL_Window* win, SDL_Point* area, void* userdata)
{
    try
    {
        var holder = (HitTestHolder)GCHandle.FromIntPtr((nint)userdata).Target!;
        return (SDL_HitTestResult)holder.Handler(holder.Window, new Point(area->x, area->y));
    }
    catch
    {
        return SDL_HitTestResult.SDL_HITTEST_NORMAL;
    }
}
```

Also free `_hitTestHandle` in `Dispose()` (before the destroy call). Verify `Point` field names on the native SDL_Point (x/y).

- [ ] **Step 4.4: Build + grep gate (`grep -n "public.*SDL_" src/SdlSharp/Graphics/Window.cs` → body calls only) + commit**

```bash
git add src/SdlSharp/Graphics/
git commit -m "Managed window management batch and hit-test callback"
```

---

### Task 5: GlContext + Gl + Window GL members

**Files:**
- Create: `src/SdlSharp/Graphics/GlContext.cs`, `src/SdlSharp/Graphics/Gl.cs`, `src/SdlSharp/Graphics/GlEnums.cs`
- Modify: `src/SdlSharp/Graphics/Window.cs`

- [ ] **Step 5.1: `GlEnums.cs`** — public `GlAttribute` (mirror SDL_GLAttr) + the profile/context-flag/release-behavior/reset-notification groups as public enums (`GlProfile`, `GlContextFlags` [Flags], `GlContextReleaseBehavior`, `GlContextResetNotification` — names/values verified against header).

- [ ] **Step 5.2: `GlContext.cs`** — standard guarded-handle owning wrapper:

```csharp
public sealed unsafe class GlContext : IDisposable
{
    internal SDL_GLContext* Handle { get { ObjectDisposedException.ThrowIf(_handle == null, this); return _handle; } }
    private SDL_GLContext* _handle;
    internal GlContext(SDL_GLContext* handle) => _handle = handle;

    /// <summary>Makes this context current on the given window.</summary>
    public void MakeCurrent(Window window) => Check(SDL_GL_MakeCurrent(window.Handle, Handle));

    /// <summary>Gets the currently active context, as a non-owning wrapper, or null.</summary>
    public static GlContext? Current
    {
        get
        {
            var handle = SDL_GL_GetCurrentContext();
            return handle == null ? null : new GlContext(handle, ownsHandle: false);
        }
    }

    /// <summary>Gets the window whose context is current, as a non-owning wrapper, or null.</summary>
    public static Window? CurrentWindow
    {
        get
        {
            var handle = SDL_GL_GetCurrentWindow();
            return handle == null ? null : new Window(handle, ownsHandle: false);
        }
    }

    public void Dispose() { if (_ownsHandle && _handle != null) Check(SDL_GL_DestroyContext(_handle)); _handle = null; }
}
```

(Complete with `_ownsHandle` plumbing exactly like Cursor.cs; note SDL_GL_DestroyContext returns bool → Check.)

- [ ] **Step 5.3: `Gl.cs`**:

```csharp
public static unsafe class Gl
{
    public static void LoadLibrary(string? path) => Check(SDL_GL_LoadLibrary(ToUtf8(path)));
    public static void UnloadLibrary() => SDL_GL_UnloadLibrary();
    public static nint GetProcAddress(string name) => (nint)SDL_GL_GetProcAddress(ToUtf8(name));  // adjust to actual native return
    public static bool ExtensionSupported(string name) => SDL_GL_ExtensionSupported(ToUtf8(name));
    public static void ResetAttributes() => SDL_GL_ResetAttributes();
    public static void SetAttribute(GlAttribute attribute, int value) => Check(SDL_GL_SetAttribute((SDL_GLAttr)attribute, value));
    public static int GetAttribute(GlAttribute attribute) { int v; Check(SDL_GL_GetAttribute((SDL_GLAttr)attribute, &v)); return v; }
    /// <summary>...0 immediate, 1 vsync, -1 adaptive (throws when adaptive unsupported)...</summary>
    public static int SwapInterval
    {
        get { int v; Check(SDL_GL_GetSwapInterval(&v)); return v; }
        set => Check(SDL_GL_SetSwapInterval(value));
    }
}
```

(All documented; adjust signatures to the actual bindings.)

- [ ] **Step 5.4: Window members**: `CreateGlContext()` → `new GlContext(Check(SDL_GL_CreateContext(Handle)))` (owning) + `GlSwap()` → `Check(SDL_GL_SwapWindow(Handle))`, docs noting the window needs `WindowFlags.OpenGL`.

- [ ] **Step 5.5: Build + commit**

```bash
git add src/SdlSharp/Graphics/
git commit -m "Add GlContext, Gl statics, and window OpenGL members"
```

---

### Task 6: Native surface batch

**Files:**
- Modify: `src/SdlSharp/Native/Surface.cs`

- [ ] **Step 6.1: Add `SDL_SurfaceFlags`** as `[Flags] public enum SDL_SurfaceFlags : uint` with the 4 bits, and bind the missing functions (27 bound; derive missing by set-diff of the header's 65). Expected additions (verify each signature): color key trio, ReadSurfacePixel/Float, WriteSurfacePixel/Float, SDL_LoadPNG, SDL_SavePNG, SDL_LoadSurface, SDL_ScaleSurface, SDL_FlipSurface, SDL_RotateSurface, SDL_MapSurfaceRGB, SDL_MapSurfaceRGBA, SDL_CreateSurfacePalette, SDL_SetSurfacePalette, SDL_GetSurfacePalette, SDL_PremultiplySurfaceAlpha, SDL_BlitSurfaceTiled, SDL_BlitSurfaceTiledWithScale, SDL_BlitSurface9Grid, SDL_AddSurfaceAlternateImage, SDL_SurfaceHasAlternateImages, SDL_GetSurfaceImages, SDL_RemoveSurfaceAlternateImages, SDL_BlitSurfaceUnchecked, SDL_BlitSurfaceUncheckedScaled (bind — they're plain functions; managed exposure decided in Task 7), SDL_ConvertSurfaceAndColorspace, SDL_StretchSurface if present (verify).
- [ ] **Step 6.2: Skips with comments + accounting**: IO variants (LoadBMP_IO/SaveBMP_IO/LoadPNG_IO/SavePNG_IO/LoadSurface_IO — .NET), raw pixel-buffer trio (SDL_ConvertPixels, SDL_ConvertPixelsAndColorspace, SDL_PremultiplyAlpha — "operates on raw pixel buffers; Surface-based equivalents cover the managed use cases"), SDL_MUSTLOCK macro (macro — managed property instead). bound + skipped = 65, set-diff proof.
- [ ] **Step 6.3: The 4 `SDL_PROP_SURFACE_*` const strings** (SDR white point, HDR headroom, hotspot x/y — verify names/count in header).
- [ ] **Step 6.4: Build + commit**

```bash
git add src/SdlSharp/Native/Surface.cs
git commit -m "Bind remaining surface functions, flags, and property names"
```

---

### Task 7: Managed surface

**Files:**
- Create: `src/SdlSharp/Graphics/SurfaceFlags.cs`, `src/SdlSharp/Graphics/SurfaceProperties.cs`
- Modify: `src/SdlSharp/Graphics/Surface.cs`

- [ ] **Step 7.1: `SurfaceFlags` public [Flags] enum** (4 members mirroring native) + `SurfaceProperties` const class (4 names).

- [ ] **Step 7.2: Surface members.** Color-key decision rule from the spec, resolved concretely: use uint-mapped keys with Color convenience — `SetColorKey(Color color)` maps via `MapColor(color)`; `SetColorKey(uint key)` raw overload; `ClearColorKey()`; `bool HasColorKey`; `uint? GetColorKey()` (null when none) — no reverse-unmapping to Color (SDL provides no surface-level unmap; document that the raw key is format-dependent). Then:

```csharp
public Color ReadPixel(int x, int y)                     // SDL_ReadSurfacePixel out r,g,b,a bytes; Check
public void WritePixel(int x, int y, Color color)
public FColor ReadPixelFloat(int x, int y)
public void WritePixelFloat(int x, int y, FColor color)
public static Surface Load(string file)                 // SDL_LoadSurface; Check pointer
public static Surface LoadPng(string file)
public void SavePng(string file)
public Surface Scale(int width, int height, ScaleMode scaleMode)   // new owning Surface
public void Flip(FlipMode flip)                          // in place; Check
public Surface Rotate(float angleDegrees)                // new owning Surface; doc: 3.4+ arbitrary-angle rotation, result sized to fit
public uint MapColor(Color color)                        // SDL_MapSurfaceRGBA
public uint MapColorRgb(Color color)                     // SDL_MapSurfaceRGB (alpha ignored)
public Palette CreatePalette()                           // verify ownership per header (surface-owned → non-owning wrapper? header says the palette is owned by the surface → non-owning; read Palette.cs ctor)
public Palette? GetPalette()                             // non-owning, null when none
public void SetPalette(Palette palette)                  // Check
public void PremultiplyAlpha(bool linear)                // Check
public void BlitTiled(Surface destination, Rectangle? sourceRect = null, Rectangle? destinationRect = null)
public void BlitTiledWithScale(Surface destination, float scale, ScaleMode scaleMode, Rectangle? sourceRect = null, Rectangle? destinationRect = null)
public void Blit9Grid(Surface destination, int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, ScaleMode scaleMode, Rectangle? sourceRect = null, Rectangle? destinationRect = null)
public bool MustLock                                     // (flags & LOCK_NEEDED) != 0 — mirror the SDL_MUSTLOCK macro over the Flags property
public SurfaceFlags Flags                                // from the native surface struct field (read how Surface.cs accesses struct fields)
public PropertyGroup Properties                          // non-owning
public Colorspace Colorspace { get; set; }               // existing enum; Check on set
public Surface Convert(PixelFormat format)               // verify: may already exist; add ConvertWithColorspace(PixelFormat, Colorspace, PropertyGroup?) per native
public void AddAlternateImage(Surface image)             // Check
public bool HasAlternateImages
public Surface[] GetImages()                             // ownership per header (returned array SDL_free'd; surfaces themselves ref-counted → verify and document; likely non-owning wrappers + SDL_free of array)
public void RemoveAlternateImages()
```

Blit param order note: managed convention in the existing `Blit` methods — READ them first and mirror (src.Blit(dst, ...) vs dst-first); the natives are src-first. `SDL_BlitSurfaceUnchecked*`: managed exposure SKIPPED (document in INVENTORY as niche/unsafe — no bounds clipping), native-only binding is acceptable per the CLAUDE.md rule only if noted; per phase-8 orphan policy every bound native needs a managed path — so either expose as `BlitUnchecked` with a strong doc warning or don't bind them in Task 6. DECISION: do not bind them in Task 6; skip-comment them (niche/unsafe, clipping Blit covers use cases) — adjust Task 6's list accordingly and keep the accounting straight.

- [ ] **Step 7.3: Build + grep gate + commit**

```bash
git add src/SdlSharp/Graphics/
git commit -m "Complete managed surface: pixels, PNG, transforms, palette, tiled blits"
```

---

### Task 8: Render completion (native + managed)

**Files:**
- Modify: `src/SdlSharp/Native/Render.cs`, `src/SdlSharp/Graphics/Renderer.cs`, `src/SdlSharp/Graphics/Texture.cs`

- [ ] **Step 8.1: Native additions** (set-diff the header's 102 vs the 70 bound; expected): SDL_ConvertEventToRenderCoordinates, SDL_CreateSoftwareRenderer, SDL_GetTextureProperties, SDL_LockTextureToSurface, SDL_UpdateYUVTexture, SDL_UpdateNVTexture, SDL_GetRenderLogicalPresentationRect, SDL_GetRenderSafeArea, SDL_GetGPURendererDevice, SDL_GetRendererFromTexture, SDL_RenderViewportSet, SDL_AddVulkanRenderSemaphores (verify — platform? if Vulkan-tied, SKIP with platform rationale), SDL_GetDefaultTextureScaleMode/SetDefaultTextureScaleMode if present (verify header).
- [ ] **Step 8.2: Skips + accounting**: GPU render-state quartet + SDL_GPURenderState struct (deferred, documented), SDL_GetRenderMetalLayer/SDL_GetRenderMetalCommandEncoder (platform), SDL_RenderDebugTextFormat (variadic). bound + skipped = 102, set-diff proof.
- [ ] **Step 8.3: The missing `SDL_PROP_RENDERER_*`/`SDL_PROP_TEXTURE_*` const strings** — complete to the header's full set (audit says 42/99 exposed; count in header, add missing native consts + extend the existing `PropCreate*`-style members on Renderer.cs/Texture.cs per their established pattern, all documented).
- [ ] **Step 8.4: Managed members**:

```csharp
// Renderer.cs
public static Renderer CreateSoftware(Surface surface)
public void ConvertEventToRenderCoordinates(in RawEvent e)   // Check(SDL_ConvertEventToRenderCoordinates(Handle, (SDL_Event*)e.Pointer)); doc: mutates the event in place; only valid during a RawEventFilter callback (RawEvent.Pointer contract)
public Texture CreateTexture(PropertyGroup properties)       // WithProperties
public FRectangle GetLogicalPresentationRect()               // Check + FromNative (check FRectangle helper)
public Rectangle GetSafeArea()
public bool IsViewportSet                                     // SDL_RenderViewportSet
public GpuDevice GetGpuDevice()                               // non-owning GpuDevice — GpuDevice needs an ownsHandle ctor flag if it lacks one (READ GpuDevice.cs; add the flag per Cursor pattern if missing, Dispose gated)
public void GeometryRaw(Texture? texture, ReadOnlySpan<float> xy, int xyStride, ReadOnlySpan<FColor> colors, int colorStride, ReadOnlySpan<float> uv, int uvStride, int vertexCount, ReadOnlySpan<int> indices)  // match SDL_RenderGeometryRaw's exact params (verify: indices are (const void*, int num_indices, int size_indices) — expose int-index overload passing size 4; pin spans with fixed; Check)
// Texture.cs
public Surface LockToSurface(Rectangle? rect = null)         // non-owning Surface (Surface needs ownsHandle flag — verify; header: surface freed by UnlockTexture, do not free)
public void UpdateYuv(Rectangle? rect, ReadOnlySpan<byte> yPlane, int yPitch, ReadOnlySpan<byte> uPlane, int uPitch, ReadOnlySpan<byte> vPlane, int vPitch)
public void UpdateNv(Rectangle? rect, ReadOnlySpan<byte> yPlane, int yPitch, ReadOnlySpan<byte> uvPlane, int uvPitch)
public PropertyGroup Properties                               // non-owning
public Renderer GetRenderer()                                 // SDL_GetRendererFromTexture, non-owning (Renderer ownsHandle flag — same check)
```

Non-owning flags: this task may need to retrofit `_ownsHandle` into Surface/Renderer/GpuDevice if absent — do it per the Cursor pattern (constructor default true; Dispose gated; always null), each retrofit noted in the report.

- [ ] **Step 8.5: Build + grep gates on Renderer.cs/Texture.cs + commit**

```bash
git add src/SdlSharp/Native/Render.cs src/SdlSharp/Graphics/
git commit -m "Complete render surface: software renderer, event coords, YUV, texture locking"
```

---

### Task 9: SurfaceOps sample + regression gates

**Files:**
- Create: `Samples/SurfaceOps/SurfaceOps.csproj` (copy GpuInfo's csproj shape), `Samples/SurfaceOps/Program.cs`
- Modify: `SdlSharp.slnx` (register like the other samples)

- [ ] **Step 9.1: `Program.cs`** (adjust to real API surface — verify member names from Tasks 3-8 before writing):

```csharp
// SurfaceOps sample — headless exercise of the phase 9 surface API.
// Exercises: pixel read/write, color key, scale, flip, PNG save/reload round-trip.

using SdlSharp;
using SdlSharp.Graphics;

using var app = new Application(InitFlags.Video);

using var surface = Surface.Create(64, 64, PixelFormat.Rgba8888);
surface.WritePixel(10, 10, new Color(255, 0, 0, 255));
var read = surface.ReadPixel(10, 10);
Console.WriteLine($"Pixel round-trip: {read} (red: {read.R == 255})");

surface.SetColorKey(new Color(0, 0, 0, 255));
Console.WriteLine($"Has color key: {surface.HasColorKey}");
surface.ClearColorKey();
Console.WriteLine($"Cleared color key: {!surface.HasColorKey}");

using var scaled = surface.Scale(128, 128, ScaleMode.Nearest);
Console.WriteLine($"Scaled: 64x64 -> {scaled.Width}x{scaled.Height}");

surface.Flip(FlipMode.Horizontal);
var flipped = surface.ReadPixel(53, 10);   // 63 - 10
Console.WriteLine($"Flip moved pixel: {flipped.R == 255}");

var pngPath = Path.Combine(Path.GetTempPath(), $"sdlsharp-surfaceops-{Environment.ProcessId}.png");
surface.SavePng(pngPath);
using var reloaded = Surface.LoadPng(pngPath);
var probe = reloaded.ReadPixel(53, 10);
Console.WriteLine($"PNG round-trip: {probe.R == 255}");
File.Delete(pngPath);

Console.WriteLine($"MustLock: {surface.MustLock}, Flags: {surface.Flags}");
Console.WriteLine("Done.");
```

(Check `Surface.Width`/`Height` exist — read Surface.cs; `PixelFormat.Rgba8888` — verify member name; InitFlags.Video — verify. Every printed check must come out true; if one doesn't, that's a REAL BUG in tasks 6-7 — report BLOCKED.)

- [ ] **Step 9.2: Run gates**: SurfaceOps exit 0 with all-true output; InputInfo, GpuInfo exit 0; ImGuiDemo 10-15s smoke. Report all.

- [ ] **Step 9.3: Commit**

```bash
git add Samples/SurfaceOps/ SdlSharp.slnx
git commit -m "Add SurfaceOps sample exercising the phase 9 surface API"
```

---

### Task 10: INVENTORY.md + TODO.md close-out

**Files:**
- Modify: `INVENTORY.md` (`## SDL_video.h`, `## SDL_surface.h`, `## SDL_render.h`), `TODO.md` (9.1-9.3)

- [ ] **Step 10.1**: Update the three sections — every delivered symbol's cells grep-verified; skips carry settled rationales (EGL/Metal/Vulkan-semaphores → platform; IO variants → .NET; raw pixel-buffer trio → niche with Surface-equivalents note; unchecked blits → niche/unsafe; GPU render-state → niche + own-design note; ICC profile → niche; DebugTextFormat → variadic). Evaluate the ✅ rule per section (GPU render-state deferral is a settled documented skip, so SDL_render.h may earn ✅ — apply the rule honestly and state verdicts).
- [ ] **Step 10.2**: TODO.md 9.1/9.2/9.3 → `[x]`.
- [ ] **Step 10.3**: `dotnet build SdlSharp.slnx --no-incremental` → 0/0, then:

```bash
git add INVENTORY.md TODO.md
git commit -m "Record phase 9 graphics surface in INVENTORY.md; complete phase 9"
```
