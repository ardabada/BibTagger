using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BibCore.FaceApi.Models
{
    public class FaceDetectionResult : IFaceApiData
    {
        public FaceDetectionResult()
        {
            Faces = new List<Face>();
        }
        public FaceDetectionResult(JToken json)
        {
            Read(json);
        }

        public List<Face> Faces { get; set; }

        public void Read(JToken json)
        {
            Faces = new List<Face>();
            if (json["faces"].HasValues)
                foreach (var face in json["faces"])
                {
                    Faces.Add(new Face(face));
                }
        }
    }
}
