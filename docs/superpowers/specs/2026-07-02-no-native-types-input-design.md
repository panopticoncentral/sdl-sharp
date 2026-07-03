# Phase 7.3: No-Native-Types Input Fixes — Design

**Date:** 2026-07-02
**Phase:** 7.3 (TODO.md, "Phases 7–11: SDL3 Surface Completion")
**Problem:** Three areas outside the GPU layer still violate the no-native-types rule or
leave native-bound features unreachable from the public API: the `Application` raw event
filter exposes `Native.SDL_Event*`; the text-input flow (`SDL_StartTextInput` etc.) is
reachable only through `SdlSharp.Native`; the bound cursor API has no public wrapper.
Additionally `MouseWheelEventArgs` omits the wheel direction the native event carries.

## 1. Event filter → `RawEvent`

- New public enum `EventType` in `SdlSharp.Input` (file `src/SdlSharp/Input/EventType.cs`)
  wrapping `SDL_EventType`: clean C# names, members as `= (uint)Native.SDL_EventType.…`
  per the established enum pattern. Mirror every member of the native enum (~150), using
  grouped `//` section comments matching the native file's grouping.
- New `public readonly record struct RawEvent(EventType Type, nint Pointer)` in
  `SdlSharp.Input` (file `src/SdlSharp/Input/RawEvent.cs`). `Pointer` is the raw
  `SDL_Event*` for interop with external backends; documented as valid only for the
  duration of the callback — do not store it.
- `Application.RawEventHandler` becomes `public delegate void RawEventHandler(in RawEvent e)`
  (no longer `unsafe`, no `Native` types). `DispatchEvents` constructs
  `new RawEvent((EventType)e.type, (nint)(&e))` before the typed dispatch switch.
- `src/SdlSharp.ImGui/ImGuiBackend.cs` updates: `OnRawEvent(in RawEvent e)` casts
  `(SDL_Event*)e.Pointer` for `IGSharp_ImplSDL3_ProcessEvent`.

## 2. Text input

Native layer (`src/SdlSharp/Native/Keyboard.cs`):
- Add `SDL_TextInputType` and `SDL_Capitalization` enums (original C names).
- Add `SDL_SetTextInputArea(SDL_Window*, SDL_Rect*, int cursor)`,
  `SDL_GetTextInputArea(SDL_Window*, SDL_Rect*, int* cursor)`,
  `SDL_StartTextInputWithProperties(SDL_Window*, SDL_PropertiesID)`.
- Add the four property-name constants as `public const string`
  (`SDL_PROP_TEXTINPUT_TYPE_NUMBER`, `…_CAPITALIZATION_NUMBER`, `…_AUTOCORRECT_BOOLEAN`,
  `…_MULTILINE_BOOLEAN`) — const-string form per the GPU device-create precedent, values
  byte-exact from SDL_keyboard.h.
- Remove the now-stale skip comments for these symbols (lines ~8–10).

Public surface — instance methods on `Window` (SDL scopes text input per-window):
- `void StartTextInput()`, `void StopTextInput()`, `bool IsTextInputActive { get; }`
- `void StartTextInput(TextInputProperties properties)` — builds a transient
  `PropertyGroup`, sets only the non-default fields, calls the WithProperties native.
- `void SetTextInputArea(Rectangle area, int cursorOffset = 0)`
- `Rectangle GetTextInputArea(out int cursorOffset)`
- `void ClearComposition()` (native already bound)
- All `Common.Check`-wrapped.

New public types in `SdlSharp.Input`:
- `TextInputType` enum wrapping `SDL_TextInputType` (Text, TextName, TextEmail,
  TextUsername, TextPasswordHidden, TextPasswordVisible, Number, NumberPasswordHidden,
  NumberPasswordVisible).
- `Capitalization` enum wrapping `SDL_Capitalization` (None, Sentences, Words, Letters).
- `readonly record struct TextInputProperties(TextInputType Type = TextInputType.Text,
  Capitalization Capitalization = Capitalization.None, bool Autocorrect = true,
  bool Multiline = false)` — defaults mirror SDL's documented defaults (verify against
  SDL_keyboard.h during implementation).

## 3. Cursor

- `public sealed unsafe class Cursor : IDisposable` in `SdlSharp.Input`
  (file `src/SdlSharp/Input/Cursor.cs`), standard handle-wrapper pattern including the
  use-after-dispose guard convention (`ObjectDisposedException.ThrowIf(_handle == null, this)`
  in the internal `Handle` getter, per commit bd075a7's codebase-wide pattern) and
  `_ownsHandle` for non-owning instances.
- Members: `static Cursor CreateSystem(SystemCursor id)`, `static Cursor CreateColor(Surface surface, int hotX, int hotY)`,
  `static Cursor? Current { get; set; }` (get wraps `SDL_GetCursor` non-owning, null when
  no cursor; set accepts null → `SDL_SetCursor(null)` redraw semantics per SDL docs),
  `static Cursor Default { get; }` (non-owning), `Dispose()` → `SDL_DestroyCursor` when owning.
- New public `SystemCursor` enum in `SdlSharp.Input` wrapping `SDL_SystemCursor`
  (Default, Text, Wait, Crosshair, Progress, NwseResize, NeswResize, EwResize, NsResize,
  Move, NotAllowed, Pointer, NwResize, NResize, NeResize, EResize, SeResize, SResize,
  SwResize, WResize — verify names against the native enum).
- `Mouse.ShowCursor`/`HideCursor`/`IsCursorVisible` remain on the static `Mouse` class.

## 4. Wheel direction

- New public enum `MouseWheelDirection` (Normal, Flipped) in `SdlSharp.Input` wrapping
  `SDL_MouseWheelDirection`.
- `MouseWheelEventArgs` gains a `MouseWheelDirection Direction` positional field;
  `Application.DispatchEvents` populates it from the native wheel event.

## Error handling

`Common.Check(bool)` / `Check<T>(T*)` on every native call that reports failure, throwing
`SdlException` — unchanged project pattern.

## Verification

1. `dotnet build SdlSharp.slnx` — 0 errors, 0 warnings (zero-warning baseline restored in
   commit 065a666; the Native additions fall under the Native-layer CS1591 policy, public
   members are fully documented).
2. Grep gate: `grep -n "Native\." src/SdlSharp/Application.cs | grep "public"` and
   `grep -rn "public.*SDL_" src/SdlSharp/Input/` → no native types in public signatures
   (body call expressions and const initializers referencing native constants excepted).
3. ImGuiDemo smoke test — exercises the reworked event filter end-to-end through the
   ImGui backend (input forwarding is the filter's real consumer).
4. INVENTORY.md updates: SDL_keyboard.h section (new bindings + managed wrappers, remove
   stale deferral notes), SDL_mouse.h section (Cursor class rows), SDL_events.h section
   (RawEvent/EventType rows for the filter), and TODO.md 7.3 checked off in the same
   change.
