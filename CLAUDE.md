# SdlSharp

## Project Overview

This solution provides support for the Simple DirectMedia Layer 3 (SDL3) and the Dear ImGui libraries in C#. SDL3 is a cross-platform development library designed to provide low-level access to audio, keyboard, mouse, joystick, and graphics hardware via OpenGL and Direct3D. Dear Imgui is a cross-platform library designed to provide immediate-mode GUI primitives for applications.

## Modern P/Invoke Conventions

- Use `[LibraryImport]` instead of `[DllImport]` for all P/Invoke declarations
- Use `[UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]` to specify calling convention
- Methods using `LibraryImport` must be declared as `static partial`
- For `bool` parameters and return values, add `[MarshalAs(UnmanagedType.U1)]` to ensure proper marshalling
- For UTF-8 strings (`const char*` or `byte*` in SDL), use `[MarshalUsing(typeof(Utf8StringMarshaller))]` for parameters and `[return: MarshalUsing(typeof(Utf8StringMarshaller))]` for return values - this provides efficient UTF-8 conversion with minimal copying
- For callback parameters, use function pointer syntax `delegate* unmanaged[Cdecl]<...>` instead of delegate types (better performance, no GC allocation, explicit calling convention)
- For opaque `void*` userdata parameters that SDL does not dereference, use `nuint` instead to indicate they are opaque values rather than pointers
- For typedefs of primitive types (like `typedef Uint32 SDL_DisplayID`), create type-safe wrapper structs with:
  - A readonly `Value` property of the underlying type
  - A constructor that takes the underlying type
  - Implicit conversion operators to/from the underlying type
  - Full XML documentation for all members (Value property, constructor, and both conversion operators)
- Import `System.Runtime.InteropServices.Marshalling` namespace when needed

## XML Documentation

- All public types (classes, structs, enums), methods, properties, and fields must have XML documentation comments
- For enums, document each enum member with a `<summary>` tag explaining its purpose
- For struct fields and properties, include XML documentation describing what the field/property represents
- For type-safe wrapper structs (like `SDL_DisplayID`, `SDL_WindowID`), document the `Value` property, constructor, and implicit conversion operators
- For string constants (especially SDL property names), include XML documentation explaining what the constant represents and how it's used
- Use clear, concise descriptions that help developers understand the purpose and usage of each member

## Code Style and Formatting Rules

See .editorconfig for detailed formatting and style rules.