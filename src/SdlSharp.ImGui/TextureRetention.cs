using static SdlSharp.ImGui.Native;
namespace SdlSharp.ImGui;

// QueueUserData prevents ImGui from recycling never-uploaded textures while a
// snapshot still refers to them. The managed backend also defers GPU destruction.
internal static unsafe class TextureRetention
{
    private sealed class Entry(nint pointer)
    {
        internal readonly nint Pointer = pointer;
        internal int Count = 1;
    }
    private static readonly Dictionary<nint, Entry> Entries = [];
    internal static bool IsRetained(IGSharp_TextureData* pointer) => Entries.ContainsKey((nint)pointer);
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "Each lease transfers to its returned ReleaseAction, or is released if registration fails.")]
    internal static IDisposable Retain(IGSharp_TextureData* pointer, Lifetime owner)
    {
        var key = (nint)pointer;
        Entries.TryGetValue(key, out var entry);
        if (entry == null && IGSharp_TextureData_GetQueueUserData(pointer) != null)
            throw new InvalidOperationException("The texture already belongs to a different render queue.");
        var lease = SdlSharp.ImGui.Lifetime.Capture(key).RetainFor(owner);
        try
        {
            if (entry != null) entry.Count++;
            else
            {
                entry = new(key);
                Entries.Add(key, entry);
                IGSharp_TextureData_SetQueueUserData(pointer, (void*)1);
            }
        }
        catch { lease.Dispose(); throw; }
        return new ReleaseAction(() =>
        {
            if (--entry.Count != 0) { lease.Dispose(); return; }
            IGSharp_TextureData_SetQueueUserData((IGSharp_TextureData*)entry.Pointer, null);
            Entries.Remove(entry.Pointer); lease.Dispose();
        });
    }
    internal static IDisposable DeferDestruction()
    {
        var pending = new List<nint>();
        foreach (var entry in Entries.Values)
        {
            var pointer = (IGSharp_TextureData*)entry.Pointer;
            if (IGSharp_TextureData_GetStatus(pointer) != (int)TextureStatus.WantDestroy) continue;
            pending.Add(entry.Pointer); IGSharp_TextureData_SetStatus(pointer, (int)TextureStatus.OK);
        }
        return new ReleaseAction(() =>
        {
            foreach (var pointer in pending) IGSharp_TextureData_SetStatus((IGSharp_TextureData*)pointer, (int)TextureStatus.WantDestroy);
        });
    }
}
