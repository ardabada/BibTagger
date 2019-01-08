using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using BibCore.FaceApi.Models;
using System.IO;
using System.Drawing;

namespace BibCore.FaceApi
{
    public class FaceManager
    {
        /// <summary>
        /// Face++ app key
        /// </summary>
        private const string APP_KEY = "dnenos3pXbn5MQiVmwfJ9mIc-WNrO4tL";
        /// <summary>
        /// Face++ app secret
        /// </summary>
        private const string APP_SECRET = "iVMOBqEz3u7puGJe7AhYrWHUE0c7naS3";

        #region Private methods

        private static string request(string path, NameValueCollection query)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string url = "https://api-us.faceplusplus.com/facepp/v3/" + path;
                    query.Add("api_key", APP_KEY);
                    query.Add("api_secret", APP_SECRET);
                    byte[] response = client.UploadValues(url, query);
                    string result = Encoding.UTF8.GetString(response);
                    return result;
                }
            }
            catch { return "{}"; }
        }
        private static JObject getJsonReturn(string response)
        {
            return JObject.Parse(response);
        }
        internal static JObject getJsonReturn(string path, NameValueCollection query)
        {
            return getJsonReturn(request(path, query));
        }
        internal static T getResponse<T>(JObject json) where T : IFaceApiData, new()
        {
            try
            {
                T result = new T();
                result.Read(json);
                return result;
            }
            catch
            {
                return default(T);
            }
        }
        internal static T getResponse<T>(string response) where T : IFaceApiData, new()
        {
            return getResponse<T>(getJsonReturn(response));
        }
        internal static T getResponse<T>(string path, NameValueCollection query) where T : IFaceApiData, new()
        {
            return getResponse<T>(getJsonReturn(path, query));
        }

        #endregion

        #region Internal methods

        internal static Gender ParseGenger(string gender)
        {
            switch (gender.ToLower())
            {
                case "male":
                    return Gender.Male;
                case "female":
                    return Gender.Female;
                default:
                    return Gender.Unknown;
            }
        }

        #endregion

        #region Methods implimentation

        public static FaceDetectionResult DetectFaceFromUrl(string imageUrl)
        {
            return DetectFaceFromUrl(imageUrl, false, FaceAttributesValues.All);
        }
        public static FaceDetectionResult DetectFaceFromUrl(string imageUrl, FaceAttributesValues faceAttributes)
        {
            return DetectFaceFromUrl(imageUrl, false, faceAttributes);
        }
        public static FaceDetectionResult DetectFaceFromUrl(string imageUrl, bool landmarks)
        {
            return DetectFaceFromUrl(imageUrl, landmarks, FaceAttributesValues.All);
        }
        public static FaceDetectionResult DetectFaceFromUrl(string imageUrl, bool landmarks, FaceAttributesValues faceAttributes)
        {
            string attributes = "gender,age,smiling,headpose,blur,facequality,eyestatus,ethnicity";
            if (!faceAttributes.HasFlag(FaceAttributesValues.All))
            {
                attributes = "";
                foreach (var attr in Enum.GetValues(typeof(FaceAttributesValues)).Cast<Enum>().Where(faceAttributes.HasFlag))
                {
                    attributes += attr.ToString().ToLower() + ",";
                }
                //TODO: check if last , removing
                attributes = attributes.Substring(0, attributes.Length);
            }
            return getResponse<FaceDetectionResult>("detect", new NameValueCollection()
            {
                { "image_url", imageUrl },
                { "return_landmark", (landmarks ? "1" : "0") },
                { "return_attributes", attributes }
            });
        }

        public static FaceDetectionResult DetectFaceFromFile(string imageUrl)
        {
            return DetectFaceFromFile(imageUrl, false, FaceAttributesValues.All);
        }
        public static FaceDetectionResult DetectFaceFromFile(string imageUrl, FaceAttributesValues faceAttributes)
        {
            return DetectFaceFromFile(imageUrl, false, faceAttributes);
        }
        public static FaceDetectionResult DetectFaceFromFile(string imageUrl, bool landmarks)
        {
            return DetectFaceFromFile(imageUrl, landmarks, FaceAttributesValues.All);
        }
        public static FaceDetectionResult DetectFaceFromFile(string imagePath, bool landmarks, FaceAttributesValues faceAttributes)
        {
            if (!File.Exists(imagePath))
                return null;

            string attributes = "gender,age,smiling,headpose,blur,facequality,eyestatus,ethnicity";
            if (!faceAttributes.HasFlag(FaceAttributesValues.All))
            {
                attributes = "";
                foreach (var attr in Enum.GetValues(typeof(FaceAttributesValues)).Cast<Enum>().Where(faceAttributes.HasFlag))
                {
                    attributes += attr.ToString().ToLower() + ",";
                }
                //TODO: check if last , removing
                attributes = attributes.Substring(0, attributes.Length);
            }

            string base64 = ImageBase64.ImageToBase64(Image.FromFile(imagePath), System.Drawing.Imaging.ImageFormat.Jpeg);
            return getResponse<FaceDetectionResult>("detect", new NameValueCollection()
            {
                { "image_base64", base64 },
                { "return_landmark", (landmarks ? "1" : "0") },
                { "return_attributes", attributes }
            });
        }
        
        #endregion
    }
}
