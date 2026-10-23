# ImGui high-level integration tests

A dependency-free .NET 10 console suite; failures exit nonzero. It uses the real managed/native bindings and SDL's dummy video driver, so it needs no visible window or GPU.

Coverage includes default numeric widgets, fixed/growing/string text editing over multiple frames (including `EnterReturnsTrue`), repeated size callbacks, UTF-8 filtering and IDs, balanced UI scopes, shared atlases, failed font loads, glyph/command snapshots, callback retention across frames, retained render/texture containers, nested platform-handler restoration, callback exceptions, thread/context guards, and backend/resource disposal ordering. Native ABI layouts and exports are checked as well.

```sh
dotnet restore SdlSharp.slnx
python3 scripts/check-imgui-model-coverage.py

dotnet build tests/SdlSharp.ImGui.Tests/SdlSharp.ImGui.Tests.csproj -c Release \
  -m:1 -p:UseSharedCompilation=false -p:GeneratePackageOnBuild=false

dotnet tests/SdlSharp.ImGui.Tests/bin/Release/net10.0/SdlSharp.ImGui.Tests.dll
```

The public `ImguiSharp.Redist` 0.4.0-preview.3 package supplies the native runtime, generated C# contract, and CloneOutput fix. The commands above exercise ordinary NuGet resolution; no sibling checkout or binding regeneration is required.

For a local native build, pass its absolute library path as the executable's optional argument. Place the matching SDL3 shared library next to it (`libSDL3.dylib`, `SDL3.dll`, or `libSDL3.so.0`). Explicit-path tests resolve **both** libraries from that directory. On macOS use a signed SDL3 binary, such as the one from SdlSharp.Redist. Use the updated native repository for debug validation, including its CloneOutput fix; test both Debug and Release configurations when changing native code.

To test an unpublished native package, regenerate and pack it in `imgui-sharp-native`, including its native runtime under `runtimes/<rid>/native/`. Restore from the resulting feed with an isolated cache, then use the build/test commands above:

```sh
dotnet restore SdlSharp.slnx \
  --source /absolute/path/to/imgui-sharp-native/packages \
  --source https://api.nuget.org/v3/index.json \
  --packages /absolute/path/to/isolated-packages
```

Use a fresh cache when repacking the same unpublished version. The coverage checker reads the restored package selected by MSBuild, including packages restored outside the default NuGet cache.

These tests do not publish packages. Actual hardware GPU command submission remains a separate manual integration check. The native repository also contains the C++ wrapper/backend-forwarding tests and the exhaustive P/Invoke suite.
