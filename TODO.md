# SdlSharp SDL3 Wrapper — TODO

Architecture rules and code patterns are in `CLAUDE.md`.
Per-API tracking is in `INVENTORY.md` (SdlSharp) and `src/SdlSharp.ImGui/INVENTORY.md` (ImGui).

## What's Done

### Phase 1: Foundation ✅

All files compile clean (0 warnings, 0 errors) with `dotnet build`.

#### Native layer (`src/SdlSharp/Native/`)

| File | Wraps | Contents |
|------|-------|----------|
| `Common.cs` | — | `Sdl3 = "SDL3"` library name constant |
| `Error.cs` | `SDL_error.h` | `SDL_GetError()` → `byte*`, `SDL_ClearError()` → `bool` |
| `Init.cs` | `SDL_init.h` | `SDL_InitFlags` enum, `SDL_Init`, `SDL_InitSubSystem`, `SDL_QuitSubSystem`, `SDL_WasInit`, `SDL_Quit`, `SDL_IsMainThread`, `SDL_SetAppMetadata`, `SDL_SetAppMetadataProperty`, `SDL_GetAppMetadataProperty`, all `SDL_PROP_APP_METADATA_*` constants |
| `Properties.cs` | `SDL_properties.h` | `SDL_PropertyType` enum, full CRUD: `SDL_CreateProperties`, `SDL_DestroyProperties`, `SDL_CopyProperties`, `SDL_LockProperties`, `SDL_UnlockProperties`, typed Set/Get for Pointer/String/Number/Float/Boolean, `SDL_HasProperty`, `SDL_GetPropertyType`, `SDL_ClearProperty`, `SDL_EnumerateProperties` (with unmanaged function pointer callback), `SDL_SetPointerPropertyWithCleanup`, `SDL_PROP_NAME_STRING` |

#### High-level wrappers (`src/SdlSharp/`)

| File | Purpose |
|------|---------|
| `SdlException.cs` | Captures `SDL_GetError()` automatically, clears error after reading |
| `Sdl.cs` | Static `Check(bool)`, `Check<T>(T*)`, `CheckId(uint)` helpers |
| `PropertyGroup.cs` | Full managed wrapper for `SDL_PropertiesID` — owns/borrows handle, typed get/set, `GetNames()` via `[UnmanagedCallersOnly]` enumerate callback, `IDisposable` |
| `InitFlags.cs` | Public `InitFlags` enum wrapping `SDL_InitFlags`, members expressed as `(uint)Native.SDL_InitFlags.Audio` etc. |
| `PropertyType.cs` | Public `PropertyType` enum wrapping `SDL_PropertyType`, members expressed as `(int)Native.SDL_PropertyType.Invalid` etc. |
| `Application.cs` | Init/quit lifecycle, `SetMetadata`/`GetMetadataProperty`, `IsMainThread`, `WasInit`, `InitSubSystem`, `QuitSubSystem` — uses `InitFlags` (not native type) |

### Phase 2: Video + Rendering ✅

#### Native layer (`src/SdlSharp/Native/`)

| File | Wraps | Contents |
|------|-------|----------|
| `Rect.cs` | `SDL_rect.h` | `SDL_Point`, `SDL_FPoint`, `SDL_Rect`, `SDL_FRect` structs, rect utility functions |
| `Pixels.cs` | `SDL_pixels.h` | `SDL_PixelFormat`, `SDL_ColorType`, `SDL_ColorRange`, `SDL_Colorspace` enums, `SDL_Color`, `SDL_FColor`, `SDL_Palette` structs, pixel format functions |
| `BlendMode.cs` | `SDL_blendmode.h` | `SDL_BlendMode`, `SDL_BlendOperation`, `SDL_BlendFactor` enums, `SDL_ComposeCustomBlendMode` |
| `Surface.cs` | `SDL_surface.h` | `SDL_Surface` struct, surface creation/destruction, BMP load/save, blit, fill |
| `Video.cs` | `SDL_video.h` | `SDL_Window` opaque, `SDL_WindowID`/`SDL_DisplayID` typed IDs, `SDL_WindowFlags`, `SDL_DisplayMode`, window/display management functions |
| `Render.cs` | `SDL_render.h` | `SDL_Renderer`/`SDL_Texture` opaque, `SDL_Vertex`, `SDL_TextureAccess`, renderer creation, drawing primitives, viewport, clipping |

