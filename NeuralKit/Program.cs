using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BibOCR.Neural;
using BibOCR;
using BibIO;
using System.Drawing;
using System.Drawing.Imaging;

namespace NeuralKit
{
    class Program
    {
        const int OUTPUT_SIZE = 15;
        const int INPUT_W = 28;
        const int INPUT_H = 28;
        const int INPUT_SIZE = INPUT_W * INPUT_H;
        const double LEARN_RATE = 1;//0.2;
        const double MIN_ERROR = 0.005;
        const int MAX_TRIES = 1000;

        static void Main(string[] args)
        {
            //testNetwork();
            //return;

            //Network net = Network.Load("e5.json", LEARN_RATE);


            Console.WriteLine("Loading training samples");
            Metadata[] trainData = loadMetadata("train\\train.json");
            List<Sample> trainSamples = new List<Sample>();
            foreach (var folder in trainData)
            {
                string root = "train\\";
                string[] images = Directory.GetFiles(root + folder.Path, "*.png");
                foreach (var image in images)
                {
                    trainSamples.Add(new Sample(readImage(image), getOutputArray(folder.Target)));
                    break;
                }
                //break;
            }

            Console.WriteLine("Loading test samples");
            Metadata[] testData = JsonHelper.Deserialize<Metadata[]>("test\\test.json");
            List<Sample> testSamples = new List<Sample>();
            foreach (var file in testData)
            {
                testSamples.Add(new Sample(readImage("test\\" + file.Path), getOutputArray(file.Target)));
            }

            //Console.WriteLine("");
            //Console.ReadLine();
            //return;
            
            Network net = new Network(INPUT_SIZE, new int[] { 500, 100 }, OUTPUT_SIZE, LEARN_RATE);
            var trainer = new Trainer(net, trainSamples.ToArray(), CheckCorrect, testSamples.ToArray());
            trainer.TrainUntilDone(true);

            trainer.net.Save("trained.json");
            Console.WriteLine("Done");
            Console.ReadLine();

            /*
             * var training = mnistSamples.Training.Take(trainingCount).ToArray();
            var testing = mnistSamples.Testing.Take(training.Length).ToArray();

            var trainer = new Trainer(net, training, CheckCorrect, testing);
            trainer.TrainUntilDone(true);
             * */

            //Console.WriteLine("Ready to train images (" + trainSamples.Count + ")");
            //Console.WriteLine("Initializing network");
            //Network net = new Network(INPUT_SIZE, new int[] { 500, 100 }, OUTPUT_SIZE, LEARN_RATE);
            //Console.WriteLine("Start training");

            //List<Sample> dataToTrain = new List<Sample>(trainSamples);
            //int gen = 0;
            //while (dataToTrain.Any() && gen < MAX_TRIES)
            //{
            //    Console.Title = string.Format("GEN {0}; SAMPLES {1}", gen, dataToTrain.Count);
            //    //dataToTrain = shuffle(dataToTrain);

            //    Console.WriteLine(gen);
            //    List<int> indexesToRemove = new List<int>();
            //    for (int i = 0; i < dataToTrain.Count; i++)
            //    {
            //        var t = dataToTrain[i];
            //        double[] calc = net.FeedForward(t.input);
            //        double error = 0;
            //        for (int j = 0; j < calc.Length; j++)
            //            error += Math.Pow(calc[j] - t.target[j], 2);
            //        if (error <= MIN_ERROR)
            //            Console.ForegroundColor = ConsoleColor.Green;
            //        Console.WriteLine(indexOfMaxValue(t.target) + " :\t" + error);
            //        Console.ResetColor();
            //        if (error <= MIN_ERROR)
            //            indexesToRemove.Add(i);
            //        net.PropagateBack(t.target);
            //    }

            //    foreach (var index in indexesToRemove.OrderByDescending(v => v))
            //    {
            //        dataToTrain.RemoveAt(index);
            //    }
            //    indexesToRemove.Clear();

            //    gen++;
            //}

            //net.Save("network.json");
            
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static bool CheckCorrect(double[] target, double[] output)
        {
            return indexOfMaxValue(target) == indexOfMaxValue(output);
        }

        static void testNetwork()
        {
            Network net = Network.Load("network.json", LEARN_RATE);

            Metadata[] testData = JsonHelper.Deserialize<Metadata[]>("test\\test.json");
            List<Sample> testSamples = new List<Sample>();
            foreach (var file in testData)
            {
                testSamples.Add(new Sample(readImage("test\\" + file.Path), getOutputArray(file.Target)));
                //string root = "test\\";
                //string[] images = Directory.GetFiles(root + folder.Path, "*.png");
                //foreach (var image in images)
                //{
                //    testSamples.Add(new Sample(readImage(image), getOutputArray(folder.Target)));
                //    break;
                //}
            }
            for (int i = 0; i < testSamples.Count; i++)
            {
                Console.Write(testData[i].Path + " -> " + testData[i].Target + " -> ");
                double[] calc = net.FeedForward(testSamples[i].input);
                double item = indexOfMaxValue(calc);
                if (item == testData[i].Target)
                    Console.ForegroundColor = ConsoleColor.Green;
                else Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(item);
                Console.ResetColor();
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static int indexOfMaxValue(double[] numbers)
        {
            var index = 0;
            return numbers
                .Select(number => new { index = index++, number })
                .OrderBy(t => t.number)
                .Last().index;
        }

        static List<T> shuffle<T>(List<T> source)
        {
            Random rand = new Random();
            var models = source.OrderBy(c => rand.Next()).Select(c => c).ToList();
            return models;
        }

        static Metadata[] loadMetadata(string path)
        {
            return JsonHelper.Deserialize<Metadata[]>(path);
        }

        static double[] getOutputArray(int number)
        {
            double[] result = new double[OUTPUT_SIZE];
            result[number] = 1;
            return result;
        }
        static double[] readImage(string path)
        {
            Image img = Image.FromFile(path);
            img = resizeImage(img, INPUT_W, INPUT_H);
            Bitmap bmp = (Bitmap)img;
            double[] result = new double[bmp.Width * bmp.Height];
            int i = 0;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++, i++)
                {
                    var color = bmp.GetPixel(x, y);
                    float avg = (color.R + color.G + color.B) / 3.0f;
                    //result[i] = 1 - avg / 255;
                    var isBlack = avg <= 15;
                    result[i] = isBlack ? 1 : 0;
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
            grPhoto.Clear(Color.White);
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
    }
}
