# Releasing the managed SDL3 packages

`main` ships `SdlSharp` and `SdlSharp.ImGui` together. Change the shared version in [src/Directory.Build.props](../src/Directory.Build.props) in a reviewed commit before a release. Keep prereleases explicitly versioned. SDL2 stays on the legacy 0.x line on `sdl2`; native redistributables have their own release processes.

The [GitHub `nuget` environment](https://github.com/panopticoncentral/sdl-sharp/settings/environments) permits only the `main` branch and requires approval from `panopticoncentral`. Self-review is allowed so the maintainer can approve their own dispatch; administrator bypass is disabled.

The [NuGet trusted publisher](https://www.nuget.org/account/trustedpublishing) named **SDL3 managed packages** is active for `panopticoncentral/sdl-sharp`, workflow `release.yml`, environment `nuget`. It permits pushing new packages and versions only for the exact IDs `SdlSharp` and `SdlSharp.ImGui`. No stored API key is required.

1. From Actions → **Release SDL3 packages**, select `main` and leave **Publish** unchecked for a rehearsal. The workflow builds and tests on Windows and macOS, packs both libraries, and runs a fresh consumer that loads both native dependencies. Download the `managed-packages` artifact to inspect the exact packages.
2. For an approved release, dispatch again from `main` with **Publish** checked. After both CI jobs pass, the publish job sends that run's tested artifact to NuGet.org. Normal pushes, PRs, and tags do not publish. NuGet package versions are immutable: retry a partial upload from the same commit; use a new version for changed contents.
