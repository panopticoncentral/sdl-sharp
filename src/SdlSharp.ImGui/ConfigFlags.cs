namespace SdlSharp.ImGui;

/// <summary>Configuration flags stored in io.ConfigFlags.</summary>
[Flags]
public enum ConfigFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Master keyboard navigation enable flag.</summary>
    NavEnableKeyboard = 1 << 0,
    /// <summary>Master gamepad navigation enable flag.</summary>
    NavEnableGamepad = 1 << 1,
    /// <summary>Instruct dear imgui to disable mouse inputs and interactions.</summary>
    NoMouse = 1 << 4,
    /// <summary>Instruct backend to not alter mouse cursor shape and visibility.</summary>
    NoMouseCursorChange = 1 << 5,
    /// <summary>Instruct dear imgui to disable keyboard inputs and interactions.</summary>
    NoKeyboard = 1 << 6,
    /// <summary>Application is SRGB-aware.</summary>
    IsSRGB = 1 << 20,
    /// <summary>Application is using a touch screen instead of a mouse.</summary>
    IsTouchScreen = 1 << 21,
}
