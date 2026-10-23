using static SdlSharp.ImGui.Native;
namespace SdlSharp.ImGui;

/// <summary>Owns immutable draw output and retains its managed callbacks and texture dependencies across frames.</summary>
public sealed unsafe class ClonedDrawList : IDisposable
{
    private readonly IGSharp_DrawList* _handle;
    internal Lifetime Lifetime { get; }
    internal IGSharp_DrawList* Handle { get { Lifetime.Check(); return _handle; } }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "All leases are transferred to Lifetime cleanup; the catch path disposes Lifetime.")]
    internal ClonedDrawList(IGSharp_DrawList* source)
    {
        _handle = IGSharp_DrawList_CloneOutput(source);
        var pointer = (nint)_handle;
        Lifetime = Lifetime.Own(pointer, () => IGSharp_DrawList_Destroy((IGSharp_DrawList*)pointer));
        Lifetime.ReadOnly = true; Lifetime.IsSnapshot = true;
        try
        {
            // Finalize output before exposing it. This also normalizes trailing
            // empty commands when used with the previous release native binary.
            var submission = IGSharp_DrawData_Create();
            try { IGSharp_DrawData_AddDrawList(submission, _handle); }
            finally { IGSharp_DrawData_Destroy(submission); }
            var backend = ImGuiBackend.RetainCurrent(Lifetime); if (backend != null) Lifetime.AddCleanup(backend.Dispose);
            DrawList.RetainCallbacks(_handle, Lifetime);
            var dependencies = new HashSet<Lifetime> { ImGui.GetFontAtlas().Lifetime };
            var sourceLifetime = SdlSharp.ImGui.Lifetime.Capture((nint)source);
            var texturePointers = new HashSet<nint>();
            for (var i = 0; i < IGSharp_DrawList_GetCmdBufferSize(_handle); i++)
            {
                var texture = IGSharp_DrawCmd_GetTexRef(IGSharp_DrawList_GetCmd(_handle, i));
                if (texture.Data != null) texturePointers.Add((nint)texture.Data);
                else if (sourceLifetime.GpuTextures.TryGetValue(texture.ID, out var gpu) && gpu.RetainFor(Lifetime) is { } lease)
                    Lifetime.AddCleanup(lease.Dispose);
            }
            foreach (var pointerToTexture in texturePointers) Lifetime.AddCleanup(TextureRetention.Retain((IGSharp_TextureData*)pointerToTexture, Lifetime).Dispose);
            foreach (var dependency in dependencies) Lifetime.AddCleanup(dependency.RetainFor(Lifetime).Dispose);
        }
        catch { Lifetime.Dispose(); throw; }
    }
    /// <summary>Number of copied drawing commands.</summary>
    public int CommandCount => IGSharp_DrawList_GetCmdBufferSize(Handle);
    /// <summary>Number of copied vertices.</summary>
    public int VertexCount => IGSharp_DrawList_GetVtxBufferSize(Handle);
    /// <summary>Number of copied indices.</summary>
    public int IndexCount => IGSharp_DrawList_GetIdxBufferSize(Handle);
    /// <summary>Copies one command's metadata.</summary>
    public DrawCmd GetCommand(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index); ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, CommandCount);
        return new(IGSharp_DrawList_GetCmd(Handle, index), Lifetime);
    }
    /// <summary>Copies the vertices into a managed array.</summary>
    public DrawVertex[] CopyVertices() => DrawGeometry.CopyVertices(Handle);
    /// <summary>Copies the indices into a managed array.</summary>
    public ushort[] CopyIndices() => DrawGeometry.CopyIndices(Handle);
    /// <summary>Invokes a retained managed callback; returns false for geometry and renderer-state commands.</summary>
    public bool TryInvokeCallback(int index) => DrawList.InvokeCallback(Handle, index);
    /// <summary>Creates a separate snapshot with scaled clipping.</summary>
    public ClonedDrawList Transform(Vec2 clipScale)
    {
        var copy = new ClonedDrawList(Handle);
        var temporary = IGSharp_DrawData_Create();
        try
        {
            IGSharp_DrawData_AddDrawList(temporary, copy.Handle);
            IGSharp_DrawData_ScaleClipRects(temporary, new(clipScale.X, clipScale.Y));
            return copy;
        }
        catch { copy.Dispose(); throw; }
        finally { IGSharp_DrawData_Destroy(temporary); }
    }
    /// <summary>Releases native output and all retained callback and texture references.</summary>
    public void Dispose() => Lifetime.Dispose();
}
