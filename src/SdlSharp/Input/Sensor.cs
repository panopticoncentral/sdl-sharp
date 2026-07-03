// src/SdlSharp/Input/Sensor.cs
using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Sensor;

namespace SdlSharp.Input;

/// <summary>
/// Managed wrapper for an SDL sensor device.
/// </summary>
public sealed unsafe class Sensor : IDisposable
{
    private readonly bool _ownsHandle;

    internal Native.SDL_Sensor* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private Native.SDL_Sensor* _handle;

    internal Sensor(Native.SDL_Sensor* handle, bool ownsHandle = true)
    {
        _handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>Opens a sensor by instance ID.</summary>
    public static Sensor Open(uint id) =>
        new(Check(SDL_OpenSensor(id)));

    /// <summary>Gets the instance IDs of all connected sensors.</summary>
    public static uint[] GetDevices()
    {
        int count;
        var ids = SDL_GetSensors(&count);
        if (ids == null) return [];
        try
        {
            var result = new uint[count];
            for (var i = 0; i < count; i++)
                result[i] = ids[i];
            return result;
        }
        finally
        {
            SDL_free(ids);
        }
    }

    /// <summary>Gets the name of a sensor by instance ID.</summary>
    public static string? GetName(uint id) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetSensorNameForID(id));

    /// <summary>Gets the type of a sensor by instance ID.</summary>
    public static SensorType GetType(uint id) =>
        (SensorType)SDL_GetSensorTypeForID(id);

    /// <summary>Gets the name of this sensor.</summary>
    public string? Name => Marshal.PtrToStringUTF8((nint)SDL_GetSensorName(Handle));

    /// <summary>Gets the type of this sensor.</summary>
    public SensorType Type => (SensorType)SDL_GetSensorType(Handle);

    /// <summary>Gets the non-portable type of this sensor.</summary>
    public int NonPortableType => SDL_GetSensorNonPortableType(Handle);

    /// <summary>Gets the current sensor data.</summary>
    /// <param name="data">A span to receive the sensor data values.</param>
    public void GetData(Span<float> data)
    {
        fixed (float* ptr = data)
            Check(SDL_GetSensorData(Handle, ptr, data.Length));
    }

    /// <summary>Gets the properties associated with this sensor.</summary>
    public PropertyGroup Properties =>
        new(CheckId(SDL_GetSensorProperties(Handle)), ownsHandle: false);

    /// <summary>Updates the state of all open sensors.</summary>
    public static void Update() => SDL_UpdateSensors();

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && _handle != null)
        {
            SDL_CloseSensor(_handle);
        }
        _handle = null;
    }
}
