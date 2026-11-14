#!/usr/bin/env python3
"""Run a .NET 10 application using only the two freshly packed managed packages."""
import argparse
from pathlib import Path
import subprocess
import tempfile
import xml.etree.ElementTree as ET
from zipfile import ZipFile

parser = argparse.ArgumentParser(description=__doc__)
parser.add_argument('feed', type=Path)
feed = parser.parse_args().feed.resolve()
versions = {}
for package in feed.glob('*.nupkg'):
    with ZipFile(package) as archive:
        nuspec, = (n for n in archive.namelist() if n.endswith('.nuspec'))
        metadata = ET.fromstring(archive.read(nuspec)).find('{*}metadata')
        name, version = (metadata.findtext('{*}' + k) for k in ('id', 'version'))
        if name in versions:
            raise SystemExit(f'Multiple versions of {name} in {feed}')
        versions[name] = version
if set(versions) != {'SdlSharp', 'SdlSharp.ImGui'} or len(set(versions.values())) != 1:
    raise SystemExit(f'Expected matching core and ImGui packages, got {versions}')

with tempfile.TemporaryDirectory(prefix='sdl-consumer-') as temp:
    root = Path(temp) / "app"
    root.mkdir()
    config = ET.fromstring("""<configuration><packageSources><clear/></packageSources>
      <packageSourceMapping><clear/>
        <packageSource key="built"><package pattern="SdlSharp"/><package pattern="SdlSharp.ImGui"/></packageSource>
        <packageSource key="public"><package pattern="*"/></packageSource>
      </packageSourceMapping></configuration>""")
    for key, source in [('built', str(feed)), ('public', 'https://api.nuget.org/v3/index.json')]:
        ET.SubElement(config.find('packageSources'), 'add', key=key, value=source)
    ET.ElementTree(config).write(root / 'NuGet.Config', encoding='utf-8')
    (root / 'Consumer.csproj').write_text(f'''<Project Sdk="Microsoft.NET.Sdk">
      <PropertyGroup><TargetFramework>net10.0</TargetFramework><OutputType>Exe</OutputType></PropertyGroup>
      <ItemGroup><PackageReference Include="SdlSharp.ImGui" Version="[{versions['SdlSharp.ImGui']}]"/></ItemGroup>
    </Project>''')
    (root / 'Program.cs').write_text("""using SdlSharp;
using SdlSharp.ImGui;
using var app = new Application(InitFlags.Events);
using var context = Context.Create();
System.Console.WriteLine($"SDL {Sdl.Version}, ImGui context active: {context.IsCurrent}");
return Sdl.Version.Major == 3 && context.IsCurrent ? 0 : 1;
""")
    subprocess.run(['dotnet', 'restore', '--configfile', 'NuGet.Config', '--packages', str(Path(temp) / 'cache')], cwd=root, check=True)
    subprocess.run(['dotnet', 'run', '-c', 'Release', '--no-restore'], cwd=root, check=True)
