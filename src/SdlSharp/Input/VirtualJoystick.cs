using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Joystick;

namespace SdlSharp.Input;

/// <summary>A virtual sensor's type and update frequency in hertz.</summary>
public readonly record struct VirtualSensor(SensorType Type, float Rate);

/// <summary>Describes a virtual joystick and optional output callbacks. Callbacks may run on an SDL thread.</summary>
public sealed class VirtualJoystickOptions
{
    /// <summary>The advertised name.</summary>
    public string? Name { get; init; }
    /// <summary>The device type.</summary>
    public JoystickType Type { get; init; }
    /// <summary>USB vendor ID.</summary>
    public ushort VendorId { get; init; }
    /// <summary>USB product ID.</summary>
    public ushort ProductId { get; init; }
    /// <summary>Number of axes.</summary>
    public ushort AxisCount { get; init; }
    /// <summary>Number of buttons.</summary>
    public ushort ButtonCount { get; init; }
    /// <summary>Number of balls.</summary>
    public ushort BallCount { get; init; }
    /// <summary>Number of hats.</summary>
    public ushort HatCount { get; init; }
    /// <summary>Mask of available gamepad buttons.</summary>
    public uint ButtonMask { get; init; }
    /// <summary>Mask of available gamepad axes.</summary>
    public uint AxisMask { get; init; }
    /// <summary>Maximum simultaneous contacts for each touchpad.</summary>
    public IReadOnlyList<ushort> TouchpadFingerCounts { get; init; } = [];
    /// <summary>Virtual sensor descriptions.</summary>
    public IReadOnlyList<VirtualSensor> Sensors { get; init; } = [];
    /// <summary>Called when input state should be updated.</summary>
    public Action? Update { get; init; }
    /// <summary>Called when the player index changes.</summary>
    public Action<int>? PlayerIndexChanged { get; init; }
    /// <summary>Handles low- and high-frequency rumble.</summary>
    public Func<ushort, ushort, bool>? Rumble { get; init; }
    /// <summary>Handles left and right trigger rumble.</summary>
    public Func<ushort, ushort, bool>? RumbleTriggers { get; init; }
    /// <summary>Handles an RGB LED color.</summary>
    public Func<byte, byte, byte, bool>? SetLed { get; init; }
    /// <summary>Handles a copied vendor-specific effect packet.</summary>
    public Func<byte[], bool>? SendEffect { get; init; }
    /// <summary>Handles sensor enablement.</summary>
    public Func<bool, bool>? SetSensorsEnabled { get; init; }
}

