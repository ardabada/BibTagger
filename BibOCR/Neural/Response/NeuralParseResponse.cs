using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibOCR.Neural.Response
{
    public class NeuralNumberResponse
    {
        public List<DataShift> Chars { get; set; } = new List<DataShift>();

        public static NeuralNumberResponse Parse(double[] output)
        {
            NeuralNumberResponse result = new NeuralNumberResponse();
            result.Chars = new List<DataShift>(); //(output.Length);
            for (int i = 0; i<output.Length; i++)
            {
                result.Chars.Add(new DataShift(i, output[i]));
            }
            return result;
        }
    }

    public class NeuralParseResponse
    {
        public List<NeuralNumberResponse> Chars { get; set; } = new List<NeuralNumberResponse>();

        public string Value
        {
            get
            {
                string result = string.Empty;
                for (int c_i = 0; c_i < Chars.Count; c_i++)
                {
                    var c = Chars[c_i];

                    int maxIndex = 0;
                    for (int i = 1; c_i == 0 ? i < c.Chars.Count : i <= 10; i++)
                    {
                        if (c.Chars[i].Confidence > c.Chars[maxIndex].Confidence)
                            maxIndex = i;
                    }

                    if (c.Chars[maxIndex].Confidence >= OCRParser.MIN_CONFIDENCE)
                        result += c.Chars[maxIndex].Value;
                    else result += " ";
                }
                return result;
            }
        }
    }
}
