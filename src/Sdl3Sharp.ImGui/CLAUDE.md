# Sdl3Sharp.ImGui

## Project Overview

This project provides Dear ImGui C# bindings that wrap the native ImGui library for use with Sdl3Sharp.

## ImGui Managed Wrappers

- When creating managed wrappers for ImGui native types, remove the "Im" prefix from the wrapper class name
  - Example: `ImFontAtlas` native type → `FontAtlas` managed wrapper class
  - Example: `ImFont` native type → `Font` managed wrapper class
- Place managed wrappers in the `Sdl3Sharp.ImGui` namespace (not in `Native`)
- Follow the same wrapper patterns used in `Sdl3Sharp` (e.g., `Texture`, `Window` classes)
