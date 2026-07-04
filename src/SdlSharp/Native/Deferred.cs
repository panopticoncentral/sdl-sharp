// This file intentionally contains no code. It documents, per the CLAUDE.md
// skip-comment convention, every SDL3 public header that SdlSharp deliberately
// does not bind at all — so the rationale lives in the native layer even though
// no binding file exists for these headers. Headers that are partially bound
// carry their skip comments in their own binding files.
//
// SDL_asyncio.h — asynchronous file I/O (queues, tasks, outcomes). .NET has
// async/await and System.IO; a managed bridge would fight both models.
// SDL_atomic.h — spinlocks, atomic ints, memory barriers. .NET has
// System.Threading.Interlocked / Volatile / Thread.MemoryBarrier.
// SDL_bits.h / SDL_endian.h / SDL_intrin.h — bit tricks, byte swapping, and
// compiler intrinsics (inline-only, no exports). .NET has
// System.Numerics.BitOperations, BinaryPrimitives, and hardware intrinsics.
// SDL_hidapi.h — raw HID device access. Large, low-level surface serving niche
// hardware; use the joystick/gamepad APIs, or a .NET HID library (e.g. HidSharp).
// SDL_iostream.h — stream abstraction consumed by SDL's *_IO loader variants.
// .NET has System.IO; the *_IO loaders are individually skip-commented in the
// binding files of the headers that declare them (Audio.cs, Surface.cs, ...).
// SDL_loadso.h — shared-object loading; .NET has
// System.Runtime.InteropServices.NativeLibrary.
// SDL_main.h (+ SDL_main_impl.h) — C entry-point plumbing (SDL_main, SDL_RunApp,
// app-callback shims); managed apps own Main(). See also the app-callbacks skip
// comment in Init.cs.
// SDL_metal.h / SDL_vulkan.h / SDL_egl.h — platform graphics-API interop headers
// (Metal view creation, Vulkan instance extensions/surfaces, EGL typedefs).
// Interop belongs to the graphics API's own bindings (Silk.NET, Vortice); raw
// window handles are reachable via window properties.
// SDL_opengl.h / SDL_opengl_glext.h / SDL_opengles.h / SDL_opengles2*.h — GL/GLES
// typedef and prototype headers with no SDL exports; GL function loading is
// available via Gl.GetProcAddress (SDL_GL_GetProcAddress, bound in Video.cs).
// SDL_mutex.h — mutexes, rwlocks, semaphores, condition variables, init state.
// .NET has System.Threading (lock/Monitor, ReaderWriterLockSlim, SemaphoreSlim,
// Lazy<T>).
// SDL_platform.h / SDL_platform_defines.h — compile-time platform macros; the
// single runtime export, SDL_GetPlatform, is deliberately not bound — .NET has
// System.Runtime.InteropServices.RuntimeInformation.
// SDL_process.h — subprocess management; .NET has System.Diagnostics.Process.
// SDL_stdinc.h — C standard-library shims (string/math/memory/PRNG/iconv). The
// BCL covers all of it; the one needed piece, SDL_free (for SDL-owned returns),
// is bound in Common.cs.
// SDL_storage.h — console title/user storage abstraction; .NET targets use
// System.IO, and the console platforms it abstracts are not .NET targets.
// SDL_test*.h (11 headers) — SDL's own test harness (SDLTest_* symbols, shipped
// as a separate static library); not part of the runtime API.
// SDL_thread.h — threads and TLS; .NET has System.Threading.Thread,
// Task.Run, and ThreadLocal<T>.
//
// The remaining unbound headers carry no API surface at all: SDL.h (umbrella
// include), SDL_begin_code.h / SDL_close_code.h (compiler pragma brackets),
// SDL_copying.h (license text), SDL_dlopennote.h (ELF note macros),
// SDL_oldnames.h (SDL2→SDL3 rename shims), and SDL_revision.h (the SDL_REVISION
// macro — the runtime value is available via Sdl.Revision / SDL_GetRevision in
// Version.cs).
