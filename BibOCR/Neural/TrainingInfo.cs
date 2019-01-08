namespace BibOCR.Neural
{
    public class TrainingInfo
    {
        public TrainingInfo(double learnRate)
        {
            this.LearnRate = learnRate;
        }

        public double LearnRate { get; private set; }
    }
}
