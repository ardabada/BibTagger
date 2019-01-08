using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BibCore.FaceApi.Models
{
    public class FaceAttributes : IFaceApiData
    {
        public FaceAttributes() { }
        public FaceAttributes(JToken json)
        {
            Read(json);
        }

        public int Age { get; set; }
        public BlurData Blur { get; set; }
        public string Ethnicity { get; set; }
        public EyeStatus EyeStatus { get; set; }
        public Gender Gender { get; set; }
        public ThresholdValue FaceQuality { get; set; }
        public string Glass { get; set; }
        public Headpose Headpose { get; set; }
        public ThresholdValue Smiling { get; set; }

        public void Read(JToken json)
        {
            if (json["age"] != null)
                Age = json["age"]["value"].ToObject<int>();

            if (json["blur"] != null)
                Blur = new BlurData(json["blur"]);

            if (json["ethnicity"] != null)
                Ethnicity = json["ethnicity"]["value"].ToString();

            if (json["eyestatus"] != null)
                EyeStatus = new EyeStatus(json["eyestatus"]);

            if (json["gender"] != null)
                Gender = FaceManager.ParseGenger(json["gender"]["value"].ToString());

            if (json["facequality"] != null)
                FaceQuality = new ThresholdValue(json["facequality"]);

            if (json["headpose"] != null)
                Headpose = new Headpose(json["headpose"]);

            if (json["smile"] != null)
                Smiling = new ThresholdValue(json["smile"]);
        }
    }
}
