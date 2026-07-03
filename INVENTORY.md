# SDL3 API Inventory

Cross-reference of SDL 3.4.2 headers with SdlSharp native bindings and managed wrappers.

- **Native Wrapper**: Qualified name in the `SdlSharp.Native` namespace (e.g. `Pixels.SDL_GetMasksForPixelFormat`).
- **Managed Wrapper**: Qualified name of the public C# API (e.g. `PixelFormatExtensions.GetMasks`).
- **Notes**: Why an unwrapped API is skipped: *deferred* = planned but not yet done, *inline* = SDL_FORCE_INLINE (not exported), *variadic* = C va_list/printf-style, *.NET* = .NET has a built-in equivalent, *platform* = platform-specific API, *niche* = rarely needed.
- **"-"**: Not yet wrapped.

---

## SDL_assert.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_ASSERT_LEVEL | macro | - | - | macro — .NET has Debug.Assert |
| SDL_TriggerBreakpoint | macro | - | - | macro — use Debugger.Break() |
| SDL_FUNCTION | macro | - | - | macro |
| SDL_FILE | macro | - | - | macro |
| SDL_ASSERT_FILE | macro | - | - | macro |
| SDL_LINE | macro | - | - | macro |
| SDL_NULL_WHILE_LOOP_CONDITION | macro | - | - | macro — internal |
| SDL_disabled_assert | macro | - | - | macro — internal |
| SDL_enabled_assert | macro | - | - | macro — internal |
| SDL_AssertBreakpoint | macro | - | - | macro — internal |
| SDL_assert | macro | - | - | macro — .NET has Debug.Assert |
| SDL_assert_release | macro | - | - | macro — .NET has Debug.Assert |
| SDL_assert_paranoid | macro | - | - | macro — .NET has Debug.Assert |
| SDL_assert_always | macro | - | - | macro — .NET has Debug.Assert |
| SDL_AssertState | enum | Assert.SDL_AssertState | AssertState | |
| SDL_AssertData | struct | Assert.SDL_AssertData | AssertionData | |
| SDL_AssertionHandler | callback | Assert.SDL_SetAssertionHandler (function pointer) | SdlAssert.SetAssertionHandler | |
| SDL_ReportAssertion | function | - | - | internal — "never call this directly" |
| SDL_SetAssertionHandler | function | Assert.SDL_SetAssertionHandler | SdlAssert.SetAssertionHandler | |
| SDL_GetDefaultAssertionHandler | function | Assert.SDL_GetDefaultAssertionHandler | - | niche — returns native function pointer |
| SDL_GetAssertionHandler | function | Assert.SDL_GetAssertionHandler | - | niche — returns native function pointer |
| SDL_GetAssertionReport | function | Assert.SDL_GetAssertionReport | SdlAssert.GetAssertionReport | |
| SDL_ResetAssertionReport | function | Assert.SDL_ResetAssertionReport | SdlAssert.ResetAssertionReport | |

## SDL_asyncio.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_AsyncIO | struct | - | - | .NET has async/await and System.IO |
| SDL_AsyncIOTaskType | enum | - | - | .NET has async/await |
| SDL_AsyncIOResult | enum | - | - | .NET has async/await |
| SDL_AsyncIOOutcome | struct | - | - | .NET has async/await |
| SDL_AsyncIOQueue | struct | - | - | .NET has async/await |
| SDL_AsyncIOFromFile | function | - | - | .NET has async/await and System.IO |
| SDL_GetAsyncIOSize | function | - | - | .NET has async/await |
| SDL_ReadAsyncIO | function | - | - | .NET has async/await |
| SDL_WriteAsyncIO | function | - | - | .NET has async/await |
| SDL_CloseAsyncIO | function | - | - | .NET has async/await |
| SDL_CreateAsyncIOQueue | function | - | - | .NET has async/await |
| SDL_DestroyAsyncIOQueue | function | - | - | .NET has async/await |
| SDL_GetAsyncIOResult | function | - | - | .NET has async/await |
| SDL_WaitAsyncIOResult | function | - | - | .NET has async/await |
| SDL_SignalAsyncIOQueue | function | - | - | .NET has async/await |
| SDL_LoadFileAsync | function | - | - | .NET has async/await |

## SDL_atomic.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_SpinLock | typedef | - | - | .NET has System.Threading.SpinLock |
| SDL_TryLockSpinlock | function | - | - | .NET has System.Threading.SpinLock |
| SDL_LockSpinlock | function | - | - | .NET has System.Threading.SpinLock |
| SDL_UnlockSpinlock | function | - | - | .NET has System.Threading.SpinLock |
| SDL_CompilerBarrier | macro | - | - | macro — compiler-specific; .NET has Interlocked/Volatile |
| SDL_MemoryBarrierReleaseFunction | function | - | - | .NET has Thread.MemoryBarrier |
| SDL_MemoryBarrierAcquireFunction | function | - | - | .NET has Thread.MemoryBarrier |
| SDL_MemoryBarrierRelease | macro | - | - | macro — .NET has Thread.MemoryBarrier |
| SDL_MemoryBarrierAcquire | macro | - | - | macro — .NET has Thread.MemoryBarrier |
| SDL_CPUPauseInstruction | macro | - | - | macro — .NET has Thread.SpinWait |
| SDL_AtomicInt | struct | - | - | .NET has Interlocked |
| SDL_CompareAndSwapAtomicInt | function | - | - | .NET has Interlocked.CompareExchange |
| SDL_SetAtomicInt | function | - | - | .NET has Interlocked.Exchange |
| SDL_GetAtomicInt | function | - | - | .NET has Volatile.Read |
| SDL_AddAtomicInt | function | - | - | .NET has Interlocked.Add |
| SDL_AtomicIncRef | macro | - | - | macro — .NET has Interlocked.Increment |
| SDL_AtomicDecRef | macro | - | - | macro — .NET has Interlocked.Decrement |
| SDL_AtomicU32 | struct | - | - | .NET has Interlocked |
| SDL_CompareAndSwapAtomicU32 | function | - | - | .NET has Interlocked.CompareExchange |
| SDL_SetAtomicU32 | function | - | - | .NET has Interlocked.Exchange |
| SDL_GetAtomicU32 | function | - | - | .NET has Volatile.Read |
| SDL_AddAtomicU32 | function | - | - | .NET has Interlocked.Add |
| SDL_CompareAndSwapAtomicPointer | function | - | - | .NET has Interlocked.CompareExchange |
| SDL_SetAtomicPointer | function | - | - | .NET has Interlocked.Exchange |
| SDL_GetAtomicPointer | function | - | - | .NET has Volatile.Read |

## SDL_audio.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_AudioFormat | enum | Audio.SDL_AudioFormat | AudioFormat | |
| SDL_AUDIO_UNKNOWN | enum value | Audio.SDL_AudioFormat.SDL_AUDIO_UNKNOWN | AudioFormat.Unknown | |
| SDL_AUDIO_U8 | enum value | Audio.SDL_AudioFormat.SDL_AUDIO_U8 | AudioFormat.U8 | |
| SDL_AUDIO_S8 | enum value | Audio.SDL_AudioFormat.SDL_AUDIO_S8 | AudioFormat.S8 | |
| SDL_AUDIO_S16LE | enum value | Audio.SDL_AudioFormat.SDL_AUDIO_S16LE | AudioFormat.S16LE | |
| SDL_AUDIO_S16BE | enum value | Audio.SDL_AudioFormat.SDL_AUDIO_S16BE | AudioFormat.S16BE | |
| SDL_AUDIO_S32LE | enum value | Audio.SDL_AudioFormat.SDL_AUDIO_S32LE | AudioFormat.S32LE | |
| SDL_AUDIO_S32BE | enum value | Audio.SDL_AudioFormat.SDL_AUDIO_S32BE | AudioFormat.S32BE | |
| SDL_AUDIO_F32LE | enum value | Audio.SDL_AudioFormat.SDL_AUDIO_F32LE | AudioFormat.F32LE | |
| SDL_AUDIO_F32BE | enum value | Audio.SDL_AudioFormat.SDL_AUDIO_F32BE | AudioFormat.F32BE | |
| SDL_AUDIO_S16 / SDL_AUDIO_S32 / SDL_AUDIO_F32 | enum value | - | - | Deferred: native-byte-order aliases, missing from both SDL_AudioFormat and AudioFormat |
| SDL_AUDIO_MASK_BITSIZE / _FLOAT / _BIG_ENDIAN / _SIGNED | macro | - | - | macro — format bit masks (4 constants) |
| SDL_DEFINE_AUDIO_FORMAT | macro | - | - | macro — internal format constructor |
| SDL_AUDIO_BITSIZE / BYTESIZE / ISFLOAT / ISBIGENDIAN / ISLITTLEENDIAN / ISSIGNED / ISINT / ISUNSIGNED | macro | - | - | Deferred: format-introspection helpers (AudioFormat extensions; 8 macros) |
| SDL_AudioDeviceID | typedef | Audio.SDL_AudioDeviceID | - | Internal ID type |
| SDL_AudioSpec | struct | Audio.SDL_AudioSpec | AudioSpec | |
| SDL_AUDIO_FRAMESIZE | macro | - | - | Deferred: AudioSpec.FrameSize helper |
| SDL_AudioStream | opaque | Audio.SDL_AudioStream | AudioStream | |
| SDL_AUDIO_DEVICE_DEFAULT_PLAYBACK | constant | Audio.SDL_AUDIO_DEVICE_DEFAULT_PLAYBACK | - | Used internally |
| SDL_AUDIO_DEVICE_DEFAULT_RECORDING | constant | Audio.SDL_AUDIO_DEVICE_DEFAULT_RECORDING | - | Used internally |
| SDL_PROP_AUDIOSTREAM_AUTO_CLEANUP_BOOLEAN | macro | Audio.SDL_PROP_AUDIOSTREAM_AUTO_CLEANUP_BOOLEAN | - | Property string constant |
| SDL_GetNumAudioDrivers | function | Audio.SDL_GetNumAudioDrivers | AudioDevice.NumAudioDrivers | |
| SDL_GetAudioDriver | function | Audio.SDL_GetAudioDriver | AudioDevice.GetAudioDriver | |
| SDL_GetCurrentAudioDriver | function | Audio.SDL_GetCurrentAudioDriver | AudioDevice.CurrentAudioDriver | |
| SDL_GetAudioPlaybackDevices | function | Audio.SDL_GetAudioPlaybackDevices | AudioDevice.GetPlaybackDevices | |
| SDL_GetAudioRecordingDevices | function | Audio.SDL_GetAudioRecordingDevices | AudioDevice.GetRecordingDevices | |
| SDL_GetAudioDeviceName | function | Audio.SDL_GetAudioDeviceName | - | Used internally by AudioDevice |
| SDL_GetAudioDeviceFormat | function | Audio.SDL_GetAudioDeviceFormat | AudioDevice.GetFormat | |
| SDL_OpenAudioDevice | function | Audio.SDL_OpenAudioDevice | AudioDevice.Open | |
| SDL_CloseAudioDevice | function | Audio.SDL_CloseAudioDevice | AudioDevice.Dispose | |
| SDL_PauseAudioDevice | function | Audio.SDL_PauseAudioDevice | AudioDevice.Pause | |
| SDL_ResumeAudioDevice | function | Audio.SDL_ResumeAudioDevice | AudioDevice.Resume | |
| SDL_AudioDevicePaused | function | Audio.SDL_AudioDevicePaused | AudioDevice.IsPaused | |
| SDL_GetAudioDeviceGain | function | Audio.SDL_GetAudioDeviceGain | AudioDevice.Gain (get) | |
| SDL_SetAudioDeviceGain | function | Audio.SDL_SetAudioDeviceGain | AudioDevice.Gain (set) | |
| SDL_BindAudioStream | function | Audio.SDL_BindAudioStream | AudioDevice.Bind | |
| SDL_UnbindAudioStream | function | Audio.SDL_UnbindAudioStream | AudioStream.Unbind | |
| SDL_GetAudioStreamDevice | function | Audio.SDL_GetAudioStreamDevice | - | Deferred: expose as AudioStream.Device — native binding exists but is currently unused |
| SDL_CreateAudioStream | function | Audio.SDL_CreateAudioStream | AudioStream.Create | |
| SDL_DestroyAudioStream | function | Audio.SDL_DestroyAudioStream | AudioStream.Dispose | |
| SDL_OpenAudioDeviceStream | function | Audio.SDL_OpenAudioDeviceStream | AudioStream.OpenDevice | |
| SDL_GetAudioStreamProperties | function | Audio.SDL_GetAudioStreamProperties | AudioStream.Properties | |
| SDL_GetAudioStreamFormat | function | Audio.SDL_GetAudioStreamFormat | AudioStream.GetFormat | |
| SDL_SetAudioStreamFormat | function | Audio.SDL_SetAudioStreamFormat | AudioStream.SetFormat | |
| SDL_GetAudioStreamFrequencyRatio | function | Audio.SDL_GetAudioStreamFrequencyRatio | AudioStream.FrequencyRatio (get) | |
| SDL_SetAudioStreamFrequencyRatio | function | Audio.SDL_SetAudioStreamFrequencyRatio | AudioStream.FrequencyRatio (set) | |
| SDL_GetAudioStreamGain | function | Audio.SDL_GetAudioStreamGain | AudioStream.Gain (get) | |
| SDL_SetAudioStreamGain | function | Audio.SDL_SetAudioStreamGain | AudioStream.Gain (set) | |
| SDL_PutAudioStreamData | function | Audio.SDL_PutAudioStreamData | AudioStream.PutData | |
| SDL_GetAudioStreamData | function | Audio.SDL_GetAudioStreamData | AudioStream.GetData | |
| SDL_GetAudioStreamAvailable | function | Audio.SDL_GetAudioStreamAvailable | AudioStream.Available | |
| SDL_GetAudioStreamQueued | function | Audio.SDL_GetAudioStreamQueued | AudioStream.Queued | |
| SDL_FlushAudioStream | function | Audio.SDL_FlushAudioStream | AudioStream.Flush | |
| SDL_ClearAudioStream | function | Audio.SDL_ClearAudioStream | AudioStream.Clear | |
| SDL_PauseAudioStreamDevice | function | Audio.SDL_PauseAudioStreamDevice | AudioStream.PauseDevice | |
| SDL_ResumeAudioStreamDevice | function | Audio.SDL_ResumeAudioStreamDevice | AudioStream.ResumeDevice | |
| SDL_AudioStreamDevicePaused | function | Audio.SDL_AudioStreamDevicePaused | AudioStream.IsDevicePaused | |
| SDL_LoadWAV | function | Audio.SDL_LoadWAV | WavData.Load | |
| SDL_GetAudioFormatName | function | Audio.SDL_GetAudioFormatName | AudioFormatInfo.GetName | |
| SDL_LoadWAV_IO | function | - | - | Deferred: needs SDL_IOStream |
| SDL_AudioPostmixCallback | callback | - | - | Deferred: advanced callback interop |
| SDL_SetAudioPostmixCallback | function | - | - | Deferred: advanced callback interop |
| SDL_AudioStreamDataCompleteCallback | callback | - | - | Deferred: only needed for SDL_PutAudioStreamDataNoCopy |
| SDL_PutAudioStreamDataNoCopy | function | - | - | Deferred: advanced zero-copy |
| SDL_PutAudioStreamPlanarData | function | - | - | Deferred: planar layout, niche |
| SDL_ConvertAudioSamples | function | - | - | Deferred: use AudioStream instead |
| SDL_MixAudio | function | - | - | Deferred: low-level mixing |
| SDL_GetSilenceValueForFormat | function | - | - | Deferred: low-level |
| SDL_AudioStreamCallback | callback | Audio.SDL_OpenAudioDeviceStream (function pointer param) | - | Deferred: managed callback interop — AudioStream.OpenDevice passes null |
| SDL_SetAudioStreamGetCallback | function | - | - | Deferred: advanced callback interop |
| SDL_SetAudioStreamPutCallback | function | - | - | Deferred: advanced callback interop |
| SDL_GetAudioDeviceChannelMap | function | - | - | Deferred: channel remapping, uncommon |
| SDL_GetAudioStreamInputChannelMap | function | - | - | Deferred: channel remapping, uncommon |
| SDL_GetAudioStreamOutputChannelMap | function | - | - | Deferred: channel remapping, uncommon |
| SDL_SetAudioStreamInputChannelMap | function | - | - | Deferred: channel remapping, uncommon |
| SDL_SetAudioStreamOutputChannelMap | function | - | - | Deferred: channel remapping, uncommon |
| SDL_LockAudioStream | function | - | - | Deferred: low-level locking |
| SDL_UnlockAudioStream | function | - | - | Deferred: low-level locking |
| SDL_IsAudioDevicePhysical | function | - | - | Deferred: query helper |
| SDL_IsAudioDevicePlayback | function | - | - | Deferred: query helper |
| SDL_BindAudioStreams | function | - | - | Deferred: multi-stream bind |
| SDL_UnbindAudioStreams | function | - | - | Deferred: multi-stream bind |

## SDL_blendmode.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_BlendMode | enum | BlendMode.SDL_BlendMode | BlendMode | |
| SDL_BlendOperation | enum | BlendMode.SDL_BlendOperation | BlendOperation | |
| SDL_BlendFactor | enum | BlendMode.SDL_BlendFactor | BlendFactor | |
| SDL_ComposeCustomBlendMode | function | BlendMode.SDL_ComposeCustomBlendMode | BlendModeExtensions.Compose | |

## SDL_camera.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_CameraID | typedef | Camera.SDL_CameraID | - | Internal ID type |
| SDL_Camera | opaque | Camera.SDL_Camera | Camera | |
| SDL_CameraSpec | struct | Camera.SDL_CameraSpec | CameraSpec | |
| SDL_CameraPosition | enum | Camera.SDL_CameraPosition | CameraPosition | |
| SDL_CameraPermissionState | enum | - | - | deferred — SDL_GetCameraPermissionState bound as raw int |
| SDL_GetNumCameraDrivers | function | Camera.SDL_GetNumCameraDrivers | Camera.GetDrivers | |
| SDL_GetCameraDriver | function | Camera.SDL_GetCameraDriver | Camera.GetDrivers | |
| SDL_GetCurrentCameraDriver | function | Camera.SDL_GetCurrentCameraDriver | Camera.GetCurrentDriver | |
| SDL_GetCameras | function | Camera.SDL_GetCameras | Camera.GetDevices | |
| SDL_GetCameraSupportedFormats | function | Camera.SDL_GetCameraSupportedFormats | Camera.GetSupportedFormats | |
| SDL_GetCameraName | function | Camera.SDL_GetCameraName | Camera.GetName | |
| SDL_GetCameraPosition | function | Camera.SDL_GetCameraPosition | Camera.GetPosition | |
| SDL_OpenCamera | function | Camera.SDL_OpenCamera | Camera.Open | |
| SDL_GetCameraPermissionState | function | Camera.SDL_GetCameraPermissionState | Camera.PermissionState | returns untyped int; typed CameraPermissionState enum deferred |
| SDL_GetCameraID | function | Camera.SDL_GetCameraID | - | deferred — binding unused, no Camera.Id property |
| SDL_GetCameraProperties | function | Camera.SDL_GetCameraProperties | Camera.Properties | |
| SDL_GetCameraFormat | function | Camera.SDL_GetCameraFormat | Camera.Format | |
| SDL_AcquireCameraFrame | function | Camera.SDL_AcquireCameraFrame | Camera.AcquireFrame | |
| SDL_ReleaseCameraFrame | function | Camera.SDL_ReleaseCameraFrame | Camera.ReleaseFrame | |
| SDL_CloseCamera | function | Camera.SDL_CloseCamera | Camera.Dispose | |

## SDL_clipboard.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_SetClipboardText | function | Clipboard.SDL_SetClipboardText | Clipboard.Text (set) | |
| SDL_GetClipboardText | function | Clipboard.SDL_GetClipboardText | Clipboard.Text (get) | |
| SDL_HasClipboardText | function | Clipboard.SDL_HasClipboardText | Clipboard.HasText | |
| SDL_SetPrimarySelectionText | function | - | - | Deferred: X11 primary selection, platform-specific niche |
| SDL_GetPrimarySelectionText | function | - | - | Deferred: X11 primary selection, platform-specific niche |
| SDL_HasPrimarySelectionText | function | - | - | Deferred: X11 primary selection, platform-specific niche |
| SDL_ClipboardDataCallback | callback | Clipboard.SDL_ClipboardDataCallback | Clipboard.ClipboardDataProvider | |
| SDL_ClipboardCleanupCallback | callback | Clipboard.SDL_ClipboardCleanupCallback | (internal) | |
| SDL_SetClipboardData | function | Clipboard.SDL_SetClipboardData | Clipboard.SetData | |
| SDL_ClearClipboardData | function | Clipboard.SDL_ClearClipboardData | Clipboard.ClearData | |
| SDL_GetClipboardData | function | Clipboard.SDL_GetClipboardData | Clipboard.GetData | |
| SDL_HasClipboardData | function | Clipboard.SDL_HasClipboardData | Clipboard.HasData | |
| SDL_GetClipboardMimeTypes | function | Clipboard.SDL_GetClipboardMimeTypes | Clipboard.GetMimeTypes | |

