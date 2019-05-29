using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Http;

namespace CherwellTest.Controllers
{
    /// <summary>Represents the Web API controller for retrieving an image as defined by the Cherwell test document.</summary>
    public class ValuesController : ApiController
    {
        /// <summary>Returns a base-64 string containing both a triangle and an inverted triangle image. The base-64 image is replicated on the client-side to the required size.</summary>
        public string GetOption1()
        {
            Size sz = new Size(10, 10);
            Image image1 = CreateImage(sz, Color.DarkGray);
            Image image2 = CreateImage(sz, Color.DeepSkyBlue, true);

            using (Graphics g = Graphics.FromImage(image1))
            {
                g.DrawImage(image2, 0, 0);

                using (MemoryStream stream = new MemoryStream())
                {
                    image1.Save(stream, ImageFormat.Png);
                    return string.Concat("data:image/png;base64,", Convert.ToBase64String(stream.ToArray()));
                }
            }
        }

        /// <summary>Returns a base-64 string containing a replicated image of every block. The  base-64 image is then displayed on the client-side.</summary>
        public string GetOption2()
        {
            Size sz = new Size(10, 10);
            Image image1 = CreateImage(sz, Color.DarkGray);
            Image image2 = CreateImage(sz, Color.DeepSkyBlue, true);

            using (Graphics g = Graphics.FromImage(image1))
            {
                g.DrawImage(image2, 0, 0);

                using (MemoryStream stream = new MemoryStream())
                    image1.Save(stream, ImageFormat.Png);
            }

            Image image = new Bitmap(sz.Width * 6, sz.Height * 6);

            using (Graphics g = Graphics.FromImage(image))
            {
                for (int y = 0; y < 6; y++)
                {
                    for (int x = 0; x < 6; x++)
                        g.DrawImage(image1, new Point(sz.Width * y, sz.Height * x));
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    image.Save(stream, ImageFormat.Png);
                    return string.Concat("data:image/png;base64,", Convert.ToBase64String(stream.ToArray()));
                }
            }
        }

        /// <summary>Returns two base-64 strings, one containing a triangle image and the other containing an inverted triangle image. The base-64 image is replicated on the client-side to the required size.</summary>
        public string[] GetOption3()
        {
            Size sz = new Size(10, 10);
            Image[] images = new Image[] { CreateImage(sz, Color.DarkGray), CreateImage(sz, Color.DeepSkyBlue, true) };
            List<string> base64 = new List<string>();

            foreach (Image image in images)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    image.Save(stream, ImageFormat.Png);
                    base64.Add(string.Concat("data:image/png;base64,", Convert.ToBase64String(stream.ToArray())));
                }
            }

            return base64.ToArray();
        }

        /// <summary>Creates a triangle in an image of the specified size and color. Optionally, will invert (flip) the image.</summary>
        /// <param name="sz">The size of the image block.</param>
        /// <param name="color">The fill color of the triangle.</param>
        /// <param name="flip">When <c>true</c> inverts the triangle with no rotation.</param>
        private Image CreateImage(Size sz, Color color, bool flip = false)
        {
            Rectangle rect = new Rectangle(Point.Empty, sz);

            using (Bitmap bmp = new Bitmap(sz.Width, sz.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.FillRectangle(Brushes.Magenta, rect);
                    g.FillPolygon(new SolidBrush(color), new Point[] { Point.Empty, new Point(sz.Width, sz.Height), new Point(0, sz.Height), Point.Empty });
                }

                bmp.MakeTransparent(Color.Magenta);

                using (MemoryStream stream = new MemoryStream())
                {
                    bmp.Save(stream, ImageFormat.Png);
                    Image image = Image.FromStream(stream);

                    if (flip)
                        image.RotateFlip(RotateFlipType.Rotate180FlipNone);

                    return image;
                }
            }
        }
    }
}