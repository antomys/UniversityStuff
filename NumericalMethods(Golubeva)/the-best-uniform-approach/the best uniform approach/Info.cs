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
    
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
        }


        
        private void Info_Load(object sender, EventArgs e)
        {
            int a = -1; int b = 2;
            double x1 = 0.0; double x2 = 1.17;
            string DX = "(3*Math.Pow(x-1,2))/4)";
            DX_textBox.Text = DX.ToString();
            dfA_textBox.Text = Dx(a).ToString();
            DfB_textBox.Text = Dx(b).ToString();
            Fa_textBox.Text = X(a).ToString();
            Fb_textBox.Text = X(b).ToString();
            Fx0_textBox.Text = X(x1).ToString();
            Fx1_textBox.Text = X(x2).ToString();
            T11_textBox4.Text = "4*x^3-3*x";
            T02_textBox5.Text = "4*x^3-24*x+45*x-26";
            A_textBox6.Text = "x-2";
            q2x_textBox1.Text = "-0.75x^{3}+5.25x^{2}-10.5x+8.5";
            textBox4.Text = "|x^3-6*x^2+11.25*x-6.75|";




            double min; double max;
            min = Math.Min(X(x1), X(x2));
            double i = Math.Min(X(a), X(b));
            if (min > i) { min = i; i = 0; } else { i = 0; }
            Fmin_textbox.Text = min.ToString();
            max = Math.Max(X(x1), X(x2));
            i = Math.Max(X(a), X(b));
            if (max < i) { max = i; i = 0; } else { i = 0; }
            Fmax_textbox.Text = max.ToString();

            double Q0 = (max + min) / 2;

            Q0_Textbox.Text = Q0.ToString();


        }

        public double Dx(double x)
        {
            return ((3*Math.Pow(x-1,2))/4);
        }
        public double X(double i)
        {
            return ((Math.Pow(i - 1, 3) / 4) + 2);
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
