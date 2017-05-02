#include "pingLM.h"
#include "FreeImage.h"
 
#include <stdexcept>

using namespace std;

struct DecodeResult
{
    char pdata_buf[];
    int buf_len;
    int data_len;
};

struct ImageData
{
   int img_w;
   int img_h;
   int row_bytes;
   int bitcount;
   int is_dark_pixel_bit1;
   void * praw_data;
};

char * decode(char * filename)
{
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
        this.textBox1.Text = resultInfo;
    }
    else
    {
        MessageBox.Show(result+"");
    }
    FreeImage.Unload(img);
    Marshal.FreeHGlobal(pResultIntPtr);   
    Marshal.FreeHGlobal(pImageDataIntPtr);
    
	return filename;
}
