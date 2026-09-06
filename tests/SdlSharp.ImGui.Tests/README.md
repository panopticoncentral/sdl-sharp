# Local ImGui integration check

A dependency-free console regression test (nonzero exit on failure), using the
actual managed binding and either NuGet runtime resolution or an explicitly selected native shared library.
It checks mirrored struct sizes, standalone drawing, cloning, reset/reuse on a
second frame, disposal guards, and availability of both new native exports.
Custom GPU pipeline rendering itself needs a GPU and is not exercised here.

Build the sibling native repo first, then build this project:

```sh
dotnet build tests/SdlSharp.ImGui.Tests/SdlSharp.ImGui.Tests.csproj -c Release -p:GeneratePackageOnBuild=false
```

To verify the referenced NuGet package using normal runtime resolution, run without arguments:

```sh
dotnet tests/SdlSharp.ImGui.Tests/bin/Release/net10.0/SdlSharp.ImGui.Tests.dll
```

To verify a local native build, pass an absolute library path when running. Put the matching SDL3 runtime next
to that library. On macOS, use the signed SDL3 binary from SdlSharp.Redist,
which the build places under the test output's runtimes directory. The native
repo's extracted build-time SDL framework binary may not have a valid signature.

Example from the sdl-sharp repo on Apple Silicon (adjust the native build path):

```sh
stage=$(mktemp -d)
cp ../imgui-sharp-native/build/libimgui_sharp.dylib "$stage/"
cp tests/SdlSharp.ImGui.Tests/bin/Release/net10.0/runtimes/osx-arm64/native/libSDL3.dylib "$stage/"
dotnet tests/SdlSharp.ImGui.Tests/bin/Release/net10.0/SdlSharp.ImGui.Tests.dll "$stage/libimgui_sharp.dylib"
```

With an explicit path, the resolver bypasses the packaged ImguiSharp.Redist copy.
Without an argument, no resolver override is installed and the test verifies the
referenced NuGet package. These additions require ImguiSharp.Redist 0.3.0-preview.2
or later.
