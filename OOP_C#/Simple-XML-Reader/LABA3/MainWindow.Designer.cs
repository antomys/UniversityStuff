namespace LABA3
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wind = new System.Windows.Forms.RichTextBox();
            this.countryCheck = new System.Windows.Forms.CheckBox();
            this.companyCheck = new System.Windows.Forms.CheckBox();
            this.countryComboBox = new System.Windows.Forms.ComboBox();
            this.companyComboBox = new System.Windows.Forms.ComboBox();
            this.yearCheck = new System.Windows.Forms.CheckBox();
            this.rateComboBox = new System.Windows.Forms.ComboBox();
            this.yearComboBox = new System.Windows.Forms.ComboBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.rateCheck = new System.Windows.Forms.CheckBox();
            this.saxButton = new System.Windows.Forms.RadioButton();
            this.domButton = new System.Windows.Forms.RadioButton();
            this.linqButton = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вихідToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.допомогаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.natoCheck = new System.Windows.Forms.CheckBox();
            this.natoComboBox = new System.Windows.Forms.ComboBox();
            this.gaCheck = new System.Windows.Forms.CheckBox();
            this.gaComboBox = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wind
            // 
            this.wind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wind.BackColor = System.Drawing.SystemColors.Info;
            this.wind.Location = new System.Drawing.Point(401, 43);
            this.wind.Name = "wind";
            this.wind.Size = new System.Drawing.Size(657, 601);
            this.wind.TabIndex = 0;
            this.wind.Text = "";
            // 
            // countryCheck
            // 
            this.countryCheck.AutoSize = true;
            this.countryCheck.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.countryCheck.Location = new System.Drawing.Point(12, 136);
            this.countryCheck.Name = "countryCheck";
            this.countryCheck.Size = new System.Drawing.Size(78, 23);
            this.countryCheck.TabIndex = 14;
            this.countryCheck.Text = "Country";
            this.countryCheck.UseVisualStyleBackColor = true;
            this.countryCheck.CheckedChanged += new System.EventHandler(this.countryCheck_CheckedChanged);
            // 
            // companyCheck
            // 
            this.companyCheck.AutoSize = true;
            this.companyCheck.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.companyCheck.Location = new System.Drawing.Point(12, 192);
            this.companyCheck.Name = "companyCheck";
            this.companyCheck.Size = new System.Drawing.Size(115, 23);
            this.companyCheck.TabIndex = 17;
            this.companyCheck.Text = "Manufacturer";
            this.companyCheck.UseVisualStyleBackColor = true;
            this.companyCheck.CheckedChanged += new System.EventHandler(this.companyCheck_CheckedChanged);
            // 
            // countryComboBox
            // 
            this.countryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.countryComboBox.Enabled = false;
            this.countryComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.countryComboBox.FormattingEnabled = true;
            this.countryComboBox.Location = new System.Drawing.Point(157, 136);
            this.countryComboBox.Name = "countryComboBox";
            this.countryComboBox.Size = new System.Drawing.Size(230, 21);
            this.countryComboBox.TabIndex = 18;
            this.countryComboBox.SelectedIndexChanged += new System.EventHandler(this.countryComboBox_SelectedIndexChanged_1);
            // 
            // companyComboBox
            // 
            this.companyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.companyComboBox.Enabled = false;
            this.companyComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.companyComboBox.FormattingEnabled = true;
            this.companyComboBox.Location = new System.Drawing.Point(157, 194);
            this.companyComboBox.Name = "companyComboBox";
            this.companyComboBox.Size = new System.Drawing.Size(230, 21);
            this.companyComboBox.TabIndex = 19;
            this.companyComboBox.SelectedIndexChanged += new System.EventHandler(this.companyComboBox_SelectedIndexChanged_1);
            // 
            // yearCheck
            // 
            this.yearCheck.AutoSize = true;
            this.yearCheck.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.yearCheck.Location = new System.Drawing.Point(12, 249);
            this.yearCheck.Name = "yearCheck";
            this.yearCheck.Size = new System.Drawing.Size(57, 23);
            this.yearCheck.TabIndex = 16;
            this.yearCheck.Text = "Year";
            this.yearCheck.UseVisualStyleBackColor = true;
            this.yearCheck.CheckedChanged += new System.EventHandler(this.yearCheck_CheckedChanged);
            // 
            // rateComboBox
            // 
            this.rateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rateComboBox.Enabled = false;
            this.rateComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rateComboBox.FormattingEnabled = true;
            this.rateComboBox.Location = new System.Drawing.Point(157, 302);
            this.rateComboBox.Name = "rateComboBox";
            this.rateComboBox.Size = new System.Drawing.Size(233, 21);
            this.rateComboBox.TabIndex = 20;
            this.rateComboBox.SelectedIndexChanged += new System.EventHandler(this.rateComboBox_SelectedIndexChanged_1);
            // 
            // yearComboBox
            // 
            this.yearComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.yearComboBox.Enabled = false;
            this.yearComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.yearComboBox.FormattingEnabled = true;
            this.yearComboBox.Location = new System.Drawing.Point(157, 255);
            this.yearComboBox.Name = "yearComboBox";
            this.yearComboBox.Size = new System.Drawing.Size(233, 21);
            this.yearComboBox.TabIndex = 21;
            this.yearComboBox.SelectedIndexChanged += new System.EventHandler(this.yearComboBox_SelectedIndexChanged_1);
            // 
            // searchButton
            // 
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Font = new System.Drawing.Font("Roboto Condensed", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.searchButton.Location = new System.Drawing.Point(12, 589);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(348, 55);
            this.searchButton.TabIndex = 25;
            this.searchButton.Text = "Пошук";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // rateCheck
            // 
            this.rateCheck.AutoSize = true;
            this.rateCheck.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rateCheck.Location = new System.Drawing.Point(12, 302);
            this.rateCheck.Name = "rateCheck";
            this.rateCheck.Size = new System.Drawing.Size(70, 23);
            this.rateCheck.TabIndex = 15;
            this.rateCheck.Text = "Rating";
            this.rateCheck.UseVisualStyleBackColor = true;
            this.rateCheck.CheckedChanged += new System.EventHandler(this.rateCheck_CheckedChanged);
            // 
            // saxButton
            // 
            this.saxButton.AutoSize = true;
            this.saxButton.Font = new System.Drawing.Font("Roboto Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saxButton.Location = new System.Drawing.Point(12, 546);
            this.saxButton.Name = "saxButton";
            this.saxButton.Size = new System.Drawing.Size(60, 27);
            this.saxButton.TabIndex = 22;
            this.saxButton.TabStop = true;
            this.saxButton.Text = "SAX";
            this.saxButton.UseVisualStyleBackColor = true;
            // 
            // domButton
            // 
            this.domButton.AutoSize = true;
            this.domButton.Font = new System.Drawing.Font("Roboto Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.domButton.Location = new System.Drawing.Point(296, 546);
            this.domButton.Name = "domButton";
            this.domButton.Size = new System.Drawing.Size(64, 27);
            this.domButton.TabIndex = 24;
            this.domButton.TabStop = true;
            this.domButton.Text = "DOM";
            this.domButton.UseVisualStyleBackColor = true;
            // 
            // linqButton
            // 
            this.linqButton.AutoSize = true;
            this.linqButton.Font = new System.Drawing.Font("Roboto Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linqButton.Location = new System.Drawing.Point(130, 546);
            this.linqButton.Name = "linqButton";
            this.linqButton.Size = new System.Drawing.Size(113, 27);
            this.linqButton.TabIndex = 23;
            this.linqButton.TabStop = true;
            this.linqButton.Text = "Linq to Xml";
            this.linqButton.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.допомогаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1070, 27);
            this.menuStrip1.TabIndex = 28;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.вHTMLToolStripMenuItem,
            this.вихідToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Roboto Condensed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(53, 23);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Відкрити";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // вHTMLToolStripMenuItem
            // 
            this.вHTMLToolStripMenuItem.Name = "вHTMLToolStripMenuItem";
            this.вHTMLToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.вHTMLToolStripMenuItem.Text = "В HTML";
            this.вHTMLToolStripMenuItem.Click += new System.EventHandler(this.вHTMLToolStripMenuItem_Click);
            // 
            // вихідToolStripMenuItem
            // 
            this.вихідToolStripMenuItem.Name = "вихідToolStripMenuItem";
            this.вихідToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.вихідToolStripMenuItem.Text = "Вихід";
            this.вихідToolStripMenuItem.Click += new System.EventHandler(this.вихідToolStripMenuItem_Click);
            // 
            // допомогаToolStripMenuItem
            // 
            this.допомогаToolStripMenuItem.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.допомогаToolStripMenuItem.Name = "допомогаToolStripMenuItem";
            this.допомогаToolStripMenuItem.Size = new System.Drawing.Size(88, 23);
            this.допомогаToolStripMenuItem.Text = "Допомога";
            this.допомогаToolStripMenuItem.Click += new System.EventHandler(this.допомогаToolStripMenuItem_Click);
            // 
            // natoCheck
            // 
            this.natoCheck.AutoSize = true;
            this.natoCheck.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.natoCheck.Location = new System.Drawing.Point(12, 359);
            this.natoCheck.Name = "natoCheck";
            this.natoCheck.Size = new System.Drawing.Size(66, 23);
            this.natoCheck.TabIndex = 29;
            this.natoCheck.Text = "NATO";
            this.natoCheck.UseVisualStyleBackColor = true;
            this.natoCheck.CheckedChanged += new System.EventHandler(this.natoCheck_CheckedChanged);
            // 
            // natoComboBox
            // 
            this.natoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.natoComboBox.Enabled = false;
            this.natoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.natoComboBox.FormattingEnabled = true;
            this.natoComboBox.Location = new System.Drawing.Point(154, 359);
            this.natoComboBox.Name = "natoComboBox";
            this.natoComboBox.Size = new System.Drawing.Size(233, 21);
            this.natoComboBox.TabIndex = 30;
            this.natoComboBox.SelectedIndexChanged += new System.EventHandler(this.natoComboBox_SelectedIndexChanged);
            // 
            // gaCheck
            // 
            this.gaCheck.AutoSize = true;
            this.gaCheck.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gaCheck.Location = new System.Drawing.Point(12, 417);
            this.gaCheck.Name = "gaCheck";
            this.gaCheck.Size = new System.Drawing.Size(47, 23);
            this.gaCheck.TabIndex = 31;
            this.gaCheck.Text = "GA";
            this.gaCheck.UseVisualStyleBackColor = true;
            this.gaCheck.CheckedChanged += new System.EventHandler(this.gaCheck_CheckedChanged);
            // 
            // gaComboBox
            // 
            this.gaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gaComboBox.Enabled = false;
            this.gaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gaComboBox.FormattingEnabled = true;
            this.gaComboBox.Location = new System.Drawing.Point(157, 417);
            this.gaComboBox.Name = "gaComboBox";
            this.gaComboBox.Size = new System.Drawing.Size(233, 21);
            this.gaComboBox.TabIndex = 32;
            this.gaComboBox.SelectedIndexChanged += new System.EventHandler(this.gaComboBox_SelectedIndexChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1070, 656);
            this.Controls.Add(this.gaComboBox);
            this.Controls.Add(this.gaCheck);
            this.Controls.Add(this.natoComboBox);
            this.Controls.Add(this.natoCheck);
            this.Controls.Add(this.countryCheck);
            this.Controls.Add(this.companyCheck);
            this.Controls.Add(this.countryComboBox);
            this.Controls.Add(this.companyComboBox);
            this.Controls.Add(this.yearCheck);
            this.Controls.Add(this.rateComboBox);
            this.Controls.Add(this.yearComboBox);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.rateCheck);
            this.Controls.Add(this.saxButton);
            this.Controls.Add(this.domButton);
            this.Controls.Add(this.linqButton);
            this.Controls.Add(this.wind);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Laba3";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox wind;
        private System.Windows.Forms.CheckBox countryCheck;
        private System.Windows.Forms.CheckBox companyCheck;
        private System.Windows.Forms.ComboBox countryComboBox;
        private System.Windows.Forms.ComboBox companyComboBox;
        private System.Windows.Forms.CheckBox yearCheck;
        private System.Windows.Forms.ComboBox rateComboBox;
        private System.Windows.Forms.ComboBox yearComboBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.CheckBox rateCheck;
        private System.Windows.Forms.RadioButton saxButton;
        private System.Windows.Forms.RadioButton domButton;
        private System.Windows.Forms.RadioButton linqButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вHTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вихідToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem допомогаToolStripMenuItem;
        private System.Windows.Forms.CheckBox natoCheck;
        private System.Windows.Forms.ComboBox natoComboBox;
        private System.Windows.Forms.CheckBox gaCheck;
        private System.Windows.Forms.ComboBox gaComboBox;
    }
}

