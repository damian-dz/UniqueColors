using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace UniqueColors
{
    internal class Program
    {
        private static void Main()
        {
            // load a test image
            var bmp = new Bitmap(@"F:\Test images\test.bmp");
            // call the method to count unique colors
            int cnt = CountColors(bmp);
            // display the result
            Console.WriteLine("There are " + cnt + " unique colors in the image");
        }

        private static unsafe int CountColors(Bitmap bmp)
        {
            // get the dimensions of the bitmap image
            int width = bmp.Width;
            int height = bmp.Height;
            // lock the bitmap in system memory
            var rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            // create a hashset to store unique integer values
            var colors = new HashSet<int>();
            // get the address of the first pixel data in unmanaged memory
            var bmpPtr = (int*)bmpData.Scan0;
            // loop through the pixel data
            for (int i = 0; i < width * height; i++)
            {
                // add the 32-bit value to the hashset
                colors.Add(bmpPtr[0]);
                // move the pointer by 4 bytes
                bmpPtr++;
            }
            // unlock the bitmap from system memory
            bmp.UnlockBits(bmpData);
            // return the unique color count
            return colors.Count;
        }
    }
}
