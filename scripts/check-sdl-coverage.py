#!/usr/bin/env python3
"""Audit exported SDL declarations against bindings; gaps remain explicit, never counted as wrappers.

Use --write only after reviewing changes against the pinned release. No network required.
This is a source reference inventory, not a semantic correctness proof; run the ABI and managed tests too.
"""
import argparse
import collections
import hashlib
import json
from pathlib import Path
import re

ROOT = Path(__file__).resolve().parents[1]
p = argparse.ArgumentParser(description=__doc__)
p.add_argument('--headers', required=True, type=Path, help='Directory containing SDL_version.h (SDL3/)')
p.add_argument('--write', action='store_true')
a = p.parse_args()

def strip_comments(text):
    return re.sub(r'//[^\n]*|/\*.*?\*/', '', text, flags=re.S)

native = {f.relative_to(ROOT).as_posix(): strip_comments(f.read_text()) for f in sorted((ROOT/'src/SdlSharp/Native').glob('*.cs'), key=lambda p: p.parts)}
managed = {f.relative_to(ROOT).as_posix(): strip_comments(f.read_text()) for f in sorted((ROOT/'src/SdlSharp').rglob('*.cs'), key=lambda p: p.parts) if 'Native' not in f.parts and 'obj' not in f.parts and 'bin' not in f.parts}
imports = {symbol: path for path, text in native.items() for symbol in re.findall(r'EntryPoint\s*=\s*"(SDL_\w+)"', text)}

bcl = {
    'SDL_iostream.h': 'System.IO.Stream, FileStream, MemoryStream, BinaryReader/BinaryWriter and BinaryPrimitives; SDL codecs use the managed Stream bridge.',
    'SDL_asyncio.h': 'System.IO asynchronous operations; no SDL async queue bridge.',
    'SDL_atomic.h': 'Interlocked/Volatile and managed synchronization.',
    'SDL_loadso.h': 'NativeLibrary.',
    'SDL_mutex.h': 'Monitor/ReaderWriterLockSlim/SemaphoreSlim/Lazy<T>.',
    'SDL_platform.h': 'RuntimeInformation.',
    'SDL_process.h': 'System.Diagnostics.Process.',
    'SDL_stdinc.h': 'BCL memory, encoding, math and collection operations; SDL_free is separately bound.',
    'SDL_thread.h': 'Thread/Task/ThreadLocal<T>.',
}
platform = {
    'SDL_system.h': 'Platform-specific lifecycle, OS/console integration; outside current desktop facade.',
    'SDL_main.h': 'C entry point/app callback/platform bootstrap plumbing; .NET owns Main on supported desktops.',
}
accepted = {
    'SDL_ReportAssertion': 'Internal assertion macro backend; SDL explicitly says not to call it directly.',
    'SDL_SetErrorV': 'C va_list API; managed errors use exceptions.',
    'SDL_RenderDebugTextFormat': 'Format managed text and use SDL_RenderDebugText.',
    'SDL_BlitSurfaceUnchecked': 'Internal unlocked blit; public checked blit is exposed.',
    'SDL_BlitSurfaceUncheckedScaled': 'Internal unlocked blit; public checked scaled blit is exposed.',
    'SDL_GetEventFilter': 'Raw callback pointer getter; managed registration state should replace it.',
    'SDL_GetDefaultLogOutputFunction': 'Raw callback pointer; needed internally if implementing scoped restoration.',
    'SDL_GetLogOutputFunction': 'Raw callback pointer; needed internally if implementing scoped restoration.',
    'SDL_TimeToWindows': 'DateTimeOffset/DateTime.ToFileTimeUtc.',
    'SDL_TimeFromWindows': 'DateTimeOffset/DateTime.FromFileTimeUtc.',
    'SDL_GetDaysInMonth': 'DateTime.DaysInMonth.',
    'SDL_GetDayOfYear': 'DateTime.DayOfYear.',
    'SDL_GetDayOfWeek': 'DateTime.DayOfWeek.',
    'SDL_AddTimer': 'System.Threading.Timer/PeriodicTimer; SDL timer ID API deliberately absent.',
    'SDL_AddTimerNS': 'Managed timers; exact SDL nanosecond scheduling semantics deliberately absent.',
    'SDL_RemoveTimer': 'Dispose managed timers.',
    'SDL_EnumerateDirectory': 'Directory.EnumerateFileSystemEntries.',
    'SDL_RemovePath': 'File.Delete/Directory.Delete.',
    'SDL_RenamePath': 'File.Move/Directory.Move.',
    'SDL_CopyFile': 'File.Copy.',
    'SDL_GetPathInfo': 'FileInfo/DirectoryInfo.',
    'SDL_GlobDirectory': 'Directory enumeration and managed matching; not identical SDL glob syntax.',
}
for symbol in ['SDL_Log','SDL_LogTrace','SDL_LogVerbose','SDL_LogDebug','SDL_LogInfo','SDL_LogWarn','SDL_LogError','SDL_LogCritical','SDL_LogMessage','SDL_LogMessageV']:
    accepted[symbol] = 'C variadic logging is not directly P/Invoke-safe; use managed logging (an SDL output adapter remains optional).'
