using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibOCR.Neural
{
    public class Connection
    {
        readonly Neuron from;
        readonly Neuron to;
        [JsonProperty("weight")]
        public double weight;

        public Connection(Neuron from, Neuron to, double weight)
        {
            from.AddOutboundConnection(this);
            this.from = from;

            to.AddInboundConnection(this);
            this.to = to;

            this.weight = weight;
        }

        public double WeightedValue
        {
            get { return from.Value * weight; }
        }

        public double WeightedError { get; private set; }

        public void PropagateBack(double learnRate, double error)
        {
            WeightedError = error * weight;

            var weightDelta = learnRate * error * from.Value;
            weight += weightDelta;
        }
    }
}
