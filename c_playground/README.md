# Compilation Steps:
1. Compile cpp source code: `$ g++ -c heyDLL.cpp -Wno-write-strings`
1. Link stage with DLL generation: `$ g++ -shared -o heyDLL.dll heyDLL.o` 