#### High-level wrappers (`src/SdlSharp/Graphics/`)

| File | Purpose |
|------|---------|
| `Color.cs` | `readonly record struct Color(byte R, byte G, byte B, byte A)` |
| `FColor.cs` | `readonly record struct FColor(float R, float G, float B, float A)` |
| `Point.cs` | `readonly record struct Point(int X, int Y)` |
| `FPoint.cs` | `readonly record struct FPoint(float X, float Y)` |
| `Rectangle.cs` | `readonly record struct Rectangle(int X, int Y, int W, int H)` |
| `FRectangle.cs` | `readonly record struct FRectangle(float X, float Y, float W, float H)` |
| `Size.cs` | `readonly record struct Size(int W, int H)` |
| `Display.cs` | Display info wrapper |
| `DisplayMode.cs` | Display mode record struct |
| `Window.cs` | `sealed unsafe class Window : IDisposable`, wraps `SDL_Window*` |
| `Surface.cs` | `sealed unsafe class Surface : IDisposable`, wraps `SDL_Surface*` |
| `Renderer.cs` | `sealed unsafe class Renderer : IDisposable`, wraps `SDL_Renderer*`, drawing methods |
| `Texture.cs` | `sealed unsafe class Texture : IDisposable`, wraps `SDL_Texture*` |
| `Vertex.cs` | Vertex record struct |
| `PixelFormat.cs` | Pixel format enum |
| `BlendMode.cs` | Public `BlendMode` enum wrapping `SDL_BlendMode` |
| `BlendFactor.cs` | Public `BlendFactor` enum wrapping `SDL_BlendFactor` |
| `BlendOperation.cs` | Public `BlendOperation` enum wrapping `SDL_BlendOperation` |
| `WindowFlags.cs` | Public `WindowFlags` enum |
| `FlashOperation.cs` | Flash operation enum |
| `ScaleMode.cs` | Scale mode enum |
| `FlipMode.cs` | Flip mode enum |
| `LogicalPresentation.cs` | Logical presentation enum |
| `TextureAccess.cs` | Texture access enum |
| `TextureAddressMode.cs` | Texture address mode enum |
| `Colorspace.cs` | Colorspace enum |
| `Palette.cs` | Palette wrapper |
| `PixelFormatDetails.cs` | Pixel format details wrapper |

## What's Next

### Phase 3: Events + Input ✅

#### Native layer (`src/SdlSharp/Native/`) ✅

