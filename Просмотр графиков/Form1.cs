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
using System.Windows.Forms.DataVisualization.Charting;

namespace Просмотр_графиков
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Присваивание событию прокрутки колесика метода для обработки события
            chart1.MouseWheel += chart1_MouseWheel;
        }

        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            var chart = (Chart)sender;
            //получение данных о осях
            var xAxis = chart.ChartAreas[0].AxisX;
            var yAxis = chart.ChartAreas[0].AxisY;

            try
            {
                //Если прокрутка вниз
                if (e.Delta < 0) 
                {
                    xAxis.ScaleView.ZoomReset();
                    yAxis.ScaleView.ZoomReset();
                }
                //Если прокрутка вверх
                else if (e.Delta > 0) 
                {
                    var xMin = xAxis.ScaleView.ViewMinimum;
                    var xMax = xAxis.ScaleView.ViewMaximum;
                    var yMin = yAxis.ScaleView.ViewMinimum;
                    var yMax = yAxis.ScaleView.ViewMaximum;

                    var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                    xAxis.ScaleView.Zoom(posXStart, posXFinish);
                    yAxis.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog openFile = new OpenFileDialog();

            //Открытия окна для выбора файла
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                //Помещение пути в переменную
                string path = openFile.FileName;

                //Десерилизация файла и помещение данных в список объектов класса , схожих по структуре с файлом json
                List<MassReport> massreports = JsonConvert.DeserializeObject<List<MassReport>>(File.ReadAllText(path));

                //Очистка графика
                this.chart1.Series[0].Points.Clear();

                //Перебор всех значений и их размещение по осям
                foreach (var c in massreports)
                {
                    this.chart1.Series[0].Points.AddXY(c.year, c.sales);
                }

                //Вывод информации о пути к используемому файлу
                textBox1.Text = path;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Очистка графика при запуске
            this.chart1.Series[0].Points.Clear();
            //Устанавливается формат для времениЮ для отображения секунд по оси x
            this.chart1.ChartAreas[0].AxisX.LabelStyle.Format = "H:mm:ss";
        }


    }
}