## SDL_cpuinfo.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_CACHELINE_SIZE | constant | - | - | macro; compile-time guess (128) — use SystemInfo.CpuCacheLineSize for the runtime value |
| SDL_GetNumLogicalCPUCores | function | CpuInfo.SDL_GetNumLogicalCPUCores | SystemInfo.LogicalCpuCores | |
| SDL_GetCPUCacheLineSize | function | CpuInfo.SDL_GetCPUCacheLineSize | SystemInfo.CpuCacheLineSize | |
| SDL_HasAltiVec | function | CpuInfo.SDL_HasAltiVec | SystemInfo.HasAltiVec | |
| SDL_HasMMX | function | CpuInfo.SDL_HasMMX | SystemInfo.HasMmx | |
| SDL_HasSSE | function | CpuInfo.SDL_HasSSE | SystemInfo.HasSse | |
| SDL_HasSSE2 | function | CpuInfo.SDL_HasSSE2 | SystemInfo.HasSse2 | |
| SDL_HasSSE3 | function | CpuInfo.SDL_HasSSE3 | SystemInfo.HasSse3 | |
| SDL_HasSSE41 | function | CpuInfo.SDL_HasSSE41 | SystemInfo.HasSse41 | |
| SDL_HasSSE42 | function | CpuInfo.SDL_HasSSE42 | SystemInfo.HasSse42 | |
| SDL_HasAVX | function | CpuInfo.SDL_HasAVX | SystemInfo.HasAvx | |
| SDL_HasAVX2 | function | CpuInfo.SDL_HasAVX2 | SystemInfo.HasAvx2 | |
| SDL_HasAVX512F | function | CpuInfo.SDL_HasAVX512F | SystemInfo.HasAvx512F | |
| SDL_HasARMSIMD | function | CpuInfo.SDL_HasARMSIMD | SystemInfo.HasArmsimd | |
| SDL_HasNEON | function | CpuInfo.SDL_HasNEON | SystemInfo.HasNeon | |
| SDL_HasLSX | function | CpuInfo.SDL_HasLSX | SystemInfo.HasLsx | |
| SDL_HasLASX | function | CpuInfo.SDL_HasLASX | SystemInfo.HasLasx | |
| SDL_GetSystemRAM | function | CpuInfo.SDL_GetSystemRAM | SystemInfo.SystemRam | |
| SDL_GetSIMDAlignment | function | CpuInfo.SDL_GetSIMDAlignment | SystemInfo.SimdAlignment | |
| SDL_GetSystemPageSize | function | CpuInfo.SDL_GetSystemPageSize | SystemInfo.SystemPageSize | |

## SDL_dialog.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_DialogFileFilter | struct | Dialog.SDL_DialogFileFilter | DialogFileFilter | |
| SDL_FileDialogType | enum | Dialog.SDL_FileDialogType | FileDialogType | |
| SDL_ShowOpenFileDialog | function | Dialog.SDL_ShowOpenFileDialog | FileDialog.OpenFile / FileDialog.OpenFileAsync | |
| SDL_ShowSaveFileDialog | function | Dialog.SDL_ShowSaveFileDialog | FileDialog.SaveFile / FileDialog.SaveFileAsync | |
| SDL_ShowOpenFolderDialog | function | Dialog.SDL_ShowOpenFolderDialog | FileDialog.OpenFolder / FileDialog.OpenFolderAsync | |
| SDL_ShowFileDialogWithProperties | function | - | - | Deferred: property-based variant, rarely needed |
| SDL_DialogFileCallback | callback | Dialog.SDL_ShowOpenFileDialog (function pointer) | FileDialog.DialogCallback (internal) | Surfaced as `Action<DialogResult>` |
| SDL_PROP_FILE_DIALOG_* | macro (8) | - | - | Property string constants — deferred with SDL_ShowFileDialogWithProperties |

## SDL_error.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_SetError | function | Error.SDL_SetError | - | niche — only needed by apps reporting errors back to SDL |
| SDL_SetErrorV | function | - | - | Variadic: C va_list has no safe C# equivalent |
| SDL_OutOfMemory | function | Error.SDL_OutOfMemory | - | niche — managed code uses OutOfMemoryException |
| SDL_GetError | function | Error.SDL_GetError | SdlException | |
| SDL_ClearError | function | Error.SDL_ClearError | SdlException | |
| SDL_Unsupported | macro | Error.SDL_Unsupported | - | macro — reimplemented as native helper; niche |
| SDL_InvalidParamError | macro | Error.SDL_InvalidParamError | - | macro — reimplemented as native helper; niche |

## SDL_events.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_EventType | enum | Events.SDL_EventType | - | Used internally by event dispatch |
| SDL_CommonEvent | struct | Events.SDL_CommonEvent | - | |
| SDL_DisplayEvent | struct | Events.SDL_DisplayEvent | - | |
| SDL_WindowEvent | struct | Events.SDL_WindowEvent | WindowEventArgs | |
| SDL_KeyboardDeviceEvent | struct | Events.SDL_KeyboardDeviceEvent | - | |
| SDL_KeyboardEvent | struct | Events.SDL_KeyboardEvent | KeyEventArgs | |
| SDL_TextEditingEvent | struct | Events.SDL_TextEditingEvent | - | |
| SDL_TextEditingCandidatesEvent | struct | - | - | Deferred: IME candidate-list event |
| SDL_TextInputEvent | struct | Events.SDL_TextInputEvent | TextInputEventArgs | |
| SDL_MouseDeviceEvent | struct | Events.SDL_MouseDeviceEvent | - | |
| SDL_MouseMotionEvent | struct | Events.SDL_MouseMotionEvent | MouseMotionEventArgs | |
| SDL_MouseButtonEvent | struct | Events.SDL_MouseButtonEvent | MouseButtonEventArgs | |
| SDL_MouseWheelEvent | struct | Events.SDL_MouseWheelEvent | MouseWheelEventArgs | |
| SDL_JoyAxisEvent | struct | Events.SDL_JoyAxisEvent | - | Deferred managed wrapper |
| SDL_JoyBallEvent | struct | Events.SDL_JoyBallEvent | - | Deferred managed wrapper |
| SDL_JoyHatEvent | struct | Events.SDL_JoyHatEvent | - | Deferred managed wrapper |
| SDL_JoyButtonEvent | struct | Events.SDL_JoyButtonEvent | - | Deferred managed wrapper |
| SDL_JoyDeviceEvent | struct | Events.SDL_JoyDeviceEvent | - | Deferred managed wrapper |
| SDL_JoyBatteryEvent | struct | Events.SDL_JoyBatteryEvent | - | Deferred managed wrapper |
| SDL_GamepadAxisEvent | struct | Events.SDL_GamepadAxisEvent | - | Deferred managed wrapper |
| SDL_GamepadButtonEvent | struct | Events.SDL_GamepadButtonEvent | - | Deferred managed wrapper |
| SDL_GamepadDeviceEvent | struct | Events.SDL_GamepadDeviceEvent | - | Deferred managed wrapper |
| SDL_GamepadTouchpadEvent | struct | Events.SDL_GamepadTouchpadEvent | - | Deferred managed wrapper |
| SDL_GamepadSensorEvent | struct | Events.SDL_GamepadSensorEvent | - | Deferred managed wrapper |
| SDL_TouchFingerEvent | struct | Events.SDL_TouchFingerEvent | - | Deferred managed wrapper |
| SDL_PinchFingerEvent | struct | Events.SDL_PinchFingerEvent | - | Deferred managed wrapper |
| SDL_PenProximityEvent | struct | Events.SDL_PenProximityEvent | - | Deferred managed wrapper |
| SDL_PenMotionEvent | struct | Events.SDL_PenMotionEvent | - | Deferred managed wrapper |
| SDL_PenTouchEvent | struct | Events.SDL_PenTouchEvent | - | Deferred managed wrapper |
| SDL_PenButtonEvent | struct | Events.SDL_PenButtonEvent | - | Deferred managed wrapper |
| SDL_PenAxisEvent | struct | Events.SDL_PenAxisEvent | - | Deferred managed wrapper |
| SDL_DropEvent | struct | Events.SDL_DropEvent | - | Deferred managed wrapper |
| SDL_ClipboardEvent | struct | Events.SDL_ClipboardEvent | - | Deferred managed wrapper |
| SDL_SensorEvent | struct | Events.SDL_SensorEvent | - | Deferred managed wrapper |
| SDL_QuitEvent | struct | Events.SDL_QuitEvent | QuitEventArgs | |
| SDL_UserEvent | struct | Events.SDL_UserEvent | - | Deferred managed wrapper |
| SDL_RenderEvent | struct | Events.SDL_RenderEvent | - | Deferred managed wrapper |
| SDL_Event | union | Events.SDL_Event | - | Used internally by event dispatch |
| SDL_PumpEvents | function | Events.SDL_PumpEvents | Application.PumpEvents | |
| SDL_PollEvent | function | Events.SDL_PollEvent | Application.DispatchEvents | Drives the typed event dispatch loop |
| SDL_WaitEvent | function | Events.SDL_WaitEvent | - | Deferred managed wrapper |
| SDL_WaitEventTimeout | function | Events.SDL_WaitEventTimeout | - | Deferred managed wrapper |
| SDL_PushEvent | function | Events.SDL_PushEvent | - | Deferred managed wrapper |
| SDL_HasEvent | function | Events.SDL_HasEvent | - | Deferred managed wrapper |
| SDL_HasEvents | function | Events.SDL_HasEvents | - | Deferred managed wrapper |
| SDL_FlushEvent | function | Events.SDL_FlushEvent | - | Deferred managed wrapper |
| SDL_FlushEvents | function | Events.SDL_FlushEvents | - | Deferred managed wrapper |
| SDL_SetEventEnabled | function | Events.SDL_SetEventEnabled | - | Deferred managed wrapper |
| SDL_EventEnabled | function | Events.SDL_EventEnabled | - | Deferred managed wrapper |
| SDL_RegisterEvents | function | Events.SDL_RegisterEvents | - | Deferred managed wrapper |
| SDL_EventAction | enum | - | - | Deferred: needed by SDL_PeepEvents |
| SDL_PeepEvents | function | - | - | Deferred: bulk queue add/peek/get (no callback) |
| SDL_EventFilter | callback | - | - | Deferred: callback typedef for filter/watch APIs |
| SDL_SetEventFilter | function | - | - | Deferred: callback interop |
| SDL_GetEventFilter | function | - | - | Deferred: callback interop |
| SDL_AddEventWatch | function | - | - | Deferred: callback interop |
| SDL_RemoveEventWatch | function | - | - | Deferred: callback interop |
| SDL_FilterEvents | function | - | - | Deferred: callback interop |
| SDL_GetWindowFromEvent | function | - | - | Deferred: returns SDL_Window* |
| SDL_GetEventDescription | function | - | - | Deferred: debug utility |
| SDL_AudioDeviceEvent | struct | - | - | Deferred: audio event struct |
| SDL_CameraDeviceEvent | struct | - | - | Deferred: camera event struct |

## SDL_filesystem.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_Folder | enum | FileSystem.SDL_Folder | SystemFolder | |
| SDL_GetBasePath | function | FileSystem.SDL_GetBasePath | SystemInfo.BasePath | |
| SDL_GetPrefPath | function | FileSystem.SDL_GetPrefPath | SystemInfo.GetPrefPath | |
| SDL_GetUserFolder | function | FileSystem.SDL_GetUserFolder | SystemInfo.GetUserFolder | |
| SDL_CreateDirectory | function | FileSystem.SDL_CreateDirectory | - | Deferred managed wrapper |
| SDL_GetCurrentDirectory | function | FileSystem.SDL_GetCurrentDirectory | SystemInfo.GetCurrentDirectory | |
| SDL_EnumerationResult | enum | - | - | only used by SDL_EnumerateDirectory — .NET has Directory.EnumerateFileSystemEntries |
| SDL_EnumerateDirectoryCallback | callback | - | - | only used by SDL_EnumerateDirectory — .NET has Directory.EnumerateFileSystemEntries |
| SDL_EnumerateDirectory | function | - | - | .NET has Directory.EnumerateFileSystemEntries |
| SDL_GlobFlags | typedef | - | - | only used by SDL_GlobDirectory — .NET has Directory.GetFiles |
| SDL_GLOB_CASEINSENSITIVE | constant | - | - | only used by SDL_GlobDirectory — .NET has Directory.GetFiles |
| SDL_GlobDirectory | function | - | - | .NET has Directory.GetFiles |
| SDL_PathType | enum | - | - | only used by SDL_GetPathInfo — .NET has FileInfo/DirectoryInfo |
| SDL_PathInfo | struct | - | - | only used by SDL_GetPathInfo — .NET has FileInfo/DirectoryInfo |
| SDL_GetPathInfo | function | - | - | .NET has FileInfo/DirectoryInfo |
| SDL_RemovePath | function | - | - | .NET has File.Delete/Directory.Delete |
| SDL_RenamePath | function | - | - | .NET has File.Move/Directory.Move |
| SDL_CopyFile | function | - | - | .NET has File.Copy |

## SDL_gamepad.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_Gamepad | struct | Gamepad.SDL_Gamepad | Gamepad | |
| SDL_GamepadType | enum | Gamepad.SDL_GamepadType | GamepadType | |
| SDL_GamepadButton | enum | Gamepad.SDL_GamepadButton | GamepadButton | |
| SDL_GamepadButtonLabel | enum | Gamepad.SDL_GamepadButtonLabel | GamepadButtonLabel | |
| SDL_GamepadAxis | enum | Gamepad.SDL_GamepadAxis | GamepadAxis | |
| SDL_GamepadBindingType | enum | - | - | Deferred: binding introspection |
| SDL_GamepadBinding | struct | - | - | Deferred: binding introspection |
| SDL_GetGamepads | function | Gamepad.SDL_GetGamepads | Gamepad.GetDevices | |
| SDL_IsGamepad | function | Gamepad.SDL_IsGamepad | Gamepad.IsGamepad | |
| SDL_GetGamepadNameForID | function | Gamepad.SDL_GetGamepadNameForID | Gamepad.GetName | |
| SDL_GetGamepadTypeForID | function | Gamepad.SDL_GetGamepadTypeForID | Gamepad.GetType | |
| SDL_OpenGamepad | function | Gamepad.SDL_OpenGamepad | Gamepad.Open | |
| SDL_GetGamepadFromID | function | Gamepad.SDL_GetGamepadFromID | - | Deferred |
| SDL_GetGamepadName | function | Gamepad.SDL_GetGamepadName | Gamepad.Name | |
| SDL_GetGamepadType | function | Gamepad.SDL_GetGamepadType | Gamepad.Type | |
| SDL_GetGamepadJoystick | function | Gamepad.SDL_GetGamepadJoystick | - | Deferred |
| SDL_GetGamepadAxis | function | Gamepad.SDL_GetGamepadAxis | Gamepad.GetAxis | |
| SDL_GetGamepadButton | function | Gamepad.SDL_GetGamepadButton | Gamepad.GetButton | |
| SDL_GetGamepadButtonLabel | function | Gamepad.SDL_GetGamepadButtonLabel | Gamepad.GetButtonLabel | |
| SDL_GetGamepadConnectionState | function | Gamepad.SDL_GetGamepadConnectionState | Gamepad.ConnectionState | |
| SDL_CloseGamepad | function | Gamepad.SDL_CloseGamepad | Gamepad.Dispose | |
| SDL_UpdateGamepads | function | Gamepad.SDL_UpdateGamepads | Gamepad.Update | |
| SDL_AddGamepadMapping | function | - | - | Deferred: mapping management |
| SDL_AddGamepadMappingsFromIO | function | - | - | Deferred: mapping management (SDL_IOStream deferred in favor of .NET streams) |
| SDL_AddGamepadMappingsFromFile | function | - | - | Deferred: mapping management |
| SDL_ReloadGamepadMappings | function | - | - | Deferred: mapping management |
| SDL_GetGamepadMappings | function | - | - | Deferred: mapping management |
| SDL_GetGamepadMappingForGUID | function | - | - | Deferred: mapping management |
| SDL_GetGamepadMapping | function | - | - | Deferred: mapping management |
| SDL_GetGamepadMappingForID | function | - | - | Deferred: mapping management |
| SDL_SetGamepadMapping | function | - | - | Deferred: mapping management |
| SDL_HasGamepad | function | - | - | Deferred |
| SDL_GetGamepadPathForID | function | - | - | Deferred: device identification |
| SDL_GetGamepadGUIDForID | function | - | - | Deferred: device identification |
| SDL_GetGamepadVendorForID | function | - | - | Deferred: device identification |
| SDL_GetGamepadProductForID | function | - | - | Deferred: device identification |
| SDL_GetGamepadProductVersionForID | function | - | - | Deferred: device identification |
| SDL_GetRealGamepadTypeForID | function | - | - | Deferred: device identification |
| SDL_GetGamepadPath | function | - | - | Deferred: device identification |
| SDL_GetRealGamepadType | function | - | - | Deferred: device identification |
| SDL_GetGamepadVendor | function | - | - | Deferred: device identification |
| SDL_GetGamepadProduct | function | - | - | Deferred: device identification |
| SDL_GetGamepadProductVersion | function | - | - | Deferred: device identification |
| SDL_GetGamepadFirmwareVersion | function | - | - | Deferred: device identification |
| SDL_GetGamepadSerial | function | - | - | Deferred: device identification |
| SDL_GetGamepadSteamHandle | function | - | - | Deferred: Steam handle |
| SDL_GetGamepadPlayerIndexForID | function | - | - | Deferred: player index |
| SDL_GetGamepadFromPlayerIndex | function | - | - | Deferred: player index |
| SDL_GetGamepadPlayerIndex | function | - | - | Deferred: player index |
| SDL_SetGamepadPlayerIndex | function | - | - | Deferred: player index |
| SDL_GetGamepadProperties | function | - | - | Deferred: properties |
| SDL_PROP_GAMEPAD_CAP_*_BOOLEAN | macro (5) | - | - | Deferred: properties; property string constants |
| SDL_GetGamepadID | function | - | - | Deferred |
| SDL_GamepadConnected | function | - | - | Deferred |
| SDL_GetGamepadPowerInfo | function | - | - | Deferred: power |
| SDL_SetGamepadEventsEnabled | function | - | - | Deferred: event state toggles |
| SDL_GamepadEventsEnabled | function | - | - | Deferred: event state toggles |
| SDL_GetGamepadBindings | function | - | - | Deferred: binding introspection |
| SDL_GetGamepadTypeFromString | function | - | - | Deferred: string conversion |
| SDL_GetGamepadStringForType | function | - | - | Deferred: string conversion |
| SDL_GetGamepadAxisFromString | function | - | - | Deferred: string conversion |
| SDL_GetGamepadStringForAxis | function | - | - | Deferred: string conversion |
| SDL_GetGamepadButtonFromString | function | - | - | Deferred: string conversion |
| SDL_GetGamepadStringForButton | function | - | - | Deferred: string conversion |
| SDL_GamepadHasAxis | function | - | - | Deferred: capability queries |
| SDL_GamepadHasButton | function | - | - | Deferred: capability queries |
| SDL_GetGamepadButtonLabelForType | function | - | - | Deferred |
| SDL_GetNumGamepadTouchpads | function | - | - | Deferred: touchpad |
| SDL_GetNumGamepadTouchpadFingers | function | - | - | Deferred: touchpad |
| SDL_GetGamepadTouchpadFinger | function | - | - | Deferred: touchpad |
| SDL_GamepadHasSensor | function | - | - | Deferred: sensors |
| SDL_SetGamepadSensorEnabled | function | - | - | Deferred: sensors |
| SDL_GamepadSensorEnabled | function | - | - | Deferred: sensors |
| SDL_GetGamepadSensorDataRate | function | - | - | Deferred: sensors |
| SDL_GetGamepadSensorData | function | - | - | Deferred: sensors |
| SDL_RumbleGamepad | function | - | - | Deferred: rumble/LED/effects |
| SDL_RumbleGamepadTriggers | function | - | - | Deferred: rumble/LED/effects |
| SDL_SetGamepadLED | function | - | - | Deferred: rumble/LED/effects |
| SDL_SendGamepadEffect | function | - | - | Deferred: rumble/LED/effects |
| SDL_GetGamepadAppleSFSymbolsNameForButton | function | - | - | Platform: Apple SF Symbols |
| SDL_GetGamepadAppleSFSymbolsNameForAxis | function | - | - | Platform: Apple SF Symbols |

## SDL_gpu.h

