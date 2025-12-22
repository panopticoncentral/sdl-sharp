using Sdl3Sharp.ImGui.Native;
using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a Dear ImGui context that manages the state for a single ImGui instance.
/// </summary>
public unsafe readonly struct Context: IDisposable
{
    internal readonly ImGuiContext* Value { get; init; }

    /// <summary>
    /// The current active context.
    /// </summary>
    public static Context? Current
    {
        get
        {
            ImGuiContext* ctx = ImGui_GetCurrentContext();
            return ctx == null ? null : new Context(ctx);
        }
        set => ImGui_SetCurrentContext(value == null ? null : value.Value.Value);
    }

    /// <summary>
    /// Initializes a new instance of the Context class, optionally using a specified font atlas.
    /// </summary>
    /// <param name="fontAtlas">An optional FontAtlas to use for font rendering. If null, a default font atlas is created and used.</param>
    public Context(FontAtlas? fontAtlas = null)
    {
        Value = ImGui_CreateContext(fontAtlas == null ? null : fontAtlas.Value.Value);
    }

    internal Context(ImGuiContext* native)
    {
        Value = native;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        ImGui_DestroyContext(Value);
    }
}