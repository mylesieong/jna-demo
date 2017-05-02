extern "C" {
 
int __declspec(dllexport) multiply (float a, float b, float & result);
int __declspec(dllexport) sumArray (float a [], int length, float & result);
char * __declspec(dllexport) getVersion();
int __declspec(dllexport) addValue (float a [], int length, float value);
 
}