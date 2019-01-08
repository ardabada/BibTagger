using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace BibCore
{
    internal class Curves
    {
        Point s, c, e;

        //0-1
        float alpha = 0.5f;

        public Curves(Point start, Point control, Point end)
        {
            s = start;
            c = control;
            e = end;
        }

        public int[] CalculatePoints()
        {
            List<Point> points = new List<Point>();
            points.AddRange(calculateTable(new Point(s.X - 1, s.Y - 1), s, c, e));
            points.AddRange(calculateTable(s, c, e, new Point(e.X + 1, e.Y + 1)));

            int size = 256;
            int[] result = new int[size];

            for (int i = 0; i < size; i++)
            {
                var xs = points.Where(x => x.X == i);
                if (xs.Any())
                    result[i] = xs.First().Y;
                else result[i] = -1;
            }

            int startIndex = -1;
            int lastIndex = -1;
            for (int i = 0; i < size; i++)
            {
                if (result[i] == -1)
                {
                    if (startIndex == -1)
                        startIndex = i;
                }

                if (result[i] > -1)
                {
                    if (startIndex > -1)
                        lastIndex = i - 1;
                }

                if (startIndex > -1 && lastIndex > -1)
                {
                    int calcIndex1 = startIndex - 1;
                    int calcIndex2 = lastIndex + 1;
                    if (calcIndex1 < 0)
                        calcIndex1 = 0;
                    if (calcIndex2 >= size)
                        calcIndex2 = size - 1;

                    int val1 = result[calcIndex1];
                    int val2 = result[calcIndex2];
                    if (val1 < 0)
                        val1 = 0;
                    int dif = val2 - val1;
                    int step = dif / (lastIndex - startIndex + 1);
                    for (int j = startIndex; j <= lastIndex; j++)
                    {
                        result[j] = val1 + dif;
                        val1 = result[j];
                    }

                    startIndex = -1;
                    lastIndex = -1;
                }
            }

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = clamp(result[i]);
            }

            return result;
        }

        int clamp(int val, int min = 0, int max = 255)
        {
            return Math.Min(max, Math.Max(min, val));
        }

        List<Point> calculateTable(Point p0, Point p1, Point p2, Point p3)
        {
            int size = p2.X - p1.X + 2;
            Point[] result = new Point[size];
            List<Point> points = new List<Point>();

            //Centripetal Catmull–Rom spline

            Vector2 v0 = new Vector2(p0.X, p0.Y);
            Vector2 v1 = new Vector2(p1.X, p1.Y);
            Vector2 v2 = new Vector2(p2.X, p2.Y);
            Vector2 v3 = new Vector2(p3.X, p3.Y);

            float t0 = 0.0f;
            float t1 = getT(t0, v0, v1);
            float t2 = getT(t1, v1, v2);
            float t3 = getT(t2, v2, v3);

            for (float t = t1; t < t2; t += (t2 - t1) / size)
            {
                Vector2 A1 = (t1 - t) / (t1 - t0) * v0 + (t - t0) / (t1 - t0) * v1;
                Vector2 A2 = (t2 - t) / (t2 - t1) * v1 + (t - t1) / (t2 - t1) * v2;
                Vector2 A3 = (t3 - t) / (t3 - t2) * v2 + (t - t2) / (t3 - t2) * v3;

                Vector2 B1 = (t2 - t) / (t2 - t0) * A1 + (t - t0) / (t2 - t0) * A2;
                Vector2 B2 = (t3 - t) / (t3 - t1) * A2 + (t - t1) / (t3 - t1) * A3;

                Vector2 C = (t2 - t) / (t2 - t1) * B1 + (t - t1) / (t2 - t1) * B2;

                int x = (int)C.x;
                if (!points.Where(p => p.X == x).Any())
                    points.Add(new Point(x, (int)C.y));
            }

            return points;
        }

        float getT(float t, Vector2 p0, Vector2 p1)
        {
            double a = Math.Pow(p1.x - p0.x, 2) + Math.Pow(p1.y - p0.y, 2);
            double b = Math.Pow(a, 0.5f);
            double c = Math.Pow(b, alpha);
            return (float)(c + t);
        }
    }
}
