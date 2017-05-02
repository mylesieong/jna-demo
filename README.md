# Index
- **2017-04-28**: Try the JNI but find out JNI is suitable for Java x NativeC co-develope scenario but not suitbale for our case (DLL library built independently). So I explore JNA (Java Native Access) project and found it relevant.
- **2017-05-02**: Ping the JNA with Maven Project Structure. 
    1. Build:   `$(project_home) mvn clean package`
    1. Invoke:  `$(project_home) java -cp target/jna-demo_xxx.jar com.myles.demo.App` 
    - note. The DLL should be proven with `$ file {dll_name}.dll` that its 32/64bit
    - note. The DLL must be put at the directory where the JVM runs. If not, change the command to this `$(project_home) java -Djna.library.path={path_to_dll_folder} -cp target/jna-demo_xxx.jar com.myles.demo.App` 
    - note. Run the JVM in cygwin-bash, if use git-bash, some system depenedency would be missed.
- **2017-05-02**: 
    - The original code is using C#, maybe we can try to **ping the g++ compile C# as dll** possibility? 
    - Now reading the C++ tutorial, some retrofit will be made on the C# code if we stick to C++
    - The deadline is monday update
