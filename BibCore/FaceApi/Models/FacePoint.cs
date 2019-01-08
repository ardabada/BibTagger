using System;
using Newtonsoft.Json.Linq;

namespace BibCore.FaceApi.Models
{
    public class FacePoint : IFaceApiData
    {
        public FacePoint() { }
        public FacePoint(JToken json)
        {
            Read(json);
        }

        public int X { get; set; }
        public int Y { get; set; }

        public void Read(JToken json)
        {
            this.X = json["x"].ToObject<int>();
            this.Y = json["y"].ToObject<int>();
        }
    }
}
