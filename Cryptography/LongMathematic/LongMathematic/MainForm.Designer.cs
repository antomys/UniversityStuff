namespace LongMathematic
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbox_firstnum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_add = new System.Windows.Forms.Button();
            this.tbox_secnum = new System.Windows.Forms.TextBox();
            this.btn_substr = new System.Windows.Forms.Button();
            this.btn_mult = new System.Windows.Forms.Button();
            this.btn_div = new System.Windows.Forms.Button();
            this.btn_compare = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbox_result = new System.Windows.Forms.TextBox();
            this.tbox_mod = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_pow = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.btn_systsolve = new System.Windows.Forms.Button();
            this.tbox_reminders = new System.Windows.Forms.TextBox();
            this.tbox_modsyst = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_ropollard = new System.Windows.Forms.Button();
            this.btn_bigsmall = new System.Windows.Forms.Button();
            this.btn_mebius = new System.Windows.Forms.Button();
            this.btn_euler = new System.Windows.Forms.Button();
            this.btn_chipollo = new System.Windows.Forms.Button();
            this.btn_jacobi = new System.Windows.Forms.Button();
            this.btn_legendre = new System.Windows.Forms.Button();
            this.btn_millerrabin = new System.Windows.Forms.Button();
            this.btn_hamal = new System.Windows.Forms.Button();
            this.tbox_time = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbox_hamal1 = new System.Windows.Forms.TextBox();
            this.tbox_hamal2 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbox_firstnum
            // 
            this.tbox_firstnum.Location = new System.Drawing.Point(133, 20);
            this.tbox_firstnum.Name = "tbox_firstnum";
            this.tbox_firstnum.Size = new System.Drawing.Size(260, 20);
            this.tbox_firstnum.TabIndex = 0;
            this.tbox_firstnum.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.tbox_firstnum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbox_firstnum_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(130, 267);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(133, 78);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(129, 43);
            this.btn_add.TabIndex = 2;
            this.btn_add.Text = "Додати";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbox_secnum
            // 
            this.tbox_secnum.Location = new System.Drawing.Point(133, 52);
            this.tbox_secnum.Name = "tbox_secnum";
            this.tbox_secnum.Size = new System.Drawing.Size(260, 20);
            this.tbox_secnum.TabIndex = 3;
            this.tbox_secnum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbox_secnum_KeyPress);
            // 
            // btn_substr
            // 
            this.btn_substr.Location = new System.Drawing.Point(264, 78);
            this.btn_substr.Name = "btn_substr";
            this.btn_substr.Size = new System.Drawing.Size(129, 43);
            this.btn_substr.TabIndex = 4;
            this.btn_substr.Text = "Відняти";
            this.btn_substr.UseVisualStyleBackColor = true;
            this.btn_substr.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_mult
            // 
            this.btn_mult.Location = new System.Drawing.Point(133, 127);
            this.btn_mult.Name = "btn_mult";
            this.btn_mult.Size = new System.Drawing.Size(129, 43);
            this.btn_mult.TabIndex = 5;
            this.btn_mult.Text = "Помножити";
            this.btn_mult.UseVisualStyleBackColor = true;
            this.btn_mult.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_div
            // 
            this.btn_div.Location = new System.Drawing.Point(264, 127);
            this.btn_div.Name = "btn_div";
            this.btn_div.Size = new System.Drawing.Size(129, 43);
            this.btn_div.TabIndex = 6;
            this.btn_div.Text = "Поділити";
            this.btn_div.UseVisualStyleBackColor = true;
            this.btn_div.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_compare
            // 
            this.btn_compare.Location = new System.Drawing.Point(203, 225);
            this.btn_compare.Name = "btn_compare";
            this.btn_compare.Size = new System.Drawing.Size(129, 42);
            this.btn_compare.TabIndex = 7;
            this.btn_compare.Text = "Порівняти";
            this.btn_compare.UseVisualStyleBackColor = true;
            this.btn_compare.Click += new System.EventHandler(this.button5_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Перше число";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Друге число";
            // 
            // tbox_result
            // 
            this.tbox_result.Location = new System.Drawing.Point(6, 21);
            this.tbox_result.Name = "tbox_result";
            this.tbox_result.ReadOnly = true;
            this.tbox_result.Size = new System.Drawing.Size(396, 20);
            this.tbox_result.TabIndex = 11;
            this.tbox_result.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // tbox_mod
            // 
            this.tbox_mod.Location = new System.Drawing.Point(525, 27);
            this.tbox_mod.Name = "tbox_mod";
            this.tbox_mod.Size = new System.Drawing.Size(248, 20);
            this.tbox_mod.TabIndex = 12;
            this.tbox_mod.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            this.tbox_mod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbox_mod_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(445, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "За модулем: ";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // btn_pow
            // 
            this.btn_pow.Location = new System.Drawing.Point(264, 177);
            this.btn_pow.Name = "btn_pow";
            this.btn_pow.Size = new System.Drawing.Size(129, 42);
            this.btn_pow.TabIndex = 14;
            this.btn_pow.Text = "Піднесення до степеня";
            this.btn_pow.UseVisualStyleBackColor = true;
            this.btn_pow.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(133, 176);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(129, 43);
            this.button7.TabIndex = 15;
            this.button7.Text = "Квадратний корінь";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // btn_systsolve
            // 
            this.btn_systsolve.Location = new System.Drawing.Point(227, 83);
            this.btn_systsolve.Name = "btn_systsolve";
            this.btn_systsolve.Size = new System.Drawing.Size(129, 42);
            this.btn_systsolve.TabIndex = 16;
            this.btn_systsolve.Text = "Розв\'язання системи";
            this.btn_systsolve.UseVisualStyleBackColor = true;
            this.btn_systsolve.Click += new System.EventHandler(this.button8_Click);
            // 
            // tbox_reminders
            // 
            this.tbox_reminders.Location = new System.Drawing.Point(106, 21);
            this.tbox_reminders.Name = "tbox_reminders";
            this.tbox_reminders.Size = new System.Drawing.Size(249, 20);
            this.tbox_reminders.TabIndex = 17;
            this.tbox_reminders.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // tbox_modsyst
            // 
            this.tbox_modsyst.Location = new System.Drawing.Point(107, 57);
            this.tbox_modsyst.Name = "tbox_modsyst";
            this.tbox_modsyst.Size = new System.Drawing.Size(248, 20);
            this.tbox_modsyst.TabIndex = 18;
            this.tbox_modsyst.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Залишки:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Система модулів:";
            // 
            // btn_ropollard
            // 
            this.btn_ropollard.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_ropollard.Location = new System.Drawing.Point(17, 65);
            this.btn_ropollard.Name = "btn_ropollard";
            this.btn_ropollard.Size = new System.Drawing.Size(90, 26);
            this.btn_ropollard.TabIndex = 21;
            this.btn_ropollard.Text = "p-Полларда";
            this.btn_ropollard.UseVisualStyleBackColor = false;
            this.btn_ropollard.Click += new System.EventHandler(this.button9_Click);
            // 
            // btn_bigsmall
            // 
            this.btn_bigsmall.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_bigsmall.Location = new System.Drawing.Point(115, 65);
            this.btn_bigsmall.Name = "btn_bigsmall";
            this.btn_bigsmall.Size = new System.Drawing.Size(89, 26);
            this.btn_bigsmall.TabIndex = 22;
            this.btn_bigsmall.Text = "Большой шаг - малый шаг";
            this.btn_bigsmall.UseVisualStyleBackColor = false;
            this.btn_bigsmall.Click += new System.EventHandler(this.button10_Click);
            // 
            // btn_mebius
            // 
            this.btn_mebius.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btn_mebius.Location = new System.Drawing.Point(205, 65);
            this.btn_mebius.Name = "btn_mebius";
            this.btn_mebius.Size = new System.Drawing.Size(90, 26);
            this.btn_mebius.TabIndex = 23;
            this.btn_mebius.Text = "Мьобіус";
            this.btn_mebius.UseVisualStyleBackColor = false;
            this.btn_mebius.Click += new System.EventHandler(this.button11_Click);
            // 
            // btn_euler
            // 
            this.btn_euler.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btn_euler.Location = new System.Drawing.Point(301, 65);
            this.btn_euler.Name = "btn_euler";
            this.btn_euler.Size = new System.Drawing.Size(101, 26);
            this.btn_euler.TabIndex = 24;
            this.btn_euler.Text = "Ейлер";
            this.btn_euler.UseVisualStyleBackColor = false;
            this.btn_euler.Click += new System.EventHandler(this.button12_Click);
            // 
            // btn_chipollo
            // 
            this.btn_chipollo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_chipollo.Location = new System.Drawing.Point(205, 97);
            this.btn_chipollo.Name = "btn_chipollo";
            this.btn_chipollo.Size = new System.Drawing.Size(90, 26);
            this.btn_chipollo.TabIndex = 25;
            this.btn_chipollo.Text = "Чіполло";
            this.btn_chipollo.UseVisualStyleBackColor = false;
            this.btn_chipollo.Click += new System.EventHandler(this.button13_Click);
            // 
            // btn_jacobi
            // 
            this.btn_jacobi.Location = new System.Drawing.Point(115, 97);
            this.btn_jacobi.Name = "btn_jacobi";
            this.btn_jacobi.Size = new System.Drawing.Size(89, 26);
            this.btn_jacobi.TabIndex = 26;
            this.btn_jacobi.Text = "Якобі";
            this.btn_jacobi.UseVisualStyleBackColor = true;
            this.btn_jacobi.Click += new System.EventHandler(this.button14_Click);
            // 
            // btn_legendre
            // 
            this.btn_legendre.Location = new System.Drawing.Point(17, 96);
            this.btn_legendre.Name = "btn_legendre";
            this.btn_legendre.Size = new System.Drawing.Size(92, 27);
            this.btn_legendre.TabIndex = 27;
            this.btn_legendre.Text = "Лежандр";
            this.btn_legendre.UseVisualStyleBackColor = true;
            this.btn_legendre.Click += new System.EventHandler(this.button15_Click);
            // 
            // btn_millerrabin
            // 
            this.btn_millerrabin.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btn_millerrabin.Location = new System.Drawing.Point(301, 97);
            this.btn_millerrabin.Name = "btn_millerrabin";
            this.btn_millerrabin.Size = new System.Drawing.Size(101, 26);
            this.btn_millerrabin.TabIndex = 28;
            this.btn_millerrabin.Text = "Міллер-Рабін";
            this.btn_millerrabin.UseVisualStyleBackColor = false;
            this.btn_millerrabin.Click += new System.EventHandler(this.button16_Click);
            // 
            // btn_hamal
            // 
            this.btn_hamal.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btn_hamal.Location = new System.Drawing.Point(127, 84);
            this.btn_hamal.Name = "btn_hamal";
            this.btn_hamal.Size = new System.Drawing.Size(101, 26);
            this.btn_hamal.TabIndex = 29;
            this.btn_hamal.Text = "Ель-Гамаль";
            this.btn_hamal.UseVisualStyleBackColor = false;
            this.btn_hamal.Click += new System.EventHandler(this.button17_Click);
            // 
            // tbox_time
            // 
            this.tbox_time.Location = new System.Drawing.Point(133, 237);
            this.tbox_time.Name = "tbox_time";
            this.tbox_time.ReadOnly = true;
            this.tbox_time.Size = new System.Drawing.Size(53, 20);
            this.tbox_time.TabIndex = 31;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbox_reminders);
            this.groupBox1.Controls.Add(this.tbox_modsyst);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btn_systsolve);
            this.groupBox1.Location = new System.Drawing.Point(417, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 139);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chinese Reaminder Theorem";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbox_result);
            this.groupBox2.Controls.Add(this.btn_euler);
            this.groupBox2.Controls.Add(this.btn_ropollard);
            this.groupBox2.Controls.Add(this.btn_mebius);
            this.groupBox2.Controls.Add(this.btn_chipollo);
            this.groupBox2.Controls.Add(this.btn_bigsmall);
            this.groupBox2.Controls.Add(this.btn_millerrabin);
            this.groupBox2.Controls.Add(this.btn_jacobi);
            this.groupBox2.Controls.Add(this.btn_legendre);
            this.groupBox2.Location = new System.Drawing.Point(35, 283);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(409, 135);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Результат";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbox_hamal2);
            this.groupBox3.Controls.Add(this.tbox_hamal1);
            this.groupBox3.Controls.Add(this.btn_hamal);
            this.groupBox3.Location = new System.Drawing.Point(448, 283);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(352, 135);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ель Гамаль";
            // 
            // tbox_hamal1
            // 
            this.tbox_hamal1.Location = new System.Drawing.Point(0, 22);
            this.tbox_hamal1.Name = "tbox_hamal1";
            this.tbox_hamal1.ReadOnly = true;
            this.tbox_hamal1.Size = new System.Drawing.Size(340, 20);
            this.tbox_hamal1.TabIndex = 29;
            // 
            // tbox_hamal2
            // 
            this.tbox_hamal2.Location = new System.Drawing.Point(0, 58);
            this.tbox_hamal2.Name = "tbox_hamal2";
            this.tbox_hamal2.ReadOnly = true;
            this.tbox_hamal2.Size = new System.Drawing.Size(340, 20);
            this.tbox_hamal2.TabIndex = 30;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbox_time);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.btn_pow);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbox_mod);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_compare);
            this.Controls.Add(this.btn_div);
            this.Controls.Add(this.btn_mult);
            this.Controls.Add(this.btn_substr);
            this.Controls.Add(this.tbox_secnum);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbox_firstnum);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "LongMathematic";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbox_firstnum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.TextBox tbox_secnum;
        private System.Windows.Forms.Button btn_substr;
        private System.Windows.Forms.Button btn_mult;
        private System.Windows.Forms.Button btn_div;
        private System.Windows.Forms.Button btn_compare;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbox_result;
        private System.Windows.Forms.TextBox tbox_mod;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_pow;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btn_systsolve;
        private System.Windows.Forms.TextBox tbox_reminders;
        private System.Windows.Forms.TextBox tbox_modsyst;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_ropollard;
        private System.Windows.Forms.Button btn_bigsmall;
        private System.Windows.Forms.Button btn_mebius;
        private System.Windows.Forms.Button btn_euler;
        private System.Windows.Forms.Button btn_chipollo;
        private System.Windows.Forms.Button btn_jacobi;
        private System.Windows.Forms.Button btn_legendre;
        private System.Windows.Forms.Button btn_millerrabin;
        private System.Windows.Forms.Button btn_hamal;
        private System.Windows.Forms.TextBox tbox_time;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbox_hamal2;
        private System.Windows.Forms.TextBox tbox_hamal1;
    }
}

