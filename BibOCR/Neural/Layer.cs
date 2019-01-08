namespace BibOCR.Neural
{
    public abstract class Layer
    {
        public abstract Neuron[] Neurons { get; }

        public int Size
        {
            get
            {
                if (Neurons != null)
                    return Neurons.Length;
                return 0;
            }
        }

        public void FeedForward()
        {
            foreach (var neuron in Neurons)
                neuron.FeedForward();
        }

        public void PropagateBack()
        {
            foreach (var neuron in Neurons)
                neuron.PropagateBack();
        }

    }
}
