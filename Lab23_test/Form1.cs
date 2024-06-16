using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab23_test
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            double a;
            if (double.TryParse(textBox1.Text, out a))
            {
                Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    DrawCoordinateSystem(g);
                    DrawGraph(g, a);
                }
                pictureBox1.Image = bmp;
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть правильний коефіцієнт.");
            }
        }
        private void DrawCoordinateSystem(Graphics g)
        {
            Pen axisPen = new Pen(Color.Black, 2);
            // Центр координатної системи
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;

            // Малювання осей
            g.DrawLine(axisPen, 0, centerY, pictureBox1.Width, centerY);
            g.DrawLine(axisPen, centerX, 0, centerX, pictureBox1.Height);

            // Написи для осей
            Font font = new Font("Arial", 10);
            g.DrawString("X", font, Brushes.Black, pictureBox1.Width - 20, centerY + 5);
            g.DrawString("Y", font, Brushes.Black, centerX + 5, 5);

            // Написи значень на осях
            for (int i = -centerX; i < centerX; i += 50)
            {
                g.DrawString((i / 50).ToString(), font, Brushes.Black, centerX + i, centerY + 5);
                g.DrawLine(Pens.Gray, centerX + i, centerY - 5, centerX + i, centerY + 5);
            }
            for (int i = -centerY; i < centerY; i += 50)
            {
                g.DrawString((i / 50).ToString(), font, Brushes.Black, centerX + 5, centerY - i);
                g.DrawLine(Pens.Gray, centerX - 5, centerY - i, centerX + 5, centerY - i);
            }
        }

        private void DrawGraph(Graphics g, double a)
        {
            Pen graphPen = new Pen(Color.Blue, 2);
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;

            PointF prevPoint = PointF.Empty;
            for (double t = 0; t <= 2 * Math.PI; t += 0.01)
            {
                double x = a * Math.Cos(t) * (1 + Math.Sin(t));
                double y = a * Math.Sin(t) * (1 + Math.Cos(t));

                PointF point = new PointF((float)(centerX + x * 50), (float)(centerY - y * 50));
                if (prevPoint != PointF.Empty)
                {
                    g.DrawLine(graphPen, prevPoint, point);
                }
                prevPoint = point;
            }
        }
    }
}
