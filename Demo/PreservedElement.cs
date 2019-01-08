using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Demo
{
    public class PreservedElement
    {
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("numbers")]
        public List<string> Numbers { get; set; }
        [JsonProperty("t")]
        public int Time { get; set; }
    }
}
