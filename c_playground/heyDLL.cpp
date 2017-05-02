#include "heyDLL.h"
 
#include <stdexcept>
 
using namespace std;
 
int multiply (float a, float b, float & result)
{
	result = a * b;
	return 0;
}
 
int sumArray (float a [], int length, float & result) {
	result = 0;
	for(int i=0; i<length; i++) {
		result += a[i];
	}
	return 0;
}
 
char * getVersion()
{
	return "1.0.0";
}
 
int addValue (float a [], int length, float value)
{
	for (int i = 0; i < length; i++) {
		a[i] += value;
	}
	return 0;
}
