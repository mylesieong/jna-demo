package com.myles.demo;

import com.sun.jna.Library;
import com.sun.jna.Native;
import com.sun.jna.ptr.FloatByReference;

public class App{
    
	public interface DllInterface extends Library {
		DllInterface INSTANCE = (DllInterface)Native.loadLibrary("pingLM", DllInterface.class);
		String decode(String filename);
	}

	public String decode(String filename) {
		String v = DllInterface.INSTANCE.decode(filename);
		return v;
	}
 
    public static void main(String[] args) {
		App t = new App();
		System.out.println(t.decode("Name of Image File"));
    }

}