Enum values omitted for brevity — all values are wrapped 1:1 between native and managed.

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_GPUDevice | opaque | Gpu.SDL_GPUDevice | GpuDevice | |
| SDL_GPUBuffer | opaque | Gpu.SDL_GPUBuffer | GpuBuffer | |
| SDL_GPUTransferBuffer | opaque | Gpu.SDL_GPUTransferBuffer | GpuTransferBuffer | |
| SDL_GPUTexture | opaque | Gpu.SDL_GPUTexture | GpuTexture | |
| SDL_GPUSampler | opaque | Gpu.SDL_GPUSampler | GpuSampler | |
| SDL_GPUShader | opaque | Gpu.SDL_GPUShader | GpuShader | |
| SDL_GPUComputePipeline | opaque | Gpu.SDL_GPUComputePipeline | GpuComputePipeline | |
| SDL_GPUGraphicsPipeline | opaque | Gpu.SDL_GPUGraphicsPipeline | GpuGraphicsPipeline | |
| SDL_GPUCommandBuffer | opaque | Gpu.SDL_GPUCommandBuffer | GpuCommandBuffer | |
| SDL_GPURenderPass | opaque | Gpu.SDL_GPURenderPass | GpuRenderPass | |
| SDL_GPUComputePass | opaque | Gpu.SDL_GPUComputePass | GpuComputePass | |
| SDL_GPUCopyPass | opaque | Gpu.SDL_GPUCopyPass | GpuCopyPass | |
| SDL_GPUFence | opaque | Gpu.SDL_GPUFence | GpuFence | |
| SDL_GPUPrimitiveType | enum | Gpu.SDL_GPUPrimitiveType | GpuPrimitiveType | |
| SDL_GPULoadOp | enum | Gpu.SDL_GPULoadOp | GpuLoadOp | |
| SDL_GPUStoreOp | enum | Gpu.SDL_GPUStoreOp | GpuStoreOp | |
| SDL_GPUIndexElementSize | enum | Gpu.SDL_GPUIndexElementSize | GpuIndexElementSize | |
| SDL_GPUTextureFormat | enum | Gpu.SDL_GPUTextureFormat | GpuTextureFormat | |
| SDL_GPUTextureType | enum | Gpu.SDL_GPUTextureType | GpuTextureType | |
| SDL_GPUSampleCount | enum | Gpu.SDL_GPUSampleCount | GpuSampleCount | |
| SDL_GPUCubeMapFace | enum | Gpu.SDL_GPUCubeMapFace | GpuCubeMapFace | |
| SDL_GPUTransferBufferUsage | enum | Gpu.SDL_GPUTransferBufferUsage | GpuTransferBufferUsage | |
| SDL_GPUShaderStage | enum | Gpu.SDL_GPUShaderStage | GpuShaderStage | |
| SDL_GPUVertexElementFormat | enum | Gpu.SDL_GPUVertexElementFormat | GpuVertexElementFormat | |
| SDL_GPUVertexInputRate | enum | Gpu.SDL_GPUVertexInputRate | GpuVertexInputRate | |
| SDL_GPUFillMode | enum | Gpu.SDL_GPUFillMode | GpuFillMode | |
| SDL_GPUCullMode | enum | Gpu.SDL_GPUCullMode | GpuCullMode | |
| SDL_GPUFrontFace | enum | Gpu.SDL_GPUFrontFace | GpuFrontFace | |
| SDL_GPUCompareOp | enum | Gpu.SDL_GPUCompareOp | GpuCompareOp | |
| SDL_GPUStencilOp | enum | Gpu.SDL_GPUStencilOp | GpuStencilOp | |
| SDL_GPUBlendOp | enum | Gpu.SDL_GPUBlendOp | GpuBlendOp | |
| SDL_GPUBlendFactor | enum | Gpu.SDL_GPUBlendFactor | GpuBlendFactor | |
| SDL_GPUFilter | enum | Gpu.SDL_GPUFilter | GpuFilter | |
| SDL_GPUSamplerMipmapMode | enum | Gpu.SDL_GPUSamplerMipmapMode | GpuSamplerMipmapMode | |
| SDL_GPUSamplerAddressMode | enum | Gpu.SDL_GPUSamplerAddressMode | GpuSamplerAddressMode | |
| SDL_GPUPresentMode | enum | Gpu.SDL_GPUPresentMode | GpuPresentMode | |
| SDL_GPUSwapchainComposition | enum | Gpu.SDL_GPUSwapchainComposition | GpuSwapchainComposition | |
| SDL_GPUTextureUsageFlags | flags | Gpu.SDL_GPUTextureUsageFlags | GpuTextureUsage | |
| SDL_GPUBufferUsageFlags | flags | Gpu.SDL_GPUBufferUsageFlags | GpuBufferUsage | |
| SDL_GPUShaderFormat | flags | Gpu.SDL_GPUShaderFormat | GpuShaderFormat | |
| SDL_GPUColorComponentFlags | flags | Gpu.SDL_GPUColorComponentFlags | GpuColorComponentFlags | |
| SDL_GPUViewport | struct | Gpu.SDL_GPUViewport | GpuViewport | |
| SDL_GPUTextureTransferInfo | struct | Gpu.SDL_GPUTextureTransferInfo | GpuTextureTransferInfo | |
| SDL_GPUTransferBufferLocation | struct | Gpu.SDL_GPUTransferBufferLocation | GpuTransferBufferLocation | |
| SDL_GPUTextureLocation | struct | Gpu.SDL_GPUTextureLocation | GpuTextureLocation | |
| SDL_GPUTextureRegion | struct | Gpu.SDL_GPUTextureRegion | GpuTextureRegion | |
| SDL_GPUBlitRegion | struct | Gpu.SDL_GPUBlitRegion | GpuBlitRegion | |
| SDL_GPUBufferLocation | struct | Gpu.SDL_GPUBufferLocation | GpuBufferLocation | |
| SDL_GPUBufferRegion | struct | Gpu.SDL_GPUBufferRegion | GpuBufferRegion | |
| SDL_GPUIndirectDrawCommand | struct | Gpu.SDL_GPUIndirectDrawCommand | GpuIndirectDrawCommand | |
| SDL_GPUIndexedIndirectDrawCommand | struct | Gpu.SDL_GPUIndexedIndirectDrawCommand | GpuIndexedIndirectDrawCommand | |
| SDL_GPUIndirectDispatchCommand | struct | Gpu.SDL_GPUIndirectDispatchCommand | GpuIndirectDispatchCommand | |
| SDL_GPUSamplerCreateInfo | struct | Gpu.SDL_GPUSamplerCreateInfo | GpuSamplerCreateInfo | |
| SDL_GPUVertexBufferDescription | struct | Gpu.SDL_GPUVertexBufferDescription | GpuVertexBufferDescription | |
| SDL_GPUVertexAttribute | struct | Gpu.SDL_GPUVertexAttribute | GpuVertexAttribute | |
| SDL_GPUVertexInputState | struct | Gpu.SDL_GPUVertexInputState | GpuVertexInputState | |
| SDL_GPUStencilOpState | struct | Gpu.SDL_GPUStencilOpState | GpuStencilOpState | |
| SDL_GPUColorTargetBlendState | struct | Gpu.SDL_GPUColorTargetBlendState | GpuColorTargetBlendState | |
| SDL_GPUShaderCreateInfo | struct | Gpu.SDL_GPUShaderCreateInfo | GpuShaderCreateInfo | |
| SDL_GPUTextureCreateInfo | struct | Gpu.SDL_GPUTextureCreateInfo | GpuTextureCreateInfo | |
| SDL_GPUBufferCreateInfo | struct | Gpu.SDL_GPUBufferCreateInfo | GpuBufferCreateInfo | |
| SDL_GPUTransferBufferCreateInfo | struct | Gpu.SDL_GPUTransferBufferCreateInfo | GpuTransferBufferCreateInfo | |
| SDL_GPURasterizerState | struct | Gpu.SDL_GPURasterizerState | GpuRasterizerState | |
| SDL_GPUMultisampleState | struct | Gpu.SDL_GPUMultisampleState | GpuMultisampleState | |
| SDL_GPUDepthStencilState | struct | Gpu.SDL_GPUDepthStencilState | GpuDepthStencilState | |
| SDL_GPUColorTargetDescription | struct | Gpu.SDL_GPUColorTargetDescription | GpuColorTargetDescription | |
| SDL_GPUGraphicsPipelineTargetInfo | struct | Gpu.SDL_GPUGraphicsPipelineTargetInfo | GpuGraphicsPipelineTargetInfo | |
| SDL_GPUGraphicsPipelineCreateInfo | struct | Gpu.SDL_GPUGraphicsPipelineCreateInfo | GpuGraphicsPipelineCreateInfo | |
| SDL_GPUComputePipelineCreateInfo | struct | Gpu.SDL_GPUComputePipelineCreateInfo | GpuComputePipelineCreateInfo | |
| SDL_GPUColorTargetInfo | struct | Gpu.SDL_GPUColorTargetInfo | GpuColorTargetInfo | |
| SDL_GPUDepthStencilTargetInfo | struct | Gpu.SDL_GPUDepthStencilTargetInfo | GpuDepthStencilTargetInfo | |
| SDL_GPUBlitInfo | struct | Gpu.SDL_GPUBlitInfo | GpuBlitInfo | |
| SDL_GPUBufferBinding | struct | Gpu.SDL_GPUBufferBinding | GpuBufferBinding | |
| SDL_GPUTextureSamplerBinding | struct | Gpu.SDL_GPUTextureSamplerBinding | GpuTextureSamplerBinding | |
| SDL_GPUStorageBufferReadWriteBinding | struct | Gpu.SDL_GPUStorageBufferReadWriteBinding | GpuStorageBufferReadWriteBinding | |
| SDL_GPUStorageTextureReadWriteBinding | struct | Gpu.SDL_GPUStorageTextureReadWriteBinding | GpuStorageTextureReadWriteBinding | |
| SDL_GPUVulkanOptions | struct | - | - | deferred: advanced Vulkan config (SDL 3.4), skip-commented in Native/Gpu.cs |
| SDL_GPUSupportsShaderFormats | function | Gpu.SDL_GPUSupportsShaderFormats | GpuDevice.SupportsShaderFormats | |
| SDL_CreateGPUDevice | function | Gpu.SDL_CreateGPUDevice | GpuDevice.Create | |
| SDL_CreateGPUDeviceWithProperties | function | Gpu.SDL_CreateGPUDeviceWithProperties | GpuDevice.Create(PropertyGroup) | |
| SDL_PROP_GPU_DEVICE_CREATE_* | macro (21) | Gpu.SDL_PROP_GPU_DEVICE_CREATE_* | GpuDeviceProperties.* | |
| SDL_DestroyGPUDevice | function | Gpu.SDL_DestroyGPUDevice | GpuDevice.Dispose | |
| SDL_GetNumGPUDrivers | function | Gpu.SDL_GetNumGPUDrivers | GpuDevice.NumDrivers | |
| SDL_GetGPUDriver | function | Gpu.SDL_GetGPUDriver | GpuDevice.GetDriver | |
| SDL_GetGPUDeviceDriver | function | Gpu.SDL_GetGPUDeviceDriver | GpuDevice.Driver | |
| SDL_GetGPUShaderFormats | function | Gpu.SDL_GetGPUShaderFormats | GpuDevice.ShaderFormats | |
| SDL_GetGPUDeviceProperties | function | Gpu.SDL_GetGPUDeviceProperties | GpuDevice.Properties | |
| SDL_PROP_GPU_DEVICE_*_STRING | macro (4) | Gpu.SDL_PROP_GPU_DEVICE_*_STRING | - | Property string constants |
| SDL_CreateGPUComputePipeline | function | Gpu.SDL_CreateGPUComputePipeline | GpuDevice.CreateComputePipeline | |
| SDL_CreateGPUGraphicsPipeline | function | Gpu.SDL_CreateGPUGraphicsPipeline | GpuDevice.CreateGraphicsPipeline | |
| SDL_CreateGPUSampler | function | Gpu.SDL_CreateGPUSampler | GpuDevice.CreateSampler | |
| SDL_CreateGPUShader | function | Gpu.SDL_CreateGPUShader | GpuDevice.CreateShader | |
| SDL_CreateGPUTexture | function | Gpu.SDL_CreateGPUTexture | GpuDevice.CreateTexture | |
| SDL_CreateGPUBuffer | function | Gpu.SDL_CreateGPUBuffer | GpuDevice.CreateBuffer | |
| SDL_CreateGPUTransferBuffer | function | Gpu.SDL_CreateGPUTransferBuffer | GpuDevice.CreateTransferBuffer | |
| SDL_PROP_GPU_*_CREATE_NAME_STRING | macro (7) | - | - | niche — debug-name create properties; SetName covers buffers/textures at runtime |
| SDL_PROP_GPU_TEXTURE_CREATE_D3D12_* | macro (6) | - | - | platform: D3D12-only clear-value hints; niche |
| SDL_SetGPUBufferName | function | Gpu.SDL_SetGPUBufferName | GpuBuffer.SetName | |
| SDL_SetGPUTextureName | function | Gpu.SDL_SetGPUTextureName | GpuTexture.SetName | |
| SDL_InsertGPUDebugLabel | function | Gpu.SDL_InsertGPUDebugLabel | GpuCommandBuffer.InsertDebugLabel | |
| SDL_PushGPUDebugGroup | function | Gpu.SDL_PushGPUDebugGroup | GpuCommandBuffer.PushDebugGroup | |
| SDL_PopGPUDebugGroup | function | Gpu.SDL_PopGPUDebugGroup | GpuCommandBuffer.PopDebugGroup | |
| SDL_ReleaseGPUTexture | function | Gpu.SDL_ReleaseGPUTexture | GpuTexture.Dispose | |
| SDL_ReleaseGPUSampler | function | Gpu.SDL_ReleaseGPUSampler | GpuSampler.Dispose | |
| SDL_ReleaseGPUBuffer | function | Gpu.SDL_ReleaseGPUBuffer | GpuBuffer.Dispose | |
| SDL_ReleaseGPUTransferBuffer | function | Gpu.SDL_ReleaseGPUTransferBuffer | GpuTransferBuffer.Dispose | |
| SDL_ReleaseGPUComputePipeline | function | Gpu.SDL_ReleaseGPUComputePipeline | GpuComputePipeline.Dispose | |
| SDL_ReleaseGPUShader | function | Gpu.SDL_ReleaseGPUShader | GpuShader.Dispose | |
| SDL_ReleaseGPUGraphicsPipeline | function | Gpu.SDL_ReleaseGPUGraphicsPipeline | GpuGraphicsPipeline.Dispose | |
| SDL_AcquireGPUCommandBuffer | function | Gpu.SDL_AcquireGPUCommandBuffer | GpuDevice.AcquireCommandBuffer | |
| SDL_PushGPUVertexUniformData | function | Gpu.SDL_PushGPUVertexUniformData | GpuCommandBuffer.PushVertexUniformData | |
| SDL_PushGPUFragmentUniformData | function | Gpu.SDL_PushGPUFragmentUniformData | GpuCommandBuffer.PushFragmentUniformData | |
| SDL_PushGPUComputeUniformData | function | Gpu.SDL_PushGPUComputeUniformData | GpuCommandBuffer.PushComputeUniformData | |
| SDL_BeginGPURenderPass | function | Gpu.SDL_BeginGPURenderPass | GpuCommandBuffer.BeginRenderPass | |
| SDL_BindGPUGraphicsPipeline | function | Gpu.SDL_BindGPUGraphicsPipeline | GpuRenderPass.BindGraphicsPipeline | |
| SDL_SetGPUViewport | function | Gpu.SDL_SetGPUViewport | GpuRenderPass.SetViewport | |
| SDL_SetGPUScissor | function | Gpu.SDL_SetGPUScissor | GpuRenderPass.SetScissor | |
| SDL_SetGPUBlendConstants | function | Gpu.SDL_SetGPUBlendConstants | GpuRenderPass.SetBlendConstants | |
| SDL_SetGPUStencilReference | function | Gpu.SDL_SetGPUStencilReference | GpuRenderPass.SetStencilReference | |
| SDL_BindGPUVertexBuffers | function | Gpu.SDL_BindGPUVertexBuffers | GpuRenderPass.BindVertexBuffers | |
| SDL_BindGPUIndexBuffer | function | Gpu.SDL_BindGPUIndexBuffer | GpuRenderPass.BindIndexBuffer | |
| SDL_BindGPUVertexSamplers | function | Gpu.SDL_BindGPUVertexSamplers | GpuRenderPass.BindVertexSamplers | |
| SDL_BindGPUVertexStorageTextures | function | Gpu.SDL_BindGPUVertexStorageTextures | GpuRenderPass.BindVertexStorageTextures | |
| SDL_BindGPUVertexStorageBuffers | function | Gpu.SDL_BindGPUVertexStorageBuffers | GpuRenderPass.BindVertexStorageBuffers | |
| SDL_BindGPUFragmentSamplers | function | Gpu.SDL_BindGPUFragmentSamplers | GpuRenderPass.BindFragmentSamplers | |
| SDL_BindGPUFragmentStorageTextures | function | Gpu.SDL_BindGPUFragmentStorageTextures | GpuRenderPass.BindFragmentStorageTextures | |
| SDL_BindGPUFragmentStorageBuffers | function | Gpu.SDL_BindGPUFragmentStorageBuffers | GpuRenderPass.BindFragmentStorageBuffers | |
| SDL_DrawGPUPrimitives | function | Gpu.SDL_DrawGPUPrimitives | GpuRenderPass.DrawPrimitives | |
| SDL_DrawGPUIndexedPrimitives | function | Gpu.SDL_DrawGPUIndexedPrimitives | GpuRenderPass.DrawIndexedPrimitives | |
| SDL_DrawGPUPrimitivesIndirect | function | Gpu.SDL_DrawGPUPrimitivesIndirect | GpuRenderPass.DrawPrimitivesIndirect | |
| SDL_DrawGPUIndexedPrimitivesIndirect | function | Gpu.SDL_DrawGPUIndexedPrimitivesIndirect | GpuRenderPass.DrawIndexedPrimitivesIndirect | |
| SDL_EndGPURenderPass | function | Gpu.SDL_EndGPURenderPass | GpuRenderPass.End | |
| SDL_BeginGPUComputePass | function | Gpu.SDL_BeginGPUComputePass | GpuCommandBuffer.BeginComputePass | |
| SDL_BindGPUComputePipeline | function | Gpu.SDL_BindGPUComputePipeline | GpuComputePass.BindPipeline | |
| SDL_BindGPUComputeSamplers | function | Gpu.SDL_BindGPUComputeSamplers | GpuComputePass.BindSamplers | |
| SDL_BindGPUComputeStorageTextures | function | Gpu.SDL_BindGPUComputeStorageTextures | GpuComputePass.BindStorageTextures | |
| SDL_BindGPUComputeStorageBuffers | function | Gpu.SDL_BindGPUComputeStorageBuffers | GpuComputePass.BindStorageBuffers | |
| SDL_DispatchGPUCompute | function | Gpu.SDL_DispatchGPUCompute | GpuComputePass.Dispatch | |
| SDL_DispatchGPUComputeIndirect | function | Gpu.SDL_DispatchGPUComputeIndirect | GpuComputePass.DispatchIndirect | |
| SDL_EndGPUComputePass | function | Gpu.SDL_EndGPUComputePass | GpuComputePass.End | |
| SDL_MapGPUTransferBuffer | function | Gpu.SDL_MapGPUTransferBuffer | GpuTransferBuffer.Map | |
| SDL_UnmapGPUTransferBuffer | function | Gpu.SDL_UnmapGPUTransferBuffer | GpuTransferBuffer.Unmap | |
| SDL_BeginGPUCopyPass | function | Gpu.SDL_BeginGPUCopyPass | GpuCommandBuffer.BeginCopyPass | |
| SDL_UploadToGPUTexture | function | Gpu.SDL_UploadToGPUTexture | GpuCopyPass.UploadToTexture | |
| SDL_UploadToGPUBuffer | function | Gpu.SDL_UploadToGPUBuffer | GpuCopyPass.UploadToBuffer | |
| SDL_CopyGPUTextureToTexture | function | Gpu.SDL_CopyGPUTextureToTexture | GpuCopyPass.CopyTextureToTexture | |
| SDL_CopyGPUBufferToBuffer | function | Gpu.SDL_CopyGPUBufferToBuffer | GpuCopyPass.CopyBufferToBuffer | |
| SDL_DownloadFromGPUTexture | function | Gpu.SDL_DownloadFromGPUTexture | GpuCopyPass.DownloadFromTexture | |
| SDL_DownloadFromGPUBuffer | function | Gpu.SDL_DownloadFromGPUBuffer | GpuCopyPass.DownloadFromBuffer | |
| SDL_EndGPUCopyPass | function | Gpu.SDL_EndGPUCopyPass | GpuCopyPass.End | |
| SDL_GenerateMipmapsForGPUTexture | function | Gpu.SDL_GenerateMipmapsForGPUTexture | GpuCommandBuffer.GenerateMipmaps | |
| SDL_BlitGPUTexture | function | Gpu.SDL_BlitGPUTexture | GpuCommandBuffer.Blit | |
| SDL_WindowSupportsGPUSwapchainComposition | function | Gpu.SDL_WindowSupportsGPUSwapchainComposition | GpuDevice.WindowSupportsSwapchainComposition | |
| SDL_WindowSupportsGPUPresentMode | function | Gpu.SDL_WindowSupportsGPUPresentMode | GpuDevice.WindowSupportsPresentMode | |
| SDL_ClaimWindowForGPUDevice | function | Gpu.SDL_ClaimWindowForGPUDevice | GpuDevice.ClaimWindow | |
| SDL_ReleaseWindowFromGPUDevice | function | Gpu.SDL_ReleaseWindowFromGPUDevice | GpuDevice.ReleaseWindow | |
| SDL_SetGPUSwapchainParameters | function | Gpu.SDL_SetGPUSwapchainParameters | GpuDevice.SetSwapchainParameters | |
| SDL_SetGPUAllowedFramesInFlight | function | Gpu.SDL_SetGPUAllowedFramesInFlight | GpuDevice.SetAllowedFramesInFlight | |
| SDL_GetGPUSwapchainTextureFormat | function | Gpu.SDL_GetGPUSwapchainTextureFormat | GpuDevice.GetSwapchainTextureFormat | |
| SDL_AcquireGPUSwapchainTexture | function | Gpu.SDL_AcquireGPUSwapchainTexture | GpuCommandBuffer.AcquireSwapchainTexture | |
| SDL_WaitForGPUSwapchain | function | Gpu.SDL_WaitForGPUSwapchain | GpuDevice.WaitForSwapchain | |
| SDL_WaitAndAcquireGPUSwapchainTexture | function | Gpu.SDL_WaitAndAcquireGPUSwapchainTexture | GpuCommandBuffer.WaitAndAcquireSwapchainTexture | |
| SDL_SubmitGPUCommandBuffer | function | Gpu.SDL_SubmitGPUCommandBuffer | GpuCommandBuffer.Submit | |
| SDL_SubmitGPUCommandBufferAndAcquireFence | function | Gpu.SDL_SubmitGPUCommandBufferAndAcquireFence | GpuCommandBuffer.SubmitAndAcquireFence | |
| SDL_CancelGPUCommandBuffer | function | Gpu.SDL_CancelGPUCommandBuffer | GpuCommandBuffer.Cancel | |
| SDL_WaitForGPUIdle | function | Gpu.SDL_WaitForGPUIdle | GpuDevice.WaitForIdle | |
| SDL_WaitForGPUFences | function | Gpu.SDL_WaitForGPUFences | GpuFence.WaitAll / GpuFence.WaitAny | |
| SDL_QueryGPUFence | function | Gpu.SDL_QueryGPUFence | GpuFence.IsSignaled | |
| SDL_ReleaseGPUFence | function | Gpu.SDL_ReleaseGPUFence | GpuFence.Dispose | |
| SDL_GPUTextureFormatTexelBlockSize | function | Gpu.SDL_GPUTextureFormatTexelBlockSize | GpuTextureFormatExtensions.TexelBlockSize | |
| SDL_GPUTextureSupportsFormat | function | Gpu.SDL_GPUTextureSupportsFormat | GpuDevice.SupportsTextureFormat | |
| SDL_GPUTextureSupportsSampleCount | function | Gpu.SDL_GPUTextureSupportsSampleCount | GpuDevice.SupportsSampleCount | |
| SDL_CalculateGPUTextureFormatSize | function | Gpu.SDL_CalculateGPUTextureFormatSize | GpuTextureFormatExtensions.CalculateSize | |
| SDL_GPUSupportsProperties | function | - | - | deferred: rarely needed |
| SDL_GetPixelFormatFromGPUTextureFormat | function | - | - | deferred |
| SDL_GetGPUTextureFormatFromPixelFormat | function | - | - | deferred |
| SDL_GDKSuspendGPU | function | - | - | Platform: Xbox GDK only |
| SDL_GDKResumeGPU | function | - | - | Platform: Xbox GDK only |

