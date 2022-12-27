using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp.Input
{
    /// <summary>
    /// Base class of a virtual joystick.
    /// </summary>
    public abstract class VirtualJoystickBase : IDisposable
    {
        private static Dictionary<nint, VirtualJoystickBase>? s_instances;

        private static Dictionary<nint, VirtualJoystickBase> Instances => s_instances ??= new();

        /// <summary>
        /// The type of the joystick.
        /// </summary>
        protected internal abstract JoystickType Type { get; }

        /// <summary>
        /// The number of axes.
        /// </summary>
        protected internal abstract ushort Axes { get; }

        /// <summary>
        /// The number of buttons.
        /// </summary>
        protected internal abstract ushort Buttons { get; }

        /// <summary>
        /// The number of hats.
        /// </summary>
        protected internal abstract ushort Hats { get; }

        /// <summary>
        /// The vendor ID.
        /// </summary>
        protected internal abstract ushort VendorId { get; }

        /// <summary>
        /// The product ID.
        /// </summary>
        protected internal abstract ushort ProductId { get; }

        /// <summary>
        /// The mask of supported buttons.
        /// </summary>
        protected internal abstract uint ButtonMask { get; }

        /// <summary>
        /// The mask of supported axes.
        /// </summary>
        protected internal abstract uint AxisMask { get; }

        /// <summary>
        /// The joystick name.
        /// </summary>
        protected internal abstract string? Name { get; }

        /// <summary>
        /// Constructs a new virtual joystick.
        /// </summary>
        protected VirtualJoystickBase()
        {
            Instances[GetHashCode()] = this;
        }

        /// <summary>
        /// The joystick should be updated.
        /// </summary>
        protected abstract void Update();

        /// <summary>
        /// Sets the player index of the joystick.
        /// </summary>
        /// <param name="playerIndex"></param>
        protected abstract void SetPlayerIndex(int playerIndex);

        /// <summary>
        /// Rumbles the joystick.
        /// </summary>
        /// <param name="lowFrequencyRumble">The low frequency.</param>
        /// <param name="highFrequencyRumble">The high frequency.</param>
        /// <returns>Whether the rumble is supported.</returns>
        protected abstract bool Rumble(ushort lowFrequencyRumble, ushort highFrequencyRumble);

        /// <summary>
        /// Rumbles the joystick triggers.
        /// </summary>
        /// <param name="leftRumble">The left trigger rumble.</param>
        /// <param name="rightRumble">The right trigger rumble.</param>
        /// <returns>Whether the rumble is supported.</returns>
        protected abstract bool RumbleTriggers(ushort leftRumble, ushort rightRumble);

        /// <summary>
        /// Sets the LED values.
        /// </summary>
        /// <param name="red">Red value.</param>
        /// <param name="green">Green value.</param>
        /// <param name="blue">Blue value.</param>
        /// <returns>Whether LED is supported.</returns>
        protected abstract bool SetLed(byte red, byte green, byte blue);

        /// <summary>
        /// Sends a joystick effect.
        /// </summary>
        /// <param name="effect">The effect data.</param>
        /// <returns>Whether the effect is supported.</returns>
        protected abstract bool SendEffect(Span<byte> effect);

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        internal static unsafe void UpdateCallback(nint userData)
        {
            if (Instances.TryGetValue(userData, out var instance))
            {
                instance.Update();
            }
        }

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        internal static unsafe void SetPlayerIndexCallback(nint userData, int playerIndex)
        {
            if (Instances.TryGetValue(userData, out var instance))
            {
                instance.SetPlayerIndex(playerIndex);
            }
        }

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        internal static unsafe int RumbleCallback(nint userData, ushort lowFrequencyRumble, ushort highFrequencyRumble) =>
            Instances.TryGetValue(userData, out var instance) ? instance.Rumble(lowFrequencyRumble, highFrequencyRumble) ? 0 : -1 : -1;

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        internal static unsafe int RumbleTriggersCallback(nint userData, ushort leftRumble, ushort rightRumble) =>
            Instances.TryGetValue(userData, out var instance) ? instance.RumbleTriggers(leftRumble, rightRumble) ? 0 : -1 : -1;

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        internal static unsafe int SetLedCallback(nint userData, byte red, byte green, byte blue) =>
            Instances.TryGetValue(userData, out var instance) ? instance.SetLed(red, green, blue) ? 0 : -1 : -1;

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        internal static unsafe int SendEffectCallback(nint userData, byte* data, int size) =>
            Instances.TryGetValue(userData, out var instance) ? instance.SendEffect(new Span<byte>(data, size)) ? 0 : -1 : -1;

        /// <summary>
        /// Disposes the audio source.
        /// </summary>
        public virtual void Dispose()
        {
            _ = Instances.Remove(GetHashCode());
            GC.SuppressFinalize(this);
        }
    }
}
