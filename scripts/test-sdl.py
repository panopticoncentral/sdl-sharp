#!/usr/bin/env python3
"""Compile the independent ABI probe and run SDL's managed regression suite."""
import argparse
from pathlib import Path
import subprocess
import shutil
import os
import sys
import tempfile

p = argparse.ArgumentParser(description=__doc__)
p.add_argument('--headers', required=True, type=Path, help='SDL include directory containing SDL3/')
p.add_argument('--configuration', default='Release')
p.add_argument('--library', type=Path)
a = p.parse_args()
root = Path(__file__).resolve().parents[1]
def run(*command, **kwargs):
    return subprocess.run(command, cwd=root, check=True, **kwargs)
run(sys.executable, 'scripts/check-sdl-coverage.py', '--headers', str(a.headers / 'SDL3'))
run(sys.executable, 'scripts/generate-sdl-abi-probe.py', '--headers', str(a.headers))
with tempfile.TemporaryDirectory(prefix='sdl-abi-') as temporary:
    build = Path(temporary)
    if shutil.which('cmake'):
        run('cmake', '-S', 'tests/SdlSharp.Tests', '-B', str(build), '-DSDL_INCLUDE_DIR=' + str(a.headers.resolve()))
        run('cmake', '--build', str(build), '--config', a.configuration)
        binaries = [f for f in build.rglob('sdl-abi-probe*') if f.is_file() and (f.name == 'sdl-abi-probe' or f.suffix == '.exe')]
        if len(binaries) != 1:
            raise RuntimeError(f'Expected one ABI probe executable, found {binaries}')
        binary = binaries[0]
    else:
        binary = build / 'sdl-abi-probe'
        run(os.environ.get('CC', 'cc'), '-I', str(a.headers.resolve()), 'tests/SdlSharp.Tests/native-probe.c', '-o', str(binary))
    output = build / 'abi.txt'
    output.write_text(run(str(binary), capture_output=True, text=True).stdout)
    command = ['dotnet', str(root / 'tests/SdlSharp.Tests/bin' / a.configuration / 'net10.0/SdlSharp.Tests.dll'), str(output)]
    if a.library:
        command.append(str(a.library.resolve()))
    run(*command)
