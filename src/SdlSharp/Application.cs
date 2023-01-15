using SdlSharp.Graphics;
using SdlSharp.Input;
using SdlSharp.Sound;

namespace SdlSharp
{
    /// <summary>
    /// A class representing the SDL application.
    /// </summary>
    public sealed unsafe class Application : IDisposable
    {
        private bool _quitReceived;

        private readonly bool _initializedImage;
        private readonly bool _initializedMixer;
        private readonly bool _initializedFont;

        /// <summary>
        /// The version of SDL that is being used.
        /// </summary>
        public static Version Version
        {
            get
            {
                Native.SDL_version version;
                Native.SDL_GetVersion(&version);
                return new Version(&version);
            }
        }

        /// <summary>
        /// The revision string of the version of SDL that's being used.
        /// </summary>
        public static string Revision =>
            Native.Utf8ToString(Native.SDL_GetRevision())!;

        /// <summary>
        /// The revision number of the version of SDL that's being used.
        /// </summary>
        public static int RevisionNumber =>
            Native.SDL_GetRevisionNumber();

        /// <summary>
        /// The SDL subsystems that have been initialized.
        /// </summary>
        public Subsystems InitializedSubystems
        {
            get => (Subsystems)Native.SDL_WasInit((uint)Subsystems.None);
            set
            {
                var current = InitializedSubystems;

                Native.SDL_QuitSubSystem((uint)(current & ~value));
                _ = Native.CheckError(Native.SDL_InitSubSystem((uint)(value & ~current)));
            }
        }

        /// <summary>
        /// The version of SDL_image being used.
        /// </summary>
        public static Version ImageVersion =>
            new(Native.IMG_Linked_Version());

        /// <summary>
        /// The version of SDL_mixer that is being used.
        /// </summary>
        public static Version MixerVersion =>
            new(Native.Mix_Linked_Version());

        /// <summary>
        /// The version of SDL_ttf in use.
        /// </summary>
        public static Version FontVersion =>
            new(Native.TTF_Linked_Version());

        /// <summary>
        /// The version of FreeType used by SDL_ttf.
        /// </summary>
        public static Version FreeTypeVersion
        {
            get
            {
                int major, minor, patch;
                Native.TTF_GetFreeTypeVersion(&major, &minor, &patch);
                return new(major, minor, patch);
            }
        }

        /// <summary>
        /// The version of HarfBuzz used by SDL_ttf.
        /// </summary>
        public static Version HarfBuzzVersion
        {
            get
            {
                int major, minor, patch;
                Native.TTF_GetHarfBuzzVersion(&major, &minor, &patch);
                return new(major, minor, patch);
            }
        }

        /// <summary>
        /// Whether the font subsystem is initialized.
        /// </summary>
        public static bool FontIsInitialized =>
            Native.TTF_WasInit();

        /// <summary>
        /// The platform the application is running on.
        /// </summary>
        public static string Platform =>
            Native.Utf8ToString(Native.SDL_GetPlatform())!;

        /// <summary>
        /// The base path of the application.
        /// </summary>
        public static string BasePath => Native.Utf8ToStringAndFree(Native.CheckPointer(Native.SDL_GetBasePath()))!;

        /// <summary>
        /// Preferred locales, in order of preference.
        /// </summary>
        public static IReadOnlyList<(string? Language, string? Country)> PreferredLocales
        {
            get
            {
                var locales = new List<(string? Language, string? Country)>();
                var locale = Native.CheckPointer(Native.SDL_GetPreferredLocales());
                var current = locale;
                while (current->language != null || current->country != null)
                {
                    locales.Add((Native.Utf8ToString(current->language), Native.Utf8ToString(current->country)));
                    current++;
                }
                Native.SDL_free(locale);
                return locales;
            }
        }

        /// <summary>
        /// Information about the power state of the computer.
        /// </summary>
        public static (PowerState State, int Seconds, int Percent) PowerInfo
        {
            get
            {
                int seconds, percent;
                var state = Native.SDL_GetPowerInfo(&seconds, &percent);
                return ((PowerState)state, seconds, percent);
            }
        }

