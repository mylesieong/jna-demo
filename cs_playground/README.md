# Compile C# code with C# Compiler *csc*

## Compiles to executables
1. Build *.cs file
1. run cmd: `csc /out:pgm.exe csdemo.cs`
1. run executable: `./pgm`

## Compiles to DLL library
1. Build *.cs file
1. run cmd: `csc /target:library csdemo.cs`

## External Dependency Injection
- Dependency in *using xxx*: `csc /out:pgm.exe pingLM.cs /r:FreeImageNET.dll`
- Dependency in [DllImport...], because it is a runtime dll binding, so only need to ensure that dll is available in the same directory that pgm.exe is invoked.
