# SdlSharp SDL3 — current status and remaining work

Updated November 13, 2026. SDL3 development is on `main`; the previous SDL2 implementation is maintained on `sdl2`.

The core desktop facade is implemented against **SDL 3.4.18**, supplied by **SdlSharp.Redist 1.0.1**, on **.NET 10**. The managed ImGui facade targets **Dear ImGui 1.93.0 / ABI 1**, using **ImguiSharp.Redist 0.4.0-preview.3**. Implementation coverage is complete within the declared scope; cross-platform and hardware validation and managed release preparation remain.

Architecture rules are in [CLAUDE.md](CLAUDE.md). Ownership contracts and API changes are in [SDL_REVIEW.md](SDL_REVIEW.md). The SDL [inventory](INVENTORY.md) and ImGui [inventory](src/SdlSharp.ImGui/INVENTORY.md) describe the API surface; the generated coverage manifests are authoritative for function accounting.

## Completed implementation

- [x] Foundation, application lifecycle, exceptions, properties, hints, logging callbacks, assertions, time, and system utilities.
- [x] Windows/displays, surfaces/palettes, 2D rendering/textures, graphics formats, OpenGL contexts, Metal views, and Vulkan loader/surfaces.
- [x] GPU devices/resources, command buffers, render/compute/copy passes, fences, transfer-buffer mappings, and custom GPU renderer state.
- [x] Events, keyboard/mouse/touch/pen input, gamepads/joysticks, virtual joysticks, sensors, cameras, haptics, dialogs, clipboard, and trays.
- [x] Audio devices/streams, callbacks, planar and no-copy data, WAV loading, and scoped locks.
- [x] Managed stream bridges for SDL image/audio/mapping codecs, with leave-open behavior and managed exception propagation.
- [x] All 23 HID functions and 17 storage functions, including managed custom storage backends and deterministic regression fixtures.
- [x] Shared native identity, borrowed views, parent invalidation, retained dependencies, disposable acquisitions, checked buffers, and callback exception containment.
- [x] ImGui managed API, owned draw lists/submissions/textures/font atlases, scopes, callbacks, lifetime checks, SDL platform/GPU backends, and expanded managed demo.
- [x] Consume generated ImGui imports, enums, UTF-8 adapters, and ABI probes from the restored native package; remove duplicate generated sources and the old local generator.
- [x] SDL ABI/export/regression suite, ImGui regression suites, and source coverage checks.

The former implementation phases 1–11 are complete within the scope below. The old missing-wrapper counts, deferred HID/storage/virtual-joystick entries, ImGui 0.2.0 checklist, and request to refresh the old ImGui inventory are superseded.

## Coverage and intentional scope

| SDL 3.4.18 classification | Functions |
|---|---:|
| Imported with a managed implementation reference | 899 |
| Deliberately native-only | 3 |
| Explicit .NET equivalents | 293 |
| Other intentional equivalents/exclusions | 30 |
| Platform/bootstrap exclusions | 45 |
| Missing or unclassified core functions | 0 |
| Total declared functions | 1,270 |

There are **902 distinct SDL imports** and **266 hint constants**. The [SDL audit](audit/sdl3-coverage-3.4.18.json) records each decision. The [ImGui audit](src/SdlSharp.ImGui/API_COVERAGE.json) accounts for all **923 exports**: 862 implementation/ABI references and 61 explicit equivalents or exclusions. Source references establish coverage, not runtime correctness on every platform.

.NET supplies ordinary stream transport, binary primitives, threading, timers, filesystem, and process operations. Platform-specific bootstrap/system/EGL APIs remain outside this desktop facade. SDL_image, SDL_mixer, and SDL_ttf are separate libraries with no managed projects here; they are future scope, not unclassified SDL core gaps.

## October 22 review fixes

