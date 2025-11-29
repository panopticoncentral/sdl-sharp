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
- Fortypedefs of primitive types (like `typedef Uint32 SDL_DisplayID`), create type-safe wrapper structs with:
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

### Indentation and Spacing
- Use 4 spaces for indentation (no tabs)
- Line endings should be CRLF (Windows-style)
- Do not insert a final newline at the end of files

### Namespaces and Using Directives
- Use file-scoped namespaces (e.g., `namespace Sdl3Sharp.Native;`)
- Place `using` directives outside the namespace
- Do not separate `using` directives into groups
- Do not sort System directives first

### Type and Member Preferences
- Use language keywords for types instead of BCL types (e.g., `int` not `Int32`, `string` not `String`)
- Do not use `this.` qualifier for members unless necessary
- Prefer auto-properties over properties with backing fields
- Mark fields as `readonly` when possible
- Use expression-bodied members for accessors, indexers, lambdas, and properties
- Do not use expression-bodied members for constructors, methods, operators, or local functions

### Var Usage
- Use `var` for built-in types
- Use `var` when the type is apparent from the assignment
- Do not use `var` elsewhere (specify explicit types)

### Modern C# Features
- Use collection expressions when types loosely match
- Use object and collection initializers
- Prefer pattern matching over `is` with cast check and `as` with null check
- Use switch expressions when appropriate
- Use null-coalescing expressions (`??`)
- Use null-conditional operators (`?.`)
- Use index operators (`^`) and range operators (`..`)
- Use implicit object creation when type is apparent (e.g., `new()` instead of `new Type()`)
- Prefer inferred tuple and anonymous type member names
- Use `is null` checks instead of `ReferenceEquals(x, null)`
- Use simplified boolean expressions
- Use compound assignment operators (e.g., `+=`, `*=`)
- Use tuple swap when appropriate
- Use throw expressions when appropriate
- Prefer UTF-8 string literals when appropriate

### Null-Checking
- Use conditional delegate invocation (e.g., `action?.Invoke()`)
- Prefer `is null` over reference equality method

### Braces and Blocks
- Always use braces for code blocks (even single-line)
- Opening braces should be on a new line (Allman style)
- New line before `catch`, `else`, and `finally`
- New line before members in object initializers and anonymous types

### Method and Function Preferences
- Prefer simple `using` statements (without braces when possible)
- Prefer `System.Threading.Lock` over traditional lock statements
- Prefer method group conversions
- Prefer primary constructors when appropriate
- Prefer static local functions when appropriate
- Prefer static anonymous functions when appropriate

### Modifier Order
Follow this order: `public`, `private`, `protected`, `internal`, `file`, `static`, `extern`, `new`, `virtual`, `abstract`, `sealed`, `override`, `readonly`, `unsafe`, `required`, `volatile`, `async`

### Parentheses
- Always use parentheses for clarity in arithmetic binary operators
- Always use parentheses for clarity in relational binary operators
- Always use parentheses for clarity in other binary operators
- Never use unnecessary parentheses in other operators

### Spacing
- No space after cast
- Space after colons in inheritance clauses
- Space after commas
- No space after dots
- Space after keywords in control flow statements
- Space after semicolons in for statements
- Space around binary operators (before and after)
- No space around declaration statements
- Space before colons in inheritance clauses
- No space before commas, dots, open square brackets, or semicolons in for statements
- No space between method call/declaration name and opening parenthesis
- No space in empty parameter lists
- No space between parentheses or square brackets

### Blank Lines
- Do not allow blank line after colon in constructor initializer
- Do not allow blank line after token in arrow expression clause
- Do not allow blank line after token in conditional expression
- Do not allow blank lines between consecutive braces
- Do not allow embedded statements on same line
- Do not allow multiple blank lines
- Do not allow statement immediately after block

### Code Organization
- Do not use `#region` directives to organize code
- Namespace should match folder structure
- Require accessibility modifiers for non-interface members

### Parameter and Variable Management
- Remove all unused parameters
- Use discard variable (`_`) for unused assignments and expression statements
- Inline variable declarations when possible
- Use deconstructed variable declarations when appropriate

### Naming Conventions
- Interfaces should begin with `I` (e.g., `IDisposable`)
- Types (classes, structs, enums) should use PascalCase
- Properties, events, and methods should use PascalCase