/// <summary>An attached virtual joystick. Dispose it to close its device and detach it.</summary>
public sealed unsafe class VirtualJoystick : IDisposable
{
    private readonly NativeLifetime _lifetime;
    private bool _disposed;
    /// <summary>The virtual instance ID.</summary>
    public uint Id { get; }
    /// <summary>The opened device, used to send virtual input. Owned by this object.</summary>
    public Joystick Device { get; }
    private VirtualJoystick(uint id)
    {
        Id = id;
        Device = Joystick.Open(id);
        _lifetime = NativeLifetime.Get(typeof(VirtualJoystick), (nint)id, true);
    }
    /// <summary>Attaches a virtual joystick and opens it for input updates.</summary>
    public static VirtualJoystick Create(VirtualJoystickOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        var touchpads = options.TouchpadFingerCounts.Select(n => new Native.SDL_VirtualJoystickTouchpadDesc { nfingers = n }).ToArray();
        var sensors = options.Sensors.Select(s => new Native.SDL_VirtualJoystickSensorDesc { type = (Native.SDL_SensorType)s.Type, rate = s.Rate }).ToArray();
        var token = CallbackHandle.Alloc(options);
        uint id = 0;
        try
        {
            fixed (byte* name = ToUtf8(options.Name))
            fixed (Native.SDL_VirtualJoystickTouchpadDesc* pads = touchpads)
            fixed (Native.SDL_VirtualJoystickSensorDesc* sens = sensors)
            {
                var description = new Native.SDL_VirtualJoystickDesc
                {
                    version = (uint)sizeof(Native.SDL_VirtualJoystickDesc), type = (ushort)options.Type,
                    vendor_id = options.VendorId, product_id = options.ProductId, naxes = options.AxisCount,
                    nbuttons = options.ButtonCount, nballs = options.BallCount, nhats = options.HatCount,
                    ntouchpads = checked((ushort)touchpads.Length), nsensors = checked((ushort)sensors.Length),
                    button_mask = options.ButtonMask, axis_mask = options.AxisMask, name = name, touchpads = pads, sensors = sens,
                    userdata = (void*)(nint)token, Cleanup = &Cleanup,
                    Update = options.Update == null ? null : &Update,
                    SetPlayerIndex = options.PlayerIndexChanged == null ? null : &PlayerIndex,
                    Rumble = options.Rumble == null ? null : &Rumble,
                    RumbleTriggers = options.RumbleTriggers == null ? null : &RumbleTriggers,
                    SetLED = options.SetLed == null ? null : &SetLed,
                    SendEffect = options.SendEffect == null ? null : &SendEffect,
                    SetSensorsEnabled = options.SetSensorsEnabled == null ? null : &SetSensorsEnabled
                };
                id = SDL_AttachVirtualJoystick(&description);
                if (id == 0) throw new SdlException();
            }
            return new VirtualJoystick(id);
        }
        catch { if (id != 0) SDL_DetachVirtualJoystick(id); token.Free(); throw; }
    }
    /// <summary>Closes the device and detaches it once.</summary>
    public void Dispose()
    {
        if (_disposed) return;
        _lifetime.EnsureCanDestroy();
        Device.Dispose();
        _lifetime.Release(() => Check(SDL_DetachVirtualJoystick(Id)));
        _disposed = true;
    }
    private static VirtualJoystickOptions? Options(void* data) => CallbackHandle.FromIntPtr((nint)data).Target as VirtualJoystickOptions;
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void Cleanup(void* data) => CallbackHandle.FromIntPtr((nint)data).Free();
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void Update(void* data)
    {
        try { using var scope = CallbackErrors.Enter(); Options(data)?.Update?.Invoke(); }
        catch (Exception error) { CallbackErrors.Capture(error); }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void PlayerIndex(void* data, int index)
    {
        try { using var scope = CallbackErrors.Enter(); Options(data)?.PlayerIndexChanged?.Invoke(index); }
        catch (Exception error) { CallbackErrors.Capture(error); }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte Rumble(void* data, ushort low, ushort high)
    {
        try { using var scope = CallbackErrors.Enter(); return Options(data)?.Rumble?.Invoke(low, high) == true ? (byte)1 : (byte)0; }
        catch (Exception error) { CallbackErrors.Capture(error); return 0; }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte RumbleTriggers(void* data, ushort left, ushort right)
    {
        try { using var scope = CallbackErrors.Enter(); return Options(data)?.RumbleTriggers?.Invoke(left, right) == true ? (byte)1 : (byte)0; }
        catch (Exception error) { CallbackErrors.Capture(error); return 0; }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte SetLed(void* data, byte red, byte green, byte blue)
    {
        try { using var scope = CallbackErrors.Enter(); return Options(data)?.SetLed?.Invoke(red, green, blue) == true ? (byte)1 : (byte)0; }
        catch (Exception error) { CallbackErrors.Capture(error); return 0; }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte SendEffect(void* data, void* effect, int length)
    {
        try { using var scope = CallbackErrors.Enter(); return Options(data)?.SendEffect?.Invoke(new ReadOnlySpan<byte>(effect, length).ToArray()) == true ? (byte)1 : (byte)0; }
        catch (Exception error) { CallbackErrors.Capture(error); return 0; }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte SetSensorsEnabled(void* data, byte enabled)
    {
        try { using var scope = CallbackErrors.Enter(); return Options(data)?.SetSensorsEnabled?.Invoke(enabled != 0) == true ? (byte)1 : (byte)0; }
        catch (Exception error) { CallbackErrors.Capture(error); return 0; }
    }
}
