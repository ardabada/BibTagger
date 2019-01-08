using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibOCR.Neural
{
    public abstract class Neuron
    {
        protected readonly Activation activation;
        protected readonly TrainingInfo trainInfo;
        public Neuron(Activation activation, TrainingInfo trainInfo)
        {
            this.activation = activation;
            this.trainInfo = trainInfo;
        }

        protected readonly List<Connection> inboundConnections = new List<Connection>();
        public void AddInboundConnection(Connection connection)
        {
            inboundConnections.Add(connection);
        }


        protected readonly List<Connection> outboundConnections = new List<Connection>();
        public void AddOutboundConnection(Connection connection)
        {
            outboundConnections.Add(connection);
        }

        public Connection OutBoundConnection(int index)
        {
            return outboundConnections[index];
        }
        public int OutBoundSize
        {
            get { return outboundConnections.Count; }
        }

        public double Value { get; protected set; }

        public double Error { get; protected set; }

        public virtual void FeedForward()
        {
            double sum = 0;
            foreach (var conn in inboundConnections)
                sum += conn.WeightedValue;
            Value = activation.CalcValue(sum);
        }

        public virtual void PropagateBack()
        {
            var valueDelta = CalcValueDelta();
            Error = valueDelta * activation.CalcDerivative(Value);

            foreach (var conn in inboundConnections)
                conn.PropagateBack(trainInfo.LearnRate, Error);
        }

        abstract protected double CalcValueDelta();

    }
}