| File | Wraps | Contents |
|------|-------|----------|
| `Events.cs` | `SDL_events.h` | `SDL_EventType` enum, `SDL_Event` union (`[StructLayout(LayoutKind.Explicit)]`), all event structs (`SDL_CommonEvent`, `SDL_KeyboardEvent`, `SDL_MouseMotionEvent`, `SDL_MouseButtonEvent`, `SDL_MouseWheelEvent`, `SDL_JoyAxisEvent`, `SDL_GamepadAxisEvent`, etc.), `SDL_PowerState` enum, `SDL_PollEvent`, `SDL_WaitEvent`, `SDL_WaitEventTimeout`, `SDL_PushEvent`, `SDL_RegisterEvents`, `SDL_HasEvent`, `SDL_FlushEvent`, `SDL_SetEventEnabled`, `SDL_EventEnabled` |
| `Scancode.cs` | `SDL_scancode.h` | `SDL_Scancode` enum (all ~200 values) |
| `Keycode.cs` | `SDL_keycode.h` | `SDL_Keycode` enum (all values), `SDL_Keymod` flags enum |
| `Keyboard.cs` | `SDL_keyboard.h` | `SDL_HasKeyboard`, `SDL_GetKeyboards`, `SDL_GetKeyboardState`, `SDL_GetModState`, `SDL_SetModState`, `SDL_GetKeyFromScancode`, `SDL_GetScancodeFromKey`, scancode/key name functions, text input start/stop/active/clear |
| `Mouse.cs` | `SDL_mouse.h` | `SDL_Cursor` opaque, `SDL_SystemCursor`/`SDL_MouseWheelDirection` enums, mouse state queries, warp, relative mode, capture, cursor create/destroy/show/hide, button constants |
| `Joystick.cs` | `SDL_joystick.h` | `SDL_Joystick` opaque, `SDL_JoystickID` typed ID, `SDL_JoystickType`/`SDL_JoystickConnectionState` enums, hat constants, open/close, axis/hat/button queries |
| `Gamepad.cs` | `SDL_gamepad.h` | `SDL_Gamepad` opaque, `SDL_GamepadType`/`SDL_GamepadButton`/`SDL_GamepadButtonLabel`/`SDL_GamepadAxis` enums, open/close, axis/button queries, button labels, connection state |
| `Touch.cs` | `SDL_touch.h` | `SDL_TouchDeviceType` enum, `SDL_Finger` struct, device enumeration, finger queries |
| `Pen.cs` | `SDL_pen.h` | `SDL_PenAxis`/`SDL_PenDeviceType` enums, pen input flag constants, `SDL_GetPenDeviceType` |
| `Sensor.cs` | `SDL_sensor.h` | `SDL_Sensor` opaque, `SDL_SensorType` enum, sensor enumeration, open/close, data queries |

#### High-level wrappers (`src/SdlSharp/Input/`)

| File | Purpose |
|------|---------|
| `Scancode.cs` | Public `Scancode` enum (physical key codes), wrapping `SDL_Scancode` |
| `Keycode.cs` | Public `Keycode` enum (virtual key codes) + `KeyModifiers` flags, wrapping `SDL_Keycode`/`SDL_Keymod` |
| `MouseButton.cs` | Public `MouseButton` enum |
| `EventArgs.cs` | `readonly record struct` event args: `KeyEventArgs`, `TextInputEventArgs`, `MouseMotionEventArgs`, `MouseButtonEventArgs`, `MouseWheelEventArgs`, `WindowEventArgs`, `QuitEventArgs` |
| `Keyboard.cs` | Static class: key state queries, mod state, scancode/key names |
| `Mouse.cs` | Static class: mouse state, warp, cursor show/hide, capture |

**Event dispatch** added to `Application.cs`:
- Static C# events: `Quit`, `KeyDown`, `KeyUp`, `TextInput`, `MouseMotion`, `MouseButtonDown`, `MouseButtonUp`, `MouseWheel`, `Window`
- `DispatchEvents()` method: polls all pending events via `SDL_PollEvent`, dispatches to handlers, returns `true` on quit

**High-level wrappers** (all completed in Phase 5c):
- `Input/Gamepad.cs` ✅ — sealed class wrapping `SDL_Gamepad*`
- `Input/Joystick.cs` ✅ — sealed class wrapping `SDL_Joystick*`
- `Input/TouchDevice.cs` ✅ — static class: touch device and finger queries
- `Input/PenDevice.cs` ✅ — static class: pen device type query
- `Input/Sensor.cs` ✅ — sealed class wrapping `SDL_Sensor*`

### Phase 4: Audio ✅

#### Native layer (`src/SdlSharp/Native/`)

