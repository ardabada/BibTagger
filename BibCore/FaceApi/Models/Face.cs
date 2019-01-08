using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BibCore.FaceApi.Models
{
    public class Face : IFaceApiData
    {
        public Face() { }
        public Face(JToken json)
        {
            Read(json);
        }

        public string FaceToken { get; set; }
        public FaceRectangle FaceRectangle { get; set; }
        public FaceLandmarks Landmark { get; set; }
        public FaceAttributes Attributes { get; set; }

        public void Read(JToken json)
        {
            FaceToken = json["face_token"].ToString();
            FaceRectangle = new FaceRectangle(json["face_rectangle"]);
            if (json["landmark"] != null)
                Landmark = new FaceLandmarks(json["landmark"]);
            if (json["attributes"] != null)
                Attributes = new FaceAttributes(json["attributes"]);
        }
    }
}
