# SDL-Sharp

## Project Overview
This project provides bindings for the Simple DirectMedia Layer 3 (SDL3) library in C#. SDL3 is a cross-platform development library designed to provide low-level access to audio, keyboard, mouse, joystick, and graphics hardware via OpenGL and Direct3D. The project includes low level P/Invoke bindings to SDL3's C APIs that enable .NET developers to talk directly to the API, as well as high-level .NET classes that wrap the low-level APIs in a .NET-friendly way.

The solution also contains bindings for the Simple DirectMedia Layer 2 (SDL2) library. These can be used as guidelines for writing SDL3 bindings, but more modern interop features should be preferred.

## SDL3 Headers and Documentation

The current SDL3 headers can be found in the root of the solution, in a directory that starts with `SDL3-`, in the `include/SDL3` subdirectory. SDL3 documentation can be found at https://wiki.libsdl.org/SDL3/FrontPage.

## Interop Naming Conventions
- When converting an SDL header (e.g., `SDL_video.h`), name the C# file without the `SDL_` prefix (e.g., `Video.cs`)
- Keep SDL function names exactly as in C (e.g., `SDL_Init`, `SDL_GetVersion`)
- Keep SDL constant names exactly as in C (e.g., `SDL_INIT_VIDEO`)
- Keep SDL struct names exactly as in C (e.g., `SDL_AsyncIOOutcome`)
- Use `SdlSharp.Native` namespace for all P/Invoke declarations
- Use file-scoped namespaces (e.g., `namespace SdlSharp.Native;` instead of `namespace SdlSharp.Native { ... }`)

## Modern P/Invoke Conventions for SDL3
- Use `[LibraryImport]` instead of `[DllImport]` for all P/Invoke declarations
- Use `[UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]` to specify calling convention
- Methods using `LibraryImport` must be declared as `static partial`
- For `bool` parameters and return values, add `[MarshalAs(UnmanagedType.U1)]` to ensure proper marshalling
- For callback parameters, use function pointer syntax `delegate* unmanaged[Cdecl]<...>` instead of delegate types (better performance, no GC allocation, explicit calling convention)
- For opaque `void*` userdata parameters that SDL does not dereference, use `nuint` instead to indicate they are opaque values rather than pointers
- Import `System.Runtime.InteropServices.Marshalling` namespace when needed
- Do not use `#region` directives to organize code