using BibCore;

namespace BibTagger
{
    public class ExtraParams
    {
        private FaceRecognitionMethod faceRecognition = FaceRecognitionMethod.PreservedFacesData;
        public FaceRecognitionMethod FaceRecognition
        {
            get { return faceRecognition; }
            set { faceRecognition = value; }
        }

        private BibAreaDetectionMethod bibArea = BibAreaDetectionMethod.Edges;
        public BibAreaDetectionMethod BibArea
        {
            get { return bibArea; }
            set { bibArea = value; }
        }

        private DigitsRecognitionMethod digitsRecognition = DigitsRecognitionMethod.Tesseract;
        public DigitsRecognitionMethod DigitsRecognition
        {
            get { return digitsRecognition; }
            set { digitsRecognition = value; }
        }

        private int delay = 500;
        public int Delay
        {
            get { return delay; }
            set { delay = value; }
        }

        private DetectionSteps detectionSteps = DetectionSteps.Both;
        public DetectionSteps DetectionSteps
        {
            get { return detectionSteps; }
            set { detectionSteps = value; }
        }
    }
}
