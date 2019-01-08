using System;
using Newtonsoft.Json.Linq;

namespace BibCore.FaceApi.Models
{
    public class ThresholdValue : IFaceApiData
    {
        public ThresholdValue() { }
        public ThresholdValue(JToken json)
        {
            Read(json);
        }

        public double Threshold { get; set; }
        public double Value { get; set; }

        public void Read(JToken json)
        {
            Threshold = json["threshold"].ToObject<double>();
            Value = json["value"].ToObject<double>();
        }
    }
}
