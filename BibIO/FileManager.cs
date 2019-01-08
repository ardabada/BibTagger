using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BibIO
{
    public class FileManager
    {
        public static string BaseDirectory
        {
            get { return Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)); }
        }

        public static string DefaultLogFile
        {
            get { return Path.Combine(BaseDirectory, "tagger.log"); }
        }
        public static string LogFile(string photosDir)
        {
            if (Directory.Exists(photosDir))
                return Path.Combine(photosDir, "tagger.log");
            return DefaultLogFile;
        }

        public static string PreservedFacesFile(string photosDir)
        {
            if (Directory.Exists(photosDir))
                return Path.Combine(photosDir, "faces.json");
            return string.Empty;
        }

        public static string NeuralDataFile
        {
            //digit recognition perceptron artificial neural network _ weights _ epoch final
            get { return Path.Combine(DataDirectory, "drpann_w_e_final.json"); }
        }
        
        public static string DataDirectory
        {
            get
            {
                string path = Path.Combine(BaseDirectory, "data");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }
        public static string HaarCascadeFile
        {
            get { return Path.Combine(DataDirectory, "haar_face_default.xml"); }
        }
        public static string TesseractDirectory
        {
            get { return Path.Combine(DataDirectory, "tesseract"); }
        }
        public static string TesseractDataFile
        {
            get { return Path.Combine(TesseractDirectory, "packs", "eng.traineddata"); }
        }

        public static string TempDirectory
        {
            get
            {
                string path = Path.Combine(BaseDirectory, "temp");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }


        private static List<string> tempFiles = new List<string>();

        private static string getTempFile(string name)
        {
            string file = Path.Combine(TempDirectory, name);
            tempFiles.Add(file);
            return file;
        }

        public static string TempFile
        {
            get { return getTempFile(Guid.NewGuid().ToString()); }
        }
        public static string TempTesseractOutput
        {
            get
            {
                string name = Guid.NewGuid().ToString();
                string result = Path.Combine(TempDirectory, name);
                name += ".txt";
                string file = Path.Combine(TempDirectory, name);
                tempFiles.Add(file);
                return result;
            }
        }

        public static string TempBitmap
        {
            get
            {
                return getTempFile(Guid.NewGuid().ToString() + ".bmp");
            }
        }
        public static string TempTiff
        {
            get
            {
                return getTempFile(Guid.NewGuid().ToString() + ".tif");
            }
        }

        public static string TempPng
        {
            get
            {
                return getTempFile(Guid.NewGuid().ToString() + ".png");
            }
        }

        public static void RemoveTempFiles()
        {
            foreach (var file in tempFiles)
            {
                try
                {
                    if (File.Exists(file))
                        File.Delete(file);
                }
                catch { }
            }
        }

        public static string GetRelativePath(string baseDir, string fullPath)
        {
            return fullPath.Substring(baseDir.Length + 1);
        }
    }
}