## SDL_guid.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_GUID | struct | - | - | .NET has System.Guid |
| SDL_GUIDToString | function | - | - | .NET has Guid.ToString |
| SDL_StringToGUID | function | - | - | .NET has Guid.Parse |

## SDL_haptic.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_HapticID | typedef | Haptic.SDL_HapticID | - | Internal ID type |
| SDL_Haptic | struct | Haptic.SDL_Haptic | Haptic | |
| SDL_HAPTIC_INFINITY | constant | - | - | Deferred: "repeat forever" length/iterations value |
| SDL_HapticEffectType | typedef | - | - | Deferred: complex effect struct union system |
| SDL_HAPTIC_CONSTANT … SDL_HAPTIC_CUSTOM (13 effect-type flag bits) | constant | - | - | Deferred: complex effect struct union system |
| SDL_HAPTIC_RESERVED1/2/3 | constant | - | - | internal — reserved for future use |
| SDL_HAPTIC_GAIN / AUTOCENTER / STATUS / PAUSE (4 feature bits) | constant | - | - | Deferred: device-capability bits for SDL_GetHapticFeatures |
| SDL_HapticDirectionType | typedef | - | - | Deferred: complex effect struct union system |
| SDL_HAPTIC_POLAR / CARTESIAN / SPHERICAL / STEERING_AXIS (4 direction encodings) | constant | - | - | Deferred: complex effect struct union system |
| SDL_HapticEffectID | typedef | - | - | Deferred: effect handle used by the effect system |
| SDL_HapticDirection | struct | - | - | Deferred: complex effect struct union system |
| SDL_HapticConstant | struct | - | - | Deferred: complex effect struct union system |
| SDL_HapticPeriodic | struct | - | - | Deferred: complex effect struct union system |
| SDL_HapticCondition | struct | - | - | Deferred: complex effect struct union system |
| SDL_HapticRamp | struct | - | - | Deferred: complex effect struct union system |
| SDL_HapticLeftRight | struct | - | - | Deferred: complex effect struct union system |
| SDL_HapticCustom | struct | - | - | Deferred: complex effect struct union system |
| SDL_HapticEffect | union | - | - | Deferred: complex union with ~10 effect structs |
| SDL_GetHaptics | function | Haptic.SDL_GetHaptics | Haptic.GetDevices | |
| SDL_GetHapticNameForID | function | Haptic.SDL_GetHapticNameForID | Haptic.GetName | |
| SDL_OpenHaptic | function | Haptic.SDL_OpenHaptic | Haptic.Open | |
| SDL_GetHapticFromID | function | - | - | Deferred: lookup of already-open device by instance ID |
| SDL_GetHapticID | function | Haptic.SDL_GetHapticID | - | Deferred: binding exists but no managed Haptic.Id property |
| SDL_GetHapticName | function | Haptic.SDL_GetHapticName | Haptic.Name | |
| SDL_IsMouseHaptic | function | - | - | Deferred: pre-open capability check (haptic mice rare) |
| SDL_OpenHapticFromMouse | function | Haptic.SDL_OpenHapticFromMouse | Haptic.OpenFromMouse | |
| SDL_IsJoystickHaptic | function | - | - | Deferred: pre-open capability check for enumeration UIs |
| SDL_OpenHapticFromJoystick | function | Haptic.SDL_OpenHapticFromJoystick | Haptic.OpenFromJoystick | |
| SDL_CloseHaptic | function | Haptic.SDL_CloseHaptic | Haptic.Dispose | |
| SDL_GetMaxHapticEffects | function | - | - | Deferred: full effect system |
| SDL_GetMaxHapticEffectsPlaying | function | - | - | Deferred: full effect system |
| SDL_GetHapticFeatures | function | - | - | Deferred: full effect system |
| SDL_GetNumHapticAxes | function | - | - | Deferred: full effect system |
| SDL_HapticEffectSupported | function | - | - | Deferred: full effect system |
| SDL_CreateHapticEffect | function | - | - | Deferred: full effect system |
| SDL_UpdateHapticEffect | function | - | - | Deferred: full effect system |
| SDL_RunHapticEffect | function | - | - | Deferred: full effect system |
| SDL_StopHapticEffect | function | - | - | Deferred: full effect system |
| SDL_DestroyHapticEffect | function | - | - | Deferred: full effect system |
| SDL_GetHapticEffectStatus | function | - | - | Deferred: full effect system |
| SDL_SetHapticGain | function | - | - | Deferred: full effect system |
| SDL_SetHapticAutocenter | function | - | - | Deferred: full effect system |
| SDL_PauseHaptic | function | - | - | Deferred: full effect system |
| SDL_ResumeHaptic | function | - | - | Deferred: full effect system |
| SDL_StopHapticEffects | function | - | - | Deferred: full effect system |
| SDL_HapticRumbleSupported | function | Haptic.SDL_HapticRumbleSupported | Haptic.RumbleSupported | |
| SDL_InitHapticRumble | function | Haptic.SDL_InitHapticRumble | Haptic.InitRumble | |
| SDL_PlayHapticRumble | function | Haptic.SDL_PlayHapticRumble | Haptic.PlayRumble | |
| SDL_StopHapticRumble | function | Haptic.SDL_StopHapticRumble | Haptic.StopRumble | |

## SDL_hidapi.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_hid_device | struct | - | - | Niche: low-level HID, use gamepad/joystick APIs instead |
| SDL_hid_bus_type | enum | - | - | Niche: low-level HID |
| SDL_hid_device_info | struct | - | - | Niche: low-level HID |
| SDL_hid_init | function | - | - | Niche: low-level HID |
| SDL_hid_exit | function | - | - | Niche: low-level HID |
| SDL_hid_device_change_count | function | - | - | Niche: low-level HID |
| SDL_hid_enumerate | function | - | - | Niche: low-level HID |
| SDL_hid_free_enumeration | function | - | - | Niche: low-level HID |
| SDL_hid_open | function | - | - | Niche: low-level HID |
| SDL_hid_open_path | function | - | - | Niche: low-level HID |
| SDL_hid_get_properties | function | - | - | Niche: low-level HID |
| SDL_PROP_HIDAPI_LIBUSB_DEVICE_HANDLE_POINTER | constant | - | - | Niche: low-level HID, libusb backend only |
| SDL_hid_write | function | - | - | Niche: low-level HID |
| SDL_hid_read_timeout | function | - | - | Niche: low-level HID |
| SDL_hid_read | function | - | - | Niche: low-level HID |
| SDL_hid_set_nonblocking | function | - | - | Niche: low-level HID |
| SDL_hid_send_feature_report | function | - | - | Niche: low-level HID |
| SDL_hid_get_feature_report | function | - | - | Niche: low-level HID |
| SDL_hid_get_input_report | function | - | - | Niche: low-level HID |
| SDL_hid_close | function | - | - | Niche: low-level HID |
| SDL_hid_get_manufacturer_string | function | - | - | Niche: low-level HID |
| SDL_hid_get_product_string | function | - | - | Niche: low-level HID |
| SDL_hid_get_serial_number_string | function | - | - | Niche: low-level HID |
| SDL_hid_get_indexed_string | function | - | - | Niche: low-level HID |
| SDL_hid_get_device_info | function | - | - | Niche: low-level HID |
| SDL_hid_get_report_descriptor | function | - | - | Niche: low-level HID |
| SDL_hid_ble_scan | function | - | - | Platform: iOS/tvOS BLE scan for Steam Controllers; niche low-level HID |

## SDL_hints.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_HintPriority | enum | Hints.SDL_HintPriority | - | deferred: no public HintPriority enum |
| SDL_SetHintWithPriority | function | Hints.SDL_SetHintWithPriority | - | deferred: no managed priority overload |
| SDL_SetHint | function | Hints.SDL_SetHint | SdlHints.Set | |
| SDL_ResetHint | function | Hints.SDL_ResetHint | SdlHints.Reset | |
| SDL_ResetHints | function | Hints.SDL_ResetHints | SdlHints.ResetAll | |
| SDL_GetHint | function | Hints.SDL_GetHint | SdlHints.Get | |
| SDL_GetHintBoolean | function | Hints.SDL_GetHintBoolean | SdlHints.GetBoolean | |
| SDL_HintCallback | callback | - | - | Deferred: callback-based hint watching |
| SDL_AddHintCallback | function | - | - | Deferred: callback-based hint watching |
| SDL_RemoveHintCallback | function | - | - | Deferred: callback-based hint watching |
| SDL_HINT_* | macro (261) | - | - | String constants passed via SdlHints.Set/Get |

## SDL_init.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_InitFlags | typedef | Init.SDL_InitFlags | InitFlags | |
| SDL_INIT_AUDIO | enum value | Init.SDL_InitFlags.SDL_INIT_AUDIO | InitFlags.Audio | |
| SDL_INIT_VIDEO | enum value | Init.SDL_InitFlags.SDL_INIT_VIDEO | InitFlags.Video | |
| SDL_INIT_JOYSTICK | enum value | Init.SDL_InitFlags.SDL_INIT_JOYSTICK | InitFlags.Joystick | |
| SDL_INIT_HAPTIC | enum value | Init.SDL_InitFlags.SDL_INIT_HAPTIC | InitFlags.Haptic | |
| SDL_INIT_GAMEPAD | enum value | Init.SDL_InitFlags.SDL_INIT_GAMEPAD | InitFlags.Gamepad | |
| SDL_INIT_EVENTS | enum value | Init.SDL_InitFlags.SDL_INIT_EVENTS | InitFlags.Events | |
| SDL_INIT_SENSOR | enum value | Init.SDL_InitFlags.SDL_INIT_SENSOR | InitFlags.Sensor | |
| SDL_INIT_CAMERA | enum value | Init.SDL_InitFlags.SDL_INIT_CAMERA | InitFlags.Camera | |
| SDL_AppResult | enum | - | - | App callbacks model: C# runtime owns main() |
| SDL_AppInit_func | callback | - | - | App callbacks model: C# runtime owns main() |
| SDL_AppIterate_func | callback | - | - | App callbacks model: C# runtime owns main() |
| SDL_AppEvent_func | callback | - | - | App callbacks model: C# runtime owns main() |
| SDL_AppQuit_func | callback | - | - | App callbacks model: C# runtime owns main() |
| SDL_Init | function | Init.SDL_Init | Application | |
| SDL_InitSubSystem | function | Init.SDL_InitSubSystem | Application.InitSubSystem | |
| SDL_QuitSubSystem | function | Init.SDL_QuitSubSystem | Application.QuitSubSystem | |
| SDL_WasInit | function | Init.SDL_WasInit | Application.WasInit | |
| SDL_Quit | function | Init.SDL_Quit | Application.Dispose | |
| SDL_IsMainThread | function | Init.SDL_IsMainThread | Application.IsMainThread | |
| SDL_MainThreadCallback | callback | Init.SDL_RunOnMainThread | Application.RunOnMainThread | Function-pointer parameter; surfaced as Action |
| SDL_RunOnMainThread | function | Init.SDL_RunOnMainThread | Application.RunOnMainThread | |
| SDL_SetAppMetadata | function | Init.SDL_SetAppMetadata | Application.SetMetadata | |
| SDL_SetAppMetadataProperty | function | Init.SDL_SetAppMetadataProperty | Application.SetMetadataProperty | |
| SDL_GetAppMetadataProperty | function | Init.SDL_GetAppMetadataProperty | Application.GetMetadataProperty | |
| SDL_PROP_APP_METADATA_*_STRING | macro (7) | Init.SDL_PROP_APP_METADATA_*_STRING | - | macro; pass name strings to Set/GetMetadataProperty |

## SDL_iostream.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_IOStream | struct | - | - | Deferred: needed by *_IO loaders (SDL_LoadWAV_IO, SDL_LoadBMP_IO, etc.) |
| SDL_IOStreamInterface | struct | - | - | Deferred: callback vtable for SDL_OpenIO |
| SDL_IOStatus | enum | - | - | .NET has System.IO |
| SDL_IOWhence | enum | - | - | .NET has System.IO.SeekOrigin |
| SDL_IOFromFile | function | - | - | .NET has FileStream |
| SDL_IOFromMem | function | - | - | Deferred: memory-buffer bridge for *_IO loaders |
| SDL_IOFromConstMem | function | - | - | Deferred: read-only memory bridge for *_IO loaders |
| SDL_IOFromDynamicMem | function | - | - | .NET has MemoryStream |
| SDL_OpenIO | function | - | - | Deferred: bridge from System.IO.Stream to SDL_IOStream |
| SDL_CloseIO | function | - | - | Deferred: companion to SDL_OpenIO/SDL_IOFrom* |
| SDL_GetIOProperties | function | - | - | niche |
| SDL_GetIOStatus | function | - | - | .NET has Stream error/EOF semantics |
| SDL_GetIOSize | function | - | - | .NET has Stream.Length |
| SDL_ReadIO | function | - | - | .NET has Stream.Read |
| SDL_WriteIO | function | - | - | .NET has Stream.Write |
| SDL_SeekIO | function | - | - | .NET has Stream.Seek |
| SDL_TellIO | function | - | - | .NET has Stream.Position |
| SDL_FlushIO | function | - | - | .NET has Stream.Flush |
| SDL_IOprintf | function | - | - | variadic |
| SDL_IOvprintf | function | - | - | variadic |
| SDL_LoadFile | function | - | - | .NET has File.ReadAllBytes |
| SDL_LoadFile_IO | function | - | - | .NET has Stream reads |
| SDL_SaveFile | function | - | - | .NET has File.WriteAllBytes |
| SDL_SaveFile_IO | function | - | - | .NET has Stream writes |
| SDL_ReadU8 .. SDL_WriteS64BE | function (28) | - | - | .NET has BinaryReader/BinaryWriter |
| SDL_PROP_IOSTREAM_* | macro (9) | - | - | niche: property names for SDL_GetIOProperties |

## SDL_joystick.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_Joystick | struct | Joystick.SDL_Joystick | Joystick | |
| SDL_JoystickID | typedef | Joystick.SDL_JoystickID | - | Internal ID type |
| SDL_JoystickType | enum | Joystick.SDL_JoystickType | JoystickType | |
| SDL_JoystickConnectionState | enum | Joystick.SDL_JoystickConnectionState | JoystickConnectionState | |
| SDL_VirtualJoystickTouchpadDesc | struct | - | - | Deferred: virtual joystick |
| SDL_VirtualJoystickSensorDesc | struct | - | - | Deferred: virtual joystick |
| SDL_VirtualJoystickDesc | struct | - | - | Deferred: virtual joystick (contains 8 callback pointers) |
| SDL_JOYSTICK_AXIS_MAX | constant | - | - | Deferred: axis range constant |
| SDL_JOYSTICK_AXIS_MIN | constant | - | - | Deferred: axis range constant |
| SDL_HAT_* | macro (9) | Joystick.SDL_HAT_* | HatPosition | |
| SDL_PROP_JOYSTICK_CAP_* | constant (5) | - | - | Deferred: capability properties |
| SDL_GetJoysticks | function | Joystick.SDL_GetJoysticks | Joystick.GetDevices | |
| SDL_GetJoystickNameForID | function | Joystick.SDL_GetJoystickNameForID | Joystick.GetName | |
| SDL_GetJoystickTypeForID | function | Joystick.SDL_GetJoystickTypeForID | Joystick.GetType | |
| SDL_OpenJoystick | function | Joystick.SDL_OpenJoystick | Joystick.Open | |
| SDL_GetJoystickFromID | function | Joystick.SDL_GetJoystickFromID | - | Deferred: managed exposure for event correlation |
| SDL_GetJoystickName | function | Joystick.SDL_GetJoystickName | Joystick.Name | |
| SDL_GetJoystickType | function | Joystick.SDL_GetJoystickType | Joystick.Type | |
| SDL_GetJoystickID | function | Joystick.SDL_GetJoystickID | - | Deferred: managed exposure for event correlation |
| SDL_GetNumJoystickAxes | function | Joystick.SDL_GetNumJoystickAxes | Joystick.NumAxes | |
| SDL_GetNumJoystickBalls | function | Joystick.SDL_GetNumJoystickBalls | Joystick.NumBalls | |
| SDL_GetNumJoystickHats | function | Joystick.SDL_GetNumJoystickHats | Joystick.NumHats | |
| SDL_GetNumJoystickButtons | function | Joystick.SDL_GetNumJoystickButtons | Joystick.NumButtons | |
| SDL_GetJoystickAxis | function | Joystick.SDL_GetJoystickAxis | Joystick.GetAxis | |
| SDL_GetJoystickHat | function | Joystick.SDL_GetJoystickHat | Joystick.GetHat | |
| SDL_GetJoystickButton | function | Joystick.SDL_GetJoystickButton | Joystick.GetButton | |
| SDL_GetJoystickConnectionState | function | Joystick.SDL_GetJoystickConnectionState | Joystick.ConnectionState | |
| SDL_CloseJoystick | function | Joystick.SDL_CloseJoystick | Joystick.Dispose | |
| SDL_UpdateJoysticks | function | Joystick.SDL_UpdateJoysticks | Joystick.Update | |
| SDL_LockJoysticks | function | - | - | Deferred: SDL joystick lock |
| SDL_UnlockJoysticks | function | - | - | Deferred: SDL joystick lock |
| SDL_HasJoystick | function | - | - | Deferred |
| SDL_JoystickConnected | function | - | - | Deferred |
| SDL_SetJoystickEventsEnabled | function | - | - | Deferred |
| SDL_JoystickEventsEnabled | function | - | - | Deferred |
| SDL_GetJoystickAxisInitialState | function | - | - | Deferred |
| SDL_GetJoystickBall | function | - | - | Deferred (NumBalls is exposed but ball motion is not) |
| SDL_GetJoystickPath | function | - | - | Deferred |
| SDL_GetJoystickPathForID | function | - | - | Deferred |
| SDL_GetJoystickPlayerIndex | function | - | - | Deferred: player index |
| SDL_SetJoystickPlayerIndex | function | - | - | Deferred: player index |
| SDL_GetJoystickPlayerIndexForID | function | - | - | Deferred: player index |
| SDL_GetJoystickFromPlayerIndex | function | - | - | Deferred: player index |
| SDL_GetJoystickGUID | function | - | - | Deferred: needs SDL_GUID (SDL_guid.h) |
| SDL_GetJoystickGUIDForID | function | - | - | Deferred: needs SDL_GUID (SDL_guid.h) |
| SDL_GetJoystickGUIDInfo | function | - | - | Deferred: needs SDL_GUID (SDL_guid.h) |
| SDL_GetJoystickVendor | function | - | - | Deferred: device identification |
| SDL_GetJoystickProduct | function | - | - | Deferred: device identification |
| SDL_GetJoystickProductVersion | function | - | - | Deferred: device identification |
| SDL_GetJoystickFirmwareVersion | function | - | - | Deferred: device identification |
| SDL_GetJoystickSerial | function | - | - | Deferred: device identification |
| SDL_GetJoystickVendorForID | function | - | - | Deferred: device identification |
| SDL_GetJoystickProductForID | function | - | - | Deferred: device identification |
| SDL_GetJoystickProductVersionForID | function | - | - | Deferred: device identification |
| SDL_GetJoystickProperties | function | - | - | Deferred: capability properties |
| SDL_RumbleJoystick | function | - | - | Deferred: rumble/LED/effects |
| SDL_RumbleJoystickTriggers | function | - | - | Deferred: rumble/LED/effects |
| SDL_SetJoystickLED | function | - | - | Deferred: rumble/LED/effects |
| SDL_SendJoystickEffect | function | - | - | Deferred: rumble/LED/effects |
| SDL_GetJoystickPowerInfo | function | - | - | Deferred: needs SDL_PowerState |
| SDL_AttachVirtualJoystick | function | - | - | Deferred: virtual joystick |
| SDL_DetachVirtualJoystick | function | - | - | Deferred: virtual joystick |
| SDL_IsJoystickVirtual | function | - | - | Deferred: virtual joystick |
| SDL_SetJoystickVirtualAxis | function | - | - | Deferred: virtual joystick |
| SDL_SetJoystickVirtualBall | function | - | - | Deferred: virtual joystick |
| SDL_SetJoystickVirtualButton | function | - | - | Deferred: virtual joystick |
| SDL_SetJoystickVirtualHat | function | - | - | Deferred: virtual joystick |
| SDL_SetJoystickVirtualTouchpad | function | - | - | Deferred: virtual joystick |
| SDL_SendJoystickVirtualSensorData | function | - | - | Deferred: virtual joystick |

