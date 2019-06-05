namespace SdlSharp.Input
{
    /// <summary>
    /// A binding for a game controller.
    /// </summary>
    public abstract class GameControllerBinding
    {
        internal static GameControllerBinding? FromNative(Native.SDL_GameControllerButtonBind binding)
        {
            switch (binding.Type)
            {
                case Native.SDL_ControllerBindType.Button:
                    return new GameControllerButtonBinding(binding.Value.Button);

                case Native.SDL_ControllerBindType.Axis:
                    return new GameControllerAxisBinding(binding.Value.Axis);

                case Native.SDL_ControllerBindType.Hat:
                    return new GameControllerHatBinding(binding.Value.Hat.Hat, binding.Value.Hat.HatMask);

                default:
                    return null;
            }
        }
    }
}
