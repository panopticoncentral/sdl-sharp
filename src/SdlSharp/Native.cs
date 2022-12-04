using System.Runtime.InteropServices;

using SdlSharp.Graphics;
using SdlSharp.Input;
using SdlSharp.Sound;
using SdlSharp.Touch;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

// Some interop structures have public fields
#pragma warning disable CA1051 // Do not declare visible instance fields

// Underscores are part of the public P/Invoke surface
#pragma warning disable CA1707 // Remove the underscores from member name ...

// Some interop structures have unused fields
#pragma warning disable CS0169 // The field ... is never used

// For the time being, we're not going to document all of the native APIs, see http://wiki.libsdl.org
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

// Interop structures have unused private members for padding
#pragma warning disable IDE0051 // Private member ... is unused
#pragma warning disable IDE0052 // Private member ... can be removed as the value assigned to it is never read

// Don't want to use LibraryImportAttribute since we don't need pruning at the moment
#pragma warning disable SYSLIB1054

// Naming doesn't follow general rules
#pragma warning disable IDE1006

namespace SdlSharp
{
    /// <summary>
    /// Interop methods to call native SDL APIs.
    /// </summary>
    public static unsafe class Native
    {
        private const string Sdl2 = "SDL2";
        private const string Sdl2Image = "SDL2_image";
        private const string Sdl2Ttf = "SDL2_ttf";
        private const string Sdl2Mixer = "SDL2_mixer";

        /// <summary>
        /// Check that the return of a method is not an error (i.e. less than zero).
        /// </summary>
        /// <param name="returnValue">The return value of the API.</param>
        /// <returns>The return value.</returns>
        /// <exception cref="SdlException">Thrown if method returned an error.</exception>
        public static int CheckError(int returnValue) => (returnValue < 0) ? throw new SdlException() : returnValue;

        /// <summary>
        /// Check that the return of a method is not an error (i.e. less than zero) and converts
        /// return value to a Boolean.
        /// </summary>
        /// <param name="returnValue">The return value of the API.</param>
        /// <returns>The return value as a Boolean.</returns>
        /// <exception cref="SdlException">Thrown if method returned an error.</exception>
        public static bool CheckErrorBool(int returnValue) => (returnValue < 0) ? throw new SdlException() : (returnValue == 1);

        /// <summary>
        /// Check that the return of a method is not an error (i.e. zero).
        /// </summary>
        /// <param name="returnValue">The return value of the API.</param>
        /// <returns>The return value.</returns>
        /// <exception cref="SdlException">Thrown if method returned an error.</exception>
        public static int CheckErrorZero(int returnValue) => (returnValue == 0) ? throw new SdlException() : returnValue;

        /// <summary>
        /// Check that the return of a method is not an error (i.e. zero).
        /// </summary>
        /// <param name="returnValue">The return value of the API.</param>
        /// <returns>The return value.</returns>
        /// <exception cref="SdlException">Thrown if method returned an error.</exception>
        public static uint CheckErrorZero(uint returnValue) => (returnValue == 0) ? throw new SdlException() : returnValue;

        /// <summary>
        /// Check that the return of a method is not an error (i.e. zero).
        /// </summary>
        /// <param name="returnValue">The return value of the API.</param>
        /// <returns>The return value.</returns>
        /// <exception cref="SdlException">Thrown if method returned an error.</exception>
        public static nuint CheckErrorZero(nuint returnValue) => (returnValue == 0) ? throw new SdlException() : returnValue;

        /// <summary>
        /// Check that the pointer returned from a method is not null.
        /// </summary>
        /// <param name="returnValue">The return value of the API.</param>
        /// <returns>The return value.</returns>
        /// <exception cref="SdlException">Thrown if method returned an error.</exception>
        public static T* CheckPointer<T>(T* returnValue) where T : unmanaged => (returnValue == null) ? throw new SdlException() : returnValue;

        /// <summary>
        /// Check that the pointer returned from a method is not null.
        /// </summary>
        /// <param name="returnValue">The return value of the API.</param>
        /// <returns>The return value.</returns>
        /// <exception cref="SdlException">Thrown if method returned an error.</exception>
        public static nint CheckPointer(nint returnValue) => (returnValue == 0) ? throw new SdlException() : returnValue;

        /// <summary>
        /// Checks that a class instance is not null.
        /// </summary>
        /// <typeparam name="T">The class type.</typeparam>
        /// <param name="value">The instance.</param>
        /// <returns>The instance if not <c>null</c>, throws an exception otherwise.</returns>
        public static T CheckNotNull<T>(T? value) where T : class => value ?? throw new SdlException();

        /// <summary>
        /// Converts a pixel pointer to a span.
        /// </summary>
        /// <typeparam name="T">The type of the pixel.</typeparam>
        /// <param name="pixels">A pointer to the pixels.</param>
        /// <param name="pitch">The pitch.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static Span<T> PixelsToSpan<T>(void* pixels, int pitch, int height)
        {
            var bytesPerPixel = typeof(T) == typeof(byte)
                ? 1
                : typeof(T) == typeof(ushort)
                    ? 2
                    : typeof(T) == typeof(uint)
                        ? 4
                        : throw new InvalidOperationException();
            return new Span<T>(pixels, pitch / bytesPerPixel * height);
        }

        //
        // SDL2
        //

        // begin_code.h - Only C/C++ defines, no translation needed

        // close_code.h - Only C/C++ defines, no translation needed

        #region SDL.h

        public const uint SDL_INIT_TIMER = 0x00000001u;
        public const uint SDL_INIT_AUDIO = 0x00000010u;
        public const uint SDL_INIT_VIDEO = 0x00000020u;
        public const uint SDL_INIT_JOYSTICK = 0x00000200u;
        public const uint SDL_INIT_HAPTIC = 0x00001000u;
        public const uint SDL_INIT_GAMECONTROLLER = 0x00002000u;
        public const uint SDL_INIT_EVENTS = 0x00004000u;
        public const uint SDL_INIT_SENSOR = 0x00008000u;
        public const uint SDL_INIT_EVERYTHING = SDL_INIT_TIMER
            | SDL_INIT_AUDIO
            | SDL_INIT_VIDEO
            | SDL_INIT_EVENTS
            | SDL_INIT_JOYSTICK
            | SDL_INIT_HAPTIC
            | SDL_INIT_GAMECONTROLLER
            | SDL_INIT_SENSOR;

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_Init(uint flags);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_InitSubSystem(uint flags);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_QuitSubSystem(uint flags);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_WasInit(Subsystems flags);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_Quit();

        #endregion

        // SDL_assert.h - Should use the platform facilities.

        // SDL_atomic.h - Should use the platform facilities.

        #region SDL_audio.h

        public readonly record struct SDL_AudioFormat(ushort Value);

        public const ushort SDL_AUDIO_MASK_BITSIZE = 0xFF;
        public const ushort SDL_AUDIO_MASK_DATATYPE = 1 << 8;
        public const ushort SDL_AUDIO_MASK_ENDIAN = 1 << 12;
        public const ushort SDL_AUDIO_MASK_SIGNED = 1 << 15;

        public static byte SDL_AUDIO_BITSIZE(SDL_AudioFormat x) => (byte)(x.Value & SDL_AUDIO_MASK_BITSIZE);
        public static bool SDL_AUDIO_ISFLOAT(SDL_AudioFormat x) => (x.Value & SDL_AUDIO_MASK_DATATYPE) != 0;
        public static bool SDL_AUDIO_ISBIGENDIAN(SDL_AudioFormat x) => (x.Value & SDL_AUDIO_MASK_ENDIAN) != 0;
        public static bool SDL_AUDIO_ISSIGNED(SDL_AudioFormat x) => (x.Value & SDL_AUDIO_MASK_SIGNED) != 0;
        public static bool SDL_AUDIO_ISINT(SDL_AudioFormat x) => !SDL_AUDIO_ISFLOAT(x);
        public static bool SDL_AUDIO_ISLITTLEENDIAN(SDL_AudioFormat x) => !SDL_AUDIO_ISBIGENDIAN(x);
        public static bool SDL_AUDIO_ISUNSIGNED(SDL_AudioFormat x) => !SDL_AUDIO_ISSIGNED(x);

        public static readonly SDL_AudioFormat AUDIO_U8 = new(0x0008);
        public static readonly SDL_AudioFormat AUDIO_S8 = new(0x8008);
        public static readonly SDL_AudioFormat AUDIO_U16LSB = new(0x0010);
        public static readonly SDL_AudioFormat AUDIO_S16LSB = new(0x8010);
        public static readonly SDL_AudioFormat AUDIO_U16MSB = new(0x1010);
        public static readonly SDL_AudioFormat AUDIO_S16MSB = new(0x9010);
        public static readonly SDL_AudioFormat AUDIO_U16 = AUDIO_U16LSB;
        public static readonly SDL_AudioFormat AUDIO_S16 = AUDIO_S16LSB;

        public static readonly SDL_AudioFormat AUDIO_S32LSB = new(0x8020);
        public static readonly SDL_AudioFormat AUDIO_S32MSB = new(0x9020);
        public static readonly SDL_AudioFormat AUDIO_S32 = AUDIO_S32LSB;

        public static readonly SDL_AudioFormat AUDIO_F32LSB = new(0x8120);
        public static readonly SDL_AudioFormat AUDIO_F32MSB = new(0x9120);
        public static readonly SDL_AudioFormat AUDIO_F32 = AUDIO_F32LSB;

        public static readonly SDL_AudioFormat AUDIO_U16SYS = BitConverter.IsLittleEndian ? AUDIO_U16LSB : AUDIO_U16MSB;
        public static readonly SDL_AudioFormat AUDIO_S16SYS = BitConverter.IsLittleEndian ? AUDIO_S16LSB : AUDIO_S16MSB;
        public static readonly SDL_AudioFormat AUDIO_S32SYS = BitConverter.IsLittleEndian ? AUDIO_S32LSB : AUDIO_S32MSB;
        public static readonly SDL_AudioFormat AUDIO_F32SYS = BitConverter.IsLittleEndian ? AUDIO_F32LSB : AUDIO_F32MSB;

        public const uint SDL_AUDIO_ALLOW_FREQUENCY_CHANGE = 0x00000001;
        public const uint SDL_AUDIO_ALLOW_FORMAT_CHANGE = 0x00000002;
        public const uint SDL_AUDIO_ALLOW_CHANNELS_CHANGE = 0x00000004;
        public const uint SDL_AUDIO_ALLOW_SAMPLES_CHANGE = 0x00000008;
        public const uint SDL_AUDIO_ALLOW_ANY_CHANGE = SDL_AUDIO_ALLOW_FREQUENCY_CHANGE
            | SDL_AUDIO_ALLOW_FORMAT_CHANGE
            | SDL_AUDIO_ALLOW_CHANNELS_CHANGE
            | SDL_AUDIO_ALLOW_SAMPLES_CHANGE;

        public delegate void SDL_AudioCallback(nint userdata, byte* stream, int len);

        public readonly struct SDL_AudioSpec
        {
            public readonly int freq { get; }

            public readonly SDL_AudioFormat format { get; }

            public readonly byte channels { get; }

            public readonly byte silence { get; }

            public readonly ushort samples { get; }

            private readonly ushort _padding;

            public readonly uint size { get; }

            public readonly SDL_AudioCallback? callback { get; }

            public readonly nint userdata { get; }

            public SDL_AudioSpec(int freq, SDL_AudioFormat format, byte channels, ushort samples, SDL_AudioCallback? callback = default, nint userdata = default)
            {
                this.freq = freq;
                this.format = format;
                this.channels = channels;
                silence = 0; // Always calculated
                this.samples = samples;
                _padding = 0;
                size = 0;  // Always calculated
                this.callback = callback;
                this.userdata = userdata;
            }
        }

        // SDL_AudioFilter is internal only

        public struct SDL_AudioCVT
        {
            public readonly int needed { get; }

            public readonly SDL_AudioFormat src_format { get; }

            public readonly SDL_AudioFormat dst_format { get; }

            public readonly double rate_incr { get; }

            public byte* buf { get; set; }

            public int len { get; set; }

            public readonly int len_cvt { get; }

            public readonly int len_mult { get; }

            public readonly double len_ratio { get; }

