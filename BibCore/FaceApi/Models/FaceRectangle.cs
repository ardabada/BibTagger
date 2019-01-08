using System;
using Newtonsoft.Json.Linq;

namespace BibCore.FaceApi.Models
{
    public class FaceRectangle : IFaceApiData
    {
        public FaceRectangle() { }
        public FaceRectangle(JToken json)
        {
            Read(json);
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void Read(JToken json)
        {
            Width = json["width"].ToObject<int>();
            Height = json["height"].ToObject<int>();
            Y = json["top"].ToObject<int>();
            X = json["left"].ToObject<int>();
        }
    }
}