- [x] Calculate packed pixel rows from storage bytes, including padding in XRGB4444/XRGB1555; preserve bit packing for indexed formats. Reject short surface/texture buffers before native access.
- [x] Guard stream codec and audio-memory completion callbacks against destructive reentrancy, preserving managed exception reporting.
- [x] Allow borrowed window views to be disposed while native leases or callbacks are active. This also fixes parent-view cleanup in properties-based asynchronous dialogs.
- [x] Tie GPU-device and joystick/gamepad property views to native lifetimes, including the shared joystick property group and gamepad-owned joystick references.
- [x] Keep independently opened native references alive when their identity was first observed through a borrowed parent.
- [x] Reject clipboard text replacement from a clipboard data callback.
- [x] Make parallel regression assertion counts thread-safe.
- [x] Configure Windows/macOS CI for SDL3 changes to `main`, including package-consumer tests.
- [x] Verify a fresh public NuGet restore. The clone fix is already in the referenced ImGui native package; publishing a native fix is no longer a prerequisite.

## Validation

Verified on October 22 on macOS ARM64 after restoring into a fresh cache from public NuGet:

- Release solution and all samples: **0 warnings, 0 errors**.
- SDL: **4,239 ABI/export checks and 3,232 managed checks**, zero failures.
- ImGui: **92 high-level checks and 435 final-review checks**, plus the ABI/export, context, drawing, and lifetime suite, all passing with normal package resolution.
- Both API coverage checks pass with no unreviewed gaps; `git diff --check` is clean.

The reproducible commands are:

```sh
dotnet restore SdlSharp.slnx
python3 scripts/check-imgui-model-coverage.py
dotnet build SdlSharp.slnx -c Release --no-restore -m:1 \
  -p:UseSharedCompilation=false -p:GeneratePackageOnBuild=false
python3 scripts/test-sdl.py --headers /path/to/SDL3-3.4.18/include
dotnet tests/SdlSharp.ImGui.Tests/bin/Release/net10.0/SdlSharp.ImGui.Tests.dll
```

The SDL script compiles an independent C probe covering 116 structures, nine scalar ID wrappers, enum values/sizes, offsets, alignment, and callback interfaces. Managed tests use dummy video/audio, virtual input, temporary storage, and deterministic lifetime fixtures. ImGui tests use the published native package and real native widgets with headless frames. See the [SDL test instructions](tests/SdlSharp.Tests/README.md) and [ImGui test instructions](tests/SdlSharp.ImGui.Tests/README.md) for limitations.

The November 13 promotion also verified the Release build, both regression suites, and a clean .NET 10 app consuming the two local NuGet packages on macOS ARM64.

## Remaining before a managed release

- [x] Commit the reviewed implementation, regression tests, documentation, and CI changes on `sdl3`.
- [x] [SDL3 CI on main](https://github.com/panopticoncentral/sdl-sharp/actions/runs/43569471678) passed on Windows and macOS, including package-consumer tests. The [SDL2 maintenance build](https://github.com/panopticoncentral/sdl-sharp/actions/runs/43569309698) passed on Windows.
- [ ] Run `ImGuiDemo` end to end on actual GPU backends: rendering, keyboard/mouse/scroll input, resize, fonts, drag/drop, custom draw lists/pipelines, and shutdown.
- [ ] Validate native GPU submission, window/swapchain behavior, and Metal/Vulkan integration on the intended platforms.
- [ ] Exercise physical cameras/haptics/HID devices and BLE pairing, native dialogs/trays, and desktop clipboard interaction. Headless tests cover their managed contracts, not these integrations.
- [ ] Validate Steam/cloud or console storage with the relevant host SDK/backend before advertising support for those integrations.
- [ ] Record runtime results for each architecture claimed by the managed release; the current CI matrix does not exercise every native package RID.
- [x] Update package-facing README/migration guidance for .NET 10 and the intentional alpha API breaks (disposable camera frames, locks/maps, guarded events, strict GUID/time handling, and ImGui ownership).
- [x] Configure and verify the GitHub `nuget` environment and the active NuGet trusted publisher for both managed package IDs.
- [ ] Approve the preview version and perform the first authorized publish; the live OIDC exchange and package upload remain untested. See the [release instructions](docs/releasing.md).

## Follow-up scope

- [ ] Linux `imgui_sharp` native packaging and runtime validation remain in the sibling `imgui-sharp-native` project. The current native package supplies Windows and macOS RIDs.
- [ ] Decide separately whether to add managed SDL_image, SDL_mixer, SDL_ttf, or additional platform/bootstrap APIs.

No managed package is published by this review. The native dependencies are publicly available; the outstanding release work concerns this managed branch and its platform validation.
