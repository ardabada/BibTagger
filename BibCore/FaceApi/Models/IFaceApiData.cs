using Newtonsoft.Json.Linq;

namespace BibCore.FaceApi.Models
{
    /// <summary>
    /// Contains method for reading api returns
    /// </summary>
    public interface IFaceApiData
    {
        /// <summary>
        /// Reads api json return
        /// </summary>
        void Read(JToken json);
    }
}
