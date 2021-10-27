using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace HideAndSeek
{
    public enum color_type
    {
        R = 0,
        G,
        B,
    }
    public class Seek
    {

        /// <summary>
        /// Decompress a value that was on 'n' bits to a value on 8 bits
        /// </summary>
        /// <param name="to_decompress"> The value to decompress</param>
        /// <param name="n"> On how many bits it has been compressed</param>
        /// <returns> The decompressed value</returns>
        public static int DecompressBits(int to_decompress, int n)
        {
            int x = Bits.GetMaxForNBits(n);
            int res = (to_decompress * 255) / x;
            return res;
        }

        /// <summary>
        /// Seek a new image in the Bitmap image and in the color 'where_is_hidden' on 'n' bits
        /// </summary>
        /// <param name="image"> The image where it is hidden</param>
        /// <param name="where_is_hidden"> In which color it is hidden</param>
        /// <param name="n"> On how many bits it is hidden</param>
        /// <returns> The image found</returns>
        public static Bitmap SeekGrayScale(Bitmap image, color_type where_is_hidden, int n)
        {
            int largeur = image.Width;
            int longueur = image.Height;
            Bitmap newimg = new Bitmap(largeur, longueur);
            for (int i = 0; i < largeur; i++)
            {
                int couleur = 0;
                for (int j = 0; j < longueur; j++)
                {
                    Color pixel_color = image.GetPixel(i, j);
                    if (color_type.R == where_is_hidden) couleur = pixel_color.R;
                    if (color_type.G == where_is_hidden) couleur = pixel_color.G;
                    if (color_type.B == where_is_hidden) couleur = pixel_color.B;
                    couleur = DecompressBits(Bits.GetLeastSignificantBits(couleur, n), n);
                    newimg.SetPixel(i,j,Color.FromArgb(couleur,couleur,couleur));
                }
            }
            return newimg;
        }

        /// <summary>
        /// Seek a new image in the Bitmap image on 'n' bits
        /// </summary>
        /// <param name="image"> The image where it is hidden</param>
        /// <param name="n"> On how many bits it is hidden</param>
        /// <returns> The image found</returns>
        public static Bitmap SeekColor(Bitmap image, int n)
        {
            int largeur = image.Width;
            int longueur = image.Height;
            Bitmap newimg = new Bitmap(largeur, longueur);
            for (int i = 0; i < largeur; i++)
            {
                int couleurr = 0;
                int couleurg = 0;
                int couleurb = 0;
                for (int j = 0; j < longueur; j++)
                {
                    Color pixel_color = image.GetPixel(i, j);
                    couleurr = pixel_color.R;
                    couleurg = pixel_color.G;
                    couleurb = pixel_color.B;
                    couleurr = DecompressBits(Bits.GetLeastSignificantBits(couleurr, n), n);
                    couleurg = DecompressBits(Bits.GetLeastSignificantBits(couleurg, n), n);
                    couleurb = DecompressBits(Bits.GetLeastSignificantBits(couleurb, n), n);
                    newimg.SetPixel(i,j,Color.FromArgb(couleurr,couleurg,couleurb));
                }
            }
            return newimg;
        }
    }
}
