# SDL wrapper regression tests

Dependency-free .NET 10 executable tests for SDL 3.4.18. The independently compiled C probe checks 116 concrete structures, nine scalar IDs, enum widths/values, native wchar_t width, fields, alignment, and callback interfaces against the managed declarations. Runtime checks verify every imported export.

From the repository root:

```sh
dotnet restore SdlSharp.slnx
dotnet build SdlSharp.slnx -c Release --no-restore -p:GeneratePackageOnBuild=false
python3 scripts/test-sdl.py --headers /path/to/SDL3-3.4.18/include
dotnet tests/SdlSharp.ImGui.Tests/bin/Release/net10.0/SdlSharp.ImGui.Tests.dll
```

Use the headers matching SdlSharp.Redist 1.0.1. The test script uses CMake when available, otherwise `CC` or `cc`. CMake chooses the configured native compiler on Windows; compile the probe for the same architecture as .NET. `--library /absolute/path/to/SDL3` selects a specific library. Otherwise tests use the package runtime asset on Windows/macOS or `libSDL3.so.0` on Linux.

The suite chooses dummy video/audio drivers and creates hidden dummy windows. Clipboard tests exercise SDL's dummy clipboard, not the desktop clipboard. No physical controller is needed: virtual joystick tests attach and detach their own test device. Camera-frame, tray-tree, and GPU command-state tests use deterministic lifetime fixtures without dereferencing fabricated native device handles. They complement real SDL surface, texture, audio, stream, property, callback, and event operations.

Tests cover parent disposal, aliases, repeated lease disposal, active-resource shutdown rejection, partial rows, short planes, geometry indices/strides, exception barriers, late callback tokens, registration failure, GC pinning, non-seekable streams, strict GUID/time conversions, and application restart. See [the review](../../SDL_REVIEW.md) for API contracts and scope.

Follow-up regressions cover padded RGB storage widths and sub-byte indexed rows, stream/audio-completion/clipboard callback reentrancy, borrowed-window cleanup, shared joystick/gamepad properties and native references, and independently opened references first observed through borrowed views. Video-dependent fixtures explicitly select SDL's dummy driver for each application session; they do not touch the desktop clipboard.

Physical HID transfers, BLE pairing, cloud/console storage, native GPU rendering, physical camera capture/haptics, native file dialogs/trays, Metal/Vulkan integration, and desktop clipboard interaction need separate integration tests. Local verification does not establish correctness on every supported architecture. The CI workflow adds Windows/macOS runs without packing or publishing.

After an intentional binding change, regenerate the probe and review coverage changes before running `check-sdl-coverage.py --write`. The coverage checker records .NET equivalents and explicit platform exclusions; it does not claim all SDL subsystems are implemented.

`HidStorageTests` runs real local/title-storage operations in a temporary directory, custom `StorageBackend` callbacks through SDL, native HID initialization/enumeration/open-failure paths, and an injected HID device transport. It verifies 16-bit and 32-bit wchar encoding, report IDs/lengths/timeouts, detached information, parent leases, callback errors, close failures, iterator cleanup, repeated disposal, and concurrent call serialization. It never sends output/feature reports to real hardware, opens a cloud save, or writes user save data. Tests delete their temporary local files.
