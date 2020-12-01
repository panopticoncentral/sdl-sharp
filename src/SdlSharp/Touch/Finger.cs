namespace SdlSharp.Touch
{
    /// <summary>
    /// A finger on a touch device.
    /// </summary>
    public sealed unsafe class Finger : NativePointerBase<Native.SDL_Finger, Finger>
    {
        internal Native.SDL_FingerID Id => Native->Id;

        /// <summary>
        /// The X location of the finger.
        /// </summary>
        public float X => Native->X;

        /// <summary>
        /// The Y location of the finger.
        /// </summary>
        public float Y => Native->Y;

        /// <summary>
        /// The amount of pressure.
        /// </summary>
        public float Pressure => Native->Pressure;
    }
}
