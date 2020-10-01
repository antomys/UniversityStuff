using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChebyshevInterpolation
{
    public partial class MainForm : Form
    {
        private const int Split = 10;
        private double[] coeff;
        private double[,] lx, ly;
        private double[,] nx, ny;
        private double[] tx;
        private double[] ty;
        private int method;
        private int number;
        private int m;
        private Brush brush;
        private Font font;
        private Pen pen1, pen2, pen3, pen4, pen5, pen6;
        private Complex complex;
        private Complex[] cy;
        private Interpolate inter;
        private Series series;

        public MainForm()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            brush = new SolidBrush(Color.Black);
            pen1 = new Pen(Color.Black);
            pen2 = new Pen(Color.Blue);
            pen3 = new Pen(Color.Red);
            pen4 = new Pen(Color.Green);
            pen5 = new Pen(Color.Purple);
            pen6 = new Pen(Color.Magenta);
            font = new Font("Times New Roman", 12f, FontStyle.Bold);
            panel1.Paint += new PaintEventHandler(PanelPaintHandler);
        }

        private double f(double x)
        {
            return Math.Exp(-(x * x)) * Math.Sin(Math.PI * (Math.Pow(x, 2)));
        }

        private float F(double x)
        {
            return (float)f(x);
        }

        private float G(int j)
        {
            double sum = 0.0;

            for (int k = 0; k <= number; k++)
                sum += Math.Cos(k * j * Math.PI / number) * coeff[k];

            return (float)sum;
        }

        private float H(double z)
        {
            double[] x = new double[number + 1];
            double[] y = new double[number + 1];

            int index = -1;

            for (int i = 0; i < Split; i++)
            {
                if (z >= lx[i, 0] && z <= lx[i, number])
                {
                    index = i;
                    break;
                }

                else if (z < lx[i, 0])
                {
                    index = 0;
                    break;
                }

                else if (z > lx[Split - 1, number])
                {
                    index = Split - 1;
                    break;
                }
            }

            for (int i = 0; i <= number; i++)
            {
                x[i] = lx[index, i];
                y[i] = ly[index, i];
            }

            return (float)inter.Lagrange(number + 1, z, x, y);
        }

        private float I(double x)
        {
            int index = -1;

            for (int i = 0; i < Split; i++)
            {
                if (x >= nx[i, 0] && x <= nx[i, number])
                {
                    index = i;
                    break;
                }

                else if (x < nx[i, 0])
                {
                    index = 0;
                    break;
                }

                else if (x > nx[Split - 1, number])
                {
                    index = Split - 1;
                    break;
                }
            }

            double sum = 0;

            for (int i = 0; i <= number; i++)
            {
                double prod = 1.0;

                for (int j = 0; j < i; j++)
                    prod *= x - nx[index, j];

                sum += ny[index, i] * prod;
            }

            return (float)sum;
        }

        private void DrawGraph(float u0, float v0,
            float u1, float v1,
            Graphics g)
        {
            try
            {
                float xMin = u0;
                float yMin = v0;
                float xMax = u1;
                float yMax = v1;

                float xSpan = xMax - xMin;
                float ySpan = yMax - yMin;

                float deltaX = xSpan / 8.0f;
                float deltaY = ySpan / 8.0f;
                float height = panel1.Height;
                float width = panel1.Width;

                float sx0 = 2f * width / 16f;
                float sx1 = 14f * width / 16f;
                float sy0 = 2f * height / 16f;
                float sy1 = 14f * height / 16f;

                float xSlope = (sx1 - sx0) / xSpan;
                float xInter = sx0 - xSlope * xMin;
                float ySlope = (sy0 - sy1) / ySpan;
                float yInter = sy0 - ySlope * yMax;

                float x = xMin;
                float y = yMin;

                string fTitle = "Графік функції f(x) = e(-x^2)sin(pi * x)";

                float w = g.MeasureString(fTitle, font).Width;
                float h = g.MeasureString(fTitle, font).Height;

                g.DrawString(fTitle, font, brush,
                    (width - w) / 2f, h);

                string xTitle = "Ікс";
                w = g.MeasureString(xTitle, font).Width;
                g.DrawString(xTitle, font, brush,
                    sx0 + (sx1 - sx0 - w) / 2f, sy1 + h + h);

                string yTitle = "Ігрик";
                w = g.MeasureString(yTitle, font).Width;
                g.DrawString(yTitle, font, brush,
                    sx1 + w / 5f, sy0 + (sy1 - sy0) / 2f - h / 2f);

                while (x <= xMax)
                {
                    float sx = xSlope * x + xInter;
                    string s = string.Format("{0,5:0.00}", x);

                    g.DrawLine(pen1, sx, sy0, sx, sy1);

                    w = g.MeasureString(s, font).Width;
                    g.DrawString(s, font, brush,
                        sx - w / 2, sy1 + h / 2f);
                    x += deltaX;
                }

                while (y <= yMax)
                {
                    float sy = ySlope * y + yInter;
                    string s = string.Format("{0,5:0.00}", y);

                    w = g.MeasureString(s, font).Width;
                    g.DrawLine(pen1, sx0, sy, sx1, sy);
                    g.DrawString(s, font, brush,
                        sx0 - w - w / 5f, sy - h / 2f);
                    y += deltaY;
                }

                g.Clip = new Region(new RectangleF(
                    sx0, sy0, (sx1 - sx0), (sy1 - sy0)));

                deltaX = (xMax - xMin) / 256.0f;

                x = xMin;
                y = F(x);

                int tx0 = (int)(xSlope * x + xInter);
                int ty0 = (int)(ySlope * y + yInter);

                while (x <= xMax)
                {
                    x += deltaX;
                    y = F(x);

                    int tx1 = (int)(xSlope * x + xInter);
                    int ty1 = (int)(ySlope * y + yInter);

                    g.DrawLine(pen1, tx0, ty0, tx1, ty1);

                    tx0 = tx1;
                    ty0 = ty1;
                }

                if (method == 0 && inter != null)
                {
                    int j = 0;

                    x = xMin;
                    y = G(j);
                    deltaX = (xMax - xMin) / number;
                    tx0 = (int)(xSlope * x + xInter);
                    ty0 = (int)(ySlope * y + yInter);

                    while (x <= xMax)
                    {
                        j++;
                        x += deltaX;
                        y = G(j);
                        
                        int tx1 = (int)(xSlope * x + xInter);
                        int ty1 = (int)(ySlope * y + yInter);

                        g.DrawLine(pen2, tx0, ty0, tx1, ty1);

                        tx0 = tx1;
                        ty0 = ty1;
                    }
                }

                else if (method == 1 && series != null)
                {
                    x = xMin;
                    y = (float)series.ComputeSeries(x);
                    deltaX = (xMax - xMin) / 256.0f;
                    tx0 = (int)(xSlope * x + xInter);
                    ty0 = (int)(ySlope * y + yInter);

                    while (x <= xMax)
                    {
                        x += deltaX;
                        y = (float)series.ComputeSeries(x);

                        int tx1 = (int)(xSlope * x + xInter);
                        int ty1 = (int)(ySlope * y + yInter);

                        g.DrawLine(pen3, tx0, ty0, tx1, ty1);

                        tx0 = tx1;
                        ty0 = ty1;
                    }
                }

                else if (method == 2 && inter != null)
                {
                    for (int i = 0; i < Split; i++)
                    {
                        tx0 = (int)(xSlope * lx[i, 0] + xInter);
                        ty0 = (int)(ySlope * ly[i, 0] + yInter);

                        for (int j = 1; j < number; j++)
                        {
                            x = (float)lx[i, j];
                            y = H(x);

                            int tx1 = (int)(xSlope * x + xInter);
                            int ty1 = (int)(ySlope * y + yInter);

                            g.DrawLine(pen4, tx0, ty0, tx1, ty1);

                            tx0 = tx1;
                            ty0 = ty1;
                        }
                    }
                }

                else if (method == 3 && inter != null)
                {
                    for (int i = 0; i < Split; i++)
                    {
                        tx0 = (int)(xSlope * nx[i, 0] + xInter);
                        ty0 = (int)(ySlope * ny[i, 0] + yInter);

                        for (int j = 1; j < number; j++)
                        {
                            x = (float)nx[i, j];
                            y = I(x);

                            int tx1 = (int)(xSlope * x + xInter);
                            int ty1 = (int)(ySlope * y + yInter);

                            g.DrawLine(pen4, tx0, ty0, tx1, ty1);

                            tx0 = tx1;
                            ty0 = ty1;
                        }
                    }
                }
                
                else if (method == 4 && complex != null)
                {
                    int j = 0;

                    ty = complex.InverseDFT(cy);
                    x = (float)tx[0];
                    y = (float)ty[0];
                    tx0 = (int)(xSlope * x + xInter);
                    ty0 = (int)(ySlope * y + yInter);

                    while (j < number)
                    {
                        j++;
                        x = (float)tx[j];
                        y = (float)ty[j];

                        int tx1 = (int)(xSlope * x + xInter);
                        int ty1 = (int)(ySlope * y + yInter);

                        g.DrawLine(pen6, tx0, ty0, tx1, ty1);

                        tx0 = tx1;
                        ty0 = ty1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LayOutTheForm()
        {
            // layout the panel

            int w = ClientSize.Width;
            int h = ClientSize.Height;

            panel1.Width = w;
            panel1.Height = 11 * h / 12;
            panel1.Location = new Point(0, 0);

            int y = 11 * h / 12 + 8;
            int t = label1.Width + label2.Width +
                comboBox1.Width + comboBox2.Width +
                button1.Width;
            int d = (w - t) / 12;

            // layout other controls

            label1.Location = new Point(d, y);
            comboBox1.Location = new Point(2 * d + label1.Width, y);
            label2.Location = new Point(3 * d + label1.Width +
                comboBox1.Width, y);
            comboBox2.Location = new Point(4 * d + label1.Width +
                label2.Width + comboBox1.Width, y);
            button1.Location = new Point(5 * d + label1.Width +
                label2.Width + comboBox1.Width + comboBox2.Width, y);
            panel1.Invalidate();
        }

        protected void PanelPaintHandler(object sender, PaintEventArgs pa)
        {
            float x0 = -1.6f;
            float x1 = +1.6f;

            DrawGraph(x0, -0.9f, x1, 0.9f, pa.Graphics);
        }

        protected override void OnResize(EventArgs ea)
        {
            LayOutTheForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            method = comboBox1.SelectedIndex;
            number = int.Parse((string)comboBox2.SelectedItem);

            if (method == 2 || method == 3 && number > 5)
                number = 5;

            if (method == 1)
            {
                if (number > 11)
                    number = 11;

                series = new Series(1.6, number, f);
            }

            else if (method == 0)
            {
                double[] fy = new double[number + 1];
                double h = 3.2 / number;
                double x = -1.6;

                for (int i = 0; i <= number; i++)
                {
                    fy[i] = f(x);
                    x += h;
                }

                inter = new Interpolate();
                coeff = inter.ChebyshevCalculateCoefficients(number, fy);
            }

            else if (method == 2)
            {
                lx = new double[Split, number + 1];
                ly = new double[Split, number + 1];
                double[] y = new double[number + 1];
                double[] z = new double[number + 1];
                double h = 3.2 / (number * Split);
                double x = -1.6;
                inter = new Interpolate();

                for (int i = 0; i < Split; i++)
                {
                    for (int j = 0; j <= number; j++)
                    {
                        z[j] = lx[i, j] = x;
                        y[j] = ly[i, j] = f(x);
                        x += h;
                    }
                }

                inter = new Interpolate();
            }

            else if (method == 3)
            {
                nx = new double[Split, number + 1];
                ny = new double[Split, number + 1];
                double[] y = new double[number + 1];
                double[] z = new double[number + 1];
                double h = 3.2 / (number * Split);
                double x = -1.6;
                inter = new Interpolate();

                for (int i = 0; i < Split; i++)
                {
                    for (int j = 0; j <= number; j++)
                    {
                        z[j] = nx[i, j] = x;
                        y[j] = f(x);
                        x += h;
                    }

                    inter.Newton(number, z, y);

                    for (int j = 0; j <= number; j++)
                        ny[i, j] = y[j];
                }
            }

            else if (method == 4)
            {
                double h = 3.2 / number;
                double x = -1.6;
                complex = new Complex();
                cy = new Complex[number + 1];
                tx = new double[number + 1];
                ty = new double[number + 1];

                for (int j = 0; j <= number; j++)
                {
                    tx[j] = x;
                    ty[j] = f(x);
                    x += h;
                }

                cy = complex.DFT(ty);
            }

            panel1.Invalidate();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LayOutTheForm();
        }
    }
}
