using System;
using SdlSharp.Graphics;

namespace SdlSharp.Input
{
    /// <summary>
    /// A mouse cursor.
    /// </summary>
    public unsafe sealed class Cursor : NativePointerBase<Native.SDL_Cursor, Cursor>
    {
        /// <summary>
        /// The current cursor.
        /// </summary>
        public static Cursor Current
        {
            get => PointerToInstanceNotNull(Native.SDL_GetCursor());
            set => Native.SDL_SetCursor(value.Pointer);
        }

        /// <summary>
        /// The default cursor.
        /// </summary>
        public static Cursor Default
        {
            get => PointerToInstanceNotNull(Native.SDL_GetDefaultCursor());
        }

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
                    return PointerToInstanceNotNull(Native.SDL_CreateCursor(dataPointer, maskPointer, size.Width, size.Height, hotspot.X, hotspot.Y));
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
            PointerToInstanceNotNull(Native.SDL_CreateColorCursor(surface.Pointer, hotspot.X, hotspot.Y));

        /// <summary>
        /// Creates a new cursor.
        /// </summary>
        /// <param name="systemCursor">The system cursor.</param>
        /// <returns>The cursor.</returns>
        public static Cursor Create(SystemCursor systemCursor) =>
            PointerToInstanceNotNull(Native.SDL_CreateSystemCursor(systemCursor));

        /// <inheritdoc/>
        public override void Dispose()
        {
            Native.SDL_FreeCursor(Pointer);
            base.Dispose();
        }

        /// <summary>
        /// Shows or hides the cursor.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>The old state.</returns>
        public static State Show(State state) =>
            (State)Native.CheckError(Native.SDL_ShowCursor(state));
    }
}
