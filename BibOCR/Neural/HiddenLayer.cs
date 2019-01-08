using System.Linq;

namespace BibOCR.Neural
{
    public class HiddenLayer : Layer
    {
        readonly Neuron[] neurons;

        public HiddenLayer(Activation activation, TrainingInfo trainInfo, int size)
        {
            var neurons = new Neuron[size + 1];
            for (int i = 0; i < size; i++)
                neurons[i] = new HiddenNeuron(activation, trainInfo);
            neurons[size] = new BiasNeuron();
            this.neurons = neurons;
        }

        public override Neuron[] Neurons
        {
            get { return neurons; }
        }


        class HiddenNeuron : Neuron
        {
            public HiddenNeuron(Activation activation, TrainingInfo trainInfo)
                : base(activation, trainInfo) { }

            protected override double CalcValueDelta()
            {
                return outboundConnections.Sum(conn => conn.WeightedError);
            }
        }
    }
}
