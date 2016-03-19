using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example1
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
            g.Clear(Color.White);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g.DrawRectangle(new Pen(Color.Red), 0, 0, 230, 230);
            pictureBox1.Refresh();
        }

        
        Queue<Point> order = new Queue<Point>();

        bool[,] used = new bool[500, 500];

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            F1(e.Location);
            //pictureBox1.Image = bmp;
        }

        private void F2(Point p)
        {
            SimplePaint.MapFill m = new SimplePaint.MapFill();
            m.Fill(g, p, Color.Blue, ref bmp);
        }

        private void F1(Point pp)
        {
            order.Enqueue(pp);
            used[pp.X,pp.Y] = true;

            while (order.Count > 0)
            {
                Point p = order.Dequeue();
                Step(p.X + 1, p.Y, Color.Blue, Color.White);
                Step(p.X, p.Y + 1, Color.Blue, Color.White);
                Step(p.X - 1, p.Y, Color.Blue, Color.White);
                Step(p.X, p.Y - 1, Color.Blue, Color.White);
            }
        }

        private void Step(int x, int y, Color color, Color colorOfClickedPoint)
        {
            if (x < 0 || y < 0) return;
            if (x > pictureBox1.Width || y > pictureBox1.Height) return;
            if (used[x, y]) return;
            if (!colorsAreSame(bmp.GetPixel(x,y),colorOfClickedPoint)) return;

            order.Enqueue(new Point(x, y));
            used[x, y] = true;
            bmp.SetPixel(x, y, color);
            pictureBox1.Refresh();
        }

        private bool colorsAreSame(Color color, Color colorOfClickedPoint)
        {
            if (color.A == colorOfClickedPoint.A &&
                color.R == colorOfClickedPoint.R &&
                color.G == colorOfClickedPoint.G &&
                color.B == colorOfClickedPoint.B
                )
                return true;

            return false;
        }
    }
}
