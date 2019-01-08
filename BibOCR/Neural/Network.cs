using BibIO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BibOCR.Neural
{
    public class Network
    {
        [JsonProperty("inputLayer")]
        readonly InputLayer inputLayer;
        [JsonProperty("hiddenLayers")]
        readonly HiddenLayer[] hiddenLayers;
        [JsonProperty("outputLayer")]
        readonly OutputLayer outputLayer;
        [JsonProperty("activation")]
        readonly Activation activation;

        [JsonIgnore]
        public int InputSize { get; private set; }
        [JsonIgnore]
        public int[] HiddenSizes { get; private set; }
        [JsonIgnore]
        public int OutputSize { get; private set; }

        public Network(int inputSize, int[] hiddenSizes, int outputSize, double learnRate)
            : this(Activation.Sigmoid, new TrainingInfo(learnRate), inputSize, hiddenSizes, outputSize)
        { }

        public Network(Activation activation, TrainingInfo trainInfo, int inputSize, int[] hiddenSizes, int outputSize)
        {
            this.InputSize = inputSize;
            this.HiddenSizes = hiddenSizes;
            this.OutputSize = outputSize;

            this.activation = activation;
            this.inputLayer = new InputLayer(inputSize);
            this.hiddenLayers = hiddenSizes
                .Select(size => new HiddenLayer(activation, trainInfo, size))
                .ToArray();
            this.outputLayer = new OutputLayer(activation, trainInfo, outputSize);
            ConnectLayers();
        }

        void ConnectLayers()
        {
            if (!hiddenLayers.Any())
            {
                ConnectAdjacentLayers(inputLayer, outputLayer);
            }
            else
            {
                var lastHiddenIndex = hiddenLayers.Length - 1;
                ConnectAdjacentLayers(inputLayer, hiddenLayers[0]);
                for (int i = 0; i < lastHiddenIndex; i++)
                    ConnectAdjacentLayers(hiddenLayers[i], hiddenLayers[i + 1]);
                ConnectAdjacentLayers(hiddenLayers[lastHiddenIndex], outputLayer);
            }
        }

        void ConnectAdjacentLayers(Layer fromLayer, Layer toLayer)
        {
            foreach (var fromNeuron in fromLayer.Neurons)
                foreach (var toNeuron in toLayer.Neurons)
                    new Connection(fromNeuron, toNeuron, activation.GetRandomWeight());
        }

        public double[] FeedForward(double[] input)
        {
            inputLayer.SetInputValues(input);
            foreach (var hiddenLayer in hiddenLayers)
                hiddenLayer.FeedForward();
            outputLayer.FeedForward();
            return outputLayer.Values;
        }

        public void PropagateBack(double[] target)
        {
            outputLayer.SetTargetValues(target);
            outputLayer.PropagateBack();
            foreach (var hiddenLayer in hiddenLayers.Reverse())
                hiddenLayer.PropagateBack();
        }

        public void Save(string path)
        {
            NetworkSaveModel save = new NetworkSaveModel();
            save.Input = inputLayer.Size - 1;
            save.Output = outputLayer.Size;
            save.Hidden = new int[hiddenLayers.Length];
            for (int i = 0; i < hiddenLayers.Length; i++)
                save.Hidden[i] = hiddenLayers[i].Size - 1;

            List<Layer> layers = new List<Layer>();
            layers.Add(inputLayer);
            foreach (var hidden in hiddenLayers)
                layers.Add(hidden);
            layers.Add(outputLayer);

            save.Weights = new Dictionary<string, double>();

            for (int i = 0; i < layers.Count - 1; i++)
            {
                //LAYER_NEURON_LAYER_NEURON
                string key_template = "{0}_{1}_{2}";
                for (int j = 0; j < layers[i].Neurons.Length; j++)
                {
                    for (int k = 0; k < layers[i].Neurons[j].OutBoundSize; k++)
                    {
                        string key = string.Format(key_template, i, j, k);
                        save.Weights.Add(key, layers[i].Neurons[j].OutBoundConnection(k).weight);
                    }
                }
            }

            JsonHelper.Serialize(save, path);

            //JsonHelper.Serialize(this, path);
        }

        public static Network Load(string path, double learnRate)
        {
            if (File.Exists(path))
            {
                NetworkSaveModel model = JsonHelper.Deserialize<NetworkSaveModel>(path);
                Network net = new Network(model.Input, model.Hidden, model.Output, learnRate);

                int lastLayerNumber = model.Hidden.Length + 1; //output layer number - 1
                foreach (var weight in model.Weights)
                {
                    string[] parts = weight.Key.Split('_');
                    //1. in layer number
                    //2. in neuron number
                    //3. out neuron number
                    if (parts.Length != 3)
                        continue;

                    int[] numeric = new int[parts.Length];
                    for (int i = 0; i < parts.Length; i++)
                        numeric[i] = int.Parse(parts[i]);

                    if (numeric[0] == 0) //input layer neurons
                    {
                        net.inputLayer.Neurons[numeric[1]].OutBoundConnection(numeric[2]).weight = weight.Value;
                    }
                    else //layer connected to output
                    {
                        net.hiddenLayers[numeric[0]-1].Neurons[numeric[1]].OutBoundConnection(numeric[2]).weight = weight.Value;
                    }
                }

                return net;
            }
            return null;
            //if (File.Exists(path))
            //{
            //    Network n = JsonHelper.Deserialize<Network>(path);
            //    return n;
            //}
            //return null;
        }
    }
}