| File | Wraps | Contents |
|------|-------|----------|
| `Audio.cs` | `SDL_audio.h` | `SDL_AudioFormat` enum, `SDL_AudioDeviceID` typed ID, `SDL_AudioSpec` struct, `SDL_AudioStream` opaque, device defaults, driver enumeration, device open/close/pause/resume/gain, stream create/destroy/bind/unbind, stream data put/get/available/queued/flush/clear, stream format/frequency ratio/gain, stream device pause/resume, WAV loading, format name query |

#### High-level wrappers (`src/SdlSharp/Audio/`)

| File | Purpose |
|------|---------|
| `AudioFormat.cs` | Public `AudioFormat` enum wrapping `SDL_AudioFormat` |
| `AudioSpec.cs` | `readonly record struct AudioSpec(AudioFormat Format, int Channels, int Frequency)` |
| `AudioDevice.cs` | `sealed unsafe class AudioDevice : IDisposable` — open/close playback/recording devices, enumerate devices/drivers, pause/resume, gain control, bind streams |
| `AudioStream.cs` | `sealed unsafe class AudioStream : IDisposable` — create streams with format conversion, simplified `OpenDevice` for common case, put/get data via `Span<byte>`, frequency ratio, gain, flush/clear, device pause/resume |

### Phase 5a: Small Subsystems ✅

#### Native layer (`src/SdlSharp/Native/`)

| File | Wraps | Contents |
|------|-------|----------|
| `Misc.cs` | `SDL_misc.h` | `SDL_OpenURL` |
| `Power.cs` | `SDL_power.h` | `SDL_GetPowerInfo` (enum is in `Events.cs`) |
| `CpuInfo.cs` | `SDL_cpuinfo.h` | CPU core count, cache line size, SIMD feature detection (SSE/AVX/NEON/etc.), system RAM, page size |
| `Locale.cs` | `SDL_locale.h` | `SDL_Locale` struct, `SDL_GetPreferredLocales` |
| `Timer.cs` | `SDL_timer.h` | `SDL_GetTicks`, `SDL_GetTicksNS`, performance counter, `SDL_Delay`, `SDL_DelayNS`, `SDL_DelayPrecise` |
| `Clipboard.cs` | `SDL_clipboard.h` | `SDL_SetClipboardText`, `SDL_GetClipboardText`, `SDL_HasClipboardText`, `SDL_ClipboardDataCallback`, `SDL_ClipboardCleanupCallback`, `SDL_SetClipboardData`, `SDL_ClearClipboardData`, `SDL_GetClipboardData`, `SDL_HasClipboardData`, `SDL_GetClipboardMimeTypes` |
| `Log.cs` | `SDL_log.h` | `SDL_LogCategory`/`SDL_LogPriority` enums, priority get/set/reset, `SDL_SetLogOutputFunction` callback |
| `Time.cs` | `SDL_time.h` | `SDL_DateFormat`/`SDL_TimeFormat` enums, `SDL_DateTime` struct, locale preferences, current time, date/time conversion |
| `Hints.cs` | `SDL_hints.h` | `SDL_HintPriority` enum, hint set/get/reset functions |
| `FileSystem.cs` | `SDL_filesystem.h` | `SDL_Folder` enum, `SDL_GetBasePath`, `SDL_GetPrefPath`, `SDL_GetUserFolder`, `SDL_CreateDirectory`, `SDL_GetCurrentDirectory` |
| `MessageBox.cs` | `SDL_messagebox.h` | `SDL_MessageBoxFlags` enum, `SDL_ShowSimpleMessageBox` |
| `Dialog.cs` | `SDL_dialog.h` | `SDL_DialogFileFilter` struct, `SDL_FileDialogType` enum, open/save/folder dialog functions |
| `Camera.cs` | `SDL_camera.h` | `SDL_CameraID`, `SDL_Camera` opaque, `SDL_CameraSpec`, `SDL_CameraPosition`, device enumeration, open/close, frame acquire/release |
| `Haptic.cs` | `SDL_haptic.h` | `SDL_HapticID`, `SDL_Haptic` opaque, device enumeration, open/close, simple rumble API |
| `Assert.cs` | `SDL_assert.h` | `SDL_AssertState` enum, `SDL_AssertData` struct, `SDL_SetAssertionHandler` (function pointer callback), `SDL_GetDefaultAssertionHandler`, `SDL_GetAssertionHandler`, `SDL_GetAssertionReport`, `SDL_ResetAssertionReport` |

