using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BibCore.FaceApi.Models
{
    public class FaceSearchModel : IFaceApiData
    {
        public double Confidence { get; set; }
        public string Tag { get; set; }
        public string FaceId { get; set; }

        public void Read(JToken json)
        {
            Confidence = json["confidence"].ToObject<double>();
            Tag = json["user_id"].ToString();
            FaceId = json["face_token"].ToString();
        }
    }
}
