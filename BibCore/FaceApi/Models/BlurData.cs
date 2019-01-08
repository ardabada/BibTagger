using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BibCore.FaceApi.Models
{
    public class BlurData : IFaceApiData
    {
        public BlurData() { }
        public BlurData(JToken json)
        {
            Read(json);
        }

        public ThresholdValue Blurness { get; set; }
        public ThresholdValue GaussianBlur { get; set; }
        public ThresholdValue MotionBlur { get; set; }

        public void Read(JToken json)
        {
            Blurness = new ThresholdValue(json["blurness"]);
            GaussianBlur = new ThresholdValue(json["gaussianblur"]);
            MotionBlur = new ThresholdValue(json["motionblur"]);
        }
    }
}
