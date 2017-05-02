package com.myles.demo;

import com.sun.jna.Library;
import com.sun.jna.Native;
import com.sun.jna.ptr.FloatByReference;

public class App{
    
	public interface DllInterface extends Library {
		DllInterface INSTANCE = (DllInterface) Native.loadLibrary("simpleDLL", DllInterface.class);
		int multiply(float a, float b, FloatByReference result);
		int sumArray(float[] a, int length, FloatByReference result);
		String getVersion ();
		int addValue (float a [], int length, float value);
	}

    public static void main(String[] args) {
		System.out.println("SimpleDll r: ");
    }

}
