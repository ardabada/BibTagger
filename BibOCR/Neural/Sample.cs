namespace BibOCR.Neural
{
    public class Sample
    {
        public readonly double[] input;
        public readonly double[] target;

        public Sample(double[] input, double[] target)
        {
            this.input = input;
            this.target = target;
        }
    }
}