            // Work around the fact that can't make buffers of nint
            private readonly nint _filter0;
            private readonly nint _filter1;
            private readonly nint _filter2;
            private readonly nint _filter3;
            private readonly nint _filter4;
            private readonly nint _filter5;
            private readonly nint _filter6;
            private readonly nint _filter7;
            private readonly nint _filter8;
            private readonly nint _filter9;
            private readonly int _filterIndex;
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumAudioDrivers();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte *SDL_GetAudioDriver(int index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int SDL_AudioInit(string driver_name);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_AudioQuit();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string SDL_GetCurrentAudioDriver();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_OpenAudio(in SDL_AudioSpec desired, out SDL_AudioSpec obtained);

        public readonly record struct SDL_AudioDeviceID(uint Id);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumAudioDevices(bool iscapture);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern Utf8String SDL_GetAudioDeviceName(int index, bool iscapture);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_OpenAudioDevice(Utf8String device, bool iscapture, in SDL_AudioSpec desired, out SDL_AudioSpec obtained, AudioAllowChange allowed_changes);

        // SDL_AudioStatus is covered by AudioStatus.cs

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern AudioStatus SDL_GetAudioStatus();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern AudioStatus SDL_GetAudioDeviceStatus(SDL_AudioDeviceID dev);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_PauseAudio(bool pause_on);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_PauseAudioDevice(SDL_AudioDeviceID dev, bool pause_on);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nint SDL_LoadWAV_RW(SDL_RWops* src, bool freesrc, out SDL_AudioSpec spec, out byte* audio_buf, out uint audio_len);

        public static nint SDL_LoadWAV(string file, out SDL_AudioSpec spec, out byte* audio_buf, out uint audio_len) =>
                SDL_LoadWAV_RW(SDL_RWFromFile(file, "rb"), true, out spec, out audio_buf, out audio_len);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FreeWAV(byte* audio_buf);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_BuildAudioCVT(out SDL_AudioCVT cvt, AudioFormat src_format, byte src_channels, int src_rate, AudioFormat dst_format, byte dst_channels, int dst_rate);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_ConvertAudio(ref SDL_AudioCVT cvt);

#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
        // This is the native name.
        public readonly struct SDL_AudioStream
        {
        }
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_AudioStream* SDL_NewAudioStream(AudioFormat src_format, byte src_channels, int src_rate, AudioFormat dst_format, byte dst_channels, int dst_rate);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_AudioStreamPut(SDL_AudioStream* stream, byte* buf, int len);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_AudioStreamGet(SDL_AudioStream* stream, byte* buf, int len);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_AudioStreamAvailable(SDL_AudioStream* stream);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_AudioStreamFlush(SDL_AudioStream* stream);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_AudioStreamClear(SDL_AudioStream* stream);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FreeAudioStream(SDL_AudioStream* stream);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_MixAudio(byte* dst, byte* src, uint len, int volume);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_MixAudioFormat(byte* dst, byte* src, AudioFormat format, uint len, int volume);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_QueueAudio(SDL_AudioDeviceID dev, byte* data, uint len);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_DequeueAudio(SDL_AudioDeviceID dev, byte* data, uint len);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_GetQueuedAudioSize(SDL_AudioDeviceID dev);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_ClearQueuedAudio(SDL_AudioDeviceID dev);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LockAudio();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LockAudioDevice(SDL_AudioDeviceID dev);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_UnlockAudio();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_UnlockAudioDevice(SDL_AudioDeviceID dev);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_CloseAudio();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_CloseAudioDevice(SDL_AudioDeviceID dev);

        #endregion

        // SDL_bits.h - Should use platform facilities.

        #region SDL_blendmode.h

        // SDL_BlendMode is covered by BlendMode.cs
        // SDL_BlendOperation is covered by BlendOperation.cs
        // SDL_BlendFactor is covered by BlendFactor.cs

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern BlendMode SDL_ComposeCustomBlendMode(BlendFactor srcColorFactor, BlendFactor dstColorFactor, BlendOperation colorOperation, BlendFactor srcAlphaFactor, BlendFactor dstAlphaFactor, BlendOperation alphaOperation);

        #endregion

        #region SDL_clipboard.h

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetClipboardText(Utf8String text);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern Utf8String SDL_GetClipboardText();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasClipboardText();

        #endregion

        // SDL_config.h - Just defines

        // SDL_copying.h - Just text

        #region SDL_cpuinfo.h

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetCPUCount();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetCPUCacheLineSize();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasRDTSC();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasAltiVec();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasMMX();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_Has3DNow();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasSSE();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasSSE2();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasSSE3();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasSSE41();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasSSE42();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasAVX();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasAVX2();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasAVX512F();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasARMSIMD();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasNEON();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetSystemRAM();

        // SIMD should be used through .NET support, no direct support otherwise
        //public static extern nuint SDL_SIMDGetAlignment();
        //public static extern nint SDL_SIMDAlloc(nuint len);
        //public static extern void SDL_SIMDFree(nint ptr);

        #endregion

        // SDL_egl.h - Not supporting OpenGL and related APIs at this time

        // SDL_endian.h - Should use platform facilities.

        #region SDL_error.h

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int SDL_SetError(string message /* ... */);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern Utf8String SDL_GetError();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_ClearError();

        #endregion

        #region SDL_events.h

        public enum SDL_EventType
        {
            FirstEvent = 0,

            Quit = 0x100,

            ApplicationTerminating,
            ApplicationLowMemory,
            ApplicationWillEnterBackground,
            ApplicationDidEnterBackground,
            ApplicationWillEnterForeground,
            ApplicationDidEnterForeground,

            Display = 0x150,

            Window = 0x200,
            SystemWindowMessageEvent,

            KeyDown = 0x300,
            KeyUp,
            TextEditing,
            TextInput,
            KeymapChanged,

            MouseMotion = 0x400,
            MouseButtonDown,
            MouseButtonUp,
            MouseWheel,

            JoystickAxisMotion = 0x600,
            JoystickBallMotion,
            JoystickHatMotion,
            JoystickButtonDown,
            JoystickButtonUp,
            JoystickDeviceAdded,
            JoystickDeviceRemoved,

            ControllerAxisMotion = 0x650,
            ControllerButtonDown,
            ControllerButtonUp,
            ControllerDeviceAdded,
            ControllerDeviceRemoved,
            ControllerDeviceRemapped,

            FingerDown = 0x700,
            FingerUp,
            FingerMotion,

            DollarGesture = 0x800,
            DollarRecord,
            MultiGesture,

            ClipboardUpdate = 0x900,

            DropFile = 0x1000,
            DropText,
            DropBegin,
            DropComplete,

            AudioDeviceAdded = 0x1100,
            AudioDeviceRemoved,

            SensorUpdate = 0x1200,

            RenderTargetsReset = 0x2000,
            RenderDeviceReset,

            UserEvent = 0x8000,

            LastEvent = 0xFFFF
        }

        public readonly struct SDL_CommonEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
        }

        public readonly struct SDL_DisplayEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly uint DisplayIndex { get; }
            public readonly SDL_DisplayEventID DisplayEventId { get; }

            private readonly byte _padding1;
            private readonly byte _padding2;
            private readonly byte _padding3;

            public readonly int Data { get; }
        }

        public readonly struct SDL_WindowEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly uint WindowId { get; }
            public readonly SDL_WindowEventID WindowEventId { get; }

            private readonly byte _padding1;
            private readonly byte _padding2;
            private readonly byte _padding3;

            public readonly int Data1 { get; }
            public readonly int Data2 { get; }
        }

