namespace SdlSharp.Input
{
    /// <summary>
    /// A binding for a game controller.
    /// </summary>
    public abstract class GameControllerBinding
    {
        internal static GameControllerBinding? FromNative(Native.SDL_GameControllerButtonBind binding)
        {
            return binding.Type switch
            {
                Native.SDL_ControllerBindType.Button => new GameControllerButtonBinding(binding.Value.Button),

                Native.SDL_ControllerBindType.Axis => new GameControllerAxisBinding(binding.Value.Axis),

                Native.SDL_ControllerBindType.Hat => new GameControllerHatBinding(binding.Value.Hat.Hat, binding.Value.Hat.HatMask),

                _ => null,
            };
        }
    }
}