        /// <summary>
        /// Starts the application with the specified capabilities.
        /// </summary>
        /// <param name="subsystems">The subsystems to initialize.</param>
        /// <param name="imageFormats">The image formats to initialize.</param>
        /// <param name="mixerFormats">The mixer formats to initialize.</param>
        /// <param name="fontSupport">Whether font support should be initialized.</param>
        /// <param name="hints">Hints.</param>
        public Application(Subsystems subsystems, ImageFormats imageFormats = ImageFormats.None, MixerFormats mixerFormats = MixerFormats.None, bool fontSupport = false, params (Hint Hint, string Value)[] hints)
        {
            _ = Native.CheckError(Native.SDL_Init((uint)subsystems));

            if (hints != null)
            {
                foreach (var hint in hints)
                {
                    _ = hint.Hint.Set(hint.Value);
                }
            }

            if (imageFormats != ImageFormats.None)
            {
                _ = Native.CheckError(Native.IMG_Init((int)imageFormats));
                _initializedImage = true;
            }

            if (mixerFormats != MixerFormats.None)
            {
                _ = Native.CheckError(Native.Mix_Init((int)mixerFormats));
                _initializedMixer = true;
            }

            if (fontSupport)
            {
                _ = Native.CheckError(Native.TTF_Init());
                _initializedFont = true;
            }
        }

        /// <summary>
        /// An event that fires when the locale changed.
        /// </summary>
        public event EventHandler<SdlEventArgs>? LocaleChanged;

        /// <summary>
        /// An event that fires when a drop begins.
        /// </summary>
        public event EventHandler<DropEventArgs>? DropBegin;

        /// <summary>
        /// An event that fires when a drop completes.
        /// </summary>
        public event EventHandler<DropEventArgs>? DropComplete;

        /// <summary>
        /// An event that fires when a file is dropped.
        /// </summary>
        public event EventHandler<DroppedEventArgs>? FileDropped;

        /// <summary>
        /// An event that fires when text is dropped.
        /// </summary>
        public event EventHandler<DroppedEventArgs>? TextDropped;

        /// <summary>
        /// An event that fires when the application is quit.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Quitting;

        /// <summary>
        /// An event that fires when the application is terminating.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Terminating;

        /// <summary>
        /// An event that fires when the application is low on memory.
        /// </summary>
        public event EventHandler<SdlEventArgs>? LowMemory;

        /// <summary>
        /// An event that fires when the application is going to enter the background.
        /// </summary>
        public event EventHandler<SdlEventArgs>? WillEnterBackground;

        /// <summary>
        /// An event that fires when the application entered the background.
        /// </summary>
        public event EventHandler<SdlEventArgs>? DidEnterBackground;

        /// <summary>
        /// An event that fires when the application is going to enter the foreground.
        /// </summary>
        public event EventHandler<SdlEventArgs>? WillEnterForeground;

        /// <summary>
        /// An event that fires when the application entered the foreground.
        /// </summary>
        public event EventHandler<SdlEventArgs>? DidEnterForeground;

