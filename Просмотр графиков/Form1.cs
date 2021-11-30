using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Просмотр_графиков
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string path = openFile.FileName;

                List<MassReport> massreports = JsonConvert.DeserializeObject<List<MassReport>>(File.ReadAllText(path));

                this.chart1.Series[0].Points.Clear();

                foreach (var c in massreports)
                {
                    this.chart1.Series[0].Points.AddXY(c.year, c.sales);
                }

                textBox1.Text = path;
            }

            //string file = @"C:\Users\urafr\OneDrive\Рабочий стол\Разработка программы для просмотра графиков из JSON\Графики\1.JSON";

            //if (File.Exists(file))
            //{
            //    List<MassReport> massreports = JsonConvert.DeserializeObject<List<MassReport>>(File.ReadAllText(path));

            //    foreach (var c in massreports)
            //    {
            //        this.chart1.Series[0].Points.AddXY(c.year, c.sales);
            //    }
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.chart1.Series[0].Points.Clear();
            this.chart1.ChartAreas[0].AxisX.LabelStyle.Format = "H:mm:ss";
        }
    }
}
