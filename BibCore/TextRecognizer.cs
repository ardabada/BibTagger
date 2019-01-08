using BibIO;
using BibOCR;
using BibOCR.Neural.Response;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using Bitmap = System.Drawing.Bitmap;

namespace BibCore
{
    internal class TextRecognizer
    {
        public static string[] Recognize(IplImage input, TextDetectionParams _params, Chain[] chains, List<Tuple<Point2d, Point2d>> compBB, List<Tuple<CvPoint, CvPoint>> chainBB, DigitsRecognitionMethod digitsRecognition)
        {
            List<string> variants = new List<string>();

            //convert to grayscale
            IplImage grayImage = Cv.CreateImage(input.GetSize(), BitDepth.U8, 1);
            Cv.CvtColor(input, grayImage, ColorConversion.RgbToGray);

            for (int i = 0; i < chainBB.Count; i++)
            {
                Rect chainRect = new Rect(chainBB[i].Item1.X, chainBB[i].Item1.Y, chainBB[i].Item2.X - chainBB[i].Item1.X, chainBB[i].Item2.Y - chainBB[i].Item1.Y);
                CvPoint center = new CvPoint((chainBB[i].Item1.X + chainBB[i].Item2.X) / 2, (chainBB[i].Item1.Y + chainBB[i].Item2.Y) / 2);

                //work out if total width of chain is large enough
                if (chainBB[i].Item2.X - chainBB[i].Item1.X < input.Width / _params.MaxImgWidthToTextRatio)
                    continue;

                //eliminate chains with components of lower height than required minimum
                int minHeight = chainBB[i].Item2.Y - chainBB[i].Item1.Y;
                for (int j = 0; j < chains[i].components.Count; j++)
                {
                    minHeight = Math.Min(minHeight, compBB[chains[i].components[j]].Item2.y - compBB[chains[i].components[j]].Item1.y);
                }

                if (minHeight < _params.MinCharacterHeight)
                    continue;

                //invert direction if angle is in 3rd/4th quadrants
                if (chains[i].direction.x < 0)
                {
                    chains[i].direction.x = -chains[i].direction.x;
                    chains[i].direction.y = -chains[i].direction.y;
                }

                //work out chain angle
                double theta_deg = 180 * Math.Atan2(chains[i].direction.y, chains[i].direction.x) / Math.PI;

                if (Math.Abs(theta_deg) > _params.MaxAngle)
                    continue;

                if ((chainBB.Count == 2) && (Math.Abs(theta_deg) > 5))
                    continue;

                //Console.WriteLine("Chain #" + i + " angle: " + theta_deg + " degress");

                //create copy of input image including only the selected components
                Mat inputMat = new Mat(input);
                Mat grayMat = new Mat(grayImage);
                Mat componentsImg = Mat.Zeros(new Size(grayMat.Cols, grayMat.Rows), grayMat.Type());
                //CvMat componentsImg = _componentsImg.ToCvMat();
                Mat componentsImgRoi = null;
                List<CvPoint> compCoords = new List<CvPoint>();

                chains[i].components = chains[i].components.Distinct().ToList();

                int order = 0;
                //ordering components bounding boxes by x coord
                var ordCompBB = compBB.OrderBy(x => x.Item1.x).ToList();

                List<string> digits = new List<string>();
                for (int j = 0; j < ordCompBB.Count; j++)
                {
                    Rect roi = new Rect(ordCompBB[j].Item1.x, ordCompBB[j].Item1.y, ordCompBB[j].Item2.x - ordCompBB[j].Item1.x, ordCompBB[j].Item2.y - ordCompBB[j].Item1.y);
                    if (!chainRect.Contains(roi))
                        continue;

                    Mat componentRoi = new Mat(grayMat, roi);
                    compCoords.Add(new CvPoint(ordCompBB[j].Item1.x, ordCompBB[j].Item1.y));
                    compCoords.Add(new CvPoint(ordCompBB[j].Item2.x, ordCompBB[j].Item2.y));
                    compCoords.Add(new CvPoint(ordCompBB[j].Item1.x, ordCompBB[j].Item2.y));
                    compCoords.Add(new CvPoint(ordCompBB[j].Item2.x, ordCompBB[j].Item1.y));

                    Mat thresholded = new Mat(grayMat, roi);

                    Cv2.Threshold(componentRoi, thresholded, 0, 255, ThresholdType.Otsu | ThresholdType.BinaryInv);

                    componentsImgRoi = new Mat(componentsImg, roi);

                    Cv2.Threshold(componentRoi, componentsImgRoi, 0, 255, ThresholdType.Otsu | ThresholdType.BinaryInv);

                    //var size = thresholded.Size();
                    //digits.Add(new Bitmap(size.Width, size.Height, (int)thresholded.Step1(), System.Drawing.Imaging.PixelFormat.Format24bppRgb, thresholded.Data));

                    if (digitsRecognition == DigitsRecognitionMethod.Neural || digitsRecognition == DigitsRecognitionMethod.Both)
                    {
                        string file = FileManager.TempBitmap;
                        Cv2.ImWrite(file, thresholded);
                        try
                        {
                            digits.Add(file);
                        }
                        catch
                        {
                            GC.Collect();
                            GC.WaitForFullGCComplete();
                        }
                        //digits.Last().Save("test" + order + ".bmp");
                        order++;
                    }
                    //else if (digitsRecognition == DigitsRecognitionMethod.Tesseract || digitsRecognition == DigitsRecognitionMethod.Both)
                    //{
                    // DO NOTHING
                    //}
                }

                if (digitsRecognition == DigitsRecognitionMethod.Neural || digitsRecognition == DigitsRecognitionMethod.Both)
                {
                    //TODO: neural recognition
                    var result = OCRParser.ParseNeural(digits.ToArray());
                    variants.Add(result.Value);
                    //variants.AddRange(OCRParser.ParseNeural(digits.ToArray()));
                    //variants.Add(BibOCR.OCRParser.ParseBib(digits.ToArray()));
                }
                if (digitsRecognition == DigitsRecognitionMethod.Tesseract || digitsRecognition == DigitsRecognitionMethod.Both)
                {
                    CvRect _roi = GetBoundingBox(compCoords, new CvSize(input.Width, input.Height));
                    //ROI area can be null if outside of clipping area
                    if ((_roi.Width == 0) || (_roi.Height == 0))
                        continue;

                    //rotate each component coordinates
                    const int border = 3;

                    Mat _mat = new Mat(_roi.Height + 2 * border, _roi.Width + 2 * border, grayMat.Type());

                    Mat tmp = new Mat(grayMat, _roi);
                    //copy bounded box from rotated mat to new mat with borders - borders are needed to improve OCR success rate
                    Mat mat = new Mat(_mat, new Rect(border, border, _roi.Width, _roi.Height));
                    tmp.CopyTo(mat);

                    //resize image to improve OCR success rate
                    float upscale = 5.0f;
                    Cv2.Resize(mat, mat, new Size(0, 0), upscale, upscale);

                    //erode text to get rid of thin joints
                    int s = (int)(0.05 * mat.Rows); // 5% of up-scaled size
                    Mat elem = Cv2.GetStructuringElement(StructuringElementShape.Ellipse, new Size(2 * s + 1, 2 * s + 1), new Point(s, s));
                    //Cv2.Erode(mat, mat, elem);

                    //Cv2.Threshold(mat, mat, 0, 255, ThresholdType.Otsu | ThresholdType.BinaryInv);
                    
                    string file = FileManager.TempPng;
                    Cv2.ImWrite(file, mat);

                    // TODO: Pass it to Tesseract API
                    variants.Add(OCRParser.ParseTesseract(file));                     
                }

                //for (int j = 0; j < digits.Count; j++)
                //    digits[j].Dispose();
                digits.Clear();

                GC.Collect();
                GC.WaitForFullGCComplete(5000);
            }

            Cv.ReleaseImage(grayImage);

            return variants.Distinct().ToArray();
        }

        public static CvRect GetBoundingBox(List<CvPoint> vec, CvSize clip)
        {
            int minx = clip.Width - 1, miny = clip.Height - 1, maxx = 0, maxy = 0;
            foreach (Point it in vec)
            {
                if (it.X < minx)
                    minx = Math.Max(it.X, 0);
                if (it.Y < miny)
                    miny = Math.Max(it.Y, 0);
                if (it.X > maxx)
                    maxx = Math.Min(it.X, clip.Width - 1);
                if (it.Y > maxy)
                    maxy = Math.Min(it.Y, clip.Height - 1);
            }
            return new CvRect(new CvPoint(minx, miny), new CvSize(maxx - minx, maxy - miny));
        }
    }
}
