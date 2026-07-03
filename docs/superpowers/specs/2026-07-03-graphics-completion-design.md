# Phase 9: Graphics Completion — Design

**Date:** 2026-07-03
**Phase:** 9 (TODO.md, "Phases 7–11: SDL3 Surface Completion")
**Problem:** The three big graphics headers are the largest remaining gaps:
SDL_video.h 48/114 functions bound, SDL_surface.h 27/65, SDL_render.h 70/102.
Missing-symbol source of truth: `audit/sdl3-coverage-2026-07.json` (those three
header entries); the headers themselves win over the audit where they disagree
(phase 8 precedent: the audit under-listed two gamepad functions).

## Conventions (unchanged from phases 7–8)

- Native bindings appended to existing `Native/Video.cs` / `Native/Surface.cs` /
  `Native/Render.cs`; every skipped symbol gets a skip comment; per-file accounting
  (bound + skipped = header total) stated in each task's report.
- Handle wrappers use the guarded-Handle/`_ownsHandle` pattern; non-owning wrappers
  for SDL-owned returns.
- `Common.Check` on failure-reporting calls; documented no-throw contracts where SDL
  returns sentinel values; throw-on-unsupported documented for hardware/driver-
  dependent operations.
- Property-name constants: native `public const string` + public const class
  referencing them (GpuDeviceProperties pattern).
- Doc conventions from phase 8 carry over (both-direction sentinel semantics,
  event-pump notes where SDL requires them).
- Zero-warning solution build per task. IOStream-based variants stay skipped
  (.NET policy). Typed native IDs (`SDL_WindowID`/`SDL_DisplayID`) wrapped/unwrapped
  at call sites per existing file conventions.

## 9.1 Video (SDL_video.h)

### Fullscreen display modes
- `Display.GetFullscreenModes()` → `DisplayMode[]` (native returns an SDL-owned
  array of pointers — copy then `SDL_free` the array per header semantics; verify).
- `Display.GetClosestFullscreenMode(int w, int h, float refreshRate = 0,
  bool includeHighDensityModes = false)` → `DisplayMode`.
- `DisplayMode` gains `internal SDL_DisplayMode ToNative()` (inverse of the existing
  `FromNative`).
- `Window.SetFullscreenMode(DisplayMode? mode)` — null selects borderless
  fullscreen-desktop; non-null selects exclusive fullscreen with that mode
  (doc: mode should come from `Display.GetFullscreenModes`; takes effect on the
  next `SetFullscreen(true)` or immediately if already fullscreen, per SDL docs —
  verify wording against the header).
- `Window.GetFullscreenMode()` → `DisplayMode?` (null when borderless/windowed).

### Window-create and window properties
- Native: all 38 `SDL_PROP_WINDOW_CREATE_*` const strings + the post-create
  `SDL_PROP_WINDOW_*` query names (verify count in header), byte-exact.