        public readonly struct SDL_KeyboardEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }

            public readonly uint WindowId { get; }
            public readonly bool State { get; }
            public readonly byte Repeat { get; }

            private readonly byte _padding2;
            private readonly byte _padding3;

            public readonly SDL_Keysym Keysym { get; }
        }

        public struct SDL_TextEditingEvent
        {
            public const int TextEditingEventTextSize = 32;

            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly uint WindowId { get; }

            private fixed byte _text[TextEditingEventTextSize];

            public readonly string Text
            {
                get
                {
                    fixed (byte* textBuffer = _text)
                    {
                        return new Utf8String(textBuffer).ToString()!;
                    }
                }
            }

            public readonly int Start { get; }
            public readonly int Length { get; }
        }

        public struct SDL_TextInputEvent
        {
            public const int TextInputEventTextSize = 32;

            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly uint WindowId { get; }

            private fixed byte _text[TextInputEventTextSize];

            public readonly string Text
            {
                get
                {
                    fixed (byte* textBuffer = _text)
                    {
                        return new Utf8String(textBuffer).ToString()!;
                    }
                }
            }
        }

        public readonly struct SDL_MouseMotionEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly uint WindowId { get; }
            public readonly uint Which { get; }
            public readonly uint State { get; }
            public readonly int X { get; }
            public readonly int Y { get; }
            public readonly int XRel { get; }
            public readonly int YRel { get; }
        }

        public readonly struct SDL_MouseButtonEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly uint WindowId { get; }
            public readonly uint Which { get; }
            public readonly byte Button { get; }
            public readonly bool State { get; }
            public readonly byte Clicks { get; }

            private readonly byte _padding1;

            public readonly int X { get; }
            public readonly int Y { get; }
        }

        public readonly struct SDL_MouseWheelEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly uint WindowId { get; }
            public readonly uint Which { get; }
            public readonly int X { get; }
            public readonly int Y { get; }
            public readonly MouseWheelDirection Direction { get; }
        }

        public readonly struct SDL_JoyAxisEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly SDL_JoystickID Which { get; }
            public readonly byte Axis { get; }

            private readonly byte _padding1;
            private readonly byte _padding2;
            private readonly byte _padding3;

            public readonly short Value { get; }

            private readonly ushort _padding4;
        }

        public readonly struct SDL_JoyBallEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly SDL_JoystickID Which { get; }
            public readonly byte Ball { get; }

            private readonly byte _padding1;
            private readonly byte _padding2;
            private readonly byte _padding3;

            public readonly short Xrel { get; }
            public readonly short Yrel { get; }
        }

        public readonly struct SDL_JoyHatEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly SDL_JoystickID Which { get; }
            public readonly byte Hat { get; }
            public readonly HatState Value { get; }

            private readonly byte _padding1;
            private readonly byte _padding2;
        }

        public readonly struct SDL_JoyButtonEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly SDL_JoystickID Which { get; }
            public readonly byte Button { get; }
            public readonly bool State { get; }

            private readonly byte _padding1;
            private readonly byte _padding2;
        }

        public readonly struct SDL_JoyDeviceEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly int Which { get; }
        }

        public readonly struct SDL_ControllerAxisEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly SDL_JoystickID Which { get; }
            public readonly byte Axis { get; }

            private readonly byte _padding1;
            private readonly byte _padding2;
            private readonly byte _padding3;

            public readonly short Value { get; }

            private readonly ushort _padding4;
        }

        public readonly struct SDL_ControllerButtonEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly SDL_JoystickID Which { get; }
            public readonly byte Button { get; }
            public readonly bool State { get; }

            private readonly byte _padding1;
            private readonly byte _padding2;
        }

        public readonly struct SDL_ControllerDeviceEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly int Which { get; }
        }

        public readonly struct SDL_AudioDeviceEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly uint Which { get; }

            [field: MarshalAs(UnmanagedType.I1)]
            public readonly bool IsCapture { get; }

            private readonly byte _padding1;
            private readonly byte _padding2;
            private readonly byte _padding3;
        }

        public readonly struct SDL_TouchFingerEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly SDL_TouchID TouchId { get; }
            public readonly SDL_FingerID FingerId { get; }
            public readonly float X { get; }
            public readonly float Y { get; }
            public readonly float Dx { get; }
            public readonly float Dy { get; }
            public readonly float Pressure { get; }
            public readonly uint WindowId { get; }
        }

        public readonly struct SDL_MultiGestureEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly SDL_TouchID TouchId { get; }
            public readonly float DTheta { get; }
            public readonly float DDist { get; }
            public readonly float X { get; }
            public readonly float Y { get; }
            public readonly ushort NumFingers { get; }

            private readonly ushort _padding;
        }

        public readonly struct SDL_DollarGestureEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly SDL_TouchID TouchId { get; }
            public readonly SDL_GestureID GestureId { get; }
            public readonly uint NumFingers { get; }
            public readonly float Error { get; }
            public readonly float X { get; }
            public readonly float Y { get; }
        }

        public readonly struct SDL_DropEvent
        {
            public readonly SDL_EventType Type { get; }
            public readonly uint Timestamp { get; }
            public readonly DisposableAnsiString File { get; }
            public readonly uint WindowId { get; }
        }

        public struct SDL_SensorEvent
        {
            public readonly SDL_EventType Type;
            public readonly uint Timestamp;
            public readonly SDL_SensorID Which;

            private fixed float _data[6];

            public readonly float[] Data
            {
                get
                {
                    var data = new float[6];

                    for (var i = 0; i < 6; i++)
                    {
                        data[i] = _data[i];
                    }

                    return data;
                }
            }
        }

        public readonly struct SDL_QuitEvent
        {
            public readonly SDL_EventType Type;
            public readonly uint Timestamp;
        }

        // SDL_OSEvent isn't used

        public readonly struct SDL_UserEvent
        {
            public readonly SDL_EventType Type;
            public readonly uint Timestamp;
            public readonly uint WindowId;
            public readonly int Code;
            public readonly nint Data1;
            public readonly nint Data2;
        }

        public readonly struct SDL_SysWMEvent
        {
            public readonly SDL_EventType Type;
            public readonly uint Timestamp;
            public readonly SDL_SysWMmsg* Msg;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct SDL_Event
        {
            [field: FieldOffset(0)]
            public readonly SDL_EventType Type { get; }

            [field: FieldOffset(0)]
            public readonly SDL_CommonEvent Common { get; }

            [field: FieldOffset(0)]
            public readonly SDL_DisplayEvent Display { get; }

            [field: FieldOffset(0)]
            public readonly SDL_WindowEvent Window { get; }

            [field: FieldOffset(0)]
            public readonly SDL_KeyboardEvent Key { get; }

            [field: FieldOffset(0)]
            public readonly SDL_TextEditingEvent Edit { get; }

            [field: FieldOffset(0)]
            public readonly SDL_TextInputEvent Text { get; }

            [field: FieldOffset(0)]
            public readonly SDL_MouseMotionEvent Motion { get; }

            [field: FieldOffset(0)]
            public readonly SDL_MouseButtonEvent Button { get; }

            [field: FieldOffset(0)]
            public readonly SDL_MouseWheelEvent Wheel { get; }

            [field: FieldOffset(0)]
            public readonly SDL_JoyAxisEvent Jaxis { get; }

            [field: FieldOffset(0)]
            public readonly SDL_JoyBallEvent Jball { get; }

            [field: FieldOffset(0)]
            public readonly SDL_JoyHatEvent Jhat { get; }

            [field: FieldOffset(0)]
            public readonly SDL_JoyButtonEvent Jbutton { get; }

            [field: FieldOffset(0)]
            public readonly SDL_JoyDeviceEvent Jdevice { get; }

            [field: FieldOffset(0)]
            public readonly SDL_ControllerAxisEvent Caxis { get; }

            [field: FieldOffset(0)]
            public readonly SDL_ControllerButtonEvent Cbutton { get; }

            [field: FieldOffset(0)]
            public readonly SDL_ControllerDeviceEvent Cdevice { get; }

            [field: FieldOffset(0)]
            public readonly SDL_AudioDeviceEvent Adevice { get; }

            [field: FieldOffset(0)]
            public readonly SDL_SensorEvent Sensor { get; }

            [field: FieldOffset(0)]
            public readonly SDL_QuitEvent Quit { get; }

            [field: FieldOffset(0)]
            public readonly SDL_UserEvent User { get; }

            [field: FieldOffset(0)]
            public readonly SDL_SysWMEvent Syswm { get; }

            [field: FieldOffset(0)]
            public readonly SDL_TouchFingerEvent Tfinger { get; }

            [field: FieldOffset(0)]
            public readonly SDL_MultiGestureEvent Mgesture { get; }

            [field: FieldOffset(0)]
            public readonly SDL_DollarGestureEvent Dgesture { get; }

            [field: FieldOffset(0)]
            public readonly SDL_DropEvent Drop { get; }

            [FieldOffset(0)]
            private fixed byte _padding[56];
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_PumpEvents();

        public enum SDL_EventAction
        {
            Add,
            Peek,
            Get
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_PeepEvents([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] SDL_Event[] events, int numevents, SDL_EventAction action, SDL_EventType minType = SDL_EventType.FirstEvent, SDL_EventType maxType = SDL_EventType.LastEvent);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasEvent(SDL_EventType type);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasEvents(SDL_EventType minType, SDL_EventType maxType);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FlushEvent(SDL_EventType type);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FlushEvents(SDL_EventType minType, SDL_EventType maxType);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_PollEvent(out SDL_Event e);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_WaitEvent(out SDL_Event e);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_WaitEventTimeout(out SDL_Event e, int timeout);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_PushEvent(ref SDL_Event e);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool SDL_EventFilter(nint userdata, ref SDL_Event e);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetEventFilter(SDL_EventFilter filter, nint userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GetEventFilter(out SDL_EventFilter filter, out nint userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_AddEventWatch(SDL_EventFilter filter, nint userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_DelEventWatch(SDL_EventFilter filter, nint userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FilterEvents(SDL_EventFilter filter, nint userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte SDL_EventState(SDL_EventType type, State state);

        public static State SDL_GetEventState(SDL_EventType type) =>
            (State)SDL_EventState(type, State.Query);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_RegisterEvents(int numevents);

        #endregion

        #region SDL_filesystem.h

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern Utf8String SDL_GetBasePath();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern Utf8String SDL_GetPrefPath(Utf8String org, Utf8String app);

        #endregion

        #region SDL_gamecontroller.h

        public readonly struct SDL_GameController
        {
        }

        public enum SDL_ControllerBindType
        {
            None,
            Button,
            Axis,
            Hat
        }

        public readonly struct SDL_GameControllerButtonBind
        {
            public readonly SDL_ControllerBindType Type { get; }
            public readonly GameControllerButtonBindUnion Value { get; }

            [StructLayout(LayoutKind.Explicit)]
            public readonly struct GameControllerButtonBindUnion
            {
                [field: FieldOffset(0)]
                public readonly int Button { get; }

                [field: FieldOffset(0)]
                public readonly int Axis { get; }

                [field: FieldOffset(0)]
                public readonly HatValue Hat { get; }

                public readonly struct HatValue
                {
                    public readonly int Hat { get; }
                    public readonly int HatMask { get; }
                }
            }
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerAddMappingsFromRW(SDL_RWops* rw, bool freerw);

        public static int SDL_GameControllerAddMappingsFromFile(string file) =>
            SDL_GameControllerAddMappingsFromRW(SDL_RWFromFile(file, "rb"), true);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int SDL_GameControllerAddMapping(string mappingString);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerNumMappings();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern DisposableAnsiString SDL_GameControllerMappingForIndex(int mapping_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern DisposableAnsiString SDL_GameControllerMappingForGUID(Guid id);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern DisposableAnsiString SDL_GameControllerMapping(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_IsGameController(int joystick_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string SDL_GameControllerNameForIndex(int joystick_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern GameControllerType SDL_GameControllerTypeForIndex(int joystick_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern DisposableAnsiString SDL_GameControllerMappingForDeviceIndex(int joystick_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_GameController* SDL_GameControllerOpen(int joystick_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_GameController* SDL_GameControllerFromInstanceID(SDL_JoystickID joyid);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_GameController* SDL_GameControllerFromPlayerIndex(int player_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string? SDL_GameControllerName(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern GameControllerType SDL_GameControllerGetType(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerGetPlayerIndex(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GameControllerSetPlayerIndex(SDL_GameController* gamecontroller, int player_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_GameControllerGetVendor(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_GameControllerGetProduct(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_GameControllerGetProductVersion(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GameControllerGetAttached(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Joystick* SDL_GameControllerGetJoystick(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerEventState(State state);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GameControllerUpdate();

        // SDL_GameControllerAxis is covered by ControllerAxis.cs

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern GameControllerAxis SDL_GameControllerGetAxisFromString(string pchString);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string SDL_GameControllerGetStringForAxis(GameControllerAxis axis);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_GameControllerButtonBind SDL_GameControllerGetBindForAxis(SDL_GameController* gamecontroller, GameControllerAxis axis);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern short SDL_GameControllerGetAxis(SDL_GameController* gamecontroller, GameControllerAxis axis);

        // SDL_GameControllerButton is covered by ControllerButton.cs

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern GameControllerButton SDL_GameControllerGetButtonFromString(string pchString);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string SDL_GameControllerGetStringForButton(GameControllerButton button);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_GameControllerButtonBind SDL_GameControllerGetBindForButton(SDL_GameController* gamecontroller, GameControllerButton button);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool SDL_GameControllerGetButton(SDL_GameController* gamecontroller, GameControllerButton button);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerRumble(SDL_GameController* gamecontroller, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GameControllerClose(SDL_GameController* gamecontroller);

        #endregion

        #region SDL_gesture.h

        public readonly struct SDL_GestureID
        {
            public long Id { get; }
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_RecordGesture(SDL_TouchID touchId);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SaveAllDollarTemplates(SDL_RWops* dst);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SaveDollarTemplate(SDL_GestureID gestureId, SDL_RWops* dst);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_LoadDollarTemplates(SDL_TouchID touchId, SDL_RWops* src);

        #endregion

        #region SDL_haptic.h

        public readonly struct SDL_Haptic
        {
        }

        [Flags]
        public enum SDL_HapticType : uint
        {
            None = 0,
            Constant = 1u << 0,
            Sine = 1u << 1,
            LeftRight = 1u << 2,
            Triangle = 1u << 3,
            SawToothUp = 1u << 4,
            SawToothDown = 1u << 5,
            Ramp = 1u << 6,
            Spring = 1u << 7,
            Damper = 1u << 8,
            Inertia = 1u << 9,
            Friction = 1u << 10,
            Custom = 1u << 11,
            Gain = 1u << 12,
            Autocenter = 1u << 13,
            Status = 1u << 14,
            Pause = 1u << 15
        }

        public enum SDL_HapticDirectionType : byte
        {
            Polar,
            Cartesian,
            Spherical
        }

        public struct SDL_HapticDirection
        {
            private readonly SDL_HapticDirectionType _type;

            private fixed short _dir[3];

            public SDL_HapticDirection(SDL_HapticDirectionType type, short dir1)
            {
                _type = type;
                _dir[0] = dir1;
            }

            public SDL_HapticDirection(SDL_HapticDirectionType type, short dir1, short dir2)
            {
                _type = type;
                _dir[0] = dir1;
                _dir[1] = dir2;
            }

            public SDL_HapticDirection(SDL_HapticDirectionType type, short dir1, short dir2, short dir3)
            {
                _type = type;
                _dir[0] = dir1;
                _dir[1] = dir2;
                _dir[2] = dir3;
            }
        }

        public readonly struct SDL_HapticConstant
        {
            private readonly SDL_HapticType _type;
            private readonly SDL_HapticDirection _direction;

            private readonly uint _length;
            private readonly ushort _delay;

            private readonly ushort _button;
            private readonly ushort _interval;

            private readonly short _level;

            private readonly ushort _attackLength;
            private readonly ushort _attackLevel;
            private readonly ushort _fadeLength;
            private readonly ushort _fadeLevel;

            public SDL_HapticConstant(SDL_HapticType type, SDL_HapticDirection direction, uint length, ushort delay, ushort button, ushort interval, short level, ushort attackLength, ushort attackLevel, ushort fadeLength, ushort fadeLevel)
            {
                _type = type;
                _direction = direction;
                _length = length;
                _delay = delay;
                _button = button;
                _interval = interval;
                _level = level;
                _attackLength = attackLength;
                _attackLevel = attackLevel;
                _fadeLength = fadeLength;
                _fadeLevel = fadeLevel;
            }
        }

        public readonly struct SDL_HapticPeriodic
        {
            private readonly SDL_HapticType _type;
            private readonly SDL_HapticDirection _direction;

            private readonly uint _length;
            private readonly ushort _delay;

            private readonly ushort _button;
            private readonly ushort _interval;

            private readonly ushort _period;
            private readonly short _magnitude;
            private readonly short _offset;
            private readonly ushort _phase;

            private readonly ushort _attackLength;
            private readonly ushort _attackLevel;
            private readonly ushort _fadeLength;
            private readonly ushort _fadeLevel;

            public SDL_HapticPeriodic(SDL_HapticType type, SDL_HapticDirection direction, uint length, ushort delay, ushort button, ushort interval, ushort period, short magnitude, short offset, ushort phase, ushort attackLength, ushort attackLevel, ushort fadeLength, ushort fadeLevel)
            {
                _type = type;
                _direction = direction;
                _length = length;
                _delay = delay;
                _button = button;
                _interval = interval;
                _period = period;
                _magnitude = magnitude;
                _offset = offset;
                _phase = phase;
                _attackLength = attackLength;
                _attackLevel = attackLevel;
                _fadeLength = fadeLength;
                _fadeLevel = fadeLevel;
            }
        }

        public struct SDL_HapticCondition
        {
            private readonly SDL_HapticType _type;
            private readonly SDL_HapticDirection _direction;

            private readonly uint _length;
            private readonly ushort _delay;

            private readonly ushort _button;
            private readonly ushort _interval;

            private fixed ushort _rightSat[3];
            private fixed ushort _leftSat[3];
            private fixed short _rightCoeff[3];
            private fixed short _leftCoeff[3];
            private fixed ushort _deadband[3];
            private fixed short _center[3];

            public SDL_HapticCondition(SDL_HapticType type, SDL_HapticDirection direction, uint length, ushort delay, ushort button, ushort interval, (ushort XAxis, ushort YAxis, ushort ZAxis) rightSat, (ushort XAxis, ushort YAxis, ushort ZAxis) leftSat, (short XAxis, short YAxis, short ZAxis) rightCoeff, (short XAxis, short YAxis, short ZAxis) leftCoeff, (ushort XAxis, ushort YAxis, ushort ZAxis) deadband, (short XAxis, short YAxis, short ZAxis) center)
            {
                _type = type;
                _direction = direction;
                _length = length;
                _delay = delay;
                _button = button;
                _interval = interval;
                _rightSat[0] = rightSat.XAxis;
                _rightSat[1] = rightSat.YAxis;
                _rightSat[2] = rightSat.ZAxis;
                _leftSat[0] = leftSat.XAxis;
                _leftSat[1] = leftSat.YAxis;
                _leftSat[2] = leftSat.ZAxis;
                _rightCoeff[0] = rightCoeff.XAxis;
                _rightCoeff[1] = rightCoeff.YAxis;
                _rightCoeff[2] = rightCoeff.ZAxis;
                _leftCoeff[0] = leftCoeff.XAxis;
                _leftCoeff[1] = leftCoeff.YAxis;
                _leftCoeff[2] = leftCoeff.ZAxis;
                _deadband[0] = deadband.XAxis;
                _deadband[1] = deadband.YAxis;
                _deadband[2] = deadband.ZAxis;
                _center[0] = center.XAxis;
                _center[1] = center.YAxis;
                _center[2] = center.ZAxis;
            }
        }

        public readonly struct SDL_HapticRamp
        {
            private readonly SDL_HapticType _type;
            private readonly SDL_HapticDirection _direction;

            private readonly uint _length;
            private readonly ushort _delay;

            private readonly ushort _button;
            private readonly ushort _interval;

            private readonly short _start;
            private readonly short _end;

            private readonly ushort _attackLength;
            private readonly ushort _attackLevel;
            private readonly ushort _fadeLength;
            private readonly ushort _fadeLevel;

            public SDL_HapticRamp(SDL_HapticType type, SDL_HapticDirection direction, uint length, ushort delay, ushort button, ushort interval, short start, short end, ushort attackLength, ushort attackLevel, ushort fadeLength, ushort fadeLevel)
            {
                _type = type;
                _direction = direction;
                _length = length;
                _delay = delay;
                _button = button;
                _interval = interval;
                _start = start;
                _end = end;
                _attackLength = attackLength;
                _attackLevel = attackLevel;
                _fadeLength = fadeLength;
                _fadeLevel = fadeLevel;
            }
        }

        public readonly struct SDL_HapticLeftRight
        {
            private readonly SDL_HapticType _type;

            private readonly uint _length;

            private readonly ushort _largeMagnitude;
            private readonly ushort _smallMagnitude;

            public SDL_HapticLeftRight(SDL_HapticType type, uint length, ushort largeMagnitude, ushort smallMagnitude)
            {
                _type = type;
                _length = length;
                _largeMagnitude = largeMagnitude;
                _smallMagnitude = smallMagnitude;
            }
        }

        public readonly struct SDL_HapticCustom
        {
            private readonly SDL_HapticType _type;
            private readonly SDL_HapticDirection _direction;

            private readonly uint _length;
            private readonly ushort _delay;

            private readonly ushort _button;
            private readonly ushort _interval;

            private readonly byte _channels;
            private readonly ushort _period;
            private readonly ushort _samples;

            [MarshalAs(UnmanagedType.LPArray)]
            private readonly ushort[] _data;

            private readonly ushort _attackLength;
            private readonly ushort _attackLevel;
            private readonly ushort _fadeLength;
            private readonly ushort _fadeLevel;

            public SDL_HapticCustom(SDL_HapticType type, SDL_HapticDirection direction, uint length, ushort delay, ushort button, ushort interval, byte channels, ushort period, ushort samples, ushort[] data, ushort attackLength, ushort attackLevel, ushort fadeLength, ushort fadeLevel)
            {
                _type = type;
                _direction = direction;
                _length = length;
                _delay = delay;
                _button = button;
                _interval = interval;
                _channels = channels;
                _period = period;
                _samples = samples;
                _data = data;
                _attackLength = attackLength;
                _attackLevel = attackLevel;
                _fadeLength = fadeLength;
                _fadeLevel = fadeLevel;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct SDL_HapticEffect
        {
            [FieldOffset(0)]
            public SDL_HapticType _type;

            [FieldOffset(0)]
            public SDL_HapticConstant _constant;

            [FieldOffset(0)]
            public SDL_HapticPeriodic _periodic;

            [FieldOffset(0)]
            public SDL_HapticCondition _condition;

            [FieldOffset(0)]
            public SDL_HapticRamp _ramp;

            [FieldOffset(0)]
            public SDL_HapticLeftRight _leftright;

            [FieldOffset(0)]
            public SDL_HapticCustom _custom;
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_NumHaptics();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string SDL_HapticName(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Haptic* SDL_HapticOpen(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HapticOpened(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticIndex(SDL_Haptic* haptic);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_MouseIsHaptic();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Haptic* SDL_HapticOpenFromMouse();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_JoystickIsHaptic(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Haptic* SDL_HapticOpenFromJoystick(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_HapticClose(SDL_Haptic* haptic);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticNumEffects(SDL_Haptic* haptic);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticNumEffectsPlaying(SDL_Haptic* haptic);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_HapticType SDL_HapticQuery(SDL_Haptic* haptic);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticNumAxes(SDL_Haptic* haptic);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HapticEffectSupported(SDL_Haptic* haptic, in SDL_HapticEffect effect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticNewEffect(SDL_Haptic* haptic, in SDL_HapticEffect effect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticUpdateEffect(SDL_Haptic* haptic, int effect, in SDL_HapticEffect data);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticRunEffect(SDL_Haptic* haptic, int effect, uint iterations);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticStopEffect(SDL_Haptic* haptic, int effect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_HapticDestroyEffect(SDL_Haptic* haptic, int effect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticGetEffectStatus(SDL_Haptic* haptic, int effect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticSetGain(SDL_Haptic* haptic, int gain);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticSetAutocenter(SDL_Haptic* haptic, int autocenter);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticPause(SDL_Haptic* haptic);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticUnpause(SDL_Haptic* haptic);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticStopAll(SDL_Haptic* haptic);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticRumbleSupported(SDL_Haptic* haptic);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticRumbleInit(SDL_Haptic* haptic);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticRumblePlay(SDL_Haptic* haptic, float strength, uint length);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticRumbleStop(SDL_Haptic* haptic);

        #endregion

        #region SDL_hints.h

        // SDL_HINT_* is covered by Hints.cs
        // SDL_HintPriority is covered by HintPriority.cs

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern bool SDL_SetHintWithPriority(string name, string value, HintPriority priority);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern bool SDL_SetHint(string name, string value);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string SDL_GetHint(string name);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern bool SDL_GetHintBoolean(string name, bool default_value);

        // SDL_HintCallback is covered by HintCallback.cs

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_AddHintCallback(string name, HintCallback callback, nint userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_DelHintCallback(string name, HintCallback callback, nint userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_ClearHints();

        #endregion

        #region SDL_joystick.h

        public readonly struct SDL_Joystick
        {
        }

        // SDL_JoystickGUID is just System.Guid

        public readonly struct SDL_JoystickID
        {
            public readonly int Id { get; }

            public SDL_JoystickID(int id)
            {
                Id = id;
            }
        }

        // SDL_JoystickType is covered by JoystickType.cs
        // SDL_JoystickPowerLevel is covered by JoystickPowerLevel.cs

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LockJoysticks();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_UnlockJoysticks();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_NumJoysticks();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string? SDL_JoystickNameForIndex(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickGetDevicePlayerIndex(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern Guid SDL_JoystickGetDeviceGUID(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_JoystickGetDeviceVendor(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_JoystickGetDeviceProduct(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_JoystickGetDeviceProductVersion(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern JoystickType SDL_JoystickGetDeviceType(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_JoystickID SDL_JoystickGetDeviceInstanceID(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Joystick* SDL_JoystickOpen(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Joystick* SDL_JoystickFromInstanceID(SDL_JoystickID instance_id);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Joystick* SDL_JoystickFromPlayerIndex(int player_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string? SDL_JoystickName(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickGetPlayerIndex(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_JoystickSetPlayerIndex(SDL_Joystick* joystick, int player_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern Guid SDL_JoystickGetGUID(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_JoystickGetVendor(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_JoystickGetProduct(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_JoystickGetProductVersion(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern JoystickType SDL_JoystickGetType(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_JoystickGetGUIDString(Guid id, string pszGUID, int cbGUID);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern Guid SDL_JoystickGetGUIDFromString(string pchGUID);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_JoystickGetAttached(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_JoystickID SDL_JoystickInstanceID(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickNumAxes(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickNumBalls(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickNumHats(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickNumButtons(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_JoystickUpdate();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickEventState(State state);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern short SDL_JoystickGetAxis(SDL_Joystick* joystick, int axis);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_JoystickGetAxisInitialState(SDL_Joystick* joystick, int axis, out short state);

        // SDL_HAT_* is covered by HatFlags.cs

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern HatState SDL_JoystickGetHat(SDL_Joystick* joystick, int hat);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickGetBall(SDL_Joystick* joystick, int ball, out int dx, out int dy);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool SDL_JoystickGetButton(SDL_Joystick* joystick, int button);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickRumble(SDL_Joystick* joystick, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_JoystickClose(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern JoystickPowerLevel SDL_JoystickCurrentPowerLevel(SDL_Joystick* joystick);

        #endregion

        #region SDL_keyboard.h

        public readonly struct SDL_Keysym
        {
            public readonly Scancode Scancode;
            public readonly Keycode Sym;
            public readonly KeyModifier Mod;
            private readonly uint _unused;
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Window* SDL_GetKeyboardFocus();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetKeyboardState(out int numkeys);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern KeyModifier SDL_GetModState();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetModState(KeyModifier modstate);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern Keycode SDL_GetKeyFromScancode(Scancode scancode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern Scancode SDL_GetScancodeFromKey(Keycode key);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string SDL_GetScancodeName(Scancode scancode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern Scancode SDL_GetScancodeFromName(string name);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string SDL_GetKeyName(Keycode key);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern Keycode SDL_GetKeyFromName(string name);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_StartTextInput();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_IsTextInputActive();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_StopTextInput();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetTextInputRect(ref Rectangle rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasScreenKeyboardSupport();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_IsScreenKeyboardShown(SDL_Window* window);

        #endregion

        #region SDK_keycode.h

        // SDL_Keycode is covered by Keycode.cs
        // SDLK_SCANCODE_MASK is covered by Scancode.cs

        // SDL_Keymod is covered by KeyModifier.cs

        #endregion

        // SDL_loadso.h -- Should use platform assembly loading methods

        #region SDL_log.h

        // SDL_LOG_CATEGORY_* is covered by LogCategory.cs
        // SDL_LogPriority is covered by LogPriority.cs

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LogSetAllPriority(LogPriority priority);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LogSetPriority(LogCategory category, LogPriority priority);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern LogPriority SDL_LogGetPriority(LogCategory category);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LogResetPriorities();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_Log(string fmt /*, ...*/);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_LogVerbose(LogCategory category, string fmt /*, ...*/);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_LogDebug(LogCategory category, string fmt /*, ...*/);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_LogInfo(LogCategory category, string fmt /*, ...*/);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_LogWarn(LogCategory category, string fmt /*, ...*/);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_LogError(LogCategory category, string fmt /*, ...*/);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_LogCritical(LogCategory category, string fmt /*, ...*/);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_LogMessage(LogCategory category, LogPriority priority, string fmt /*, ...*/);

        // public static extern void SDL_LogMessageV(LogCategory category, LogPriority priority, string fmt, va_list ap);

        // SDL_LogOutputFunction is covered by LogOutputFunction.cs

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LogGetOutputFunction(out LogOutputFunction callback, out nint userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LogSetOutputFunction(LogOutputFunction callback, nint userdata);

        #endregion

        // SDL_main.h -- Don't need to redefine main or change application class

        #region SDL_messagebox.h

        // SDL_MessageBoxFlags is covered by MessageBoxType.cs
        // SDL_MessageBoxButtonFlags is covered by MessageBoxButtonFlags.cs

        public readonly struct SDL_MessageBoxButtonData
        {
            public MessageBoxButtonOptions Flags { get; }
            public int ButtonId { get; }
            public Utf8String Text { get; }

            public SDL_MessageBoxButtonData(MessageBoxButtonOptions flags, int buttonId, Utf8String text)
            {
                Flags = flags;
                ButtonId = buttonId;
                Text = text;
            }
        }

        // SDL_MessageBoxColor is covered by MessageBoxColor.cs
        // SDL_MessageBoxColorType is not used
        // SDL_MessageBoxColorScheme is covered by MessageBoxColorScheme.cs

        public readonly struct SDL_MessageBoxData
        {
            public readonly MessageBoxType Flags { get; }

            public readonly SDL_Window* Window { get; }

            public readonly Utf8String Title { get; }

            public readonly Utf8String Message { get; }

            public readonly int Numbuttons { get; }

            public readonly SDL_MessageBoxButtonData* Buttons { get; }

            public readonly MessageBoxColorScheme* ColorScheme { get; }

            public SDL_MessageBoxData(MessageBoxType flags, SDL_Window* window, Utf8String title, Utf8String message, int numbuttons, SDL_MessageBoxButtonData* buttons, MessageBoxColorScheme* colorScheme)
            {
                Flags = flags;
                Window = window;
                Title = title;
                Message = message;
                Numbuttons = numbuttons;
                Buttons = buttons;
                ColorScheme = colorScheme;
            }
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_ShowMessageBox(in SDL_MessageBoxData messageboxdata, out int buttonid);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_ShowSimpleMessageBox(MessageBoxType flags, Utf8String title, Utf8String message, SDL_Window* window);

        #endregion

        // SDL_metal.h -- macOS/iOS specific window routines

        #region SDL_mouse.h

        public readonly struct SDL_Cursor
        {
        }

        // SDL_SYSTEM_CURSOR_* is covered by SystemCursor.cs

        // SDL_MouseWheelDirection is covered by MouseWheelDirection.cs

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Window* SDL_GetMouseFocus();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern MouseButton SDL_GetMouseState(out int x, out int y);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern MouseButton SDL_GetGlobalMouseState(out int x, out int y);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern MouseButton SDL_GetRelativeMouseState(out int x, out int y);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_WarpMouseInWindow(SDL_Window* window, int x, int y);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_WarpMouseGlobal(int x, int y);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetRelativeMouseMode(bool enabled);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_CaptureMouse(bool enabled);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GetRelativeMouseMode();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Cursor* SDL_CreateCursor(byte* data, byte* mask, int w, int h, int hot_x, int hot_y);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Cursor* SDL_CreateColorCursor(SDL_Surface* surface, int hot_x, int hot_y);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Cursor* SDL_CreateSystemCursor(SystemCursor id);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetCursor(SDL_Cursor* cursor);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Cursor* SDL_GetCursor();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Cursor* SDL_GetDefaultCursor();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FreeCursor(SDL_Cursor* cursor);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_ShowCursor(State toggle);

        // SDL_BUTTON_* is covered by MouseButton.cs

        #endregion

        // SDL_mutex.h -- Should use platform structures

        // SDL_name.h -- Defines

        // SDL_opengl.h -- Not supporting OpenGL at this time

        // SDL_opengl_glext.h -- Not supporting OpenGL at this time

        // SDL_opengles.h -- Not supporting OpenGL at this time

        // SDL_opengles2.h -- Not supporting OpenGL at this time

        // SDL_opengles2_gl2.h -- Not supporting OpenGL at this time

        // SDL_opengles2_gl2ext.h -- Not supporting OpenGL at this time

        // SDL_opengles2_gl2platform.h -- Not supporting OpenGL at this time

        // SDL_opengles2_khrplatform.h -- Not supporting OpenGL at this time

        #region SDL_pixels.h

        public enum SDL_PixelType : uint
        {
            Unknown,
            Index1,
            Index4,
            Index8,
            Packed8,
            Packed16,
            Packed32,
            ArrayU8,
            ArrayU16,
            ArrayU32,
            ArrayF16,
            ArrayF32
        };

        public enum SDL_BitmapOrder
        {
            None,
            Order4321,
            Order1234
        };

        public enum SDL_PackedOrder
        {
            None,
            Xrgb,
            Rgbx,
            Argb,
            Rgba,
            Xbgr,
            Bgrx,
            Abgr,
            Bgra
        };

        public enum SDL_ArrayOrder
        {
            None,
            Rgb,
            Rgba,
            Argb,
            Bgr,
            Bgra,
            Abgr
        };

        public enum SDL_PackedLayout
        {
            None,
            Layout332,
            Layout4444,
            Layout1555,
            Layout5551,
            Layout565,
            Layout8888,
            Layout2101010,
            Layout1010102
        };

        public static uint SDL_DefinePixelFormatCharacters(char a, char b, char c, char d) =>
            ((uint)(((byte)a) << 0))
            | ((uint)(((byte)b) << 8))
            | ((uint)(((byte)c) << 16))
            | ((uint)(((byte)d) << 24));

        public static uint SDL_DefinePixelFormat(uint type, uint order, uint layout, uint bits, uint bytes) =>
                (1 << 28) | (type << 24) | (order << 20) | (layout << 16)
                | (bits << 8) | (bytes << 0);

        public static uint SDL_GetPixelFlag(uint x) => (x >> 28) & 0x0F;
        public static SDL_PixelType SDL_GetPixelType(uint x) => (SDL_PixelType)((x >> 24) & 0x0F);
        public static uint SDL_GetPixelOrder(uint x) => (x >> 20) & 0x0F;
        public static uint SDL_GetPixelLayout(uint x) => (x >> 16) & 0x0F;
        public static uint SDL_GetBitsPerPixel(uint x) => (x >> 8) & 0xFF;

        public static uint SDL_GetBytesPerPixel(uint x) => SDL_IsPixelFormatCharacters(x) ? (((x == EnumeratedPixelFormat.Yuy2.Format) || (x == EnumeratedPixelFormat.Uyvy.Format) || (x == EnumeratedPixelFormat.Yvyu.Format)) ? 2u : 1u) : ((x >> 0) & 0xFF);

        public static bool SDL_IsPixelFormatIndexed(uint format) => !SDL_IsPixelFormatCharacters(format) && ((SDL_GetPixelType(format) == SDL_PixelType.Index1) || (SDL_GetPixelType(format) == SDL_PixelType.Index4) || (SDL_GetPixelType(format) == SDL_PixelType.Index8));

        public static bool SDL_IsPixelFormatPacked(uint format) => !SDL_IsPixelFormatCharacters(format) && ((SDL_GetPixelType(format) == SDL_PixelType.Packed8) || (SDL_GetPixelType(format) == SDL_PixelType.Packed16) || (SDL_GetPixelType(format) == SDL_PixelType.Packed32));

        public static bool SDL_IsPixelFormatArray(uint format) => !SDL_IsPixelFormatCharacters(format) && ((SDL_GetPixelType(format) == SDL_PixelType.ArrayU8) || (SDL_GetPixelType(format) == SDL_PixelType.ArrayU16) || (SDL_GetPixelType(format) == SDL_PixelType.ArrayU32) || (SDL_GetPixelType(format) == SDL_PixelType.ArrayF16) || (SDL_GetPixelType(format) == SDL_PixelType.ArrayF32));

        public static bool SDL_IsPixelFormatAlpha(uint format) => (SDL_IsPixelFormatPacked(format) && ((SDL_GetPixelOrder(format) == (uint)SDL_PackedOrder.Argb) || (SDL_GetPixelOrder(format) == (uint)SDL_PackedOrder.Rgba) || (SDL_GetPixelOrder(format) == (uint)SDL_PackedOrder.Abgr) || (SDL_GetPixelOrder(format) == (uint)SDL_PackedOrder.Bgra))) || (SDL_IsPixelFormatArray(format) && ((SDL_GetPixelOrder(format) == (uint)SDL_ArrayOrder.Argb) || (SDL_GetPixelOrder(format) == (uint)SDL_ArrayOrder.Rgba) || (SDL_GetPixelOrder(format) == (uint)SDL_ArrayOrder.Abgr) || (SDL_GetPixelOrder(format) == (uint)SDL_ArrayOrder.Bgra)));

        public static bool SDL_IsPixelFormatCharacters(uint format) => (format != 0) && (SDL_GetPixelFlag(format) != 1);

        // Enumerated pixel formats are in EnumeratedPixelFormat.cs

        // SDL_Color is covered by Color.cs

        public readonly struct SDL_Palette
        {
        }

        public readonly struct SDL_PixelFormat
        {
            public readonly uint Format;
            public readonly SDL_Palette* Palette;
            public readonly byte BitsPerPixel;
            public readonly byte BytesPerPixel;
            private readonly byte _padding1;
            private readonly byte _padding2;
            public readonly uint Rmask;
            public readonly uint Gmask;
            public readonly uint Bmask;
            public readonly uint Amask;
            public readonly byte Rloss;
            public readonly byte Gloss;
            public readonly byte Bloss;
            public readonly byte Aloss;
            public readonly byte Rshift;
            public readonly byte Gshift;
            public readonly byte Bshift;
            public readonly byte Ashift;
            public readonly int Refcount;
            public readonly SDL_PixelFormat* Next;
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string? SDL_GetPixelFormatName(uint format);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_PixelFormatEnumToMasks(uint format, out int bpp, out uint rmask, out uint gmask, out uint bmask, out uint amask);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_MasksToPixelFormatEnum(int bpp, uint rmask, uint gmask, uint bmask, uint amask);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_PixelFormat* SDL_AllocFormat(uint pixel_format);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FreeFormat(SDL_PixelFormat* format);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Palette* SDL_AllocPalette(int ncolors);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetPixelFormatPalette(SDL_PixelFormat* format, SDL_Palette* palette);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetPaletteColors(SDL_Palette* palette, Color[] colors, int firstcolor, int ncolors);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FreePalette(SDL_Palette* palette);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern PixelColor SDL_MapRGB(SDL_PixelFormat* format, byte r, byte g, byte b);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern PixelColor SDL_MapRGBA(SDL_PixelFormat* format, byte r, byte g, byte b, byte a);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetRGB(PixelColor pixel, SDL_PixelFormat* format, out byte r, out byte g, out byte b);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetRGBA(PixelColor pixel, SDL_PixelFormat* format, out byte r, out byte g, out byte b, out byte a);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_CalculateGammaRamp(float gamma, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)] out ushort[] ramp);

        #endregion

        #region SDL_platform.h

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string SDL_GetPlatform();

        #endregion

        #region SDL_power.h

        // SDL_PowerState is covered by PowerState.cs

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern PowerState SDL_GetPowerInfo(out int secs, out int pct);

        #endregion

        // SDL_quit.h -- Defines

        #region SDL_rect.h

        // SDL_Point is covered by Point.cs
        // SDL_FPoint is covered by PointF.cs
        // SDL_Rect is covered by Rectangle.cs
        // SDL_FRect is covered by RectangleF.cs

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasIntersection(in Rectangle a, in Rectangle b);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_IntersectRect(in Rectangle a, in Rectangle b, out Rectangle result);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_UnionRect(in Rectangle a, in Rectangle b, out Rectangle result);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_EnclosePoints([In] Point[] points, int count, in Rectangle clip, out Rectangle result);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_IntersectRectAndLine(in Rectangle rect, in int x1, in int y1, in int x2, in int y2);

        #endregion

        #region SDL_render.h

        // SDL_RendererFlags is covered by RendererOptions.cs
        // SDL_RendererInfo is covered by RendererInfo.cs
        // SDL_ScaleMode is covered by ScaleMode.cs
        // SDL_TextureAccess is covered by TextureAccess.cs
        // SDL_TextureModulate is not used anywhere
        // SDL_RendererFlip is covered by RendererFlip.cs

        public readonly struct SDL_Renderer
        {
        }

        public readonly struct SDL_Texture
        {
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumRenderDrivers();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRenderDriverInfo(int index, out RendererInfo info);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_CreateWindowAndRenderer(int width, int height, WindowOptions window_flags, out SDL_Window* window, out SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Renderer* SDL_CreateRenderer(SDL_Window* window, int index, RendererOptions flags);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Renderer* SDL_CreateSoftwareRenderer(SDL_Surface* surface);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Renderer* SDL_GetRenderer(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRendererInfo(SDL_Renderer* renderer, [Out] out RendererInfo info);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRendererOutputSize(SDL_Renderer* renderer, out int w, out int h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Texture* SDL_CreateTexture(SDL_Renderer* renderer, EnumeratedPixelFormat format, TextureAccess access, int w, int h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Texture* SDL_CreateTextureFromSurface(SDL_Renderer* renderer, SDL_Surface* surface);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_QueryTexture(SDL_Texture* texture, out EnumeratedPixelFormat format, out TextureAccess access, out int w, out int h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetTextureColorMod(SDL_Texture* texture, byte r, byte g, byte b);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetTextureColorMod(SDL_Texture* texture, out byte r, out byte g, out byte b);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetTextureAlphaMod(SDL_Texture* texture, byte alpha);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetTextureAlphaMod(SDL_Texture* texture, out byte alpha);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetTextureBlendMode(SDL_Texture* texture, BlendMode blendMode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetTextureBlendMode(SDL_Texture* texture, out BlendMode blendMode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetTextureScaleMode(SDL_Texture* texture, ScaleMode scaleMode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetTextureScaleMode(SDL_Texture* texture, out ScaleMode scaleMode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_UpdateTexture(SDL_Texture* texture, Rectangle* rect, byte* pixels, int pitch);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_UpdateYUVTexture(SDL_Texture* texture, Rectangle* rect, byte* yplane, int ypitch, byte* uplane, int upitch, byte* vplane, int vpitch);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_LockTexture(SDL_Texture* texture, Rectangle* rect, out byte* pixels, out int pitch);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_LockTextureToSurface(SDL_Texture* texture, Rectangle* rect, out SDL_Surface* surface);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_UnlockTexture(SDL_Texture* texture);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_RenderTargetSupported(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetRenderTarget(SDL_Renderer* renderer, SDL_Texture* texture);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Texture* SDL_GetRenderTarget(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderSetLogicalSize(SDL_Renderer* renderer, int w, int h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RenderGetLogicalSize(SDL_Renderer* renderer, out int w, out int h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderSetIntegerScale(SDL_Renderer* renderer, bool enable);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_RenderGetIntegerScale(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderSetViewport(SDL_Renderer* renderer, Rectangle* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RenderGetViewport(SDL_Renderer* renderer, out Rectangle rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderSetClipRect(SDL_Renderer* renderer, Rectangle* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RenderGetClipRect(SDL_Renderer* renderer, out Rectangle rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_RenderIsClipEnabled(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderSetScale(SDL_Renderer* renderer, float scaleX, float scaleY);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RenderGetScale(SDL_Renderer* renderer, out float scaleX, out float scaleY);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetRenderDrawColor(SDL_Renderer* renderer, byte r, byte g, byte b, byte a);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRenderDrawColor(SDL_Renderer* renderer, out byte r, out byte g, out byte b, out byte a);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetRenderDrawBlendMode(SDL_Renderer* renderer, BlendMode blendMode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRenderDrawBlendMode(SDL_Renderer* renderer, out BlendMode blendMode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderClear(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawPoint(SDL_Renderer* renderer, int x, int y);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawPoints(SDL_Renderer* renderer, Point[] points, int count);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawLine(SDL_Renderer* renderer, int x1, int y1, int x2, int y2);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawLines(SDL_Renderer* renderer, Point[] points, int count);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawRect(SDL_Renderer* renderer, Rectangle* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawRects(SDL_Renderer* renderer, Rectangle[] rects, int count);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFillRect(SDL_Renderer* renderer, Rectangle* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFillRects(SDL_Renderer* renderer, Rectangle[] rects, int count);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopy(SDL_Renderer* renderer, SDL_Texture* texture, Rectangle* srcrect, Rectangle* dstrect);

        [DllImport(Sdl2, EntryPoint = "SDL_RenderCopyEx", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopy(SDL_Renderer* renderer, SDL_Texture* texture, Rectangle* srcrect, Rectangle* dstrect, double angle, Point* center, RendererFlip flip);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawPointF(SDL_Renderer* renderer, float x, float y);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawPointsF(SDL_Renderer* renderer, PointF[] points, int count);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawLineF(SDL_Renderer* renderer, float x1, float y1, float x2, float y2);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawLinesF(SDL_Renderer* renderer, PointF[] points, int count);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawRectF(SDL_Renderer* renderer, RectangleF* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawRectsF(SDL_Renderer* renderer, RectangleF[] rects, int count);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFillRectF(SDL_Renderer* renderer, RectangleF* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFillRectsF(SDL_Renderer* renderer, RectangleF[] rects, int count);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyF(SDL_Renderer* renderer, SDL_Texture* texture, Rectangle* srcrect, RectangleF* dstrect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyExF(SDL_Renderer* renderer, SDL_Texture* texture, Rectangle* srcrect, RectangleF* dstrect, double angle, PointF* center, RendererFlip flip);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderReadPixels(SDL_Renderer* renderer, Rectangle* rect, EnumeratedPixelFormat format, byte* pixels, int pitch);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RenderPresent(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_DestroyTexture(SDL_Texture* texture);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_DestroyRenderer(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFlush(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GL_BindTexture(SDL_Texture* texture, out float texw, out float texh);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GL_UnbindTexture(SDL_Texture* texture);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nint SDL_RenderGetMetalLayer(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nint SDL_RenderGetMetalCommandEncoder(SDL_Renderer* renderer);

        #endregion

        // SDL_revision.h -- Revision number

        #region SDL_rwops.h

        public enum SDL_RWOpsType
        {
            Unknown,
            WinFile,
            StdFile,
            JniFile,
            Memory,
            MemoryReadOnly
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long SizeRWOps(SDL_RWops* context);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long SeekRWOps(SDL_RWops* context, long offset, SeekType whence);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate nuint ReadRWOps(SDL_RWops* context, void* ptr, nuint size, nuint maxnum);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate nuint WriteRWOps(SDL_RWops* context, void* ptr, nuint size, nuint num);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int CloseRWOps(SDL_RWops* context);

        public struct SDL_RWops
        {
            public nint Size { get; set; }
            public nint Seek { get; set; }
            public nint Read { get; set; }
            public nint Write { get; set; }
            public nint Close { get; set; }

            public SDL_RWOpsType Type { get; set; }

            public void* Data1 { get; set; }
            public void* Data2 { get; set; }
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern long SDL_RWsize(SDL_RWops* context);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern long SDL_RWseek(SDL_RWops* context, long offset, SeekType whence);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nuint SDL_RWread(SDL_RWops* context, void* buffer, nuint size, nuint maxnum);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nuint SDL_RWwrite(SDL_RWops* context, void* buffer, nuint size, nuint num);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RWclose(SDL_RWops* context);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern SDL_RWops* SDL_RWFromFile(string file, string mode);

        // SDL_RWFromFP

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_RWops* SDL_RWFromMem(void* mem, int size);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_RWops* SDL_RWFromConstMem(void* mem, int size);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_RWops* SDL_AllocRW();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FreeRW(SDL_RWops* area);

        // Seek macros covered by SeekType.cs
        // Macros in RWOps structure

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void* SDL_LoadFile_RW(SDL_RWops* src, out nuint datasize, bool freesrc);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void* SDL_LoadFile(string file, out nuint datasize);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte SDL_ReadU8(SDL_RWops* src);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_ReadLE16(SDL_RWops* src);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_ReadBE16(SDL_RWops* src);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_ReadLE32(SDL_RWops* src);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_ReadBE32(SDL_RWops* src);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong SDL_ReadLE64(SDL_RWops* src);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong SDL_ReadBE64(SDL_RWops* src);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nuint SDL_WriteU8(SDL_RWops* dst, byte value);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nuint SDL_WriteLE16(SDL_RWops* dst, ushort value);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nuint SDL_WriteBE16(SDL_RWops* dst, ushort value);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nuint SDL_WriteLE32(SDL_RWops* dst, uint value);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nuint SDL_WriteBE32(SDL_RWops* dst, uint value);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nuint SDL_WriteLE64(SDL_RWops* dst, ulong value);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nuint SDL_WriteBE64(SDL_RWops* dst, ulong value);

        #endregion

        #region SDL_scancode.h

        // SDL_Scancode is covered by Scancode.cs

        #endregion

        #region SDL_sensor.h

        public readonly struct SDL_Sensor
        {
        }

        // SDL_SensorType is covered by SensorType.cs

        public readonly struct SDL_SensorID
        {
            public int Id { get; }
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_NumSensors();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string? SDL_SensorGetDeviceName(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SensorType SDL_SensorGetDeviceType(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SensorGetDeviceNonPortableType(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_SensorID SDL_SensorGetDeviceInstanceID(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Sensor* SDL_SensorOpen(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Sensor* SDL_SensorFromInstanceID(SDL_SensorID instance_id);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string SDL_SensorGetName(SDL_Sensor* sensor);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SensorType SDL_SensorGetType(SDL_Sensor* sensor);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SensorGetNonPortableType(SDL_Sensor* sensor);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_SensorID SDL_SensorGetInstanceID(SDL_Sensor* sensor);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SensorGetData(SDL_Sensor* sensor, [MarshalAs(UnmanagedType.LPArray)] float[] data, int num_values);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SensorClose(SDL_Sensor* sensor);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SensorUpdate();

        #endregion

        #region SDL_shape.h

        public const int SDL_NonShapeableWindow = -1;
        public const int SDL_InvalidShapeArgument = -2;
        public const int SDL_WindowLacksShape = -3;

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Window* SDL_CreateShapedWindow(Utf8String title, uint x, uint y, uint w, uint h, WindowOptions flags);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_IsShapedWindow(SDL_Window* window);

        public enum WindowShapeMode
        {
            Default,
            BinarizeAlpha,
            ReverseBinarizeAlpha,
            ColorKey
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct SDL_WindowShapeParams
        {
            [FieldOffset(0)]
            public byte _binarizationCutoff;

            [FieldOffset(0)]
            public Color _colorKey;
        }

        public readonly struct SDL_WindowShapeMode
        {
            public WindowShapeMode Mode { get; }
            public SDL_WindowShapeParams Parameters { get; }

            public SDL_WindowShapeMode(WindowShapeMode mode, SDL_WindowShapeParams parameters = default)
            {
                Mode = mode;
                Parameters = parameters;
            }
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowShape(SDL_Window* window, SDL_Surface* shape, ref SDL_WindowShapeMode shape_mode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetShapedWindowMode(SDL_Window* window, out SDL_WindowShapeMode shape_mode);

        #endregion

        #region SDL_stdinc.h

        // Mostly native functions that should not be used on .NET

        // These are needed for interop, though

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void* SDL_malloc(nuint size);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_free(void* memblock);

        #endregion

        #region SDL_surface.h

        [Flags]
        public enum SDL_SurfaceOptions
        {
            None = 0,
            Prealloc = 0x00000001,
            RleAccel = 0x00000002,
            DontFree = 0x00000004,
            SimdAligned = 0x00000008
        }

        [StructLayout(LayoutKind.Sequential)]
        public readonly struct SDL_Surface
        {
            private readonly SDL_SurfaceOptions _flags;
            public readonly SDL_PixelFormat* Format { get; }
            public readonly int Width { get; }
            public readonly int Height { get; }
            public readonly int Pitch { get; }
            public readonly void* Pixels { get; }
            private readonly nint _userdata; // void*
            private readonly int _locked;
            private readonly nint _lock_data; // void*
            private readonly Rectangle _clip_rect;
            private readonly nint _map; // SDL_BlitMap*
            private readonly int _refcount;

            public bool MustLock => (_flags & SDL_SurfaceOptions.RleAccel) != 0;
        }

        // SDL_YUV_CONVERSION_MODE is covered by YuvConversionMode.cs

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* SDL_CreateRGBSurface(uint flags, int width, int height, int depth, uint rmask, uint gmask, uint bmask, uint amask);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* SDL_CreateRGBSurfaceWithFormat(uint flags, int width, int height, int depth, uint format);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* SDL_CreateRGBSurfaceFrom(byte* pixels, int width, int height, int depth, int pitch, uint rmask, uint gmask, uint bmask, uint amask);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* SDL_CreateRGBSurfaceWithFormatFrom(byte* pixels, int width, int height, int depth, int pitch, uint format);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FreeSurface(SDL_Surface* surface);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetSurfacePalette(SDL_Surface* surface, SDL_Palette* palette);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_LockSurface(SDL_Surface* surface);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_UnlockSurface(SDL_Surface* surface);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* SDL_LoadBMP_RW(SDL_RWops* src, bool freesrc);

        // SDL_LoadBMP

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SaveBMP_RW(SDL_Surface* surface, SDL_RWops* dst, bool freedst);

        // SDL_SaveBMP

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetSurfaceRLE(SDL_Surface* surface, bool flag);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetColorKey(SDL_Surface* surface, bool flag, PixelColor key);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasColorKey(SDL_Surface* surface);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetColorKey(SDL_Surface* surface, out PixelColor key);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetSurfaceColorMod(SDL_Surface* surface, byte r, byte g, byte b);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetSurfaceColorMod(SDL_Surface* surface, out byte r, out byte g, out byte b);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetSurfaceAlphaMod(SDL_Surface* surface, byte alpha);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetSurfaceAlphaMod(SDL_Surface* surface, out byte alpha);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetSurfaceBlendMode(SDL_Surface* surface, BlendMode blendMode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetSurfaceBlendMode(SDL_Surface* surface, out BlendMode blendMode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_SetClipRect(SDL_Surface* surface, Rectangle* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetClipRect(SDL_Surface* surface, out Rectangle rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* SDL_DuplicateSurface(SDL_Surface* surface);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* SDL_ConvertSurface(SDL_Surface* src, SDL_PixelFormat* fmt, uint flags = 0);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* SDL_ConvertSurfaceFormat(SDL_Surface* src, EnumeratedPixelFormat pixel_format, uint flags = 0);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_ConvertPixels(int width, int height, EnumeratedPixelFormat src_format, byte* src, int src_pitch, EnumeratedPixelFormat dst_format, byte* dst, int dst_pitch);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_FillRect(SDL_Surface* dst, Rectangle* rect, PixelColor color);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_FillRects(SDL_Surface* dst, Rectangle[] rects, int count, PixelColor color);

        //#define SDL_BlitSurface SDL_UpperBlit

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_UpperBlit(SDL_Surface* src, Rectangle* srcrect, SDL_Surface* dst, Rectangle* dstrect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_LowerBlit(SDL_Surface* src, Rectangle* srcrect, SDL_Surface* dst, Rectangle* dstrect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SoftStretch(SDL_Surface* src, Rectangle* srcrect, SDL_Surface* dst, Rectangle* dstrect);

        //#define SDL_BlitScaled SDL_UpperBlitScaled

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_UpperBlitScaled(SDL_Surface* src, Rectangle* srcrect, SDL_Surface* dst, Rectangle* dstrect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_LowerBlitScaled(SDL_Surface* src, Rectangle* srcrect, SDL_Surface* dst, Rectangle* dstrect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetYUVConversionMode(YuvConversionMode mode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern YuvConversionMode SDL_GetYUVConversionMode();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern YuvConversionMode SDL_GetYUVConversionModeForResolution(int width, int height);

        #endregion

        // SDL_system.h -- Not supporting low level Windows functions.

        #region SDL_syswm.h

        public enum SDL_SysWmType
        {
            Unknown,
            Windows,
            X11,
            DirectFB,
            Cocoa,
            UIKit,
            Wayland,
            MIR,
            WinRT,
            Android,
            Vivante,
            OS2,
            Haiku
        }

        // SDL_SysWMmsg for Windows is covered by SystemWindowMessage.cs

        public readonly struct SDL_SysWMmsg
        {
            public readonly Version Version;
            public readonly SDL_SysWmType Subsystem;
            public readonly SystemWindowMessage Win;
        }

        public readonly struct SDL_SysWMinfo_Windows
        {
            public readonly nint Window;
            public readonly nint Hdc;
            public readonly nint Hinstance;
        }

        [StructLayout(LayoutKind.Explicit)]
        public readonly struct SDL_SysWMinfo_Union
        {
            [FieldOffset(0)]
            public readonly SDL_SysWMinfo_Windows Win;

            [FieldOffset(0)]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public readonly byte[] Buffer;
        }

        public readonly struct SDL_SysWMinfo
        {
            public readonly Version Version;
            public readonly SDL_SysWmType Subsystem;
            public readonly SDL_SysWMinfo_Union Union;
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GetWindowWMInfo(SDL_Window* window, ref SDL_SysWMinfo info);

        #endregion

        // SDL_thread.h -- Should use Framework threading primitives

        // SDL_timer.h -- Should use Framework timing primitives

        #region SDL_touch.h

        public readonly struct SDL_TouchID
        {
            public long Id { get; }

            public SDL_TouchID(long id)
            {
                Id = id;
            }
        }

        public readonly struct SDL_FingerID
        {
            public long Id { get; }
        }

        // SDL_TouchDeviceType is covered by TouchDeviceType.cs

        public readonly struct SDL_Finger
        {
            public readonly SDL_FingerID Id;
            public readonly float X;
            public readonly float Y;
            public readonly float Pressure;
        }

        public const uint TouchMouseId = uint.MaxValue;

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumTouchDevices();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_TouchID SDL_GetTouchDevice(int index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern TouchDeviceType SDL_GetTouchDeviceType(SDL_TouchID touchID);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumTouchFingers(SDL_TouchID touchID);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Finger* SDL_GetTouchFinger(SDL_TouchID touchID, int index);

        #endregion

        #region SDL_version.h

        public static readonly Version IntegratedSdl2Version = new(2, 0, 12);

        public static int SDL_VersionNumber(Version version) =>
            (version.Major * 1000) + (version.Minor * 100) + version.Patch;

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetVersion(out Version ver);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string SDL_GetRevision();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRevisionNumber();

        #endregion

        #region SDL_video.h

        // SDL_DisplayMode is covered by DisplayMode.cs

        public readonly struct SDL_Window
        {
        }

        // SDL_WindowFlags is covered by WindowOptions.cs

        public enum SDL_WindowEventID : byte
        {
            None,
            Shown,
            Hidden,
            Exposed,
            Moved,
            Resized,
            SizeChanged,
            Minimized,
            Maximized,
            Restored,
            Enter,
            Leave,
            FocusGained,
            FocusLost,
            Close,
            TakeFocus,
            HitTest
        }

        public enum SDL_DisplayEventID : byte
        {
            None,
            Orientation
        }

        // SDL_DisplayOrientation is covered by DisplayOrientation.cs

        // SDL_GlContext
        // SDL_GLattr
        // SDL_GLprofile
        // SDL_GLcontextFlag
        // SDL_GLcontextReleaseFlag
        // SDL_GLContextResetNotification

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumVideoDrivers();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string SDL_GetVideoDriver(int index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int SDL_VideoInit(string driver_name);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_VideoQuit();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string SDL_GetCurrentVideoDriver();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumVideoDisplays();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern Utf8String SDL_GetDisplayName(int displayIndex);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetDisplayBounds(int displayIndex, out Rectangle rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetDisplayUsableBounds(int displayIndex, out Rectangle rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetDisplayDPI(int displayIndex, out float ddpi, out float hdpi, out float vdpi);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern DisplayOrientation SDL_GetDisplayOrientation(int displayIndex);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumDisplayModes(int displayIndex);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetDisplayMode(int displayIndex, int modeIndex, out DisplayMode mode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetDesktopDisplayMode(int displayIndex, out DisplayMode mode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetCurrentDisplayMode(int displayIndex, out DisplayMode mode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern DisplayMode* SDL_GetClosestDisplayMode(int displayIndex, ref DisplayMode mode, out DisplayMode closest);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetWindowDisplayIndex(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowDisplayMode(SDL_Window* window, ref DisplayMode mode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetWindowDisplayMode(SDL_Window* window, out DisplayMode mode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_GetWindowPixelFormat(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Window* SDL_CreateWindow(Utf8String title, int x, int y, int w, int h, WindowOptions flags);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Window* SDL_CreateWindowFrom(nint data);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_GetWindowID(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Window* SDL_GetWindowFromID(uint id);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern WindowOptions SDL_GetWindowFlags(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowTitle(SDL_Window* window, Utf8String title);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern Utf8String SDL_GetWindowTitle(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowIcon(SDL_Window* window, SDL_Surface* icon);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern nint SDL_SetWindowData(SDL_Window* window, string name, nint userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern nint SDL_GetWindowData(SDL_Window* window, string name);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowPosition(SDL_Window* window, int x, int y);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetWindowPosition(SDL_Window* window, out int x, out int y);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowSize(SDL_Window* window, int w, int h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetWindowSize(SDL_Window* window, out int w, out int h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetWindowBordersSize(SDL_Window* window, out int top, out int left, out int bottom, out int right);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowMinimumSize(SDL_Window* window, int min_w, int min_h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetWindowMinimumSize(SDL_Window* window, out int w, out int h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowMaximumSize(SDL_Window* window, int max_w, int max_h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetWindowMaximumSize(SDL_Window* window, out int w, out int h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowBordered(SDL_Window* window, bool bordered);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowResizable(SDL_Window* window, bool resizable);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_ShowWindow(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_HideWindow(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RaiseWindow(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_MaximizeWindow(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_MinimizeWindow(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RestoreWindow(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowFullscreen(SDL_Window* window, WindowOptions flags);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* SDL_GetWindowSurface(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_UpdateWindowSurface(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_UpdateWindowSurfaceRects(SDL_Window* window, Rectangle[] rects, int numrects);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowGrab(SDL_Window* window, bool grabbed);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GetWindowGrab(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Window* SDL_GetGrabbedWindow();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowBrightness(SDL_Window* window, float brightness);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern float SDL_GetWindowBrightness(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowOpacity(SDL_Window* window, float opacity);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetWindowOpacity(SDL_Window* window, out float out_opacity);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowModalFor(SDL_Window* modal_window, SDL_Window* parent_window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowInputFocus(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowGammaRamp(SDL_Window* window, [In, MarshalAs(UnmanagedType.LPArray, SizeConst = 256)] ushort[] red, [In, MarshalAs(UnmanagedType.LPArray, SizeConst = 256)] ushort[] green, [In, MarshalAs(UnmanagedType.LPArray, SizeConst = 256)] ushort[] blue);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetWindowGammaRamp(SDL_Window* window, [MarshalAs(UnmanagedType.LPArray, SizeConst = 256)] ushort[] red, [MarshalAs(UnmanagedType.LPArray, SizeConst = 256)] ushort[] green, [MarshalAs(UnmanagedType.LPArray, SizeConst = 256)] ushort[] blue);

        // SDL_HitTestResult is covered by HitTestResult.cs

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate HitTestResult HitTestCallback(SDL_Window* win, ref Point area, nint data);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowHitTest(SDL_Window* window, HitTestCallback? callback, nint callback_data);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_DestroyWindow(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_IsScreenSaverEnabled();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_EnableScreenSaver();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_DisableScreenSaver();

        //public extern static int SDL_GL_LoadLibrary(string path);
        //public extern static void* SDL_GL_GetProcAddress(string proc);
        //public extern static void SDL_GL_UnloadLibrary();
        //public extern static bool SDL_GL_ExtensionSupported(string extension);
        //public extern static void SDL_GL_ResetAttributes();
        //public extern static int SDL_GL_SetAttribute(SDL_GLattr attr, int value);
        //public extern static int SDL_GL_GetAttribute(SDL_GLattr attr, out int value);
        //public extern static SDL_GLContext SDL_GL_CreateContext(SDL_Window* window);
        //public extern static int SDL_GL_MakeCurrent(SDL_Window* window, SDL_GLContext context);
        //public extern static nint SDL_GL_GetCurrentWindow();
        //public extern static SDL_GLContext SDL_GL_GetCurrentContext();
        //public extern static void SDL_GL_GetDrawableSize(SDL_Window* window, out int w, out int h);
        //public extern static int SDL_GL_SetSwapInterval(int interval);
        //public extern static int SDL_GL_GetSwapInterval();
        //public extern static void SDL_GL_SwapWindow(SDL_Window* window);
        //public extern static void SDL_GL_DeleteContext(SDL_GLContext context);

        #endregion

        // SDL_vulkan.h -- Not supporting OpenGL at this time

        #region SDL_image.h

        public static readonly Version IntegratedSdl2ImageVersion = new(2, 0, 5);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern Version* IMG_Linked_Version();

        // IMG_INIT_* is covered by ImageFormats.cs

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IMG_Init(ImageFormats flags);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern void IMG_Quit();

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern SDL_Surface* IMG_LoadTyped_RW(SDL_RWops* src, bool freesrc, string type);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern SDL_Surface* IMG_Load(string file);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_Load_RW(SDL_RWops* src, bool freesrc);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern SDL_Texture* IMG_LoadTexture(SDL_Renderer* renderer, string file);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Texture* IMG_LoadTexture_RW(SDL_Renderer* renderer, SDL_RWops* src, bool freesrc);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern SDL_Texture* IMG_LoadTextureTyped_RW(SDL_Renderer* renderer, SDL_RWops* src, bool freesrc, string type);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IMG_isICO(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IMG_isCUR(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IMG_isBMP(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IMG_isGIF(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IMG_isJPG(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IMG_isLBM(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IMG_isPCX(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IMG_isPNG(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IMG_isPNM(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IMG_isSVG(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IMG_isTIF(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IMG_isXCF(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IMG_isXPM(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IMG_isXV(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IMG_isWEBP(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadICO_RW(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadCUR_RW(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadBMP_RW(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadGIF_RW(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadJPG_RW(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadLBM_RW(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadPCX_RW(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadPNG_RW(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadPNM_RW(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadSVG_RW(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadTGA_RW(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadTIF_RW(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadXCF_RW(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadXPM_RW(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadXV_RW(SDL_RWops* src);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadWEBP_RW(SDL_RWops* src);

        // IMG_ReadXPMFromArray

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int IMG_SavePNG(SDL_Surface* surface, string file);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IMG_SavePNG_RW(SDL_Surface* surface, SDL_RWops* dst, bool freedst);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int IMG_SaveJPG(SDL_Surface* surface, string file, int quality);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IMG_SaveJPG_RW(SDL_Surface* surface, SDL_RWops* dst, bool freedst, int quality);

        #endregion

        #region SDL_ttf.h

        public static readonly Version IntegratedSdl2TtfVersion = new(2, 0, 15);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern Version* TTF_Linked_Version();

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TTF_ByteSwappedUNICODE(bool swapped);

        public readonly struct TTF_Font
        {
        }

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TTF_Init();

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern TTF_Font* TTF_OpenFont(string file, int ptsize);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern TTF_Font* TTF_OpenFontIndex(string file, int ptsize, long index);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern TTF_Font* TTF_OpenFontRW(SDL_RWops* src, bool freesrc, int ptsize);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern TTF_Font* TTF_OpenFontIndexRW(SDL_RWops* src, bool freesrc, int ptsize, long index);

        // TTF_STYLE_* is covered by FontStyle.cs

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern FontStyle TTF_GetFontStyle(TTF_Font* font);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TTF_SetFontStyle(TTF_Font* font, FontStyle style);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool TTF_GetFontOutline(TTF_Font* font);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TTF_SetFontOutline(TTF_Font* font, bool outline);

        // TTF_HINTING_* is covered by FontHinting.cs

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern FontHinting TTF_GetFontHinting(TTF_Font* font);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TTF_SetFontHinting(TTF_Font* font, FontHinting hinting);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TTF_FontHeight(TTF_Font* font);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TTF_FontAscent(TTF_Font* font);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TTF_FontDescent(TTF_Font* font);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TTF_FontLineSkip(TTF_Font* font);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool TTF_GetFontKerning(TTF_Font* font);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TTF_SetFontKerning(TTF_Font* font, bool allowed);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern long TTF_FontFaces(TTF_Font* font);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool TTF_FontFaceIsFixedWidth(TTF_Font* font);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string TTF_FontFaceFamilyName(TTF_Font* font);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string TTF_FontFaceStyleName(TTF_Font* font);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int TTF_GlyphIsProvided(TTF_Font* font, char ch);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int TTF_GlyphMetrics(TTF_Font* font, char ch, out int minx, out int maxx, out int miny, out int maxy, out int advance);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int TTF_SizeText(TTF_Font* font, string text, out int w, out int h);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TTF_SizeUTF8(TTF_Font* font, nint text, out int w, out int h);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int TTF_SizeUNICODE(TTF_Font* font, string text, out int w, out int h);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern SDL_Surface* TTF_RenderText_Solid(TTF_Font* font, string text, Color fg);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* TTF_RenderUTF8_Solid(TTF_Font* font, nint text, Color fg);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern SDL_Surface* TTF_RenderUNICODE_Solid(TTF_Font* font, string text, Color fg);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern SDL_Surface* TTF_RenderGlyph_Solid(TTF_Font* font, char ch, Color fg);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern SDL_Surface* TTF_RenderText_Shaded(TTF_Font* font, string text, Color fg, Color bg);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* TTF_RenderUTF8_Shaded(TTF_Font* font, nint text, Color fg, Color bg);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern SDL_Surface* TTF_RenderUNICODE_Shaded(TTF_Font* font, string text, Color fg, Color bg);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern SDL_Surface* TTF_RenderGlyph_Shaded(TTF_Font* font, char ch, Color fg, Color bg);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern SDL_Surface* TTF_RenderText_Blended(TTF_Font* font, string text, Color fg);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* TTF_RenderUTF8_Blended(TTF_Font* font, nint text, Color fg);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern SDL_Surface* TTF_RenderUNICODE_Blended(TTF_Font* font, string text, Color fg);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern SDL_Surface* TTF_RenderText_Blended_Wrapped(TTF_Font* font, string text, Color fg, uint wrapLength);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* TTF_RenderUTF8_Blended_Wrapped(TTF_Font* font, nint text, Color fg, uint wrapLength);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern SDL_Surface* TTF_RenderUNICODE_Blended_Wrapped(TTF_Font* font, string text, Color fg, uint wrapLength);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern SDL_Surface* TTF_RenderGlyph_Blended(TTF_Font* font, char ch, Color fg);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TTF_CloseFont(TTF_Font* font);

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TTF_Quit();

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool TTF_WasInit();

        [DllImport(Sdl2Ttf, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int TTF_GetFontKerningSizeGlyphs(TTF_Font* font, char previous_ch, char ch);

        #endregion

        #region SDL_mixer.h

        public static readonly Version IntegratedSdl2MixerVersion = new(2, 0, 4);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern Version* Mix_Linked_Version();

        // MiX_InitFlags is covered by MixerFormats.cs

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_Init(MixerFormats flags);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Mix_Quit();

        // MIX_* constants are in Mixer.cs

        public readonly struct Mix_Chunk
        {
            private readonly int _allocated;
            private readonly nint _abuf;
            private readonly uint _alen;
            private readonly byte _volume;
        }

        // Mix_Fading is covered by Fading.cs
        // Mix_MusicType is covered by MusicType.cs

        public readonly struct Mix_Music
        {
        }

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_OpenAudio(int frequency, AudioFormat format, int channels, int chunksize);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_OpenAudioDevice(int frequency, AudioFormat format, int channels, int chunksize, Utf8String device, AudioAllowChange allowed_changes);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_AllocateChannels(int numchans);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_QuerySpec(out int frequency, out AudioFormat format, out int channels);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern Mix_Chunk* Mix_LoadWAV_RW(SDL_RWops* src, bool freesrc);

        public static Mix_Chunk* Mix_LoadWAV(string file) =>
            Mix_LoadWAV_RW(SDL_RWFromFile(file, "rb"), true);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern Mix_Music* Mix_LoadMUS(string file);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern Mix_Music* Mix_LoadMUS_RW(SDL_RWops* src, bool freesrc);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern Mix_Music* Mix_LoadMUSType_RW(SDL_RWops* src, MusicType type, bool freesrc);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern Mix_Chunk* Mix_QuickLoad_WAV(byte* mem);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern Mix_Chunk* Mix_QuickLoad_RAW(byte* mem, uint len);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Mix_FreeChunk(Mix_Chunk* chunk);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Mix_FreeMusic(Mix_Music* music);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_GetNumChunkDecoders();

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string Mix_GetChunkDecoder(int index);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern bool Mix_HasChunkDecoder(string name);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_GetNumMusicDecoders();

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string Mix_GetMusicDecoder(int index);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern bool Mix_HasMusicDecoder(string name);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern MusicType Mix_GetMusicType(Mix_Music* music);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void MixFunctionCallback(nint udata, nint stream, int len);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Mix_SetPostMix(MixFunctionCallback mix_func, nint arg);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Mix_HookMusic(MixFunctionCallback mix_func, nint arg);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Mix_HookMusicFinished(MusicFinishedCallback music_finished);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern nint Mix_GetMusicHookData();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void MusicChannelFinishedCallback(int channel);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Mix_ChannelFinished(MusicChannelFinishedCallback channel_finished);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void MixEffectCallback(int channel, nint stream, int length, nint userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void MixEffectDoneCallback(int channel, nint userData);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_RegisterEffect(int chan, MixEffectCallback f, MixEffectDoneCallback d, nint arg);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_UnregisterEffect(int channel, MixEffectCallback f);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_UnregisterAllEffects(int channel);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_SetPanning(int channel, byte left, byte right);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_SetPosition(int channel, short angle, byte distance);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_SetDistance(int channel, byte distance);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_SetReverseStereo(int channel, int flip);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_ReserveChannels(int num);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Mix_GroupChannel(int which, int tag);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Mix_GroupChannels(int from, int to, int tag);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_GroupAvailable(int tag);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_GroupCount(int tag);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_GroupOldest(int tag);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_GroupNewer(int tag);

        public static int Mix_PlayChannel(int channel, Mix_Chunk* chunk, int loops) =>
            Mix_PlayChannelTimed(channel, chunk, loops, -1);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_PlayChannelTimed(int channel, Mix_Chunk* chunk, int loops, int ticks);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_PlayMusic(Mix_Music* music, int loops);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_FadeInMusic(Mix_Music* music, int loops, int ms);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_FadeInMusicPos(Mix_Music* music, int loops, int ms, double position);

        public static int Mix_FadeInChannel(int channel, Mix_Chunk* chunk, int loops, int ms) =>
            Mix_FadeInChannelTimed(channel, chunk, loops, ms, -1);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_FadeInChannelTimed(int channel, Mix_Chunk* chunk, int loops, int ms, int ticks);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_Volume(int channel, int volume);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_VolumeChunk(Mix_Chunk* chunk, int volume);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_VolumeMusic(int volume);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_HaltChannel(int channel);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_HaltGroup(int tag);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_HaltMusic();

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_ExpireChannel(int channel, int ticks);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_FadeOutChannel(int which, int ms);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_FadeOutGroup(int tag, int ms);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Mix_FadeOutMusic(int ms);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern Fading Mix_FadingMusic();

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern Fading Mix_FadingChannel(int which);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Mix_Pause(int channel);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Mix_Resume(int channel);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Mix_Paused(int channel);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Mix_PauseMusic();

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Mix_ResumeMusic();

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Mix_RewindMusic();

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Mix_PausedMusic();

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_SetMusicPosition(double position);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Mix_Playing(int channel);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Mix_PlayingMusic();

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int Mix_SetMusicCMD(string command);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_SetSynchroValue(int value);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Mix_GetSynchroValue();

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int Mix_SetSoundFonts(string paths);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern string Mix_GetSoundFonts();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public delegate bool SoundFontFunction(string s, nint data);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern bool Mix_EachSoundFont(SoundFontFunction function, nint data);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern Mix_Chunk* Mix_GetChunk(int channel);

        [DllImport(Sdl2Mixer, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Mix_CloseAudio();

        #endregion
    }
}
