using SdlSharp.Graphics.Gpu;

namespace SdlSharp.ImGui;

// Capture native identity rather than the wrapper's disposed bit: disposing a
// borrowed alias must not invalidate a texture retained by drawing commands.
internal sealed class GpuTextureReference
{
    internal readonly ulong Id;
    internal readonly NativeLifetime Lifetime;
    internal readonly NativeLifetime Device;
    internal GpuTextureReference(GpuTexture texture)
    {
        if (texture.OwnerCommand != null) throw new ArgumentException("A swapchain texture cannot be retained by ImGui drawing.", nameof(texture));
        Id = (ulong)texture.NativeHandle; Lifetime = texture.Lifetime; Device = texture.Device.Lifetime;
    }
    internal void Check() { Lifetime.Check(); Device.Check(); }
    internal IDisposable? RetainFor(SdlSharp.ImGui.Lifetime owner)
    {
        Check();
        if (owner.GpuTextures.ContainsKey(Id)) return null;
        var lease = Lifetime.Retain();
        owner.GpuTextures.Add(Id, this);
        return new ReleaseAction(() => { owner.GpuTextures.Remove(Id); lease.Dispose(); });
    }
}
