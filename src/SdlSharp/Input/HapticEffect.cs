using static SdlSharp.Native.Common;
using static SdlSharp.Native.Haptic;

namespace SdlSharp.Input;

/// <summary>
/// A haptic effect uploaded to a device. Created via <see cref="Haptic.CreateEffect(in HapticConstantEffect)"/>
/// and overloads. Dispose to free the effect slot; effects become invalid when the
/// owning <see cref="Haptic"/> device is disposed. When the effect was created through
/// a non-owning wrapper (see <see cref="Haptic.FromId"/>), dispose the effect before
/// that wrapper — disposing after skips the native release and leaves the device's
/// effect slot occupied until the device itself is closed.
/// </summary>
public sealed unsafe class HapticEffect : IDisposable
{
    /// <summary>Pass as an iteration count or effect length to repeat/run forever.</summary>
    public const uint Infinity = SDL_HAPTIC_INFINITY;

    private readonly Haptic _device;
    private int _id;

    internal HapticEffect(Haptic device, int id)
    {
        _device = device;
        _id = id;
    }

    private Native.SDL_HapticEffectID Id
    {
        get
        {
            ObjectDisposedException.ThrowIf(_id < 0, this);
            return new Native.SDL_HapticEffectID(_id);
        }
    }

    /// <summary>
    /// Runs the effect.
    /// </summary>
    /// <param name="iterations">How many times to repeat, or <see cref="Infinity"/> to repeat until stopped.</param>
    public void Run(uint iterations = 1) => Check(SDL_RunHapticEffect(_device.Handle, Id, iterations));

    /// <summary>Stops playback of this effect.</summary>
    public void Stop() => Check(SDL_StopHapticEffect(_device.Handle, Id));

    /// <summary>
    /// Gets whether the effect is currently playing. Requires the device to support
    /// <see cref="HapticFeatures.Status"/>; returns false (with no exception) when
    /// unsupported or on error.
    /// </summary>
    public bool IsRunning => SDL_GetHapticEffectStatus(_device.Handle, Id);

    /// <summary>Updates this effect in place. The effect kind cannot change (SDL contract).</summary>
    /// <param name="effect">The new parameters.</param>
    public void Update(in HapticConstantEffect effect)
    {
        var native = effect.ToNative();
        Check(SDL_UpdateHapticEffect(_device.Handle, Id, &native));
    }

    /// <summary>Updates this effect in place. The effect kind (waveform family) cannot change (SDL contract).</summary>
    /// <param name="effect">The new parameters.</param>
    public void Update(in HapticPeriodicEffect effect)
    {
        var native = effect.ToNative();
        Check(SDL_UpdateHapticEffect(_device.Handle, Id, &native));
    }

    /// <summary>Updates this effect in place. The effect kind cannot change (SDL contract).</summary>
    /// <param name="effect">The new parameters.</param>
    public void Update(in HapticConditionEffect effect)
    {
        var native = effect.ToNative();
        Check(SDL_UpdateHapticEffect(_device.Handle, Id, &native));
    }

    /// <summary>Updates this effect in place. The effect kind cannot change (SDL contract).</summary>
    /// <param name="effect">The new parameters.</param>
    public void Update(in HapticRampEffect effect)
    {
        var native = effect.ToNative();
        Check(SDL_UpdateHapticEffect(_device.Handle, Id, &native));
    }

    /// <summary>Updates this effect in place. The effect kind cannot change (SDL contract).</summary>
    /// <param name="effect">The new parameters.</param>
    public void Update(in HapticLeftRightEffect effect)
    {
        var native = effect.ToNative();
        Check(SDL_UpdateHapticEffect(_device.Handle, Id, &native));
    }

    /// <summary>Updates this effect in place. The effect kind cannot change (SDL contract).</summary>
    /// <param name="effect">The new parameters.</param>
    public void Update(in HapticCustomEffect effect)
    {
        fixed (ushort* data = effect.Data)
        {
            var native = effect.ToNative(data);
            Check(SDL_UpdateHapticEffect(_device.Handle, Id, &native));
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_id >= 0 && !_device.IsDisposed)
        {
            SDL_DestroyHapticEffect(_device.Handle, new Native.SDL_HapticEffectID(_id));
        }

        _id = -1;
    }
}
