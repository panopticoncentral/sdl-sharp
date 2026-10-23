using System.Reflection;
using System.Runtime.InteropServices;
using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Audio;
using SdlSharp.Input;
using N = SdlSharp.Native;

internal static unsafe class HighLevelTests
{
    internal static void Run(Action<bool, string> check)
    {
        check((int)GamepadType.Steam == 12, "Steam controller enum");
        check((int)Scancode.Front == 165 && (uint)Keycode.Front == 0x400000a5u, "Front key values");
        check(typeof(HintNames).GetFields().Length == 266, "Complete SDL 3.4.18 hint names");
        check(HintNames.AndroidAaudioInputPreset == "SDL_ANDROID_AAUDIO_INPUT_PRESET", "AAudio hint");
        check(HintNames.EnableSteamScreenKeyboard == "SDL_ENABLE_STEAM_SCREEN_KEYBOARD", "Steam hint");
        check(HintNames.JoystickGameinputRaw == "SDL_JOYSTICK_GAMEINPUT_RAW", "GameInput hint");
        check(HintNames.VideoX11EnableXsyncExt == "SDL_VIDEO_X11_ENABLE_XSYNC_EXT", "XSync hint");
        check(HintNames.WindowsRawKeyboardInputsink == "SDL_WINDOWS_RAW_KEYBOARD_INPUTSINK", "Raw keyboard hint");
        SdlHints.Set(HintNames.VideoDriver, "dummy");
        SdlHints.Set(HintNames.AudioDriver, "dummy");
        using var app = new Application(InitFlags.Video | InitFlags.Events);
        check(SdlHints.Set("SDL_SHARP_TEST", "héllo"), "Set UTF-8 hint");
        check(SdlHints.Get("SDL_SHARP_TEST") == "héllo", "Read UTF-8 hint");
        check(SdlHints.Set("SDL_SHARP_TEST", null) && SdlHints.Get("SDL_SHARP_TEST") == null, "Remove nullable hint");
        using (var props = new PropertyGroup())
        {
            props.SetString("test", "héllo");
            props.SetNumber("number", long.MaxValue);
            props.SetBoolean("flag", true);
            check(props.GetString("test") == "héllo", "Property UTF-8 round trip");
            check(props.GetNumber("number") == long.MaxValue && props.GetBoolean("flag"), "Property numeric and bool round trip");
        }
        var guid = SdlGuid.Parse("0123456789abcdef0011223344556677");
        check(guid.ToString() == "0123456789abcdef0011223344556677", "Packed GUID by-value ABI round trip");
        using (var stream = AudioStream.Create(new AudioSpec(AudioFormat.F32, 2, 48000), new AudioSpec(AudioFormat.S16, 2, 48000)))
        {
            stream.GetFormat(out var input, out var output);
            check(input.Format == AudioFormat.F32 && output.Format == AudioFormat.S16, "32-bit audio format ABI round trip");
            var data = AudioSamples.Convert(new AudioSpec(AudioFormat.U8, 1, 48000), [128, 128, 128, 128], new AudioSpec(AudioFormat.S16, 1, 48000));
            check(data.Length == 8 && data.All(b => b == 0), "Audio conversion through updated ABI");
        }
        var state = PenInput.Down | PenInput.Button2 | PenInput.InProximity;
        var deviceType = PenDeviceType.Indirect;
        int eventCount = 0;
        void Proximity(PenProximityEventArgs e) { check(e.WindowId == 42 && e.Which == 71 && e.PenState == state && e.DeviceType == deviceType, "Pen proximity payload"); eventCount++; }
        void Motion(PenMotionEventArgs e) { check(e.X == 12.5f && e.Y == 3.25f && e.PenState == state && e.DeviceType == deviceType, "Pen motion payload"); eventCount++; }
        void Touch(PenTouchEventArgs e) { check(e.IsDown && e.IsEraser && e.PenState == state && e.DeviceType == deviceType, "Pen touch payload"); eventCount++; }
        void Button(PenButtonEventArgs e) { check(e.Button == 2 && e.IsDown && e.PenState == state && e.DeviceType == deviceType, "Pen button payload"); eventCount++; }
        void Axis(PenAxisEventArgs e) { check(e.Axis == PenAxis.Pressure && e.Value == .75f && e.PenState == state && e.DeviceType == deviceType, "Pen axis payload"); eventCount++; }
        Application.PenProximityIn += Proximity;
        Application.PenProximityOut += Proximity;
        Application.PenMotion += Motion;
        Application.PenDown += Touch;
        Application.PenUp += Touch;
        Application.PenButtonDown += Button;
        Application.PenButtonUp += Button;
        Application.PenAxisMotion += Axis;
        try
        {
            foreach (var type in new[] { N.SDL_EventType.SDL_EVENT_PEN_PROXIMITY_IN, N.SDL_EventType.SDL_EVENT_PEN_PROXIMITY_OUT })
            {
                var e = new N.SDL_Event { pproximity = new() { type = type, windowID = new(42), which = 71, pen_state = (uint)state, device_type = (N.SDL_PenDeviceType)deviceType } };
                N.Common.Check(N.Events.SDL_PushEvent(&e));
            }
            var motion = new N.SDL_Event { pmotion = new() { type = N.SDL_EventType.SDL_EVENT_PEN_MOTION, x = 12.5f, y = 3.25f, pen_state = (uint)state, device_type = (N.SDL_PenDeviceType)deviceType } };
            N.Common.Check(N.Events.SDL_PushEvent(&motion));
            foreach (var type in new[] { N.SDL_EventType.SDL_EVENT_PEN_DOWN, N.SDL_EventType.SDL_EVENT_PEN_UP })
            {
                var e = new N.SDL_Event { ptouch = new() { type = type, down = 1, eraser = 1, pen_state = (uint)state, device_type = (N.SDL_PenDeviceType)deviceType } };
                N.Common.Check(N.Events.SDL_PushEvent(&e));
            }
            foreach (var type in new[] { N.SDL_EventType.SDL_EVENT_PEN_BUTTON_DOWN, N.SDL_EventType.SDL_EVENT_PEN_BUTTON_UP })
            {
                var e = new N.SDL_Event { pbutton = new() { type = type, button = 2, down = 1, pen_state = (uint)state, device_type = (N.SDL_PenDeviceType)deviceType } };
                N.Common.Check(N.Events.SDL_PushEvent(&e));
            }
            var axis = new N.SDL_Event { paxis = new() { type = N.SDL_EventType.SDL_EVENT_PEN_AXIS, axis = N.SDL_PenAxis.SDL_PEN_AXIS_PRESSURE, value = .75f, pen_state = (uint)state, device_type = (N.SDL_PenDeviceType)deviceType } };
            N.Common.Check(N.Events.SDL_PushEvent(&axis));
            Application.DispatchEvents();
            check(eventCount == 8, "All eight pen event paths dispatched");
        }
        finally
        {
            Application.PenProximityIn -= Proximity;
            Application.PenProximityOut -= Proximity;
            Application.PenMotion -= Motion;
            Application.PenDown -= Touch;
            Application.PenUp -= Touch;
            Application.PenButtonDown -= Button;
            Application.PenButtonUp -= Button;
            Application.PenAxisMotion -= Axis;
        }
        // These buffers are used through copies, so disposing the native source cannot invalidate them.
        using var surface = Surface.Create(16, 16, PixelFormat.Rgba32);
        using var palette = new Palette(2);
        palette.SetColors([new Color(1, 2, 3, 4), new Color(5, 6, 7, 8)]);
        check(palette[1] == new Color(5, 6, 7, 8), "Palette value copy");
        palette.Dispose();
        try { _ = palette.Count; check(false, "Disposed palette must throw"); }
        catch (ObjectDisposedException) { check(true, "Disposed palette throws"); }

        static bool NativeType(Type t) => t.Namespace == "SdlSharp.Native" || (t.HasElementType && NativeType(t.GetElementType()!)) || (t.IsGenericType && t.GetGenericArguments().Any(NativeType));
        foreach (var t in typeof(Application).Assembly.GetExportedTypes().Where(t => t.Namespace != "SdlSharp.Native"))
        foreach (var method in t.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            check(!NativeType(method.ReturnType) && !method.GetParameters().Any(p => NativeType(p.ParameterType)), $"Native type escapes: {t.Name}.{method.Name}");
    }
}