native_only = {
    'SDL_GetAssertionHandler': 'Raw callback getter; managed registration tokens should own restoration.',
    'SDL_GetDefaultAssertionHandler': 'Raw callback getter.',
    'SDL_SetError': 'Native-only printf-format entry point; do not pass user text as a format.',
    'SDL_OutOfMemory': 'Managed code uses OutOfMemoryException.',
}
rows = []
headers = {}
for f in sorted(a.headers.glob('SDL_*.h'), key=lambda p: p.name):
    if f.name.startswith('SDL_test'): continue
    raw = f.read_text()
    text = strip_comments(raw)
    declarations = re.findall(r'extern\s+SDL_DECLSPEC\s+([^;]+?)\s*SDLCALL\s+(SDL_\w+)\s*\(([^;]*?)\)\s*;', text, re.S)
    for rt, symbol, args in declarations:
        refs = [path for path,t in managed.items() if re.search(r'\b'+symbol+r'\s*\(', t)]
        row = {'symbol': symbol, 'header': f.name, 'signature': ' '.join((rt+' ('+args+')').split())}
        if symbol in imports:
            row['native'] = imports[symbol]
            row['managedReferences'] = refs
            if refs: row['status'] = 'managed-reference'
            elif symbol in native_only: row.update(status='native-only', reason=native_only[symbol])
            else: row.update(status='managed-gap', reason='Native binding exists; no high-level call site. See SDL_REVIEW.md.')
        elif symbol in accepted: row.update(status='intentional-equivalent-or-exclusion',reason=accepted[symbol])
        elif f.name in bcl: row.update(status='bcl-equivalent',reason=bcl[f.name])
        elif f.name in platform: row.update(status='platform-scope',reason=platform[f.name])
        elif symbol in ['SDL_GDKSuspendGPU','SDL_GDKResumeGPU','SDL_GetGamepadAppleSFSymbolsNameForButton','SDL_GetGamepadAppleSFSymbolsNameForAxis'] or symbol.startswith('SDL_EGL_'):
            row.update(status='platform-scope',reason='Platform-specific graphics/OS interop remains outside the managed facade.')
        else: row.update(status='api-gap',reason='Existing omission requires an API design decision; not a .NET equivalent. See SDL_REVIEW.md.')
        rows.append(row)
    if declarations: headers[f.name] = hashlib.sha256(text.encode()).hexdigest()

version = (a.headers/'SDL_version.h').read_text()
version = '.'.join(re.search(r'#define SDL_'+n+r'_VERSION\s+(\d+)',version)[1] for n in ['MAJOR','MINOR','MICRO'])
assert version == '3.4.18', f'Review a new SDL release explicitly before changing the baseline: {version}'
known = {r['symbol'] for r in rows}
assert not imports.keys()-known, f'Imports absent from headers: {imports.keys()-known}'
# Check all hint names, including newly added ones, by their actual string values.
hints = dict(re.findall(r'^#define (SDL_HINT_\w+)\s+"([^"\n]+)"',(a.headers/'SDL_hints.h').read_text(), re.M))
managed_hints = re.findall(r'public const string \w+ = "([^"\n]+)";', (ROOT/'src/SdlSharp/HintNames.cs').read_text())
assert sorted(hints.values()) == sorted(managed_hints), 'Hint name constants drifted from SDL_hints.h'
counts = dict(sorted(collections.Counter(r['status'] for r in rows).items()))
result = {'sdlVersion':version,'redistVersion':'1.0.1','description':'Declared functions across supported and conditional platform headers; managed-reference means a call site, not a lifetime-safety guarantee. Inline helpers/compiler macros are not exports. Review findings are in SDL_REVIEW.md.','counts':counts,'headerDeclarationHashes':headers,'hintNames':hints,'functions':rows}
path = ROOT/'audit/sdl3-coverage-3.4.18.json'
encoded = json.dumps(result,indent=2)+'\n'
if a.write: path.write_text(encoded)
else:
    assert path.read_text() == encoded, 'SDL coverage baseline is stale; review changes and run --write.'
print(f'{len(rows)} declarations; {len(imports)} imports; {len(hints)} hint constants; {counts}')
