using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace the_best_uniform_approach
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        public void DrawMainFunc(int a, int b, double af, double rash)
        {
            b = b + 1;
            int ya, yb;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(-10, 10);
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            if (a < 0 && b > 0)
            {

                ya = a - a;
                yb = b + b;

            }
            else if (a > 0 && b < 0)
            {
                MessageBox.Show("Wrong Integer", "Error", MessageBoxButtons.OK);
                ya = -1; yb = 3;

            }
            else if (a > b)
            {
                MessageBox.Show("Wrong Integer", "Error", MessageBoxButtons.OK);
                yb = a;
                ya = b;
            }
            else
            {
                ya = a + a;
                yb = b + b;
            }
            chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-5, 19);
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;



            for (int i = -10; i < 10; i++)
            {


                chart1.Series[0].Points.AddXY(i, ((Math.Pow(i - 1, 3) / 4) + 2)); //(Math.Pow(i,5)-4*(Math.Pow(i,2))+2)
                chart1.Series[2].Points.AddXY(i, af);
                chart1.Series[4].Points.AddXY(i, i+1);
                chart1.Series[3].Points.AddXY(i, ((i-rash)+X(rash)));
                chart1.Series[1].Points.AddXY(i, (i+(1+2.15)/2));
                chart1.Series[5].Points.AddXY(i, ((-0.75*Math.Pow(i,3))+(5.25*Math.Pow(i,2))-10.5*i+8.5)); //-0.75x^{3}+5.25x^{2}-10.5x+8.5
                chart1.Series[6].Points.AddXY(i, (Math.Pow(i, 3) - (6 * (Math.Pow(i, 2))) + 11.25 * i - 6.75));
            }


        }

        private void Button1_Click(object sender, EventArgs e)
        {

            int fir = Convert.ToInt32(textBoxA.Text);

            int sec = Convert.ToInt32(textBoxB.Text);
            double af = Sup_Inf(fir, sec);
            double rash = raschet(fir, sec);
            DrawMainFunc(fir, sec, af, rash);
            MessageBox.Show(Tchebyczow(fir, af).ToString(), "ALTERNANCE To a", MessageBoxButtons.OK);
            MessageBox.Show(Tchebyczow(sec, af).ToString(), "ALTERNANCE To b", MessageBoxButtons.OK);
            
            MessageBox.Show(rash.ToString());

        }

        private void ОбрахункиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Info info = new Info();
            info.Show();
        }

        public double Sup_Inf(int a, int b)
        {
            double x1 = 1;
            double x2 = 1;
            MessageBox.Show("Derivative is (3*Math.Pow(x-1,2))/4)", "Derivative", MessageBoxButtons.OK);
            MessageBox.Show("x1=0;" + X(x1), "f(x1)", MessageBoxButtons.OK);
            MessageBox.Show("x2=1.17;" + X(x2), "f(x2)", MessageBoxButtons.OK);
            MessageBox.Show("a+" + a + "; f(a)=" + X(a), "f(a)", MessageBoxButtons.OK);
            MessageBox.Show("b+" + b + "; f(b)=" + X(b), "f(b)", MessageBoxButtons.OK);
            double min; double max;
            min = Math.Min(X(x1), X(x2));
            double i = Math.Min(X(a), X(b));
            if (min > i) { min = i; i = 0; } else { i = 0; }
            MessageBox.Show("fmin =" + min, "Fmin", MessageBoxButtons.OK);
            max = Math.Max(X(x1), X(x2));
            i = Math.Max(X(a), X(b));
            if (max < i) { max = i; i = 0; } else { i = 0; }
            MessageBox.Show("fmax =" + max, "Fmax", MessageBoxButtons.OK);
            double Q0 = (max + min) / 2;

            MessageBox.Show("Calculate Q0(x)" + Q0, "Q(x)", MessageBoxButtons.OK);

            for (i = a; i < b; i++)
            {

            }

            return (Q0);


        }
        
        public double Dx(double x)
        {
            return (((3 * Math.Pow(x - 1, 2)) / 4));
        }
        public double X(double i)
        {
            return ((Math.Pow(i - 1, 3) / 4) + 2);
        }

        public double Tchebyczow(int i, double o)
        {
            return Math.Abs(((Math.Pow(i - 1, 3) / 4) + 2) - o);
        }

        public double raschet(int fi, int se)
        {
            double a = 3; double b = -6; double c = -1;
            double D; double x1, x2;
            D = Math.Pow(b, 2) - 4 * a * c;
            if (D > 0 || D == 0)
            {
                x1 = (-b + Math.Sqrt(D)) / (2 * a);
                x2 = (-b - Math.Sqrt(D)) / (2 * a);
                MessageBox.Show("X1=" + x1 + ";X2=" + x2);

                if (x1 < fi || x1 > se)
                {
                    return x2;
                } else if(x2 < fi || x2 > se )
                    {
                    return x1;
                    }
                
                }
            return 0;


        }
        public double Tcheb(double i)
        {
            return (4 * (Math.Pow(i, 3)) - 3 * i);    
        }
        public double convert(double i, double a, double b)
        {
            return ((2 * i - (a + b) / (b - a)));
        }

        }
    }

