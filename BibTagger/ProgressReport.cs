using System;
using BibCore;
using Logger;

namespace BibTagger
{
    public static class ProgressReport
    {
        public static event EventHandler OnProgressChanged;

        public static void Reset()
        {
            _totalPhotos = -2;
            _totalParticipants = -2;
            _currentPhoto = string.Empty;
            _currentPhotoIndex = 0;
            _detectedBibs = 0;
            _unresolvedImages = 0;
            _resolvedImages = 0;

            Operation = CurrentOperation.AwaitingForStart;
        }

        public static string OperationText
        {
            get
            {
                switch (Operation)
                {
                    case CurrentOperation.AwaitingForStart:
                        return "Awaiting for start";
                    case CurrentOperation.ListingAllPhotos:
                        return "Listing all photos";
                    case CurrentOperation.ProcessingParticipantsBibs:
                        return "Processing participants bibs";
                    case CurrentOperation.ReadingParticipantsData:
                        return "Reading participants data";
                    case CurrentOperation.ProcessingPhotos:
                        return "Processing photos";
                    case CurrentOperation.TrainingFaces:
                        return "Training faces";
                    case CurrentOperation.SearchingUnresolvedFaces:
                        return "Searching unresolved faces";
                    default:
                        return string.Empty;
                }
            }
        }

