# Phase 7.3 No-Native-Types Input Fixes Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Remove the last `SdlSharp.Native` types from the public input/event surface: replace the raw event filter with a managed `RawEvent`, expose the text-input flow on `Window`, add a public `Cursor` class, and surface the wheel direction.

**Architecture:** Spec at `docs/superpowers/specs/2026-07-02-no-native-types-input-design.md`. Same patterns as the completed GPU migration: public enums cast from native members, `readonly record struct`s, handle-wrapper class with the use-after-dispose guard (`ObjectDisposedException.ThrowIf`, see `GpuBuffer.cs` post-bd075a7 for the current shape), `Common.Check` error handling.

**Tech Stack:** C# 13 / .NET 10, LibraryImport P/Invoke.

**One documented deviation from the spec:** `TextInputProperties` uses **nullable** fields (`TextInputType? Type` etc.) instead of the spec's literal defaults. Verification against SDL_keyboard.h showed SDL's defaults are *conditional* (capitalization defaults to Sentences for plain text but Words for names and None for emails/passwords; multiline's default depends on a hint), so unconditionally writing a default value would override SDL's smarter behavior. Null = "don't set the property, let SDL decide" is the faithful implementation of the spec's "defaults mirror SDL's documented defaults" intent.

**Build gates:** the repo is back to a zero-warning baseline (commit 065a666: CS1591 suppressed for the Native layer via `src/SdlSharp/Native/.editorconfig`; public members still require docs). Every task's gate is: `dotnet build SdlSharp.slnx` → `0 Warning(s), 0 Error(s)` unless stated otherwise.

**Git:** Commit after every task with the message shown. NO Co-Authored-By lines.

---

### Task 1: Public `EventType` enum

**Files:**
- Create: `src/SdlSharp/Input/EventType.cs`

- [ ] **Step 1.1: Create the enum by mirroring `Native.SDL_EventType`**

Source of truth: the `SDL_EventType` enum in `src/SdlSharp/Native/Events.cs` (~119 `SDL_EVENT_*` members). Create:

```csharp
namespace SdlSharp.Input;

/// <summary>
/// The types of events that can be delivered.
/// </summary>
public enum EventType : uint
{
    /// <summary>Application quit requested.</summary>
    Quit = (uint)Native.SDL_EventType.SDL_EVENT_QUIT,
    // ... every member of Native.SDL_EventType, same order ...
}
```

Derivation rules (apply mechanically to every member):
- Strip the `SDL_EVENT_` prefix, PascalCase the rest: `SDL_EVENT_KEY_DOWN` → `KeyDown`, `SDL_EVENT_MOUSE_BUTTON_DOWN` → `MouseButtonDown`, `SDL_EVENT_GAMEPAD_ADDED` → `GamepadAdded`.
- Compound tokens stay joined: `SDL_EVENT_JOYSTICK_AXIS_MOTION` → `JoystickAxisMotion`, `SDL_EVENT_TEXT_EDITING_CANDIDATES` → `TextEditingCandidates`, `SDL_EVENT_CLIPBOARD_UPDATE` → `ClipboardUpdate`.
- Range sentinels keep First/Last suffixes: `SDL_EVENT_WINDOW_FIRST` → `WindowFirst`, `SDL_EVENT_WINDOW_LAST` → `WindowLast`, `SDL_EVENT_DISPLAY_FIRST` → `DisplayFirst`, etc. `SDL_EVENT_FIRST` → `First`, `SDL_EVENT_LAST` → `Last`, `SDL_EVENT_USER` → `User`, `SDL_EVENT_ENUM_PADDING` → omit (padding machinery, not API).
- Every member cast: `= (uint)Native.SDL_EventType.SDL_EVENT_…`.
- Each member gets a one-line `/// <summary>` (derive from the native member's doc comment where present, else a short factual line).
- Keep the native file's `//` section grouping comments (Application events, Display events, Window events, Keyboard, Mouse, Joystick, Gamepad, Touch, Clipboard, Drag and drop, Audio, Sensor, Pen, Camera, Render, Internal/User).

- [ ] **Step 1.2: Verify member parity**

Run: `grep -c "SDL_EVENT_" src/SdlSharp/Native/Events.cs` (counts native references — note the enum defines ~119; subtract non-enum-definition matches) and compare with a count of members in the new file: `grep -c "= (uint)Native.SDL_EventType" src/SdlSharp/Input/EventType.cs`. Expected: every native enum member except `SDL_EVENT_ENUM_PADDING` is represented. List any you deliberately omitted.

- [ ] **Step 1.3: Build**

Run: `dotnet build SdlSharp.slnx`
Expected: `0 Warning(s), 0 Error(s)`

- [ ] **Step 1.4: Commit**

```bash
git add src/SdlSharp/Input/EventType.cs
git commit -m "Add public EventType enum"
```

---

### Task 2: `RawEvent` + filter rework + ImGuiBackend

**Files:**
- Create: `src/SdlSharp/Input/RawEvent.cs`
- Modify: `src/SdlSharp/Application.cs:115-122,157-162`
- Modify: `src/SdlSharp.ImGui/ImGuiBackend.cs:26,66,71-74`

- [ ] **Step 2.1: Create `RawEvent`**

```csharp
namespace SdlSharp.Input;

/// <summary>
/// A raw SDL event passed to <see cref="Application.RawEventFilter"/> before typed dispatch.
/// </summary>
/// <param name="Type">The event type.</param>
/// <param name="Pointer">
/// The address of the native SDL_Event. Intended for passing to external
/// backends (for example the Dear ImGui SDL3 platform backend). Only valid for the
/// duration of the callback — do not store it.
/// </param>
public readonly record struct RawEvent(EventType Type, nint Pointer);
```

- [ ] **Step 2.2: Rework the filter in `Application.cs`**

Replace lines 115-121 (the `RawEventHandler` delegate and `RawEventFilter` event):

```csharp
/// <summary>
/// Handles a raw event before typed dispatch.
/// </summary>
/// <param name="e">The raw event.</param>
public delegate void RawEventHandler(in RawEvent e);

/// <summary>
/// Raised for every polled event before typed dispatch. The event's
/// <see cref="RawEvent.Pointer"/> is only valid during the callback.
/// </summary>
public static event RawEventHandler? RawEventFilter;
```

(The delegate loses its `unsafe` modifier.) In `DispatchEvents`, replace `RawEventFilter?.Invoke(&e);` (line ~162) with:

```csharp
RawEventFilter?.Invoke(new RawEvent((EventType)e.type, (nint)(&e)));
```

Add `using SdlSharp.Input;` to Application.cs if not already present (it is — typed event args are used).

- [ ] **Step 2.3: Update `ImGuiBackend`**

In `src/SdlSharp.ImGui/ImGuiBackend.cs`, replace the `OnRawEvent` method (lines ~71-74):

```csharp
private static void OnRawEvent(in RawEvent e)
{
    IGSharp_ImplSDL3_ProcessEvent((SDL_Event*)e.Pointer);
}
```

Add `using SdlSharp.Input;` to the file's usings if missing. The `+=`/`-=` subscription lines are unchanged (the delegate type name is the same).

- [ ] **Step 2.4: Build (solution — the delegate change spans assemblies)**

Run: `dotnet build SdlSharp.slnx`
Expected: `0 Warning(s), 0 Error(s)`

- [ ] **Step 2.5: Grep gate for this task**

Run: `grep -n "Native.SDL_Event\*" src/SdlSharp/Application.cs`
Expected: no matches in public declarations (internal body uses of `Native.SDL_Event e;` for polling remain and are fine — the gate is the public delegate signature).

- [ ] **Step 2.6: Commit**

```bash
git add src/SdlSharp/Input/RawEvent.cs src/SdlSharp/Application.cs src/SdlSharp.ImGui/ImGuiBackend.cs
git commit -m "Replace native event pointer in RawEventFilter with managed RawEvent"
```

---

### Task 3: Native text-input additions

**Files:**
- Modify: `src/SdlSharp/Native/Keyboard.cs`

- [ ] **Step 3.1: Replace the stale skip comments (lines 8-10)**

Delete the two `// Skipped:` comment blocks about `SDL_TextInputType`/`SDL_Capitalization`/`SDL_StartTextInputWithProperties` and `SDL_SetTextInputArea`/`SDL_GetTextInputArea` — all are being bound now.

- [ ] **Step 3.2: Add the two enums (before the `Keyboard` class)**

```csharp
/// <summary>
/// Text input type for SDL_PROP_TEXTINPUT_TYPE_NUMBER.
/// </summary>
public enum SDL_TextInputType
{
    SDL_TEXTINPUT_TYPE_TEXT,
    SDL_TEXTINPUT_TYPE_TEXT_NAME,
    SDL_TEXTINPUT_TYPE_TEXT_EMAIL,
    SDL_TEXTINPUT_TYPE_TEXT_USERNAME,
    SDL_TEXTINPUT_TYPE_TEXT_PASSWORD_HIDDEN,
    SDL_TEXTINPUT_TYPE_TEXT_PASSWORD_VISIBLE,
    SDL_TEXTINPUT_TYPE_NUMBER,
    SDL_TEXTINPUT_TYPE_NUMBER_PASSWORD_HIDDEN,
    SDL_TEXTINPUT_TYPE_NUMBER_PASSWORD_VISIBLE,
}

/// <summary>
/// Auto capitalization type for SDL_PROP_TEXTINPUT_CAPITALIZATION_NUMBER.
/// </summary>
public enum SDL_Capitalization
{
    SDL_CAPITALIZE_NONE,
    SDL_CAPITALIZE_SENTENCES,
    SDL_CAPITALIZE_WORDS,
    SDL_CAPITALIZE_LETTERS,
}
```

- [ ] **Step 3.3: Add the bindings and constants (inside the `Keyboard` class, after `SDL_StopTextInput`)**

```csharp
[LibraryImport(Common.Sdl3, EntryPoint = "SDL_StartTextInputWithProperties")]
[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
[return: MarshalAs(UnmanagedType.U1)]
public static unsafe partial bool SDL_StartTextInputWithProperties(SDL_Window* window, SDL_PropertiesID props);

[LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTextInputArea")]
[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
[return: MarshalAs(UnmanagedType.U1)]
public static unsafe partial bool SDL_SetTextInputArea(SDL_Window* window, SDL_Rect* rect, int cursor);

[LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTextInputArea")]
[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
[return: MarshalAs(UnmanagedType.U1)]
public static unsafe partial bool SDL_GetTextInputArea(SDL_Window* window, SDL_Rect* rect, int* cursor);

/// <summary>Property: an SDL_TextInputType describing the text being input.</summary>
public const string SDL_PROP_TEXTINPUT_TYPE_NUMBER = "SDL.textinput.type";

/// <summary>Property: an SDL_Capitalization describing how text should be capitalized.</summary>
public const string SDL_PROP_TEXTINPUT_CAPITALIZATION_NUMBER = "SDL.textinput.capitalization";

/// <summary>Property: true to enable auto completion and auto correction.</summary>
public const string SDL_PROP_TEXTINPUT_AUTOCORRECT_BOOLEAN = "SDL.textinput.autocorrect";

/// <summary>Property: true if multiple lines of text are allowed.</summary>
public const string SDL_PROP_TEXTINPUT_MULTILINE_BOOLEAN = "SDL.textinput.multiline";
```

Note: `SDL_PROP_TEXTINPUT_ANDROID_INPUTTYPE_NUMBER` is deliberately skipped (platform-specific) — add a one-line `// Skipped:` comment saying so. `SDL_Rect` comes from `SdlSharp.Native` (Rect.cs) — already in scope via the namespace. `SDL_PropertiesID` likewise.

- [ ] **Step 3.4: Build and commit**

Run: `dotnet build SdlSharp.slnx` → `0 Warning(s), 0 Error(s)` (Native layer is CS1591-exempt, but the enums/constants above are documented anyway).

```bash
git add src/SdlSharp/Native/Keyboard.cs
git commit -m "Bind text input area, properties variant, and text input enums"
```

---

### Task 4: Public text-input surface

**Files:**
- Create: `src/SdlSharp/Input/TextInputType.cs`
- Create: `src/SdlSharp/Input/Capitalization.cs`
- Create: `src/SdlSharp/Input/TextInputProperties.cs`
- Modify: `src/SdlSharp/Graphics/Window.cs` (add methods after `Flash`, line ~261)

- [ ] **Step 4.1: Create the two enums**

`src/SdlSharp/Input/TextInputType.cs`:

```csharp
namespace SdlSharp.Input;

/// <summary>
/// The kind of text being input, used to hint on-screen keyboards and IMEs.
/// </summary>
public enum TextInputType
{
    /// <summary>The input is text.</summary>
    Text = Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT,
    /// <summary>The input is a person's name.</summary>
    TextName = Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT_NAME,
    /// <summary>The input is an e-mail address.</summary>
    TextEmail = Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT_EMAIL,
    /// <summary>The input is a username.</summary>
    TextUsername = Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT_USERNAME,
    /// <summary>The input is a secure password that is hidden.</summary>
    TextPasswordHidden = Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT_PASSWORD_HIDDEN,
    /// <summary>The input is a secure password that is visible.</summary>
    TextPasswordVisible = Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT_PASSWORD_VISIBLE,
    /// <summary>The input is a number.</summary>
    Number = Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_NUMBER,
    /// <summary>The input is a secure PIN that is hidden.</summary>
    NumberPasswordHidden = Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_NUMBER_PASSWORD_HIDDEN,
    /// <summary>The input is a secure PIN that is visible.</summary>
    NumberPasswordVisible = Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_NUMBER_PASSWORD_VISIBLE,
}
```

(If the compiler requires explicit `(int)` casts on the member initializers — it will, matching every other outer enum in the codebase — add them.)

`src/SdlSharp/Input/Capitalization.cs`:

```csharp
namespace SdlSharp.Input;

/// <summary>
/// How text input should be auto-capitalized.
/// </summary>
public enum Capitalization
{
    /// <summary>No auto-capitalization.</summary>
    None = Native.SDL_Capitalization.SDL_CAPITALIZE_NONE,
    /// <summary>The first letter of sentences will be capitalized.</summary>
    Sentences = Native.SDL_Capitalization.SDL_CAPITALIZE_SENTENCES,
    /// <summary>The first letter of words will be capitalized.</summary>
    Words = Native.SDL_Capitalization.SDL_CAPITALIZE_WORDS,
    /// <summary>All letters will be capitalized.</summary>
    Letters = Native.SDL_Capitalization.SDL_CAPITALIZE_LETTERS,
}
```

(Same cast note.)

- [ ] **Step 4.2: Create `TextInputProperties`**

```csharp
namespace SdlSharp.Input;

/// <summary>
/// Optional text input configuration for <see cref="SdlSharp.Graphics.Window.StartTextInput(in TextInputProperties)"/>.
/// Unset (null) fields are not sent to SDL, which then applies its own
/// context-sensitive defaults (for example, capitalization defaults depend on
/// the input type).
/// </summary>
/// <param name="Type">The kind of text being input, or null for SDL's default (plain text).</param>
/// <param name="Capitalization">The auto-capitalization mode, or null for SDL's type-dependent default.</param>
/// <param name="Autocorrect">Whether auto completion/correction is enabled, or null for SDL's default (true).</param>
/// <param name="Multiline">Whether multiple lines are allowed, or null for SDL's hint-dependent default.</param>
public readonly record struct TextInputProperties(
    TextInputType? Type = null,
    Capitalization? Capitalization = null,
    bool? Autocorrect = null,
    bool? Multiline = null);
```

- [ ] **Step 4.3: Add the `Window` methods**

Insert into `src/SdlSharp/Graphics/Window.cs` after the `Flash` method (line ~261), following the file's doc/`Check` style. Add `using SdlSharp.Input;` and `using static SdlSharp.Native.Keyboard;` to the file's usings (the file currently uses `using static SdlSharp.Native.Video;` — keep both; if any binding names collide, qualify with `Native.Keyboard.`).

```csharp
/// <summary>
/// Starts accepting Unicode text input events in this window. Shows the
/// on-screen keyboard where applicable.
/// </summary>
public void StartTextInput() => Check(SDL_StartTextInput(Handle));

/// <summary>
/// Starts accepting Unicode text input events in this window, with hints
/// describing the kind of text being entered.
/// </summary>
/// <param name="properties">The text input configuration. Unset fields use SDL's defaults.</param>
public void StartTextInput(in TextInputProperties properties)
{
    using var props = new PropertyGroup();
    if (properties.Type is { } type)
        props.SetNumber(Native.Keyboard.SDL_PROP_TEXTINPUT_TYPE_NUMBER, (long)type);
    if (properties.Capitalization is { } capitalization)
        props.SetNumber(Native.Keyboard.SDL_PROP_TEXTINPUT_CAPITALIZATION_NUMBER, (long)capitalization);
    if (properties.Autocorrect is { } autocorrect)
        props.SetBoolean(Native.Keyboard.SDL_PROP_TEXTINPUT_AUTOCORRECT_BOOLEAN, autocorrect);
    if (properties.Multiline is { } multiline)
        props.SetBoolean(Native.Keyboard.SDL_PROP_TEXTINPUT_MULTILINE_BOOLEAN, multiline);
    Check(SDL_StartTextInputWithProperties(Handle, props.Id));
}

/// <summary>
/// Stops accepting text input events in this window.
/// </summary>
public void StopTextInput() => Check(SDL_StopTextInput(Handle));

/// <summary>
/// Gets whether text input is active in this window.
/// </summary>
public bool IsTextInputActive => SDL_TextInputActive(Handle);

/// <summary>
/// Sets the area used for typing in this window, informing the IME where to
/// position candidate/composition windows.
/// </summary>
/// <param name="area">The text input area, in window coordinates.</param>
/// <param name="cursorOffset">The cursor X offset relative to the area's left edge.</param>
public void SetTextInputArea(Rectangle area, int cursorOffset = 0)
{
    var native = area.ToNative();
    Check(SDL_SetTextInputArea(Handle, &native, cursorOffset));
}

/// <summary>
/// Gets the area used for typing in this window.
/// </summary>
/// <param name="cursorOffset">Receives the cursor X offset relative to the area's left edge.</param>
/// <returns>The text input area, in window coordinates.</returns>
public Rectangle GetTextInputArea(out int cursorOffset)
{
    SDL_Rect rect;
    int cursor;
    Check(SDL_GetTextInputArea(Handle, &rect, &cursor));
    cursorOffset = cursor;
    return new Rectangle(rect.x, rect.y, rect.w, rect.h);
}

/// <summary>
/// Dismisses the composition window and clears any pending IME composition state.
/// </summary>
public void ClearComposition() => Check(SDL_ClearComposition(Handle));
```

Check whether `Rectangle` has a `FromNative`-style helper or ctor-from-`SDL_Rect` already (look at `Rectangle.cs`); if so use it instead of the field-by-field construction. `PropertyGroup.Id` is internal — Window.cs is in the same assembly, fine.

- [ ] **Step 4.4: Build and commit**

Run: `dotnet build SdlSharp.slnx` → `0 Warning(s), 0 Error(s)`

```bash
git add src/SdlSharp/Input/TextInputType.cs src/SdlSharp/Input/Capitalization.cs src/SdlSharp/Input/TextInputProperties.cs src/SdlSharp/Graphics/Window.cs
git commit -m "Expose per-window text input flow with IME area and input hints"
```

---

### Task 5: `Cursor` class + `SystemCursor` enum

**Files:**
- Create: `src/SdlSharp/Input/SystemCursor.cs`
- Create: `src/SdlSharp/Input/Cursor.cs`

- [ ] **Step 5.1: Create `SystemCursor`**

Mirror `Native.SDL_SystemCursor` (src/SdlSharp/Native/Mouse.cs:21-46), omitting the `SDL_SYSTEM_CURSOR_COUNT` sentinel:

```csharp
namespace SdlSharp.Input;

/// <summary>
/// Standard system cursor shapes.
/// </summary>
public enum SystemCursor
{
    /// <summary>Default cursor (usually an arrow).</summary>
    Default = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_DEFAULT,
    /// <summary>Text selection (usually an I-beam).</summary>
    Text = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_TEXT,
    /// <summary>Wait cursor (usually an hourglass or spinner).</summary>
    Wait = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_WAIT,
    /// <summary>Crosshair.</summary>
    Crosshair = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_CROSSHAIR,
    /// <summary>Program is busy but still interactive.</summary>
    Progress = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_PROGRESS,
    /// <summary>Double arrow pointing northwest and southeast.</summary>
    NwseResize = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_NWSE_RESIZE,
    /// <summary>Double arrow pointing northeast and southwest.</summary>
    NeswResize = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_NESW_RESIZE,
    /// <summary>Double arrow pointing west and east.</summary>
    EwResize = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_EW_RESIZE,
    /// <summary>Double arrow pointing north and south.</summary>
    NsResize = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_NS_RESIZE,
    /// <summary>Four-pointed move arrow.</summary>
    Move = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_MOVE,
    /// <summary>Action not permitted (usually a slashed circle).</summary>
    NotAllowed = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_NOT_ALLOWED,
    /// <summary>Pointer that indicates a link (usually a pointing hand).</summary>
    Pointer = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_POINTER,
    /// <summary>Window resize top-left.</summary>
    NwResize = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_NW_RESIZE,
    /// <summary>Window resize top.</summary>
    NResize = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_N_RESIZE,
    /// <summary>Window resize top-right.</summary>
    NeResize = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_NE_RESIZE,
    /// <summary>Window resize right.</summary>
    EResize = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_E_RESIZE,
    /// <summary>Window resize bottom-right.</summary>
    SeResize = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_SE_RESIZE,
    /// <summary>Window resize bottom.</summary>
    SResize = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_S_RESIZE,
    /// <summary>Window resize bottom-left.</summary>
    SwResize = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_SW_RESIZE,
    /// <summary>Window resize left.</summary>
    WResize = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_W_RESIZE,
}
```

(Add `(int)` casts on initializers as required by the compiler.)

- [ ] **Step 5.2: Create `Cursor`**

Follow the current handle-wrapper shape — read `src/SdlSharp/Graphics/Gpu/GpuBuffer.cs` first for the post-bd075a7 disposed-guard pattern and match it exactly:

```csharp
using SdlSharp.Graphics;
using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Mouse;

namespace SdlSharp.Input;

/// <summary>
/// A managed wrapper around an SDL mouse cursor (SDL_Cursor).
/// </summary>
public sealed unsafe class Cursor : IDisposable
{
    private readonly bool _ownsHandle;

    internal SDL_Cursor* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private SDL_Cursor* _handle;

    internal Cursor(SDL_Cursor* handle, bool ownsHandle = true)
    {
        _handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Creates a standard system cursor.
    /// </summary>
    /// <param name="id">The system cursor shape.</param>
    /// <returns>A new cursor. Dispose it when no longer needed.</returns>
    public static Cursor CreateSystem(SystemCursor id) =>
        new(Check(SDL_CreateSystemCursor((SDL_SystemCursor)id)));

    /// <summary>
    /// Creates a color cursor from a surface.
    /// </summary>
    /// <param name="surface">The cursor image.</param>
    /// <param name="hotX">The X position of the cursor hot spot.</param>
    /// <param name="hotY">The Y position of the cursor hot spot.</param>
    /// <returns>A new cursor. Dispose it when no longer needed.</returns>
    public static Cursor CreateColor(Surface surface, int hotX, int hotY) =>
        new(Check(SDL_CreateColorCursor(surface.Handle, hotX, hotY)));

    /// <summary>
    /// Gets or sets the active cursor. The getter returns a non-owning wrapper
    /// (or null when no cursor is set). Setting null forces a cursor redraw,
    /// mirroring SDL_SetCursor(NULL).
    /// </summary>
    public static Cursor? Current
    {
        get
        {
            var handle = SDL_GetCursor();
            return handle == null ? null : new Cursor(handle, ownsHandle: false);
        }
        set => Check(SDL_SetCursor(value == null ? null : value.Handle));
    }

    /// <summary>
    /// Gets the default cursor as a non-owning wrapper.
    /// </summary>
    public static Cursor Default => new(Check(SDL_GetDefaultCursor()), ownsHandle: false);

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && _handle != null)
        {
            SDL_DestroyCursor(_handle);
        }
        _handle = null;
    }
}
```

If `GpuBuffer.cs`'s actual guard pattern differs (field/property names, guard placement), match the codebase, not this snippet — the behavior contract is: internal Handle getter throws `ObjectDisposedException` after dispose; Dispose destroys only when owning and always nulls; double-dispose safe.

- [ ] **Step 5.3: Build and commit**

Run: `dotnet build SdlSharp.slnx` → `0 Warning(s), 0 Error(s)`

```bash
git add src/SdlSharp/Input/SystemCursor.cs src/SdlSharp/Input/Cursor.cs
git commit -m "Add public Cursor class and SystemCursor enum"
```

---

### Task 6: Wheel direction

**Files:**
- Create: `src/SdlSharp/Input/MouseWheelDirection.cs`
- Modify: `src/SdlSharp/Input/EventArgs.cs:60-72` (MouseWheelEventArgs)
- Modify: `src/SdlSharp/Application.cs:208-213` (wheel dispatch)

- [ ] **Step 6.1: Create the enum**

```csharp
namespace SdlSharp.Input;

/// <summary>
/// The direction of a mouse wheel event.
/// </summary>
public enum MouseWheelDirection
{
    /// <summary>The scroll direction is normal.</summary>
    Normal = Native.SDL_MouseWheelDirection.SDL_MOUSEWHEEL_NORMAL,
    /// <summary>The scroll direction is flipped ("natural" scrolling).</summary>
    Flipped = Native.SDL_MouseWheelDirection.SDL_MOUSEWHEEL_FLIPPED,
}
```

(Cast note as before.)

- [ ] **Step 6.2: Extend `MouseWheelEventArgs`**

Replace the record declaration in `src/SdlSharp/Input/EventArgs.cs`:

```csharp
/// <summary>
/// Event data for mouse wheel events.
/// </summary>
/// <param name="WindowId">The window with mouse focus.</param>
/// <param name="X">Horizontal scroll amount.</param>
/// <param name="Y">Vertical scroll amount.</param>
/// <param name="MouseX">Mouse X position.</param>
/// <param name="MouseY">Mouse Y position.</param>
/// <param name="Direction">The scroll direction. When <see cref="MouseWheelDirection.Flipped"/>, the scroll amounts are inverted relative to normal scrolling.</param>
public readonly record struct MouseWheelEventArgs(
    uint WindowId,
    float X, float Y,
    float MouseX, float MouseY,
    MouseWheelDirection Direction);
```

- [ ] **Step 6.3: Populate it in `Application.DispatchEvents`**

Replace the wheel case (Application.cs ~208-213):

```csharp
case Native.SDL_EventType.SDL_EVENT_MOUSE_WHEEL:
    MouseWheel?.Invoke(new MouseWheelEventArgs(
        e.wheel.windowID.Value,
        e.wheel.x, e.wheel.y,
        e.wheel.mouse_x, e.wheel.mouse_y,
        (MouseWheelDirection)e.wheel.direction));
    break;
```

- [ ] **Step 6.4: Build and commit**

Run: `dotnet build SdlSharp.slnx` → `0 Warning(s), 0 Error(s)` (check no other call sites construct MouseWheelEventArgs — `grep -rn "MouseWheelEventArgs(" --include="*.cs" src/ Samples/` and fix any).

```bash
git add src/SdlSharp/Input/MouseWheelDirection.cs src/SdlSharp/Input/EventArgs.cs src/SdlSharp/Application.cs
git commit -m "Surface mouse wheel direction on MouseWheelEventArgs"
```

---

### Task 7: Verification gates + INVENTORY.md + TODO.md

**Files:**
- Modify: `INVENTORY.md` (`## SDL_keyboard.h`, `## SDL_mouse.h`, `## SDL_events.h` sections)
- Modify: `TODO.md` (7.3 checkbox)

- [ ] **Step 7.1: Full gates**

1. `dotnet build SdlSharp.slnx --no-incremental` → `0 Warning(s), 0 Error(s)`.
2. `grep -rn "public.*SDL_" src/SdlSharp/Input/ src/SdlSharp/Application.cs` → only body call expressions and const initializers referencing `Native.` constants; zero native types in public signatures.
3. `dotnet run --project Samples/GpuInfo` → exit 0.
4. ImGuiDemo smoke test (background run ~10s, both processes alive, empty stderr, then kill) — this exercises the reworked RawEventFilter through the ImGui backend every frame; a broken filter means no input processing, but a crash/no-window means a broken migration: report what you observe. Interactive verification stays with the user.

- [ ] **Step 7.2: INVENTORY.md updates (verify every name against code before writing)**

- `## SDL_keyboard.h`: fill Native/Managed cells for SDL_TextInputType (`Keyboard.SDL_TextInputType` / `TextInputType`), SDL_Capitalization (→ `Capitalization`), SDL_StartTextInputWithProperties (→ `Window.StartTextInput(in TextInputProperties)`), SDL_SetTextInputArea (→ `Window.SetTextInputArea`), SDL_GetTextInputArea (→ `Window.GetTextInputArea`), the four SDL_PROP_TEXTINPUT_* constants (grouped row → `Keyboard.SDL_PROP_TEXTINPUT_*` / `TextInputProperties`), and update the managed cells for SDL_StartTextInput/SDL_StopTextInput/SDL_TextInputActive/SDL_ClearComposition (→ `Window.StartTextInput`/`Window.StopTextInput`/`Window.IsTextInputActive`/`Window.ClearComposition`). Remove stale deferral notes. `SDL_PROP_TEXTINPUT_ANDROID_INPUTTYPE_NUMBER` gets a row with a `platform` note if missing.
- `## SDL_mouse.h`: managed cells for SDL_SystemCursor (→ `SystemCursor`), SDL_CreateSystemCursor (→ `Cursor.CreateSystem`), SDL_CreateColorCursor (→ `Cursor.CreateColor`), SDL_SetCursor/SDL_GetCursor (→ `Cursor.Current`), SDL_GetDefaultCursor (→ `Cursor.Default`), SDL_DestroyCursor (→ `Cursor.Dispose`), SDL_MouseWheelDirection (→ `MouseWheelDirection`).
- `## SDL_events.h`: update the SDL_EventType row (→ `EventType`) and the row/note describing the event filter (→ `Application.RawEventFilter` with `RawEvent`).

- [ ] **Step 7.3: Check off TODO.md 7.3**

Change `- [ ] 7.3 No-native-types fixes elsewhere: …` to `- [x]`, text intact.

- [ ] **Step 7.4: Final build + commit**

Run: `dotnet build SdlSharp.slnx` → clean.

```bash
git add INVENTORY.md TODO.md
git commit -m "Record phase 7.3 input surface in INVENTORY.md; complete phase 7.3"
```
