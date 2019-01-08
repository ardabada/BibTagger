using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BibCore.FaceApi.Models
{
    public class OneEyeStatus : IFaceApiData
    {
        public OneEyeStatus() { }
        public OneEyeStatus(JToken json)
        {
            Read(json);
        }

        public double DarkGlasses { get; set; }
        public double NoGlassEyeClose { get; set; }
        public double NoGlassEyeOpen { get; set; }
        public double NormalGlassEyeClose { get; set; }
        public double NormalGlassEyeOpen { get; set; }
        public double Occlusion { get; set; }

        public void Read(JToken json)
        {
            DarkGlasses = json["dark_glasses"].ToObject<double>();
            NoGlassEyeClose = json["no_glass_eye_close"].ToObject<double>();
            NoGlassEyeOpen = json["no_glass_eye_open"].ToObject<double>();
            NormalGlassEyeClose = json["normal_glass_eye_close"].ToObject<double>();
            NormalGlassEyeOpen = json["normal_glass_eye_open"].ToObject<double>();
            Occlusion = json["occlusion"].ToObject<double>();
        }
    }
}
