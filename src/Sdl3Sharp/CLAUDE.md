# Sdl3Sharp

## Project Overview
This project provides bindings for the Simple DirectMedia Layer 3 (SDL3) library in C#. SDL3 is a cross-platform development library designed to provide low-level access to audio, keyboard, mouse, joystick, and graphics hardware via OpenGL and Direct3D. The project includes low level P/Invoke bindings to SDL3's C APIs that enable .NET developers to talk directly to the API, as well as high-level .NET classes that wrap the low-level APIs in a .NET-friendly way.

You can use the SdlSharp project (which are older bindings for SDL2) as a guideline for writing SDL3 bindings, but more modern interop features and designs should be preferred.

## SDL3 Headers and Documentation

The current SDL3 headers can be found in directory next to the root of the solution, in a directory that starts with `SDL3-`, in the `include/SDL3` subdirectory. SDL3 documentation can be found at https://wiki.libsdl.org/SDL3/FrontPage.

## Interop Naming Conventions

- When converting an SDL header (e.g., `SDL_video.h`), name the C# file without the `SDL_` prefix (e.g., `Video.cs`)
- Keep SDL function names exactly as in C (e.g., `SDL_Init`, `SDL_GetVersion`)
- Keep SDL constant names exactly as in C (e.g., `SDL_INIT_VIDEO`)
- Keep SDL struct names exactly as in C (e.g., `SDL_AsyncIOOutcome`)
- Use `Sdl3Sharp.Native` namespace for all P/Invoke declarations

## Modern P/Invoke Conventions for SDL3

- For `SDL_GUID`, use the native `System.Guid` structure directly - both are 128-bit/16-byte structs with compatible memory layouts, and `System.Guid` provides better .NET integration (string conversion, equality, formatting). Note: SDL's GUID string format is lowercase hex without dashes; use `guid.ToString("N")` for SDL-compatible formatting. The `SDL_GUIDToString` and `SDL_StringToGUID` functions do not need to be wrapped.
- For `SDL_mutex.h`, use .NET's built-in threading primitives instead of wrapping SDL's: `System.Threading.Lock` or `lock` statement for `SDL_Mutex`, `ReaderWriterLockSlim` for `SDL_RWLock`, `SemaphoreSlim` for `SDL_Semaphore`, and `Monitor.Wait/Pulse/PulseAll` for `SDL_Condition`. .NET primitives offer better integration with async/await, no P/Invoke overhead, and safer resource management. Only wrap `SDL_InitState`/`SDL_InitStatus` if needed for SDL API interop.
- Do not use `#region` directives to organize code

## Documenting Skipped APIs

When wrapping SDL headers, some APIs cannot or should not be wrapped. Document these with comments in the C# file:
- Add a comment explaining which API was not wrapped and why
- Common reasons for skipping APIs:
  - Variadic functions (e.g., functions with `...` parameters) - va_list is not supported in C# P/Invoke
  - C macros that are simple wrappers - can be implemented as C# helper methods if needed
  - Platform-specific internal APIs not intended for public use
  - APIs that don't apply to .NET (e.g., main() entry point functions)
- Example format: `// SDL_FunctionName is not wrapped - reason why. Alternative approach if applicable.`

## High-Level Wrapper Classes (Sdl3Sharp namespace)

When creating managed wrapper classes in the `Sdl3Sharp` namespace that wrap low-level P/Invoke APIs:
- Each type (class, struct, enum) should be in its own file
- Use the error checking helper methods from `Sdl3Sharp.Native.Common` for consistent error handling
- Use `CheckErrorNull<T>(T? value)` for return values that should not be null
- Use `CheckErrorPointer<T/T*>(T*/T** ptr)` for pointer return values that should not be null
- Use `CheckErrorBool(bool returnValue)` for boolean return values where `false` indicates an error
- Use `CheckErrorZero(float/int/uint/nuint returnValue)` for return values where `0` indicates an error
- These helper methods automatically throw `SdlException` with the appropriate SDL error message
- Example: `return new IOStream(CheckPointer(SDL_IOFromFile(path, mode)), ownsHandle: true);` instead of manually checking for null and throwing