        private static CurrentOperation _operation = CurrentOperation.AwaitingForStart;
        public static CurrentOperation Operation
        {
            get { return _operation; }
            set
            {
                if (value != _operation)
                {
                    switch (value)
                    {
                        case CurrentOperation.Start:
                            LogManager.Info("Starting bib tagger for \"" + Directory + "\" " + (UseSubdirs ? "with" : "without") + " subdirs. Data file located in \"" + DataFile + "\"");
                            break;
                        case CurrentOperation.ListingAllPhotos:
                            LogManager.Info("Listing all photos");
                            break;
                        case CurrentOperation.PhotoListingDone:
                            if (TotalPhotos > 0)
                                LogManager.Info("Found " + TotalPhotos + " photos");
                            else LogManager.Warning("No photos found.");
                            break;
                        case CurrentOperation.ParticipantsDataReadingDone:
                            if (TotalParticipants > 0)
                                LogManager.Info("Found " + TotalParticipants + " participants");
                            else LogManager.Warning("No participants found.");
                            break;
                        case CurrentOperation.ReadingSavedProgress:
                            LogManager.Info("Reading saved progress");
                            break;
                        case CurrentOperation.ProcessingParticipantsBibs:
                            LogManager.Info("Processing participants bibs");
                            break;
                        case CurrentOperation.ProcessingParticipantsBibsDone:
                            LogManager.Info("Participants bibs have been processed");
                            break;
                        case CurrentOperation.ProcessingPhotos:
                            string msg = "Starting processing photos. Configuration: ";
                            msg += "faceRecognition=";
                            if (Configuration.FaceRecognition == FaceRecognitionMethod.FacePlusPlus)
                                msg += "face++";
                            else if (Configuration.FaceRecognition == FaceRecognitionMethod.HaarCascade)
                                msg += "haar";
                            else msg += "preserved";
                            msg += ";bibArea=";
                            if (Configuration.BibArea == BibAreaDetectionMethod.SWT)
                                msg += "swt";
                            else if (Configuration.BibArea == BibAreaDetectionMethod.Edges)
                                msg += "edges";
                            else msg += "both";
                            msg += ";digitsRecognition=";
                            if (Configuration.DigitsRecognition == DigitsRecognitionMethod.Neural)
                                msg += "neural";
                            else if (Configuration.DigitsRecognition == DigitsRecognitionMethod.Tesseract)
                                msg += "tesseract";
                            else msg += "both";
                            msg += ";delay=" + Configuration.Delay + "ms";
                            msg += ";steps=";
                            if (Configuration.DetectionSteps == DetectionSteps.DetectionAndRecognition)
                                msg += "detectionAndRecognition";
                            else if (Configuration.DetectionSteps == DetectionSteps.FacialUnresolved)
                                msg += "facialUnresolved";
                            else msg += "both";
                            LogManager.Info(msg);
                            break;
                        case CurrentOperation.TrainingFaces:
                            LogManager.Info("Start training faces");
                            break;
                        case CurrentOperation.SearchingUnresolvedFaces:
                            LogManager.Info("Start searching unresolved faces");
                            break;
                    }
                }
                _operation = value;
                OnProgressChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        private static ExtraParams _params = new ExtraParams();
        public static ExtraParams Configuration
        {
            get { return _params; }
            set { _params = value; }
        }

        private static string _directory = string.Empty;
        public static string Directory
        {
            get { return _directory; }
            set { _directory = value; }
        }
        private static string _dataFile = string.Empty;
        public static string DataFile
        {
            get { return _dataFile; }
            set { _dataFile = value; }
        }
        private static bool _useSubDirs = true;
        public static bool UseSubdirs
        {
            get { return _useSubDirs; }
            set { _useSubDirs = value; }
        }


        private static int _totalPhotos = -2;
        public static int TotalPhotos
        {
            get { return _totalPhotos; }
            set
            {
                _totalPhotos = value;
                OnProgressChanged?.Invoke(null, EventArgs.Empty);
            }
        }
        private static int _totalParticipants = -2;
        public static int TotalParticipants
        {
            get { return _totalParticipants; }
            set
            {
                _totalParticipants = value;
                OnProgressChanged?.Invoke(null, EventArgs.Empty);
            }
        }
        private static string _currentPhoto = string.Empty;
        public static string CurrentPhoto
        {
            get { return _currentPhoto; }
            set
            {
                _currentPhoto = value;
                OnProgressChanged?.Invoke(null, EventArgs.Empty);
            }
        }
        private static int _currentPhotoIndex = 0;
        public static int CurrentPhotoIndex
        {
            get { return _currentPhotoIndex; }
            set
            {
                _currentPhotoIndex = value;
                OnProgressChanged?.Invoke(null, EventArgs.Empty);
            }
        }
        private static int _detectedBibs = 0;
        public static int DetectedBibs
        {
            get { return _detectedBibs; }
            set
            {
                _detectedBibs = value;
                OnProgressChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        private static int _resolvedImages = 0;
        public static int ResolvedImages
        {
            get { return _resolvedImages; }
            set
            {
                _resolvedImages = value;
                OnProgressChanged?.Invoke(null, EventArgs.Empty);
            }
        }
        private static int _unresolvedImages = 0;
        public static int UnresolvedImages
        {
            get { return _unresolvedImages; }
            set
            {
                _unresolvedImages = value;
                OnProgressChanged?.Invoke(null, EventArgs.Empty);
            }
        }
        public static int CurrentProgress
        {
            get
            {
                switch (Operation)
                {
                    case CurrentOperation.AwaitingForStart:
                        return 0;
                    case CurrentOperation.ListingAllPhotos:
                        return -1;
                    case CurrentOperation.ProcessingParticipantsBibs:
                        return -1;
                    case CurrentOperation.ReadingParticipantsData:
                        return -1;
                    case CurrentOperation.ProcessingPhotos:
                        if (TotalPhotos > 0)
                            return CurrentPhotoIndex * 100 / TotalPhotos;
                        break;
                }
                return 0;
            }
        }
        public static int TotalProgress
        {
            get
            {
                switch (Operation)
                {
                    case CurrentOperation.AwaitingForStart:
                        return 0;
                    case CurrentOperation.ListingAllPhotos:
                        return -1;
                    case CurrentOperation.ProcessingParticipantsBibs:
                        return -1;
                    case CurrentOperation.ReadingParticipantsData:
                        return -1;
                    case CurrentOperation.ProcessingPhotos:
                        if (TotalPhotos <= 0)
                            return 0;
                        return CurrentPhotoIndex * 70 / TotalPhotos;
                        
                        break;
                }
                return 0;
            }
        }
    }
}
