using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BibCore.FaceApi.Models
{
    public class EyeStatus : IFaceApiData
    {
        public EyeStatus() { }
        public EyeStatus(JToken json)
        {
            Read(json);
        }

        public OneEyeStatus Left { get; set; }
        public OneEyeStatus Right { get; set; }

        public void Read(JToken json)
        {
            Left = new OneEyeStatus(json["left_eye_status"]);
            Right = new OneEyeStatus(json["right_eye_status"]);
        }
    }
}