## SDL_keyboard.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_HasKeyboard | function | Keyboard.SDL_HasKeyboard | Keyboard.HasKeyboard | |
| SDL_GetKeyboards | function | Keyboard.SDL_GetKeyboards | - | Deferred managed wrapper |
| SDL_GetKeyboardNameForID | function | Keyboard.SDL_GetKeyboardNameForID | - | Deferred managed wrapper |
| SDL_GetKeyboardFocus | function | Keyboard.SDL_GetKeyboardFocus | - | Deferred managed wrapper |
| SDL_GetKeyboardState | function | Keyboard.SDL_GetKeyboardState | Keyboard.IsKeyPressed | Per-key query only; full key-state span deferred |
| SDL_ResetKeyboard | function | Keyboard.SDL_ResetKeyboard | Keyboard.Reset | |
| SDL_GetModState | function | Keyboard.SDL_GetModState | Keyboard.ModState | |
| SDL_SetModState | function | Keyboard.SDL_SetModState | Keyboard.SetModState | |
| SDL_GetKeyFromScancode | function | Keyboard.SDL_GetKeyFromScancode | Keyboard.GetKeyFromScancode | |
| SDL_GetScancodeFromKey | function | Keyboard.SDL_GetScancodeFromKey | - | Deferred managed wrapper |
| SDL_SetScancodeName | function | Keyboard.SDL_SetScancodeName | - | Deferred managed wrapper |
| SDL_GetScancodeName | function | Keyboard.SDL_GetScancodeName | Keyboard.GetScancodeName | |
| SDL_GetScancodeFromName | function | Keyboard.SDL_GetScancodeFromName | - | Deferred managed wrapper |
| SDL_GetKeyName | function | Keyboard.SDL_GetKeyName | Keyboard.GetKeyName | |
| SDL_GetKeyFromName | function | Keyboard.SDL_GetKeyFromName | - | Deferred managed wrapper |
| SDL_StartTextInput | function | Keyboard.SDL_StartTextInput | - | Deferred managed wrapper |
| SDL_StartTextInputWithProperties | function | - | - | Deferred: property-based variant |
| SDL_TextInputActive | function | Keyboard.SDL_TextInputActive | - | Deferred managed wrapper |
| SDL_StopTextInput | function | Keyboard.SDL_StopTextInput | - | Deferred managed wrapper |
| SDL_ClearComposition | function | Keyboard.SDL_ClearComposition | - | Deferred managed wrapper |
| SDL_SetTextInputArea | function | - | - | Deferred (SDL_Rect interop now available) |
| SDL_GetTextInputArea | function | - | - | Deferred (SDL_Rect interop now available) |
| SDL_HasScreenKeyboardSupport | function | Keyboard.SDL_HasScreenKeyboardSupport | Keyboard.HasScreenKeyboardSupport | |
| SDL_ScreenKeyboardShown | function | Keyboard.SDL_ScreenKeyboardShown | - | Deferred managed wrapper |
| SDL_KeyboardID | typedef | - | - | Deferred |
| SDL_TextInputType | enum | - | - | Deferred: text input properties |
| SDL_Capitalization | enum | - | - | Deferred: text input properties |
| SDL_PROP_TEXTINPUT_*_NUMBER/BOOLEAN | macro (5) | - | - | Property string constants; deferred with SDL_StartTextInputWithProperties |

## SDL_keycode.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_Keycode | typedef (Uint32) + 256 SDLK_* constants | Keycode.SDL_Keycode | Keycode | Native enum has all 256 SDLK_* values; managed Keycode exposes only 86 of 256 — rest deferred |
| SDLK_EXTENDED_MASK | constant | - | - | Deferred: needed to classify extended keycodes |
| SDLK_SCANCODE_MASK | constant | - | - | Deferred: needed to test scancode-derived keycodes |
| SDL_SCANCODE_TO_KEYCODE | macro | - | - | macro; SDL_GetKeyFromScancode (SDL_keyboard.h) is the layout-aware alternative |
| SDL_Keymod | typedef (Uint16) + 18 SDL_KMOD_* flags | Keycode.SDL_Keymod | KeyModifiers | Managed enum missing SDL_KMOD_LEVEL5 (Level5) — deferred |

## SDL_loadso.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_SharedObject | struct | - | - | .NET has NativeLibrary / DllImport |
| SDL_LoadObject | function | - | - | .NET has NativeLibrary.Load |
| SDL_LoadFunction | function | - | - | .NET has NativeLibrary.GetExport |
| SDL_UnloadObject | function | - | - | .NET has NativeLibrary.Free |

## SDL_locale.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_Locale | struct | Locale.SDL_Locale | LocaleInfo | |
| SDL_GetPreferredLocales | function | Locale.SDL_GetPreferredLocales | LocaleInfo.GetPreferred | |

## SDL_log.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_LogCategory | enum | Log.SDL_LogCategory | LogCategory | |
| SDL_LogPriority | enum | Log.SDL_LogPriority | LogPriority | |
| SDL_SetLogPriorities | function | Log.SDL_SetLogPriorities | SdlLog.SetAllPriorities | |
| SDL_SetLogPriority | function | Log.SDL_SetLogPriority | SdlLog.SetPriority | |
| SDL_GetLogPriority | function | Log.SDL_GetLogPriority | SdlLog.GetPriority | |
| SDL_ResetLogPriorities | function | Log.SDL_ResetLogPriorities | SdlLog.ResetPriorities | |
| SDL_SetLogPriorityPrefix | function | Log.SDL_SetLogPriorityPrefix | SdlLog.SetPriorityPrefix | |
| SDL_LogOutputFunction | callback | Log.SDL_SetLogOutputFunction (delegate* param) | SdlLog.SetOutputFunction (Action delegate) | |
| SDL_SetLogOutputFunction | function | Log.SDL_SetLogOutputFunction | SdlLog.SetOutputFunction | |
| SDL_GetLogOutputFunction | function | - | - | niche: function pointer retrieval, rarely needed |
| SDL_GetDefaultLogOutputFunction | function | - | - | niche: only for callback-chaining to default sink |
| SDL_Log | function | - | - | Variadic: printf-style, format in C# instead |
| SDL_LogTrace | function | - | - | Variadic: printf-style |
| SDL_LogVerbose | function | - | - | Variadic: printf-style |
| SDL_LogDebug | function | - | - | Variadic: printf-style |
| SDL_LogInfo | function | - | - | Variadic: printf-style |
| SDL_LogWarn | function | - | - | Variadic: printf-style |
| SDL_LogError | function | - | - | Variadic: printf-style |
| SDL_LogCritical | function | - | - | Variadic: printf-style |
| SDL_LogMessage | function | - | - | deferred: variadic, but bindable via fixed "%s" format; only way to emit into SDL's log stream |
| SDL_LogMessageV | function | - | - | Variadic: va_list variant, not callable from C# |

## SDL_main.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_main_func | typedef | - | - | Entry point: only used by SDL_RunApp; C# runtime owns main() |
| SDL_main | function | - | - | Entry point: app-supplied main() prototype; C# runtime owns main() |
| SDL_SetMainReady | function | - | - | Entry point: SDL_MAIN_HANDLED handshake; C# runtime owns main() |
| SDL_RunApp | function | - | - | Entry point: platform main() shim; C# runtime owns main() |
| SDL_EnterAppMainCallbacks | function | - | - | App callbacks model: C# runtime owns main() |
| SDL_AppInit / SDL_AppIterate / SDL_AppEvent / SDL_AppQuit | prototype (4) | - | - | App callbacks model: app-defined, not SDL exports — skip documented in Native/Init.cs |
| SDL_RegisterApp | function | - | - | Platform: Windows-specific |
| SDL_UnregisterApp | function | - | - | Platform: Windows-specific |
| SDL_GDKSuspendComplete | function | - | - | Platform: Xbox GDK only |

## SDL_messagebox.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_MessageBoxFlags | typedef | MessageBox.SDL_MessageBoxFlags | MessageBoxType | Managed enum exposes dialog-type bits only |
| SDL_MESSAGEBOX_ERROR | enum value | MessageBox.SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR | MessageBoxType.Error | |
| SDL_MESSAGEBOX_WARNING | enum value | MessageBox.SDL_MessageBoxFlags.SDL_MESSAGEBOX_WARNING | MessageBoxType.Warning | |
| SDL_MESSAGEBOX_INFORMATION | enum value | MessageBox.SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION | MessageBoxType.Information | |
| SDL_MESSAGEBOX_BUTTONS_LEFT_TO_RIGHT | enum value | MessageBox.SDL_MessageBoxFlags.SDL_MESSAGEBOX_BUTTONS_LEFT_TO_RIGHT | - | Deferred: only meaningful with SDL_ShowMessageBox |
| SDL_MESSAGEBOX_BUTTONS_RIGHT_TO_LEFT | enum value | MessageBox.SDL_MessageBoxFlags.SDL_MESSAGEBOX_BUTTONS_RIGHT_TO_LEFT | - | Deferred: only meaningful with SDL_ShowMessageBox |
| SDL_ShowSimpleMessageBox | function | MessageBox.SDL_ShowSimpleMessageBox | MessageBox.Show | |
| SDL_ShowMessageBox | function | - | - | Deferred: complex struct hierarchy for custom buttons/colors |
| SDL_MessageBoxButtonFlags | typedef | - | - | Deferred: custom messagebox |
| SDL_MESSAGEBOX_BUTTON_RETURNKEY_DEFAULT | macro | - | - | Deferred: custom messagebox |
| SDL_MESSAGEBOX_BUTTON_ESCAPEKEY_DEFAULT | macro | - | - | Deferred: custom messagebox |
| SDL_MessageBoxButtonData | struct | - | - | Deferred: custom messagebox |
| SDL_MessageBoxColor | struct | - | - | Deferred: custom messagebox |
| SDL_MessageBoxColorType | enum | - | - | Deferred: custom messagebox |
| SDL_MessageBoxColorScheme | struct | - | - | Deferred: custom messagebox |
| SDL_MessageBoxData | struct | - | - | Deferred: custom messagebox |

## SDL_metal.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_MetalView | typedef | - | - | Platform: Metal-specific (macOS/iOS) |
| SDL_Metal_CreateView | function | - | - | Platform: Metal-specific |
| SDL_Metal_DestroyView | function | - | - | Platform: Metal-specific |
| SDL_Metal_GetLayer | function | - | - | Platform: Metal-specific |

## SDL_misc.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_OpenURL | function | Misc.SDL_OpenURL | SystemInfo.OpenUrl | |

## SDL_mouse.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_MouseID | typedef | - | - | Surfaced as raw `uint` in bindings |
| SDL_Cursor | struct | Mouse.SDL_Cursor | - | Deferred managed wrapper |
| SDL_SystemCursor | enum | Mouse.SDL_SystemCursor | - | Deferred managed wrapper |
| SDL_MouseWheelDirection | enum | Mouse.SDL_MouseWheelDirection | - | Deferred managed wrapper |
| SDL_CursorFrameInfo | struct | - | - | Niche: animated cursors only (SDL 3.4) |
| SDL_MouseButtonFlags | typedef | - | - | Surfaced as raw `uint` bitmask |
| SDL_MouseMotionTransformCallback | callback | - | - | Niche: only used by SDL_SetRelativeMouseTransform |
| SDL_BUTTON_LEFT | macro | Mouse.SDL_BUTTON_LEFT | MouseButton.Left | |
| SDL_BUTTON_MIDDLE | macro | Mouse.SDL_BUTTON_MIDDLE | MouseButton.Middle | |
| SDL_BUTTON_RIGHT | macro | Mouse.SDL_BUTTON_RIGHT | MouseButton.Right | |
| SDL_BUTTON_X1 | macro | Mouse.SDL_BUTTON_X1 | MouseButton.X1 | |
| SDL_BUTTON_X2 | macro | Mouse.SDL_BUTTON_X2 | MouseButton.X2 | |
| SDL_BUTTON_MASK | macro | - | Mouse.IsButtonPressed | Macro; mask math reimplemented |
| SDL_BUTTON_LMASK/MMASK/RMASK/X1MASK/X2MASK | macro (5) | - | - | Macro; use Mouse.IsButtonPressed |
| SDL_HasMouse | function | Mouse.SDL_HasMouse | Mouse.HasMouse | |
| SDL_GetMice | function | Mouse.SDL_GetMice | - | Deferred managed wrapper |
| SDL_GetMouseNameForID | function | Mouse.SDL_GetMouseNameForID | - | Deferred managed wrapper |
| SDL_GetMouseFocus | function | Mouse.SDL_GetMouseFocus | - | Deferred managed wrapper |
| SDL_GetMouseState | function | Mouse.SDL_GetMouseState | Mouse.GetState | |
| SDL_GetGlobalMouseState | function | Mouse.SDL_GetGlobalMouseState | Mouse.GetGlobalState | |
| SDL_GetRelativeMouseState | function | Mouse.SDL_GetRelativeMouseState | Mouse.GetRelativeState | |
| SDL_WarpMouseInWindow | function | Mouse.SDL_WarpMouseInWindow | Mouse.WarpInWindow | |
| SDL_WarpMouseGlobal | function | Mouse.SDL_WarpMouseGlobal | Mouse.WarpGlobal | |
| SDL_SetWindowRelativeMouseMode | function | Mouse.SDL_SetWindowRelativeMouseMode | - | Deferred managed wrapper |
| SDL_GetWindowRelativeMouseMode | function | Mouse.SDL_GetWindowRelativeMouseMode | - | Deferred managed wrapper |
| SDL_CaptureMouse | function | Mouse.SDL_CaptureMouse | Mouse.Capture | |
| SDL_CreateColorCursor | function | Mouse.SDL_CreateColorCursor | - | Deferred managed wrapper |
| SDL_CreateSystemCursor | function | Mouse.SDL_CreateSystemCursor | - | Deferred managed wrapper |
| SDL_SetCursor | function | Mouse.SDL_SetCursor | - | Deferred managed wrapper |
| SDL_GetCursor | function | Mouse.SDL_GetCursor | - | Deferred managed wrapper |
| SDL_GetDefaultCursor | function | Mouse.SDL_GetDefaultCursor | - | Deferred managed wrapper |
| SDL_DestroyCursor | function | Mouse.SDL_DestroyCursor | - | Deferred managed wrapper |
| SDL_ShowCursor | function | Mouse.SDL_ShowCursor | Mouse.ShowCursor | |
| SDL_HideCursor | function | Mouse.SDL_HideCursor | Mouse.HideCursor | |
| SDL_CursorVisible | function | Mouse.SDL_CursorVisible | Mouse.IsCursorVisible | |
| SDL_SetRelativeMouseTransform | function | - | - | Niche: realtime-thread callback, complex interop |
| SDL_CreateCursor | function | - | - | Niche: bitmap cursor, rarely used |
| SDL_CreateAnimatedCursor | function | - | - | Niche: SDL 3.4 addition |

## SDL_mutex.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_CAPABILITY … SDL_NO_THREAD_SAFETY_ANALYSIS (21 thread-safety annotation macros) | macro | - | - | macro — Clang thread-safety analysis annotations, no-op in C# |
| SDL_Mutex | struct | - | - | .NET has System.Threading.Mutex / lock |
| SDL_CreateMutex | function | - | - | .NET has System.Threading.Mutex |
| SDL_LockMutex | function | - | - | .NET has lock / Monitor.Enter |
| SDL_TryLockMutex | function | - | - | .NET has Monitor.TryEnter |
| SDL_UnlockMutex | function | - | - | .NET has Monitor.Exit |
| SDL_DestroyMutex | function | - | - | .NET has IDisposable |
| SDL_RWLock | struct | - | - | .NET has ReaderWriterLockSlim |
| SDL_CreateRWLock | function | - | - | .NET has ReaderWriterLockSlim |
| SDL_LockRWLockForReading | function | - | - | .NET has ReaderWriterLockSlim.EnterReadLock |
| SDL_LockRWLockForWriting | function | - | - | .NET has ReaderWriterLockSlim.EnterWriteLock |
| SDL_TryLockRWLockForReading | function | - | - | .NET has ReaderWriterLockSlim.TryEnterReadLock |
| SDL_TryLockRWLockForWriting | function | - | - | .NET has ReaderWriterLockSlim.TryEnterWriteLock |
| SDL_UnlockRWLock | function | - | - | .NET has ReaderWriterLockSlim.ExitReadLock / ExitWriteLock |
| SDL_DestroyRWLock | function | - | - | .NET has IDisposable |
| SDL_Semaphore | struct | - | - | .NET has SemaphoreSlim |
| SDL_CreateSemaphore | function | - | - | .NET has SemaphoreSlim |
| SDL_DestroySemaphore | function | - | - | .NET has IDisposable |
| SDL_WaitSemaphore | function | - | - | .NET has SemaphoreSlim.Wait |
| SDL_TryWaitSemaphore | function | - | - | .NET has SemaphoreSlim.Wait(0) |
| SDL_WaitSemaphoreTimeout | function | - | - | .NET has SemaphoreSlim.Wait(timeout) |
| SDL_SignalSemaphore | function | - | - | .NET has SemaphoreSlim.Release |
| SDL_GetSemaphoreValue | function | - | - | .NET has SemaphoreSlim.CurrentCount |
| SDL_Condition | struct | - | - | .NET has Monitor.Wait/Pulse |
| SDL_CreateCondition | function | - | - | .NET has Monitor.Wait/Pulse |
| SDL_DestroyCondition | function | - | - | .NET has IDisposable |
| SDL_SignalCondition | function | - | - | .NET has Monitor.Pulse |
| SDL_BroadcastCondition | function | - | - | .NET has Monitor.PulseAll |
| SDL_WaitCondition | function | - | - | .NET has Monitor.Wait |
| SDL_WaitConditionTimeout | function | - | - | .NET has Monitor.Wait(timeout) |
| SDL_InitStatus | enum | - | - | .NET has Lazy&lt;T&gt; / LazyInitializer |
| SDL_InitState | struct | - | - | .NET has Lazy&lt;T&gt; / LazyInitializer |
| SDL_ShouldInit | function | - | - | .NET has LazyInitializer / Interlocked |
| SDL_ShouldQuit | function | - | - | .NET has LazyInitializer / Interlocked |
| SDL_SetInitialized | function | - | - | .NET has LazyInitializer / Interlocked |

## SDL_pen.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_PenID | typedef | - | - | Internal ID type — passed as raw uint |
| SDL_PenAxis | enum | Pen.SDL_PenAxis | PenAxis | |
| SDL_PenDeviceType | enum | Pen.SDL_PenDeviceType | PenDeviceType | |
| SDL_PenInputFlags | typedef | - | PenInput | Raw uint constants natively |
| SDL_PEN_INPUT_* | macro (8) | Pen.SDL_PEN_INPUT_* | PenInput | |
| SDL_PEN_MOUSEID | macro | Mouse.SDL_PEN_MOUSEID | - | deferred — sentinel for pen-simulated mouse events; bound in Mouse native class |
| SDL_PEN_TOUCHID | macro | Pen.SDL_PEN_TOUCHID | - | deferred — sentinel for pen-simulated touch events |
| SDL_GetPenDeviceType | function | Pen.SDL_GetPenDeviceType | PenDevice.GetType | |

## SDL_pixels.h ✅

Pixel format and colorspace enum values omitted — all wrapped 1:1 between native (`Pixels.SDL_PixelFormat.*`) and managed (`PixelFormat.*` / `Colorspace.*`).

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_PixelFormat | enum | Pixels.SDL_PixelFormat | PixelFormat | |
| SDL_Colorspace | enum | Pixels.SDL_Colorspace | Colorspace | |
| SDL_Color | struct | Pixels.SDL_Color | Color | |
| SDL_FColor | struct | Pixels.SDL_FColor | FColor | |
| SDL_Palette | struct | Pixels.SDL_Palette | Palette | |
| SDL_PixelFormatDetails | struct | Pixels.SDL_PixelFormatDetails | PixelFormatDetails | |
| SDL_ALPHA_OPAQUE | constant | Pixels.SDL_ALPHA_OPAQUE | Color.Opaque | |
| SDL_ALPHA_OPAQUE_FLOAT | constant | Pixels.SDL_ALPHA_OPAQUE_FLOAT | FColor.Opaque | |
| SDL_ALPHA_TRANSPARENT | constant | Pixels.SDL_ALPHA_TRANSPARENT | Color.TransparentAlpha | |
| SDL_ALPHA_TRANSPARENT_FLOAT | constant | Pixels.SDL_ALPHA_TRANSPARENT_FLOAT | FColor.Transparent | |
| SDL_PixelType | enum | - | - | Internal: only used to compose pixel format values |
| SDL_BitmapOrder | enum | - | - | Internal: only used to compose pixel format values |
| SDL_PackedOrder | enum | - | - | Internal: only used to compose pixel format values |
| SDL_ArrayOrder | enum | - | - | Internal: only used to compose pixel format values |
| SDL_PackedLayout | enum | - | - | Internal: only used to compose pixel format values |
| SDL_ColorType | enum | - | - | Internal: only used to compose colorspace values |
| SDL_ColorRange | enum | - | - | Internal: only used to compose colorspace values |
| SDL_ColorPrimaries | enum | - | - | Internal: only used to compose colorspace values |
| SDL_TransferCharacteristics | enum | - | - | Internal: only used to compose colorspace values |
| SDL_MatrixCoefficients | enum | - | - | Internal: only used to compose colorspace values |
| SDL_ChromaLocation | enum | - | - | Internal: only used to compose colorspace values |
| SDL_DEFINE_PIXELFOURCC | macro | - | - | macro — pixel-format composition; named SDL_PixelFormat values cover all practical use |
| SDL_DEFINE_PIXELFORMAT | macro | - | - | macro — pixel-format composition; named SDL_PixelFormat values cover all practical use |
| SDL_PIXELFLAG / SDL_PIXELTYPE / SDL_PIXELORDER / SDL_PIXELLAYOUT | macro (4) | - | - | macro — pixel-format bit decomposition |
| SDL_BITSPERPIXEL / SDL_BYTESPERPIXEL | macro (2) | - | - | macro — use PixelFormatDetails.Get(format).BitsPerPixel/BytesPerPixel |
| SDL_ISPIXELFORMAT_* | macro (7) | - | - | macro — format classification bit tests (INDEXED/PACKED/ARRAY/10BIT/FLOAT/ALPHA/FOURCC) |
| SDL_DEFINE_COLORSPACE | macro | - | - | macro — colorspace composition; named SDL_Colorspace values cover all practical use |
| SDL_COLORSPACETYPE / SDL_COLORSPACERANGE / SDL_COLORSPACECHROMA / SDL_COLORSPACEPRIMARIES / SDL_COLORSPACETRANSFER / SDL_COLORSPACEMATRIX | macro (6) | - | - | macro — colorspace bit decomposition |
| SDL_ISCOLORSPACE_* | macro (5) | - | - | macro — colorspace classification bit tests (MATRIX_BT601/MATRIX_BT709/MATRIX_BT2020_NCL/LIMITED_RANGE/FULL_RANGE) |
| SDL_GetPixelFormatName | function | Pixels.SDL_GetPixelFormatName | PixelFormatExtensions.GetName | |
| SDL_GetMasksForPixelFormat | function | Pixels.SDL_GetMasksForPixelFormat | PixelFormatExtensions.GetMasks | |
| SDL_GetPixelFormatForMasks | function | Pixels.SDL_GetPixelFormatForMasks | PixelFormatExtensions.FromMasks | |
| SDL_GetPixelFormatDetails | function | Pixels.SDL_GetPixelFormatDetails | PixelFormatDetails.Get | |
| SDL_CreatePalette | function | Pixels.SDL_CreatePalette | Palette (constructor) | |
| SDL_SetPaletteColors | function | Pixels.SDL_SetPaletteColors | Palette.SetColors | |
| SDL_DestroyPalette | function | Pixels.SDL_DestroyPalette | Palette.Dispose | |
| SDL_MapRGB | function | Pixels.SDL_MapRGB | PixelFormatDetails.MapRgb | |
| SDL_MapRGBA | function | Pixels.SDL_MapRGBA | PixelFormatDetails.MapRgba | |
| SDL_GetRGB | function | Pixels.SDL_GetRGB | PixelFormatDetails.GetRgb | |
| SDL_GetRGBA | function | Pixels.SDL_GetRGBA | PixelFormatDetails.GetRgba | |

