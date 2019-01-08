using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using BibIO;
using BibOCR.Neural;
using BibOCR.Neural.Response;
using Tesseract;

namespace BibOCR
{
    public class OCRParser
    {
        const int INPUT_W = 28;
        const int INPUT_H = 28;

        internal const double MIN_CONFIDENCE = 0.05;

        public static string ParseTesseract(params string[] digitPaths)
        {
            if (digitPaths == null || digitPaths.Length == 0)
                return string.Empty;
            int desiredW = digitPaths.Length * INPUT_W;
            Bitmap finalImg = new Bitmap(desiredW, INPUT_H);
            string result = string.Empty;
            using (Graphics g = Graphics.FromImage(finalImg))
            {
                for (int i = 0; i < digitPaths.Length; i++)
                {
                    Bitmap img = (Bitmap)Image.FromFile(digitPaths[i]);
                    img = (Bitmap)resizeImage(img, INPUT_W, INPUT_H);
                    Rectangle destRegion = new Rectangle(i * INPUT_W, 0, INPUT_W, INPUT_H);
                    Rectangle sourceRegion = new Rectangle(0, 0, INPUT_W, INPUT_H);
                    g.DrawImage(img, destRegion, sourceRegion, GraphicsUnit.Pixel);
                }
            }
            finalImg = reversColors(finalImg);
            string destFile = FileManager.TempTiff;
            finalImg.Save(destFile, System.Drawing.Imaging.ImageFormat.Tiff);

            result = parseTesseract(destFile);

            return result;
        }
        public static string ParseTesseract(string inputPath)
        {
            Bitmap img = (Bitmap)Image.FromFile(inputPath);
            img = reversColors(img);
            string destFile = FileManager.TempTiff;
            img.Save(destFile, System.Drawing.Imaging.ImageFormat.Tiff);
            return parseTesseract(destFile);
        }
        public static NeuralParseResponse ParseNeural(string[] digitsPaths)
        {
            Image[] images = new Image[digitsPaths.Length];
            for (int i = 0; i < digitsPaths.Length; i++)
                images[i] = Image.FromFile(digitsPaths[i]);
            return ParseNeural(images);
        }

        public static NeuralParseResponse ParseNeural(Image[] digits)
        {
            const double LEARN_RATE = 1.0;
            string file = FileManager.NeuralDataFile;
            if (!File.Exists(file))
                return null;

            Network net = Network.Load(file, LEARN_RATE);
            NeuralParseResponse result = new NeuralParseResponse();
            foreach (var image in digits)
            {
                double[] data = readImage(image);
                dataToImage(data).Save("temp\\test.png");
                double[] response = net.FeedForward(data);
                NeuralNumberResponse number = NeuralNumberResponse.Parse(response);
                result.Chars.Add(number);
            }
            return result;
        }

        static double[] readImage(string path)
        {
            Image img = Image.FromFile(path);
            return readImage(img);
        }

        static Image dataToImage(double[] data)
        {
            Bitmap result = new Bitmap(INPUT_W, INPUT_H);
            int i = 0;
            for (int y = 0; y < INPUT_H; y++)
                for (int x = 0; x< INPUT_W; x++, i++)
                    result.SetPixel(x, y, data[i] == 0 ? Color.Black : Color.White);

            return result;
        }

        static Bitmap reversColors(Bitmap source)
        {
            Bitmap result = new Bitmap(source.Width, source.Height);
            for (int y = 0; y < result.Height; y++)
                for (int x = 0; x < result.Width; x++)
                    result.SetPixel(x, y, source.GetPixel(x, y).R <= 15 ? Color.White : Color.Black);
            return result;
        }

        static double[] readImage(Image img)
        {
            img = resizeImage(img, INPUT_W, INPUT_H);
            //if (img.Width > INPUT_W || img.Height > INPUT_H)
            //    img = resizeImage(img, INPUT_W, INPUT_H);
            Bitmap bmp = (Bitmap)img;
            double[] result = new double[INPUT_W * INPUT_H]; //[bmp.Width * bmp.Height];
            int i = 0;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++, i++)
                {
                    var color = bmp.GetPixel(x, y);
                    //float avg = (color.R + color.G + color.B) / 3.0f;
                    //result[i] = 1 - avg / 255;

                    //white -> 1
                    //black -> 0

                    result[i] = color.R <= 15 ? 0 : 1;
                }
            }

            return result;
        }

        static Image resizeImage(Image imgPhoto, int newWidth, int newHeight)
        {
            if (imgPhoto.Width == newWidth && imgPhoto.Height == newHeight)
                return imgPhoto;

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;

            //Consider vertical pics
            if (sourceWidth < sourceHeight)
            {
                int buff = newWidth;

                newWidth = newHeight;
                newHeight = buff;
            }

            int sourceX = 0, sourceY = 0, destX = 0, destY = 0;
            float nPercent = 0, nPercentW = 0, nPercentH = 0;

            nPercentW = ((float)newWidth / (float)sourceWidth);
            nPercentH = ((float)newHeight / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((newWidth -
                          (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((newHeight -
                          (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);


            Bitmap bmPhoto = new Bitmap(newWidth, newHeight,
                          PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Black);
            grPhoto.InterpolationMode =
                System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            imgPhoto.Dispose();
            return bmPhoto;
        }

        static string parseTesseract(string imagePath)
        {
            string output = string.Empty;
            string tempOutputFile = FileManager.TempTesseractOutput;
            
            try
            {
                //TODO: change to file manager
                ProcessStartInfo info = new ProcessStartInfo();
                info.WorkingDirectory = @"C:\Program Files (x86)\Tesseract-OCR";//FileManager.TesseractDirectory;
                info.WindowStyle = ProcessWindowStyle.Hidden;
                info.CreateNoWindow = true;
                info.UseShellExecute = false;
                //info.RedirectStandardOutput = true;
                info.FileName = "cmd.exe";
                info.Arguments = "/c tesseract.exe \"" + imagePath + "\" \"" + tempOutputFile + "\" -psm 7 -c bib";
                Process p = Process.Start(info);
                p.WaitForExit();
                if (p.ExitCode == 0)
                    output = File.ReadAllLines(tempOutputFile + ".txt").First();
            }
            catch { }
            output = output.Replace(Environment.NewLine, string.Empty);
            return output;
        }
    }
}
