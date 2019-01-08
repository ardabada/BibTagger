namespace BibCore
{
    internal struct TextDetectionParams
    {
        public bool DarkOnLight { get; set; }
        public double MaxStrokeLength { get; set; }
        public double MinCharacterHeight { get; set; }
        public double MaxImgWidthToTextRatio { get; set; }
        public double MaxAngle { get; set; }
        public double TopBorder { get; set; }
        public double BottomBorder { get; set; }
        public int MinChainLen { get; set; }
        public bool VerifyWithSvmModelUpToThisChainLen { get; set; }
        public bool HightNeedsToBeThisLargeToVerifyWithModel { get; set; }

        public static TextDetectionParams Default(double rows)
        {
            TextDetectionParams r = new TextDetectionParams();
            r.DarkOnLight = true;
            r.MaxStrokeLength = 15;
            r.MinCharacterHeight = 11;
            r.MaxImgWidthToTextRatio = 100;
            r.MaxAngle = 45;
            r.TopBorder = rows * 0.1;
            r.BottomBorder = rows * 0.05;
            r.MinChainLen = 3;
            r.VerifyWithSvmModelUpToThisChainLen = false;
            r.HightNeedsToBeThisLargeToVerifyWithModel = false;
            return r;
        }
    }
}
