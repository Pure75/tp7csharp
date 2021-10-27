using System;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Basics
{
    public class Basics
    {
        /// <summary>
        /// As the name states it apply a filter function on each pixel of the image
        /// </summary>
        /// <param name="image"> The image to modify</param>
        /// <param name="filter"> The function to apply on each pixel</param>
        public static Bitmap ApplyFilter(Bitmap image, Func<Color, Color> filter)
        {
            int largeur = image.Width;
            int longueur = image.Height;
            Bitmap nimage = new Bitmap(largeur, longueur);
            for (int i = 0; i < largeur; i++)
            {
                for (int j = 0; j < longueur; j++)
                {
                    Color pixel_color = image.GetPixel(i, j);
                    nimage.SetPixel(i,j,filter(pixel_color));
                }
            }
            return nimage;
        }
        /// <summary>
        /// A Black and White filter
        /// </summary>
        /// <param name="color"> The color to modify </param>
        /// <returns> The new color</returns>
        public static Color BlackAndWhite(Color color)
        {
            int r = color.R;
            int g = color.G;
            int b = color.B;
            int couleur = (r + g + b)/3;
            if (couleur > 127)
            {
                return Color.White;
            }
            return Color.Black;
        }

        /// <summary>
        /// A Yellow filter
        /// </summary>
        /// <param name="color"> The color to modify </param>
        /// <returns> The new color</returns>
        public static Color Yellow(Color color)
        {
            int red = color.R;
            int green = color.G;
            return Color.FromArgb(red, green, 0);
        }

        /// <summary>
        /// A Grayscale filter
        /// </summary>
        /// <param name="color"> The color to modify </param>
        /// <returns> The new color</returns>
        public static Color Grayscale(Color color)
        {
            int r = color.R;
            int g = color.G;
            int b = color.B;
            int res = (int)((0.21 * r) + (0.72 * g) + (0.07 * b));
            return Color.FromArgb(res, res, res);
        }

        /// <summary>
        /// A Negative filter
        /// </summary>
        /// <param name="color"> The color to modify </param>
        /// <returns> The new color</returns>
        public static Color Negative(Color color)
        {
            return (Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B));
        }

        /// <summary>
        /// Remove the maxes of the composants of the color
        /// </summary>
        /// <param name="color"> The color to modify </param>
        /// <returns> The new color</returns>

        private static int FindMax(int r, int g, int b) //trouve le max entre 3 entiers
        {
            if (r > g && r > b) return 1;
            if (r > g && r < b) return 2;
            if (r < g && r > b) return 3;
            if (g > r && g > b) return 3;
            if (g > r && g < b) return 2;
            if (g < r && g > b) return 1;
            if (b > r && b > g) return 2;
            if (b < r && b > g) return 3;
            if (b < r && b > g) return 1;
            if (r > b && r == g) return 5;
            if (r > g && r == b) return 6;
            if (b > g && b == r) return 7;
            if (b > r && b == g) return 8;
            if (g > b && g == r) return 9;
            if (g > r && g == b) return 10;
            return 0;
        }
        public static Color RemoveMaxes(Color color)
        {
            int r = color.R;
            int g = color.G;
            int b = color.B;
            int max = FindMax(r, g, b);
            switch (max)
            {
                case 1:
                    return Color.FromArgb(0, g, b);
                case 2:
                    return Color.FromArgb(r, g, 0);
                case 3:
                    return Color.FromArgb(r, 0, b);
                case 5:
                    return Color.FromArgb(0, 0, b);
                case 6:
                    return Color.FromArgb(0, g, 0);
                case 7:
                    return Color.FromArgb(0, g, 0);
                case 8:
                    return Color.FromArgb(r, 0, 0);
                case 9:
                    return Color.FromArgb(0, 0, b);
                case 10:
                    return Color.FromArgb(r, 0, 0);
                default:
                    return Color.FromArgb(0, 0, 0);
            }
        }

        /// <summary>
        /// Create the new_image as if the image was seen in a mirror
        /// ....o.  =>  .o....
        /// ...o..  =>  ..o...
        /// ..o...  =>  ...o..
        /// .o....  =>  ....o.
        /// o.....  =>  .....o
        /// </summary>
        /// <param name="image"> The image to 'mirror'</param>
        /// <returns> The new image</returns>
        public static Bitmap Mirror(Bitmap image)
        {
            int largeur = image.Width;
            int longueur = image.Height;
            Bitmap newimg = new Bitmap(largeur,longueur);
            for (int i = 0; i < largeur; i++)
            {
                for (int j = 0; j < longueur; j++)
                {
                    Color pixel_color = image.GetPixel(i, j);
                    newimg.SetPixel(largeur-i-1,j,pixel_color);
                }
            }
            return newimg;
        }

        /// <summary>
        /// Apply a right rotation
        /// </summary>
        /// <param name="image"> The image to rotate</param>
        /// <returns> The new_image</returns>
        public static Bitmap RotateRight(Bitmap image)
        {
            int largeur = image.Width;
            int longueur = image.Height;
            Bitmap newimg = new Bitmap(longueur,largeur);
            for (int i = 0; i < largeur; i++)
            {
                for (int j = 0; j < longueur; j++)
                {
                    Color pixel_color = image.GetPixel(i, j);
                    newimg.SetPixel(longueur-j-1,i,pixel_color);
                }
            }
            return newimg;
        }

        /// <summary>
        /// <!> Bonus <!>
        /// Rotate to the right n times
        /// </summary>
        /// <param name="image"> The image to rotate</param>
        /// <param name="n"> Number of rotation (n can be negative and thus must be handled properly)</param>
        /// <returns> The new_image</returns>
        public static Bitmap RotateN(Bitmap image, int n)
        {
            Bitmap imgbis = RotateRight(image);
            int res = n % 4;
            if (res <= 0) res = res + 4;
            while (res > 1)
            {
                imgbis = RotateRight(imgbis);
                res--;
            }
            return imgbis;
        }

        public static Color LifeInPink(Color color)
        {
            int blue = color.B;
            int red = color.R;
            return Color.FromArgb(red, 0, blue);
        }
    }
}
