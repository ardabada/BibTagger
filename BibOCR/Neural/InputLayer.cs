namespace BibOCR.Neural
{
    public class InputLayer : Layer
    {
        readonly ValueNeuron[] neurons; // inputNeurons + Bias

        public InputLayer(int size)
        {
            var neurons = new ValueNeuron[size + 1];
            for (int i = 0; i < size; i++)
                neurons[i] = new ValueNeuron();
            neurons[size] = new BiasNeuron();
            this.neurons = neurons;
        }

        public override Neuron[] Neurons
        {
            get { return neurons; }
        }

        public void SetInputValues(double[] input)
        {
            for (int i = 0; i < input.Length; i++)
                neurons[i].SetValue(input[i]);
        }
    }
}
