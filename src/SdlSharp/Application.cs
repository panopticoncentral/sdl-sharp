using System;
using System.Threading;
using SdlSharp.Graphics;
using SdlSharp.Input;
using SdlSharp.Sound;
using SdlSharp.Touch;

namespace SdlSharp
{
    /// <summary>
    /// A class representing the SDL application.
    /// </summary>
    public sealed unsafe class Application : IDisposable
    {
        private bool _quitReceived = false;

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
                Native.SDL_GetVersion(out var version);
                return version;
            }
        }

        /// <summary>
        /// The revision string of the version of SDL that's being used.
        /// </summary>
        public static string Revision =>
            Native.SDL_GetRevision();

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
            get => Native.SDL_WasInit(Subsystems.None);
            set
            {
                var current = InitializedSubystems;

                Native.SDL_QuitSubSystem(current & ~value);
                _ = Native.CheckError(Native.SDL_InitSubSystem(value & ~current));
            }
        }

        /// <summary>
        /// The version of SDL_image being used.
        /// </summary>
        public static Version ImageVersion =>
            *Native.IMG_Linked_Version();

        /// <summary>
        /// The version of SDL_mixer that is being used.
        /// </summary>
        public static Version MixerVersion =>
            *Native.Mix_Linked_Version();

        /// <summary>
        /// The version of SDL_ttf in use.
        /// </summary>
        public static Version FontVersion =>
            *Native.TTF_Linked_Version();

        /// <summary>
        /// Whether the font subsystem is initialized.
        /// </summary>
        public bool FontIsInitialized =>
            Native.TTF_WasInit();

        /// <summary>
        /// The platform the application is running on.
        /// </summary>
        public string Platform =>
            Native.SDL_GetPlatform();

        /// <summary>
        /// The base path of the application.
        /// </summary>
        public string BasePath =>
            Native.CheckNotNull(Native.SDL_GetBasePath().ToString());

        /// <summary>
        /// Information about the power state of the computer.
        /// </summary>
        public (PowerState State, int Seconds, int Percent) PowerInfo
        {
            get
            {
                var state = Native.SDL_GetPowerInfo(out var seconds, out var percent);
                return (state, seconds, percent);
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
            _ = Native.CheckError(Native.SDL_Init(subsystems));

            if (hints != null)
            {
                foreach (var hint in hints)
                {
                    _ = hint.Hint.Set(hint.Value);
                }
            }

            if (imageFormats != ImageFormats.None)
            {
                _ = Native.CheckError(Native.IMG_Init(imageFormats));
                _initializedImage = true;
            }

            if (mixerFormats != MixerFormats.None)
            {
                _ = Native.CheckError(Native.Mix_Init(mixerFormats));
                _initializedMixer = true;
            }

            if (fontSupport)
            {
                _ = Native.CheckError(Native.TTF_Init());
                _initializedFont = true;
            }
        }

        /// <summary>
        /// An event that fires when a drop begins.
        /// </summary>
        public event EventHandler<DropEventArgs> DropBegin;

        /// <summary>
        /// An event that fires when a drop completes.
        /// </summary>
        public event EventHandler<DropEventArgs> DropComplete;

        /// <summary>
        /// An event that fires when a file is dropped.
        /// </summary>
        public event EventHandler<DroppedEventArgs> FileDropped;

        /// <summary>
        /// An event that fires when text is dropped.
        /// </summary>
        public event EventHandler<DroppedEventArgs> TextDropped;

        /// <summary>
        /// An event that fires when the application is quit.
        /// </summary>
        public event EventHandler<SdlEventArgs> Quitting;

        /// <summary>
        /// An event that fires when the application is terminating.
        /// </summary>
        public event EventHandler<SdlEventArgs> Terminating;

        /// <summary>
        /// An event that fires when the application is low on memory.
        /// </summary>
        public event EventHandler<SdlEventArgs> LowMemory;

        /// <summary>
        /// An event that fires when the application is going to enter the background.
        /// </summary>
        public event EventHandler<SdlEventArgs> WillEnterBackground;

        /// <summary>
        /// An event that fires when the application entered the background.
        /// </summary>
        public event EventHandler<SdlEventArgs> DidEnterBackground;

        /// <summary>
        /// An event that fires when the application is going to enter the foreground.
        /// </summary>
        public event EventHandler<SdlEventArgs> WillEnterForeground;

        /// <summary>
        /// An event that fires when the application entered the foreground.
        /// </summary>
        public event EventHandler<SdlEventArgs> DidEnterForeground;

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
        public int ShowMessageBox(MessageBoxFlags flags, Window? window, string title, string message, MessageBoxButton[] buttons, MessageBoxColorScheme? colorScheme)
        {
            var nativeButtons = new Native.SDL_MessageBoxButtonData[buttons.Length];

            for (var index = 0; index < buttons.Length; index++)
            {
                nativeButtons[index] = new Native.SDL_MessageBoxButtonData(buttons[index].Flags, buttons[index].ButtonId, Utf8String.ToUtf8String(buttons[index].Text));
            }

            using var utf8Title = Utf8String.ToUtf8String(title);
            using var utf8Message = Utf8String.ToUtf8String(message);

            MessageBoxColorScheme colorSchemeValue = colorScheme.GetValueOrDefault();
            int buttonId;

            fixed (Native.SDL_MessageBoxButtonData* buttonBuffer = nativeButtons)
            {
                _ = Native.CheckError(Native.SDL_ShowMessageBox(
                    new Native.SDL_MessageBoxData(flags, window == null ? null : window.Pointer, utf8Title, utf8Message, buttons.Length, buttonBuffer, colorScheme == null ? null : &colorSchemeValue),
                    out buttonId));
            }

            foreach (var nativeButton in nativeButtons)
            {
                nativeButton.Text.Dispose();
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
        public void ShowMessageBox(MessageBoxFlags flags, string title, string message, Window? window)
        {
            using var utf8Title = Utf8String.ToUtf8String(title);
            using var utf8Message = Utf8String.ToUtf8String(message);

            _ = Native.CheckError(Native.SDL_ShowSimpleMessageBox(flags, utf8Title, utf8Message, window == null ? null : window.Pointer));
        }

        /// <summary>
        /// Get the path to store user preferences.
        /// </summary>
        /// <param name="organization">Organization name.</param>
        /// <param name="application">Application name.</param>
        /// <returns>The path to store preferences.</returns>
        public string GetPreferencesPath(string organization, string application)
        {
            using var organizationString = Utf8String.ToUtf8String(organization);
            using var applicationString = Utf8String.ToUtf8String(application);
            return Native.CheckNotNull(Native.SDL_GetPrefPath(organizationString, applicationString).ToString());
        }

        /// <summary>
        /// Dispatches one event.
        /// </summary>
        /// <param name="timeout">How long to wait for an event.</param>
        /// <returns><c>true</c> if the quit event has been received, <c>false</c> otherwise.</returns>
        public bool DispatchEvent(int timeout = 0)
        {
            Native.SDL_Event e;

            switch (timeout)
            {
                case 0:
                    if (!Native.SDL_PollEvent(out e))
                    {
                        return !_quitReceived;
                    }
                    break;

                case Timeout.Infinite:
                    _ = Native.CheckErrorZero(Native.SDL_WaitEvent(out e));
                    break;

                default:
                    _ = Native.CheckErrorZero(Native.SDL_WaitEventTimeout(out e, timeout));
                    break;
            }

            switch (e.Type)
            {
                case Native.SDL_EventType.Display:
                case Native.SDL_EventType.RenderDeviceReset:
                case Native.SDL_EventType.RenderTargetsReset:
                    Display.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.Window:
                case Native.SDL_EventType.SystemWindowMessageEvent:
                    Window.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.KeyUp:
                case Native.SDL_EventType.KeyDown:
                case Native.SDL_EventType.TextEditing:
                case Native.SDL_EventType.TextInput:
                case Native.SDL_EventType.KeymapChanged:
                    Keyboard.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.MouseButtonDown:
                case Native.SDL_EventType.MouseButtonUp:
                case Native.SDL_EventType.MouseMotion:
                case Native.SDL_EventType.MouseWheel:
                    Mouse.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.JoystickDeviceAdded:
                case Native.SDL_EventType.JoystickAxisMotion:
                case Native.SDL_EventType.JoystickBallMotion:
                case Native.SDL_EventType.JoystickButtonDown:
                case Native.SDL_EventType.JoystickButtonUp:
                case Native.SDL_EventType.JoystickDeviceRemoved:
                case Native.SDL_EventType.JoystickHatMotion:
                    Joystick.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.ControllerAxisMotion:
                case Native.SDL_EventType.ControllerButtonDown:
                case Native.SDL_EventType.ControllerButtonUp:
                case Native.SDL_EventType.ControllerDeviceAdded:
                case Native.SDL_EventType.ControllerDeviceRemapped:
                case Native.SDL_EventType.ControllerDeviceRemoved:
                    GameController.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.AudioDeviceAdded:
                case Native.SDL_EventType.AudioDeviceRemoved:
                    AudioDevice.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.FingerDown:
                case Native.SDL_EventType.FingerUp:
                case Native.SDL_EventType.FingerMotion:
                    TouchDevice.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.MultiGesture:
                case Native.SDL_EventType.DollarGesture:
                case Native.SDL_EventType.DollarRecord:
                    Gesture.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.DropBegin:
                    DropBegin?.Invoke(null, new DropEventArgs(e.Drop));
                    break;

                case Native.SDL_EventType.DropComplete:
                    DropComplete?.Invoke(null, new DropEventArgs(e.Drop));
                    break;

                case Native.SDL_EventType.DropFile:
                    FileDropped?.Invoke(null, new DroppedEventArgs(e.Drop));
                    e.Drop.File.Dispose();
                    break;

                case Native.SDL_EventType.DropText:
                    TextDropped?.Invoke(null, new DroppedEventArgs(e.Drop));
                    e.Drop.File.Dispose();
                    break;

                case Native.SDL_EventType.SensorUpdate:
                    Sensor.DispatchEvent(e);
                    break;

                case Native.SDL_EventType.Quit:
                    Quitting?.Invoke(null, new SdlEventArgs(e.Common));
                    _quitReceived = true;
                    break;

                case Native.SDL_EventType.UserEvent:
                    // We don't expect to see this
                    throw new InvalidOperationException();

                case Native.SDL_EventType.ApplicationTerminating:
                    Terminating?.Invoke(null, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_EventType.ApplicationLowMemory:
                    LowMemory?.Invoke(null, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_EventType.ApplicationWillEnterBackground:
                    WillEnterBackground?.Invoke(null, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_EventType.ApplicationDidEnterBackground:
                    DidEnterBackground?.Invoke(null, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_EventType.ApplicationWillEnterForeground:
                    WillEnterForeground?.Invoke(null, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_EventType.ApplicationDidEnterForeground:
                    DidEnterForeground?.Invoke(null, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_EventType.ClipboardUpdate:
                    Clipboard.DispatchEvent(e);
                    break;

                default:
                    throw new InvalidOperationException();
            }

            return !_quitReceived;
        }
    }
}
