using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibOCR.Neural
{
    public class NetworkSaveModel
    {
        public int Input { get; set; }
        public int Output { get; set; }
        public int[] Hidden { get; set; }
        public Dictionary<string, double> Weights { get; set; }
    }
}
