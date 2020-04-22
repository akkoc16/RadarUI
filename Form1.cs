using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Radar
{
    public partial class Form1 : Form
    {
        Timer t = new Timer();

        int WIDTH = 1000, HEIGHT = 1000, HAND = 500;
        int u;
        int cx, cy;
        int x, y;
        int xDelay, yDelay, txDelay, tyDelay;
        int tx, ty, lim = 20;

        Bitmap bmp;
        Pen p;
        Graphics g;


        public Form1()
        {

            InitializeComponent();


        }

        private void set_background(object sender, PaintEventArgs e)
        {

            Graphics graphics = e.Graphics;
            Rectangle gradient_rectangle = new Rectangle(0, 0, Width, Height);
            Brush b = new LinearGradientBrush(gradient_rectangle, Color.FromArgb(0, 0, 0), Color.FromArgb(57, 128, 227), 65f);
            graphics.FillRectangle(b, gradient_rectangle);

        }
        private void Form1_Load(object sender, EventArgs e)
        {

            bmp = new Bitmap(WIDTH + 1, HEIGHT + 1);



            this.BackColor = Color.Black;
            cx = WIDTH / 2;
            cy = HEIGHT / 2;
            u = 0;
            t.Interval = 25;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();

        }

        private void t_Tick(object sender, EventArgs e)
        {

            p = new Pen(Color.DarkGreen, 2f);
            g = Graphics.FromImage(bmp);
            int ellipseCount = 10;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


            // Transparan Elipse kodu
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(2, 10, 204, 0));                      //Transparan Elipse kodu burda 
            g.FillEllipse(semiTransBrush, 0, 0, WIDTH, HEIGHT);
            // transparan elipse kodu


            int tu = (u - lim) % 360;

            if (u >= 0 && u <= 180)
            {
                x = cx + (int)(HAND * Math.Sin(Math.PI * u / 180));
                y = cy - (int)(HAND * Math.Cos(Math.PI * u / 180));
            }
            else
            {
                x = cx - (int)(HAND * -Math.Sin(Math.PI * u / 180));
                y = cy - (int)(HAND * Math.Cos(Math.PI * u / 180));
            }

            if (tu >= 0 && tu <= 180)
            {
                tx = cx + (int)(HAND * Math.Sin(Math.PI * tu / 180));
                ty = cy - (int)(HAND * Math.Cos(Math.PI * tu / 180));
            }
            else
            {
                tx = cx - (int)(HAND * -Math.Sin(Math.PI * tu / 180));
                ty = cy - (int)(HAND * Math.Cos(Math.PI * tu / 180));
            }

            for (int i = 0; i < ellipseCount; i++)
            {
                g.DrawEllipse(p, WIDTH / 2 * i / ellipseCount,
                    HEIGHT / 2 * i / ellipseCount,
                    WIDTH - WIDTH / ellipseCount * i,
                    HEIGHT - HEIGHT / ellipseCount * i);
            }

            Pen p2 = new Pen(Color.FromArgb(140, 10, 204, 0), 1f);
            Pen p3 = new Pen(Color.FromArgb(190, 10, 204, 0), 5f);


            g.DrawLine(p2, new Point(cx, 0), new Point(cx, HEIGHT));
            g.DrawLine(p2, new Point(0, cy), new Point(WIDTH, cy));


            g.DrawLine(new Pen(Color.Black, 5f), new Point(cx, cy), new Point(tx, ty));

            g.DrawLine(p3, new Point(cx, cy), new Point(x, y));

            pictureBox1.Image = bmp;
            p.Dispose();
            g.Dispose();

            u++;
            if (u == 360)
            {
                u = 0;
            }
        }

    }
}
