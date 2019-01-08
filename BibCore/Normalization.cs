using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace BibCore
{
    public class Normalization<TColor> where TColor : struct, IColor
    {
        delegate TColor Process(TColor r);

        const int MAX_ITERATIONS = 10;

        public Normalization(Image<TColor, byte> image)
        {
            Source = image;
            Result = image;
        }
        ~Normalization()
        {
            if (Source != null)
                Source.Dispose();
            if (Result != null)
                Result.Dispose();
        }

        public Image<TColor, byte> Source { get; private set; }
        public Image<TColor, byte> Result { get; private set; }

        /// <summary>
        /// Normalizes image
        /// </summary>
        /// <returns>Returns value that shows if image was normalized (depending on brightness)</returns>
        public bool Normalize()
        {
            int b = GetBrightness();
            //Console.WriteLine(b);
            if (b <= 110)
            {
                Brightness(150);
                Normalize();
                return true;
            }
            return false;
        }
        public void NormalizeAny(int amount = 150, int i = 0)
        {
            if (i > MAX_ITERATIONS)
                return;
            Brightness(amount);
            //if (GetBrightness() <= 150)
            //    NormalizeAny(amount / 2, i + 1);
        }

        public void Brightness(int p)
        {
            var _p = (int)normalize(p, -70, 70, -150, 150);
            Point c = new Point(185 - _p, 185 + _p);
            Curves(new Point(0, 0), c, new Point(255, 255));
        }

        public int GetBrightness()
        {
            double value = 0;
            for (int x = 0; x < Result.Width; x++)
            {
                for (int y = 0; y < Result.Height; y++)
                {
                    var data = Result[y, x];
                    double avg = 0; //(float)(data.Red + data.Green + data.Blue) / 3f;
                    int dim = data.Dimension;
                    if (dim >= 1)
                        avg += data.MCvScalar.V0;
                    if (dim >= 2)
                        avg += data.MCvScalar.V1;
                    if (dim >= 3)
                        avg += data.MCvScalar.V2;
                    if (dim >= 4)
                        avg += data.MCvScalar.V3;
                    avg /= dim;
                    value += avg;
                }
            }

            return (int)Math.Floor(value / (Result.Width * Result.Height));
        }

        /// <summary>
        /// Performs Centripetal Catmull–Rom spline adjustment
        /// </summary>
        /// <param name="s">Start point</param>
        /// <param name="c">Control point</param>
        /// <param name="e">End point</param>
        public void Curves(Point s, Point c, Point e)
        {
            var cur = new Curves(s, c, e);
            //curves array
            //represents [0-255] array that contains RGB shift for each color component (R, G, B apart)
            //color at index data[i] must be shift to value of data[i]
            var data = cur.CalculatePoints();

            process((rgba) =>
            {
                TColor result = new TColor();
                int v0 = -1, v1 = -1, v2 = -1, v3 = -1;
                int dim = rgba.Dimension;
                if (dim >= 1)
                    v0 = data[(int)rgba.MCvScalar.V0];
                if (dim >= 2)
                    v1 = data[(int)rgba.MCvScalar.V1];
                if (dim >= 3)
                    v2 = data[(int)rgba.MCvScalar.V2];
                if (dim >= 4)
                    v3 = data[(int)rgba.MCvScalar.V3];

                if (dim == 1)
                    result.MCvScalar = new MCvScalar(v0);
                else if (dim == 2)
                    result.MCvScalar = new MCvScalar(v0, v1);
                else if (dim == 3)
                    result.MCvScalar = new MCvScalar(v0, v1, v2);
                else if (dim == 4)
                    result.MCvScalar = new MCvScalar(v0, v1, v2, v3);

                return result;
                //int r = data[rgba.R];
                //int g = data[rgba.G];
                //int b = data[rgba.B];
                //return Color.FromArgb(r, g, b);
            });
        }

        double normalize(int val, int dmin, int dmax, int smin, int smax)
        {
            double sdist = dist(smin, smax),
                ddist = dist(dmin, dmax),
                ratio = ddist / sdist,
                _val = clamp(val, smin, smax);
            return dmin + (val - smin) * ratio;
        }

        double dist(int x1, int x2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2));
        }
        double clamp(double val, double min = 0, double max = 255)
        {
            return Math.Min(max, Math.Max(min, val));
        }

        /// <summary>
        /// launches per pixel procession
        /// </summary>
        /// <param name="func"></param>
        void process(Process func)
        {
            for (int x = 0; x < Result.Width; x++)
            {
                for (int y = 0; y < Result.Height; y++)
                {
                    var c = Result[y, x];
                    c = func(c);
                    Result[y, x] = c;
                }
            }
        }
    }
}
