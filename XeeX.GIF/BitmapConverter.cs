using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Windows;

using System.IO;

namespace XeeX.GIF
{
    /*
     * 1. The difference between 'Bitmap' and 'BitmapSource' is that ...
     *      - BitmapSource doesn't necessarily load the bitmap until it is need.
     *      - Once the bits have been loaded they are locked and you cannot modify them without destroying the existing object and creating a new one.
     *      - A BitmapSource really is a source of bitmaps for other objects to use rather than begin a bitmap in its own rightl
     * 2. Once ypi have read a Bitmap object, you can't get the original file stream back. The Bitmap object only contains the uncompressed data, not the original data.
     *    You should read the resource as byte data instread by reading from resource stream and write to a file.
     * Reference :
     *      - [BitmapSource: WPF Bitmaps] : http://www.i-programmer.info/programming/wpf-workings/500-bitmapsource-wpf-bitmaps-1.html
     *      - 
     */

    public class BitmapConverter
    {
        /// <summary>
        /// Get Bitmap from BitmapSource
        /// </summary>
        /// <param name="source">BitmapSource</param>
        /// <returns>Bitmap</returns>
        public static Bitmap GetBitmap(BitmapSource source)
        {
            Bitmap bmp = new Bitmap(
                source.PixelWidth, 
                source.PixelHeight, 
                PixelFormat.Format32bppPArgb);

            BitmapData data = bmp.LockBits(
                new Rectangle(System.Drawing.Point.Empty, bmp.Size),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppPArgb);

            source.CopyPixels(
                Int32Rect.Empty,
                data.Scan0,
                data.Height * data.Stride,
                data.Stride);

            bmp.UnlockBits(data);

            return bmp;
        }

        /// <summary>
        /// Get BitmapSource from Bitmap
        /// </summary>
        /// <param name="bmp">Bitmap</param>
        /// <returns>BitmapSource</returns>
        public static BitmapSource GetBitmapSource(Bitmap bmp)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                // Save bitmap into byte[] stream
                bmp.Save(stream, ImageFormat.Bmp);

                // Send byte[] stream as a source of BitmapImage through new instance of MemoryStream object
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.StreamSource = new MemoryStream(stream.ToArray());
                img.EndInit();
                return img;
            }

            /* === Another way === */
            //BitmapSource source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
            //    bmp.GetHbitmap(),
            //    IntPtr.Zero,
            //    Int32Rect.Empty,
            //    BitmapSizeOptions.FromEmptyOptions());
            //return source;
            
        }

        /// <summary>
        /// Get BitmapSource from Image Uri
        /// </summary>
        /// <param name="uri">Image Uri</param>
        /// <returns>BitmapSource</returns>
        public static BitmapSource GetBitmapSource(Uri uri)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = uri;
            img.EndInit();
            return img;
        }


    }
}
