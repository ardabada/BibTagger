using BibIO;
using BibOCR;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace BibCore
{
    public class EdgeContrours
    {
        string imagePath;
        Bitmap image = null;
        private readonly DigitsRecognitionMethod digitsRecognitionMethod;
        private readonly List<TaggableFace> faces;


        public EdgeContrours(string imagePath, DigitsRecognitionMethod digitsRecognitionMethod, List<TaggableFace> faces)
        {
            this.imagePath = imagePath;
            this.digitsRecognitionMethod = digitsRecognitionMethod;
            this.faces = faces;

            if (File.Exists(imagePath))
            {
                //read image
                image = (Bitmap)Image.FromFile(imagePath);
            }
            else throw new FileNotFoundException("File not found", imagePath);
        }
        ~EdgeContrours()
        {
            if (image != null)
                image.Dispose();
        }
        

        public List<TaggableFace> Process()
        {
            List<TaggableFace> result = new List<TaggableFace>(faces);
            Image<Bgr, byte> image = new Image<Bgr, byte>(imagePath);
            List<string> temps = new List<string>();
            foreach (var face in result)
            {
                try
                {
                    image.ROI = new Rectangle(face.Body.X, face.Body.Y, face.Body.Width, face.Body.Height);
                    string path = FileManager.TempBitmap;
                    temps.Add(path);
                    image.Save(path);
                    ContoursFill cf = new ContoursFill(path, digitsRecognitionMethod);
                    face.DetectedBibs.AddRange(cf.ProcessBodyImage());
                }
                catch { }
            }

            return result;
        }

    }

    public class ContoursFill
    {
        string imagePath;
        Bitmap image = null;
        private readonly DigitsRecognitionMethod digitsRecognitionMethod;

        public ContoursFill(string imagePath, DigitsRecognitionMethod digitsRecognitionMethod)
        {
            this.imagePath = imagePath;
            this.digitsRecognitionMethod = digitsRecognitionMethod;

            if (File.Exists(imagePath))
            {
                //read image
                image = (Bitmap)Image.FromFile(imagePath);
            }
            else throw new FileNotFoundException("File not found", imagePath);
        }
        ~ContoursFill()
        {
            if (image != null)
                image.Dispose();
        }

        public List<string> ProcessBodyImage(bool normAny = false)
        {
            Image<Bgr, byte> baseImg = new Image<Bgr, byte>(image);

            Normalization<Bgr> rgb_norm = new Normalization<Bgr>(baseImg);
            var normalized = rgb_norm.Normalize();
            baseImg = rgb_norm.Result;
            //saveImage(baseImg, Path.GetFileName(imagePath)+"base");

            Image<Gray, byte> grayImg = baseImg.Convert<Gray, byte>();
            //saveImage(grayImg, Path.GetFileName(imagePath)+"gray");
            if (normalized)
            {
                Normalization<Gray> gray_norm = new Normalization<Gray>(grayImg);
                if (normAny)
                    gray_norm.NormalizeAny();
                grayImg = gray_norm.Result;
                //saveImage(grayImg, Path.GetFileName(imagePath) + "gray_norm");
            }

            Image<Gray, byte> canny = grayImg.Canny(175, 320);
            //saveImage(canny, "canny");

            //detecting bounding boxes
            var aContours = new VectorOfVectorOfPoint();
            var aHierarchy = new Mat();
            CvInvoke.FindContours(canny, aContours, aHierarchy, Emgu.CV.CvEnum.RetrType.List, Emgu.CV.CvEnum.ChainApproxMethod.LinkRuns, new Point(0, 0));

            List<Rectangle> boxes = new List<Rectangle>();
            for (int i = 0; i < aContours.Size; i++)
            {
                var item = aContours[i];
                List<Point> points = new List<Point>();
                for (int j = 0; j < item.Size; j++)
                {
                    var item2 = item[j];
                    points.Add(new Point(item2.X, item2.Y));
                }
                var x_query = from Point p in points select p.X;
                int xmin = x_query.Min();
                int xmax = x_query.Max();

                var y_query = from Point p in points select p.Y;
                int ymin = y_query.Min();
                int ymax = y_query.Max();

                Rectangle r = new Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
                boxes.Add(r);
            }
            //saveImage(drawBoxesOnImage(canny.Bitmap, boxes), Path.GetFileName(imagePath)+"test");

            List<Tuple<Rectangle, List<Rectangle>>> itemsToUnite = new List<Tuple<Rectangle, List<Rectangle>>>();
            //check if boxes contact more than 70%, if yes - unite them
            for (int i = 0; i < boxes.Count; i++)
            {
                //contacts = new List<Rectangle>();
                List<Rectangle> unions = new List<Rectangle>();
                for (int j = i + 1; j < boxes.Count; j++)
                {
                    //if (i == j)
                    //    continue;

                    var b1 = boxes[i];
                    var b2 = boxes[j];

                    int dif = 1; //contact differenct

                    //check up/down & left/right contact
                    bool hasContact = false;

                    if (Math.Abs(b1.Bottom - b2.Top) == dif)
                    {
                        Rectangle left = b1.Left < b2.Left ? b1 : b2;
                        Rectangle right = b1.Right > b2.Right ? b1 : b2;

                        if (left.Right < right.Left)
                            continue;

                        hasContact = true;
                    }
                    else if (Math.Abs(b1.Right - b2.Right) == dif)
                    {
                        Rectangle top = b1.Top < b2.Top ? b1 : b2;
                        Rectangle bottom = b1.Bottom > b2.Bottom ? b1 : b2;

                        if (top.Bottom < bottom.Top)
                            continue;

                        hasContact = true;
                    }

                    if (hasContact)
                    {
                        //contacts.Add(b1);
                        //contacts.Add(b2);

                        //check if contact area if more than 70%
                        var length1 = b1.Right - b1.Left;
                        var length2 = b2.Right - b1.Left;
                        var length = Math.Max(b1.Right, b2.Right) - Math.Min(b1.Left, b2.Left);
                        if (length > 0)
                        {
                            var left_offset = Math.Max(b1.Left, b2.Left) - Math.Min(b1.Left, b2.Left);
                            var right_offset = Math.Max(b1.Right, b2.Right) - Math.Min(b1.Right, b2.Right);
                            var intersection = length - left_offset - right_offset;

                            var perc = 100 * intersection / (float)length;

                            if (perc >= 70)
                                unions.Add(b2);
                        }
                    }
                }
                //if (contacts.Any())
                //    saveImage(drawBoxesOnImage(canny.Bitmap, contacts), "contact_" + i);

                //if (unions.Any())
                itemsToUnite.Add(new Tuple<Rectangle, List<Rectangle>>(boxes[i], unions));

                //if (contacts.Any())
                //    break;
            }
            //saveImage(drawBoxesOnImage(canny.Bitmap, contacts), "contact");

            List<Rectangle> newBoxes = new List<Rectangle>();
            foreach (var item in itemsToUnite)
            {
                if (item.Item2.Any())
                {
                    var lst = item.Item2;
                    lst.Add(item.Item1);
                    Rectangle r = getBoundingBox(lst);
                    newBoxes.Add(r);
                }
                else
                {
                    bool canAdd = true;
                    foreach (var i in itemsToUnite)
                    {
                        if (i.Item2.Contains(item.Item1))
                        {
                            canAdd = false;
                            break;
                        }
                    }
                    if (canAdd)
                        newBoxes.Add(item.Item1);
                }
            }
            boxes = newBoxes;
            //saveImage(drawBoxesOnImage(canny.Bitmap, boxes), Path.GetFileName(imagePath) + "unions");

            //filter bounding boxes
            float minHeight = 5;
            boxes.RemoveAll(x => x.Height < minHeight);
            boxes.RemoveAll(x => x.Height < x.Width);
            boxes.RemoveAll(x => x.Height > canny.Height / 2);
            boxes.RemoveAll(x => x.Width < 2);
            
            //saveImage(drawBoxesOnImage(canny.Bitmap, boxes), Path.GetFileName(imagePath) + "filtered");

            //detecting numbers bounding boxes
            List<Rectangle> sums = new List<Rectangle>();
            List<Rectangle> lefts = new List<Rectangle>();
            List<Rectangle> rights = new List<Rectangle>();
            List<Rectangle> extended = new List<Rectangle>();
            boxes = boxes.OrderBy(x => x.X).ToList();
            for (int i = 0; i < boxes.Count; i++)
            {
                var box = boxes[i];
                int offsetWidth = (int)(box.Width / 3);
                Rectangle offset1 = new Rectangle(box.X - offsetWidth, box.Y, offsetWidth, box.Height),
                    offset2 = new Rectangle(box.X + box.Width, box.Y, offsetWidth, box.Height);

                Rectangle uni = Rectangle.Union(box, offset1);
                uni = Rectangle.Union(uni, offset2);
                extended.Add(uni);

                lefts.Add(offset1);
                rights.Add(offset2);
            }
            //saveImage(drawBoxesOnImage(canny.Bitmap, new Color[] { Color.Red, Color.Green, Color.Blue }, boxes, lefts, rights), "offsets");
            //saveImage(drawBoxesOnImage(canny.Bitmap, extended), Path.GetFileName(imagePath) + "extended");

            List<IntersectionHierarchyItem> intersections = new List<IntersectionHierarchyItem>();
            foreach (var box in extended)
            {
                intersections.Add(findIntersectingHierarchy(extended, box));
            }

            List<Rectangle> result = new List<Rectangle>();
            foreach (var box in intersections)
            {
                if (box.HasIntersection)
                    result.Add(box.Union);
            }

            result = result.Distinct().ToList();
            //filtering horizontal rectangles
            result.RemoveAll(x => x.Width <= x.Height);
            //filtering rectangles by aspect ratio
            result.RemoveAll(x =>
            {
                float aspectRatio = (float)x.Width / (float)x.Height;
                return aspectRatio > 0.75 && aspectRatio < 1.3;
            });

            //saveImage(drawBoxesOnImage(canny.Bitmap, result), Path.GetFileName(imagePath) + "filtered");

            if (!result.Any())
            {
                if (!normAny)
                    return ProcessBodyImage(true);

                return new List<string>();
            }

            List<Rectangle> bounding = new List<Rectangle>();
            List<Rectangle[]> sRects = new List<Rectangle[]>();

            List<List<string>> digitVariants = new List<List<string>>();
            //cutting numbers from images
            for (int j = 0; j < result.Count; j++)
            {
                var area = result[j];
                //find source bounding boxes that are inside intersecting area
                List<Rectangle> rects = findInnerRectangles(boxes, area);

                //save(drawBoxesOnImage(canny, rects), imgNumber, "inner1_"+j);

                //remove rectangles that are inside another rect
                rects = removeInnerRectangles(rects);

                //save(drawBoxesOnImage(canny, rects), imgNumber, "inner2_" + j);
                //saveCoords(rects, imgNumber, "inner2_" + j);
                //TODO: do intersection
                rects = merge(rects);
                sRects.Add(rects.ToArray());
                bounding.Add(getBoundingBox(rects));

                //saveImage(drawBoxesOnImage(canny.Bitmap, rects), "inner_" + j);
                //saveCoords(rects, imgNumber, "inner_" + j);

                //distinct list to prevent adding duplicating rectangles after merging
                rects = rects.Distinct().ToList();

                List<string> tesseractParts = new List<string>();
                //cropping each rectangle and saving as image
                if (digitsRecognitionMethod == DigitsRecognitionMethod.Tesseract || digitsRecognitionMethod == DigitsRecognitionMethod.Both)
                {
                    List<string> digitVariant = new List<string>();
                    for (int i = 0; i < rects.Count; i++)
                    {
                        var gray = grayImg.Clone();
                        gray.ROI = rects[i];
                        Mat componentRoi = gray.Mat;
                        Mat thresholdedMat = gray.Mat;
                        CvInvoke.Threshold(componentRoi, thresholdedMat, 0, 255, Emgu.CV.CvEnum.ThresholdType.Otsu | Emgu.CV.CvEnum.ThresholdType.BinaryInv);

                        string digitLocation = FileManager.TempPng;
                        thresholdedMat.Save(digitLocation);
                        digitVariant.Add(digitLocation);

                        //save(thresholdedMat, imgNumber, "digit_" + j + "_" + i);
                        //save(crop(canny, rects[i]), imgNumber, "digit_" + j + "_" + i);
                    }
                    digitVariants.Add(digitVariant);
                }
            }
            //saveImage(drawBoxesOnImage(canny.Bitmap, bounding), "bb");

            List<string> numbersFinals = new List<string>();

            if (digitsRecognitionMethod == DigitsRecognitionMethod.Tesseract || digitsRecognitionMethod == DigitsRecognitionMethod.Both)
            {
                foreach (var dvar in digitVariants)
                {
                    string file = saveTesseract(dvar);
                    numbersFinals.Add(OCRParser.ParseTesseract(file));
                }
            }
            if (digitsRecognitionMethod == DigitsRecognitionMethod.Neural || digitsRecognitionMethod == DigitsRecognitionMethod.Both)
            {
                //get max campatible bounding box
                //var largestRect = bounding.Aggregate((r1, r2) => (((r1.Height * r1.Width) > (r2.Height * r2.Width)) || ()) ? r1 : r2);
                int index = 0;
                List<string> digitPaths = new List<string>();
                if (bounding.Count > 0)
                {
                    int maxArea = bounding[index].Height * bounding[index].Width;
                    int lastSubs = sRects[index].Length;
                    int goodAspects = checkGoodLetters(sRects[index]);
                    for (int i = 1; i < bounding.Count; i++)
                    {
                        //exclude elements that contain much more than 5 rectangles inside (this means that rectagles don't represent letters and numbers but other shapes)
                        int subs = sRects[i].Length;
                        if (subs > 5)
                            continue;

                        //exclude elements by aspect ratio
                        float aspectRatio = (float)bounding[i].Width / (float)bounding[i].Height;
                        const float MAX_ASPECT = 2.4f; //12 / 5
                        const float MIN_ASPECT = 1.7f;

                        //if (aspectRatio > MAX_ASPECT || aspectRatio < MIN_ASPECT)
                        //    continue;

                        //if (lastSubs > subs)
                        //    continue;

                        int area = bounding[i].Height * bounding[i].Width;
                        if (area > maxArea)
                        {
                            //check letters aspect ratio
                            int lets = checkGoodLetters(sRects[i]);
                            if (lets > goodAspects)
                            {
                                index = i;
                                maxArea = area;
                                lastSubs = subs;
                                goodAspects = lets;
                            }
                        }
                    }

                    //int index = bounding.IndexOf(largestRect);
                    var elems = sRects[index];
                    for (int i = 0; i < elems.Length; i++)
                    {
                        var gray = grayImg.Clone();
                        gray.ROI = elems[i];
                        Mat componentRoi = gray.Mat;
                        Mat thresholdedMat = gray.Mat;
                        CvInvoke.Threshold(componentRoi, thresholdedMat, 0, 255, Emgu.CV.CvEnum.ThresholdType.Otsu | Emgu.CV.CvEnum.ThresholdType.BinaryInv);

                        /*
                         * int s = (int)(0.05 * mat.Rows); // 5% of up-scaled size
                            Mat elem = Cv2.GetStructuringElement(StructuringElementShape.Ellipse, new Size(2 * s + 1, 2 * s + 1), new Point(s, s));
                            //Cv2.Erode(mat, mat, elem);
                        */

                        int s = (int)(0.05 * thresholdedMat.Rows);
                        Mat elem = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Ellipse, new Size(2 * s + 1, 2 * s + 1), new Point(s, s));
                        CvInvoke.Erode(thresholdedMat, thresholdedMat, elem, new Point(s, s), 1, Emgu.CV.CvEnum.BorderType.Reflect, default(MCvScalar));

                        string digitPath = FileManager.TempPng;
                        digitPaths.Add(digitPath);
                        thresholdedMat.Save(digitPath);
                        //save(thresholdedMat, imgNumber, "digit_" + "_" + i);
                    }
                }
                numbersFinals.Add(OCRParser.ParseNeural(digitPaths.ToArray()).Value);
            }

            return numbersFinals;
        }


        IntersectionHierarchyItem findIntersectingHierarchy(List<Rectangle> boxes, Rectangle source, List<Rectangle> skip = null)
        {
            if (skip == null)
                skip = new List<Rectangle>();

            IntersectionHierarchyItem result = new IntersectionHierarchyItem(source);
            List<int> indexes = new List<int>();
            for (int i = 0; i < boxes.Count; i++)
            {
                if (source == boxes[i])
                    continue;

                if (skip.Contains(boxes[i]))
                    continue;

                if (source.IntersectsWith(boxes[i]) || source.Contains(boxes[i]))
                {
                    int max = Math.Max(source.Height, boxes[i].Height);
                    int min = Math.Min(source.Height, boxes[i].Height);
                    if (max / min >= 2)
                        continue;

                    indexes.Add(i);
                    skip.Add(boxes[i]);
                }
            }

            foreach (var index in indexes)
            {
                result.Intersection.Add(findIntersectingHierarchy(boxes, boxes[index], skip));
            }

            return result;
        }
        List<Rectangle> findInnerRectangles(List<Rectangle> source, Rectangle area)
        {
            List<Rectangle> result = new List<Rectangle>();
            for (int i = 0; i < source.Count; i++)
            {
                if (area.Contains(source[i]))
                    result.Add(source[i]);
            }
            return result;
        }
        List<Rectangle> removeInnerRectangles(List<Rectangle> source)
        {
            List<Rectangle> result = new List<Rectangle>();
            List<int> excludeIds = new List<int>();
            for (int i = 0; i < source.Count; i++)
            {
                for (int j = 0; j < source.Count; j++)
                {
                    if (i == j)
                        continue;

                    if (source[i].Contains(source[j]))
                        excludeIds.Add(j);
                }
            }

            for (int i = 0; i < source.Count; i++)
            {
                if (!excludeIds.Contains(i))
                    result.Add(source[i]);
            }

            return result;
        }
        List<Rectangle> merge(List<Rectangle> boxes)
        {
            List<Rectangle> result = new List<Rectangle>();
            for (int i = 0; i < boxes.Count; i++)
            {
                bool hasIntersection = false;
                List<int> ids = new List<int>();
                //List<int> sums = new List<int>();
                for (int j = 0; j < boxes.Count; j++)
                {
                    if (i == j)
                        continue;

                    if (boxes[i].IntersectsWith(boxes[j])) //if (intersectsOrContacts(boxes[i], boxes[j]))
                    {
                        hasIntersection = true;
                        ids.Add(j);
                    }
                    //TODO: check second intersection condition (right == left and ??? )
                    //IntersectsWith does not calculates intersection only with border
                    //else if (boxes[i].Right == boxes[j].Left)
                    //{
                    //    hasIntersection = true;
                    //    sums.Add(j);
                    //}
                }

                if (!hasIntersection)
                    result.Add(boxes[i]);
                else
                {
                    Rectangle target = boxes[i];
                    foreach (var id in ids)
                    {
                        target = Rectangle.Union(target, boxes[id]);
                    }
                    //foreach (var id in sums)
                    //{
                    //    var elems = new List<Rectangle>()
                    //    {
                    //        boxes[id],
                    //        target
                    //    };

                    //    int xMin = elems.Min(s => s.X);
                    //    int yMin = elems.Min(s => s.Y);
                    //    int xMax = elems.Max(s => s.X + s.Width);
                    //    int yMax = elems.Max(s => s.Y + s.Height);
                    //    int width = xMax - xMin;
                    //    int height = yMax - yMin;

                    //    if (xMin > 0 && yMin > 0 && width > 0 & height > 0)
                    //        target = new Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
                    //}
                    result.Add(target);
                }
            }

            return result;
        }
        Rectangle getBoundingBox(List<Rectangle> elements)
        {
            List<Point> points = new List<Point>();
            foreach (var e in elements)
            {
                points.Add(new Point(e.Left, e.Top));
                points.Add(new Point(e.Right, e.Bottom));
            }

            var x_query = from Point p in points select p.X;
            int xmin = x_query.Min();
            int xmax = x_query.Max();

            var y_query = from Point p in points select p.Y;
            int ymin = y_query.Min();
            int ymax = y_query.Max();

            Rectangle r = new Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
            return r;
        }

        string saveTesseract(List<string> parts)
        {
            List<Bitmap> images = new List<Bitmap>();
            int w = 0; int h = 0;
            foreach (var part in parts)
            {
                var img = Image.FromFile(part);
                images.Add((Bitmap)img);
                w += img.Width + 10;
                if (h < img.Height)
                    h = img.Height;
            }
            w -= 10;

            Bitmap final = new Bitmap(w, h);
            int last_w = 0;
            using (Graphics g = Graphics.FromImage(final))
            {
                for (int i = 0; i < images.Count; i++)
                {
                    Bitmap img = images[i];
                    Rectangle destRegion = new Rectangle(last_w, 0, img.Width, img.Height);
                    last_w += img.Width + 10;
                    Rectangle sourceRegion = new Rectangle(0, 0, img.Width, img.Height);
                    g.DrawImage(img, destRegion, sourceRegion, GraphicsUnit.Pixel);
                }
            }
            string path = FileManager.TempPng; //Path.GetDirectoryName(parts.First());
            //path = Path.Combine(path, "tesseract");
            //if (!Directory.Exists(path))
            //    Directory.CreateDirectory(path);
            final.Save(path);
            return path;
        }

        int checkGoodLetters(Rectangle[] letters)
        {
            int count = 0;
            foreach (var rect in letters)
            {
                float aspectRatio = (float)rect.Width / (float)rect.Height;
                const float min = 0.45f;
                const float max = 0.85f;
                if (aspectRatio > min && aspectRatio < max)
                    count++;
            }
            return count;
        }





        void saveImage(string name)
        {
            saveImage(image, name);
        }
        void saveImage(Bitmap img, string name)
        {
            string path = FileManager.TempDirectory;
            img.Save(Path.Combine(path, name + ".bmp"));
        }
        void saveImage(Mat source, string name)
        {
            string path = FileManager.TempDirectory;
            source.Save(Path.Combine(path, name + ".bmp"));
        }
        void saveImage<TColor, TDepth>(Image<TColor, TDepth> img, string name) where TColor : struct, IColor
        where TDepth : new()
        {
            string path = FileManager.TempDirectory;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            img.Save(Path.Combine(path, name + ".bmp"));
        }

        Bitmap drawBoxesOnImage(Bitmap source, List<Rectangle> boxes)
        {
            Bitmap bmp = new Bitmap(source);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                foreach (var r in boxes)
                    g.DrawRectangle(new Pen(Color.Red, 0.2f), r);
            }
            return bmp;
        }
        Bitmap drawBoxesOnImage(Bitmap source, Color[] colors, params List<Rectangle>[] boxes)
        {
            if (colors.Length != boxes.Length)
                return new Bitmap(source.Width, source.Height);
            Bitmap bmp = new Bitmap(source);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                for (int i = boxes.Length - 1; i >= 0; i--)
                {
                    foreach (var r in boxes[i])
                        g.DrawRectangle(new Pen(colors[i], 0.2f), r);
                }
            }
            return bmp;
        }
    }
}
