using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CSV;

namespace BibTagger
{
    public partial class BibColumnsForm : Form
    {
        public BibColumnsForm()
        {
            InitializeComponent();
        }

        private CsvRow _header = null;
        public CsvRow Header
        {
            get { return _header; }
            set
            {
                _header = value;
                foreach (var column in value.Columns)
                {
                    listBox.Items.Add(column);
                }
            }
        }

        public int SelectedIndex
        {
            get { return listBox.SelectedIndex; }
            set
            {
                if (listBox.Items.Count > value)
                    listBox.SelectedIndex = value;
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
