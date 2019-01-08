using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSV
{
    public class CsvManager
    {
        const char CVS_SEPARATOR = ';';

        private static bool _cancelationRequested = false;

        public static void CancelOperation()
        {
            _cancelationRequested = true;
        }
        private static void resetCancelationStatus()
        {
            _cancelationRequested = false;
        }

        public static bool SaveCsv(List<CsvRow> data, string path)
        {
            try
            {
                resetCancelationStatus();
                string content = "";

                for (int i = 0; i < data.Count; i++)
                {
                    for (int j = 0; j < data[i].Columns.Count; j++)
                    {
                        content += "\"" + data[i].Columns[j] + "\"" + CVS_SEPARATOR;
                        if (_cancelationRequested)
                            break;
                    }
                    if (_cancelationRequested)
                        break;
                    content += Environment.NewLine;
                }

                StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
                sw.Write(content);
                sw.Close();

                return true;
            }
            catch { return false; }
        }

        public static List<CsvRow> ReadAll(string path)
        {
            try
            {
                resetCancelationStatus();
                List<CsvRow> result = new List<CsvRow>();

                StreamReader reader = new StreamReader(path, Encoding.UTF8);
                while (reader.Peek() >= 0)
                {
                    if (_cancelationRequested)
                        break;
                    result.Add(parse(reader.ReadLine()));
                }

                return result;
            }
            catch { return new List<CsvRow>(); }
        }
        public static CsvRow ReadHeader(string path)
        {
            try
            {
                StreamReader reader = new StreamReader(path, Encoding.UTF8);
                return parse(reader.ReadLine());
            }
            catch { return new CsvRow(); }
        }
        public static List<CsvRow> ReadAllWithoutHeader(string path)
        {
            var data = ReadAll(path);

            if (data.Any())
                return data.Skip(1).ToList();
            return data;
        }

        static CsvRow parse(string line)
        {
            try
            {
                CsvRow result = new CsvRow();
                string[] parts = line.Split(CVS_SEPARATOR);
                foreach (var part in parts)
                {
                    if (part.Length > 2)
                        result.Add(part.Substring(1, part.Length - 2));
                }

                return result;
            }
            catch { return new CsvRow(); }
        }
    }
}
