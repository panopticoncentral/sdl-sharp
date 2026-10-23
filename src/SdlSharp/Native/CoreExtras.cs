using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace SdlSharp.Native;

public enum SDL_EventAction { SDL_ADDEVENT, SDL_PEEKEVENT, SDL_GETEVENT }
public enum SDL_GamepadBindingType { SDL_GAMEPAD_BINDTYPE_NONE, SDL_GAMEPAD_BINDTYPE_BUTTON, SDL_GAMEPAD_BINDTYPE_AXIS, SDL_GAMEPAD_BINDTYPE_HAT }


[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GamepadBinding
{
    public SDL_GamepadBindingType input_type;
    public fixed int input[3];
    public SDL_GamepadBindingType output_type;
    public fixed int output[3];
}
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_CursorFrameInfo { public SDL_Surface* surface; public uint duration; }
public struct SDL_GPURenderState;
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPURenderStateCreateInfo
{
    public SDL_GPUShader* fragment_shader;
    public int num_sampler_bindings;
    public SDL_GPUTextureSamplerBinding* sampler_bindings;
    public int num_storage_textures;
    public SDL_GPUTexture** storage_textures;
    public int num_storage_buffers;
    public SDL_GPUBuffer** storage_buffers;
    public SDL_PropertiesID props;
}
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_VirtualJoystickTouchpadDesc { public ushort nfingers; public fixed ushort padding[3]; }
[StructLayout(LayoutKind.Sequential)]
public struct SDL_VirtualJoystickSensorDesc { public SDL_SensorType type; public float rate; }
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_VirtualJoystickDesc
{
    public uint version;
    public ushort type;
    public ushort padding;
    public ushort vendor_id;
    public ushort product_id;
    public ushort naxes;
    public ushort nbuttons;
    public ushort nballs;
    public ushort nhats;
    public ushort ntouchpads;
    public ushort nsensors;
    public fixed ushort padding2[2];
    public uint button_mask;
    public uint axis_mask;
    public byte* name;
    public SDL_VirtualJoystickTouchpadDesc* touchpads;
    public SDL_VirtualJoystickSensorDesc* sensors;
    public void* userdata;
    public delegate* unmanaged[Cdecl]<void*, void> Update;
    public delegate* unmanaged[Cdecl]<void*, int, void> SetPlayerIndex;
    public delegate* unmanaged[Cdecl]<void*, ushort, ushort, byte> Rumble;
    public delegate* unmanaged[Cdecl]<void*, ushort, ushort, byte> RumbleTriggers;
    public delegate* unmanaged[Cdecl]<void*, byte, byte, byte, byte> SetLED;
    public delegate* unmanaged[Cdecl]<void*, void*, int, byte> SendEffect;
    public delegate* unmanaged[Cdecl]<void*, byte, byte> SetSensorsEnabled;
    public delegate* unmanaged[Cdecl]<void*, void> Cleanup;
}

/// <summary>Additional SDL bindings.</summary>
public static partial class Events
{
    /// <summary>Calls SDL_PeepEvents.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PeepEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_PeepEvents(SDL_Event* events, int count, SDL_EventAction action, uint minType, uint maxType);
}

/// <summary>Additional SDL bindings.</summary>
public static partial class Gamepad
{
    /// <summary>Calls SDL_GetGamepadBindings.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadBindings")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GamepadBinding** SDL_GetGamepadBindings(SDL_Gamepad* gamepad, int* count);
}

/// <summary>Additional SDL bindings.</summary>
public static partial class Joystick
{
    /// <summary>Calls SDL_LockJoysticks.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_LockJoysticks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_LockJoysticks();
    /// <summary>Calls SDL_UnlockJoysticks.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UnlockJoysticks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_UnlockJoysticks();
    /// <summary>Calls SDL_AttachVirtualJoystick.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_AttachVirtualJoystick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial uint SDL_AttachVirtualJoystick(SDL_VirtualJoystickDesc* description);
    /// <summary>Calls SDL_DetachVirtualJoystick.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DetachVirtualJoystick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_DetachVirtualJoystick(uint id);
    /// <summary>Calls SDL_IsJoystickVirtual.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_IsJoystickVirtual")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_IsJoystickVirtual(uint id);
    /// <summary>Calls SDL_SetJoystickVirtualAxis.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetJoystickVirtualAxis")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetJoystickVirtualAxis(SDL_Joystick* joystick, int axis, short value);
    /// <summary>Calls SDL_SetJoystickVirtualBall.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetJoystickVirtualBall")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetJoystickVirtualBall(SDL_Joystick* joystick, int ball, short x, short y);
    /// <summary>Calls SDL_SetJoystickVirtualButton.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetJoystickVirtualButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetJoystickVirtualButton(SDL_Joystick* joystick, int button, [MarshalAs(UnmanagedType.U1)] bool down);
    /// <summary>Calls SDL_SetJoystickVirtualHat.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetJoystickVirtualHat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetJoystickVirtualHat(SDL_Joystick* joystick, int hat, byte value);
    /// <summary>Calls SDL_SetJoystickVirtualTouchpad.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetJoystickVirtualTouchpad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetJoystickVirtualTouchpad(SDL_Joystick* joystick, int touchpad, int finger, [MarshalAs(UnmanagedType.U1)] bool down, float x, float y, float pressure);
    /// <summary>Calls SDL_SendJoystickVirtualSensorData.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SendJoystickVirtualSensorData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SendJoystickVirtualSensorData(SDL_Joystick* joystick, SDL_SensorType type, ulong timestamp, float* data, int count);
    /// <summary>Calls SDL_GetJoystickGUIDInfo.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickGUIDInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_GetJoystickGUIDInfo(SDL_GUID guid, ushort* vendor, ushort* product, ushort* version, ushort* crc);
}

/// <summary>Additional SDL bindings.</summary>
public static partial class Metal
{
    /// <summary>Calls SDL_Metal_CreateView.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_Metal_CreateView")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial nint SDL_Metal_CreateView(SDL_Window* window);
    /// <summary>Calls SDL_Metal_DestroyView.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_Metal_DestroyView")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_Metal_DestroyView(nint view);
    /// <summary>Calls SDL_Metal_GetLayer.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_Metal_GetLayer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial nint SDL_Metal_GetLayer(nint view);
}

/// <summary>Additional SDL bindings.</summary>
public static partial class Mouse
{
    /// <summary>Calls SDL_SetRelativeMouseTransform.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetRelativeMouseTransform")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetRelativeMouseTransform(delegate* unmanaged[Cdecl]<void*, ulong, SDL_Window*, uint, float*, float*, void> callback, void* userdata);
    /// <summary>Calls SDL_CreateCursor.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Cursor* SDL_CreateCursor(byte* data, byte* mask, int width, int height, int hotX, int hotY);
    /// <summary>Calls SDL_CreateAnimatedCursor.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateAnimatedCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Cursor* SDL_CreateAnimatedCursor(SDL_CursorFrameInfo* frames, int count, int hotX, int hotY);
}

/// <summary>Additional SDL bindings.</summary>
public static partial class Render
{
    /// <summary>Calls SDL_GetRenderMetalLayer.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRenderMetalLayer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial nint SDL_GetRenderMetalLayer(SDL_Renderer* renderer);
    /// <summary>Calls SDL_GetRenderMetalCommandEncoder.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRenderMetalCommandEncoder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial nint SDL_GetRenderMetalCommandEncoder(SDL_Renderer* renderer);
    /// <summary>Calls SDL_AddVulkanRenderSemaphores.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_AddVulkanRenderSemaphores")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_AddVulkanRenderSemaphores(SDL_Renderer* renderer, uint waitStageMask, long waitSemaphore, long signalSemaphore);
    /// <summary>Calls SDL_CreateGPURenderState.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateGPURenderState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPURenderState* SDL_CreateGPURenderState(SDL_Renderer* renderer, SDL_GPURenderStateCreateInfo* info);
    /// <summary>Calls SDL_SetGPURenderStateFragmentUniforms.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetGPURenderStateFragmentUniforms")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetGPURenderStateFragmentUniforms(SDL_GPURenderState* state, uint slot, void* data, uint length);
    /// <summary>Calls SDL_SetGPURenderState.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetGPURenderState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetGPURenderState(SDL_Renderer* renderer, SDL_GPURenderState* state);
    /// <summary>Calls SDL_DestroyGPURenderState.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DestroyGPURenderState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DestroyGPURenderState(SDL_GPURenderState* state);
}

/// <summary>Additional SDL bindings.</summary>
public static partial class Surface
{
    /// <summary>Calls SDL_ConvertPixels.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ConvertPixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ConvertPixels(int width, int height, SDL_PixelFormat sourceFormat, void* source, int sourcePitch, SDL_PixelFormat destinationFormat, void* destination, int destinationPitch);
    /// <summary>Calls SDL_ConvertPixelsAndColorspace.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ConvertPixelsAndColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ConvertPixelsAndColorspace(int width, int height, SDL_PixelFormat sourceFormat, SDL_Colorspace sourceColorspace, SDL_PropertiesID sourceProperties, void* source, int sourcePitch, SDL_PixelFormat destinationFormat, SDL_Colorspace destinationColorspace, SDL_PropertiesID destinationProperties, void* destination, int destinationPitch);
    /// <summary>Calls SDL_PremultiplyAlpha.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PremultiplyAlpha")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_PremultiplyAlpha(int width, int height, SDL_PixelFormat sourceFormat, void* source, int sourcePitch, SDL_PixelFormat destinationFormat, void* destination, int destinationPitch, [MarshalAs(UnmanagedType.U1)] bool linear);
}

/// <summary>Additional SDL bindings.</summary>
public static partial class Vulkan
{
    /// <summary>Calls SDL_Vulkan_LoadLibrary.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_Vulkan_LoadLibrary")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_Vulkan_LoadLibrary(ReadOnlySpan<byte> path);
    /// <summary>Calls SDL_Vulkan_GetVkGetInstanceProcAddr.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_Vulkan_GetVkGetInstanceProcAddr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial nint SDL_Vulkan_GetVkGetInstanceProcAddr();
    /// <summary>Calls SDL_Vulkan_UnloadLibrary.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_Vulkan_UnloadLibrary")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_Vulkan_UnloadLibrary();
    /// <summary>Calls SDL_Vulkan_GetInstanceExtensions.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_Vulkan_GetInstanceExtensions")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte** SDL_Vulkan_GetInstanceExtensions(uint* count);
    /// <summary>Calls SDL_Vulkan_CreateSurface.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_Vulkan_CreateSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_Vulkan_CreateSurface(SDL_Window* window, nint instance, nint allocator, ulong* surface);
    /// <summary>Calls SDL_Vulkan_DestroySurface.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_Vulkan_DestroySurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_Vulkan_DestroySurface(nint instance, ulong surface, nint allocator);
    /// <summary>Calls SDL_Vulkan_GetPresentationSupport.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_Vulkan_GetPresentationSupport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_Vulkan_GetPresentationSupport(nint instance, nint device, uint queueFamilyIndex);
}
