using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using BibIO;

namespace BibCore
{
    public class TaggableFace
    {
        public TaggableFace() { }
        public TaggableFace(string imagePath, Rectangle face)
        {
            _face = face;
            _imagePath = imagePath;
        }
        public TaggableFace(string imagePath, string faceId, Rectangle face) : this(imagePath, face)
        {
            _faceId = faceId;
        }

        [JsonProperty("image")]
        private string _imagePath = string.Empty;
        [JsonIgnore]
        public string ImagePath
        {
            get { return _imagePath; }
        }

        [JsonProperty("id")]
        private string _faceId = string.Empty;
        [JsonIgnore]
        public string FaceId
        {
            get { return _faceId; }
        }
        [JsonProperty("bounds")]
        private Rectangle _face = Rectangle.Empty;
        [JsonIgnore]
        public Rectangle Face
        {
            get { return _face; }
        }
        private Rectangle _body = Rectangle.Empty;
        [JsonIgnore]
        public Rectangle Body
        {
            get
            {
                if (_body.IsEmpty)
                    _body = findBody(_face);
                return _body;
            }
        }
        
        private List<string> _bibs = new List<string>();
        [JsonIgnore]
        public List<string> DetectedBibs
        {
            get { return _bibs; }
        }
        [JsonIgnore]
        public List<string> NumericBibs
        {
            get
            {
                List<string> result = new List<string>();
                foreach (var bib in DetectedBibs)
                {
                    string numeric = Regex.Match(bib.Replace(" ", ""), @"\d+").Value;
                    result.Add(numeric);
                }
                return result;
            }
        }

        //private string _bib = string.Empty;
        //public string DetectedBib
        //{
        //    get { return _bib; }
        //    set { _bib = value; }
        //}

        private static Rectangle findBody(Rectangle face)
        {
            //TODO: review body rectangle size and position

            var bodyHeight = 3 * face.Height;
            var bodyWidth = 7 / 3 * face.Width;
            //var bodyHeight = 3.7 * face.Height;
            //var bodyWidth = 2.7 * face.Width;
            var y_body = face.Y + face.Height + 0.5 * face.Height;
            var x_body = face.X + 0.5 * face.Width - 0.5 * bodyWidth;

            return new Rectangle((int)x_body, (int)y_body, (int)bodyWidth, (int)bodyHeight);
        }
    }

    public class TaggableResult
    {
        [JsonProperty("base")]
        public string BaseDirectory { get; set; } = string.Empty;
        [JsonProperty("photos")]
        public List<TaggableImage> Images { get; set; } = new List<TaggableImage>();

        public TaggableResult() { }
        public TaggableResult(string baseDir)
        {
            BaseDirectory = baseDir;
        }

        public void Save(string path)
        {
            JsonHelper.Serialize(this, path);
        }
        public static TaggableResult Load(string path)
        {
            return JsonHelper.Deserialize<TaggableResult>(path);
        }

        public void AddImage(TaggableImage img)
        {
            Images.Add(img);
        }

        public bool PhotoExists(string fullPath)
        {
            foreach (var img in Images)
            {
                if (Path.Combine(BaseDirectory, img.Path).ToLower() == fullPath.ToLower())
                    return true;
            }
            return false;
        }
    }

    public class TaggableImage
    {
        [JsonProperty("path")]
        public string Path { get; set; } = string.Empty;
        [JsonProperty("data")]
        public List<TaggableImageData> Data { get; set; } = new List<TaggableImageData>();

        public TaggableImage() { }
        public TaggableImage(string basePath, string fullPath)
        {
            Path = FileManager.GetRelativePath(basePath, fullPath);
        }

        public void AddFace(string id, List<string> bibs)
        {
            Data.Add(new TaggableImageData(id, bibs));
        }
        public void AddFace(string id, string resolvedBib, List<string> bibs)
        {
            Data.Add(new TaggableImageData(id, resolvedBib, bibs));
        }
    }

    public class TaggableImageData
    {
        [JsonProperty("id")]
        public string FaceId { get; set; } = string.Empty;
        [JsonProperty("resolved")]
        public bool Resolved { get; set; } = false;
        [JsonProperty("resolvedBib")]
        public string ResolvedBib { get; set; } = string.Empty;
        [JsonProperty("bibs")]
        public List<string> Bibs { get; set; } = new List<string>();

        public TaggableImageData() { }
        public TaggableImageData(string faceId, List<string> bibs)
        {
            FaceId = faceId;
            Resolved = false;
            ResolvedBib = string.Empty;
            Bibs = bibs;
        }
        public TaggableImageData(string faceId, string resolvedBib, List<string> bibs)
        {
            FaceId = faceId;
            Resolved = true;
            ResolvedBib = resolvedBib;
            Bibs = bibs;
        }
    }
}