#### High-level wrappers

| File | Purpose |
|------|---------|
| `PowerState.cs` | Public `PowerState` enum |
| `PowerInfo.cs` | `readonly record struct PowerInfo` — state, battery seconds/percent |
| `SystemInfo.cs` | Static class — CPU features, RAM, paths (`BasePath`, `GetPrefPath`, `GetUserFolder`, `GetCurrentDirectory`), `OpenUrl` |
| `SystemFolder.cs` | Public `SystemFolder` enum |
| `SdlTimer.cs` | Static class — ticks, performance counter, delay |
| `Clipboard.cs` | Static class — text get/set, `HasText`, MIME data get/set/has/clear, `GetMimeTypes`, `ClipboardDataProvider` callback |
| `LogCategory.cs` | Public `LogCategory` enum |
| `LogPriority.cs` | Public `LogPriority` enum |
| `SdlLog.cs` | Static class — priority get/set, managed output callback via `[UnmanagedCallersOnly]` |
| `SdlHints.cs` | Static class — hint set/get/reset |
| `LocaleInfo.cs` | `readonly record struct LocaleInfo` — language, country, `GetPreferred()` |
| `MessageBoxType.cs` | Public `MessageBoxType` enum |
| `Graphics/MessageBox.cs` | Static class — `Show()` simple message box with optional parent window |
| `AssertState.cs` | Public `AssertState` enum wrapping `SDL_AssertState` |
| `AssertionData.cs` | `readonly record struct AssertionData` — condition, filename, line, function, trigger count |
| `SdlAssert.cs` | Static class — `SetAssertionHandler` (managed callback via `[UnmanagedCallersOnly]`/`GCHandle`), `GetAssertionReport`, `ResetAssertionReport` |

