using System.Drawing;
using System;

namespace HideAndSeek
{
    public class Hide
    {

        /// <summary>
        /// Compress a value that was on 8 bits into a value on 'n' bits
        /// </summary>
        /// <param name="to_compress"> The value to compress</param>
        /// <param name="n"> On how many bits it must be compressed</param>
        /// <returns> The compressed value</returns>
        public static int CompressBits(int to_compress, int n)
        {
            int x = Bits.GetMaxForNBits(n);
            int res = (to_compress * x) / 255;
            return res;
        }

        /// <summary>
        /// Hides the image 'to_hide' which is supposed to be a grayscale in the color 'where_to_hide'
        /// of the image 'image'
        /// </summary>
        /// <param name="image"> The image where you have to hide the image</param>
        /// <param name="to_hide"> The image to hide</param>
        /// <param name="where_to_hide"> In which of composant R, G or B you want to hide it</param>
        /// <param name="n"> The number of bits you want to hide</param>
        public static void HideGrayScale(Bitmap image, Bitmap to_hide, color_type where_to_hide, int n)
        {
            int largeur = image.Width;
            int longueur = image.Height;
            for (int i = 0; i < largeur; i++)
            {
                int couleur;
                for (int j = 0; j < longueur; j++)
                {
                    Color pixel_color2 = to_hide.GetPixel(i, j);
                    Color pixel_color = image.GetPixel(i, j);
                    int couleurr = pixel_color.R;
                    int couleurg = pixel_color.G;
                    int couleurb = pixel_color.B;
                    if (color_type.R == where_to_hide)
                    {
                        couleur = pixel_color2.R;
                        int tmp = CompressBits(couleur, n);
                        Bits.SetLeastSignificantBits(ref couleurr, tmp, n);
                    }
                    else if (color_type.G == where_to_hide)
                    {
                        couleur = pixel_color2.G;
                        int tmp = CompressBits(couleur, n);
                        Bits.SetLeastSignificantBits(ref couleurg, tmp, n);
                    }
                    else if (color_type.B == where_to_hide)
                    {
                        couleur = pixel_color2.B;
                        int tmp = CompressBits(couleur, n);
                        Bits.SetLeastSignificantBits(ref couleurb, tmp, n);
                    }
                    image.SetPixel(i,j,Color.FromArgb(couleurr,couleurg,couleurb));
                }
            }
        }

        /// <summary>
        /// Hides the image 'to_hide' in the image 'image'
        /// Red color of 'to_hide' is hidden in Red color of image
        /// Green color of 'to_hide' is hidden in Green color of image
        /// And Blue color of 'to_hide' is hidden in Blue color of image
        /// </summary>
        /// <param name="image"> The image where you have to hide the image</param>
        /// <param name="to_hide"> The image to hide</param>
        /// <param name="n"> The number of bits you want to hide</param>
        public static void HideColor(Bitmap image, Bitmap to_hide, int n)
        {
            int largeur = image.Width;
            int longueur = image.Height;
            for (int i = 0; i < largeur; i++)
            {
                int couleur;
                for (int j = 0; j < longueur; j++)
                {
                    Color pixel_color2 = to_hide.GetPixel(i, j);
                    Color pixel_color = image.GetPixel(i, j);
                    int couleurr = pixel_color.R;
                    int couleurg = pixel_color.G;
                    int couleurb = pixel_color.B;
                    
                    couleur = pixel_color2.R;
                    int tmpr = CompressBits(couleur, n);
                    Bits.SetLeastSignificantBits(ref couleurr, tmpr, n);
                    
                    couleur = pixel_color2.G;
                    int tmpg = CompressBits(couleur, n);
                    Bits.SetLeastSignificantBits(ref couleurg, tmpg, n);
                    
                    couleur = pixel_color2.B;
                    int tmpb = CompressBits(couleur, n);
                    Bits.SetLeastSignificantBits(ref couleurb, tmpb, n);
                    image.SetPixel(i,j,Color.FromArgb(couleurr,couleurg,couleurb));
                }
            }
        }
    }
}