        /// <inheritdoc/>
        public void Dispose()
        {
            if (_initializedFont)
            {
                Native.TTF_Quit();
            }

            if (_initializedMixer)
            {
                Native.Mix_Quit();
            }

            if (_initializedImage)
            {
                Native.IMG_Quit();
            }

            Native.SDL_Quit();
            _quitReceived = true;
        }

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="flags">The flags.</param>
        /// <param name="window">The parent window, if any.</param>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="colorScheme">The color scheme.</param>
        /// <returns>The ID of the button that was selected.</returns>
        public static int ShowMessageBox(MessageBoxType flags, Window? window, string title, string message, MessageBoxButton[] buttons, MessageBoxColorScheme? colorScheme)
        {
            var nativeButtons = new Native.SDL_MessageBoxButtonData[buttons.Length];

            for (var index = 0; index < buttons.Length; index++)
            {
                var name = Native.StringToUtf8(buttons[index].Text);
                var nameBuffer = (byte*)Native.SDL_malloc((nuint)name.Length);
                name.CopyTo(new Span<byte>(nameBuffer, name.Length));

                nativeButtons[index] = new Native.SDL_MessageBoxButtonData
                {
                    flags = (uint)buttons[index].Flags,
                    buttonid = buttons[index].ButtonId,
                    text = nameBuffer
                };
            }

            var nativeColorScheme = colorScheme.GetValueOrDefault().ToNative();
            int buttonId;

            fixed (byte* titlePtr = Native.StringToUtf8(title))
            fixed (byte* messagePtr = Native.StringToUtf8(message))
            fixed (Native.SDL_MessageBoxButtonData* buttonBuffer = nativeButtons)
            {
                var messageBoxData = new Native.SDL_MessageBoxData
                {
                    flags = (uint)flags,
                    window = window == null ? null : window.ToNative(),
                    title = titlePtr,
                    message = messagePtr,
                    numbuttons = buttons.Length,
                    buttons = buttonBuffer,
                    colorScheme = colorScheme == null ? null : &nativeColorScheme
                };
                _ = Native.CheckError(Native.SDL_ShowMessageBox(&messageBoxData, &buttonId));
            }

            foreach (var nativeButton in nativeButtons)
            {
                Native.SDL_free(nativeButton.text);
            }

            return buttonId;
        }

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="flags">The flags.</param>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="window">The parent window, if any.</param>
        public static void ShowMessageBox(MessageBoxType flags, string title, string message, Window? window) =>
            Native.StringToUtf8Action(title, message, (titlePtr, messagePtr) => _ = Native.CheckError(Native.SDL_ShowSimpleMessageBox((uint)flags, titlePtr, messagePtr, window == null ? null : window.ToNative())));

        /// <summary>
        /// Get the path to store user preferences.
        /// </summary>
        /// <param name="organization">Organization name.</param>
        /// <param name="application">Application name.</param>
        /// <returns>The path to store preferences.</returns>
        public static string GetPreferencesPath(string organization, string application) =>
            Native.StringToUtf8Func(organization, application, (organizationPtr, applicationPtr) => Native.Utf8ToStringAndFree(Native.SDL_GetPrefPath(organizationPtr, applicationPtr))!);

        /// <summary>
        /// Opens a URL, if possible.
        /// </summary>
        /// <param name="url">The URL to open.</param>
        public static void OpenUrl(string url) => Native.StringToUtf8Action(url, urlPtr => _ = Native.CheckError(Native.SDL_OpenURL(urlPtr)));

