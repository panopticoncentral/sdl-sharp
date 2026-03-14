# SdlSharp SDL3 Wrapper — TODO

Architecture rules and code patterns are in `CLAUDE.md`.

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
| `BlendMode.cs` | Blend mode wrapper with static presets |
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

**Deferred high-level wrappers** (native layer ready, wrap later as needed):
- `Input/Gamepad.cs` — sealed class wrapping `SDL_Gamepad*`
- `Input/Joystick.cs` — sealed class wrapping `SDL_Joystick*`
- `Input/TouchDevice.cs` — touch input
- `Input/Pen.cs` — pen/stylus input
- `Input/Sensor.cs` — accelerometer/gyroscope

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

### Phase 5: GPU + Advanced

- **`Native/Gpu.cs`** — wraps `SDL_gpu.h` (~100+ functions, the largest single header)
- **`Graphics/Gpu/GpuDevice.cs`** — device creation and management
- **`Graphics/Gpu/GpuCommandBuffer.cs`** — command recording
- **`Graphics/Gpu/GpuRenderPass.cs`** — render pass management
- **`Graphics/Gpu/GpuComputePass.cs`** — compute pass
- **`Graphics/Gpu/GpuPipeline.cs`** — graphics/compute pipelines
- **`Graphics/Gpu/GpuBuffer.cs`** — GPU buffer management
- **`Graphics/Gpu/GpuTexture.cs`** — GPU texture management
- **`Graphics/Gpu/GpuSampler.cs`** — sampler state
- **`Graphics/Gpu/GpuShader.cs`** — shader loading

Plus remaining subsystems:
- `Native/Haptic.cs` + `Input/Haptic.cs` — force feedback
- `Native/Camera.cs` + `Input/Camera.cs` — camera input
- `Native/Timer.cs` + `Timer.cs` — timing utilities
- `Native/Hints.cs` + `Hints.cs` — configuration hints
- `Native/Log.cs` + `Log.cs` — logging
- `Native/Clipboard.cs` + `Clipboard.cs` — clipboard access
- `Native/Dialog.cs` — file/folder dialogs
- `Native/MessageBox.cs` — simple message boxes
- `Native/IOStream.cs` — I/O stream abstraction
- `Native/Storage.cs` — storage/file API
- `Native/FileSystem.cs` — filesystem utilities
- `Native/Thread.cs` + `Native/Mutex.cs` + `Native/Atomic.cs` — threading (may not need high-level wrappers, C# has its own)
- `Native/Process.cs` — process management
- `Native/HidApi.cs` — HID device access
- `Native/CpuInfo.cs` + `CpuInfo.cs` — CPU detection
- `Native/Power.cs` — power state
- `Native/Locale.cs` — locale/i18n
- `Native/Time.cs` — time utilities
- `Native/Misc.cs` — miscellaneous (SDL_OpenURL, etc.)

