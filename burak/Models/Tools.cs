using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace burak.Models
{
    public static class Tools
    {
        public static string Md5(this string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static System.Drawing.Image ResizeImage(System.Drawing.Image imgToResize, int Width)
        {
            Bitmap b = null;

            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;

            nPercentW = ((float)Width / (float)sourceWidth);

            nPercent = nPercentW;

            int destWidth = (int)Math.Ceiling(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (System.Drawing.Image)b;
        }
        public static string Seo(this string Text)
        {
            string seoText = Text;

            seoText = seoText.Trim();
            seoText = seoText.ToLower();
            seoText = seoText.Replace(' ', '-');
            seoText = seoText.Replace('ç', 'c');
            seoText = seoText.Replace('ğ', 'g');
            seoText = seoText.Replace('ı', 'i');
            seoText = seoText.Replace('ö', 'o');
            seoText = seoText.Replace('ş', 's');
            seoText = seoText.Replace('ü', 'u');

            foreach (char Char in seoText)
            {
                if (!((Convert.ToInt32(Char) > 96 && Convert.ToInt32(Char) < 123) || (Convert.ToInt32(Char) > 47 && Convert.ToInt32(Char) < 58) || Char == '-'))
                {
                    seoText = seoText.Replace(Char.ToString(), string.Empty);
                }
            }

            return seoText;
        }
    }
}