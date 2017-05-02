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

	public float multiply(float a, float b) {
		FloatByReference r = new FloatByReference (0);
		int rc = DllInterface.INSTANCE.multiply (a, b, r);
		return r.getValue ();
	}
 
	public float sumArray(float[] a) {
		FloatByReference r = new FloatByReference (0);
		int l = a.length;
		int rc = DllInterface.INSTANCE.sumArray (a, l, r);
		return r.getValue ();
	}
 
	public String getVersion () {
		String v = DllInterface.INSTANCE.getVersion ();
		return v;
	}
 
	public void addValue (float[] a, float v) {
		int l = a.length;
		DllInterface.INSTANCE.addValue (a, l, v);
	}
 
    public static void main(String[] args) {
		App t = new App();
		float r = t.multiply(3.1f, 3f);
		System.out.println("SimpleDll r: " + r);
    }

}