## SDL_platform.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_GetPlatform | function | - | - | .NET has RuntimeInformation.OSDescription |

## SDL_power.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_PowerState | enum | Events.SDL_PowerState | PowerState | |
| SDL_GetPowerInfo | function | Power.SDL_GetPowerInfo | PowerInfo.Get | |

## SDL_process.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_Process | struct | - | - | .NET has System.Diagnostics.Process |
| SDL_ProcessIO | enum | - | - | .NET has Process.StandardOutput etc. |
| SDL_CreateProcess | function | - | - | .NET has Process.Start |
| SDL_CreateProcessWithProperties | function | - | - | .NET has ProcessStartInfo |
| SDL_PROP_PROCESS_CREATE_* | macro (12) | - | - | .NET has ProcessStartInfo |
| SDL_GetProcessProperties | function | - | - | .NET has Process.Id etc. |
| SDL_PROP_PROCESS_* | macro (5) | - | - | .NET has Process properties |
| SDL_ReadProcess | function | - | - | .NET has Process.StandardOutput |
| SDL_GetProcessInput | function | - | - | .NET has Process.StandardInput |
| SDL_GetProcessOutput | function | - | - | .NET has Process.StandardOutput |
| SDL_KillProcess | function | - | - | .NET has Process.Kill |
| SDL_WaitProcess | function | - | - | .NET has Process.WaitForExit |
| SDL_DestroyProcess | function | - | - | .NET has Process.Dispose |

## SDL_properties.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_PropertyType | enum | Properties.SDL_PropertyType | PropertyType | |
| SDL_PropertiesID | typedef | Properties.SDL_PropertiesID | - | Internal ID type |
| SDL_PROP_NAME_STRING | macro | Properties.SDL_PROP_NAME_STRING | - | Property string constant |
| SDL_GetGlobalProperties | function | Properties.SDL_GetGlobalProperties | PropertyGroup.Global | |
| SDL_CreateProperties | function | Properties.SDL_CreateProperties | PropertyGroup (constructor) | |
| SDL_CopyProperties | function | Properties.SDL_CopyProperties | PropertyGroup.CopyTo | |
| SDL_LockProperties | function | Properties.SDL_LockProperties | PropertyGroup.Lock | |
| SDL_UnlockProperties | function | Properties.SDL_UnlockProperties | PropertyGroup.Unlock | |
| SDL_CleanupPropertyCallback | callback | - | - | niche — only used by SDL_SetPointerPropertyWithCleanup, which is skipped |
| SDL_SetPointerPropertyWithCleanup | function | - | - | niche — cleanup-callback pointer variant; managed lifetimes handled by GC/IDisposable |
| SDL_SetPointerProperty | function | - | - | deferred — needed for create-with-properties interop (e.g. external window handles, pixel buffers) |
| SDL_SetStringProperty | function | Properties.SDL_SetStringProperty | PropertyGroup.SetString | |
| SDL_SetNumberProperty | function | Properties.SDL_SetNumberProperty | PropertyGroup.SetNumber | |
| SDL_SetFloatProperty | function | Properties.SDL_SetFloatProperty | PropertyGroup.SetFloat | |
| SDL_SetBooleanProperty | function | Properties.SDL_SetBooleanProperty | PropertyGroup.SetBoolean | |
| SDL_HasProperty | function | Properties.SDL_HasProperty | PropertyGroup.Has | |
| SDL_GetPropertyType | function | Properties.SDL_GetPropertyType | PropertyGroup.GetPropertyType | |
| SDL_GetPointerProperty | function | Properties.SDL_GetPointerProperty | - | deferred — managed IntPtr-returning PropertyGroup.GetPointer pending |
| SDL_GetStringProperty | function | Properties.SDL_GetStringProperty | PropertyGroup.GetString | |
| SDL_GetNumberProperty | function | Properties.SDL_GetNumberProperty | PropertyGroup.GetNumber | |
| SDL_GetFloatProperty | function | Properties.SDL_GetFloatProperty | PropertyGroup.GetFloat | |
| SDL_GetBooleanProperty | function | Properties.SDL_GetBooleanProperty | PropertyGroup.GetBoolean | |
| SDL_ClearProperty | function | Properties.SDL_ClearProperty | PropertyGroup.Clear | |
| SDL_EnumeratePropertiesCallback | callback | Properties.SDL_EnumerateProperties (unmanaged function pointer parameter) | PropertyGroup.GetNames | Bound inline as `delegate* unmanaged[Cdecl]` in the native signature |
| SDL_EnumerateProperties | function | Properties.SDL_EnumerateProperties | PropertyGroup.GetNames | |
| SDL_DestroyProperties | function | Properties.SDL_DestroyProperties | PropertyGroup.Dispose | |

## SDL_rect.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_Point | struct | Rect.SDL_Point | Point | |
| SDL_FPoint | struct | Rect.SDL_FPoint | FPoint | |
| SDL_Rect | struct | Rect.SDL_Rect | Rectangle | |
| SDL_FRect | struct | Rect.SDL_FRect | FRectangle | |
| SDL_HasRectIntersection | function | Rect.SDL_HasRectIntersection | Rectangle.IntersectsWith | |
| SDL_GetRectIntersection | function | Rect.SDL_GetRectIntersection | Rectangle.Intersect | |
| SDL_GetRectUnion | function | Rect.SDL_GetRectUnion | Rectangle.Union | |
| SDL_GetRectEnclosingPoints | function | Rect.SDL_GetRectEnclosingPoints | Rectangle.EnclosePoints | |
| SDL_GetRectAndLineIntersection | function | Rect.SDL_GetRectAndLineIntersection | Rectangle.ClipLine | |
| SDL_HasRectIntersectionFloat | function | Rect.SDL_HasRectIntersectionFloat | FRectangle.IntersectsWith | |
| SDL_GetRectIntersectionFloat | function | Rect.SDL_GetRectIntersectionFloat | FRectangle.Intersect | |
| SDL_GetRectUnionFloat | function | Rect.SDL_GetRectUnionFloat | FRectangle.Union | |
| SDL_GetRectEnclosingPointsFloat | function | Rect.SDL_GetRectEnclosingPointsFloat | FRectangle.EnclosePoints | |
| SDL_GetRectAndLineIntersectionFloat | function | Rect.SDL_GetRectAndLineIntersectionFloat | FRectangle.ClipLine | |
| SDL_RectToFRect | function | - | Rectangle.ToFloat | Inline: reimplemented in C# |
| SDL_PointInRect | function | - | Rectangle.Contains | Inline: reimplemented in C# |
| SDL_RectEmpty | function | - | Rectangle.IsEmpty | Inline: reimplemented in C# |
| SDL_RectsEqual | function | - | - | Inline: C# record struct equality |
| SDL_PointInRectFloat | function | - | FRectangle.Contains | Inline: reimplemented in C# |
| SDL_RectEmptyFloat | function | - | FRectangle.IsEmpty | Inline: reimplemented in C# |
| SDL_RectsEqualFloat | function | - | - | Inline: C# record struct equality |
| SDL_RectsEqualEpsilon | function | - | FRectangle.Equals | Inline: reimplemented in C# |

## SDL_render.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_SOFTWARE_RENDERER | macro | Render.SDL_SOFTWARE_RENDERER | - | String constant |
| SDL_GPU_RENDERER | macro | Render.SDL_GPU_RENDERER | - | String constant |
| SDL_Renderer | opaque | Render.SDL_Renderer | Renderer | |
| SDL_Texture | struct | Render.SDL_Texture | Texture | |
| SDL_Vertex | struct | Render.SDL_Vertex | Vertex | |
| SDL_TextureAccess | enum | Render.SDL_TextureAccess | TextureAccess | |
| SDL_TextureAddressMode | enum | Render.SDL_TextureAddressMode | TextureAddressMode | |
| SDL_RendererLogicalPresentation | enum | Render.SDL_RendererLogicalPresentation | LogicalPresentation | |
| SDL_GetNumRenderDrivers | function | Render.SDL_GetNumRenderDrivers | Renderer.NumDrivers | |
| SDL_GetRenderDriver | function | Render.SDL_GetRenderDriver | Renderer.GetDriver | |
| SDL_CreateRenderer | function | Render.SDL_CreateRenderer | Renderer.Create | |
| SDL_CreateRendererWithProperties | function | Render.SDL_CreateRendererWithProperties | Renderer.Create | |
| SDL_PROP_RENDERER_CREATE_* | macro (15) | Render.SDL_PROP_RENDERER_CREATE_* | Renderer.PropCreate* | Property string constants |
| SDL_CreateWindowAndRenderer | function | Render.SDL_CreateWindowAndRenderer | Renderer.CreateWindowAndRenderer | |
| SDL_CreateGPURenderer | function | - | - | Deferred: GPU renderer-specific |
| SDL_GetGPURendererDevice | function | - | - | Deferred: GPU renderer-specific |
| SDL_CreateSoftwareRenderer | function | - | - | Deferred |
| SDL_GetRenderer | function | Render.SDL_GetRenderer | Renderer.FromWindow | |
| SDL_GetRenderWindow | function | Render.SDL_GetRenderWindow | Renderer.Window | |
| SDL_GetRendererName | function | Render.SDL_GetRendererName | Renderer.Name | |
| SDL_GetRendererProperties | function | Render.SDL_GetRendererProperties | Renderer.Properties | |
| SDL_PROP_RENDERER_* | macro (25) | Render.SDL_PROP_RENDERER_* | Renderer.Prop* | Property string constants; 12 of 25 exposed managed (D3D/Vulkan interop pointers native-only, platform) |
| SDL_GetRenderOutputSize | function | Render.SDL_GetRenderOutputSize | Renderer.OutputSize | |
| SDL_GetCurrentRenderOutputSize | function | Render.SDL_GetCurrentRenderOutputSize | Renderer.CurrentOutputSize | |
| SDL_CreateTexture | function | Render.SDL_CreateTexture | Renderer.CreateTexture | |
| SDL_CreateTextureFromSurface | function | Render.SDL_CreateTextureFromSurface | Renderer.CreateTextureFromSurface | |
| SDL_CreateTextureWithProperties | function | - | - | Deferred |
| SDL_PROP_TEXTURE_CREATE_* | macro (29) | Render.SDL_PROP_TEXTURE_CREATE_* | Texture.PropCreate* | Property string constants; 8 of 29 exposed managed (native GPU handle props native-only, platform) |
| SDL_DestroyTexture | function | Render.SDL_DestroyTexture | Texture.Dispose | |
| SDL_GetTextureSize | function | Render.SDL_GetTextureSize | Texture.Size | |
| SDL_GetTextureProperties | function | - | - | Deferred |
| SDL_PROP_TEXTURE_* | macro (30) | Render.SDL_PROP_TEXTURE_* | Texture.Prop* | Property string constants; 7 of 30 exposed managed (native GPU handle props native-only, platform) |
| SDL_GetRendererFromTexture | function | - | - | Deferred |
| SDL_SetTexturePalette | function | - | - | Deferred |
| SDL_GetTexturePalette | function | - | - | Deferred |
| SDL_SetTextureColorMod | function | Render.SDL_SetTextureColorMod | Texture.ColorMod | |
| SDL_GetTextureColorMod | function | Render.SDL_GetTextureColorMod | Texture.ColorMod | |
| SDL_SetTextureColorModFloat | function | - | - | Deferred: float variant |
| SDL_GetTextureColorModFloat | function | - | - | Deferred: float variant |
| SDL_SetTextureAlphaMod | function | Render.SDL_SetTextureAlphaMod | Texture.AlphaMod | |
| SDL_GetTextureAlphaMod | function | Render.SDL_GetTextureAlphaMod | Texture.AlphaMod | |
| SDL_SetTextureAlphaModFloat | function | - | - | Deferred: float variant |
| SDL_GetTextureAlphaModFloat | function | - | - | Deferred: float variant |
| SDL_SetTextureBlendMode | function | Render.SDL_SetTextureBlendMode | Texture.BlendMode | |
| SDL_GetTextureBlendMode | function | Render.SDL_GetTextureBlendMode | Texture.BlendMode | |
| SDL_SetTextureScaleMode | function | Render.SDL_SetTextureScaleMode | Texture.ScaleMode | |
| SDL_GetTextureScaleMode | function | Render.SDL_GetTextureScaleMode | Texture.ScaleMode | |
| SDL_UpdateTexture | function | Render.SDL_UpdateTexture | Texture.Update | |
| SDL_LockTexture | function | Render.SDL_LockTexture | Texture.Lock | |
| SDL_UnlockTexture | function | Render.SDL_UnlockTexture | Texture.Unlock | |
| SDL_UpdateYUVTexture | function | - | - | Deferred: YUV-specific |
| SDL_UpdateNVTexture | function | - | - | Deferred: NV-specific |
| SDL_LockTextureToSurface | function | - | - | Deferred |
| SDL_SetRenderTarget | function | Render.SDL_SetRenderTarget | Renderer.Target | |
| SDL_GetRenderTarget | function | Render.SDL_GetRenderTarget | Renderer.Target | |
| SDL_SetRenderLogicalPresentation | function | Render.SDL_SetRenderLogicalPresentation | Renderer.SetLogicalPresentation | |
| SDL_GetRenderLogicalPresentation | function | Render.SDL_GetRenderLogicalPresentation | Renderer.GetLogicalPresentation | |
| SDL_GetRenderLogicalPresentationRect | function | - | - | Deferred |
| SDL_SetRenderViewport | function | Render.SDL_SetRenderViewport | Renderer.Viewport | |
| SDL_GetRenderViewport | function | Render.SDL_GetRenderViewport | Renderer.Viewport | |
| SDL_RenderViewportSet | function | - | - | Deferred |
| SDL_GetRenderSafeArea | function | - | - | Deferred |
| SDL_SetRenderClipRect | function | Render.SDL_SetRenderClipRect | Renderer.ClipRect | |
| SDL_GetRenderClipRect | function | Render.SDL_GetRenderClipRect | Renderer.ClipRect | |
| SDL_RenderClipEnabled | function | Render.SDL_RenderClipEnabled | Renderer.ClipEnabled | |
| SDL_SetRenderScale | function | Render.SDL_SetRenderScale | Renderer.Scale | |
| SDL_GetRenderScale | function | Render.SDL_GetRenderScale | Renderer.Scale | |
| SDL_SetRenderDrawColor | function | Render.SDL_SetRenderDrawColor | Renderer.DrawColor | |
| SDL_SetRenderDrawColorFloat | function | Render.SDL_SetRenderDrawColorFloat | Renderer.DrawColorFloat | |
| SDL_GetRenderDrawColor | function | Render.SDL_GetRenderDrawColor | Renderer.DrawColor | |
| SDL_GetRenderDrawColorFloat | function | Render.SDL_GetRenderDrawColorFloat | Renderer.DrawColorFloat | |
| SDL_SetRenderColorScale | function | - | - | Deferred |
| SDL_GetRenderColorScale | function | - | - | Deferred |
| SDL_SetRenderDrawBlendMode | function | Render.SDL_SetRenderDrawBlendMode | Renderer.DrawBlendMode | |
| SDL_GetRenderDrawBlendMode | function | Render.SDL_GetRenderDrawBlendMode | Renderer.DrawBlendMode | |
| SDL_RenderClear | function | Render.SDL_RenderClear | Renderer.Clear | |
| SDL_RenderPoint | function | Render.SDL_RenderPoint | Renderer.DrawPoint | |
| SDL_RenderPoints | function | Render.SDL_RenderPoints | Renderer.DrawPoints | |
| SDL_RenderLine | function | Render.SDL_RenderLine | Renderer.DrawLine | |
| SDL_RenderLines | function | Render.SDL_RenderLines | Renderer.DrawLines | |
| SDL_RenderRect | function | Render.SDL_RenderRect | Renderer.DrawRect | |
| SDL_RenderRects | function | Render.SDL_RenderRects | Renderer.DrawRects | |
| SDL_RenderFillRect | function | Render.SDL_RenderFillRect | Renderer.FillRect | |
| SDL_RenderFillRects | function | Render.SDL_RenderFillRects | Renderer.FillRects | |
| SDL_RenderTexture | function | Render.SDL_RenderTexture | Renderer.RenderTexture | |
| SDL_RenderTextureRotated | function | Render.SDL_RenderTextureRotated | Renderer.RenderTextureRotated | |
| SDL_RenderTextureAffine | function | Render.SDL_RenderTextureAffine | Renderer.RenderTextureAffine | |
| SDL_RenderTextureTiled | function | Render.SDL_RenderTextureTiled | Renderer.RenderTextureTiled | |
| SDL_RenderTexture9Grid | function | Render.SDL_RenderTexture9Grid | Renderer.RenderTexture9Grid | |
| SDL_RenderTexture9GridTiled | function | - | - | Deferred |
| SDL_RenderGeometry | function | Render.SDL_RenderGeometry | Renderer.RenderGeometry | |
| SDL_RenderGeometryRaw | function | Render.SDL_RenderGeometryRaw | - | Deferred: managed wrapper missing (native binding only) |
| SDL_RenderReadPixels | function | Render.SDL_RenderReadPixels | Renderer.ReadPixels | |
| SDL_RenderPresent | function | Render.SDL_RenderPresent | Renderer.Present | |
| SDL_FlushRenderer | function | Render.SDL_FlushRenderer | Renderer.Flush | |
| SDL_RenderCoordinatesFromWindow | function | Render.SDL_RenderCoordinatesFromWindow | Renderer.CoordinatesFromWindow | |
| SDL_RenderCoordinatesToWindow | function | Render.SDL_RenderCoordinatesToWindow | Renderer.CoordinatesToWindow | |
| SDL_ConvertEventToRenderCoordinates | function | - | - | Deferred |
| SDL_SetRenderVSync | function | Render.SDL_SetRenderVSync | Renderer.VSync | |
| SDL_GetRenderVSync | function | Render.SDL_GetRenderVSync | Renderer.VSync | |
| SDL_RENDERER_VSYNC_DISABLED | macro | - | - | Deferred: named constant for Renderer.VSync |
| SDL_RENDERER_VSYNC_ADAPTIVE | macro | - | - | Deferred: named constant for Renderer.VSync |
| SDL_SetRenderTextureAddressMode | function | Render.SDL_SetRenderTextureAddressMode | Renderer.TextureAddressMode | |
| SDL_GetRenderTextureAddressMode | function | Render.SDL_GetRenderTextureAddressMode | Renderer.TextureAddressMode | |
| SDL_DEBUG_TEXT_FONT_CHARACTER_SIZE | macro | - | - | Deferred: debug font glyph size constant |
| SDL_RenderDebugText | function | Render.SDL_RenderDebugText | Renderer.DrawDebugText | |
| SDL_RenderDebugTextFormat | function | - | - | Variadic: printf-style, use DrawDebugText + C# formatting |
| SDL_DestroyRenderer | function | Render.SDL_DestroyRenderer | Renderer.Dispose | |
| SDL_GetRenderMetalLayer | function | - | - | Platform: Metal-specific |
| SDL_GetRenderMetalCommandEncoder | function | - | - | Platform: Metal-specific |
| SDL_AddVulkanRenderSemaphores | function | - | - | Platform: Vulkan-specific |
| SDL_SetDefaultTextureScaleMode | function | - | - | Deferred |
| SDL_GetDefaultTextureScaleMode | function | - | - | Deferred |
| SDL_GPURenderStateCreateInfo | struct | - | - | Deferred: GPU renderer-specific |
| SDL_GPURenderState | opaque | - | - | Deferred: GPU renderer-specific |
| SDL_CreateGPURenderState | function | - | - | Deferred: GPU renderer-specific |
| SDL_SetGPURenderStateFragmentUniforms | function | - | - | Deferred: GPU renderer-specific |
| SDL_SetGPURenderState | function | - | - | Deferred: GPU renderer-specific |
| SDL_DestroyGPURenderState | function | - | - | Deferred: GPU renderer-specific |

## SDL_scancode.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_Scancode | enum (249 values) | Scancode.SDL_Scancode | Scancode | Native enum complete (LOCKING* values commented out upstream, omitted); managed enum covers only 105 of 249 members (through RGui) — NonUsBackslash/NonUsHash, F13–F24, media/volume, international/language, extended keypad, AC_*, system/mobile keys, and Count deferred |