        private void DispatchEvent(ref Native.SDL_Event e)
        {
            switch ((Native.SDL_EventType)e.type)
            {
                case Native.SDL_EventType.SDL_QUIT:
                    Quitting?.Invoke(null, new SdlEventArgs(e.common));
                    _quitReceived = true;
                    break;

                case Native.SDL_EventType.SDL_APP_TERMINATING:
                    Terminating?.Invoke(null, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_EventType.SDL_APP_LOWMEMORY:
                    LowMemory?.Invoke(null, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_EventType.SDL_APP_WILLENTERBACKGROUND:
                    WillEnterBackground?.Invoke(null, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_EventType.SDL_APP_DIDENTERBACKGROUND:
                    DidEnterBackground?.Invoke(null, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_EventType.SDL_APP_WILLENTERFOREGROUND:
                    WillEnterForeground?.Invoke(null, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_EventType.SDL_APP_DIDENTERFOREGROUND:
                    DidEnterForeground?.Invoke(null, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_EventType.SDL_LOCALECHANGED:
                    LocaleChanged?.Invoke(null, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_EventType.SDL_DISPLAYEVENT:
                    Display.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.SDL_WINDOWEVENT:
                case Native.SDL_EventType.SDL_SYSWMEVENT:
                    Window.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.SDL_KEYUP:
                case Native.SDL_EventType.SDL_KEYDOWN:
                case Native.SDL_EventType.SDL_TEXTEDITING:
                case Native.SDL_EventType.SDL_TEXTINPUT:
                case Native.SDL_EventType.SDL_KEYMAPCHANGED:
                case Native.SDL_EventType.SDL_TEXTEDITING_EXT:
                    Keyboard.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                case Native.SDL_EventType.SDL_MOUSEBUTTONUP:
                case Native.SDL_EventType.SDL_MOUSEMOTION:
                case Native.SDL_EventType.SDL_MOUSEWHEEL:
                    Mouse.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.SDL_JOYAXISMOTION:
                case Native.SDL_EventType.SDL_JOYBALLMOTION:
                case Native.SDL_EventType.SDL_JOYHATMOTION:
                case Native.SDL_EventType.SDL_JOYBUTTONDOWN:
                case Native.SDL_EventType.SDL_JOYBUTTONUP:
                case Native.SDL_EventType.SDL_JOYDEVICEADDED:
                case Native.SDL_EventType.SDL_JOYDEVICEREMOVED:
                case Native.SDL_EventType.SDL_JOYBATTERYUPDATED:
                    Joystick.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.SDL_CONTROLLERAXISMOTION:
                case Native.SDL_EventType.SDL_CONTROLLERBUTTONDOWN:
                case Native.SDL_EventType.SDL_CONTROLLERBUTTONUP:
                case Native.SDL_EventType.SDL_CONTROLLERDEVICEADDED:
                case Native.SDL_EventType.SDL_CONTROLLERDEVICEREMAPPED:
                case Native.SDL_EventType.SDL_CONTROLLERDEVICEREMOVED:
                case Native.SDL_EventType.SDL_CONTROLLERTOUCHPADDOWN:
                case Native.SDL_EventType.SDL_CONTROLLERTOUCHPADMOTION:
                case Native.SDL_EventType.SDL_CONTROLLERTOUCHPADUP:
                case Native.SDL_EventType.SDL_CONTROLLERSENSORUPDATE:
                    GameController.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.SDL_FINGERDOWN:
                case Native.SDL_EventType.SDL_FINGERUP:
                case Native.SDL_EventType.SDL_FINGERMOTION:
                    TouchDevice.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.SDL_DOLLARGESTURE:
                case Native.SDL_EventType.SDL_DOLLARRECORD:
                case Native.SDL_EventType.SDL_MULTIGESTURE:
                    Gesture.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.SDL_CLIPBOARDUPDATE:
                    Clipboard.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.SDL_DROPFILE:
                    FileDropped?.Invoke(null, new DroppedEventArgs(e.drop));
                    break;

                case Native.SDL_EventType.SDL_DROPTEXT:
                    TextDropped?.Invoke(null, new DroppedEventArgs(e.drop));
                    break;

                case Native.SDL_EventType.SDL_DROPBEGIN:
                    DropBegin?.Invoke(null, new DropEventArgs(e.drop));
                    break;

                case Native.SDL_EventType.SDL_DROPCOMPLETE:
                    DropComplete?.Invoke(null, new DropEventArgs(e.drop));
                    break;

                case Native.SDL_EventType.SDL_AUDIODEVICEADDED:
                case Native.SDL_EventType.SDL_AUDIODEVICEREMOVED:
                    Audio.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.SDL_SENSORUPDATE:
                    Sensor.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.SDL_RENDER_DEVICE_RESET:
                case Native.SDL_EventType.SDL_RENDER_TARGETS_RESET:
                    Display.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.SDL_USEREVENT:
                    // We don't expect to see this
                    throw new InvalidOperationException();

                default:
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Waits for an event and dispatches it..
        /// </summary>
        /// <param name="timeout">How long to wait for an event.</param>
        /// <returns><c>false</c> if the quit event has been received, <c>true</c> otherwise.</returns>
        public bool WaitEvent(int timeout = 0)
        {
            Native.SDL_Event e;

            _ = timeout == Timeout.Infinite
                ? Native.CheckErrorZero(Native.SDL_WaitEvent(&e))
                : Native.CheckErrorZero(Native.SDL_WaitEventTimeout(&e, timeout));

            DispatchEvent(ref e);
            return !_quitReceived;
        }

        /// <summary>
        /// Dispatches all events in the queue.
        /// </summary>
        /// <returns><c>false</c> if the quit event has been received, <c>true</c> otherwise.</returns>
        public bool DispatchEvents()
        {
            Native.SDL_Event e;

            while (Native.SDL_PollEvent(&e))
            {
                DispatchEvent(ref e);
            }

            return !_quitReceived;
        }
    }
}
