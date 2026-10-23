#!/usr/bin/env python3
from pathlib import Path
import re
import argparse
p=argparse.ArgumentParser(description='Generate C ABI probes from the native C# declarations.')
p.add_argument('--headers',type=Path,required=True,help='Include root containing SDL3/')
a=p.parse_args()
r=Path(__file__).resolve().parents[1];inc=a.headers
cs='\n'.join(p.read_text() for p in (r/'src/SdlSharp/Native').glob('*.cs'))
headers='\n'.join(p.read_text() for p in (inc/'SDL3').glob('SDL_*.h') if not p.name.startswith('SDL_opengl'))
headers=re.sub(r'/\*.*?\*/','',headers,flags=re.S)
code=['#include <SDL3/SDL.h>','#include <stdio.h>','#include <stddef.h>','int main(void) {']
code.append('printf("wchar-size|wchar_t|%zu\\n", sizeof(wchar_t));')
count=0
for m in re.finditer(r'public (?:unsafe |readonly )*struct (SDL_\w+)\s*\{([^{}]*)\}',cs,re.S):
 name,body=m.groups()
 if not re.search(r'(?:struct|union)\s+'+name+r'\s*\{',headers):continue
 code.append(f'printf("size|{name}|%zu\\n", sizeof({name}));')
 code.append(f'printf("alignment|{name}|%zu\\n", offsetof(struct {{ char first; {name} value; }}, value));')
 for field in re.findall(r'public\s+(?:fixed\s+)?(?:delegate\*\s+unmanaged\[[^\]]+\]<[^;\n]+>|[\w*<>]+)\s+(@?\w+)(?:\[\d+\])?\s*;',body):
  field=field.removeprefix('@')
  native_field={('SDL_Event','editCandidates'):'edit_candidates',('SDL_GUID','data0'):'data[0]',('SDL_GUID','data1'):'data[8]',('SDL_PixelFormatDetails','padding0'):'padding[0]',('SDL_PixelFormatDetails','padding1'):'padding[1]'}.get((name,field),field)
  if name=='SDL_MessageBoxColorScheme':native_field='colors['+str(['background','text','button_border','button_background','button_selected'].index(field))+']'
  code.append(f'printf("offset|{name}|{field}|%zu\\n", offsetof({name}, {native_field}));')
  if name not in ['SDL_GUID', 'SDL_MessageBoxColorScheme', 'SDL_PixelFormatDetails']:
   code.append(f'printf("field-size|{name}|{field}|%zu\\n", sizeof((({name}*)0)->{native_field}));')
 count+=1
for name,body in re.findall(r'public enum (SDL_\w+)(?:\s*:\s*\w+)?\s*\{([^{}]+)\}',cs,re.S):
 code.append(f'printf("size|{name}|%zu\\n", sizeof({name}));')
 body=re.sub(r'//[^\n]*','',body)
 for val in re.findall(r'\b(SDL_\w+|SDLK_\w+|SDL_KMOD_\w+)\s*(?:=|,|$)',body):
  code.append(f'printf("enum|{name}|{val}|%llu\\n", (unsigned long long){val});')
for name in re.findall(r'public readonly record struct (SDL_\w+)\(\w+ Value\);',cs):
 code.append(f'printf("size|{name}|%zu\\n", sizeof({name}));')
 code.append(f'printf("alignment|{name}|%zu\\n", offsetof(struct {{ char first; {name} value; }}, value));')
code+=['return 0;','}']
(r/'tests/SdlSharp.Tests').mkdir(exist_ok=True)
(r/'tests/SdlSharp.Tests/native-probe.c').write_text('\n'.join(code)+'\n')
print(count,'structs',len(code),'probe lines generated')
