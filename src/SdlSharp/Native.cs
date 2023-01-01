using System.Runtime.InteropServices;
using System.Text;

using SdlSharp.Graphics;
using SdlSharp.Sound;

using static SdlSharp.Native;

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

// Identifiers should not have incorrect suffix
#pragma warning disable CA1711 

// Do not prefix enum values with type name
#pragma warning disable CA1712

// Identifier contains type name
#pragma warning disable CA1720

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
        /// Check that the return of a method is not an error given a validation function.
        /// </summary>
        /// <param name="returnValue">The return value of the API.</param>
        /// <param name="validator">The validator.</param>
        /// <returns>The return value.</returns>
        /// <exception cref="SdlException">Thrown if method returned an error.</exception>
        public static T CheckError<T>(T returnValue, Func<T, bool> validator) => !validator(returnValue) ? throw new SdlException() : returnValue;

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

        /// <summary>
        /// Converts a UTF8 string to a string.
        /// </summary>
        /// <param name="utf8">Pointer to the UTF8 string storage.</param>
        /// <returns>The string.</returns>
        public static string? Utf8ToString(byte* utf8)
        {
            static int StringLength(byte* v)
            {
                var current = v;
                while (*current != 0)
                {
                    current++;
                }
                return (int)(current - v);
            }

            return utf8 == null ? null : Encoding.UTF8.GetString(utf8, StringLength(utf8));
        }

        /// <summary>
        /// Converts a UTF8 string to a string and frees the UTF8 string.
        /// </summary>
        /// <param name="utf8">Pointer to the UFT8 string storage.</param>
        /// <returns>The string.</returns>
        public static string? Utf8ToStringAndFree(byte* utf8)
        {
            var result = Utf8ToString(utf8);
            SDL_free(utf8);
            return result;
        }

        /// <summary>
        /// Convert a regular string to a UTF-8 string.
        /// </summary>
        /// <param name="s">The regular string.</param>
        /// <returns>The new UTF-8 string.</returns>
        public static Span<byte> StringToUtf8(string? s)
        {
            if (s != null)
            {
                var terminatedString = s + '\0';
                var byteCount = Encoding.UTF8.GetByteCount(terminatedString);
                var buffer = new byte[byteCount];

                _ = Encoding.UTF8.GetBytes(terminatedString, 0, terminatedString.Length, buffer, byteCount);
                return new Span<byte>(buffer, 0, byteCount);
            }

            return null;
        }

        /// <summary>
        /// Converts a bool to a native int value.
        /// </summary>
        /// <param name="value">The bool value.</param>
        /// <returns>The int value.</returns>
        public static int BoolToInt(bool value) => value ? 1 : 0;

        internal static IReadOnlyList<T> GetIndexedCollection<T>(Func<int, T> getter, Func<int> counter)
        {
            var count = counter();
            var array = new T[count];
            for (var index = 0; index < count; index++)
            {
                array[index] = getter(index);
            }
            return array;
        }

        internal static IReadOnlyDictionary<TKey, TValue> GetIndexedDictionary<TKey, TValue>(Func<int, TKey> keyGetter, Func<int, TValue> valueGetter, Func<int> counter)
            where TKey : notnull
        {
            var count = counter();
            var dictionary = new Dictionary<TKey, TValue>();
            for (var index = 0; index < count; index++)
            {
                dictionary[keyGetter(index)] = valueGetter(index);
            }
            return dictionary;
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
        public static extern uint SDL_WasInit(uint flags);

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

        public readonly struct SDL_AudioSpec
        {
            public readonly int freq;
            public readonly SDL_AudioFormat format;
            public readonly byte channels;
            public readonly byte silence;
            public readonly ushort samples;
            private readonly ushort _padding;
            public readonly uint size;
            public readonly delegate* unmanaged[Cdecl]<nint, byte*, int, void> callback;
            public readonly nint userdata;

            public SDL_AudioSpec(int freq, SDL_AudioFormat format, byte channels, ushort samples, delegate* unmanaged[Cdecl]<nint, byte*, int, void> callback = default, nint userdata = default)
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
            public readonly int needed;
            public readonly SDL_AudioFormat src_format;
            public readonly SDL_AudioFormat dst_format;
            public readonly double rate_incr;
            public byte* buf;
            public int len;
            public readonly int len_cvt;
            public readonly int len_mult;
            public readonly double len_ratio;
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
        public static extern byte* SDL_GetAudioDriver(int index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_AudioInit(byte* driver_name);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_AudioQuit();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetCurrentAudioDriver();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_OpenAudio(SDL_AudioSpec* desired, SDL_AudioSpec* obtained);

        public readonly record struct SDL_AudioDeviceID(uint Id);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumAudioDevices(int iscapture);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetAudioDeviceName(int index, int iscapture);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetAudioDeviceSpec(int index, int iscapture, SDL_AudioSpec* spec);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetDefaultAudioInfo(byte** name, SDL_AudioSpec* spec, int iscapture);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_AudioDeviceID SDL_OpenAudioDevice(byte* device, int iscapture, SDL_AudioSpec* desired, SDL_AudioSpec* obtained, int allowed_changes);

        public enum SDL_AudioStatus
        {
            SDL_AUDIO_STOPPED = 0,
            SDL_AUDIO_PLAYING,
            SDL_AUDIO_PAUSED
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_AudioStatus SDL_GetAudioStatus();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_AudioStatus SDL_GetAudioDeviceStatus(SDL_AudioDeviceID dev);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_PauseAudio(int pause_on);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_PauseAudioDevice(SDL_AudioDeviceID dev, int pause_on);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_AudioSpec* SDL_LoadWAV_RW(SDL_RWops* src, int freesrc, SDL_AudioSpec* spec, byte** audio_buf, uint* audio_len);

        public static SDL_AudioSpec* SDL_LoadWAV(byte* file, SDL_AudioSpec* spec, byte** audio_buf, uint* audio_len)
        {
            fixed (byte* ptr = StringToUtf8("rb"))
            {
                return SDL_LoadWAV_RW(SDL_RWFromFile(file, ptr), BoolToInt(true), spec, audio_buf, audio_len);
            }
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FreeWAV(byte* audio_buf);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_BuildAudioCVT(SDL_AudioCVT* cvt, SDL_AudioFormat src_format, byte src_channels, int src_rate, SDL_AudioFormat dst_format, byte dst_channels, int dst_rate);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_ConvertAudio(SDL_AudioCVT* cvt);

        public readonly struct SDL_AudioStream { }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_AudioStream* SDL_NewAudioStream(SDL_AudioFormat src_format, byte src_channels, int src_rate, SDL_AudioFormat dst_format, byte dst_channels, int dst_rate);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_AudioStreamPut(SDL_AudioStream* stream, void* buf, int len);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_AudioStreamGet(SDL_AudioStream* stream, void* buf, int len);

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
        public static extern void SDL_MixAudioFormat(byte* dst, byte* src, SDL_AudioFormat format, uint len, int volume);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_QueueAudio(SDL_AudioDeviceID dev, void* data, uint len);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_DequeueAudio(SDL_AudioDeviceID dev, void* data, uint len);

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

        public enum SDL_BlendMode : uint
        {
            SDL_BLENDMODE_NONE = 0x00000000,
            SDL_BLENDMODE_BLEND = 0x00000001,
            SDL_BLENDMODE_ADD = 0x00000002,
            SDL_BLENDMODE_MOD = 0x00000004,
            SDL_BLENDMODE_MUL = 0x00000008,
            SDL_BLENDMODE_INVALID = 0x7FFFFFFF
        }

        public enum SDL_BlendOperation
        {
            SDL_BLENDOPERATION_ADD = 0x1,
            SDL_BLENDOPERATION_SUBTRACT = 0x2,
            SDL_BLENDOPERATION_REV_SUBTRACT = 0x3,
            SDL_BLENDOPERATION_MINIMUM = 0x4,
            SDL_BLENDOPERATION_MAXIMUM = 0x5
        }

        public enum SDL_BlendFactor
        {
            SDL_BLENDFACTOR_ZERO = 0x1,
            SDL_BLENDFACTOR_ONE = 0x2,
            SDL_BLENDFACTOR_SRC_COLOR = 0x3,
            SDL_BLENDFACTOR_ONE_MINUS_SRC_COLOR = 0x4,
            SDL_BLENDFACTOR_SRC_ALPHA = 0x5,
            SDL_BLENDFACTOR_ONE_MINUS_SRC_ALPHA = 0x6,
            SDL_BLENDFACTOR_DST_COLOR = 0x7,
            SDL_BLENDFACTOR_ONE_MINUS_DST_COLOR = 0x8,
            SDL_BLENDFACTOR_DST_ALPHA = 0x9,
            SDL_BLENDFACTOR_ONE_MINUS_DST_ALPHA = 0xA
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_BlendMode SDL_ComposeCustomBlendMode(SDL_BlendFactor srcColorFactor, SDL_BlendFactor dstColorFactor, SDL_BlendOperation colorOperation, SDL_BlendFactor srcAlphaFactor, SDL_BlendFactor dstAlphaFactor, SDL_BlendOperation alphaOperation);

        #endregion

        #region SDL_clipboard.h

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetClipboardText(byte* text);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetClipboardText();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasClipboardText();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetPrimarySelectionText(byte* text);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetPrimarySelectionText();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasPrimarySelectionText();

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
        public static extern bool SDL_HasLSX();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasLASX();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetSystemRAM();

        // SIMD should be used through .NET support, no direct support otherwise
        // SDL_SIMDGetAlignment
        // SDL_SIMDAlloc
        // SDL_SIMDRealloc
        // SDL_SIMDFree

        #endregion

        // SDL_egl.h - Not supporting OpenGL and related APIs at this time

        // SDL_endian.h - Should use platform facilities.

        #region SDL_error.h

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetError(byte* message /* ... */);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetError();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetErrorMsg(byte* errstr, int maxlen);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_ClearError();

        #endregion

        #region SDL_events.h

        public enum SDL_EventType
        {
            SDL_FIRSTEVENT = 0,

            SDL_QUIT = 0x100,

            SDL_APP_TERMINATING,
            SDL_APP_LOWMEMORY,
            SDL_APP_WILLENTERBACKGROUND,
            SDL_APP_DIDENTERBACKGROUND,
            SDL_APP_WILLENTERFOREGROUND,
            SDL_APP_DIDENTERFOREGROUND,

            SDL_LOCALECHANGED,

            SDL_DISPLAYEVENT = 0x150,

            SDL_WINDOWEVENT = 0x200,
            SDL_SYSWMEVENT,

            SDL_KEYDOWN = 0x300,
            SDL_KEYUP,
            SDL_TEXTEDITING,
            SDL_TEXTINPUT,
            SDL_KEYMAPCHANGED,

            SDL_TEXTEDITING_EXT,

            SDL_MOUSEMOTION = 0x400,
            SDL_MOUSEBUTTONDOWN,
            SDL_MOUSEBUTTONUP,
            SDL_MOUSEWHEEL,

            SDL_JOYAXISMOTION = 0x600,
            SDL_JOYBALLMOTION,
            SDL_JOYHATMOTION,
            SDL_JOYBUTTONDOWN,
            SDL_JOYBUTTONUP,
            SDL_JOYDEVICEADDED,
            SDL_JOYDEVICEREMOVED,
            SDL_JOYBATTERYUPDATED,

            SDL_CONTROLLERAXISMOTION = 0x650,
            SDL_CONTROLLERBUTTONDOWN,
            SDL_CONTROLLERBUTTONUP,
            SDL_CONTROLLERDEVICEADDED,
            SDL_CONTROLLERDEVICEREMOVED,
            SDL_CONTROLLERDEVICEREMAPPED,
            SDL_CONTROLLERTOUCHPADDOWN,
            SDL_CONTROLLERTOUCHPADMOTION,
            SDL_CONTROLLERTOUCHPADUP,
            SDL_CONTROLLERSENSORUPDATE,

            SDL_FINGERDOWN = 0x700,
            SDL_FINGERUP,
            SDL_FINGERMOTION,

            SDL_DOLLARGESTURE = 0x800,
            SDL_DOLLARRECORD,
            SDL_MULTIGESTURE,

            SDL_CLIPBOARDUPDATE = 0x900,

            SDL_DROPFILE = 0x1000,
            SDL_DROPTEXT,
            SDL_DROPBEGIN,
            SDL_DROPCOMPLETE,

            SDL_AUDIODEVICEADDED = 0x1100,
            SDL_AUDIODEVICEREMOVED,

            SDL_SENSORUPDATE = 0x1200,

            SDL_RENDER_TARGETS_RESET = 0x2000,
            SDL_RENDER_DEVICE_RESET,

            SDL_USEREVENT = 0x8000,

            SDL_LASTEVENT = 0xFFFF
        }

        public readonly struct SDL_CommonEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
        }

        public readonly struct SDL_DisplayEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly uint display;
            public readonly byte @event;
            public readonly byte _padding1;
            public readonly byte _padding2;
            public readonly byte _padding3;
            public readonly int data1;
        }

        public readonly struct SDL_WindowEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly uint windowID;
            public readonly byte @event;
            public readonly byte _padding1;
            public readonly byte _padding2;
            public readonly byte _padding3;
            public readonly int data1;
            public readonly int data2;
        }

        public readonly struct SDL_KeyboardEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly uint windowID;
            public readonly byte state;
            public readonly byte repeat;
            public readonly byte _padding2;
            public readonly byte _padding3;
            public readonly SDL_Keysym keysym;
        }

        public struct SDL_TextEditingEvent
        {
            public const int TextEditingEventTextSize = 32;

            public readonly uint type;
            public readonly uint timestamp;
            public readonly uint windowID;
            public fixed byte text[TextEditingEventTextSize];
            public readonly int start;
            public readonly int length;
        }

        public readonly struct SDL_TextEditingExtEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly uint windowID;
            public readonly byte* text;
            public readonly int start;
            public readonly int length;
        }

        public struct SDL_TextInputEvent
        {
            public const int TextInputEventTextSize = 32;

            public readonly uint type;
            public readonly uint timestamp;
            public readonly uint windowID;
            public fixed byte text[TextInputEventTextSize];
        }

        public readonly record struct SDL_MouseMotionEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly uint windowID;
            public readonly uint which;
            public readonly uint state;
            public readonly int x;
            public readonly int y;
            public readonly int xrel;
            public readonly int yrel;
        }

        public readonly struct SDL_MouseButtonEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly uint windowID;
            public readonly uint which;
            public readonly byte button;
            public readonly byte state;
            public readonly byte clicks;
            public readonly byte _padding1;
            public readonly int x;
            public readonly int y;
        }

        public readonly struct SDL_MouseWheelEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly uint windowID;
            public readonly uint which;
            public readonly int x;
            public readonly int y;
            public readonly uint direction;
            public readonly float preciseX;
            public readonly float preciseY;
            public readonly int mouseX;
            public readonly int mouseY;
        }

        public readonly struct SDL_JoyAxisEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly SDL_JoystickID which;
            public readonly byte axis;
            public readonly byte _padding1;
            public readonly byte _padding2;
            public readonly byte _padding3;
            public readonly short value;
            public readonly ushort _padding4;
        }

        public readonly struct SDL_JoyBallEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly SDL_JoystickID which;
            public readonly byte ball;
            public readonly byte _padding1;
            public readonly byte _padding2;
            public readonly byte _padding3;
            public readonly short xrel;
            public readonly short yrel;
        }

        public readonly struct SDL_JoyHatEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly SDL_JoystickID which;
            public readonly byte hat;
            public readonly byte value;
            public readonly byte _padding1;
            public readonly byte _padding2;
        }

        public readonly struct SDL_JoyButtonEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly SDL_JoystickID which;
            public readonly byte button;
            public readonly byte state;
            public readonly byte _padding1;
            public readonly byte _padding2;
        }

        public readonly struct SDL_JoyDeviceEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly int which;
        }

        public readonly struct SDL_JoyBatteryEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly SDL_JoystickID which;
            public readonly SDL_JoystickPowerLevel level;
        }

        public readonly struct SDL_ControllerAxisEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly SDL_JoystickID which;
            public readonly byte axis;
            public readonly byte _padding1;
            public readonly byte _padding2;
            public readonly byte _padding3;
            public readonly short value;
            public readonly ushort _padding4;
        }

        public readonly struct SDL_ControllerButtonEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly SDL_JoystickID which;
            public readonly byte button;
            public readonly byte state;
            public readonly byte _padding1;
            public readonly byte _padding2;
        }

        public readonly struct SDL_ControllerDeviceEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly int which;
        }

        public readonly struct SDL_ControllerTouchpadEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly SDL_JoystickID which;
            public readonly int touchpad;
            public readonly int finger;
            public readonly float x;
            public readonly float y;
            public readonly float pressure;
        }

        public readonly struct SDL_ControllerSensorEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly SDL_JoystickID which;
            public readonly int sensor;
            public readonly float data1;
            public readonly float data2;
            public readonly float data3;
            public readonly ulong timestamp_us;
        }

        public readonly struct SDL_AudioDeviceEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly uint which;
            public readonly byte iscapture;
            public readonly byte _padding1;
            public readonly byte _padding2;
            public readonly byte _padding3;
        }

        public readonly struct SDL_TouchFingerEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly SDL_TouchID touchId;
            public readonly SDL_FingerID fingerId;
            public readonly float x;
            public readonly float y;
            public readonly float dx;
            public readonly float dy;
            public readonly float pressure;
            public readonly uint windowID;
        }

        public readonly struct SDL_MultiGestureEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly SDL_TouchID touchId;
            public readonly float dTheta;
            public readonly float dDist;
            public readonly float x;
            public readonly float y;
            public readonly ushort numFingers;
            public readonly ushort _padding;
        }

        public readonly struct SDL_DollarGestureEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly SDL_TouchID touchId;
            public readonly SDL_GestureID gestureId;
            public readonly uint numFingers;
            public readonly float error;
            public readonly float x;
            public readonly float y;
        }

        public readonly struct SDL_DropEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly byte* file;
            public readonly uint windowID;
        }

        public struct SDL_SensorEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly int which;
            public fixed float data[6];
        }

        public readonly struct SDL_QuitEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
        }

        public readonly struct SDL_OSEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
        }

        public readonly struct SDL_UserEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly uint windowID;
            public readonly int code;
            public readonly void* data1;
            public readonly void* data2;
        }

        public readonly struct SDL_SysWMEvent
        {
            public readonly uint type;
            public readonly uint timestamp;
            public readonly /*SDL_SysWMmsg*/byte* msg;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct SDL_Event
        {
            [field: FieldOffset(0)]
            public readonly uint type { get; }

            [field: FieldOffset(0)]
            public readonly SDL_CommonEvent common { get; }

            [field: FieldOffset(0)]
            public readonly SDL_DisplayEvent display { get; }

            [field: FieldOffset(0)]
            public readonly SDL_WindowEvent window { get; }

            [field: FieldOffset(0)]
            public readonly SDL_KeyboardEvent key { get; }

            [field: FieldOffset(0)]
            public readonly SDL_TextEditingEvent edit { get; }

            [field: FieldOffset(0)]
            public readonly SDL_TextEditingExtEvent editExt { get; }

            [field: FieldOffset(0)]
            public readonly SDL_TextInputEvent text { get; }

            [field: FieldOffset(0)]
            public readonly SDL_MouseMotionEvent motion { get; }

            [field: FieldOffset(0)]
            public readonly SDL_MouseButtonEvent button { get; }

            [field: FieldOffset(0)]
            public readonly SDL_MouseWheelEvent wheel { get; }

            [field: FieldOffset(0)]
            public readonly SDL_JoyAxisEvent jaxis { get; }

            [field: FieldOffset(0)]
            public readonly SDL_JoyBallEvent jball { get; }

            [field: FieldOffset(0)]
            public readonly SDL_JoyHatEvent jhat { get; }

            [field: FieldOffset(0)]
            public readonly SDL_JoyButtonEvent jbutton { get; }

            [field: FieldOffset(0)]
            public readonly SDL_JoyDeviceEvent jdevice { get; }

            [field: FieldOffset(0)]
            public readonly SDL_JoyBatteryEvent jbattery { get; }

            [field: FieldOffset(0)]
            public readonly SDL_ControllerAxisEvent caxis { get; }

            [field: FieldOffset(0)]
            public readonly SDL_ControllerButtonEvent cbutton { get; }

            [field: FieldOffset(0)]
            public readonly SDL_ControllerDeviceEvent cdevice { get; }

            [field: FieldOffset(0)]
            public readonly SDL_ControllerTouchpadEvent ctouchpad { get; }

            [field: FieldOffset(0)]
            public readonly SDL_ControllerSensorEvent csensor { get; }

            [field: FieldOffset(0)]
            public readonly SDL_AudioDeviceEvent adevice { get; }

            [field: FieldOffset(0)]
            public readonly SDL_SensorEvent sensor { get; }

            [field: FieldOffset(0)]
            public readonly SDL_QuitEvent quit { get; }

            [field: FieldOffset(0)]
            public readonly SDL_UserEvent user { get; }

            [field: FieldOffset(0)]
            public readonly SDL_SysWMEvent syswm { get; }

            [field: FieldOffset(0)]
            public readonly SDL_TouchFingerEvent tfinger { get; }

            [field: FieldOffset(0)]
            public readonly SDL_MultiGestureEvent mgesture { get; }

            [field: FieldOffset(0)]
            public readonly SDL_DollarGestureEvent dgesture { get; }

            [field: FieldOffset(0)]
            public readonly SDL_DropEvent drop { get; }

            [FieldOffset(0)]
            private fixed byte _padding[56];
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_PumpEvents();

        public enum SDL_eventaction
        {
            SDL_ADDEVENT,
            SDL_PEEKEVENT,
            SDL_GETEVENT
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_PeepEvents(SDL_Event* events, int numevents, SDL_eventaction action, uint minType, uint maxType);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasEvent(uint type);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasEvents(uint minType, uint maxType);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FlushEvent(uint type);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FlushEvents(uint minType, uint maxType);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_PollEvent(SDL_Event* e);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_WaitEvent(SDL_Event* e);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_WaitEventTimeout(SDL_Event* e, int timeout);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_PushEvent(SDL_Event* e);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetEventFilter(delegate* unmanaged[Cdecl]<nint, SDL_Event*, bool> filter, nint userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GetEventFilter(delegate* unmanaged[Cdecl]<nint, SDL_Event*, bool>* filter, nint* userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_AddEventWatch(delegate* unmanaged[Cdecl]<nint, SDL_Event*, bool> filter, nint userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_DelEventWatch(delegate* unmanaged[Cdecl]<nint, SDL_Event*, bool> filter, nint userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FilterEvents(delegate* unmanaged[Cdecl]<nint, SDL_Event*, bool> filter, nint userdata);

        public const int SDL_QUERY = -1;
        public const int SDL_IGNORE = 0;
        public const int SDL_DISABLE = 0;
        public const int SDL_ENABLE = 1;

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte SDL_EventState(uint type, int state);

        public static int SDL_GetEventState(uint type) =>
            SDL_EventState(type, SDL_QUERY);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_RegisterEvents(int numevents);

        #endregion

        #region SDL_filesystem.h

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetBasePath();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetPrefPath(byte* org, byte* app);

        #endregion

        #region SDL_gamecontroller.h

        public readonly struct SDL_GameController { }

        public enum SDL_GameControllerType
        {
            SDL_CONTROLLER_TYPE_UNKNOWN = 0,
            SDL_CONTROLLER_TYPE_XBOX360,
            SDL_CONTROLLER_TYPE_XBOXONE,
            SDL_CONTROLLER_TYPE_PS3,
            SDL_CONTROLLER_TYPE_PS4,
            SDL_CONTROLLER_TYPE_NINTENDO_SWITCH_PRO,
            SDL_CONTROLLER_TYPE_VIRTUAL,
            SDL_CONTROLLER_TYPE_PS5,
            SDL_CONTROLLER_TYPE_AMAZON_LUNA,
            SDL_CONTROLLER_TYPE_GOOGLE_STADIA,
            SDL_CONTROLLER_TYPE_NVIDIA_SHIELD,
            SDL_CONTROLLER_TYPE_NINTENDO_SWITCH_JOYCON_LEFT,
            SDL_CONTROLLER_TYPE_NINTENDO_SWITCH_JOYCON_RIGHT,
            SDL_CONTROLLER_TYPE_NINTENDO_SWITCH_JOYCON_PAIR
        }

        public enum SDL_ControllerBindType
        {
            SDL_CONTROLLER_BINDTYPE_NONE = 0,
            SDL_CONTROLLER_BINDTYPE_BUTTON,
            SDL_CONTROLLER_BINDTYPE_AXIS,
            SDL_CONTROLLER_BINDTYPE_HAT
        }

        public readonly struct SDL_GameControllerButtonBind
        {
            public readonly SDL_ControllerBindType bindType;
            public readonly SDL_GameControllerButtonBindUnion value;

            [StructLayout(LayoutKind.Explicit)]
            public readonly struct SDL_GameControllerButtonBindUnion
            {
                [field: FieldOffset(0)]
                public readonly int button;

                [field: FieldOffset(0)]
                public readonly int axis;

                [field: FieldOffset(0)]
                public readonly SDL_GameControllerButtonBindUnionHatUnion hat;

                public readonly struct SDL_GameControllerButtonBindUnionHatUnion
                {
                    public readonly int hat;
                    public readonly int hat_mask;
                }
            }
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerAddMappingsFromRW(SDL_RWops* rw, int freerw);

        public static int SDL_GameControllerAddMappingsFromFile(byte* file)
        {
            fixed (byte* ptr = StringToUtf8("rb"))
            {
                return SDL_GameControllerAddMappingsFromRW(SDL_RWFromFile(file, ptr), BoolToInt(true));
            }
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerAddMapping(byte* mappingString);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerNumMappings();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GameControllerMappingForIndex(int mapping_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GameControllerMappingForGUID(Guid id);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GameControllerMapping(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_IsGameController(int joystick_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GameControllerNameForIndex(int joystick_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GameControllerPathForIndex(int joystick_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_GameControllerType SDL_GameControllerTypeForIndex(int joystick_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern byte* SDL_GameControllerMappingForDeviceIndex(int joystick_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_GameController* SDL_GameControllerOpen(int joystick_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_GameController* SDL_GameControllerFromInstanceID(SDL_JoystickID joyid);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_GameController* SDL_GameControllerFromPlayerIndex(int player_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GameControllerName(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GameControllerPath(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_GameControllerType SDL_GameControllerGetType(SDL_GameController* gamecontroller);

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
        public static extern ushort SDL_GameControllerGetFirmwareVersion(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GameControllerGetSerial(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GameControllerGetAttached(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Joystick* SDL_GameControllerGetJoystick(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerEventState(int state);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GameControllerUpdate();

        public enum SDL_GameControllerAxis : uint
        {
            SDL_CONTROLLER_AXIS_INVALID,
            SDL_CONTROLLER_AXIS_LEFTX,
            SDL_CONTROLLER_AXIS_LEFTY,
            SDL_CONTROLLER_AXIS_RIGHTX,
            SDL_CONTROLLER_AXIS_RIGHTY,
            SDL_CONTROLLER_AXIS_TRIGGERLEFT,
            SDL_CONTROLLER_AXIS_TRIGGERRIGHT,
            SDL_CONTROLLER_AXIS_MAX
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_GameControllerAxis SDL_GameControllerGetAxisFromString(byte* pchString);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GameControllerGetStringForAxis(SDL_GameControllerAxis axis);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_GameControllerButtonBind SDL_GameControllerGetBindForAxis(SDL_GameController* gamecontroller, SDL_GameControllerAxis axis);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GameControllerHasAxis(SDL_GameController* gamecontroller, SDL_GameControllerAxis axis);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern short SDL_GameControllerGetAxis(SDL_GameController* gamecontroller, SDL_GameControllerAxis axis);

        public enum SDL_GameControllerButton
        {
            SDL_CONTROLLER_BUTTON_INVALID = -1,
            SDL_CONTROLLER_BUTTON_A,
            SDL_CONTROLLER_BUTTON_B,
            SDL_CONTROLLER_BUTTON_X,
            SDL_CONTROLLER_BUTTON_Y,
            SDL_CONTROLLER_BUTTON_BACK,
            SDL_CONTROLLER_BUTTON_GUIDE,
            SDL_CONTROLLER_BUTTON_START,
            SDL_CONTROLLER_BUTTON_LEFTSTICK,
            SDL_CONTROLLER_BUTTON_RIGHTSTICK,
            SDL_CONTROLLER_BUTTON_LEFTSHOULDER,
            SDL_CONTROLLER_BUTTON_RIGHTSHOULDER,
            SDL_CONTROLLER_BUTTON_DPAD_UP,
            SDL_CONTROLLER_BUTTON_DPAD_DOWN,
            SDL_CONTROLLER_BUTTON_DPAD_LEFT,
            SDL_CONTROLLER_BUTTON_DPAD_RIGHT,
            SDL_CONTROLLER_BUTTON_MISC1,
            SDL_CONTROLLER_BUTTON_PADDLE1,
            SDL_CONTROLLER_BUTTON_PADDLE2,
            SDL_CONTROLLER_BUTTON_PADDLE3,
            SDL_CONTROLLER_BUTTON_PADDLE4,
            SDL_CONTROLLER_BUTTON_TOUCHPAD,
            SDL_CONTROLLER_BUTTON_MAX
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_GameControllerButton SDL_GameControllerGetButtonFromString(byte* pchString);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GameControllerGetStringForButton(SDL_GameControllerButton button);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_GameControllerButtonBind SDL_GameControllerGetBindForButton(SDL_GameController* gamecontroller, SDL_GameControllerButton button);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GameControllerHasButton(SDL_GameController* gamecontroller, SDL_GameControllerButton button);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte SDL_GameControllerGetButton(SDL_GameController* gamecontroller, SDL_GameControllerButton button);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerGetNumTouchpads(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerGetNumTouchpadFingers(SDL_GameController* gamecontroller, int touchpad);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerGetTouchpadFinger(SDL_GameController* gamecontroller, int touchpad, int finger, byte* state, float* x, float* y, float* pressure);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GameControllerHasSensor(SDL_GameController* gamecontroller, SDL_SensorType type);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerSetSensorEnabled(SDL_GameController* gamecontroller, SDL_SensorType type, bool enabled);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GameControllerIsSensorEnabled(SDL_GameController* gamecontroller, SDL_SensorType type);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern float SDL_GameControllerGetSensorDataRate(SDL_GameController* gamecontroller, SDL_SensorType type);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerGetSensorData(SDL_GameController* gamecontroller, SDL_SensorType type, float* data, int num_values);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerGetSensorDataWithTimestamp(SDL_GameController* gamecontroller, SDL_SensorType type, ulong* timestamp, float* data, int num_values);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerRumble(SDL_GameController* gamecontroller, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerRumbleTriggers(SDL_GameController* gamecontroller, ushort left_rumble, ushort right_rumble, uint duration_ms);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GameControllerHasLED(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GameControllerHasRumble(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GameControllerHasRumbleTriggers(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerSetLED(SDL_GameController* gamecontroller, byte red, byte green, byte blue);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerSendEffect(SDL_GameController* gamecontroller, void* data, int size);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GameControllerClose(SDL_GameController* gamecontroller);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GameControllerGetAppleSFSymbolsNameForButton(SDL_GameController* gamecontroller, SDL_GameControllerButton button);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GameControllerGetAppleSFSymbolsNameForAxis(SDL_GameController* gamecontroller, SDL_GameControllerAxis axis);

        #endregion

        #region SDL_gesture.h

        public readonly record struct SDL_GestureID(long Id);

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

        public readonly struct SDL_Haptic { }

        public const ushort SDL_HAPTIC_CONSTANT = 1 << 0;
        public const ushort SDL_HAPTIC_SINE = 1 << 1;
        public const ushort SDL_HAPTIC_LEFTRIGHT = 1 << 2;
        public const ushort SDL_HAPTIC_TRIANGLE = 1 << 3;
        public const ushort SDL_HAPTIC_SAWTOOTHUP = 1 << 4;
        public const ushort SDL_HAPTIC_SAWTOOTHDOWN = 1 << 5;
        public const ushort SDL_HAPTIC_RAMP = 1 << 6;
        public const ushort SDL_HAPTIC_SPRING = 1 << 7;
        public const ushort SDL_HAPTIC_DAMPER = 1 << 8;
        public const ushort SDL_HAPTIC_INERTIA = 1 << 9;
        public const ushort SDL_HAPTIC_FRICTION = 1 << 10;
        public const ushort SDL_HAPTIC_CUSTOM = 1 << 11;
        public const ushort SDL_HAPTIC_GAIN = 1 << 12;
        public const ushort SDL_HAPTIC_AUTOCENTER = 1 << 13;
        public const ushort SDL_HAPTIC_STATUS = 1 << 14;
        public const ushort SDL_HAPTIC_PAUSE = 1 << 15;

        public const byte SDL_HAPTIC_POALR = 0;
        public const byte SDL_HAPTIC_CARTESIAN = 1;
        public const byte SDL_HAPTIC_SPHERICAL = 2;
        public const byte SDL_HAPTIC_STEERING_AXIS = 3;

        public const uint SDL_HAPTIC_INFINITY = 4294967295U;

        public struct SDL_HapticDirection
        {
            public byte type;
            public int dir0;
            public int dir1;
            public int dir2;
        }

        public struct SDL_HapticConstant
        {
            public ushort type;
            public SDL_HapticDirection direction;

            public uint length;
            public ushort delay;

            public ushort button;
            public ushort interval;

            public short level;

            public ushort attack_length;
            public ushort attack_level;
            public ushort fade_length;
            public ushort fade_level;
        }

        public struct SDL_HapticPeriodic
        {
            public ushort type;
            public SDL_HapticDirection direction;

            public uint length;
            public ushort delay;

            public ushort button;
            public ushort interval;

            public ushort period;
            public short magnitude;
            public short offset;
            public ushort phase;

            public ushort attack_length;
            public ushort attack_level;
            public ushort fade_length;
            public ushort fade_level;
        }

        public struct SDL_HapticCondition
        {
            public ushort type;
            public SDL_HapticDirection direction;

            public uint length;
            public ushort delay;

            public ushort button;
            public ushort interval;

            public ushort right_sat0;
            public ushort right_sat1;
            public ushort right_sat2;
            public ushort left_sat0;
            public ushort left_sat1;
            public ushort left_sat2;
            public short right_coeff0;
            public short right_coeff1;
            public short right_coeff2;
            public short left_coeff0;
            public short left_coeff1;
            public short left_coeff2;
            public ushort deadband0;
            public ushort deadband1;
            public ushort deadband2;
            public short center0;
            public short center1;
            public short center2;
        }

        public struct SDL_HapticRamp
        {
            public ushort type;
            public SDL_HapticDirection direction;

            public uint length;
            public ushort delay;

            public ushort button;
            public ushort interval;

            public short start;
            public short end;

            public ushort attack_length;
            public ushort attack_level;
            public ushort fade_length;
            public ushort fade_level;
        }

        public struct SDL_HapticLeftRight
        {
            public ushort type;

            public uint length;

            public ushort large_magnitude;
            public ushort small_magnitude;
        }

        public struct SDL_HapticCustom
        {
            public ushort type;
            public SDL_HapticDirection direction;

            public uint length;
            public ushort delay;

            public ushort button;
            public ushort interval;

            public byte channels;
            public ushort period;
            public ushort samples;
            public ushort* data;

            public ushort attack_length;
            public ushort attack_level;
            public ushort fade_length;
            public ushort fade_level;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct SDL_HapticEffect
        {
            [FieldOffset(0)]
            public byte _type;

            [FieldOffset(0)]
            public SDL_HapticConstant constant;

            [FieldOffset(0)]
            public SDL_HapticPeriodic periodic;

            [FieldOffset(0)]
            public SDL_HapticCondition condition;

            [FieldOffset(0)]
            public SDL_HapticRamp ramp;

            [FieldOffset(0)]
            public SDL_HapticLeftRight leftright;

            [FieldOffset(0)]
            public SDL_HapticCustom custom;
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_NumHaptics();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_HapticName(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Haptic* SDL_HapticOpen(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticOpened(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticIndex(SDL_Haptic* haptic);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_MouseIsHaptic();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Haptic* SDL_HapticOpenFromMouse();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickIsHaptic(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Haptic* SDL_HapticOpenFromJoystick(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_HapticClose(SDL_Haptic* haptic);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticNumEffects(SDL_Haptic* haptic);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticNumEffectsPlaying(SDL_Haptic* haptic);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticQuery(SDL_Haptic* haptic);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticNumAxes(SDL_Haptic* haptic);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticEffectSupported(SDL_Haptic* haptic, SDL_HapticEffect* effect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticNewEffect(SDL_Haptic* haptic, SDL_HapticEffect* effect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticUpdateEffect(SDL_Haptic* haptic, int effect, SDL_HapticEffect* data);

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

        #region SDL_hidapi.h

        public readonly struct SDL_hid_device { }

        public readonly struct SDL_hid_device_info
        {
            public readonly byte* path;
            public readonly ushort vendor_id;
            public readonly ushort product_id;
            public readonly char* serial_number;
            public readonly ushort release_number;
            public readonly char* manufacturer_string;
            public readonly char* product_string;
            public readonly ushort usage_page;
            public readonly ushort usage;
            public readonly int interface_number;
            public readonly int interface_class;
            public readonly int interface_subclass;
            public readonly int interface_protocol;

            public readonly SDL_hid_device_info* next;
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_hid_init();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_hid_exit();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_hid_device_change_count();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_hid_device_info* SDL_hid_enumerate(ushort vendor_id, ushort product_id);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_hid_free_enumeration(SDL_hid_device_info* devs);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_hid_device* SDL_hid_open(ushort vendor_id, ushort product_id, char* serial_number);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_hid_device* SDL_hid_open_path(byte* path, int bExclusive);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_hid_write(SDL_hid_device* dev, byte* data, nuint length);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_hid_read_timeout(SDL_hid_device* dev, byte* data, nuint length, int milliseconds);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_hid_read(SDL_hid_device* dev, byte* data, nuint length);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_hid_set_nonblocking(SDL_hid_device* dev, int nonblock);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_hid_send_feature_report(SDL_hid_device* dev, byte* data, nuint length);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_hid_get_feature_report(SDL_hid_device* dev, byte* data, nuint length);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_hid_close(SDL_hid_device* dev);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_hid_get_manufacturer_string(SDL_hid_device* dev, char* s, nuint maxlen);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_hid_get_product_string(SDL_hid_device* dev, char* s, nuint maxlen);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_hid_get_serial_number_string(SDL_hid_device* dev, char* s, nuint maxlen);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_hid_get_indexed_string(SDL_hid_device* dev, int string_index, char* s, nuint maxlen);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_hid_ble_scan(bool active);

        #endregion

        #region SDL_hints.h

        public const string SDL_HINT_ACCELEROMETER_AS_JOYSTICK = "SDL_ACCELEROMETER_AS_JOYSTICK";
        public const string SDL_HINT_ALLOW_ALT_TAB_WHILE_GRABBED = "SDL_ALLOW_ALT_TAB_WHILE_GRABBED";
        public const string SDL_HINT_ALLOW_TOPMOST = "SDL_ALLOW_TOPMOST";
        public const string SDL_HINT_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION = "SDL_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION";
        public const string SDL_HINT_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION = "SDL_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION";
        public const string SDL_HINT_ANDROID_BLOCK_ON_PAUSE = "SDL_ANDROID_BLOCK_ON_PAUSE";
        public const string SDL_HINT_ANDROID_BLOCK_ON_PAUSE_PAUSEAUDIO = "SDL_ANDROID_BLOCK_ON_PAUSE_PAUSEAUDIO";
        public const string SDL_HINT_ANDROID_TRAP_BACK_BUTTON = "SDL_ANDROID_TRAP_BACK_BUTTON";
        public const string SDL_HINT_APP_NAME = "SDL_APP_NAME";
        public const string SDL_HINT_APPLE_TV_CONTROLLER_UI_EVENTS = "SDL_APPLE_TV_CONTROLLER_UI_EVENTS";
        public const string SDL_HINT_APPLE_TV_REMOTE_ALLOW_ROTATION = "SDL_APPLE_TV_REMOTE_ALLOW_ROTATION";
        public const string SDL_HINT_AUDIO_CATEGORY = "SDL_AUDIO_CATEGORY";
        public const string SDL_HINT_AUDIO_DEVICE_APP_NAME = "SDL_AUDIO_DEVICE_APP_NAME";
        public const string SDL_HINT_AUDIO_DEVICE_STREAM_NAME = "SDL_AUDIO_DEVICE_STREAM_NAME";
        public const string SDL_HINT_AUDIO_DEVICE_STREAM_ROLE = "SDL_AUDIO_DEVICE_STREAM_ROLE";
        public const string SDL_HINT_AUDIO_RESAMPLING_MODE = "SDL_AUDIO_RESAMPLING_MODE";
        public const string SDL_HINT_AUTO_UPDATE_JOYSTICKS = "SDL_AUTO_UPDATE_JOYSTICKS";
        public const string SDL_HINT_AUTO_UPDATE_SENSORS = "SDL_AUTO_UPDATE_SENSORS";
        public const string SDL_HINT_BMP_SAVE_LEGACY_FORMAT = "SDL_BMP_SAVE_LEGACY_FORMAT";
        public const string SDL_HINT_DISPLAY_USABLE_BOUNDS = "SDL_DISPLAY_USABLE_BOUNDS";
        public const string SDL_HINT_EMSCRIPTEN_ASYNCIFY = "SDL_EMSCRIPTEN_ASYNCIFY";
        public const string SDL_HINT_EMSCRIPTEN_KEYBOARD_ELEMENT = "SDL_EMSCRIPTEN_KEYBOARD_ELEMENT";
        public const string SDL_HINT_ENABLE_STEAM_CONTROLLERS = "SDL_ENABLE_STEAM_CONTROLLERS";
        public const string SDL_HINT_EVENT_LOGGING = "SDL_EVENT_LOGGING";
        public const string SDL_HINT_FORCE_RAISEWINDOW = "SDL_HINT_FORCE_RAISEWINDOW";
        public const string SDL_HINT_FRAMEBUFFER_ACCELERATION = "SDL_FRAMEBUFFER_ACCELERATION";
        public const string SDL_HINT_GAMECONTROLLERCONFIG = "SDL_GAMECONTROLLERCONFIG";
        public const string SDL_HINT_GAMECONTROLLERCONFIG_FILE = "SDL_GAMECONTROLLERCONFIG_FILE";
        public const string SDL_HINT_GAMECONTROLLERTYPE = "SDL_GAMECONTROLLERTYPE";
        public const string SDL_HINT_GAMECONTROLLER_IGNORE_DEVICES = "SDL_GAMECONTROLLER_IGNORE_DEVICES";
        public const string SDL_HINT_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT = "SDL_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT";
        public const string SDL_HINT_GAMECONTROLLER_USE_BUTTON_LABELS = "SDL_GAMECONTROLLER_USE_BUTTON_LABELS";
        public const string SDL_HINT_GRAB_KEYBOARD = "SDL_GRAB_KEYBOARD";
        public const string SDL_HINT_HIDAPI_IGNORE_DEVICES = "SDL_HIDAPI_IGNORE_DEVICES";
        public const string SDL_HINT_IDLE_TIMER_DISABLED = "SDL_IOS_IDLE_TIMER_DISABLED";
        public const string SDL_HINT_IME_INTERNAL_EDITING = "SDL_IME_INTERNAL_EDITING";
        public const string SDL_HINT_IME_SHOW_UI = "SDL_IME_SHOW_UI";
        public const string SDL_HINT_IME_SUPPORT_EXTENDED_TEXT = "SDL_IME_SUPPORT_EXTENDED_TEXT";
        public const string SDL_HINT_IOS_HIDE_HOME_INDICATOR = "SDL_IOS_HIDE_HOME_INDICATOR";
        public const string SDL_HINT_JOYSTICK_ALLOW_BACKGROUND_EVENTS = "SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS";
        public const string SDL_HINT_JOYSTICK_HIDAPI = "SDL_JOYSTICK_HIDAPI";
        public const string SDL_HINT_JOYSTICK_HIDAPI_GAMECUBE = "SDL_JOYSTICK_HIDAPI_GAMECUBE";
        public const string SDL_HINT_JOYSTICK_GAMECUBE_RUMBLE_BRAKE = "SDL_JOYSTICK_GAMECUBE_RUMBLE_BRAKE";
        public const string SDL_HINT_JOYSTICK_HIDAPI_JOY_CONS = "SDL_JOYSTICK_HIDAPI_JOY_CONS";
        public const string SDL_HINT_JOYSTICK_HIDAPI_COMBINE_JOY_CONS = "SDL_JOYSTICK_HIDAPI_COMBINE_JOY_CONS";
        public const string SDL_HINT_JOYSTICK_HIDAPI_VERTICAL_JOY_CONS = "SDL_JOYSTICK_HIDAPI_VERTICAL_JOY_CONS";
        public const string SDL_HINT_JOYSTICK_HIDAPI_LUNA = "SDL_JOYSTICK_HIDAPI_LUNA";
        public const string SDL_HINT_JOYSTICK_HIDAPI_NINTENDO_CLASSIC = "SDL_JOYSTICK_HIDAPI_NINTENDO_CLASSIC";
        public const string SDL_HINT_JOYSTICK_HIDAPI_SHIELD = "SDL_JOYSTICK_HIDAPI_SHIELD";
        public const string SDL_HINT_JOYSTICK_HIDAPI_PS3 = "SDL_JOYSTICK_HIDAPI_PS3";
        public const string SDL_HINT_JOYSTICK_HIDAPI_PS4 = "SDL_JOYSTICK_HIDAPI_PS4";
        public const string SDL_HINT_JOYSTICK_HIDAPI_PS4_RUMBLE = "SDL_JOYSTICK_HIDAPI_PS4_RUMBLE";
        public const string SDL_HINT_JOYSTICK_HIDAPI_PS5 = "SDL_JOYSTICK_HIDAPI_PS5";
        public const string SDL_HINT_JOYSTICK_HIDAPI_PS5_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_PS5_PLAYER_LED";
        public const string SDL_HINT_JOYSTICK_HIDAPI_PS5_RUMBLE = "SDL_JOYSTICK_HIDAPI_PS5_RUMBLE";
        public const string SDL_HINT_JOYSTICK_HIDAPI_STADIA = "SDL_JOYSTICK_HIDAPI_STADIA";
        public const string SDL_HINT_JOYSTICK_HIDAPI_STEAM = "SDL_JOYSTICK_HIDAPI_STEAM";
        public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH = "SDL_JOYSTICK_HIDAPI_SWITCH";
        public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH_HOME_LED = "SDL_JOYSTICK_HIDAPI_SWITCH_HOME_LED";
        public const string SDL_HINT_JOYSTICK_HIDAPI_JOYCON_HOME_LED = "SDL_JOYSTICK_HIDAPI_JOYCON_HOME_LED";
        public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_SWITCH_PLAYER_LED";
        public const string SDL_HINT_JOYSTICK_HIDAPI_WII = "SDL_JOYSTICK_HIDAPI_WII";
        public const string SDL_HINT_JOYSTICK_HIDAPI_WII_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_WII_PLAYER_LED";
        public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX = "SDL_JOYSTICK_HIDAPI_XBOX";
        public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_360 = "SDL_JOYSTICK_HIDAPI_XBOX_360";
        public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_360_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_XBOX_360_PLAYER_LED";
        public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_360_WIRELESS = "SDL_JOYSTICK_HIDAPI_XBOX_360_WIRELESS";
        public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_ONE = "SDL_JOYSTICK_HIDAPI_XBOX_ONE";
        public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_ONE_HOME_LED = "SDL_JOYSTICK_HIDAPI_XBOX_ONE_HOME_LED";
        public const string SDL_HINT_JOYSTICK_RAWINPUT = "SDL_JOYSTICK_RAWINPUT";
        public const string SDL_HINT_JOYSTICK_RAWINPUT_CORRELATE_XINPUT = "SDL_JOYSTICK_RAWINPUT_CORRELATE_XINPUT";
        public const string SDL_HINT_JOYSTICK_ROG_CHAKRAM = "SDL_JOYSTICK_ROG_CHAKRAM";
        public const string SDL_HINT_JOYSTICK_THREAD = "SDL_JOYSTICK_THREAD";
        public const string SDL_HINT_KMSDRM_REQUIRE_DRM_MASTER = "SDL_KMSDRM_REQUIRE_DRM_MASTER";
        public const string SDL_HINT_JOYSTICK_DEVICE = "SDL_JOYSTICK_DEVICE";
        public const string SDL_HINT_LINUX_DIGITAL_HATS = "SDL_LINUX_DIGITAL_HATS";
        public const string SDL_HINT_LINUX_HAT_DEADZONES = "SDL_LINUX_HAT_DEADZONES";
        public const string SDL_HINT_LINUX_JOYSTICK_CLASSIC = "SDL_LINUX_JOYSTICK_CLASSIC";
        public const string SDL_HINT_LINUX_JOYSTICK_DEADZONES = "SDL_LINUX_JOYSTICK_DEADZONES";
        public const string SDL_HINT_MAC_BACKGROUND_APP = "SDL_MAC_BACKGROUND_APP";
        public const string SDL_HINT_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK = "SDL_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK";
        public const string SDL_HINT_MAC_OPENGL_ASYNC_DISPATCH = "SDL_MAC_OPENGL_ASYNC_DISPATCH";
        public const string SDL_HINT_MOUSE_DOUBLE_CLICK_RADIUS = "SDL_MOUSE_DOUBLE_CLICK_RADIUS";
        public const string SDL_HINT_MOUSE_DOUBLE_CLICK_TIME = "SDL_MOUSE_DOUBLE_CLICK_TIME";
        public const string SDL_HINT_MOUSE_FOCUS_CLICKTHROUGH = "SDL_MOUSE_FOCUS_CLICKTHROUGH";
        public const string SDL_HINT_MOUSE_NORMAL_SPEED_SCALE = "SDL_MOUSE_NORMAL_SPEED_SCALE";
        public const string SDL_HINT_MOUSE_RELATIVE_MODE_CENTER = "SDL_MOUSE_RELATIVE_MODE_CENTER";
        public const string SDL_HINT_MOUSE_RELATIVE_MODE_WARP = "SDL_MOUSE_RELATIVE_MODE_WARP";
        public const string SDL_HINT_MOUSE_RELATIVE_SCALING = "SDL_MOUSE_RELATIVE_SCALING";
        public const string SDL_HINT_MOUSE_RELATIVE_SPEED_SCALE = "SDL_MOUSE_RELATIVE_SPEED_SCALE";
        public const string SDL_HINT_MOUSE_RELATIVE_SYSTEM_SCALE = "SDL_MOUSE_RELATIVE_SYSTEM_SCALE";
        public const string SDL_HINT_MOUSE_RELATIVE_WARP_MOTION = "SDL_MOUSE_RELATIVE_WARP_MOTION";
        public const string SDL_HINT_MOUSE_TOUCH_EVENTS = "SDL_MOUSE_TOUCH_EVENTS";
        public const string SDL_HINT_MOUSE_AUTO_CAPTURE = "SDL_MOUSE_AUTO_CAPTURE";
        public const string SDL_HINT_NO_SIGNAL_HANDLERS = "SDL_NO_SIGNAL_HANDLERS";
        public const string SDL_HINT_OPENGL_ES_DRIVER = "SDL_OPENGL_ES_DRIVER";
        public const string SDL_HINT_ORIENTATIONS = "SDL_IOS_ORIENTATIONS";
        public const string SDL_HINT_POLL_SENTINEL = "SDL_POLL_SENTINEL";
        public const string SDL_HINT_PREFERRED_LOCALES = "SDL_PREFERRED_LOCALES";
        public const string SDL_HINT_QTWAYLAND_CONTENT_ORIENTATION = "SDL_QTWAYLAND_CONTENT_ORIENTATION";
        public const string SDL_HINT_QTWAYLAND_WINDOW_FLAGS = "SDL_QTWAYLAND_WINDOW_FLAGS";
        public const string SDL_HINT_RENDER_BATCHING = "SDL_RENDER_BATCHING";
        public const string SDL_HINT_RENDER_LINE_METHOD = "SDL_RENDER_LINE_METHOD";
        public const string SDL_HINT_RENDER_DIRECT3D11_DEBUG = "SDL_RENDER_DIRECT3D11_DEBUG";
        public const string SDL_HINT_RENDER_DIRECT3D_THREADSAFE = "SDL_RENDER_DIRECT3D_THREADSAFE";
        public const string SDL_HINT_RENDER_DRIVER = "SDL_RENDER_DRIVER";
        public const string SDL_HINT_RENDER_LOGICAL_SIZE_MODE = "SDL_RENDER_LOGICAL_SIZE_MODE";
        public const string SDL_HINT_RENDER_OPENGL_SHADERS = "SDL_RENDER_OPENGL_SHADERS";
        public const string SDL_HINT_RENDER_SCALE_QUALITY = "SDL_RENDER_SCALE_QUALITY";
        public const string SDL_HINT_RENDER_VSYNC = "SDL_RENDER_VSYNC";
        public const string SDL_HINT_PS2_DYNAMIC_VSYNC = "SDL_PS2_DYNAMIC_VSYNC";
        public const string SDL_HINT_RETURN_KEY_HIDES_IME = "SDL_RETURN_KEY_HIDES_IME";
        public const string SDL_HINT_RPI_VIDEO_LAYER = "SDL_RPI_VIDEO_LAYER";
        public const string SDL_HINT_SCREENSAVER_INHIBIT_ACTIVITY_NAME = "SDL_SCREENSAVER_INHIBIT_ACTIVITY_NAME";
        public const string SDL_HINT_THREAD_FORCE_REALTIME_TIME_CRITICAL = "SDL_THREAD_FORCE_REALTIME_TIME_CRITICAL";
        public const string SDL_HINT_THREAD_PRIORITY_POLICY = "SDL_THREAD_PRIORITY_POLICY";
        public const string SDL_HINT_THREAD_STACK_SIZE = "SDL_THREAD_STACK_SIZE";
        public const string SDL_HINT_TIMER_RESOLUTION = "SDL_TIMER_RESOLUTION";
        public const string SDL_HINT_TOUCH_MOUSE_EVENTS = "SDL_TOUCH_MOUSE_EVENTS";
        public const string SDL_HINT_VITA_TOUCH_MOUSE_DEVICE = "SDL_HINT_VITA_TOUCH_MOUSE_DEVICE";
        public const string SDL_HINT_TV_REMOTE_AS_JOYSTICK = "SDL_TV_REMOTE_AS_JOYSTICK";
        public const string SDL_HINT_VIDEO_ALLOW_SCREENSAVER = "SDL_VIDEO_ALLOW_SCREENSAVER";
        public const string SDL_HINT_VIDEO_DOUBLE_BUFFER = "SDL_VIDEO_DOUBLE_BUFFER";
        public const string SDL_HINT_VIDEO_EGL_ALLOW_TRANSPARENCY = "SDL_VIDEO_EGL_ALLOW_TRANSPARENCY";
        public const string SDL_HINT_VIDEO_EXTERNAL_CONTEXT = "SDL_VIDEO_EXTERNAL_CONTEXT";
        public const string SDL_HINT_VIDEO_HIGHDPI_DISABLED = "SDL_VIDEO_HIGHDPI_DISABLED";
        public const string SDL_HINT_VIDEO_MAC_FULLSCREEN_SPACES = "SDL_VIDEO_MAC_FULLSCREEN_SPACES";
        public const string SDL_HINT_VIDEO_MINIMIZE_ON_FOCUS_LOSS = "SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS";
        public const string SDL_HINT_VIDEO_WAYLAND_ALLOW_LIBDECOR = "SDL_VIDEO_WAYLAND_ALLOW_LIBDECOR";
        public const string SDL_HINT_VIDEO_WAYLAND_PREFER_LIBDECOR = "SDL_VIDEO_WAYLAND_PREFER_LIBDECOR";
        public const string SDL_HINT_VIDEO_WAYLAND_MODE_EMULATION = "SDL_VIDEO_WAYLAND_MODE_EMULATION";
        public const string SDL_HINT_VIDEO_WAYLAND_EMULATE_MOUSE_WARP = "SDL_VIDEO_WAYLAND_EMULATE_MOUSE_WARP";
        public const string SDL_HINT_VIDEO_WINDOW_SHARE_PIXEL_FORMAT = "SDL_VIDEO_WINDOW_SHARE_PIXEL_FORMAT";
        public const string SDL_HINT_VIDEO_FOREIGN_WINDOW_OPENGL = "SDL_VIDEO_FOREIGN_WINDOW_OPENGL";
        public const string SDL_HINT_VIDEO_FOREIGN_WINDOW_VULKAN = "SDL_VIDEO_FOREIGN_WINDOW_VULKAN";
        public const string SDL_HINT_VIDEO_WIN_D3DCOMPILER = "SDL_VIDEO_WIN_D3DCOMPILER";
        public const string SDL_HINT_VIDEO_X11_FORCE_EGL = "SDL_VIDEO_X11_FORCE_EGL";
        public const string SDL_HINT_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR = "SDL_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR";
        public const string SDL_HINT_VIDEO_X11_NET_WM_PING = "SDL_VIDEO_X11_NET_WM_PING";
        public const string SDL_HINT_VIDEO_X11_WINDOW_VISUALID = "SDL_VIDEO_X11_WINDOW_VISUALID";
        public const string SDL_HINT_VIDEO_X11_XINERAMA = "SDL_VIDEO_X11_XINERAMA";
        public const string SDL_HINT_VIDEO_X11_XRANDR = "SDL_VIDEO_X11_XRANDR";
        public const string SDL_HINT_VIDEO_X11_XVIDMODE = "SDL_VIDEO_X11_XVIDMODE";
        public const string SDL_HINT_WAVE_FACT_CHUNK = "SDL_WAVE_FACT_CHUNK";
        public const string SDL_HINT_WAVE_RIFF_CHUNK_SIZE = "SDL_WAVE_RIFF_CHUNK_SIZE";
        public const string SDL_HINT_WAVE_TRUNCATION = "SDL_WAVE_TRUNCATION";
        public const string SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING = "SDL_WINDOWS_DISABLE_THREAD_NAMING";
        public const string SDL_HINT_WINDOWS_ENABLE_MESSAGELOOP = "SDL_WINDOWS_ENABLE_MESSAGELOOP";
        public const string SDL_HINT_WINDOWS_FORCE_MUTEX_CRITICAL_SECTIONS = "SDL_WINDOWS_FORCE_MUTEX_CRITICAL_SECTIONS";
        public const string SDL_HINT_WINDOWS_FORCE_SEMAPHORE_KERNEL = "SDL_WINDOWS_FORCE_SEMAPHORE_KERNEL";
        public const string SDL_HINT_WINDOWS_INTRESOURCE_ICON = "SDL_WINDOWS_INTRESOURCE_ICON";
        public const string SDL_HINT_WINDOWS_INTRESOURCE_ICON_SMALL = "SDL_WINDOWS_INTRESOURCE_ICON_SMALL";
        public const string SDL_HINT_WINDOWS_NO_CLOSE_ON_ALT_F4 = "SDL_WINDOWS_NO_CLOSE_ON_ALT_F4";
        public const string SDL_HINT_WINDOWS_USE_D3D9EX = "SDL_WINDOWS_USE_D3D9EX";
        public const string SDL_HINT_WINDOWS_DPI_AWARENESS = "SDL_WINDOWS_DPI_AWARENESS";
        public const string SDL_HINT_WINDOWS_DPI_SCALING = "SDL_WINDOWS_DPI_SCALING";
        public const string SDL_HINT_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN = "SDL_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN";
        public const string SDL_HINT_WINDOW_NO_ACTIVATION_WHEN_SHOWN = "SDL_WINDOW_NO_ACTIVATION_WHEN_SHOWN";
        public const string SDL_HINT_WINRT_HANDLE_BACK_BUTTON = "SDL_WINRT_HANDLE_BACK_BUTTON";
        public const string SDL_HINT_WINRT_PRIVACY_POLICY_LABEL = "SDL_WINRT_PRIVACY_POLICY_LABEL";
        public const string SDL_HINT_WINRT_PRIVACY_POLICY_URL = "SDL_WINRT_PRIVACY_POLICY_URL";
        public const string SDL_HINT_X11_FORCE_OVERRIDE_REDIRECT = "SDL_X11_FORCE_OVERRIDE_REDIRECT";
        public const string SDL_HINT_XINPUT_ENABLED = "SDL_XINPUT_ENABLED";
        public const string SDL_HINT_DIRECTINPUT_ENABLED = "SDL_DIRECTINPUT_ENABLED";
        public const string SDL_HINT_XINPUT_USE_OLD_JOYSTICK_MAPPING = "SDL_XINPUT_USE_OLD_JOYSTICK_MAPPING";
        public const string SDL_HINT_AUDIO_INCLUDE_MONITORS = "SDL_AUDIO_INCLUDE_MONITORS";
        public const string SDL_HINT_X11_WINDOW_TYPE = "SDL_X11_WINDOW_TYPE";
        public const string SDL_HINT_QUIT_ON_LAST_WINDOW_CLOSE = "SDL_QUIT_ON_LAST_WINDOW_CLOSE";
        public const string SDL_HINT_VIDEODRIVER = "SDL_VIDEODRIVER";
        public const string SDL_HINT_AUDIODRIVER = "SDL_AUDIODRIVER";
        public const string SDL_HINT_KMSDRM_DEVICE_INDEX = "SDL_KMSDRM_DEVICE_INDEX";
        public const string SDL_HINT_TRACKPAD_IS_TOUCH_ONLY = "SDL_TRACKPAD_IS_TOUCH_ONLY";

        public enum SDL_HintPriority
        {
            SDL_HINT_DEFAULT,
            SDL_HINT_NORMAL,
            SDL_HINT_OVERRIDE
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_SetHintWithPriority(byte* name, byte* value, SDL_HintPriority priority);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_SetHint(byte* name, byte* value);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_ResetHint(byte* name);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_ResetHints();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetHint(byte* name);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GetHintBoolean(byte* name, bool default_value);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_AddHintCallback(byte* name, delegate* unmanaged[Cdecl]<nint, byte*, byte*, byte*, void> callback, nint userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_DelHintCallback(byte* name, delegate* unmanaged[Cdecl]<nint, byte*, byte*, byte*, void> callback, nint userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_ClearHints();

        #endregion

        #region SDL_joystick.h

        public readonly struct SDL_Joystick { }

        // SDL_JoystickGUID is just System.Guid

        public readonly record struct SDL_JoystickID(int Id);

        public enum SDL_JoystickType
        {
            SDL_JOYSTICK_TYPE_UNKNOWN,
            SDL_JOYSTICK_TYPE_GAMECONTROLLER,
            SDL_JOYSTICK_TYPE_WHEEL,
            SDL_JOYSTICK_TYPE_ARCADE_STICK,
            SDL_JOYSTICK_TYPE_FLIGHT_STICK,
            SDL_JOYSTICK_TYPE_DANCE_PAD,
            SDL_JOYSTICK_TYPE_GUITAR,
            SDL_JOYSTICK_TYPE_DRUM_KIT,
            SDL_JOYSTICK_TYPE_ARCADE_PAD,
            SDL_JOYSTICK_TYPE_THROTTLE
        }

        public enum SDL_JoystickPowerLevel
        {
            SDL_JOYSTICK_POWER_UNKNOWN = -1,
            SDL_JOYSTICK_POWER_EMPTY,
            SDL_JOYSTICK_POWER_LOW,
            SDL_JOYSTICK_POWER_MEDIUM,
            SDL_JOYSTICK_POWER_FULL,
            SDL_JOYSTICK_POWER_WIRED,
            SDL_JOYSTICK_POWER_MAX
        }

        public const float SDL_IPHONE_MAX_GFORCE = 5.0F;

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LockJoysticks();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_UnlockJoysticks();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_NumJoysticks();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_JoystickNameForIndex(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_JoystickPathForIndex(int device_index);

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
        public static extern SDL_JoystickType SDL_JoystickGetDeviceType(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_JoystickID SDL_JoystickGetDeviceInstanceID(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Joystick* SDL_JoystickOpen(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Joystick* SDL_JoystickFromInstanceID(SDL_JoystickID instance_id);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Joystick* SDL_JoystickFromPlayerIndex(int player_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickAttachVirtual(SDL_JoystickType type, int naxes, int nbuttons, int nhats);

        public struct SDL_VirtualJoystickDesc
        {
            public ushort version;
            public ushort type;
            public ushort naxes;
            public ushort nbuttons;
            public ushort nhats;
            public ushort vendor_id;
            public ushort product_id;
            private ushort _padding;
            public uint button_mask;
            public uint axis_mask;
            public byte* name;

            public nint userdata;
            public delegate* unmanaged[Cdecl]<nint, void> Update;
            public delegate* unmanaged[Cdecl]<nint, int, void> SetPlayerIndex;
            public delegate* unmanaged[Cdecl]<nint, ushort, ushort, int> Rumble;
            public delegate* unmanaged[Cdecl]<nint, ushort, ushort, int> RumbleTriggers;
            public delegate* unmanaged[Cdecl]<nint, byte, byte, byte, int> SetLED;
            public delegate* unmanaged[Cdecl]<nint, byte*, int, int> SendEffect;
        }

        public const ushort SDL_VIRTUAL_JOYSTICK_DESC_VERSION = 1;

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickAttachVirtualEx(SDL_VirtualJoystickDesc* desc);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickDetachVirtual(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_JoystickIsVirtual(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickSetVirtualAxis(SDL_Joystick* joystick, int axis, short value);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickSetVirtualButton(SDL_Joystick* joystick, int button, byte value);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickSetVirtualHat(SDL_Joystick* joystick, int hat, byte value);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_JoystickName(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_JoystickPath(SDL_Joystick* joystick);

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
        public static extern ushort SDL_JoystickGetFirmwareVersion(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_JoystickGetSerial(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_JoystickType SDL_JoystickGetType(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_JoystickGetGUIDString(Guid id, byte* pszGUID, int cbGUID);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern Guid SDL_JoystickGetGUIDFromString(byte* pchGUID);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetJoystickGUIDInfo(Guid guid, ushort* vendor, ushort* product, ushort* version, ushort* crc16);

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
        public static extern int SDL_JoystickEventState(int state);

        public const short SDL_JOYSTICK_AXIS_MAX = 32767;
        public const short SDL_JOYSTICK_AXIS_MIN = -32768;

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern short SDL_JoystickGetAxis(SDL_Joystick* joystick, int axis);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_JoystickGetAxisInitialState(SDL_Joystick* joystick, int axis, short* state);

        public const byte SDL_HAT_CENTERED = 0x00;
        public const byte SDL_HAT_UP = 0x01;
        public const byte SDL_HAT_RIGHT = 0x02;
        public const byte SDL_HAT_DOWN = 0x04;
        public const byte SDL_HAT_LEFT = 0x08;
        public const byte SDL_HAT_RIGHTUP = SDL_HAT_RIGHT | SDL_HAT_UP;
        public const byte SDL_HAT_RIGHTDOWN = SDL_HAT_RIGHT | SDL_HAT_DOWN;
        public const byte SDL_HAT_LEFTUP = SDL_HAT_LEFT | SDL_HAT_UP;
        public const byte SDL_HAT_LEFTDOWN = SDL_HAT_LEFT | SDL_HAT_DOWN;

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte SDL_JoystickGetHat(SDL_Joystick* joystick, int hat);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickGetBall(SDL_Joystick* joystick, int ball, int* dx, int* dy);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern byte SDL_JoystickGetButton(SDL_Joystick* joystick, int button);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickRumble(SDL_Joystick* joystick, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickRumbleTriggers(SDL_Joystick* joystick, ushort left_rumble, ushort right_rumble, uint duration_ms);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_JoystickHasLED(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_JoystickHasRumble(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_JoystickHasRumbleTriggers(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickSetLED(SDL_Joystick* joystick, byte red, byte green, byte blue);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickSendEffect(SDL_Joystick* joystick, byte* data, int size);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_JoystickClose(SDL_Joystick* joystick);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_JoystickPowerLevel SDL_JoystickCurrentPowerLevel(SDL_Joystick* joystick);

        #endregion

        #region SDL_keyboard.h

        public readonly struct SDL_Keysym
        {
            public readonly SDL_Scancode scancode;
            public readonly SDL_KeycodeValue sym;
            public readonly ushort mod;
            private readonly uint _unused;
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Window* SDL_GetKeyboardFocus();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetKeyboardState(int* numkeys);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_ResetKeyboard();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Keymod SDL_GetModState();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetModState(SDL_Keymod modstate);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_KeycodeValue SDL_GetKeyFromScancode(SDL_Scancode scancode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Scancode SDL_GetScancodeFromKey(SDL_KeycodeValue key);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetScancodeName(SDL_Scancode scancode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Scancode SDL_GetScancodeFromName(byte* name);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetKeyName(SDL_KeycodeValue key);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_KeycodeValue SDL_GetKeyFromName(byte* name);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_StartTextInput();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_IsTextInputActive();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_StopTextInput();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_ClearComposition();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_IsTextInputShown();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetTextInputRect(SDL_Rect* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasScreenKeyboardSupport();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_IsScreenKeyboardShown(SDL_Window* window);

        #endregion

        #region SDK_keycode.h

        public readonly record struct SDL_KeycodeValue(int Value);

        public const int SDLK_SCANCODE_MASK = 1 << 30;

        public enum SDL_KeyCode
        {
            SDLK_UNKNOWN = 0,

            SDLK_RETURN = '\r',
            SDLK_ESCAPE = '\x1B',
            SDLK_BACKSPACE = '\b',
            SDLK_TAB = '\t',
            SDLK_SPACE = ' ',
            SDLK_EXCLAIM = '!',
            SDLK_QUOTEDBL = '"',
            SDLK_HASH = '#',
            SDLK_PERCENT = '%',
            SDLK_DOLLAR = '$',
            SDLK_AMPERSAND = '&',
            SDLK_QUOTE = '\'',
            SDLK_LEFTPAREN = '(',
            SDLK_RIGHTPAREN = ')',
            SDLK_ASTERISK = '*',
            SDLK_PLUS = '+',
            SDLK_COMMA = ',',
            SDLK_MINUS = '-',
            SDLK_PERIOD = '.',
            SDLK_SLASH = '/',
            SDLK_0 = '0',
            SDLK_1 = '1',
            SDLK_2 = '2',
            SDLK_3 = '3',
            SDLK_4 = '4',
            SDLK_5 = '5',
            SDLK_6 = '6',
            SDLK_7 = '7',
            SDLK_8 = '8',
            SDLK_9 = '9',
            SDLK_COLON = ':',
            SDLK_SEMICOLON = ';',
            SDLK_LESS = '<',
            SDLK_EQUALS = '=',
            SDLK_GREATER = '>',
            SDLK_QUESTION = '?',
            SDLK_AT = '@',

            /*
               Skip uppercase letters
             */

            SDLK_LEFTBRACKET = '[',
            SDLK_BACKSLASH = '\\',
            SDLK_RIGHTBRACKET = ']',
            SDLK_CARET = '^',
            SDLK_UNDERSCORE = '_',
            SDLK_BACKQUOTE = '`',
            SDLK_a = 'a',
            SDLK_b = 'b',
            SDLK_c = 'c',
            SDLK_d = 'd',
            SDLK_e = 'e',
            SDLK_f = 'f',
            SDLK_g = 'g',
            SDLK_h = 'h',
            SDLK_i = 'i',
            SDLK_j = 'j',
            SDLK_k = 'k',
            SDLK_l = 'l',
            SDLK_m = 'm',
            SDLK_n = 'n',
            SDLK_o = 'o',
            SDLK_p = 'p',
            SDLK_q = 'q',
            SDLK_r = 'r',
            SDLK_s = 's',
            SDLK_t = 't',
            SDLK_u = 'u',
            SDLK_v = 'v',
            SDLK_w = 'w',
            SDLK_x = 'x',
            SDLK_y = 'y',
            SDLK_z = 'z',

            SDLK_CAPSLOCK = SDL_Scancode.SDL_SCANCODE_CAPSLOCK | SDLK_SCANCODE_MASK,

            SDLK_F1 = SDL_Scancode.SDL_SCANCODE_F1 | SDLK_SCANCODE_MASK,
            SDLK_F2 = SDL_Scancode.SDL_SCANCODE_F2 | SDLK_SCANCODE_MASK,
            SDLK_F3 = SDL_Scancode.SDL_SCANCODE_F3 | SDLK_SCANCODE_MASK,
            SDLK_F4 = SDL_Scancode.SDL_SCANCODE_F4 | SDLK_SCANCODE_MASK,
            SDLK_F5 = SDL_Scancode.SDL_SCANCODE_F5 | SDLK_SCANCODE_MASK,
            SDLK_F6 = SDL_Scancode.SDL_SCANCODE_F6 | SDLK_SCANCODE_MASK,
            SDLK_F7 = SDL_Scancode.SDL_SCANCODE_F7 | SDLK_SCANCODE_MASK,
            SDLK_F8 = SDL_Scancode.SDL_SCANCODE_F8 | SDLK_SCANCODE_MASK,
            SDLK_F9 = SDL_Scancode.SDL_SCANCODE_F9 | SDLK_SCANCODE_MASK,
            SDLK_F10 = SDL_Scancode.SDL_SCANCODE_F10 | SDLK_SCANCODE_MASK,
            SDLK_F11 = SDL_Scancode.SDL_SCANCODE_F11 | SDLK_SCANCODE_MASK,
            SDLK_F12 = SDL_Scancode.SDL_SCANCODE_F12 | SDLK_SCANCODE_MASK,

            SDLK_PRINTSCREEN = SDL_Scancode.SDL_SCANCODE_PRINTSCREEN | SDLK_SCANCODE_MASK,
            SDLK_SCROLLLOCK = SDL_Scancode.SDL_SCANCODE_SCROLLLOCK | SDLK_SCANCODE_MASK,
            SDLK_PAUSE = SDL_Scancode.SDL_SCANCODE_PAUSE | SDLK_SCANCODE_MASK,
            SDLK_INSERT = SDL_Scancode.SDL_SCANCODE_INSERT | SDLK_SCANCODE_MASK,
            SDLK_HOME = SDL_Scancode.SDL_SCANCODE_HOME | SDLK_SCANCODE_MASK,
            SDLK_PAGEUP = SDL_Scancode.SDL_SCANCODE_PAGEUP | SDLK_SCANCODE_MASK,
            SDLK_DELETE = '\x7F',
            SDLK_END = SDL_Scancode.SDL_SCANCODE_END | SDLK_SCANCODE_MASK,
            SDLK_PAGEDOWN = SDL_Scancode.SDL_SCANCODE_PAGEDOWN | SDLK_SCANCODE_MASK,
            SDLK_RIGHT = SDL_Scancode.SDL_SCANCODE_RIGHT | SDLK_SCANCODE_MASK,
            SDLK_LEFT = SDL_Scancode.SDL_SCANCODE_LEFT | SDLK_SCANCODE_MASK,
            SDLK_DOWN = SDL_Scancode.SDL_SCANCODE_DOWN | SDLK_SCANCODE_MASK,
            SDLK_UP = SDL_Scancode.SDL_SCANCODE_UP | SDLK_SCANCODE_MASK,

            SDLK_NUMLOCKCLEAR = SDL_Scancode.SDL_SCANCODE_NUMLOCKCLEAR | SDLK_SCANCODE_MASK,
            SDLK_KP_DIVIDE = SDL_Scancode.SDL_SCANCODE_KP_DIVIDE | SDLK_SCANCODE_MASK,
            SDLK_KP_MULTIPLY = SDL_Scancode.SDL_SCANCODE_KP_MULTIPLY | SDLK_SCANCODE_MASK,
            SDLK_KP_MINUS = SDL_Scancode.SDL_SCANCODE_KP_MINUS | SDLK_SCANCODE_MASK,
            SDLK_KP_PLUS = SDL_Scancode.SDL_SCANCODE_KP_PLUS | SDLK_SCANCODE_MASK,
            SDLK_KP_ENTER = SDL_Scancode.SDL_SCANCODE_KP_ENTER | SDLK_SCANCODE_MASK,
            SDLK_KP_1 = SDL_Scancode.SDL_SCANCODE_KP_1 | SDLK_SCANCODE_MASK,
            SDLK_KP_2 = SDL_Scancode.SDL_SCANCODE_KP_2 | SDLK_SCANCODE_MASK,
            SDLK_KP_3 = SDL_Scancode.SDL_SCANCODE_KP_3 | SDLK_SCANCODE_MASK,
            SDLK_KP_4 = SDL_Scancode.SDL_SCANCODE_KP_4 | SDLK_SCANCODE_MASK,
            SDLK_KP_5 = SDL_Scancode.SDL_SCANCODE_KP_5 | SDLK_SCANCODE_MASK,
            SDLK_KP_6 = SDL_Scancode.SDL_SCANCODE_KP_6 | SDLK_SCANCODE_MASK,
            SDLK_KP_7 = SDL_Scancode.SDL_SCANCODE_KP_7 | SDLK_SCANCODE_MASK,
            SDLK_KP_8 = SDL_Scancode.SDL_SCANCODE_KP_8 | SDLK_SCANCODE_MASK,
            SDLK_KP_9 = SDL_Scancode.SDL_SCANCODE_KP_9 | SDLK_SCANCODE_MASK,
            SDLK_KP_0 = SDL_Scancode.SDL_SCANCODE_KP_0 | SDLK_SCANCODE_MASK,
            SDLK_KP_PERIOD = SDL_Scancode.SDL_SCANCODE_KP_PERIOD | SDLK_SCANCODE_MASK,

            SDLK_APPLICATION = SDL_Scancode.SDL_SCANCODE_APPLICATION | SDLK_SCANCODE_MASK,
            SDLK_POWER = SDL_Scancode.SDL_SCANCODE_POWER | SDLK_SCANCODE_MASK,
            SDLK_KP_EQUALS = SDL_Scancode.SDL_SCANCODE_KP_EQUALS | SDLK_SCANCODE_MASK,
            SDLK_F13 = SDL_Scancode.SDL_SCANCODE_F13 | SDLK_SCANCODE_MASK,
            SDLK_F14 = SDL_Scancode.SDL_SCANCODE_F14 | SDLK_SCANCODE_MASK,
            SDLK_F15 = SDL_Scancode.SDL_SCANCODE_F15 | SDLK_SCANCODE_MASK,
            SDLK_F16 = SDL_Scancode.SDL_SCANCODE_F16 | SDLK_SCANCODE_MASK,
            SDLK_F17 = SDL_Scancode.SDL_SCANCODE_F17 | SDLK_SCANCODE_MASK,
            SDLK_F18 = SDL_Scancode.SDL_SCANCODE_F18 | SDLK_SCANCODE_MASK,
            SDLK_F19 = SDL_Scancode.SDL_SCANCODE_F19 | SDLK_SCANCODE_MASK,
            SDLK_F20 = SDL_Scancode.SDL_SCANCODE_F20 | SDLK_SCANCODE_MASK,
            SDLK_F21 = SDL_Scancode.SDL_SCANCODE_F21 | SDLK_SCANCODE_MASK,
            SDLK_F22 = SDL_Scancode.SDL_SCANCODE_F22 | SDLK_SCANCODE_MASK,
            SDLK_F23 = SDL_Scancode.SDL_SCANCODE_F23 | SDLK_SCANCODE_MASK,
            SDLK_F24 = SDL_Scancode.SDL_SCANCODE_F24 | SDLK_SCANCODE_MASK,
            SDLK_EXECUTE = SDL_Scancode.SDL_SCANCODE_EXECUTE | SDLK_SCANCODE_MASK,
            SDLK_HELP = SDL_Scancode.SDL_SCANCODE_HELP | SDLK_SCANCODE_MASK,
            SDLK_MENU = SDL_Scancode.SDL_SCANCODE_MENU | SDLK_SCANCODE_MASK,
            SDLK_SELECT = SDL_Scancode.SDL_SCANCODE_SELECT | SDLK_SCANCODE_MASK,
            SDLK_STOP = SDL_Scancode.SDL_SCANCODE_STOP | SDLK_SCANCODE_MASK,
            SDLK_AGAIN = SDL_Scancode.SDL_SCANCODE_AGAIN | SDLK_SCANCODE_MASK,
            SDLK_UNDO = SDL_Scancode.SDL_SCANCODE_UNDO | SDLK_SCANCODE_MASK,
            SDLK_CUT = SDL_Scancode.SDL_SCANCODE_CUT | SDLK_SCANCODE_MASK,
            SDLK_COPY = SDL_Scancode.SDL_SCANCODE_COPY | SDLK_SCANCODE_MASK,
            SDLK_PASTE = SDL_Scancode.SDL_SCANCODE_PASTE | SDLK_SCANCODE_MASK,
            SDLK_FIND = SDL_Scancode.SDL_SCANCODE_FIND | SDLK_SCANCODE_MASK,
            SDLK_MUTE = SDL_Scancode.SDL_SCANCODE_MUTE | SDLK_SCANCODE_MASK,
            SDLK_VOLUMEUP = SDL_Scancode.SDL_SCANCODE_VOLUMEUP | SDLK_SCANCODE_MASK,
            SDLK_VOLUMEDOWN = SDL_Scancode.SDL_SCANCODE_VOLUMEDOWN | SDLK_SCANCODE_MASK,
            SDLK_KP_COMMA = SDL_Scancode.SDL_SCANCODE_KP_COMMA | SDLK_SCANCODE_MASK,
            SDLK_KP_EQUALSAS400 =
                SDL_Scancode.SDL_SCANCODE_KP_EQUALSAS400 | SDLK_SCANCODE_MASK,

            SDLK_ALTERASE = SDL_Scancode.SDL_SCANCODE_ALTERASE | SDLK_SCANCODE_MASK,
            SDLK_SYSREQ = SDL_Scancode.SDL_SCANCODE_SYSREQ | SDLK_SCANCODE_MASK,
            SDLK_CANCEL = SDL_Scancode.SDL_SCANCODE_CANCEL | SDLK_SCANCODE_MASK,
            SDLK_CLEAR = SDL_Scancode.SDL_SCANCODE_CLEAR | SDLK_SCANCODE_MASK,
            SDLK_PRIOR = SDL_Scancode.SDL_SCANCODE_PRIOR | SDLK_SCANCODE_MASK,
            SDLK_RETURN2 = SDL_Scancode.SDL_SCANCODE_RETURN2 | SDLK_SCANCODE_MASK,
            SDLK_SEPARATOR = SDL_Scancode.SDL_SCANCODE_SEPARATOR | SDLK_SCANCODE_MASK,
            SDLK_OUT = SDL_Scancode.SDL_SCANCODE_OUT | SDLK_SCANCODE_MASK,
            SDLK_OPER = SDL_Scancode.SDL_SCANCODE_OPER | SDLK_SCANCODE_MASK,
            SDLK_CLEARAGAIN = SDL_Scancode.SDL_SCANCODE_CLEARAGAIN | SDLK_SCANCODE_MASK,
            SDLK_CRSEL = SDL_Scancode.SDL_SCANCODE_CRSEL | SDLK_SCANCODE_MASK,
            SDLK_EXSEL = SDL_Scancode.SDL_SCANCODE_EXSEL | SDLK_SCANCODE_MASK,

            SDLK_KP_00 = SDL_Scancode.SDL_SCANCODE_KP_00 | SDLK_SCANCODE_MASK,
            SDLK_KP_000 = SDL_Scancode.SDL_SCANCODE_KP_000 | SDLK_SCANCODE_MASK,
            SDLK_THOUSANDSSEPARATOR =
                SDL_Scancode.SDL_SCANCODE_THOUSANDSSEPARATOR | SDLK_SCANCODE_MASK,
            SDLK_DECIMALSEPARATOR =
                SDL_Scancode.SDL_SCANCODE_DECIMALSEPARATOR | SDLK_SCANCODE_MASK,
            SDLK_CURRENCYUNIT = SDL_Scancode.SDL_SCANCODE_CURRENCYUNIT | SDLK_SCANCODE_MASK,
            SDLK_CURRENCYSUBUNIT =
                SDL_Scancode.SDL_SCANCODE_CURRENCYSUBUNIT | SDLK_SCANCODE_MASK,
            SDLK_KP_LEFTPAREN = SDL_Scancode.SDL_SCANCODE_KP_LEFTPAREN | SDLK_SCANCODE_MASK,
            SDLK_KP_RIGHTPAREN = SDL_Scancode.SDL_SCANCODE_KP_RIGHTPAREN | SDLK_SCANCODE_MASK,
            SDLK_KP_LEFTBRACE = SDL_Scancode.SDL_SCANCODE_KP_LEFTBRACE | SDLK_SCANCODE_MASK,
            SDLK_KP_RIGHTBRACE = SDL_Scancode.SDL_SCANCODE_KP_RIGHTBRACE | SDLK_SCANCODE_MASK,
            SDLK_KP_TAB = SDL_Scancode.SDL_SCANCODE_KP_TAB | SDLK_SCANCODE_MASK,
            SDLK_KP_BACKSPACE = SDL_Scancode.SDL_SCANCODE_KP_BACKSPACE | SDLK_SCANCODE_MASK,
            SDLK_KP_A = SDL_Scancode.SDL_SCANCODE_KP_A | SDLK_SCANCODE_MASK,
            SDLK_KP_B = SDL_Scancode.SDL_SCANCODE_KP_B | SDLK_SCANCODE_MASK,
            SDLK_KP_C = SDL_Scancode.SDL_SCANCODE_KP_C | SDLK_SCANCODE_MASK,
            SDLK_KP_D = SDL_Scancode.SDL_SCANCODE_KP_D | SDLK_SCANCODE_MASK,
            SDLK_KP_E = SDL_Scancode.SDL_SCANCODE_KP_E | SDLK_SCANCODE_MASK,
            SDLK_KP_F = SDL_Scancode.SDL_SCANCODE_KP_F | SDLK_SCANCODE_MASK,
            SDLK_KP_XOR = SDL_Scancode.SDL_SCANCODE_KP_XOR | SDLK_SCANCODE_MASK,
            SDLK_KP_POWER = SDL_Scancode.SDL_SCANCODE_KP_POWER | SDLK_SCANCODE_MASK,
            SDLK_KP_PERCENT = SDL_Scancode.SDL_SCANCODE_KP_PERCENT | SDLK_SCANCODE_MASK,
            SDLK_KP_LESS = SDL_Scancode.SDL_SCANCODE_KP_LESS | SDLK_SCANCODE_MASK,
            SDLK_KP_GREATER = SDL_Scancode.SDL_SCANCODE_KP_GREATER | SDLK_SCANCODE_MASK,
            SDLK_KP_AMPERSAND = SDL_Scancode.SDL_SCANCODE_KP_AMPERSAND | SDLK_SCANCODE_MASK,
            SDLK_KP_DBLAMPERSAND =
                SDL_Scancode.SDL_SCANCODE_KP_DBLAMPERSAND | SDLK_SCANCODE_MASK,
            SDLK_KP_VERTICALBAR =
                SDL_Scancode.SDL_SCANCODE_KP_VERTICALBAR | SDLK_SCANCODE_MASK,
            SDLK_KP_DBLVERTICALBAR =
                SDL_Scancode.SDL_SCANCODE_KP_DBLVERTICALBAR | SDLK_SCANCODE_MASK,
            SDLK_KP_COLON = SDL_Scancode.SDL_SCANCODE_KP_COLON | SDLK_SCANCODE_MASK,
            SDLK_KP_HASH = SDL_Scancode.SDL_SCANCODE_KP_HASH | SDLK_SCANCODE_MASK,
            SDLK_KP_SPACE = SDL_Scancode.SDL_SCANCODE_KP_SPACE | SDLK_SCANCODE_MASK,
            SDLK_KP_AT = SDL_Scancode.SDL_SCANCODE_KP_AT | SDLK_SCANCODE_MASK,
            SDLK_KP_EXCLAM = SDL_Scancode.SDL_SCANCODE_KP_EXCLAM | SDLK_SCANCODE_MASK,
            SDLK_KP_MEMSTORE = SDL_Scancode.SDL_SCANCODE_KP_MEMSTORE | SDLK_SCANCODE_MASK,
            SDLK_KP_MEMRECALL = SDL_Scancode.SDL_SCANCODE_KP_MEMRECALL | SDLK_SCANCODE_MASK,
            SDLK_KP_MEMCLEAR = SDL_Scancode.SDL_SCANCODE_KP_MEMCLEAR | SDLK_SCANCODE_MASK,
            SDLK_KP_MEMADD = SDL_Scancode.SDL_SCANCODE_KP_MEMADD | SDLK_SCANCODE_MASK,
            SDLK_KP_MEMSUBTRACT =
                SDL_Scancode.SDL_SCANCODE_KP_MEMSUBTRACT | SDLK_SCANCODE_MASK,
            SDLK_KP_MEMMULTIPLY =
                SDL_Scancode.SDL_SCANCODE_KP_MEMMULTIPLY | SDLK_SCANCODE_MASK,
            SDLK_KP_MEMDIVIDE = SDL_Scancode.SDL_SCANCODE_KP_MEMDIVIDE | SDLK_SCANCODE_MASK,
            SDLK_KP_PLUSMINUS = SDL_Scancode.SDL_SCANCODE_KP_PLUSMINUS | SDLK_SCANCODE_MASK,
            SDLK_KP_CLEAR = SDL_Scancode.SDL_SCANCODE_KP_CLEAR | SDLK_SCANCODE_MASK,
            SDLK_KP_CLEARENTRY = SDL_Scancode.SDL_SCANCODE_KP_CLEARENTRY | SDLK_SCANCODE_MASK,
            SDLK_KP_BINARY = SDL_Scancode.SDL_SCANCODE_KP_BINARY | SDLK_SCANCODE_MASK,
            SDLK_KP_OCTAL = SDL_Scancode.SDL_SCANCODE_KP_OCTAL | SDLK_SCANCODE_MASK,
            SDLK_KP_DECIMAL = SDL_Scancode.SDL_SCANCODE_KP_DECIMAL | SDLK_SCANCODE_MASK,
            SDLK_KP_HEXADECIMAL =
                SDL_Scancode.SDL_SCANCODE_KP_HEXADECIMAL | SDLK_SCANCODE_MASK,

            SDLK_LCTRL = SDL_Scancode.SDL_SCANCODE_LCTRL | SDLK_SCANCODE_MASK,
            SDLK_LSHIFT = SDL_Scancode.SDL_SCANCODE_LSHIFT | SDLK_SCANCODE_MASK,
            SDLK_LALT = SDL_Scancode.SDL_SCANCODE_LALT | SDLK_SCANCODE_MASK,
            SDLK_LGUI = SDL_Scancode.SDL_SCANCODE_LGUI | SDLK_SCANCODE_MASK,
            SDLK_RCTRL = SDL_Scancode.SDL_SCANCODE_RCTRL | SDLK_SCANCODE_MASK,
            SDLK_RSHIFT = SDL_Scancode.SDL_SCANCODE_RSHIFT | SDLK_SCANCODE_MASK,
            SDLK_RALT = SDL_Scancode.SDL_SCANCODE_RALT | SDLK_SCANCODE_MASK,
            SDLK_RGUI = SDL_Scancode.SDL_SCANCODE_RGUI | SDLK_SCANCODE_MASK,

            SDLK_MODE = SDL_Scancode.SDL_SCANCODE_MODE | SDLK_SCANCODE_MASK,

            SDLK_AUDIONEXT = SDL_Scancode.SDL_SCANCODE_AUDIONEXT | SDLK_SCANCODE_MASK,
            SDLK_AUDIOPREV = SDL_Scancode.SDL_SCANCODE_AUDIOPREV | SDLK_SCANCODE_MASK,
            SDLK_AUDIOSTOP = SDL_Scancode.SDL_SCANCODE_AUDIOSTOP | SDLK_SCANCODE_MASK,
            SDLK_AUDIOPLAY = SDL_Scancode.SDL_SCANCODE_AUDIOPLAY | SDLK_SCANCODE_MASK,
            SDLK_AUDIOMUTE = SDL_Scancode.SDL_SCANCODE_AUDIOMUTE | SDLK_SCANCODE_MASK,
            SDLK_MEDIASELECT = SDL_Scancode.SDL_SCANCODE_MEDIASELECT | SDLK_SCANCODE_MASK,
            SDLK_WWW = SDL_Scancode.SDL_SCANCODE_WWW | SDLK_SCANCODE_MASK,
            SDLK_MAIL = SDL_Scancode.SDL_SCANCODE_MAIL | SDLK_SCANCODE_MASK,
            SDLK_CALCULATOR = SDL_Scancode.SDL_SCANCODE_CALCULATOR | SDLK_SCANCODE_MASK,
            SDLK_COMPUTER = SDL_Scancode.SDL_SCANCODE_COMPUTER | SDLK_SCANCODE_MASK,
            SDLK_AC_SEARCH = SDL_Scancode.SDL_SCANCODE_AC_SEARCH | SDLK_SCANCODE_MASK,
            SDLK_AC_HOME = SDL_Scancode.SDL_SCANCODE_AC_HOME | SDLK_SCANCODE_MASK,
            SDLK_AC_BACK = SDL_Scancode.SDL_SCANCODE_AC_BACK | SDLK_SCANCODE_MASK,
            SDLK_AC_FORWARD = SDL_Scancode.SDL_SCANCODE_AC_FORWARD | SDLK_SCANCODE_MASK,
            SDLK_AC_STOP = SDL_Scancode.SDL_SCANCODE_AC_STOP | SDLK_SCANCODE_MASK,
            SDLK_AC_REFRESH = SDL_Scancode.SDL_SCANCODE_AC_REFRESH | SDLK_SCANCODE_MASK,
            SDLK_AC_BOOKMARKS = SDL_Scancode.SDL_SCANCODE_AC_BOOKMARKS | SDLK_SCANCODE_MASK,

            SDLK_BRIGHTNESSDOWN =
                SDL_Scancode.SDL_SCANCODE_BRIGHTNESSDOWN | SDLK_SCANCODE_MASK,
            SDLK_BRIGHTNESSUP = SDL_Scancode.SDL_SCANCODE_BRIGHTNESSUP | SDLK_SCANCODE_MASK,
            SDLK_DISPLAYSWITCH = SDL_Scancode.SDL_SCANCODE_DISPLAYSWITCH | SDLK_SCANCODE_MASK,
            SDLK_KBDILLUMTOGGLE =
                SDL_Scancode.SDL_SCANCODE_KBDILLUMTOGGLE | SDLK_SCANCODE_MASK,
            SDLK_KBDILLUMDOWN = SDL_Scancode.SDL_SCANCODE_KBDILLUMDOWN | SDLK_SCANCODE_MASK,
            SDLK_KBDILLUMUP = SDL_Scancode.SDL_SCANCODE_KBDILLUMUP | SDLK_SCANCODE_MASK,
            SDLK_EJECT = SDL_Scancode.SDL_SCANCODE_EJECT | SDLK_SCANCODE_MASK,
            SDLK_SLEEP = SDL_Scancode.SDL_SCANCODE_SLEEP | SDLK_SCANCODE_MASK,
            SDLK_APP1 = SDL_Scancode.SDL_SCANCODE_APP1 | SDLK_SCANCODE_MASK,
            SDLK_APP2 = SDL_Scancode.SDL_SCANCODE_APP2 | SDLK_SCANCODE_MASK,

            SDLK_AUDIOREWIND = SDL_Scancode.SDL_SCANCODE_AUDIOREWIND | SDLK_SCANCODE_MASK,
            SDLK_AUDIOFASTFORWARD = SDL_Scancode.SDL_SCANCODE_AUDIOFASTFORWARD | SDLK_SCANCODE_MASK,

            SDLK_SOFTLEFT = SDL_Scancode.SDL_SCANCODE_SOFTLEFT | SDLK_SCANCODE_MASK,
            SDLK_SOFTRIGHT = SDL_Scancode.SDL_SCANCODE_SOFTRIGHT | SDLK_SCANCODE_MASK,
            SDLK_CALL = SDL_Scancode.SDL_SCANCODE_CALL | SDLK_SCANCODE_MASK,
            SDLK_ENDCALL = SDL_Scancode.SDL_SCANCODE_ENDCALL | SDLK_SCANCODE_MASK
        }

        public enum SDL_Keymod
        {
            KMOD_NONE = 0x0000,
            KMOD_LSHIFT = 0x0001,
            KMOD_RSHIFT = 0x0002,
            KMOD_LCTRL = 0x0040,
            KMOD_RCTRL = 0x0080,
            KMOD_LALT = 0x0100,
            KMOD_RALT = 0x0200,
            KMOD_LGUI = 0x0400,
            KMOD_RGUI = 0x0800,
            KMOD_NUM = 0x1000,
            KMOD_CAPS = 0x2000,
            KMOD_MODE = 0x4000,
            KMOD_SCROLL = 0x8000,

            KMOD_CTRL = KMOD_LCTRL | KMOD_RCTRL,
            KMOD_SHIFT = KMOD_LSHIFT | KMOD_RSHIFT,
            KMOD_ALT = KMOD_LALT | KMOD_RALT,
            KMOD_GUI = KMOD_LGUI | KMOD_RGUI,
        }

        #endregion

        // SDL_loadso.h -- Should use platform assembly loading methods

        #region SDL_locale.h

        public struct SDL_Locale
        {
            public readonly byte* language;
            public readonly byte* country;
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Locale* SDL_GetPreferredLocales();

        #endregion

        #region SDL_log.h

        public enum SDL_LogCategory
        {
            SDL_LOG_CATEGORY_APPLICATION,
            SDL_LOG_CATEGORY_ERROR,
            SDL_LOG_CATEGORY_ASSERT,
            SDL_LOG_CATEGORY_SYSTEM,
            SDL_LOG_CATEGORY_AUDIO,
            SDL_LOG_CATEGORY_VIDEO,
            SDL_LOG_CATEGORY_RENDER,
            SDL_LOG_CATEGORY_INPUT,
            SDL_LOG_CATEGORY_TEST,

            SDL_LOG_CATEGORY_RESERVED1,
            SDL_LOG_CATEGORY_RESERVED2,
            SDL_LOG_CATEGORY_RESERVED3,
            SDL_LOG_CATEGORY_RESERVED4,
            SDL_LOG_CATEGORY_RESERVED5,
            SDL_LOG_CATEGORY_RESERVED6,
            SDL_LOG_CATEGORY_RESERVED7,
            SDL_LOG_CATEGORY_RESERVED8,
            SDL_LOG_CATEGORY_RESERVED9,
            SDL_LOG_CATEGORY_RESERVED10,

            SDL_LOG_CATEGORY_CUSTOM
        }

        public enum SDL_LogPriority
        {
            SDL_LOG_PRIORITY_VERBOSE = 1,
            SDL_LOG_PRIORITY_DEBUG,
            SDL_LOG_PRIORITY_INFO,
            SDL_LOG_PRIORITY_WARN,
            SDL_LOG_PRIORITY_ERROR,
            SDL_LOG_PRIORITY_CRITICAL,
            SDL_NUM_LOG_PRIORITIES
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LogSetAllPriority(SDL_LogPriority priority);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LogSetPriority(int category, SDL_LogPriority priority);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_LogPriority SDL_LogGetPriority(int category);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LogResetPriorities();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_Log(string fmt /*, ...*/);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_LogVerbose(int category, string fmt /*, ...*/);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_LogDebug(int category, string fmt /*, ...*/);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_LogInfo(int category, string fmt /*, ...*/);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_LogWarn(int category, string fmt /*, ...*/);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_LogError(int category, string fmt /*, ...*/);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_LogCritical(int category, string fmt /*, ...*/);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern void SDL_LogMessage(int category, SDL_LogPriority priority, string fmt /*, ...*/);

        // public static extern void SDL_LogMessageV(LogCategory category, LogPriority priority, string fmt, va_list ap);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LogGetOutputFunction(delegate* unmanaged[Cdecl]<nint, int, SDL_LogPriority, byte*, void>* callback, nint* userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LogSetOutputFunction(delegate* unmanaged[Cdecl]<nint, int, SDL_LogPriority, byte*, void> callback, nint userdata);

        #endregion

        // SDL_main.h -- Don't need to redefine main or change application class

        #region SDL_messagebox.h

        public enum SDL_MessageBoxFlags
        {
            SDL_MESSAGEBOX_ERROR = 0x00000010,
            SDL_MESSAGEBOX_WARNING = 0x00000020,
            SDL_MESSAGEBOX_INFORMATION = 0x00000040,
            SDL_MESSAGEBOX_BUTTONS_LEFT_TO_RIGHT = 0x00000080,
            SDL_MESSAGEBOX_BUTTONS_RIGHT_TO_LEFT = 0x00000100
        }

        public enum SDL_MessageBoxButtonFlags
        {
            SDL_MESSAGEBOX_BUTTON_RETURNKEY_DEFAULT = 0x00000001,
            SDL_MESSAGEBOX_BUTTON_ESCAPEKEY_DEFAULT = 0x00000002
        }

        public struct SDL_MessageBoxButtonData
        {
            public uint flags;
            public int buttonid;
            public byte* text;
        }

        public struct SDL_MessageBoxColor
        {
            public byte r, g, b;
        }

        public struct SDL_MessageBoxColorScheme
        {
            public SDL_MessageBoxColor background;
            public SDL_MessageBoxColor text;
            public SDL_MessageBoxColor button_border;
            public SDL_MessageBoxColor button_background;
            public SDL_MessageBoxColor button_selected;
        }

        public struct SDL_MessageBoxData
        {
            public uint flags;
            public SDL_Window* window;
            public byte* title;
            public byte* message;
            public int numbuttons;
            public SDL_MessageBoxButtonData* buttons;
            public SDL_MessageBoxColorScheme* colorScheme;
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_ShowMessageBox(SDL_MessageBoxData* messageboxdata, int* buttonid);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_ShowSimpleMessageBox(uint flags, byte* title, byte* message, SDL_Window* window);

        #endregion

        // SDL_metal.h -- macOS/iOS specific window routines

        #region SDL_misc.h

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_OpenURL(byte* url);

        #endregion

        #region SDL_mouse.h

        public readonly struct SDL_Cursor { }

        public enum SDL_SystemCursor
        {
            SDL_SYSTEM_CURSOR_ARROW,
            SDL_SYSTEM_CURSOR_IBEAM,
            SDL_SYSTEM_CURSOR_WAIT,
            SDL_SYSTEM_CURSOR_CROSSHAIR,
            SDL_SYSTEM_CURSOR_WAITARROW,
            SDL_SYSTEM_CURSOR_SIZENWSE,
            SDL_SYSTEM_CURSOR_SIZENESW,
            SDL_SYSTEM_CURSOR_SIZEWE,
            SDL_SYSTEM_CURSOR_SIZENS,
            SDL_SYSTEM_CURSOR_SIZEALL,
            SDL_SYSTEM_CURSOR_NO,
            SDL_SYSTEM_CURSOR_HAND,
            SDL_NUM_SYSTEM_CURSORS
        }

        public enum SDL_MouseWheelDirection
        {
            SDL_MOUSEWHEEL_NORMAL,
            SDL_MOUSEWHEEL_FLIPPED
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Window* SDL_GetMouseFocus();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_GetMouseState(int* x, int* y);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_GetGlobalMouseState(int* x, int* y);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_GetRelativeMouseState(int* x, int* y);

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
        public static extern SDL_Cursor* SDL_CreateSystemCursor(SDL_SystemCursor id);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetCursor(SDL_Cursor* cursor);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Cursor* SDL_GetCursor();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Cursor* SDL_GetDefaultCursor();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FreeCursor(SDL_Cursor* cursor);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_ShowCursor(int toggle);

        public const int SDL_BUTTON_LEFT = 1;
        public const int SDL_BUTTON_MIDDLE = 2;
        public const int SDL_BUTTON_RIGHT = 3;
        public const int SDL_BUTTON_X1 = 4;
        public const int SDL_BUTTON_X2 = 5;
        public const int SDL_BUTTON_LMASK = 1 << (SDL_BUTTON_LEFT - 1);
        public const int SDL_BUTTON_MMASK = 1 << (SDL_BUTTON_MIDDLE - 1);
        public const int SDL_BUTTON_RMASK = 1 << (SDL_BUTTON_RIGHT - 1);
        public const int SDL_BUTTON_X1MASK = 1 << (SDL_BUTTON_X1 - 1);
        public const int SDL_BUTTON_X2MASK = 1 << (SDL_BUTTON_X2 - 1);

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
            SDL_PIXELTYPE_UNKNOWN,
            SDL_PIXELTYPE_INDEX1,
            SDL_PIXELTYPE_INDEX4,
            SDL_PIXELTYPE_INDEX8,
            SDL_PIXELTYPE_PACKED8,
            SDL_PIXELTYPE_PACKED16,
            SDL_PIXELTYPE_PACKED32,
            SDL_PIXELTYPE_ARRAYU8,
            SDL_PIXELTYPE_ARRAYU16,
            SDL_PIXELTYPE_ARRAYU32,
            SDL_PIXELTYPE_ARRAYF16,
            SDL_PIXELTYPE_ARRAYF32
        };

        public enum SDL_BitmapOrder
        {
            SDL_BITMAPORDER_NONE,
            SDL_BITMAPORDER_4321,
            SDL_BITMAPORDER_1234
        };

        public enum SDL_PackedOrder
        {
            SDL_PACKEDORDER_NONE,
            SDL_PACKEDORDER_XRGB,
            SDL_PACKEDORDER_RGBX,
            SDL_PACKEDORDER_ARGB,
            SDL_PACKEDORDER_RGBA,
            SDL_PACKEDORDER_XBGR,
            SDL_PACKEDORDER_BGRX,
            SDL_PACKEDORDER_ABGR,
            SDL_PACKEDORDER_BGRA
        };

        public enum SDL_ArrayOrder
        {
            SDL_ARRAYORDER_NONE,
            SDL_ARRAYORDER_RGB,
            SDL_ARRAYORDER_RGBA,
            SDL_ARRAYORDER_ARGB,
            SDL_ARRAYORDER_BGR,
            SDL_ARRAYORDER_BGRA,
            SDL_ARRAYORDER_ABGR
        };

        public enum SDL_PackedLayout
        {
            SDL_PACKEDLAYOUT_NONE,
            SDL_PACKEDLAYOUT_332,
            SDL_PACKEDLAYOUT_4444,
            SDL_PACKEDLAYOUT_1555,
            SDL_PACKEDLAYOUT_5551,
            SDL_PACKEDLAYOUT_565,
            SDL_PACKEDLAYOUT_8888,
            SDL_PACKEDLAYOUT_2101010,
            SDL_PACKEDLAYOUT_1010102
        };

        public static uint SDL_DEFINE_PIXELFOURCC(char a, char b, char c, char d) =>
            ((uint)(((byte)a) << 0))
            | ((uint)(((byte)b) << 8))
            | ((uint)(((byte)c) << 16))
            | ((uint)(((byte)d) << 24));

        public static uint SDL_DEFINE_PIXELFORMAT(uint type, uint order, uint layout, uint bits, uint bytes) =>
                (1 << 28) | (type << 24) | (order << 20) | (layout << 16)
                | (bits << 8) | (bytes << 0);

        public static uint SDL_PIXELFLAG(uint x) => (x >> 28) & 0x0F;
        public static SDL_PixelType SDL_MAKEPIXELTYPE(uint x) => (SDL_PixelType)((x >> 24) & 0x0F);
        public static uint SDL_PIXELORDER(uint x) => (x >> 20) & 0x0F;
        public static uint SDL_PIXELLAYOUT(uint x) => (x >> 16) & 0x0F;
        public static uint SDL_BITSPERPIXEL(uint x) => (x >> 8) & 0xFF;

        public static uint SDL_BYTESPERPIXEL(uint x) => SDL_ISPIXELFORMAT_FOURCC(x) ? (((x == SDL_PixelFormatEnum.SDL_PIXELFORMAT_YUY2) || (x == SDL_PixelFormatEnum.SDL_PIXELFORMAT_UYVY) || (x == SDL_PixelFormatEnum.SDL_PIXELFORMAT_YVYU)) ? 2u : 1u) : ((x >> 0) & 0xFF);

        public static bool SDL_ISPIXELFORMAT_INDEXED(uint format) => !SDL_ISPIXELFORMAT_FOURCC(format) && ((SDL_MAKEPIXELTYPE(format) == SDL_PixelType.SDL_PIXELTYPE_INDEX1) || (SDL_MAKEPIXELTYPE(format) == SDL_PixelType.SDL_PIXELTYPE_INDEX4) || (SDL_MAKEPIXELTYPE(format) == SDL_PixelType.SDL_PIXELTYPE_INDEX8));

        public static bool SDL_ISPIXELFORMAT_PACKED(uint format) => !SDL_ISPIXELFORMAT_FOURCC(format) && ((SDL_MAKEPIXELTYPE(format) == SDL_PixelType.SDL_PIXELTYPE_PACKED8) || (SDL_MAKEPIXELTYPE(format) == SDL_PixelType.SDL_PIXELTYPE_PACKED16) || (SDL_MAKEPIXELTYPE(format) == SDL_PixelType.SDL_PIXELTYPE_PACKED32));

        public static bool SDL_ISPIXELFORMAT_ARRAY(uint format) => !SDL_ISPIXELFORMAT_FOURCC(format) && ((SDL_MAKEPIXELTYPE(format) == SDL_PixelType.SDL_PIXELTYPE_ARRAYU8) || (SDL_MAKEPIXELTYPE(format) == SDL_PixelType.SDL_PIXELTYPE_ARRAYU16) || (SDL_MAKEPIXELTYPE(format) == SDL_PixelType.SDL_PIXELTYPE_ARRAYU32) || (SDL_MAKEPIXELTYPE(format) == SDL_PixelType.SDL_PIXELTYPE_ARRAYF16) || (SDL_MAKEPIXELTYPE(format) == SDL_PixelType.SDL_PIXELTYPE_ARRAYF32));

        public static bool SDL_ISPIXELFORMAT_ALPHA(uint format) => (SDL_ISPIXELFORMAT_PACKED(format) && ((SDL_PIXELORDER(format) == (uint)SDL_PackedOrder.SDL_PACKEDORDER_ARGB) || (SDL_PIXELORDER(format) == (uint)SDL_PackedOrder.SDL_PACKEDORDER_RGBA) || (SDL_PIXELORDER(format) == (uint)SDL_PackedOrder.SDL_PACKEDORDER_ABGR) || (SDL_PIXELORDER(format) == (uint)SDL_PackedOrder.SDL_PACKEDORDER_BGRA))) || (SDL_ISPIXELFORMAT_ARRAY(format) && ((SDL_PIXELORDER(format) == (uint)SDL_ArrayOrder.SDL_ARRAYORDER_ARGB) || (SDL_PIXELORDER(format) == (uint)SDL_ArrayOrder.SDL_ARRAYORDER_RGBA) || (SDL_PIXELORDER(format) == (uint)SDL_ArrayOrder.SDL_ARRAYORDER_ABGR) || (SDL_PIXELORDER(format) == (uint)SDL_ArrayOrder.SDL_ARRAYORDER_BGRA)));

        public static bool SDL_ISPIXELFORMAT_FOURCC(uint format) => (format != 0) && (SDL_PIXELFLAG(format) != 1);

        public readonly record struct SDL_PixelFormatEnum(uint Value)
        {
            public static readonly uint SDL_PIXELFORMAT_UNKNOWN;
            public static readonly uint SDL_PIXELFORMAT_INDEX1LSB =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_INDEX1, (uint)SDL_BitmapOrder.SDL_BITMAPORDER_4321, 0,
                                       1, 0);
            public static readonly uint SDL_PIXELFORMAT_INDEX1MSB =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_INDEX1, (uint)SDL_BitmapOrder.SDL_BITMAPORDER_1234, 0,
                                       1, 0);
            public static readonly uint SDL_PIXELFORMAT_INDEX4LSB =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_INDEX4, (uint)SDL_BitmapOrder.SDL_BITMAPORDER_4321, 0,
                                       4, 0);
            public static readonly uint SDL_PIXELFORMAT_INDEX4MSB =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_INDEX4, (uint)SDL_BitmapOrder.SDL_BITMAPORDER_1234, 0,
                                       4, 0);
            public static readonly uint SDL_PIXELFORMAT_INDEX8 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_INDEX8, 0, 0, 8, 1);
            public static readonly uint SDL_PIXELFORMAT_RGB332 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED8, (uint)SDL_PackedOrder.SDL_PACKEDORDER_XRGB,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_332, 8, 1);
            public static readonly uint SDL_PIXELFORMAT_XRGB4444 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED16, (uint)SDL_PackedOrder.SDL_PACKEDORDER_XRGB,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_4444, 12, 2);
            public static readonly uint SDL_PIXELFORMAT_RGB444 = SDL_PIXELFORMAT_XRGB4444;
            public static readonly uint SDL_PIXELFORMAT_XBGR4444 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED16, (uint)SDL_PackedOrder.SDL_PACKEDORDER_XBGR,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_4444, 12, 2);
            public static readonly uint SDL_PIXELFORMAT_BGR444 = SDL_PIXELFORMAT_XBGR4444;
            public static readonly uint SDL_PIXELFORMAT_XRGB1555 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED16, (uint)SDL_PackedOrder.SDL_PACKEDORDER_XRGB,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_1555, 15, 2);
            public static readonly uint SDL_PIXELFORMAT_RGB555 = SDL_PIXELFORMAT_XRGB1555;
            public static readonly uint SDL_PIXELFORMAT_XBGR1555 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED16, (uint)SDL_PackedOrder.SDL_PACKEDORDER_XBGR,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_1555, 15, 2);
            public static readonly uint SDL_PIXELFORMAT_BGR555 = SDL_PIXELFORMAT_XBGR1555;
            public static readonly uint SDL_PIXELFORMAT_ARGB4444 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED16, (uint)SDL_PackedOrder.SDL_PACKEDORDER_ARGB,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_4444, 16, 2);
            public static readonly uint SDL_PIXELFORMAT_RGBA4444 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED16, (uint)SDL_PackedOrder.SDL_PACKEDORDER_RGBA,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_4444, 16, 2);
            public static readonly uint SDL_PIXELFORMAT_ABGR4444 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED16, (uint)SDL_PackedOrder.SDL_PACKEDORDER_ABGR,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_4444, 16, 2);
            public static readonly uint SDL_PIXELFORMAT_BGRA4444 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED16, (uint)SDL_PackedOrder.SDL_PACKEDORDER_BGRA,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_4444, 16, 2);
            public static readonly uint SDL_PIXELFORMAT_ARGB1555 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED16, (uint)SDL_PackedOrder.SDL_PACKEDORDER_ARGB,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_1555, 16, 2);
            public static readonly uint SDL_PIXELFORMAT_RGBA5551 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED16, (uint)SDL_PackedOrder.SDL_PACKEDORDER_RGBA,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_5551, 16, 2);
            public static readonly uint SDL_PIXELFORMAT_ABGR1555 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED16, (uint)SDL_PackedOrder.SDL_PACKEDORDER_ABGR,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_1555, 16, 2);
            public static readonly uint SDL_PIXELFORMAT_BGRA5551 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED16, (uint)SDL_PackedOrder.SDL_PACKEDORDER_BGRA,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_5551, 16, 2);
            public static readonly uint SDL_PIXELFORMAT_RGB565 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED16, (uint)SDL_PackedOrder.SDL_PACKEDORDER_XRGB,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_565, 16, 2);
            public static readonly uint SDL_PIXELFORMAT_BGR565 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED16, (uint)SDL_PackedOrder.SDL_PACKEDORDER_XBGR,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_565, 16, 2);
            public static readonly uint SDL_PIXELFORMAT_RGB24 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_ARRAYU8, (uint)SDL_ArrayOrder.SDL_ARRAYORDER_RGB, 0,
                                       24, 3);
            public static readonly uint SDL_PIXELFORMAT_BGR24 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_ARRAYU8, (uint)SDL_ArrayOrder.SDL_ARRAYORDER_BGR, 0,
                                       24, 3);
            public static readonly uint SDL_PIXELFORMAT_XRGB8888 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED32, (uint)SDL_PackedOrder.SDL_PACKEDORDER_XRGB,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_8888, 24, 4);
            public static readonly uint SDL_PIXELFORMAT_RGB888 = SDL_PIXELFORMAT_XRGB8888;
            public static readonly uint SDL_PIXELFORMAT_RGBX8888 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED32, (uint)SDL_PackedOrder.SDL_PACKEDORDER_RGBX,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_8888, 24, 4);
            public static readonly uint SDL_PIXELFORMAT_XBGR8888 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED32, (uint)SDL_PackedOrder.SDL_PACKEDORDER_XBGR,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_8888, 24, 4);
            public static readonly uint SDL_PIXELFORMAT_BGR888 = SDL_PIXELFORMAT_XBGR8888;
            public static readonly uint SDL_PIXELFORMAT_BGRX8888 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED32, (uint)SDL_PackedOrder.SDL_PACKEDORDER_BGRX,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_8888, 24, 4);
            public static readonly uint SDL_PIXELFORMAT_ARGB8888 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED32, (uint)SDL_PackedOrder.SDL_PACKEDORDER_ARGB,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_8888, 32, 4);
            public static readonly uint SDL_PIXELFORMAT_RGBA8888 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED32, (uint)SDL_PackedOrder.SDL_PACKEDORDER_RGBA,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_8888, 32, 4);
            public static readonly uint SDL_PIXELFORMAT_ABGR8888 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED32, (uint)SDL_PackedOrder.SDL_PACKEDORDER_ABGR,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_8888, 32, 4);
            public static readonly uint SDL_PIXELFORMAT_BGRA8888 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED32, (uint)SDL_PackedOrder.SDL_PACKEDORDER_BGRA,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_8888, 32, 4);
            public static readonly uint SDL_PIXELFORMAT_ARGB2101010 =
                SDL_DEFINE_PIXELFORMAT((uint)SDL_PixelType.SDL_PIXELTYPE_PACKED32, (uint)SDL_PackedOrder.SDL_PACKEDORDER_ARGB,
                                       (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_2101010, 32, 4);

            public static readonly uint SDL_PIXELFORMAT_RGBA32 = BitConverter.IsLittleEndian ? SDL_PIXELFORMAT_ABGR8888 : SDL_PIXELFORMAT_RGBA8888;
            public static readonly uint SDL_PIXELFORMAT_ARGB32 = BitConverter.IsLittleEndian ? SDL_PIXELFORMAT_BGRA8888 : SDL_PIXELFORMAT_ARGB8888;
            public static readonly uint SDL_PIXELFORMAT_BGRA32 = BitConverter.IsLittleEndian ? SDL_PIXELFORMAT_ARGB8888 : SDL_PIXELFORMAT_BGRA8888;
            public static readonly uint SDL_PIXELFORMAT_ABGR32 = BitConverter.IsLittleEndian ? SDL_PIXELFORMAT_RGBA8888 : SDL_PIXELFORMAT_ABGR8888;

            public static readonly uint SDL_PIXELFORMAT_YV12 =
                SDL_DEFINE_PIXELFOURCC('Y', 'V', '1', '2');
            public static readonly uint SDL_PIXELFORMAT_IYUV =
                SDL_DEFINE_PIXELFOURCC('I', 'Y', 'U', 'V');
            public static readonly uint SDL_PIXELFORMAT_YUY2 =
                SDL_DEFINE_PIXELFOURCC('Y', 'U', 'Y', '2');
            public static readonly uint SDL_PIXELFORMAT_UYVY =
                SDL_DEFINE_PIXELFOURCC('U', 'Y', 'V', 'Y');
            public static readonly uint SDL_PIXELFORMAT_YVYU =
                SDL_DEFINE_PIXELFOURCC('Y', 'V', 'Y', 'U');
            public static readonly uint SDL_PIXELFORMAT_NV12 =
                SDL_DEFINE_PIXELFOURCC('N', 'V', '1', '2');
            public static readonly uint SDL_PIXELFORMAT_NV21 =
                SDL_DEFINE_PIXELFOURCC('N', 'V', '2', '1');
            public static readonly uint SDL_PIXELFORMAT_EXTERNAL_OES =
                SDL_DEFINE_PIXELFOURCC('O', 'E', 'S', ' ');
        }

        public readonly record struct SDL_Color(byte r, byte g, byte b, byte a);

        public struct SDL_Palette
        {
            public int ncolors;
            public SDL_Color* colors;
            public uint version;
            public int refcount;
        }

        public struct SDL_PixelFormat
        {
            public uint format;
            public SDL_Palette* palette;
            public byte BitsPerPixel;
            public byte BytesPerPixel;
            private readonly byte _padding1;
            private readonly byte _padding2;
            public uint Rmask;
            public uint Gmask;
            public uint Bmask;
            public uint Amask;
            public byte Rloss;
            public byte Gloss;
            public byte Bloss;
            public byte Aloss;
            public byte Rshift;
            public byte Gshift;
            public byte Bshift;
            public byte Ashift;
            public int refcount;
            public SDL_PixelFormat* next;
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetPixelFormatName(uint format);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_PixelFormatEnumToMasks(uint format, int* bpp, uint* rmask, uint* gmask, uint* bmask, uint* amask);

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
        public static extern int SDL_SetPaletteColors(SDL_Palette* palette, SDL_Color* colors, int firstcolor, int ncolors);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FreePalette(SDL_Palette* palette);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_MapRGB(SDL_PixelFormat* format, byte r, byte g, byte b);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_MapRGBA(SDL_PixelFormat* format, byte r, byte g, byte b, byte a);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetRGB(uint pixel, SDL_PixelFormat* format, byte* r, byte* g, byte* b);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetRGBA(uint pixel, SDL_PixelFormat* format, byte* r, byte* g, byte* b, byte* a);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_CalculateGammaRamp(float gamma, ushort* ramp);

        #endregion

        #region SDL_platform.h

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetPlatform();

        #endregion

        #region SDL_power.h

        public enum SDL_PowerState
        {
            SDL_POWERSTATE_UNKNOWN,
            SDL_POWERSTATE_ON_BATTERY,
            SDL_POWERSTATE_NO_BATTERY,
            SDL_POWERSTATE_CHARGING,
            SDL_POWERSTATE_CHARGED
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_PowerState SDL_GetPowerInfo(int* secs, int* pct);

        #endregion

        // SDL_quit.h -- Defines

        #region SDL_rect.h

        public readonly record struct SDL_Point(int x, int y);

        public readonly record struct SDL_FPoint(float x, float y);

        public readonly record struct SDL_Rect(int x, int y, int w, int h);

        public readonly record struct SDL_FRect(float x, float y, float w, float h);

        public static bool SDL_PointInRect(SDL_Point* p, SDL_Rect* r) =>
            (p->x >= r->x) && (p->x < (r->x + r->w)) && (p->y >= r->y) && (p->y < (r->y + r->h));

        public static bool SDL_RectEmpty(SDL_Rect* r) =>
            (r == null) || (r->w <= 0) || (r->h <= 0);

        public static bool SDL_RectEquals(SDL_Rect* a, SDL_Rect* b) =>
            a != null && b != null && (a->x == b->x) && (a->y == b->y) && (a->w == b->w) && (a->h == b->h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasIntersection(SDL_Rect* a, SDL_Rect* b);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_IntersectRect(SDL_Rect* a, SDL_Rect* b, SDL_Rect* result);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_UnionRect(SDL_Rect* a, SDL_Rect* b, SDL_Rect* result);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_EnclosePoints(SDL_Point* points, int count, SDL_Rect* clip, SDL_Rect* result);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_IntersectRectAndLine(SDL_Rect* rect, int* x1, int* y1, int* x2, int* y2);

        public static bool SDL_PointInFRect(SDL_FPoint* p, SDL_FRect* r) =>
            (p->x >= r->x) && (p->x < (r->x + r->w)) && (p->y >= r->y) && (p->y < (r->y + r->h));

        public static bool SDL_FRectEmpty(SDL_FRect* r) =>
            (r == null) || (r->w <= 0.0f) || (r->h <= 0.0f);

        public static bool SDL_FRectEqualsEpsilon(SDL_FRect* a, SDL_FRect* b, float epsilon) =>
            a != null
            && b != null
            && ((a == b)
                || ((Math.Abs(a->x - b->x) <= epsilon)
                    && (Math.Abs(a->y - b->y) <= epsilon)
                    && (Math.Abs(a->w - b->w) <= epsilon)
                    && (Math.Abs(a->h - b->h) <= epsilon)));

        public static bool SDL_FRectEquals(SDL_FRect* a, SDL_FRect* b) => SDL_FRectEqualsEpsilon(a, b, 1.192092896e-07F);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasIntersectionF(SDL_FRect* a, SDL_FRect* b);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_IntersectFRect(SDL_FRect* a, SDL_FRect* b, SDL_FRect* result);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_UnionFRect(SDL_FRect* a, SDL_FRect* b, SDL_FRect* result);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_EncloseFPoints(SDL_FPoint* points, int count, SDL_FRect* clip, SDL_FRect* result);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_IntersectFRectAndLine(SDL_FRect* rect, float* x1, float* y1, float* x2, float* y2);

        #endregion

        #region SDL_render.h

        public enum SDL_RendererFlags
        {
            SDL_RENDERER_SOFTWARE = 0x00000001,
            SDL_RENDERER_ACCELERATED = 0x00000002,
            SDL_RENDERER_PRESENTVSYNC = 0x00000004,
            SDL_RENDERER_TARGETTEXTURE = 0x00000008
        }

        public struct SDL_RendererInfo
        {
            public byte* name;
            public uint flags;
            public uint num_texture_formats;
            public fixed uint texture_formats[16];
            public int max_texture_width;
            public int max_texture_height;
        }

        public struct SDL_Vertex
        {
            public SDL_FPoint position;
            public SDL_Color color;
            public SDL_FPoint tex_coord;
        }

        public enum SDL_ScaleMode
        {
            SDL_ScaleModeNearest,
            SDL_ScaleModeLinear,
            SDL_ScaleModeBest
        }

        public enum SDL_TextureAccess
        {
            SDL_TEXTUREACCESS_STATIC,
            SDL_TEXTUREACCESS_STREAMING,
            SDL_TEXTUREACCESS_TARGET
        }

        public enum SDL_TextureModulate
        {
            SDL_TEXTUREMODULATE_NONE = 0x00000000,
            SDL_TEXTUREMODULATE_COLOR = 0x00000001,
            SDL_TEXTUREMODULATE_ALPHA = 0x00000002
        }

        public enum SDL_RendererFlip
        {
            SDL_FLIP_NONE = 0x00000000,
            SDL_FLIP_HORIZONTAL = 0x00000001,
            SDL_FLIP_VERTICAL = 0x00000002
        }

        public readonly struct SDL_Renderer { }

        public readonly struct SDL_Texture { }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumRenderDrivers();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRenderDriverInfo(int index, SDL_RendererInfo* info);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_CreateWindowAndRenderer(int width, int height, uint window_flags, SDL_Window** window, SDL_Renderer** renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Renderer* SDL_CreateRenderer(SDL_Window* window, int index, uint flags);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Renderer* SDL_CreateSoftwareRenderer(SDL_Surface* surface);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Renderer* SDL_GetRenderer(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Window* SDL_GetRenderGetWindow(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRendererInfo(SDL_Renderer* renderer, SDL_RendererInfo* info);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRendererOutputSize(SDL_Renderer* renderer, int* w, int* h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Texture* SDL_CreateTexture(SDL_Renderer* renderer, uint format, int access, int w, int h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Texture* SDL_CreateTextureFromSurface(SDL_Renderer* renderer, SDL_Surface* surface);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_QueryTexture(SDL_Texture* texture, uint* format, int* access, int* w, int* h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetTextureColorMod(SDL_Texture* texture, byte r, byte g, byte b);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetTextureColorMod(SDL_Texture* texture, byte* r, byte* g, byte* b);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetTextureAlphaMod(SDL_Texture* texture, byte alpha);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetTextureAlphaMod(SDL_Texture* texture, byte* alpha);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetTextureBlendMode(SDL_Texture* texture, SDL_BlendMode blendMode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetTextureBlendMode(SDL_Texture* texture, SDL_BlendMode* blendMode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetTextureScaleMode(SDL_Texture* texture, SDL_ScaleMode scaleMode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetTextureScaleMode(SDL_Texture* texture, SDL_ScaleMode* scaleMode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetTextureUserData(SDL_Texture* texture, nint userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nint SDL_GetTextureUserData(SDL_Texture* texture);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_UpdateTexture(SDL_Texture* texture, SDL_Rect* rect, byte* pixels, int pitch);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_UpdateYUVTexture(SDL_Texture* texture, SDL_Rect* rect, byte* yplane, int ypitch, byte* uplane, int upitch, byte* vplane, int vpitch);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_UpdateNVTexture(SDL_Texture* texture, SDL_Rect* rect, byte* yplane, int ypitch, byte* uvplane, int uvpitch);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_LockTexture(SDL_Texture* texture, SDL_Rect* rect, byte** pixels, int* pitch);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_LockTextureToSurface(SDL_Texture* texture, SDL_Rect* rect, SDL_Surface** surface);

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
        public static extern void SDL_RenderGetLogicalSize(SDL_Renderer* renderer, int* w, int* h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderSetIntegerScale(SDL_Renderer* renderer, bool enable);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_RenderGetIntegerScale(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderSetViewport(SDL_Renderer* renderer, SDL_Rect* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RenderGetViewport(SDL_Renderer* renderer, SDL_Rect* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderSetClipRect(SDL_Renderer* renderer, SDL_Rect* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RenderGetClipRect(SDL_Renderer* renderer, SDL_Rect* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_RenderIsClipEnabled(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderSetScale(SDL_Renderer* renderer, float scaleX, float scaleY);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RenderGetScale(SDL_Renderer* renderer, float* scaleX, float* scaleY);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RenderWindowToLogical(SDL_Renderer* renderer, int windowX, int windowY, float* logicalX, float* logicalY);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RenderLogicalToWindow(SDL_Renderer* renderer, float logicalX, float logicalY, int* windowX, int* windowY);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetRenderDrawColor(SDL_Renderer* renderer, byte r, byte g, byte b, byte a);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRenderDrawColor(SDL_Renderer* renderer, byte* r, byte* g, byte* b, byte* a);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetRenderDrawBlendMode(SDL_Renderer* renderer, SDL_BlendMode blendMode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRenderDrawBlendMode(SDL_Renderer* renderer, SDL_BlendMode* blendMode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderClear(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawPoint(SDL_Renderer* renderer, int x, int y);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawPoints(SDL_Renderer* renderer, SDL_Point* points, int count);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawLine(SDL_Renderer* renderer, int x1, int y1, int x2, int y2);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawLines(SDL_Renderer* renderer, SDL_Point* points, int count);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawRect(SDL_Renderer* renderer, SDL_Rect* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawRects(SDL_Renderer* renderer, SDL_Rect* rects, int count);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFillRect(SDL_Renderer* renderer, SDL_Rect* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFillRects(SDL_Renderer* renderer, SDL_Rect* rects, int count);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopy(SDL_Renderer* renderer, SDL_Texture* texture, SDL_Rect* srcrect, SDL_Rect* dstrect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyEx(SDL_Renderer* renderer, SDL_Texture* texture, SDL_Rect* srcrect, SDL_Rect* dstrect, double angle, SDL_Point* center, SDL_RendererFlip flip);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawPointF(SDL_Renderer* renderer, float x, float y);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawPointsF(SDL_Renderer* renderer, SDL_FPoint* points, int count);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawLineF(SDL_Renderer* renderer, float x1, float y1, float x2, float y2);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawLinesF(SDL_Renderer* renderer, SDL_FPoint* points, int count);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawRectF(SDL_Renderer* renderer, SDL_FRect* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawRectsF(SDL_Renderer* renderer, SDL_FRect* rects, int count);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFillRectF(SDL_Renderer* renderer, SDL_FRect* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFillRectsF(SDL_Renderer* renderer, SDL_FRect* rects, int count);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyF(SDL_Renderer* renderer, SDL_Texture* texture, SDL_Rect* srcrect, SDL_FRect* dstrect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyExF(SDL_Renderer* renderer, SDL_Texture* texture, SDL_Rect* srcrect, SDL_FRect* dstrect, double angle, SDL_FPoint* center, SDL_RendererFlip flip);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderGeometry(SDL_Renderer* renderer, SDL_Texture* texture, SDL_Vertex* vertices, int num_verticies, int* indices, int num_indices);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderGeometryRaw(SDL_Renderer* renderer, SDL_Texture* texture, float* xy, int xy_stride, SDL_Color* color, int color_stride, float* uv, int uv_stride, int num_vertices, byte* indices, int num_indices, int size_indices);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderReadPixels(SDL_Renderer* renderer, SDL_Rect* rect, uint format, byte* pixels, int pitch);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RenderPresent(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_DestroyTexture(SDL_Texture* texture);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_DestroyRenderer(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFlush(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GL_BindTexture(SDL_Texture* texture, float* texw, float* texh);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GL_UnbindTexture(SDL_Texture* texture);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nint SDL_RenderGetMetalLayer(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nint SDL_RenderGetMetalCommandEncoder(SDL_Renderer* renderer);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderSetVSync(SDL_Renderer* renderer, int vsync);

        #endregion

        // SDL_revision.h -- Revision number

        #region SDL_rwops.h

        public const uint SDL_RWOPS_UNKNOWN = 0U;
        public const uint SDL_RWOPS_WINFILE = 1U;
        public const uint SDL_RWOPS_STDFILE = 2U;
        public const uint SDL_RWOPS_JNIFILE = 3U;
        public const uint SDL_RWOPS_MEMORY = 4U;
        public const uint SDL_RWOPS_MEMORY_RO = 5U;

        public struct SDL_RWops
        {
            public delegate* unmanaged[Cdecl]<SDL_RWops*, long> size;
            public delegate* unmanaged[Cdecl]<SDL_RWops*, long, int, long> seek;
            public delegate* unmanaged[Cdecl]<SDL_RWops*, byte*, nuint, nuint, nuint> read;
            public delegate* unmanaged[Cdecl]<SDL_RWops*, byte*, nuint, nuint, nuint> write;
            public delegate* unmanaged[Cdecl]<SDL_RWops*, int> close;

            public uint type;

            public byte* data1;
            public byte* data2;
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_RWops* SDL_RWFromFile(byte* file, byte* mode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_RWops* SDL_RWFromFP(void* fp, bool autoclose);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_RWops* SDL_RWFromMem(byte* mem, int size);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_RWops* SDL_RWFromConstMem(byte* mem, int size);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_RWops* SDL_AllocRW();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FreeRW(SDL_RWops* area);

        public const int RW_SEEK_SET = 0;
        public const int RW_SEEK_CUR = 1;
        public const int RW_SEEK_END = 2;

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern long SDL_RWsize(SDL_RWops* context);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern long SDL_RWseek(SDL_RWops* context, long offset, int whence);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern long SDL_RWtell(SDL_RWops* context);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nuint SDL_RWread(SDL_RWops* context, byte* buffer, nuint size, nuint maxnum);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nuint SDL_RWwrite(SDL_RWops* context, byte* buffer, nuint size, nuint num);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RWclose(SDL_RWops* context);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_LoadFile_RW(SDL_RWops* src, nuint* datasize, int freesrc);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_LoadFile(byte* file, nuint* datasize);

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

        public enum SDL_Scancode
        {
            SDL_SCANCODE_UNKNOWN = 0,

            SDL_SCANCODE_A = 4,
            SDL_SCANCODE_B = 5,
            SDL_SCANCODE_C = 6,
            SDL_SCANCODE_D = 7,
            SDL_SCANCODE_E = 8,
            SDL_SCANCODE_F = 9,
            SDL_SCANCODE_G = 10,
            SDL_SCANCODE_H = 11,
            SDL_SCANCODE_I = 12,
            SDL_SCANCODE_J = 13,
            SDL_SCANCODE_K = 14,
            SDL_SCANCODE_L = 15,
            SDL_SCANCODE_M = 16,
            SDL_SCANCODE_N = 17,
            SDL_SCANCODE_O = 18,
            SDL_SCANCODE_P = 19,
            SDL_SCANCODE_Q = 20,
            SDL_SCANCODE_R = 21,
            SDL_SCANCODE_S = 22,
            SDL_SCANCODE_T = 23,
            SDL_SCANCODE_U = 24,
            SDL_SCANCODE_V = 25,
            SDL_SCANCODE_W = 26,
            SDL_SCANCODE_X = 27,
            SDL_SCANCODE_Y = 28,
            SDL_SCANCODE_Z = 29,

            SDL_SCANCODE_1 = 30,
            SDL_SCANCODE_2 = 31,
            SDL_SCANCODE_3 = 32,
            SDL_SCANCODE_4 = 33,
            SDL_SCANCODE_5 = 34,
            SDL_SCANCODE_6 = 35,
            SDL_SCANCODE_7 = 36,
            SDL_SCANCODE_8 = 37,
            SDL_SCANCODE_9 = 38,
            SDL_SCANCODE_0 = 39,

            SDL_SCANCODE_RETURN = 40,
            SDL_SCANCODE_ESCAPE = 41,
            SDL_SCANCODE_BACKSPACE = 42,
            SDL_SCANCODE_TAB = 43,
            SDL_SCANCODE_SPACE = 44,

            SDL_SCANCODE_MINUS = 45,
            SDL_SCANCODE_EQUALS = 46,
            SDL_SCANCODE_LEFTBRACKET = 47,
            SDL_SCANCODE_RIGHTBRACKET = 48,
            SDL_SCANCODE_BACKSLASH = 49,
            SDL_SCANCODE_NONUSHASH = 50,
            SDL_SCANCODE_SEMICOLON = 51,
            SDL_SCANCODE_APOSTROPHE = 52,
            SDL_SCANCODE_GRAVE = 53,
            SDL_SCANCODE_COMMA = 54,
            SDL_SCANCODE_PERIOD = 55,
            SDL_SCANCODE_SLASH = 56,

            SDL_SCANCODE_CAPSLOCK = 57,

            SDL_SCANCODE_F1 = 58,
            SDL_SCANCODE_F2 = 59,
            SDL_SCANCODE_F3 = 60,
            SDL_SCANCODE_F4 = 61,
            SDL_SCANCODE_F5 = 62,
            SDL_SCANCODE_F6 = 63,
            SDL_SCANCODE_F7 = 64,
            SDL_SCANCODE_F8 = 65,
            SDL_SCANCODE_F9 = 66,
            SDL_SCANCODE_F10 = 67,
            SDL_SCANCODE_F11 = 68,
            SDL_SCANCODE_F12 = 69,

            SDL_SCANCODE_PRINTSCREEN = 70,
            SDL_SCANCODE_SCROLLLOCK = 71,
            SDL_SCANCODE_PAUSE = 72,
            SDL_SCANCODE_INSERT = 73,
            SDL_SCANCODE_HOME = 74,
            SDL_SCANCODE_PAGEUP = 75,
            SDL_SCANCODE_DELETE = 76,
            SDL_SCANCODE_END = 77,
            SDL_SCANCODE_PAGEDOWN = 78,
            SDL_SCANCODE_RIGHT = 79,
            SDL_SCANCODE_LEFT = 80,
            SDL_SCANCODE_DOWN = 81,
            SDL_SCANCODE_UP = 82,

            SDL_SCANCODE_NUMLOCKCLEAR = 83,
            SDL_SCANCODE_KP_DIVIDE = 84,
            SDL_SCANCODE_KP_MULTIPLY = 85,
            SDL_SCANCODE_KP_MINUS = 86,
            SDL_SCANCODE_KP_PLUS = 87,
            SDL_SCANCODE_KP_ENTER = 88,
            SDL_SCANCODE_KP_1 = 89,
            SDL_SCANCODE_KP_2 = 90,
            SDL_SCANCODE_KP_3 = 91,
            SDL_SCANCODE_KP_4 = 92,
            SDL_SCANCODE_KP_5 = 93,
            SDL_SCANCODE_KP_6 = 94,
            SDL_SCANCODE_KP_7 = 95,
            SDL_SCANCODE_KP_8 = 96,
            SDL_SCANCODE_KP_9 = 97,
            SDL_SCANCODE_KP_0 = 98,
            SDL_SCANCODE_KP_PERIOD = 99,

            SDL_SCANCODE_NONUSBACKSLASH = 100,
            SDL_SCANCODE_APPLICATION = 101,
            SDL_SCANCODE_POWER = 102,
            SDL_SCANCODE_KP_EQUALS = 103,
            SDL_SCANCODE_F13 = 104,
            SDL_SCANCODE_F14 = 105,
            SDL_SCANCODE_F15 = 106,
            SDL_SCANCODE_F16 = 107,
            SDL_SCANCODE_F17 = 108,
            SDL_SCANCODE_F18 = 109,
            SDL_SCANCODE_F19 = 110,
            SDL_SCANCODE_F20 = 111,
            SDL_SCANCODE_F21 = 112,
            SDL_SCANCODE_F22 = 113,
            SDL_SCANCODE_F23 = 114,
            SDL_SCANCODE_F24 = 115,
            SDL_SCANCODE_EXECUTE = 116,
            SDL_SCANCODE_HELP = 117,
            SDL_SCANCODE_MENU = 118,
            SDL_SCANCODE_SELECT = 119,
            SDL_SCANCODE_STOP = 120,
            SDL_SCANCODE_AGAIN = 121,
            SDL_SCANCODE_UNDO = 122,
            SDL_SCANCODE_CUT = 123,
            SDL_SCANCODE_COPY = 124,
            SDL_SCANCODE_PASTE = 125,
            SDL_SCANCODE_FIND = 126,
            SDL_SCANCODE_MUTE = 127,
            SDL_SCANCODE_VOLUMEUP = 128,
            SDL_SCANCODE_VOLUMEDOWN = 129,
            SDL_SCANCODE_KP_COMMA = 133,
            SDL_SCANCODE_KP_EQUALSAS400 = 134,

            SDL_SCANCODE_INTERNATIONAL1 = 135,
            SDL_SCANCODE_INTERNATIONAL2 = 136,
            SDL_SCANCODE_INTERNATIONAL3 = 137,
            SDL_SCANCODE_INTERNATIONAL4 = 138,
            SDL_SCANCODE_INTERNATIONAL5 = 139,
            SDL_SCANCODE_INTERNATIONAL6 = 140,
            SDL_SCANCODE_INTERNATIONAL7 = 141,
            SDL_SCANCODE_INTERNATIONAL8 = 142,
            SDL_SCANCODE_INTERNATIONAL9 = 143,
            SDL_SCANCODE_LANG1 = 144,
            SDL_SCANCODE_LANG2 = 145,
            SDL_SCANCODE_LANG3 = 146,
            SDL_SCANCODE_LANG4 = 147,
            SDL_SCANCODE_LANG5 = 148,
            SDL_SCANCODE_LANG6 = 149,
            SDL_SCANCODE_LANG7 = 150,
            SDL_SCANCODE_LANG8 = 151,
            SDL_SCANCODE_LANG9 = 152,

            SDL_SCANCODE_ALTERASE = 153,
            SDL_SCANCODE_SYSREQ = 154,
            SDL_SCANCODE_CANCEL = 155,
            SDL_SCANCODE_CLEAR = 156,
            SDL_SCANCODE_PRIOR = 157,
            SDL_SCANCODE_RETURN2 = 158,
            SDL_SCANCODE_SEPARATOR = 159,
            SDL_SCANCODE_OUT = 160,
            SDL_SCANCODE_OPER = 161,
            SDL_SCANCODE_CLEARAGAIN = 162,
            SDL_SCANCODE_CRSEL = 163,
            SDL_SCANCODE_EXSEL = 164,

            SDL_SCANCODE_KP_00 = 176,
            SDL_SCANCODE_KP_000 = 177,
            SDL_SCANCODE_THOUSANDSSEPARATOR = 178,
            SDL_SCANCODE_DECIMALSEPARATOR = 179,
            SDL_SCANCODE_CURRENCYUNIT = 180,
            SDL_SCANCODE_CURRENCYSUBUNIT = 181,
            SDL_SCANCODE_KP_LEFTPAREN = 182,
            SDL_SCANCODE_KP_RIGHTPAREN = 183,
            SDL_SCANCODE_KP_LEFTBRACE = 184,
            SDL_SCANCODE_KP_RIGHTBRACE = 185,
            SDL_SCANCODE_KP_TAB = 186,
            SDL_SCANCODE_KP_BACKSPACE = 187,
            SDL_SCANCODE_KP_A = 188,
            SDL_SCANCODE_KP_B = 189,
            SDL_SCANCODE_KP_C = 190,
            SDL_SCANCODE_KP_D = 191,
            SDL_SCANCODE_KP_E = 192,
            SDL_SCANCODE_KP_F = 193,
            SDL_SCANCODE_KP_XOR = 194,
            SDL_SCANCODE_KP_POWER = 195,
            SDL_SCANCODE_KP_PERCENT = 196,
            SDL_SCANCODE_KP_LESS = 197,
            SDL_SCANCODE_KP_GREATER = 198,
            SDL_SCANCODE_KP_AMPERSAND = 199,
            SDL_SCANCODE_KP_DBLAMPERSAND = 200,
            SDL_SCANCODE_KP_VERTICALBAR = 201,
            SDL_SCANCODE_KP_DBLVERTICALBAR = 202,
            SDL_SCANCODE_KP_COLON = 203,
            SDL_SCANCODE_KP_HASH = 204,
            SDL_SCANCODE_KP_SPACE = 205,
            SDL_SCANCODE_KP_AT = 206,
            SDL_SCANCODE_KP_EXCLAM = 207,
            SDL_SCANCODE_KP_MEMSTORE = 208,
            SDL_SCANCODE_KP_MEMRECALL = 209,
            SDL_SCANCODE_KP_MEMCLEAR = 210,
            SDL_SCANCODE_KP_MEMADD = 211,
            SDL_SCANCODE_KP_MEMSUBTRACT = 212,
            SDL_SCANCODE_KP_MEMMULTIPLY = 213,
            SDL_SCANCODE_KP_MEMDIVIDE = 214,
            SDL_SCANCODE_KP_PLUSMINUS = 215,
            SDL_SCANCODE_KP_CLEAR = 216,
            SDL_SCANCODE_KP_CLEARENTRY = 217,
            SDL_SCANCODE_KP_BINARY = 218,
            SDL_SCANCODE_KP_OCTAL = 219,
            SDL_SCANCODE_KP_DECIMAL = 220,
            SDL_SCANCODE_KP_HEXADECIMAL = 221,

            SDL_SCANCODE_LCTRL = 224,
            SDL_SCANCODE_LSHIFT = 225,
            SDL_SCANCODE_LALT = 226,
            SDL_SCANCODE_LGUI = 227,
            SDL_SCANCODE_RCTRL = 228,
            SDL_SCANCODE_RSHIFT = 229,
            SDL_SCANCODE_RALT = 230,
            SDL_SCANCODE_RGUI = 231,

            SDL_SCANCODE_MODE = 257,

            SDL_SCANCODE_AUDIONEXT = 258,
            SDL_SCANCODE_AUDIOPREV = 259,
            SDL_SCANCODE_AUDIOSTOP = 260,
            SDL_SCANCODE_AUDIOPLAY = 261,
            SDL_SCANCODE_AUDIOMUTE = 262,
            SDL_SCANCODE_MEDIASELECT = 263,
            SDL_SCANCODE_WWW = 264,
            SDL_SCANCODE_MAIL = 265,
            SDL_SCANCODE_CALCULATOR = 266,
            SDL_SCANCODE_COMPUTER = 267,
            SDL_SCANCODE_AC_SEARCH = 268,
            SDL_SCANCODE_AC_HOME = 269,
            SDL_SCANCODE_AC_BACK = 270,
            SDL_SCANCODE_AC_FORWARD = 271,
            SDL_SCANCODE_AC_STOP = 272,
            SDL_SCANCODE_AC_REFRESH = 273,
            SDL_SCANCODE_AC_BOOKMARKS = 274,

            SDL_SCANCODE_BRIGHTNESSDOWN = 275,
            SDL_SCANCODE_BRIGHTNESSUP = 276,
            SDL_SCANCODE_DISPLAYSWITCH = 277,
            SDL_SCANCODE_KBDILLUMTOGGLE = 278,
            SDL_SCANCODE_KBDILLUMDOWN = 279,
            SDL_SCANCODE_KBDILLUMUP = 280,
            SDL_SCANCODE_EJECT = 281,
            SDL_SCANCODE_SLEEP = 282,

            SDL_SCANCODE_APP1 = 283,
            SDL_SCANCODE_APP2 = 284,

            SDL_SCANCODE_AUDIOREWIND = 285,
            SDL_SCANCODE_AUDIOFASTFORWARD = 286,

            SDL_SCANCODE_SOFTLEFT = 287,
            SDL_SCANCODE_SOFTRIGHT = 288,
            SDL_SCANCODE_CALL = 289,
            SDL_SCANCODE_ENDCALL = 290,

            SDL_NUM_SCANCODES = 512
        }

        #endregion

        #region SDL_sensor.h

        public readonly struct SDL_Sensor { }

        public readonly record struct SDL_SensorID(int Value);

        public enum SDL_SensorType
        {
            SDL_SENSOR_INVALID = -1,
            SDL_SENSOR_UNKNOWN,
            SDL_SENSOR_ACCEL,
            SDL_SENSOR_GYRO,
            SDL_SENSOR_ACCEL_L,
            SDL_SENSOR_GYRO_L,
            SDL_SENSOR_ACCEL_R,
            SDL_SENSOR_GYRO_R
        }

        public const float SDL_STANDARD_GRAVITY = 9.80665f;

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LockSensors();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_UnlockSensors();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_NumSensors();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_SensorGetDeviceName(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_SensorType SDL_SensorGetDeviceType(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SensorGetDeviceNonPortableType(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_SensorID SDL_SensorGetDeviceInstanceID(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Sensor* SDL_SensorOpen(int device_index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Sensor* SDL_SensorFromInstanceID(SDL_SensorID instance_id);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_SensorGetName(SDL_Sensor* sensor);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_SensorType SDL_SensorGetType(SDL_Sensor* sensor);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SensorGetNonPortableType(SDL_Sensor* sensor);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_SensorID SDL_SensorGetInstanceID(SDL_Sensor* sensor);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SensorGetData(SDL_Sensor* sensor, float* data, int num_values);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SensorGetDataWithTimestamp(SDL_Sensor* sensor, ulong* timestamp, float* data, int num_values);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SensorClose(SDL_Sensor* sensor);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SensorUpdate();

        #endregion

        #region SDL_shape.h

        public const int SDL_NONSHAPEABLE_WINDOW = -1;
        public const int SDL_INVALID_SHAPE_ARGUMENT = -2;
        public const int SDL_WINDOW_LACKS_SHAPE = -3;

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Window* SDL_CreateShapedWindow(byte* title, uint x, uint y, uint w, uint h, uint flags);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_IsShapedWindow(SDL_Window* window);

        public enum WindowShapeMode
        {
            ShapeModeDefault,
            ShapeModeBinarizeAlpha,
            ShapeModeReverseBinarizeAlpha,
            ShapeModeColorKey
        }

        public static bool SDL_SHAPEMODEALPHA(WindowShapeMode mode) =>
            mode is WindowShapeMode.ShapeModeDefault
            or WindowShapeMode.ShapeModeBinarizeAlpha
            or WindowShapeMode.ShapeModeReverseBinarizeAlpha;

        [StructLayout(LayoutKind.Explicit)]
        public struct SDL_WindowShapeParams
        {
            [FieldOffset(0)]
            public byte binarizationCutoff;

            [FieldOffset(0)]
            public SDL_Color colorKey;
        }

        public struct SDL_WindowShapeMode
        {
            public WindowShapeMode mode;
            public SDL_WindowShapeParams parameters;
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowShape(SDL_Window* window, SDL_Surface* shape, SDL_WindowShapeMode* shape_mode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetShapedWindowMode(SDL_Window* window, SDL_WindowShapeMode* shape_mode);

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

        public const uint SDL_SWSURFACE = 0;
        public const uint SDL_PREALLOC = 0x00000001;
        public const uint SDL_RLEACCEL = 0x00000002;
        public const uint SDL_DONTFREE = 0x00000004;
        public const uint SDL_SIMD_ALIGNED = 0x00000008;

        public static bool SDL_MUSTLOCK(SDL_Surface* surface) => (surface->flags & SDL_RLEACCEL) != 0;

        public struct SDL_BlitMap { }

        public readonly struct SDL_Surface
        {
            public readonly uint flags;
            public readonly SDL_PixelFormat* format;
            public readonly int w, h;
            public readonly int pitch;
            public readonly byte* pixels;
            public readonly nint userdata;
            public readonly int locked;
            public readonly byte* list_blitmap;
            public readonly SDL_Rect clip_rect;
            public readonly SDL_BlitMap* map; // SDL_BlitMap*
            public readonly int refcount;
        }

        public enum SDL_YUV_CONVERSION_MODE
        {
            SDL_YUV_CONVERSION_JPEG,
            SDL_YUV_CONVERSION_BT601,
            SDL_YUV_CONVERSION_BT709,
            SDL_YUV_CONVERSION_AUTOMATIC
        }

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
        public static extern SDL_Surface* SDL_LoadBMP_RW(SDL_RWops* src, int freesrc);

        public static SDL_Surface* SDL_LoadBMP(byte* file)
        {
            fixed (byte* ptr = StringToUtf8("rb"))
            {
                return SDL_LoadBMP_RW(SDL_RWFromFile(file, ptr), BoolToInt(true));
            }
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SaveBMP_RW(SDL_Surface* surface, SDL_RWops* dst, int freedst);

        public static int SDL_SaveBMP(SDL_Surface* surface, byte* file)
        {
            fixed (byte* ptr = StringToUtf8("wb"))
            {
                return SDL_SaveBMP_RW(surface, SDL_RWFromFile(file, ptr), BoolToInt(true));
            }
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetSurfaceRLE(SDL_Surface* surface, int flag);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasSurfaceRLE(SDL_Surface* surface);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetColorKey(SDL_Surface* surface, int flag, uint key);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasColorKey(SDL_Surface* surface);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetColorKey(SDL_Surface* surface, uint* key);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetSurfaceColorMod(SDL_Surface* surface, byte r, byte g, byte b);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetSurfaceColorMod(SDL_Surface* surface, byte* r, byte* g, byte* b);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetSurfaceAlphaMod(SDL_Surface* surface, byte alpha);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetSurfaceAlphaMod(SDL_Surface* surface, byte* alpha);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetSurfaceBlendMode(SDL_Surface* surface, SDL_BlendMode blendMode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetSurfaceBlendMode(SDL_Surface* surface, SDL_BlendMode* blendMode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_SetClipRect(SDL_Surface* surface, SDL_Rect* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetClipRect(SDL_Surface* surface, SDL_Rect* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* SDL_DuplicateSurface(SDL_Surface* surface);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* SDL_ConvertSurface(SDL_Surface* src, SDL_PixelFormat* fmt, uint flags = 0);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* SDL_ConvertSurfaceFormat(SDL_Surface* src, uint pixel_format, uint flags = 0);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_ConvertPixels(int width, int height, uint src_format, byte* src, int src_pitch, uint dst_format, byte* dst, int dst_pitch);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_PremultiplyAlpha(int width, int height, uint src_format, byte* src, int src_pitch, uint dst_format, byte* dst, int dst_pitch);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_FillRect(SDL_Surface* dst, SDL_Rect* rect, uint color);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_FillRects(SDL_Surface* dst, SDL_Rect* rects, int count, uint color);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_BlitSurface(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_LowerBlit(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SoftStretch(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SoftStretchLinear(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_BlitScaled(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_LowerBlitScaled(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetYUVConversionMode(SDL_YUV_CONVERSION_MODE mode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_YUV_CONVERSION_MODE SDL_GetYUVConversionMode();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_YUV_CONVERSION_MODE SDL_GetYUVConversionModeForResolution(int width, int height);

        #endregion

        // SDL_system.h -- Not supporting low level Windows functions.

        // SDL_syswm -- Platform specific windows stuff.

        // SDL_thread.h -- Should use Framework threading primitives

        // SDL_timer.h -- Should use Framework timing primitives

        #region SDL_touch.h

        public readonly record struct SDL_TouchID(long Value);

        public readonly record struct SDL_FingerID(long Value);

        public enum SDL_TouchDeviceType
        {
            SDL_TOUCH_DEVICE_INVALID = -1,
            SDL_TOUCH_DEVICE_DIRECT,
            SDL_TOUCH_DEVICE_INDIRECT_ABSOLUTE,
            SDL_TOUCH_DEVICE_INDIRECT_RELATIVE
        }

        public struct SDL_Finger
        {
            public SDL_FingerID id;
            public float x;
            public float y;
            public float pressure;
        }

        public const uint SDL_TOUCH_MOUSEID = uint.MaxValue;

        public const long SDL_MOUSE_TOUCHID = -1;

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumTouchDevices();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_TouchID SDL_GetTouchDevice(int index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetTouchName(int index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_TouchDeviceType SDL_GetTouchDeviceType(SDL_TouchID touchID);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumTouchFingers(SDL_TouchID touchID);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Finger* SDL_GetTouchFinger(SDL_TouchID touchID, int index);

        #endregion

        #region SDL_version.h

        public readonly record struct SDL_version(byte major, byte minor, byte patch);

        public static readonly SDL_version IntegratedSdl2Version = new(2, 26, 0);

        public static int SDL_VersionNumber(SDL_version version) =>
            (version.major * 1000) + (version.minor * 100) + version.patch;

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetVersion(SDL_version* ver);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetRevision();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRevisionNumber();

        #endregion

        #region SDL_video.h

        public struct SDL_DisplayMode
        {
            public uint format;
            public int w;
            public int h;
            public int refresh_rate;
            public byte* driverdata;
        }

        public readonly struct SDL_Window { }

        public enum SDL_WindowFlags
        {
            SDL_WINDOW_FULLSCREEN = 0x00000001,
            SDL_WINDOW_OPENGL = 0x00000002,
            SDL_WINDOW_SHOWN = 0x00000004,
            SDL_WINDOW_HIDDEN = 0x00000008,
            SDL_WINDOW_BORDERLESS = 0x00000010,
            SDL_WINDOW_RESIZABLE = 0x00000020,
            SDL_WINDOW_MINIMIZED = 0x00000040,
            SDL_WINDOW_MAXIMIZED = 0x00000080,
            SDL_WINDOW_MOUSE_GRABBED = 0x00000100,
            SDL_WINDOW_INPUT_FOCUS = 0x00000200,
            SDL_WINDOW_MOUSE_FOCUS = 0x00000400,
            SDL_WINDOW_FULLSCREEN_DESKTOP = SDL_WINDOW_FULLSCREEN | 0x00001000,
            SDL_WINDOW_FOREIGN = 0x00000800,
            SDL_WINDOW_ALLOW_HIGHDPI = 0x00002000,
            SDL_WINDOW_MOUSE_CAPTURE = 0x00004000,
            SDL_WINDOW_ALWAYS_ON_TOP = 0x00008000,
            SDL_WINDOW_SKIP_TASKBAR = 0x00010000,
            SDL_WINDOW_UTILITY = 0x00020000,
            SDL_WINDOW_TOOLTIP = 0x00040000,
            SDL_WINDOW_POPUP_MENU = 0x00080000,
            SDL_WINDOW_KEYBOARD_GRABBED = 0x00100000,
            SDL_WINDOW_VULKAN = 0x10000000,
            SDL_WINDOW_METAL = 0x20000000,

            SDL_WINDOW_INPUT_GRABBED = SDL_WINDOW_MOUSE_GRABBED
        }

        public const uint SDL_WINDOWPOS_UNDEFINED_MASK = 0x1FFF0000u;
        public static uint SDL_WINDOWPOS_UNDEFINED_DISPLAY(uint X) => SDL_WINDOWPOS_UNDEFINED_MASK | (X);
        public static readonly uint SDL_WINDOWPOS_UNDEFINED = SDL_WINDOWPOS_UNDEFINED_DISPLAY(0);
        public static bool SDL_WINDOWPOS_ISUNDEFINED(uint X) => ((X) & 0xFFFF0000) == SDL_WINDOWPOS_UNDEFINED_MASK;

        public const uint SDL_WINDOWPOS_CENTERED_MASK = 0x2FFF0000u;
        public static uint SDL_WINDOWPOS_CENTERED_DISPLAY(uint X) => SDL_WINDOWPOS_CENTERED_MASK | (X);
        public static readonly uint SDL_WINDOWPOS_CENTERED = SDL_WINDOWPOS_CENTERED_DISPLAY(0);
        public static bool SDL_WINDOWPOS_ISCENTERED(uint X) => ((X) & 0xFFFF0000) == SDL_WINDOWPOS_CENTERED_MASK;

        public enum SDL_WindowEventID
        {
            SDL_WINDOWEVENT_NONE,
            SDL_WINDOWEVENT_SHOWN,
            SDL_WINDOWEVENT_HIDDEN,
            SDL_WINDOWEVENT_EXPOSED,
            SDL_WINDOWEVENT_MOVED,
            SDL_WINDOWEVENT_RESIZED,
            SDL_WINDOWEVENT_SIZE_CHANGED,
            SDL_WINDOWEVENT_MINIMIZED,
            SDL_WINDOWEVENT_MAXIMIZED,
            SDL_WINDOWEVENT_RESTORED,
            SDL_WINDOWEVENT_ENTER,
            SDL_WINDOWEVENT_LEAVE,
            SDL_WINDOWEVENT_FOCUS_GAINED,
            SDL_WINDOWEVENT_FOCUS_LOST,
            SDL_WINDOWEVENT_CLOSE,
            SDL_WINDOWEVENT_TAKE_FOCUS,
            SDL_WINDOWEVENT_HIT_TEST,
            SDL_WINDOWEVENT_ICCPROF_CHANGED,
            SDL_WINDOWEVENT_DISPLAY_CHANGED
        }

        public enum SDL_DisplayEventID
        {
            SDL_DISPLAYEVENT_NONE,
            SDL_DISPLAYEVENT_ORIENTATION,
            SDL_DISPLAYEVENT_CONNECTED,
            SDL_DISPLAYEVENT_DISCONNECTED
        }

        public enum SDL_DisplayOrientation
        {
            SDL_ORIENTATION_UNKNOWN,
            SDL_ORIENTATION_LANDSCAPE,
            SDL_ORIENTATION_LANDSCAPE_FLIPPED,
            SDL_ORIENTATION_PORTRAIT,
            SDL_ORIENTATION_PORTRAIT_FLIPPED
        }

        public enum SDL_FlashOperation
        {
            SDL_FLASH_CANCEL,
            SDL_FLASH_BRIEFLY,
            SDL_FLASH_UNTIL_FOCUSED
        }

        // SDL_GlContext
        // SDL_GLattr
        // SDL_GLprofile
        // SDL_GLcontextFlag
        // SDL_GLcontextReleaseFlag
        // SDL_GLContextResetNotification

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumVideoDrivers();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetVideoDriver(int index);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_VideoInit(byte* driver_name);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_VideoQuit();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetCurrentVideoDriver();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumVideoDisplays();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetDisplayName(int displayIndex);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetDisplayBounds(int displayIndex, SDL_Rect* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetDisplayUsableBounds(int displayIndex, SDL_Rect* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetDisplayDPI(int displayIndex, float* ddpi, float* hdpi, float* vdpi);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_DisplayOrientation SDL_GetDisplayOrientation(int displayIndex);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumDisplayModes(int displayIndex);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetDisplayMode(int displayIndex, int modeIndex, SDL_DisplayMode* mode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetDesktopDisplayMode(int displayIndex, SDL_DisplayMode* mode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetCurrentDisplayMode(int displayIndex, SDL_DisplayMode* mode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_DisplayMode* SDL_GetClosestDisplayMode(int displayIndex, SDL_DisplayMode* mode, SDL_DisplayMode* closest);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetPointDisplayIndex(SDL_Point* point);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRectDisplayIndex(SDL_Rect* point);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetWindowDisplayIndex(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowDisplayMode(SDL_Window* window, SDL_DisplayMode* mode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetWindowDisplayMode(SDL_Window* window, SDL_DisplayMode* mode);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetWindowICCProfile(SDL_Window* window, nuint* size);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_GetWindowPixelFormat(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Window* SDL_CreateWindow(byte* title, int x, int y, int w, int h, uint flags);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Window* SDL_CreateWindowFrom(void* data);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_GetWindowID(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Window* SDL_GetWindowFromID(uint id);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_GetWindowFlags(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowTitle(SDL_Window* window, byte* title);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* SDL_GetWindowTitle(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowIcon(SDL_Window* window, SDL_Surface* icon);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nint SDL_SetWindowData(SDL_Window* window, byte* name, nint userdata);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern nint SDL_GetWindowData(SDL_Window* window, byte* name);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowPosition(SDL_Window* window, int x, int y);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetWindowPosition(SDL_Window* window, int* x, int* y);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowSize(SDL_Window* window, int w, int h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetWindowSize(SDL_Window* window, int* w, int* h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetWindowBordersSize(SDL_Window* window, int* top, int* left, int* bottom, int* right);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetWindowSizeInPixels(SDL_Window* window, int* w, int* h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowMinimumSize(SDL_Window* window, int min_w, int min_h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetWindowMinimumSize(SDL_Window* window, int* w, int* h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowMaximumSize(SDL_Window* window, int max_w, int max_h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetWindowMaximumSize(SDL_Window* window, int* w, int* h);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowBordered(SDL_Window* window, bool bordered);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowResizable(SDL_Window* window, bool resizable);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowAlwaysOnTop(SDL_Window* window, bool on_top);

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
        public static extern int SDL_SetWindowFullscreen(SDL_Window* window, uint flags);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* SDL_GetWindowSurface(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_UpdateWindowSurface(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_UpdateWindowSurfaceRects(SDL_Window* window, SDL_Rect* rects, int numrects);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowGrab(SDL_Window* window, bool grabbed);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowKeyboardGrab(SDL_Window* window, bool grabbed);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowMouseGrab(SDL_Window* window, bool grabbed);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GetWindowGrab(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GetWindowKeyboardGrab(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_GetWindowMouseGrab(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Window* SDL_GetGrabbedWindow();

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowMouseRect(SDL_Window* window, SDL_Rect* rect);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Rect* SDL_GetWindowMouseRect(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowBrightness(SDL_Window* window, float brightness);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern float SDL_GetWindowBrightness(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowOpacity(SDL_Window* window, float opacity);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetWindowOpacity(SDL_Window* window, float* out_opacity);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowModalFor(SDL_Window* modal_window, SDL_Window* parent_window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowInputFocus(SDL_Window* window);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowGammaRamp(SDL_Window* window, ushort* red, ushort* green, ushort* blue);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetWindowGammaRamp(SDL_Window* window, ushort* red, ushort* green, ushort* blue);

        public enum SDL_HitTestResult
        {
            SDL_HITTEST_NORMAL,
            SDL_HITTEST_DRAGGABLE,
            SDL_HITTEST_RESIZE_TOPLEFT,
            SDL_HITTEST_RESIZE_TOP,
            SDL_HITTEST_RESIZE_TOPRIGHT,
            SDL_HITTEST_RESIZE_RIGHT,
            SDL_HITTEST_RESIZE_BOTTOMRIGHT,
            SDL_HITTEST_RESIZE_BOTTOM,
            SDL_HITTEST_RESIZE_BOTTOMLEFT,
            SDL_HITTEST_RESIZE_LEFT
        }

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetWindowHitTest(SDL_Window* window, delegate* unmanaged[Cdecl]<SDL_Window*, SDL_Point*, nint, SDL_HitTestResult> callback, nint callback_data);

        [DllImport(Sdl2, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FlashWindow(SDL_Window* window, SDL_FlashOperation operation);

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
        //public extern static int SDL_GL_GetAttribute(SDL_GLattr attr, int* value);
        //public extern static SDL_GLContext SDL_GL_CreateContext(SDL_Window* window);
        //public extern static int SDL_GL_MakeCurrent(SDL_Window* window, SDL_GLContext context);
        //public extern static nint SDL_GL_GetCurrentWindow();
        //public extern static SDL_GLContext SDL_GL_GetCurrentContext();
        //public extern static void SDL_GL_GetDrawableSize(SDL_Window* window, int* w, int* h);
        //public extern static int SDL_GL_SetSwapInterval(int interval);
        //public extern static int SDL_GL_GetSwapInterval();
        //public extern static void SDL_GL_SwapWindow(SDL_Window* window);
        //public extern static void SDL_GL_DeleteContext(SDL_GLContext context);

        #endregion

        // SDL_vulkan.h -- Not supporting OpenGL at this time

        #region SDL_image.h

        public static readonly Version IntegratedSdl2ImageVersion = new(2, 6, 2);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_version* IMG_Linked_Version();

        public enum IMG_InitFlags
        {
            IMG_INIT_JPG = 0x00000001,
            IMG_INIT_PNG = 0x00000002,
            IMG_INIT_TIF = 0x00000004,
            IMG_INIT_WEBP = 0x00000008,
            IMG_INIT_JXL = 0x00000010,
            IMG_INIT_AVIF = 0x00000020
        }

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IMG_Init(int flags);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern void IMG_Quit();

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadTyped_RW(SDL_RWops* src, int freesrc, byte* type);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_Load(byte* file);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_Load_RW(SDL_RWops* src, int freesrc);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Texture* IMG_LoadTexture(SDL_Renderer* renderer, byte* file);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Texture* IMG_LoadTexture_RW(SDL_Renderer* renderer, SDL_RWops* src, int freesrc);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Texture* IMG_LoadTextureTyped_RW(SDL_Renderer* renderer, SDL_RWops* src, int freesrc, byte* type);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IMG_isAVIF(SDL_RWops* src);

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
        public static extern bool IMG_isJXL(SDL_RWops* src);

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
        public static extern bool IMG_isQOI(SDL_RWops* src);

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
        public static extern SDL_Surface* IMG_LoadAVIF_RW(SDL_RWops* src);

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
        public static extern SDL_Surface* IMG_LoadJXL_RW(SDL_RWops* src);

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
        public static extern SDL_Surface* IMG_LoadQOI_RW(SDL_RWops* src);

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

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_LoadSizedSVG_RW(SDL_RWops* src, int width, int height);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_ReadXPMFromArray(byte** xpm);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Surface* IMG_ReadXPMFromArrayToRGB888(byte** xpm);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IMG_SavePNG(SDL_Surface* surface, byte* file);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IMG_SavePNG_RW(SDL_Surface* surface, SDL_RWops* dst, int freedst);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IMG_SaveJPG(SDL_Surface* surface, byte* file, int quality);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IMG_SaveJPG_RW(SDL_Surface* surface, SDL_RWops* dst, int freedst, int quality);

        public struct IMG_Animation
        {
            public int w, h;
            public int count;
            public SDL_Surface** frames;
            public int* delays;
        }

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern IMG_Animation* IMG_LoadAnimation(byte* file);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern IMG_Animation* IMG_LoadAnimation_RW(SDL_RWops* src, int freesrc);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern IMG_Animation* IMG_LoadAnimationTyped_RW(SDL_RWops* src, int freesrc, byte* type);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern void IMG_FreeAnimation(IMG_Animation* anim);

        [DllImport(Sdl2Image, CallingConvention = CallingConvention.Cdecl)]
        public static extern IMG_Animation* IMG_LoadGIFAnimation_RW(SDL_RWops* src);

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

        public static Mix_Chunk* Mix_LoadWAV(byte* file)
        {
            fixed (byte* ptr = StringToUtf8("rb"))
            {
                return Mix_LoadWAV_RW(SDL_RWFromFile(file, ptr), true);
            }
        }

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
