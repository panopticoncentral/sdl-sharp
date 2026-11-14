# SDL# — SDL3 for .NET 10

[![SDL3 CI](https://github.com/panopticoncentral/sdl-sharp/actions/workflows/ci.yaml/badge.svg?branch=main)](https://github.com/panopticoncentral/sdl-sharp/actions/workflows/ci.yaml?query=branch%3Amain)

`main` contains the C# bindings for **SDL 3.4.18**. Applications using SDL2 should stay on their existing package version; the [sdl2 maintenance branch](https://github.com/panopticoncentral/sdl-sharp/tree/sdl2) contains that API and its tutorials.

- **SdlSharp** wraps SDL3 graphics, audio, input, and system APIs. Its native dependency is SdlSharp.Redist.
- **SdlSharp.ImGui** adds Dear ImGui 1.93 and the SDL3 GPU backend. It brings in SdlSharp and ImguiSharp.Redist. The packaged ImGui native binaries target Windows and macOS.

To try SDL3 from source, install the .NET 10 SDK and run `dotnet run --project Samples/Lifecycle`. For an interactive GPU example, run `dotnet run --project Samples/ImGuiDemo`.

SDL3 is a breaking change for SDL2 consumers. Start with the [SDL3 samples](https://github.com/panopticoncentral/sdl-sharp/tree/main/Samples). Dispose acquired camera frames, pixel locks, and GPU maps when finished; borrowed views are valid only while their native owner is alive. The [API and ownership notes](https://github.com/panopticoncentral/sdl-sharp/blob/main/SDL_REVIEW.md) cover the remaining migration details.

See the [release instructions](https://github.com/panopticoncentral/sdl-sharp/blob/main/docs/releasing.md) for package versions, validation, and publishing.
