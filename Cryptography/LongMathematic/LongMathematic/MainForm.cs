using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LongMathematic
{
    public partial class MainForm : Form
    {
        private int mod = 1;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BigInteger bigInteger1 = new BigInteger(tbox_firstnum.Text);
            BigInteger bigInteger2 = new BigInteger(tbox_secnum.Text);
            BigInteger result = bigInteger1.Add(bigInteger2);
            if (!tbox_mod.Text.Equals(""))
            {
                int r;
                result.Divide2(Int32.Parse(tbox_mod.Text), out r);
                tbox_result.Text = (r.ToString());
            }
            else
            {
                tbox_result.Text = (result.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BigInteger bigInteger1 = new BigInteger(tbox_firstnum.Text);
            BigInteger bigInteger2 = new BigInteger(tbox_secnum.Text);
         
            BigInteger result = bigInteger1.Subtract(bigInteger2);
            if (!tbox_mod.Text.Equals(""))
            {
                int r;
                result.Divide2(Int32.Parse(tbox_mod.Text), out r);
                tbox_result.Text = (r.ToString());
            }
            else
            {
                tbox_result.Text = (result.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BigInteger bigInteger1 = new BigInteger(tbox_firstnum.Text);
            BigInteger bigInteger2 = new BigInteger(tbox_secnum.Text);
            //int bigInteger2 = Int32.Parse(textBox2.Text);
            BigInteger result = bigInteger1.Multiply(bigInteger2);
            if (!tbox_mod.Text.Equals(""))
            {
                int r;
                result.Divide2(Int32.Parse(tbox_mod.Text), out r);
                tbox_result.Text = (r.ToString());
            }
            else
            {
                tbox_result.Text = (result.ToString());
            }
            tbox_result.Text = (result.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BigInteger bigInteger1 = new BigInteger(tbox_firstnum.Text);
            BigInteger bigInteger2 = new BigInteger(tbox_secnum.Text);
            int result = bigInteger1.CompareTo(bigInteger2);
            tbox_result.Text = (result.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (tbox_mod.Text.Equals("")) mod = 1;
            else mod = Int32.Parse(tbox_mod.Text);
            BigInteger bigInteger1 = new BigInteger(tbox_firstnum.Text);
            if (tbox_secnum.Text.Length < 9)
            {
                int bigInteger2 = Int32.Parse(tbox_secnum.Text);
                int r;
                BigInteger result = bigInteger1.Divide2(bigInteger2, out r);
                if (!tbox_mod.Text.Equals(""))
                {
                    int r1;
                    result.Divide2(Int32.Parse(tbox_mod.Text), out r1);
                    tbox_result.Text = (r1.ToString());
                    r = r % Int32.Parse(tbox_mod.Text);
                    tbox_result.Text = ("Ціла частина = " + r1.ToString() + ", " + "залишок = " + r.ToString());
                }
                else
                {
                    tbox_result.Text = ("Ціла частина = " + result.ToString() + ", " + "залишок = " + r.ToString());
                }
            }
            else
            {
                BigInteger bigInteger2 = new BigInteger(tbox_secnum.Text);
                BigInteger r;
                BigInteger q;
                int result = BigInteger.Divide(out q, out r, bigInteger1, bigInteger2);
                if (!tbox_mod.Text.Equals(""))
                {
                    int r1;
                    int r2;
                    q.Divide2(Int32.Parse(tbox_mod.Text), out r1);
                    r.Divide2(Int32.Parse(tbox_mod.Text), out r2);
                    tbox_result.Text = ("Ціла частина = " + r1.ToString() + ", " + "залишок = " + r2.ToString());
                }
                else
                {
                    tbox_result.Text = ("Ціла частина = " + q.ToString() + ", " + "залишок = " + r.ToString());
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (tbox_mod.Text.Equals("")) mod = 1;
            else mod = Int32.Parse(tbox_mod.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            BigInteger bigInteger1 = new BigInteger(tbox_firstnum.Text);
            int power = Int32.Parse(tbox_secnum.Text);
            BigInteger result = bigInteger1.Pow(power, tbox_mod.Text);
            tbox_result.Text = (result.ToString());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var watch = new System.Diagnostics.Stopwatch();
            BigInteger bigInteger1 = new BigInteger(tbox_firstnum.Text);
            watch.Start();
            BigInteger result = BigInteger.Sqrt(bigInteger1);
            if (!tbox_mod.Text.Equals(""))
            {
                int r;
                result.Divide2(Int32.Parse(tbox_mod.Text), out r);
                tbox_result.Text = (r.ToString());
            }
            else
            {
                tbox_result.Text = (result.ToString());
            }
            watch.Stop();
            tbox_time.Text = $"{watch.ElapsedMilliseconds} ms";
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            List<int> r = new List<int>();
            List<int> mods = new List<int>();
            string[] rs = tbox_reminders.Text.Split(',');
            string[] modss = tbox_modsyst.Text.Split(',');
            for (int i = 0; i < rs.Length; i++)
                r.Add(Int32.Parse(rs[i]));
            for (int i = 0; i < modss.Length; i++)
                mods.Add(Int32.Parse(modss[i]));
            //int result = BigInteger.SolveSystem(r, mods);
            BigInteger result = BigInteger.SolveSystem(r, mods);
            tbox_result.Text = (result.ToString());
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            BigInteger bigInteger1 = new BigInteger(tbox_firstnum.Text);
            //BigInteger bigInteger2 = new BigInteger(textBox2.Text);
            List<BigInteger> result = bigInteger1.Factorization2(bigInteger1);
            string ans = result[0].ToString();
            for (int i = 1;  i < result.Count(); i++)
            {
                ans += ", " + result[i].ToString();
            }
            tbox_result.Text = ans;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            BigInteger bigInteger1 = new BigInteger(tbox_firstnum.Text);
            BigInteger bigInteger2 = new BigInteger(tbox_secnum.Text);
            BigInteger bigInteger3 = new BigInteger(tbox_mod.Text);
            tbox_result.Text = bigInteger1.Solve(bigInteger1, bigInteger2, bigInteger3).ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            BigInteger bigInteger1 = new BigInteger(tbox_firstnum.Text);
            tbox_result.Text = bigInteger1.phi(bigInteger1).ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            BigInteger bigInteger1 = new BigInteger(tbox_firstnum.Text);
            tbox_result.Text = bigInteger1.Mobius(bigInteger1).ToString();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            BigInteger bigInteger1 = new BigInteger(tbox_firstnum.Text);
            BigInteger bigInteger2 = new BigInteger(tbox_secnum.Text);
            tbox_result.Text = bigInteger1.Legan(bigInteger1, bigInteger2).ToString();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            BigInteger bigInteger1 = new BigInteger(tbox_firstnum.Text);
            BigInteger bigInteger2 = new BigInteger(tbox_secnum.Text);
            tbox_result.Text = bigInteger1.Jacobi(bigInteger1, bigInteger2).ToString();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            BigInteger bigInteger1 = new BigInteger(tbox_firstnum.Text);
            tbox_result.Text = bigInteger1.IsPrimeMillerRabin(bigInteger1, 20).ToString();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            BigInteger bigInteger1 = new BigInteger(tbox_firstnum.Text);
            BigInteger bigInteger2 = new BigInteger(tbox_secnum.Text);
            List<BigInteger> result = bigInteger1.Cipollo(bigInteger1, bigInteger2);
            string ans = result[0].ToString();
            for (int i = 1; i < result.Count(); i++)
            {
                ans += ", " + result[i].ToString();
            }
            tbox_result.Text = ans;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            BigInteger bigInteger1 = new BigInteger(tbox_firstnum.Text);
             List<BigInteger> result = bigInteger1.ElGamal(bigInteger1); 
             tbox_hamal1.Text = "a = " + result[4].ToString() + ",     b = " + result[5].ToString();
            tbox_hamal2.Text = "p = " + result[0].ToString() + ",     g = " + result[1].ToString() + ",     x = " + result[2].ToString() + ",     y = " + result[3].ToString();
            //textBox3.Text = bigInteger1.PollardRho(new BigInteger("106372007578")).ToString();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_firstnum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void tbox_secnum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void tbox_mod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
