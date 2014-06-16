using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace XeeX.GIF
{
    public class ImageToGifConverter
    {

        public void A()
        {
            int width = 128;
            int height = width;
            int stride = width / 8;
            byte[] pixels = new byte[height * stride];

            BitmapPalette myPalette = BitmapPalettes.WebPalette;

            BitmapSource image = BitmapSource.Create(
                width,
                height,
                96,
                96,
                PixelFormats.Indexed1,
                myPalette,
                pixels,
                stride);

            BitmapSource A = BitmapConverter.GetBitmapSource(Properties.Resources._1);
            BitmapSource B = BitmapConverter.GetBitmapSource(Properties.Resources._2);

            FileStream stream = new FileStream("new.gif", FileMode.Create);
            GifBitmapEncoder encoder = new GifBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(A));
            encoder.Frames.Add(BitmapFrame.Create(B));
            encoder.Save(stream);


        }

    }
}
