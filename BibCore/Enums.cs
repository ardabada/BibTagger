using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibCore
{
    public enum FaceRecognitionMethod
    {
        FacePlusPlus,
        HaarCascade,
        PreservedFacesData
    }
    public enum BibAreaDetectionMethod
    {
        SWT,
        Edges,
        Both
    }
    public enum DigitsRecognitionMethod
    {
        Neural,
        Tesseract,
        Both
    }
    public enum DetectionSteps
    {
        DetectionAndRecognition,
        FacialUnresolved,
        Both
    }
}
