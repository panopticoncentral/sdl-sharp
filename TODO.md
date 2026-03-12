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

## What's Next

### Phase 2: Video + Rendering

This is the next phase to implement. Source headers are at `../SDL/include/SDL3/`.

#### Native files to create

- **`Native/Rect.cs`** — wraps `SDL_rect.h`
  - Structs: `SDL_Point`, `SDL_FPoint`, `SDL_Rect`, `SDL_FRect`
  - Functions: `SDL_RectEmpty`, `SDL_RectsEqual`, `SDL_HasRectIntersection`, `SDL_GetRectIntersection`, `SDL_GetRectUnion`, etc.

- **`Native/Pixels.cs`** — wraps `SDL_pixels.h`
  - Enums: `SDL_PixelFormat`, `SDL_ColorType`, `SDL_ColorRange`, `SDL_ColorPrimaries`, `SDL_TransferCharacteristics`, `SDL_MatrixCoefficients`, `SDL_Colorspace`
  - Structs: `SDL_Color`, `SDL_FColor`, `SDL_Palette`
  - Functions: `SDL_GetPixelFormatName`, `SDL_MapRGB`, `SDL_MapRGBA`, etc.

- **`Native/BlendMode.cs`** — wraps `SDL_blendmode.h`
  - Enums: `SDL_BlendMode`, `SDL_BlendOperation`, `SDL_BlendFactor`
  - Functions: `SDL_ComposeCustomBlendMode`

- **`Native/Surface.cs`** — wraps `SDL_surface.h`
  - Opaque type: `SDL_Surface` (note: not fully opaque in SDL3, has public fields)
  - Functions: `SDL_CreateSurface`, `SDL_DestroySurface`, `SDL_LoadBMP`, `SDL_SaveBMP`, `SDL_BlitSurface`, `SDL_FillSurfaceRect`, etc.

- **`Native/Video.cs`** — wraps `SDL_video.h`
  - Opaque type: `SDL_Window`
  - Typed IDs: `SDL_WindowID` (uint), `SDL_DisplayID` (uint)
  - Enums: `SDL_WindowFlags` (Uint64!), `SDL_FlashOperation`, `SDL_GLattr`, `SDL_GLprofile`, `SDL_GLcontextFlag`
  - Structs: `SDL_DisplayMode`
  - Functions: ~80+ covering window creation/destruction, positioning, sizing, fullscreen, display management, GL context, etc.
  - Property constants: `SDL_PROP_WINDOW_*`, `SDL_PROP_DISPLAY_*`

- **`Native/Render.cs`** — wraps `SDL_render.h`
  - Opaque types: `SDL_Renderer`, `SDL_Texture`
  - Structs: `SDL_Vertex`
  - Enums: `SDL_TextureAccess`, `SDL_RendererLogicalPresentation`
  - Functions: ~50+ covering renderer creation, texture management, drawing primitives, viewport, clipping, etc.
  - Property constants: `SDL_PROP_RENDERER_*`, `SDL_PROP_TEXTURE_*`

#### High-level wrappers to create

- **`Graphics/Color.cs`** — `readonly record struct Color(byte R, byte G, byte B, byte A)`
- **`Graphics/Point.cs`** — `readonly record struct Point(int X, int Y)`
- **`Graphics/FPoint.cs`** — `readonly record struct FPoint(float X, float Y)`
- **`Graphics/Rectangle.cs`** — `readonly record struct Rectangle(int X, int Y, int W, int H)`
- **`Graphics/FRectangle.cs`** — `readonly record struct FRectangle(float X, float Y, float W, float H)`
- **`Graphics/Size.cs`** — `readonly record struct Size(int W, int H)`
- **`Graphics/Display.cs`** — display info wrapper
- **`Graphics/Window.cs`** — sealed, IDisposable, wraps `SDL_Window*`, owns/borrows, properties (Title, Position, Size, Fullscreen, etc.)
- **`Graphics/Surface.cs`** — sealed, IDisposable, wraps `SDL_Surface*`
- **`Graphics/Renderer.cs`** — sealed, IDisposable, wraps `SDL_Renderer*`, drawing methods
- **`Graphics/Texture.cs`** — sealed, IDisposable, wraps `SDL_Texture*`
- **`Graphics/PixelFormat.cs`** — enum/helpers for pixel format management
- **`Graphics/BlendMode.cs`** — blend mode wrapper with static presets

### Phase 3: Events + Input

#### Native files to create

- **`Native/Events.cs`** — wraps `SDL_events.h`
  - `SDL_EventType` enum (large — hundreds of values)
  - `SDL_Event` union struct (`[StructLayout(LayoutKind.Explicit)]`)
  - Individual event structs: `SDL_CommonEvent`, `SDL_WindowEvent`, `SDL_KeyboardEvent`, `SDL_MouseMotionEvent`, `SDL_MouseButtonEvent`, `SDL_MouseWheelEvent`, `SDL_JoyAxisEvent`, `SDL_GamepadAxisEvent`, etc.
  - Functions: `SDL_PollEvent`, `SDL_WaitEvent`, `SDL_WaitEventTimeout`, `SDL_PushEvent`, `SDL_RegisterEvents`, `SDL_SetEventFilter`, `SDL_AddEventWatch`, etc.

- **`Native/Keyboard.cs`** — wraps `SDL_keyboard.h`
- **`Native/Keycode.cs`** — wraps `SDL_keycode.h` (large enum)
- **`Native/Scancode.cs`** — wraps `SDL_scancode.h` (large enum)
- **`Native/Mouse.cs`** — wraps `SDL_mouse.h`
- **`Native/Joystick.cs`** — wraps `SDL_joystick.h`
- **`Native/Gamepad.cs`** — wraps `SDL_gamepad.h`
- **`Native/Touch.cs`** — wraps `SDL_touch.h`
- **`Native/Pen.cs`** — wraps `SDL_pen.h`
- **`Native/Sensor.cs`** — wraps `SDL_sensor.h`

#### High-level wrappers to create

- Add event dispatch to `Application.cs` — `DispatchEvents()` with `SDL_PollEvent` loop and switch on event type
- Event args as `readonly record struct` per event category
- **`Input/Keyboard.cs`** — static class, static events for key up/down, key state queries
- **`Input/Mouse.cs`** — static class, static events, cursor management
- **`Input/Gamepad.cs`** — sealed class wrapping `SDL_Gamepad*`
- **`Input/Joystick.cs`** — sealed class wrapping `SDL_Joystick*`
- **`Input/TouchDevice.cs`** — touch input
- **`Input/Pen.cs`** — pen/stylus input
- **`Input/Sensor.cs`** — accelerometer/gyroscope

### Phase 4: Audio

#### Native files to create

- **`Native/Audio.cs`** — wraps `SDL_audio.h`
  - Opaque type: `SDL_AudioStream`
  - Typed IDs: `SDL_AudioDeviceID`
  - Enums: `SDL_AudioFormat`
  - Structs: `SDL_AudioSpec`
  - Functions: ~50+ covering devices, streams, format conversion, etc.

#### High-level wrappers to create

- **`Audio/AudioDevice.cs`** — device enumeration and management
- **`Audio/AudioStream.cs`** — sealed, IDisposable, stream-centric audio API

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

