using System.Collections.Generic;
using System.Linq;

namespace CSV
{
    public class CsvRow
    {
        public List<string> Columns { get; set; }

        public CsvRow()
        {
            Columns = new List<string>();
        }
        public CsvRow(params string[] value)
        {
            Columns = value.ToList();
        }

        public void Add(string value)
        {
            Columns.Add(value);
        }
        public void Add(params string[] value)
        {
            Columns.AddRange(value);
        }
    }
}