- Public `static class WindowProperties` in SdlSharp.Graphics exposing both groups
  (Create* prefix for creation names, bare names for query properties — mirror the
  header's split; document which set is which).
- `Window.Properties` instance getter (non-owning PropertyGroup) if not present.

### Window management batch
- Statics: `Window.GetWindows()` → `Window[]` of non-owning wrappers;
  `Window.FromId(uint)` (verify: native `SDL_GetWindowFromID` — bind if missing).
- Screensaver trio: new small `static class ScreenSaver` in SdlSharp.Graphics with
  `bool Enabled { get; set; }` (get → SDL_ScreenSaverEnabled; set true/false →
  SDL_EnableScreenSaver/SDL_DisableScreenSaver, Check-wrapped).
- `SystemTheme` public enum + `Application.SystemTheme` static property
  (app-environment state, so it lives on Application).
- Display additions: `Orientation`/`NaturalOrientation` (+ `DisplayOrientation`
  enum), `Properties` (non-owning + `DisplayProperties` const class for the HDR
  props), `Display.GetForPoint(Point)`, `Display.GetForRect(Rectangle)`.
- Window instance additions: `MouseGrabbed` get/set, `KeyboardGrabbed` get/set,
  `MouseConfinementRect` get/set (`Rectangle?`, null clears),
  `AlwaysOnTop` get(via flags)/set, `SetAspectRatio(float min, float max)` +
  `GetAspectRatio(out float min, out float max)`, `Sync()`,
  `Opacity` get/set, `PixelFormat` get, `SafeArea` get (`Rectangle`),
  `PixelDensity` get, `DisplayScale` get, `Flags` get (`WindowFlags`),
  `CreatePopup(int offsetX, int offsetY, int w, int h, WindowFlags flags)`
  (owning child window), `Parent` get (non-owning `Window?`) / `SetParent(Window?)`,
  `SetModal(bool)`, `SetFocusable(bool)`, `Show()`/`Hide()` if missing,
  taskbar progress: `ProgressState` public enum + `Window.ProgressState` get/set +
  `Window.ProgressValue` get/set (float 0-1).
- Hit test: `public enum HitTestResult` (Normal, Draggable, ResizeTopLeft, … —
  mirror SDL_HitTestResult); `public delegate HitTestResult HitTestHandler(Window window, Point area)`;
  `Window.SetHitTest(HitTestHandler? handler)` — `UnmanagedCallersOnly` trampoline +
  `GCHandle` stored per-window, freed on replacement, on `SetHitTest(null)`, and in
  `Dispose`. The callback receives the managed `Window` (the non-owning wrapper is
  constructed inside the trampoline or resolved from the userdata — design: userdata
  carries a GCHandle to a small holder object referencing the managed Window and the
  delegate).
- Skips (documented): ICC profile (`SDL_GetWindowICCProfile` — raw blob, niche),
  `SDL_SetWindowsMessageHook`-adjacent platform items if any appear in this header,
  EGL functions (`SDL_EGL_*` — platform), Vulkan/Metal surface creation stays in
  their own skipped headers, `SDL_WINDOW_SURFACE_VSYNC_*` constants → bind if they
  are plain constants for `SDL_SetWindowSurfaceVSync` (bind the vsync pair —
  verify in header; audit lists them).

### OpenGL (per decision)
- Native: `SDL_GLContext` opaque, `SDL_GL_CreateContext`, `SDL_GL_MakeCurrent`,
  `SDL_GL_GetCurrentContext`, `SDL_GL_GetCurrentWindow`, `SDL_GL_DestroyContext`,
  `SDL_GL_SwapWindow`, `SDL_GL_SetAttribute`/`GetAttribute`/`ResetAttributes`,
  `SDL_GL_SetSwapInterval`/`GetSwapInterval`, `SDL_GL_GetProcAddress`,
  `SDL_GL_ExtensionSupported`, `SDL_GL_LoadLibrary`/`UnloadLibrary`,
  `SDL_GLAttr` enum (native + public `GlAttribute`), the `SDL_GLProfile`/
  `SDL_GLContextFlag`/`SDL_GLContextReleaseFlag`/`SDL_GLContextResetNotification`
  constant groups as public enums (verify their form in the header — they are
  typedef'd flag values).
- Public: `sealed class GlContext : IDisposable` (guarded handle, owning;
  `MakeCurrent(Window)`, static `GlContext? Current`, static `Window? CurrentWindow`);
  `Window.CreateGlContext()` → GlContext; `Window.GlSwap()`;
  `static class Gl` — `SetAttribute(GlAttribute, int)`, `GetAttribute(GlAttribute)`,
  `ResetAttributes()`, `SwapInterval` get/set (int; -1 adaptive documented),
  `GetProcAddress(string)` → `nint`, `ExtensionSupported(string)`,
  `LoadLibrary(string?)`, `UnloadLibrary()`.
- EGL subset of SDL_video.h skipped with comments (platform).

## 9.2 Surface (SDL_surface.h)

- Color key: `SetColorKey(Color?)` (null clears; maps via the surface's format),
  `GetColorKey()` → `Color?` (null when none), `HasColorKey` — decide exact shape
  against natives (`SDL_SetSurfaceColorKey(bool enabled, uint key)` — the managed
  API takes `Color?` and maps internally via `MapColor`; GetColorKey unmaps via
  `SDL_GetRGBA` on the surface's format — verify feasibility; if unmapping needs
  format-details plumbing that doesn't exist, fall back to uint-based
  `SetColorKey(uint?)`/`GetColorKey()` → `uint?` and note it).
- Pixels: `ReadPixel(int x, int y)` → `Color`, `WritePixel(int x, int y, Color)`,
  `ReadPixelFloat` → `FColor`, `WritePixelFloat(FColor)`.
- Loading/saving: `static Surface Load(string path)` (`SDL_LoadSurface`),
  `static Surface LoadPng(string path)`, `SavePng(string path)`
  (existing Bmp pair as naming precedent — read it and match).
- Transforms: `Scale(int w, int h, ScaleMode)` → new Surface,
  `Flip(FlipMode)` in place, `Rotate(...)` — VERIFY the native signature
  (`SDL_RotateSurface` — SDL 3.4; degrees? quarter turns? returns new surface?)
  and shape the managed API accordingly.
- Mapping: `MapColor(Color)` → uint (`SDL_MapSurfaceRGBA`; provide RGB overload).
- Palette: `CreatePalette()` → Palette (owning per SDL semantics — verify),
  `Palette` get (non-owning `Palette?`) / `SetPalette(Palette)`.
- `PremultiplyAlpha(bool linear)` (in place) + static
  `PremultiplyAlpha(...)`-style raw variant only if audit lists SDL_PremultiplyAlpha
  (pixel-buffer version — skip with .NET-ish rationale if awkward; verify).
- Blits: `BlitTiled(Surface, Rectangle?, Rectangle?)`,
  `BlitTiledWithScale(...)`, `Blit9Grid(...)` — mirror native params
  (verify exact signatures in header).
- Properties: `MustLock` get, `Flags` get (public `SurfaceFlags` enum — verify
  native enum/constants), `Properties` (non-owning) + `SurfaceProperties` const
  class (SDR white point, HDR headroom, hotspot x/y), `Colorspace` get/set
  (existing public Colorspace enum).
- Alternate images (if in audit/header): `AddAlternateImage(Surface)`,
  `HasAlternateImages`, `GetImages()` → `Surface[]` (ownership per header — verify),
  `RemoveAlternateImages()`.
- `ConvertSurface(PixelFormat)` / `Convert(PixelFormat, Colorspace, PropertyGroup?)`
  variants per audit (`SDL_ConvertSurface` may already be bound — check; add the
  AndColorspace variant).
- Skips: `SDL_LoadPNG_IO`/`SDL_SavePNG_IO`/`SDL_LoadBMP_IO`/`SDL_SaveBMP_IO`/
  `SDL_LoadSurface_IO` (.NET streams policy), `SDL_ConvertPixels`/
  `SDL_ConvertPixelsAndColorspace`/`SDL_PremultiplyAlpha` raw-pointer pixel-buffer
  functions IF judged too raw for the managed layer (decide at plan time: bind
  native-only with skip note on managed exposure, or full skip — prefer full skip
  with rationale "operates on raw pixel buffers; Surface-based equivalents cover
  the managed use cases").

## 9.3 Render (SDL_render.h)

- `Renderer.ConvertEventToRenderCoordinates(in RawEvent e)` — mutates the native
  event in place through `e.Pointer` (doc: only valid inside a RawEventFilter
  callback, mirrors the RawEvent.Pointer validity contract).
- `static Renderer CreateSoftware(Surface surface)`.
- `CreateTexture(PropertyGroup)` (WithProperties) + complete the
  `TextureProperties`/`RendererProperties` const classes to the full 99 names
  (audit: only 42 exposed — verify counts against header; native consts + public
  const classes per pattern; if existing const classes live on Renderer/Texture
  as PropCreate*-prefixed members, FOLLOW THAT EXISTING PATTERN instead of new
  classes — read the current code first, precedent wins).
- `RenderGeometryRaw` managed overload: span-based
  (`ReadOnlySpan<float> xy, ReadOnlySpan<FColor> colors, ReadOnlySpan<float> uv,
  ReadOnlySpan<int> indices` — mirror native strides/params; verify signature and
  design the safest span mapping; document stride semantics).
- `UpdateYuvTexture(Rectangle?, ReadOnlySpan<byte> y, int yPitch, …)` and
  `UpdateNvTexture(...)` per native params.
- `LockTextureToSurface(Rectangle?)` → non-owning `Surface` +
  `UnlockTexture()` pairing (verify existing Lock API shape in Texture.cs and
  match).
- `GetLogicalPresentationRect()` → `FRectangle`, `GetSafeArea()` → `Rectangle`,
  render scale pair, `GetRenderMetalLayer`/`CommandEncoder` SKIPPED (platform),
  `SDL_RenderDebugText`/`SDL_RenderDebugTextFormat` — bind DebugText (useful,
  trivial); skip the Format variant (variadic).
- `Renderer.GetGpuDevice()` → non-owning `GpuDevice` (`SDL_GetGPURendererDevice`,
  already bound natively per the 7.2 audit note — verify).
- GPU render-state quartet (`SDL_CreateGPURenderState`, `SDL_SetGPURenderState`,
  `SDL_SetGPURenderStateFragmentUniforms`, `SDL_DestroyGPURenderState` + struct)
  DEFERRED with documented skip (niche custom-shader 2D interop; own mini-design
  if wanted).
- Any remaining audit-listed functions (vsync pair, viewport-set checks, etc.):
  bind unless a settled skip rationale applies; per-file accounting must close.

## Error handling

Unchanged. Hit-test trampoline must be exception-safe: managed exceptions must not
propagate across the native boundary (catch-all → return Normal, matching how other
callback wrappers in the codebase handle it — verify SdlLog/Assert precedent).

## Verification

1. Zero-warning `dotnet build SdlSharp.slnx` per task.
2. Grep gates on the touched public files (body calls / const initializers excepted).
3. Per-file accounting for the three headers (bound + skipped = 114 / 65 / 102).
4. New headless `Samples/SurfaceOps` console sample: create an in-memory surface,
   write/read pixels, set/get color key, scale, save PNG to a temp path, reload,
   compare a probe pixel; exit 0. Registered in SdlSharp.slnx.
5. ImGuiDemo smoke (regression) + GpuInfo + InputInfo (regression).
6. INVENTORY.md: SDL_video.h / SDL_surface.h / SDL_render.h sections updated,
   ✅ rule evaluated per section; TODO.md 9.1–9.3 checked off.
