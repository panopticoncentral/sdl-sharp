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

## Documenting Skipped APIs
When wrapping SDL headers, some APIs cannot or should not be wrapped. Document these with comments in the C# file:
- Add a comment explaining which API was not wrapped and why
- Common reasons for skipping APIs:
  - Variadic functions (e.g., functions with `...` parameters) - va_list is not supported in C# P/Invoke
  - C macros that are simple wrappers - can be implemented as C# helper methods if needed
  - Platform-specific internal APIs not intended for public use
  - APIs that don't apply to .NET (e.g., main() entry point functions)
- Example format: `// SDL_FunctionName is not wrapped - reason why. Alternative approach if applicable.`

## Code Style and Formatting Rules

### Indentation and Spacing
- Use 4 spaces for indentation (no tabs)
- Line endings should be CRLF (Windows-style)
- Do not insert a final newline at the end of files

### Namespaces and Using Directives
- Use file-scoped namespaces (e.g., `namespace SdlSharp.Native;`)
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