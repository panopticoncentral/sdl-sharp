#!/usr/bin/env python3
"""Account for every native export as a managed implementation or an explicit .NET design decision."""
import argparse
import json
from pathlib import Path
import re
import subprocess

parser = argparse.ArgumentParser(description=__doc__)
parser.add_argument('--write', action='store_true', help='Update the generated coverage manifest after reviewing API_DECISIONS.json')
args = parser.parse_args()
root = Path(__file__).resolve().parents[1] / 'src/SdlSharp.ImGui'
# Ask MSBuild for the package imported by this project instead of guessing
# the NuGet cache location or keeping a second generated contract in this repo.
result = subprocess.run(
    ['dotnet', 'msbuild', str(root / 'SdlSharp.ImGui.csproj'), '-nologo',
     '-getProperty:ImguiSharpBindingsDirectory'], capture_output=True, text=True)
if result.returncode:
    raise SystemExit(result.stdout + result.stderr)
bindings = Path(result.stdout.strip())
if not result.stdout.strip() or not (bindings / 'Native.cs').is_file():
    raise SystemExit('Restore SdlSharp.ImGui with ImguiSharp.Redist 0.4.0-preview.3 or later '
                     'before checking coverage (dotnet restore SdlSharp.slnx).')
exports = set(re.findall(r'EntryPoint = "(IGSharp_\w+)"', (bindings / 'Native.cs').read_text()))
references = {}
# Layout probes count as ABI implementations, just as they did when generated
# locally. Preserve their filenames in the coverage manifest.
for path in [*sorted(root.glob('*.cs'), key=lambda p: p.name), bindings / 'Native.Layouts.cs']:
    if path.name in ('Native.cs', 'Native.Utf8.cs', 'Enums.cs'):
        continue
    for symbol in set(re.findall(r'\b(IGSharp_\w+)\s*\(', path.read_text())) & exports:
        references.setdefault(symbol, []).append(path.name)
decisions = json.loads((root / 'API_DECISIONS.json').read_text())
missing = exports - references.keys() - decisions.keys()
stale = decisions.keys() - exports
redundant = decisions.keys() & references.keys()
if missing or stale or redundant:
    for label, values in [('Unaccounted exports', missing), ('Removed exports', stale), ('Decisions now implemented directly', redundant)]:
        if values: print(label + ':\n  ' + '\n  '.join(sorted(values)))
    raise SystemExit(1)
entries = {name: {'implementation': references[name]} if name in references else {'design': decisions[name]} for name in sorted(exports)}
output = json.dumps(entries, indent=2, ensure_ascii=False) + '\n'
manifest = root / 'API_COVERAGE.json'
if args.write:
    manifest.write_text(output)
elif not manifest.exists() or manifest.read_text() != output:
    raise SystemExit('API_COVERAGE.json is stale. Review changes and run this script with --write.')
print(f'{len(exports)} exports accounted for: {len(references)} direct implementations/ABI checks, {len(decisions)} explicit equivalents or exclusions; no unreviewed gaps.')
