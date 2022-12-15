namespace SdlSharp.Input
{
    /// <summary>
    /// A binding for a game controller.
    /// </summary>
    public abstract class GameControllerBinding
    {
        internal static GameControllerBinding? FromNative(Native.SDL_GameControllerButtonBind binding)
        {
            return binding.bindType switch
            {
                Native.SDL_ControllerBindType.SDL_CONTROLLER_BINDTYPE_BUTTON =>
                    new GameControllerButtonBinding(binding.value.button),

                Native.SDL_ControllerBindType.SDL_CONTROLLER_BINDTYPE_AXIS =>
                    new GameControllerAxisBinding(binding.value.axis),

                Native.SDL_ControllerBindType.SDL_CONTROLLER_BINDTYPE_HAT =>
                    new GameControllerHatBinding(binding.value.hat.hat, binding.value.hat.hat_mask),

                _ => null,
            };
        }
    }
}
