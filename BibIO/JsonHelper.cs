using Newtonsoft.Json;
using System.IO;

namespace BibIO
{
    public class JsonHelper
    {
        public static void Serialize<T>(T obj, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(obj, Formatting.Indented));
        }
        public static T Deserialize<T>(string path)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }
    }
}