**Deferred subsystems** (native API available in SDL but not wrapped — C# has better alternatives or they're too niche):
- `SDL_iostream.h` — C# has `System.IO.Stream`
- `SDL_storage.h` — C# has `System.IO`
- `SDL_process.h` — C# has `System.Diagnostics.Process`
- `SDL_hidapi.h` — niche, `wchar_t` complexity
- `SDL_thread.h` / `SDL_mutex.h` / `SDL_atomic.h` — C# has `System.Threading`
- SDL callback-based timers — C# has `System.Threading.Timer`
- Full haptic effect system — complex struct unions, rumble API covers common case
- Primary selection text (`SDL_SetPrimarySelectionText`, `SDL_GetPrimarySelectionText`, `SDL_HasPrimarySelectionText`) — X11/Wayland only, niche
- File dialog properties variant — specific dialog functions suffice

### Phase 5c: Additional Subsystems ✅

#### Audio additions (`src/SdlSharp/Audio/`)

| File | Purpose |
|------|---------|
| `WavData.cs` | `readonly record struct WavData(AudioSpec Spec, byte[] Data)` — `WavData.Load(path)` wraps `SDL_LoadWAV` |
| `AudioFormatInfo.cs` | Static class — `AudioFormatInfo.GetName(AudioFormat)` wraps `SDL_GetAudioFormatName` |

#### DateTime/Time (`src/SdlSharp/`)

| File | Purpose |
|------|---------|
| `DateFormat.cs` | Public `DateFormat` enum wrapping `SDL_DateFormat` |
| `TimeFormat.cs` | Public `TimeFormat` enum wrapping `SDL_TimeFormat` |
| `SdlDateTime.cs` | Static class — locale date/time format preferences, `GetCurrentTime`, `ToDateTime`, `FromDateTime` bridging SDL nanosecond ticks to .NET `DateTime` |

#### Input devices (`src/SdlSharp/Input/`)

| File | Purpose |
|------|---------|
| `JoystickType.cs` | Public `JoystickType` enum |
| `JoystickConnectionState.cs` | Public `JoystickConnectionState` enum |
| `HatPosition.cs` | Public `HatPosition` flags enum (wraps `SDL_HAT_*` constants) |
| `Joystick.cs` | `sealed unsafe class Joystick : IDisposable` — open/close, axes/hats/buttons, connection state |
| `GamepadType.cs` | Public `GamepadType` enum |
| `GamepadButton.cs` | Public `GamepadButton` enum |
| `GamepadButtonLabel.cs` | Public `GamepadButtonLabel` enum |
| `GamepadAxis.cs` | Public `GamepadAxis` enum |
| `Gamepad.cs` | `sealed unsafe class Gamepad : IDisposable` — open/close, axis/button/label queries, connection state |
| `Haptic.cs` | `sealed unsafe class Haptic : IDisposable` — open/close, open from mouse/joystick, simple rumble API |
| `SensorType.cs` | Public `SensorType` enum |
| `Sensor.cs` | `sealed unsafe class Sensor : IDisposable` — open/close, name/type/data queries, properties |
| `TouchDeviceType.cs` | Public `TouchDeviceType` enum |
| `Finger.cs` | `readonly record struct Finger(long Id, float X, float Y, float Pressure)` |
| `TouchDevice.cs` | Static class — device enumeration, name/type queries, finger queries |
| `PenAxis.cs` | Public `PenAxis` enum |
| `PenDeviceType.cs` | Public `PenDeviceType` enum |
| `PenInput.cs` | Public `PenInput` flags enum (wraps `SDL_PEN_INPUT_*` constants) |
| `PenDevice.cs` | Static class — `PenDevice.GetType(id)` |

#### Camera (`src/SdlSharp/Graphics/`)

| File | Purpose |
|------|---------|
| `CameraPosition.cs` | Public `CameraPosition` enum |
| `CameraSpec.cs` | `readonly record struct CameraSpec` — pixel format, colorspace, size, framerate |
| `Camera.cs` | `sealed unsafe class Camera : IDisposable` — open/close, device/driver enumeration, format queries, frame acquire/release |

#### File Dialogs (`src/SdlSharp/Graphics/`)

| File | Purpose |
|------|---------|
| `FileDialogType.cs` | Public `FileDialogType` enum |
| `FileDialog.cs` | Static class — `OpenFile`, `SaveFile`, `OpenFolder` (callback + async `Task<DialogResult>` variants) |

### Phase 5b: GPU ✅

#### Native layer (`src/SdlSharp/Native/`)

| File | Wraps | Contents |
|------|-------|----------|
| `Gpu.cs` | `SDL_gpu.h` | 14 opaque types, 23 enums (incl. flag enums), 32 structs, 100+ functions: device creation/destruction, pipeline/shader/sampler creation, resource (texture/buffer/transfer buffer) creation/release, command buffer acquisition/submission, render pass (begin/end, bindpipeline/buffers/samplers, draw/draw indexed/indirect), compute pass (begin/end, bind/dispatch), copy pass (upload/download/copy), swapchain (claim/release window, acquire texture, present modes), fence synchronization, format queries, debug naming |

#### High-level wrappers (`src/SdlSharp/Graphics/Gpu/`)

| File | Purpose |
|------|---------|
| `GpuEnums.cs` | Public enums: `GpuShaderFormat`, `GpuPresentMode`, `GpuSwapchainComposition`, `GpuTextureFormat` (100+ formats), `GpuTextureType`, `GpuTextureUsage`, `GpuSampleCount`, `GpuPrimitiveType`, `GpuIndexElementSize`, `GpuBufferUsage` |
| `GpuDevice.cs` | `sealed unsafe class GpuDevice : IDisposable` — create device, driver queries, resource/pipeline factory methods, swapchain management, format support queries |
| `GpuCommandBuffer.cs` | `sealed unsafe class GpuCommandBuffer` — begin render/compute/copy passes, push uniforms, acquire swapchain texture, submit/cancel, debug labels |
| `GpuRenderPass.cs` | `sealed unsafe class GpuRenderPass` — bind pipeline/buffers/samplers, set viewport/scissor/blend/stencil, draw/draw indexed/indirect |
| `GpuComputePass.cs` | `sealed unsafe class GpuComputePass` — bind pipeline/storage, dispatch/dispatch indirect |
| `GpuCopyPass.cs` | `sealed unsafe class GpuCopyPass` — upload/download/copy textures and buffers |
| `GpuBuffer.cs` | `sealed unsafe class GpuBuffer : IDisposable` — GPU buffer with debug naming |
| `GpuTexture.cs` | `sealed unsafe class GpuTexture : IDisposable` — GPU texture with debug naming |
| `GpuTransferBuffer.cs` | `sealed unsafe class GpuTransferBuffer : IDisposable` — map/unmap for CPU access |
| `GpuSampler.cs` | `sealed unsafe class GpuSampler : IDisposable` — sampler state |
| `GpuShader.cs` | `sealed unsafe class GpuShader : IDisposable` — compiled shader |
| `GpuGraphicsPipeline.cs` | `sealed unsafe class GpuGraphicsPipeline : IDisposable` — graphics pipeline |
| `GpuComputePipeline.cs` | `sealed unsafe class GpuComputePipeline : IDisposable` — compute pipeline |
| `GpuFence.cs` | `sealed unsafe class GpuFence : IDisposable` — fence with `IsSignaled` query |

### Phase 6: Dear ImGui Integration ✅

Separate project `SdlSharp.ImGui` wrapping Dear ImGui v1.92.7 via a hand-written C
wrapper (sibling repo `../imgui-sharp-native/`, shipped as `ImguiSharp.Redist` on
nuget.org — currently `0.2.0-preview.2`).

#### Native C wrapper coverage (sibling repo)

- Core widgets, layout, popups/menus/tooltips, tables (incl. sort specs and column metadata)
- Drag, slider, input, color, tree, tab, selectable, radio, list-box, image variants
- Generic scalar widgets (`DragScalar`/`SliderScalar`/`InputScalar` + N-component / VSlider)
- DrawList (primitives, path builder, images, clipping)
- Fonts (TTF file / memory / compressed memory, push/pop, default)
- ListClipper, Viewport
- Full `ImGuiIO` field access + event queue injection (`AddKeyEvent`, `AddMousePosEvent`, etc.)
- Full `ImGuiStyle` field access
- InputText callbacks with full `InputTextCallbackData` access incl. resize helpers
  (`SetBuf`, `SetBufSize`, `ResizeBuf`)
- Plot widgets (PlotLines / PlotHistogram, array + callback)
- Drag-and-drop (source/target + payload accessors)
- Multi-select (`MultiSelectIO` / `SelectionRequest` accessors)
- Scrolling (`Get/SetScroll{X,Y}`, `SetScrollHere{X,Y}`, `SetScrollFromPos{X,Y}`, `GetScrollMax{X,Y}`)
- Item utilities (`GetItemID`, `IsAnyItem*`), focus (`SetKeyboardFocusHere`, `SetNextItemAllowOverlap`)
- Mouse cursor (`Get/SetMouseCursor`), item flags (`Push/PopItemFlag`)
- Window manipulation (`SetNextWindowSizeConstraints`, `ContentSize`, `Scroll`)
- Demo / debug windows (Demo, Metrics, DebugLog, IDStackTool, About, StyleEditor,
  StyleSelector, FontSelector, UserGuide)
- Color utilities (`GetColorU32`, `ColorConvert*`)
- SDL3 platform + SDL_GPU renderer backend wrappers
- CI/CD across win-x64/x86/arm64 and osx-x64/arm64; nuget.org publish via OIDC

#### C# managed surface (this repo)

- `SdlSharp.ImGui.Native.ImGui` / `ImGuiBackend` — full P/Invoke layer (~620 entry points)
- `SdlSharp.Gui.Gui` — static API mirroring `ImGui::*`
- `SdlSharp.Gui.Io` — IO accessors (display size, delta time, mouse/keyboard state,
  metrics, timing tunables, event-queue injection)
- `SdlSharp.Gui.Style` — per-field getters/setters for the ImGui style struct
- Handle structs: `DrawList`, `Viewport`, `Font`, `FontAtlas`, `DragDropPayload`,
  `MultiSelectIO`, `SelectionRequest`, `TableSortSpecs`, `TableColumnSortSpecs`,
  `InputTextCallbackData`
- IDisposable wrappers: `GuiContext` (with `MakeCurrent`/`IsCurrent`), `ListClipper`
- Public types: `Vec2`, `Vec4` (layout-compatible with ImVec2/ImVec4)
- Delegates: `InputTextCallback`, `PlotValuesGetter`
- Generic scalar widgets: `Gui.Drag<T>`, `Slider<T>`, `VSlider<T>`, `Input<T>`
  (any unmanaged numeric: sbyte/byte/short/ushort/int/uint/long/ulong/float/double),
  with N-component `Span<T>` overloads
- Auto-growing managed-string `InputText` / `InputTextMultiline` / `InputTextWithHint`
  via `CallbackResize` + `Marshal.AllocHGlobal`-backed buffer that grows as the user types
- Image / ImageButton / DrawList.AddImage* with `ulong textureId` and typed
  `GpuTexture` overloads (`GpuTexture.NativeHandle` exposes the SDL_GPU pointer)

#### Public enums

WindowFlags, ChildFlags, TreeNodeFlags, SelectableFlags, ComboFlags, TabBarFlags,
TabItemFlags, TableFlags, TableColumnFlags, TableRowFlags, TableBgTarget, PopupFlags,
HoveredFlags, FocusedFlags, SliderFlags, ColorEditFlags, ConfigFlags, BackendFlags,
InputTextFlags, ButtonFlags, ItemFlags, DragDropFlags, MultiSelectFlags, DrawFlags,
Cond, Col, StyleVar, Dir, SortDirection, SelectionRequestType, MouseButton, MouseCursor,
MouseSource, DataType, Key (140-value enum: keyboard, gamepad, mouse aliases, mod flags).

#### Remaining

**Linux build**:
- [ ] Linux build of `libimgui_sharp` — requires building SDL3 from source; tracked in sibling repo.

**Inventory maintenance**:
- [ ] `src/SdlSharp.ImGui/INVENTORY.md` — refresh the "Native Wrapper" / "Managed Wrapper"
  columns; the cross-reference is heavily stale after the 0.2.0 expansion.

**Testing & samples**:
- [ ] Run ImGuiDemo sample end-to-end against the 0.2.0 native library
- [ ] Verify input forwarding (keyboard, mouse, scroll)
- [ ] Verify window resize handling
- [ ] Create a richer sample (custom DrawList overlay, custom TTF font, drag-drop
  list reordering, sortable table with multi-column sort, plot of live metrics).

**Future work (deferred / niche)**:
- ListClipper `foreach`-style enumerator (the IDisposable wrapper is sufficient for
  the typical step-loop pattern, but a managed-iterator API would be a nice convenience).
- Viewport wrapper extras: secondary viewports, platform handles (only relevant if/when
  multi-viewport docking is enabled — not currently supported by the SDL_GPU backend).