## SDL_sensor.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_Sensor | struct | Sensor.SDL_Sensor | Sensor | |
| SDL_SensorID | typedef | - | - | Mapped to plain uint in bindings and managed API |
| SDL_SensorType | enum | Sensor.SDL_SensorType | SensorType | |
| SDL_STANDARD_GRAVITY | macro | Sensor.SDL_STANDARD_GRAVITY | - | deferred — public constant pending |
| SDL_GetSensors | function | Sensor.SDL_GetSensors | Sensor.GetDevices | |
| SDL_GetSensorNameForID | function | Sensor.SDL_GetSensorNameForID | Sensor.GetName | |
| SDL_GetSensorTypeForID | function | Sensor.SDL_GetSensorTypeForID | Sensor.GetType | |
| SDL_GetSensorNonPortableTypeForID | function | Sensor.SDL_GetSensorNonPortableTypeForID | - | Deferred |
| SDL_OpenSensor | function | Sensor.SDL_OpenSensor | Sensor.Open | |
| SDL_GetSensorFromID | function | Sensor.SDL_GetSensorFromID | - | Deferred |
| SDL_GetSensorProperties | function | Sensor.SDL_GetSensorProperties | Sensor.Properties | |
| SDL_GetSensorName | function | Sensor.SDL_GetSensorName | Sensor.Name | |
| SDL_GetSensorType | function | Sensor.SDL_GetSensorType | Sensor.Type | |
| SDL_GetSensorNonPortableType | function | Sensor.SDL_GetSensorNonPortableType | Sensor.NonPortableType | |
| SDL_GetSensorID | function | Sensor.SDL_GetSensorID | - | deferred — bound but no Sensor.Id property yet |
| SDL_GetSensorData | function | Sensor.SDL_GetSensorData | Sensor.GetData | |
| SDL_CloseSensor | function | Sensor.SDL_CloseSensor | Sensor.Dispose | |
| SDL_UpdateSensors | function | Sensor.SDL_UpdateSensors | Sensor.Update | |

## SDL_stdinc.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Sint8/Uint8 ... Sint64/Uint64 | typedef | - | - | .NET — sbyte/byte ... long/ulong |
| SDL_Time / SDL_MAX_TIME / SDL_MIN_TIME | typedef | - | - | .NET — represented as `long` (see Native/Time.cs); long.MaxValue/MinValue |
| SDL_MAX_SINT8 ... SDL_MIN_UINT64 / SDL_FLT_EPSILON / SDL_PI_D / SDL_PI_F / SDL_INVALID_UNICODE_CODEPOINT | constant | - | - | .NET — T.MaxValue, float.Epsilon, Math.PI, Rune.ReplacementChar |
| SDL_FOURCC / SDL_arraysize / SDL_stack_alloc / SDL_min / SDL_max / SDL_clamp / SDL_zero(p/a) | macro | - | - | macro — .NET equivalents (Math.Clamp, stackalloc, default) |
| SDL_malloc / SDL_calloc / SDL_realloc | function | - | - | deferred — needed for buffers SDL later frees with its own allocator (e.g. clipboard provider data); Marshal.AllocHGlobal is not a safe substitute |
| SDL_free | function | Common.SDL_free | - | internal — used by managed wrappers to free SDL-owned returns |
| SDL_aligned_alloc / SDL_aligned_free | function | - | - | .NET — NativeMemory.AlignedAlloc |
| SDL_SetMemoryFunctions / SDL_GetMemoryFunctions / SDL_GetOriginalMemoryFunctions / SDL_GetNumAllocations | function | - | - | niche — replacing SDL's allocator with managed callbacks is a perf/GC hazard |
| SDL_malloc_func / SDL_calloc_func / SDL_realloc_func / SDL_free_func | callback | - | - | niche — only needed for allocator hooks |
| SDL_Environment | struct | - | - | deferred — opaque handle for SDL's environment API |
| SDL_GetEnvironment | function | - | - | deferred — only reliable way to influence SDL env-based behavior at runtime |
| SDL_CreateEnvironment / SDL_DestroyEnvironment | function | - | - | deferred — standalone environment objects (for SDL_CreateProcessWithProperties) |
| SDL_GetEnvironmentVariable / SDL_GetEnvironmentVariables / SDL_SetEnvironmentVariable / SDL_UnsetEnvironmentVariable | function | - | - | deferred — with SDL_GetEnvironment |
| SDL_getenv | function | - | - | .NET — Environment.GetEnvironmentVariable |
| SDL_getenv_unsafe / SDL_setenv_unsafe / SDL_unsetenv_unsafe | function | - | - | niche — thread-unsafe libc variants; SDL_Environment API preferred |
| SDL_qsort / SDL_qsort_r / SDL_bsearch / SDL_bsearch_r | function | - | - | .NET — Array.Sort/MemoryExtensions |
| SDL_CompareCallback / SDL_CompareCallback_r | callback | - | - | .NET — only needed for qsort/bsearch |
| SDL_abs | function | - | - | .NET — Math.Abs |
| ctype family (SDL_isalpha, SDL_isalnum, SDL_isblank, SDL_iscntrl, SDL_isdigit, SDL_isxdigit, SDL_ispunct, SDL_isspace, SDL_isupper, SDL_islower, SDL_isprint, SDL_isgraph, SDL_tolower, SDL_toupper) | function | - | - | .NET — char.IsLetter etc. |
| SDL_memcpy / SDL_memmove / SDL_memset / SDL_memset4 / SDL_memcmp | function | - | - | .NET — Span<T>, Buffer.MemoryCopy, NativeMemory |
| string family (SDL_strlen, SDL_strnlen, SDL_strlcpy, SDL_strlcat, SDL_strdup, SDL_strndup, SDL_strcmp, SDL_strncmp, SDL_strcasecmp, SDL_strncasecmp, SDL_strstr, SDL_strnstr, SDL_strcasestr, SDL_strchr, SDL_strrchr, SDL_strpbrk, SDL_strtok_r, SDL_strrev, SDL_strlwr, SDL_strupr) | function | - | - | .NET — System.String/MemoryExtensions |
| wide-string family (SDL_wcslen, SDL_wcsnlen, SDL_wcslcpy, SDL_wcslcat, SDL_wcsdup, SDL_wcscmp, SDL_wcsncmp, SDL_wcscasecmp, SDL_wcsncasecmp, SDL_wcsstr, SDL_wcsnstr, SDL_wcstol) | function | - | - | .NET — System.String |
| numeric conversion family (SDL_atoi, SDL_atof, SDL_strtol, SDL_strtoul, SDL_strtoll, SDL_strtoull, SDL_strtod, SDL_itoa, SDL_uitoa, SDL_ltoa, SDL_ultoa, SDL_lltoa, SDL_ulltoa) | function | - | - | .NET — int.Parse/ToString |
| printf/scanf family (SDL_snprintf, SDL_vsnprintf, SDL_asprintf, SDL_vasprintf, SDL_swprintf, SDL_vswprintf, SDL_sscanf, SDL_vsscanf) | function | - | - | variadic — .NET string formatting |
| UTF-8 helpers (SDL_StepUTF8, SDL_StepBackUTF8, SDL_UCS4ToUTF8, SDL_utf8strlen, SDL_utf8strnlen, SDL_utf8strlcpy) | function | - | - | .NET — System.Text.Rune |
| SDL_crc16 / SDL_crc32 / SDL_murmur3_32 | function | - | - | .NET — System.IO.Hashing |
| PRNG family (SDL_rand, SDL_srand, SDL_rand_bits, SDL_randf, SDL_rand_r, SDL_rand_bits_r, SDL_randf_r) | function | - | - | .NET — System.Random |
| math family (SDL_acos(f), SDL_asin(f), SDL_atan(f), SDL_atan2(f), SDL_ceil(f), SDL_copysign(f), SDL_cos(f), SDL_exp(f), SDL_fabs(f), SDL_floor(f), SDL_fmod(f), SDL_isinf(f), SDL_isnan(f), SDL_log(f), SDL_log10(f), SDL_lround(f), SDL_modf(f), SDL_pow(f), SDL_round(f), SDL_scalbn(f), SDL_sin(f), SDL_sqrt(f), SDL_tan(f), SDL_trunc(f)) | function | - | - | .NET — Math/MathF |
| SDL_iconv_open / SDL_iconv_close / SDL_iconv / SDL_iconv_string | function | - | - | .NET — System.Text.Encoding |
| SDL_iconv_t | struct | - | - | .NET — skipped with iconv |
| SDL_ICONV_ERROR / SDL_ICONV_E2BIG / SDL_ICONV_EILSEQ / SDL_ICONV_EINVAL | constant | - | - | .NET — skipped with iconv |
| SDL_FunctionPointer | typedef | - | - | .NET — nint/function pointers used directly |
| SDL_size_mul_check_overflow / SDL_size_add_check_overflow | function | - | - | inline — .NET has checked arithmetic |

## SDL_storage.h ✅

Entire header skipped by design — .NET's `System.IO` covers desktop filesystem use, and the console title/user storage abstraction doesn't apply to .NET targets (see TODO.md).

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_StorageInterface | struct | - | - | Niche: function table for custom storage backends — implement an abstraction in C# instead |
| SDL_Storage | struct | - | - | Niche: game platform storage abstraction |
| SDL_OpenTitleStorage | function | - | - | Niche: console platform title storage |
| SDL_OpenUserStorage | function | - | - | Niche: console platform user storage |
| SDL_OpenFileStorage | function | - | - | .NET has System.IO |
| SDL_OpenStorage | function | - | - | Niche: custom SDL_StorageInterface backends |
| SDL_CloseStorage | function | - | - | Niche |
| SDL_StorageReady | function | - | - | Niche: async readiness only matters on console backends |
| SDL_GetStorageFileSize | function | - | - | .NET has FileInfo.Length |
| SDL_ReadStorageFile | function | - | - | .NET has File.ReadAllBytes |
| SDL_WriteStorageFile | function | - | - | .NET has File.WriteAllBytes |
| SDL_CreateStorageDirectory | function | - | - | .NET has Directory.CreateDirectory |
| SDL_EnumerateStorageDirectory | function | - | - | .NET has Directory.EnumerateFileSystemEntries |
| SDL_RemoveStoragePath | function | - | - | .NET has File.Delete/Directory.Delete |
| SDL_RenameStoragePath | function | - | - | .NET has File.Move/Directory.Move |
| SDL_CopyStorageFile | function | - | - | .NET has File.Copy |
| SDL_GetStoragePathInfo | function | - | - | .NET has FileInfo/DirectoryInfo |
| SDL_GetStorageSpaceRemaining | function | - | - | Niche: console save-data quotas — .NET has DriveInfo.AvailableFreeSpace |
| SDL_GlobStorageDirectory | function | - | - | .NET has Directory.GetFiles(pattern) |

## SDL_surface.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_SurfaceFlags | enum | Surface.SDL_SurfaceFlags | - | Deferred: expose Flags/MustLock on Surface |
| SDL_SURFACE_* | macro (4) | Surface.SDL_SurfaceFlags | - | Flag bits bound as native enum members |
| SDL_MUSTLOCK | macro | - | - | macro — deferred: expose as Surface.MustLock |
| SDL_ScaleMode | enum | Surface.SDL_ScaleMode | ScaleMode | |
| SDL_FlipMode | enum | Surface.SDL_FlipMode | FlipMode | |
| SDL_Surface | struct | Surface.SDL_Surface | Surface | |
| SDL_CreateSurface | function | Surface.SDL_CreateSurface | Surface.Create | |
| SDL_CreateSurfaceFrom | function | Surface.SDL_CreateSurfaceFrom | Surface.CreateFrom | |
| SDL_DestroySurface | function | Surface.SDL_DestroySurface | Surface.Dispose | |
| SDL_GetSurfaceProperties | function | Surface.SDL_GetSurfaceProperties | Surface.Properties | |
| SDL_PROP_SURFACE_* | macro (6) | - | - | Property string constants; deferred |
| SDL_SetSurfaceColorspace | function | Surface.SDL_SetSurfaceColorspace | Surface.Colorspace (set) | |
| SDL_GetSurfaceColorspace | function | Surface.SDL_GetSurfaceColorspace | Surface.Colorspace (get) | |
| SDL_SetSurfaceColorMod | function | Surface.SDL_SetSurfaceColorMod | Surface.ColorMod (set) | |
| SDL_GetSurfaceColorMod | function | Surface.SDL_GetSurfaceColorMod | Surface.ColorMod (get) | |
| SDL_SetSurfaceAlphaMod | function | Surface.SDL_SetSurfaceAlphaMod | Surface.AlphaMod (set) | |
| SDL_GetSurfaceAlphaMod | function | Surface.SDL_GetSurfaceAlphaMod | Surface.AlphaMod (get) | |
| SDL_SetSurfaceBlendMode | function | Surface.SDL_SetSurfaceBlendMode | Surface.BlendMode (set) | |
| SDL_GetSurfaceBlendMode | function | Surface.SDL_GetSurfaceBlendMode | Surface.BlendMode (get) | |
| SDL_SetSurfaceClipRect | function | Surface.SDL_SetSurfaceClipRect | Surface.ClipRect (set) | |
| SDL_GetSurfaceClipRect | function | Surface.SDL_GetSurfaceClipRect | Surface.ClipRect (get) | |
| SDL_LockSurface | function | Surface.SDL_LockSurface | Surface.Lock | |
| SDL_UnlockSurface | function | Surface.SDL_UnlockSurface | Surface.Unlock | |
| SDL_LoadBMP | function | Surface.SDL_LoadBMP | Surface.LoadBmp | |
| SDL_SaveBMP | function | Surface.SDL_SaveBMP | Surface.SaveBmp | |
| SDL_FillSurfaceRect | function | Surface.SDL_FillSurfaceRect | Surface.FillRect | |
| SDL_FillSurfaceRects | function | Surface.SDL_FillSurfaceRects | Surface.FillRects | |
| SDL_BlitSurface | function | Surface.SDL_BlitSurface | Surface.Blit | |
| SDL_BlitSurfaceScaled | function | Surface.SDL_BlitSurfaceScaled | Surface.BlitScaled | |
| SDL_DuplicateSurface | function | Surface.SDL_DuplicateSurface | Surface.Duplicate | |
| SDL_ConvertSurface | function | Surface.SDL_ConvertSurface | Surface.Convert | |
| SDL_ClearSurface | function | Surface.SDL_ClearSurface | Surface.Clear | |
| SDL_CreateSurfacePalette | function | - | - | Deferred: palette surface support |
| SDL_SetSurfacePalette | function | - | - | Deferred: palette surface support |
| SDL_GetSurfacePalette | function | - | - | Deferred: palette surface support |
| SDL_AddSurfaceAlternateImage | function | - | - | Deferred: alternate image support |
| SDL_SurfaceHasAlternateImages | function | - | - | Deferred: alternate image support |
| SDL_GetSurfaceImages | function | - | - | Deferred: alternate image support |
| SDL_RemoveSurfaceAlternateImages | function | - | - | Deferred: alternate image support |
| SDL_LoadSurface_IO | function | - | - | Deferred: needs SDL_IOStream |
| SDL_LoadSurface | function | - | - | Deferred |
| SDL_LoadBMP_IO | function | - | - | Deferred: needs SDL_IOStream |
| SDL_SaveBMP_IO | function | - | - | Deferred: needs SDL_IOStream |
| SDL_LoadPNG_IO | function | - | - | Deferred: needs SDL_IOStream |
| SDL_LoadPNG | function | - | - | Deferred |
| SDL_SavePNG_IO | function | - | - | Deferred: needs SDL_IOStream |
| SDL_SavePNG | function | - | - | Deferred |
| SDL_SetSurfaceRLE | function | - | - | Deferred |
| SDL_SurfaceHasRLE | function | - | - | Deferred |
| SDL_SetSurfaceColorKey | function | - | - | Deferred |
| SDL_SurfaceHasColorKey | function | - | - | Deferred |
| SDL_GetSurfaceColorKey | function | - | - | Deferred |
| SDL_FlipSurface | function | - | - | Deferred |
| SDL_RotateSurface | function | - | - | Deferred |
| SDL_ScaleSurface | function | - | - | Deferred |
| SDL_ConvertSurfaceAndColorspace | function | - | - | Deferred |
| SDL_ConvertPixels | function | - | - | Deferred |
| SDL_ConvertPixelsAndColorspace | function | - | - | Deferred |
| SDL_PremultiplyAlpha | function | - | - | Deferred |
| SDL_PremultiplySurfaceAlpha | function | - | - | Deferred |
| SDL_BlitSurfaceUnchecked | function | - | - | Deferred: unchecked variant |
| SDL_BlitSurfaceUncheckedScaled | function | - | - | Deferred: unchecked variant |
| SDL_StretchSurface | function | - | - | Deferred |
| SDL_BlitSurfaceTiled | function | - | - | Deferred |
| SDL_BlitSurfaceTiledWithScale | function | - | - | Deferred |
| SDL_BlitSurface9Grid | function | - | - | Deferred |
| SDL_MapSurfaceRGB | function | - | - | Deferred |
| SDL_MapSurfaceRGBA | function | - | - | Deferred |
| SDL_ReadSurfacePixel | function | - | - | Deferred |
| SDL_ReadSurfacePixelFloat | function | - | - | Deferred |
| SDL_WriteSurfacePixel | function | - | - | Deferred |
| SDL_WriteSurfacePixelFloat | function | - | - | Deferred |

## SDL_system.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_WindowsMessageHook | callback | - | - | Platform: Windows-specific |
| SDL_SetWindowsMessageHook | function | - | - | Platform: Windows-specific |
| SDL_GetDirect3D9AdapterIndex | function | - | - | Platform: Windows Direct3D |
| SDL_GetDXGIOutputInfo | function | - | - | Platform: Windows DXGI |
| SDL_X11EventHook | callback | - | - | Platform: X11/Linux-specific |
| SDL_SetX11EventHook | function | - | - | Platform: X11/Linux-specific |
| SDL_SetLinuxThreadPriority | function | - | - | Platform: Linux-specific |
| SDL_SetLinuxThreadPriorityAndPolicy | function | - | - | Platform: Linux-specific |
| SDL_iOSAnimationCallback | callback | - | - | Platform: iOS-specific |
| SDL_SetiOSAnimationCallback | function | - | - | Platform: iOS-specific |
| SDL_SetiOSEventPump | function | - | - | Platform: iOS-specific |
| SDL_GetAndroidJNIEnv | function | - | - | Platform: Android-specific |
| SDL_GetAndroidActivity | function | - | - | Platform: Android-specific |
| SDL_GetAndroidSDKVersion | function | - | - | Platform: Android-specific |
| SDL_IsChromebook | function | - | - | Platform: Android-specific |
| SDL_IsDeXMode | function | - | - | Platform: Android-specific |
| SDL_SendAndroidBackButton | function | - | - | Platform: Android-specific |
| SDL_ANDROID_EXTERNAL_STORAGE_READ | constant | - | - | Platform: Android-specific |
| SDL_ANDROID_EXTERNAL_STORAGE_WRITE | constant | - | - | Platform: Android-specific |
| SDL_GetAndroidInternalStoragePath | function | - | - | Platform: Android-specific |
| SDL_GetAndroidExternalStorageState | function | - | - | Platform: Android-specific |
| SDL_GetAndroidExternalStoragePath | function | - | - | Platform: Android-specific |
| SDL_GetAndroidCachePath | function | - | - | Platform: Android-specific |
| SDL_RequestAndroidPermissionCallback | callback | - | - | Platform: Android-specific |
| SDL_RequestAndroidPermission | function | - | - | Platform: Android-specific |
| SDL_ShowAndroidToast | function | - | - | Platform: Android-specific |
| SDL_SendAndroidMessage | function | - | - | Platform: Android-specific |
| SDL_IsTablet | function | - | - | deferred — cross-platform device-type query |
| SDL_IsTV | function | - | - | deferred — cross-platform TV-device query |
| SDL_Sandbox | enum | - | - | deferred — return type of SDL_GetSandbox |
| SDL_GetSandbox | function | - | - | deferred — cross-platform sandbox detection |
| SDL_OnApplication* | function (7) | - | - | Platform: lifecycle callbacks |
| SDL_GetGDKTaskQueue | function | - | - | Platform: Xbox GDK only |
| SDL_GetGDKDefaultUser | function | - | - | Platform: Xbox GDK only |

## SDL_thread.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_Thread | struct | - | - | .NET has System.Threading.Thread |
| SDL_ThreadID | typedef | - | - | .NET has Thread.ManagedThreadId |
| SDL_TLSID | typedef | - | - | .NET has ThreadLocal<T> |
| SDL_ThreadPriority | enum | - | - | .NET has Thread.Priority |
| SDL_ThreadState | enum | - | - | .NET; only meaningful with SDL_GetThreadState |
| SDL_ThreadFunction | callback | - | - | .NET has ThreadStart |
| SDL_TLSDestructorCallback | callback | - | - | .NET has ThreadLocal<T> |
| SDL_CreateThread | function | - | - | .NET has new Thread() / Task.Run |
| SDL_CreateThreadWithProperties | function | - | - | .NET has new Thread() / Task.Run |
| SDL_PROP_THREAD_CREATE_* | macro (4) | - | - | .NET; property keys for SDL_CreateThreadWithProperties |
| SDL_CreateThreadRuntime | function | - | - | macro; real export behind SDL_CreateThread, .NET |
| SDL_CreateThreadWithPropertiesRuntime | function | - | - | macro; real export behind SDL_CreateThreadWithProperties, .NET |
| SDL_GetThreadName | function | - | - | .NET has Thread.Name |
| SDL_GetCurrentThreadID | function | - | - | .NET has Thread.CurrentThread |
| SDL_GetThreadID | function | - | - | .NET has Thread.ManagedThreadId |
| SDL_SetCurrentThreadPriority | function | - | - | .NET has Thread.Priority |
| SDL_WaitThread | function | - | - | .NET has Thread.Join |
| SDL_GetThreadState | function | - | - | .NET has Thread.ThreadState |
| SDL_DetachThread | function | - | - | .NET has IsBackground = true |
| SDL_GetTLS | function | - | - | .NET has ThreadLocal<T> |
| SDL_SetTLS | function | - | - | .NET has ThreadLocal<T> |
| SDL_CleanupTLS | function | - | - | .NET; only needed when SDL TLS is used on non-SDL threads |

