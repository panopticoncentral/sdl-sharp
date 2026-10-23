using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using SdlSharp.Native;

namespace SdlSharp;

// Used synchronously by codec APIs. The caller's Stream always remains open.
internal sealed unsafe class SdlStreamBridge : IDisposable
{
    private readonly Stream _stream;
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2213", Justification = "The caller owns this stream; the bridge deliberately leaves it open.")]
    private readonly Stream? _destination;
    private readonly bool _ownsStream;
    internal static SdlStreamBridge ForReading(Stream stream) => new(stream, writing: false);
    internal static SdlStreamBridge ForWriting(Stream stream) => new(stream, writing: true);
    internal void CompleteWrite()
    {
        ThrowIfError();
        if (_destination != null) { _stream.Position = 0; _stream.CopyTo(_destination); }
    }
    private readonly CallbackHandle _registration;
    private ExceptionDispatchInfo? _error;
    internal SDL_IOStream* Handle { get; private set; }
    private SdlStreamBridge(Stream stream, bool writing)
    {
        ArgumentNullException.ThrowIfNull(stream);
        if (!stream.CanSeek)
        {
            var buffer = new MemoryStream();
            try { if (!writing) { stream.CopyTo(buffer); buffer.Position = 0; } }
            catch { buffer.Dispose(); throw; }
            _stream = buffer; _ownsStream = true;
            if (writing) _destination = stream;
        }
        else _stream = stream;
        _registration = CallbackHandle.Alloc(this);
        var callbacks = new SDL_IOStreamInterface
        {
            version = (uint)sizeof(SDL_IOStreamInterface), size = &Size, seek = &Seek,
            read = &Read, write = &Write, flush = &Flush, close = &Close
        };
        Handle = Native.IOStream.SDL_OpenIO(&callbacks, (void*)(nint)_registration);
        if (Handle == null) { _registration.Free(); if (_ownsStream) _stream.Dispose(); throw new SdlException(); }
    }
    internal void ThrowIfError() => _error?.Throw();
    private static SdlStreamBridge State(void* data) => (SdlStreamBridge)CallbackHandle.FromIntPtr((nint)data).Target!;
    private static void Error(void* data, Exception error) => State(data)._error ??= ExceptionDispatchInfo.Capture(error);
    [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static long Size(void* data)
    {
        try { using var scope = CallbackErrors.Enter(); var s = State(data)._stream; return s.CanSeek ? s.Length : -1; }
        catch (Exception error) { Error(data, error); return -1; }
    }
    [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static long Seek(void* data, long offset, SDL_IOWhence origin)
    {
        try { using var scope = CallbackErrors.Enter(); var s = State(data)._stream; return s.CanSeek ? s.Seek(offset, (SeekOrigin)origin) : -1; }
        catch (Exception error) { Error(data, error); return -1; }
    }
    [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static nuint Read(void* data, void* target, nuint size, SDL_IOStatus* status)
    {
        nuint total = 0;
        try
        {
            using var scope = CallbackErrors.Enter();
            var s = State(data)._stream;
            while (total < size)
            {
                var count = s.Read(new Span<byte>((byte*)target + total, (int)Math.Min(size - total, int.MaxValue)));
                if (count == 0) { *status = SDL_IOStatus.SDL_IO_STATUS_EOF; break; }
                total += (nuint)count;
            }
            return total;
        }
        catch (Exception error) { Error(data, error); *status = SDL_IOStatus.SDL_IO_STATUS_ERROR; return total; }
    }
    [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static nuint Write(void* data, void* source, nuint size, SDL_IOStatus* status)
    {
        nuint total = 0;
        try
        {
            using var scope = CallbackErrors.Enter();
            var s = State(data)._stream;
            while (total < size)
            {
                var count = (int)Math.Min(size - total, int.MaxValue);
                s.Write(new ReadOnlySpan<byte>((byte*)source + total, count));
                total += (nuint)count;
            }
            return total;
        }
        catch (Exception error) { Error(data, error); *status = SDL_IOStatus.SDL_IO_STATUS_ERROR; return total; }
    }
    [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte Flush(void* data, SDL_IOStatus* status)
    {
        try { using var scope = CallbackErrors.Enter(); State(data)._stream.Flush(); return 1; }
        catch (Exception error) { Error(data, error); *status = SDL_IOStatus.SDL_IO_STATUS_ERROR; return 0; }
    }
    [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte Close(void* data) => 1;
    public void Dispose()
    {
        if (Handle == null) return;
        Native.IOStream.SDL_CloseIO(Handle);
        Handle = null;
        _registration.Free();
        if (_ownsStream) _stream.Dispose();
    }
}
