using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BibCore.FaceApi.Models
{
    public class Headpose : IFaceApiData
    {
        public Headpose() { }
        public Headpose(JToken json)
        {
            Read(json);
        }

        public double PitchAngle { get; set; }
        public double RollAngle { get; set; }
        public double YawAngle { get; set; }

        public void Read(JToken json)
        {
            PitchAngle = json["pitch_angle"].ToObject<double>();
            RollAngle = json["roll_angle"].ToObject<double>();
            YawAngle = json["yaw_angle"].ToObject<double>();
        }
    }
}