## SDL_time.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_DateTime | struct | Time.SDL_DateTime | - | Used internally by SdlDateTime |
| SDL_DateFormat | enum | Time.SDL_DateFormat | DateFormat | |
| SDL_TimeFormat | enum | Time.SDL_TimeFormat | TimeFormat | |
| SDL_GetDateTimeLocalePreferences | function | Time.SDL_GetDateTimeLocalePreferences | SdlDateTime.GetPreferredDateFormat / SdlDateTime.GetPreferredTimeFormat | |
| SDL_GetCurrentTime | function | Time.SDL_GetCurrentTime | SdlDateTime.GetCurrentTime | |
| SDL_TimeToDateTime | function | Time.SDL_TimeToDateTime | SdlDateTime.ToDateTime | |
| SDL_DateTimeToTime | function | Time.SDL_DateTimeToTime | SdlDateTime.FromDateTime | |
| SDL_TimeToWindows | function | - | - | Platform: Windows FILETIME interop |
| SDL_TimeFromWindows | function | - | - | Platform: Windows FILETIME interop |
| SDL_GetDaysInMonth | function | - | - | .NET has DateTime.DaysInMonth |
| SDL_GetDayOfYear | function | - | - | .NET has DateTime.DayOfYear |
| SDL_GetDayOfWeek | function | - | - | .NET has DateTime.DayOfWeek |

## SDL_timer.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_MS_PER_SECOND / SDL_US_PER_SECOND / SDL_NS_PER_SECOND / SDL_NS_PER_MS / SDL_NS_PER_US | constant | - | - | .NET has TimeSpan (5 unit-conversion constants) |
| SDL_SECONDS_TO_NS / SDL_NS_TO_SECONDS / SDL_MS_TO_NS / SDL_NS_TO_MS / SDL_US_TO_NS / SDL_NS_TO_US | macro | - | - | Macro: unit conversion; .NET has TimeSpan (6 macros) |
| SDL_TimerID | typedef | Timer.SDL_TimerID | - | Bound but only needed by the skipped callback-timer APIs |
| SDL_GetTicks | function | Timer.SDL_GetTicks | SdlTimer.Ticks | |
| SDL_GetTicksNS | function | Timer.SDL_GetTicksNS | SdlTimer.TicksNS | |
| SDL_GetPerformanceCounter | function | Timer.SDL_GetPerformanceCounter | SdlTimer.PerformanceCounter | |
| SDL_GetPerformanceFrequency | function | Timer.SDL_GetPerformanceFrequency | SdlTimer.PerformanceFrequency | |
| SDL_Delay | function | Timer.SDL_Delay | SdlTimer.Delay | |
| SDL_DelayNS | function | Timer.SDL_DelayNS | SdlTimer.DelayNS | |
| SDL_DelayPrecise | function | Timer.SDL_DelayPrecise | SdlTimer.DelayPrecise | |
| SDL_TimerCallback | callback | - | - | .NET has System.Threading.Timer / System.Timers.Timer (documented skip in Timer.cs) |
| SDL_AddTimer | function | - | - | .NET has System.Threading.Timer / System.Timers.Timer (documented skip in Timer.cs) |
| SDL_NSTimerCallback | callback | - | - | .NET has System.Threading.Timer / System.Timers.Timer (documented skip in Timer.cs) |
| SDL_AddTimerNS | function | - | - | .NET has System.Threading.Timer / System.Timers.Timer (documented skip in Timer.cs) |
| SDL_RemoveTimer | function | - | - | .NET has System.Threading.Timer / System.Timers.Timer (documented skip in Timer.cs) |

## SDL_touch.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_TouchID | typedef | - | - | Raw ulong at both layers |
| SDL_FingerID | typedef | - | - | Raw ulong in bindings; managed Finger.Id is long |
| SDL_TouchDeviceType | enum | Touch.SDL_TouchDeviceType | TouchDeviceType | |
| SDL_Finger | struct | Touch.SDL_Finger | Finger | |
| SDL_TOUCH_MOUSEID | macro | Mouse.SDL_TOUCH_MOUSEID | - | Deferred managed constant |
| SDL_MOUSE_TOUCHID | macro | Touch.SDL_MOUSE_TOUCHID | - | Deferred managed constant |
| SDL_GetTouchDevices | function | Touch.SDL_GetTouchDevices | TouchDevice.GetDevices | |
| SDL_GetTouchDeviceName | function | Touch.SDL_GetTouchDeviceName | TouchDevice.GetName | |
| SDL_GetTouchDeviceType | function | Touch.SDL_GetTouchDeviceType | TouchDevice.GetType | |
| SDL_GetTouchFingers | function | Touch.SDL_GetTouchFingers | TouchDevice.GetFingers | |

## SDL_tray.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_Tray | opaque | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_TrayMenu | opaque | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_TrayEntry | opaque | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_TrayEntryFlags | typedef | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_TRAYENTRY_BUTTON | constant | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_TRAYENTRY_CHECKBOX | constant | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_TRAYENTRY_SUBMENU | constant | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_TRAYENTRY_DISABLED | constant | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_TRAYENTRY_CHECKED | constant | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_TrayCallback | callback | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_CreateTray | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_SetTrayIcon | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_SetTrayTooltip | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_CreateTrayMenu | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_CreateTraySubmenu | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_GetTrayMenu | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_GetTraySubmenu | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_GetTrayEntries | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_RemoveTrayEntry | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_InsertTrayEntryAt | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_SetTrayEntryLabel | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_GetTrayEntryLabel | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_SetTrayEntryChecked | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_GetTrayEntryChecked | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_SetTrayEntryEnabled | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_GetTrayEntryEnabled | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_SetTrayEntryCallback | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_ClickTrayEntry | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_DestroyTray | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_GetTrayEntryParent | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_GetTrayMenuParentEntry | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_GetTrayMenuParentTray | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |
| SDL_UpdateTrays | function | - | - | deferred — wrap planned (TODO.md Phase 11.2) |

## SDL_version.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_MAJOR_VERSION | macro | - | - | macro — compile-time header version |
| SDL_MINOR_VERSION | macro | - | - | macro — compile-time header version |
| SDL_MICRO_VERSION | macro | - | - | macro — compile-time header version |
| SDL_VERSIONNUM | macro | - | - | macro — packs major/minor/micro into an int |
| SDL_VERSIONNUM_MAJOR | macro | - | - | macro — decodes SDL_GetVersion result |
| SDL_VERSIONNUM_MINOR | macro | - | - | macro — decodes SDL_GetVersion result |
| SDL_VERSIONNUM_MICRO | macro | - | - | macro — decodes SDL_GetVersion result |
| SDL_VERSION | macro | - | - | macro — compile-time header version |
| SDL_VERSION_ATLEAST | macro | - | - | macro — compile-time version check |
| SDL_GetVersion | function | - | - | Deferred |
| SDL_GetRevision | function | - | - | Deferred |

## SDL_video.h

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_DisplayID | typedef | Video.SDL_DisplayID | - | Internal ID type |
| SDL_WindowID | typedef | Video.SDL_WindowID | - | Internal ID type |
| SDL_SystemTheme | enum | Video.SDL_SystemTheme | - | Deferred managed wrapper |
| SDL_DisplayModeData | struct | - | - | Opaque internal data of SDL_DisplayMode |
| SDL_DisplayMode | struct | Video.SDL_DisplayMode | DisplayMode | |
| SDL_DisplayOrientation | enum | Video.SDL_DisplayOrientation | - | Deferred managed wrapper |
| SDL_Window | opaque | Video.SDL_Window | Window | |
| SDL_WindowFlags | typedef | Video.SDL_WindowFlags | WindowFlags | |
| SDL_WINDOWPOS_UNDEFINED_MASK | macro | Video.SDL_WINDOWPOS_UNDEFINED_MASK | - | Deferred: per-display positioning |
| SDL_WINDOWPOS_UNDEFINED_DISPLAY(X) | macro | - | - | Deferred: per-display positioning |
| SDL_WINDOWPOS_UNDEFINED | macro | Video.SDL_WINDOWPOS_UNDEFINED | Window.UndefinedPosition | |
| SDL_WINDOWPOS_ISUNDEFINED(X) | macro | - | - | Deferred: per-display positioning |
| SDL_WINDOWPOS_CENTERED_MASK | macro | Video.SDL_WINDOWPOS_CENTERED_MASK | - | Deferred: per-display positioning |
| SDL_WINDOWPOS_CENTERED_DISPLAY(X) | macro | - | - | Deferred: per-display positioning |
| SDL_WINDOWPOS_CENTERED | macro | Video.SDL_WINDOWPOS_CENTERED | Window.CenteredPosition | |
| SDL_WINDOWPOS_ISCENTERED(X) | macro | - | - | Deferred: per-display positioning |
| SDL_FlashOperation | enum | Video.SDL_FlashOperation | FlashOperation | |
| SDL_GetNumVideoDrivers | function | Video.SDL_GetNumVideoDrivers | Window.NumVideoDrivers | |
| SDL_GetVideoDriver | function | Video.SDL_GetVideoDriver | Window.GetVideoDriver | |
| SDL_GetCurrentVideoDriver | function | Video.SDL_GetCurrentVideoDriver | Window.CurrentVideoDriver | |
| SDL_GetDisplays | function | Video.SDL_GetDisplays | Display.GetAll | |
| SDL_GetPrimaryDisplay | function | Video.SDL_GetPrimaryDisplay | Display.Primary | |
| SDL_GetDisplayName | function | Video.SDL_GetDisplayName | Display.Name | |
| SDL_GetDisplayBounds | function | Video.SDL_GetDisplayBounds | Display.Bounds | |
| SDL_GetDisplayUsableBounds | function | Video.SDL_GetDisplayUsableBounds | Display.UsableBounds | |
| SDL_GetDisplayContentScale | function | Video.SDL_GetDisplayContentScale | Display.ContentScale | |
| SDL_GetDesktopDisplayMode | function | Video.SDL_GetDesktopDisplayMode | Display.DesktopDisplayMode | |
| SDL_GetCurrentDisplayMode | function | Video.SDL_GetCurrentDisplayMode | Display.CurrentDisplayMode | |
| SDL_GetDisplayForWindow | function | Video.SDL_GetDisplayForWindow | Window.GetDisplay | |
| SDL_GetWindowPixelDensity | function | Video.SDL_GetWindowPixelDensity | Window.PixelDensity | |
| SDL_GetWindowDisplayScale | function | Video.SDL_GetWindowDisplayScale | Window.DisplayScale | |
| SDL_CreateWindow | function | Video.SDL_CreateWindow | Window.Create | |
| SDL_CreateWindowWithProperties | function | Video.SDL_CreateWindowWithProperties | Window.Create | |
| SDL_GetWindowID | function | Video.SDL_GetWindowID | Window.Id | |
| SDL_GetWindowFromID | function | Video.SDL_GetWindowFromID | Window.FromId | |
| SDL_GetWindowProperties | function | Video.SDL_GetWindowProperties | Window.Properties | |
| SDL_GetWindowFlags | function | Video.SDL_GetWindowFlags | Window.Flags | |
| SDL_SetWindowTitle | function | Video.SDL_SetWindowTitle | Window.Title (set) | |
| SDL_GetWindowTitle | function | Video.SDL_GetWindowTitle | Window.Title (get) | |
| SDL_SetWindowIcon | function | Video.SDL_SetWindowIcon | Window.SetIcon | |
| SDL_SetWindowPosition | function | Video.SDL_SetWindowPosition | Window.Position (set) | |
| SDL_GetWindowPosition | function | Video.SDL_GetWindowPosition | Window.Position (get) | |
| SDL_SetWindowSize | function | Video.SDL_SetWindowSize | Window.Size (set) | |
| SDL_GetWindowSize | function | Video.SDL_GetWindowSize | Window.Size (get) | |
| SDL_GetWindowSizeInPixels | function | Video.SDL_GetWindowSizeInPixels | Window.SizeInPixels | |
| SDL_SetWindowMinimumSize | function | Video.SDL_SetWindowMinimumSize | Window.MinimumSize (set) | |
| SDL_GetWindowMinimumSize | function | Video.SDL_GetWindowMinimumSize | Window.MinimumSize (get) | |
| SDL_SetWindowMaximumSize | function | Video.SDL_SetWindowMaximumSize | Window.MaximumSize (set) | |
| SDL_GetWindowMaximumSize | function | Video.SDL_GetWindowMaximumSize | Window.MaximumSize (get) | |
| SDL_SetWindowBordered | function | Video.SDL_SetWindowBordered | Window.SetBordered | |
| SDL_SetWindowResizable | function | Video.SDL_SetWindowResizable | Window.SetResizable | |
| SDL_SetWindowFullscreen | function | Video.SDL_SetWindowFullscreen | Window.SetFullscreen | |
| SDL_ShowWindow | function | Video.SDL_ShowWindow | Window.Show | |
| SDL_HideWindow | function | Video.SDL_HideWindow | Window.Hide | |
| SDL_RaiseWindow | function | Video.SDL_RaiseWindow | Window.Raise | |
| SDL_MaximizeWindow | function | Video.SDL_MaximizeWindow | Window.Maximize | |
| SDL_MinimizeWindow | function | Video.SDL_MinimizeWindow | Window.Minimize | |
| SDL_RestoreWindow | function | Video.SDL_RestoreWindow | Window.Restore | |
| SDL_SetWindowOpacity | function | Video.SDL_SetWindowOpacity | Window.Opacity (set) | |
| SDL_GetWindowOpacity | function | Video.SDL_GetWindowOpacity | Window.Opacity (get) | |
| SDL_FlashWindow | function | Video.SDL_FlashWindow | Window.Flash | |
| SDL_GetWindowSurface | function | Video.SDL_GetWindowSurface | Window.GetSurface | |
| SDL_UpdateWindowSurface | function | Video.SDL_UpdateWindowSurface | Window.UpdateSurface | |
| SDL_DestroyWindowSurface | function | Video.SDL_DestroyWindowSurface | Window.DestroyWindowSurface | |
| SDL_DestroyWindow | function | Video.SDL_DestroyWindow | Window.Dispose | |
| SDL_GetSystemTheme | function | - | - | Deferred |
| SDL_GetDisplayProperties | function | - | - | Deferred |
| SDL_GetNaturalDisplayOrientation | function | - | - | Deferred |
| SDL_GetCurrentDisplayOrientation | function | - | - | Deferred |
| SDL_GetFullscreenDisplayModes | function | - | - | Deferred: fullscreen display mode management |
| SDL_GetClosestFullscreenDisplayMode | function | - | - | Deferred: fullscreen display mode management |
| SDL_SetWindowFullscreenMode | function | - | - | Deferred: fullscreen display mode management |
| SDL_GetWindowFullscreenMode | function | - | - | Deferred: fullscreen display mode management |
| SDL_GetDisplayForPoint | function | - | - | Deferred |
| SDL_GetDisplayForRect | function | - | - | Deferred |
| SDL_GetWindows | function | - | - | Deferred |
| SDL_GetWindowPixelFormat | function | - | - | Deferred |
| SDL_GetWindowICCProfile | function | - | - | Deferred: ICC profile |
| SDL_CreatePopupWindow | function | - | - | Deferred: popup windows |
| SDL_GetWindowParent | function | - | - | Deferred: popup windows |
| SDL_GetWindowSafeArea | function | - | - | Deferred |
| SDL_SetWindowAspectRatio | function | - | - | Deferred |
| SDL_GetWindowAspectRatio | function | - | - | Deferred |
| SDL_GetWindowBordersSize | function | - | - | Deferred |
| SDL_SetWindowAlwaysOnTop | function | - | - | Deferred |
| SDL_SetWindowFillDocument | function | - | - | Deferred; platform (Emscripten-only) |
| SDL_SyncWindow | function | - | - | Deferred |
| SDL_ShowWindowSystemMenu | function | - | - | Deferred |
| SDL_SetWindowKeyboardGrab | function | - | - | Deferred: grab management |
| SDL_SetWindowMouseGrab | function | - | - | Deferred: grab management |
| SDL_GetWindowKeyboardGrab | function | - | - | Deferred: grab management |
| SDL_GetWindowMouseGrab | function | - | - | Deferred: grab management |
| SDL_GetGrabbedWindow | function | - | - | Deferred: grab management |
| SDL_SetWindowMouseRect | function | - | - | Deferred: grab management |
| SDL_GetWindowMouseRect | function | - | - | Deferred: grab management |
| SDL_SetWindowParent | function | - | - | Deferred: modal windows |
| SDL_SetWindowModal | function | - | - | Deferred: modal windows |
| SDL_SetWindowFocusable | function | - | - | Deferred |
| SDL_SetWindowHitTest | function | - | - | Deferred: hit test callback |
| SDL_SetWindowShape | function | - | - | Deferred |
| SDL_SetWindowProgressState | function | - | - | Deferred: progress state |
| SDL_GetWindowProgressState | function | - | - | Deferred: progress state |
| SDL_SetWindowProgressValue | function | - | - | Deferred: progress state |
| SDL_GetWindowProgressValue | function | - | - | Deferred: progress state |
| SDL_ScreenSaverEnabled | function | - | - | Deferred: screen saver |
| SDL_EnableScreenSaver | function | - | - | Deferred: screen saver |
| SDL_DisableScreenSaver | function | - | - | Deferred: screen saver |
| SDL_WindowHasSurface | function | - | - | Deferred |
| SDL_UpdateWindowSurfaceRects | function | - | - | Deferred |
| SDL_SetWindowSurfaceVSync | function | - | - | Deferred: surface vsync |
| SDL_GetWindowSurfaceVSync | function | - | - | Deferred: surface vsync |
| SDL_GL_LoadLibrary | function | - | - | Deferred: all GL/EGL functions |
| SDL_GL_GetProcAddress | function | - | - | Deferred: all GL/EGL functions |
| SDL_GL_UnloadLibrary | function | - | - | Deferred: all GL/EGL functions |
| SDL_GL_ExtensionSupported | function | - | - | Deferred: all GL/EGL functions |
| SDL_GL_ResetAttributes | function | - | - | Deferred: all GL/EGL functions |
| SDL_GL_SetAttribute | function | - | - | Deferred: all GL/EGL functions |
| SDL_GL_GetAttribute | function | - | - | Deferred: all GL/EGL functions |
| SDL_GL_CreateContext | function | - | - | Deferred: all GL/EGL functions |
| SDL_GL_MakeCurrent | function | - | - | Deferred: all GL/EGL functions |
| SDL_GL_GetCurrentWindow | function | - | - | Deferred: all GL/EGL functions |
| SDL_GL_GetCurrentContext | function | - | - | Deferred: all GL/EGL functions |
| SDL_GL_SetSwapInterval | function | - | - | Deferred: all GL/EGL functions |
| SDL_GL_GetSwapInterval | function | - | - | Deferred: all GL/EGL functions |
| SDL_GL_SwapWindow | function | - | - | Deferred: all GL/EGL functions |
| SDL_GL_DestroyContext | function | - | - | Deferred: all GL/EGL functions |
| SDL_EGL_GetProcAddress | function | - | - | Deferred: all GL/EGL functions |
| SDL_EGL_GetCurrentDisplay | function | - | - | Deferred: all GL/EGL functions |
| SDL_EGL_GetCurrentConfig | function | - | - | Deferred: all GL/EGL functions |
| SDL_EGL_GetWindowSurface | function | - | - | Deferred: all GL/EGL functions |
| SDL_EGL_SetAttributeCallbacks | function | - | - | Deferred: all GL/EGL functions |
| SDL_ProgressState | enum | - | - | Deferred: progress state |
| SDL_GLContext | typedef | - | - | Deferred: GL types |
| SDL_GLAttr | enum | - | - | Deferred: GL types |
| SDL_GLProfile | typedef | - | - | Deferred: GL types (SDL_GL_CONTEXT_PROFILE_* constants) |
| SDL_GLContextFlag | typedef | - | - | Deferred: GL types (SDL_GL_CONTEXT_*_FLAG constants) |
| SDL_GLContextReleaseFlag | typedef | - | - | Deferred: GL types (SDL_GL_CONTEXT_RELEASE_BEHAVIOR_* constants) |
| SDL_GLContextResetNotification | typedef | - | - | Deferred: GL types (SDL_GL_CONTEXT_RESET_* constants) |
| SDL_EGLDisplay / SDL_EGLConfig / SDL_EGLSurface / SDL_EGLAttrib / SDL_EGLint | typedef (5) | - | - | Deferred: EGL types |
| SDL_EGLAttribArrayCallback / SDL_EGLIntArrayCallback | callback (2) | - | - | Deferred: EGL attribute callbacks |
| SDL_HitTestResult | enum | - | - | Deferred: hit test |
| SDL_HitTest | callback | - | - | Deferred: hit test callback |
| SDL_WINDOW_SURFACE_VSYNC_DISABLED / SDL_WINDOW_SURFACE_VSYNC_ADAPTIVE | macro (2) | - | - | Deferred: surface vsync |
| SDL_PROP_GLOBAL_VIDEO_WAYLAND_WL_DISPLAY_POINTER | macro | - | - | Deferred: property string constant; platform |
| SDL_PROP_DISPLAY_* | macro (4) | - | - | Deferred: property string constants |
| SDL_PROP_WINDOW_CREATE_* | macro (38) | - | - | Deferred: property string constants |
| SDL_PROP_WINDOW_* | macro (37) | - | - | Deferred: property string constants |

## SDL_vulkan.h ✅

| SDL Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SDL_Vulkan_LoadLibrary | function | - | - | Niche: use Silk.NET or Vortice for Vulkan; C# bindings load the Vulkan loader themselves |
| SDL_Vulkan_GetVkGetInstanceProcAddr | function | - | - | Niche: Vulkan interop; C# binding libraries provide their own loader entry point |
| SDL_Vulkan_UnloadLibrary | function | - | - | Niche: Vulkan interop |
| SDL_Vulkan_GetInstanceExtensions | function | - | - | Niche: deliberate skip. SDL-window surface glue not covered by Silk.NET/Vortice; Vulkan users can reach raw window handles via window properties |
| SDL_Vulkan_CreateSurface | function | - | - | Niche: deliberate skip. SDL-window→VkSurfaceKHR glue not covered by Silk.NET/Vortice; Vulkan users can reach raw window handles via window properties |
| SDL_Vulkan_DestroySurface | function | - | - | Niche: Vulkan interop; equivalent to vkDestroySurfaceKHR |
| SDL_Vulkan_GetPresentationSupport | function | - | - | Niche: Vulkan interop; redundant with vkGetPhysicalDevice*PresentationSupport |
