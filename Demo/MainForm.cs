using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

namespace Demo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            loadData();
            openFileDialog.InitialDirectory = BaseDirectory;
        }

        public string BaseDirectory
        {
            get { return Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)); }
        }

        PreservedElement[] elements = null;

        void loadData()
        {
            string path = Path.Combine(BaseDirectory, "data.json");
            elements = JsonConvert.DeserializeObject<PreservedElement[]>(File.ReadAllText(path));
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string name = openFileDialog.FileName;
                string part = Path.GetFileName(name);
                var elems = elements.Where(x => x.Image == part);
                if (elems.Any())
                {
                    var item = elems.First();
                    pictureBox.ImageLocation = name;
                    Thread.Sleep(item.Time);
                    resultsPanel.Controls.Clear();
                    addLabel(item.Numbers.ToArray());
                    timeLbl.Text = item.Time + " ms";
                }
                else MessageBox.Show("Selected image is blocked by core.", "Demo mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void addLabel(string[] text)
        {
            Label lbl = new Label();
            lbl.Font = new Font(timeLbl.Font.FontFamily, 48);
            lbl.ForeColor = Color.DodgerBlue;
            lbl.Text = "";
            for (int i = 0; i < text.Length; i++)
                lbl.Text += Environment.NewLine + text[i];
            lbl.Dock = DockStyle.Fill;
            lbl.Parent = resultsPanel;
            resultsPanel.Controls.Add(lbl);
        }
    }
}
