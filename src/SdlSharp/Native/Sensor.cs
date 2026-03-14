using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// Opaque sensor handle.
/// </summary>
public struct SDL_Sensor;

/// <summary>
/// The different sensors defined by SDL.
/// </summary>
public enum SDL_SensorType
{
    SDL_SENSOR_INVALID = -1,
    SDL_SENSOR_UNKNOWN,
    SDL_SENSOR_ACCEL,
    SDL_SENSOR_GYRO,
    SDL_SENSOR_ACCEL_L,
    SDL_SENSOR_GYRO_L,
    SDL_SENSOR_ACCEL_R,
    SDL_SENSOR_GYRO_R,
    SDL_SENSOR_COUNT,
}

/// <summary>
/// Native bindings for SDL_sensor.h.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Sensor
{
    public const float SDL_STANDARD_GRAVITY = 9.80665f;

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSensors")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial uint* SDL_GetSensors(int* count);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSensorNameForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetSensorNameForID(uint instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSensorTypeForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_SensorType SDL_GetSensorTypeForID(uint instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSensorNonPortableTypeForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetSensorNonPortableTypeForID(uint instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_OpenSensor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Sensor* SDL_OpenSensor(uint instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSensorFromID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Sensor* SDL_GetSensorFromID(uint instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSensorProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_PropertiesID SDL_GetSensorProperties(SDL_Sensor* sensor);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSensorName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetSensorName(SDL_Sensor* sensor);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSensorType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_SensorType SDL_GetSensorType(SDL_Sensor* sensor);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSensorNonPortableType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetSensorNonPortableType(SDL_Sensor* sensor);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSensorID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial uint SDL_GetSensorID(SDL_Sensor* sensor);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSensorData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetSensorData(SDL_Sensor* sensor, float* data, int num_values);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CloseSensor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_CloseSensor(SDL_Sensor* sensor);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UpdateSensors")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UpdateSensors();
}
