using OpenCvSharp;
using BibCore.FaceApi;
using BibCore.FaceApi.Models;
using System.Drawing;
using System.Collections.Generic;
using BibIO;
using OpenCvSharp.CPlusPlus;
using Logger;
using System.Linq;
using System.IO;
using System.Threading;

namespace BibCore
{
    public class BibDetector
    {
        /// <summary>
        /// Static variables cleanup before launch
        /// </summary>
        public static void Cleanup()
        {
            preservedFaces = null;
        }

        /// <summary>
        /// Cleanup after processing each photo
        /// </summary>
        public static void IterationCleanup()
        {
            FileManager.RemoveTempFiles();
        }

        public static TaggableFace[] GetPossibleBibs(string imagePath, FaceRecognitionMethod faceRecognition, BibAreaDetectionMethod bibAreaMethod, DigitsRecognitionMethod digitsRecognition, string parentDirectory = null)
        {
            List<TaggableFace> faces = new List<TaggableFace>();

            string pathToShow = imagePath;
            if (!string.IsNullOrEmpty(parentDirectory))
                pathToShow = FileManager.GetRelativePath(parentDirectory, imagePath);

            LogManager.Info("Processing image " + pathToShow);

            if (faceRecognition == FaceRecognitionMethod.FacePlusPlus)
            {
                var fppFaces = FaceManager.DetectFaceFromFile(imagePath, false, FaceAttributesValues.Gender);
                if (fppFaces != null)
                {
                    foreach (var face in fppFaces.Faces)
                        faces.Add(new TaggableFace(pathToShow, face.FaceToken, new Rectangle(face.FaceRectangle.X, face.FaceRectangle.Y, face.FaceRectangle.Width, face.FaceRectangle.Height)));
                }
                saveFaces(faces, parentDirectory);
            }
            else if (faceRecognition == FaceRecognitionMethod.HaarCascade)
            {
                var haars = getHaarFaces(imagePath);
                foreach (var face in haars)
                    faces.Add(new TaggableFace(pathToShow, face));
                
                saveFaces(faces, parentDirectory);
            }
            else
            {
                faces = getPreservedFaces(parentDirectory, pathToShow);
            }

            if (faces == null)
            {
                LogManager.Fatal("Fatal error during faces recognition. System halted.");
                return null;
            }

            if (faces.Count > 0)
                LogManager.Info("Detected " + faces.Count + (faces.Count == 1 ? " face" : " faces"));
            else LogManager.Warning("No faces detected.");

            if (bibAreaMethod == BibAreaDetectionMethod.SWT || bibAreaMethod == BibAreaDetectionMethod.Both)
            {
                faces = SWT.Process(imagePath, digitsRecognition, faces);
            }
            if (bibAreaMethod == BibAreaDetectionMethod.Edges || bibAreaMethod == BibAreaDetectionMethod.Both)
            {
                EdgeContrours contrours = new EdgeContrours(imagePath, digitsRecognition, faces);
                faces = contrours.Process();
                //faces = //ContoursFill.Process(imagePath, digitsRecognition, faces);
            }

            return faces.ToArray();
        }

        private static List<Rectangle> getHaarFaces(string imagePath)
        {
            var haarCascade = new CascadeClassifier(FileManager.HaarCascadeFile);
            var gray = new Mat(imagePath, LoadMode.GrayScale);
            Rect[] faces = haarCascade.DetectMultiScale(gray, 1.4, 4, HaarDetectionType.DoCannyPruning);
            List<Rectangle> result = new List<Rectangle>();
            foreach (var f in faces)
                result.Add(new Rectangle(f.X, f.Y, f.Width, f.Height));
            return result;
        }

        static void saveFaces(List<TaggableFace> faces, string dir)
        {
            string path = FileManager.PreservedFacesFile(dir);
            List<TaggableFace> saved = new List<TaggableFace>();
            if (File.Exists(path))
                saved = JsonHelper.Deserialize<List<TaggableFace>>(path);
            saved.AddRange(faces);
            if (!string.IsNullOrEmpty(path))
                JsonHelper.Serialize(saved, path);
        }

        static List<TaggableFace> preservedFaces = null;
        static List<TaggableFace> getPreservedFaces(string parentDirectory, string relativeImagePath)
        {
            if (string.IsNullOrEmpty(parentDirectory) || string.IsNullOrEmpty(relativeImagePath))
            {
                LogManager.Error("Unable to load preserved faces data.");
                return null;
            }
            string facesPath = FileManager.PreservedFacesFile(parentDirectory);
            if (!File.Exists(facesPath))
            {
                LogManager.Fatal("Unable to load preserved faces data.");
                return null;
            }

            if (preservedFaces == null)
                preservedFaces = JsonHelper.Deserialize<TaggableFace[]>(facesPath).ToList();

            List<TaggableFace> result = new List<TaggableFace>();
            if (preservedFaces != null)
                result = preservedFaces.Where(x => x.ImagePath == relativeImagePath).ToList();
            return result;
        }



        public static void StudyFaces(Dictionary<string, string> idsAndTags, int delay)
        {
            List<string> ids = new List<string>();
            foreach (var item in idsAndTags)
            {
                Thread.Sleep(delay);
                FaceResolver.SetFaceUser(item.Key, item.Value);
                ids.Add(item.Key);
            }
            FaceResolver.AddFacesToFaceset(ids.ToArray(), delay);
        }
        public static void StudyFace(string faceId, string tag)
        {
            StudyFaces(new Dictionary<string, string>()
            {
                { faceId, tag }
            }, 0);
        }

        public static List<FaceSearchModel> SearchFace(string faceId)
        {
            return FaceResolver.Search(faceId);
        }
    }
}
