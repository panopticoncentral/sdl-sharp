using SdlSharp.Graphics;

namespace SdlSharp.Input
{
    /// <summary>
    /// A mouse cursor.
    /// </summary>
    public sealed unsafe class Cursor : NativePointerBase<Native.SDL_Cursor, Cursor>
    {
        /// <summary>
        /// The current cursor.
        /// </summary>
        public static Cursor Current
        {
            get => PointerToInstanceNotNull(SdlSharp.Native.SDL_GetCursor());
            set => SdlSharp.Native.SDL_SetCursor(value.Native);
        }

        /// <summary>
        /// The default cursor.
        /// </summary>
        public static Cursor Default => PointerToInstanceNotNull(SdlSharp.Native.SDL_GetDefaultCursor());

        /// <summary>
        /// Creates a new cursor.
        /// </summary>
        /// <param name="data">The cursor data.</param>
        /// <param name="mask">The cursor mask.</param>
        /// <param name="size">The size of the cursor.</param>
        /// <param name="hotspot">The location of the hotspot.</param>
        /// <returns>The cursor.</returns>
        public static Cursor Create(Span<byte> data, Span<byte> mask, Size size, Point hotspot)
        {
            fixed (byte* dataPointer = data)
            {
                fixed (byte* maskPointer = mask)
                {
                    return PointerToInstanceNotNull(SdlSharp.Native.SDL_CreateCursor(dataPointer, maskPointer, size.Width, size.Height, hotspot.X, hotspot.Y));
                }
            }
        }

        /// <summary>
        /// Creates a new cursor.
        /// </summary>
        /// <param name="surface">The surface the cursor comes from.</param>
        /// <param name="hotspot">The location of the hotspot.</param>
        /// <returns>The cursor.</returns>
        public static Cursor Create(Surface surface, Point hotspot) =>
            PointerToInstanceNotNull(SdlSharp.Native.SDL_CreateColorCursor(surface.ToNative(), hotspot.X, hotspot.Y));

        /// <summary>
        /// Creates a new cursor.
        /// </summary>
        /// <param name="systemCursor">The system cursor.</param>
        /// <returns>The cursor.</returns>
        public static Cursor Create(SystemCursor systemCursor) =>
            PointerToInstanceNotNull(SdlSharp.Native.SDL_CreateSystemCursor((Native.SDL_SystemCursor)systemCursor));

        /// <inheritdoc/>
        public override void Dispose()
        {
            SdlSharp.Native.SDL_FreeCursor(Native);
            base.Dispose();
        }

        /// <summary>
        /// Shows or hides the cursor.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>The old state.</returns>
        public static State Show(State state) =>
            (State)SdlSharp.Native.CheckError(SdlSharp.Native.SDL_ShowCursor(state));
    }
}
