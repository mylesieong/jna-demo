using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using FreeImageAPI;
using System.Drawing.Imaging;

class HelloWorldWin {
    static void Main(string[] args) {
        string filename = args[0];
        FREE_IMAGE_FORMAT fif = FREE_IMAGE_FORMAT.FIF_UNKNOWN;
        fif = FreeImage.GetFileType(filename, 0);
        if (fif == FREE_IMAGE_FORMAT.FIF_UNKNOWN)
        {
            fif = FreeImage.GetFIFFromFilename(filename);
        }
        FIBITMAP img = FreeImage.Load(fif, filename, 0);
        ImageData pImageData = new ImageData();
        pImageData.bitcount = FreeImage.GetBPP(img);


        //If NOT IN (1,8,24) bit image, transfer to 24 bit.
        if (pImageData.bitcount != 1 && pImageData.bitcount != 8
            && pImageData.bitcount != 24)
        {
            img = FreeImage.ConvertTo24Bits(img);
            pImageData.bitcount = FreeImage.GetBPP(img);
        }
        pImageData.is_dark_pixel_bit1 = 1;
        pImageData.img_w = FreeImage.GetWidth(img);
        pImageData.img_h = FreeImage.GetHeight(img);
        pImageData.row_bytes = FreeImage.GetPitch(img);
        pImageData.praw_data = FreeImage.GetBits(img);

        int pImageDataSize = Marshal.SizeOf(pImageData);
        IntPtr pImageDataIntPtr = Marshal.AllocHGlobal(pImageDataSize);
        Marshal.StructureToPtr(pImageData, pImageDataIntPtr, true);

        int pResultSize = Marshal.SizeOf(typeof(DecodeResult));
        IntPtr pResultIntPtr = Marshal.AllocHGlobal(pResultSize);

        //Decode
        int result = LM_ImageData_Decode(pImageDataIntPtr, 0, ref pResultIntPtr, 1);
        if (result == 1)
        {
            DecodeResult[] infos = new DecodeResult[1];
            IntPtr ptr = (IntPtr)((UInt32)pResultIntPtr + 0 * pResultSize);
            infos[0] = (DecodeResult)Marshal.PtrToStructure(ptr, typeof(DecodeResult));
            string resultInfo = System.Text.Encoding.UTF8.GetString(infos[0].pdata_buf);
            Console.WriteLine(resultInfo);
        }
        else
        {
            Console.WriteLine("Result error");
        }
        FreeImage.Unload(img);
        Marshal.FreeHGlobal(pResultIntPtr);   
        Marshal.FreeHGlobal(pImageDataIntPtr);
    }

    [DllImport("LM_Decoder.dll", EntryPoint = "LM_ImageData_Decode")]
    static extern int LM_ImageData_Decode(IntPtr pImageData, Int32 pRect,ref IntPtr pResult, int nMaxResultNum);

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct DecodeResult
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4096)]
        public byte[] pdata_buf;
        public UInt32 buf_len;
        public UInt32 data_len;
        //public byte[] pReserved;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct ImageData
    {
       public UInt32 img_w;
       public UInt32 img_h;
       public UInt32 row_bytes;
       public UInt32 bitcount;
       public UInt32 is_dark_pixel_bit1;
       public IntPtr praw_data;
    }
    
}
