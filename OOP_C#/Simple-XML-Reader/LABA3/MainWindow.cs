using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Xsl;
using System.IO;

namespace LABA3
{
    public partial class MainWindow : Form
    {
        private string path = "C:\\Users\\Igor Volokhovych\\Desktop\\XMLReader-master\\LABA3\\XML.xml";


        #region initialization
        private string myCountry = null;
        private string myCompany = null;
        private string myYear = null;
        private string myRate = null;
        private string myNato = null;
        private string myGa = null;
        #endregion initialization
        public MainWindow()
        {
           InitializeComponent();

            buildBox(countryComboBox, companyComboBox, yearComboBox, rateComboBox, natoComboBox, gaComboBox);
           
          
        }
        private void buildBox(ComboBox countryBox, ComboBox companyBox, ComboBox yearBox, ComboBox rateBox, ComboBox natoBox, ComboBox gaBox)
        {
            IParse p = new LinqToXML();
            List <Search> res = p.AnalyzeFile(new Search(), path);
            List<string> country = new List<string>();
            List<string> company = new List<string>();
            List<string> year = new List<string>();
            List<string> rate = new List<string>();
            List<string> nato = new List<string>();
            List<string> ga = new List<string>();
            foreach (Search elem in res)
            {
                if (!country.Contains(elem.country))
                {
                    country.Add(elem.country);
                }
                if (!company.Contains(elem.company))
                {
                    company.Add(elem.company);
                }
                if (!year.Contains(elem.year))
                {
                    year.Add(elem.year);
                }
                if (!rate.Contains(elem.rate))
                {
                    rate.Add(elem.rate);
                }
                if (!nato.Contains(elem.nato))
                {
                    nato.Add(elem.nato);
                }
                if (!ga.Contains(elem.ga))
                {
                    ga.Add(elem.ga);
                }
            }

            
            country = country.OrderBy(x=>x).ToList();
            company = company.OrderBy(x => x).ToList();
            year = year.OrderBy(x => x).ToList();
            rate = rate.OrderBy(x => x).ToList();
            nato = nato.OrderBy(x => x).ToList();
            ga = ga.OrderBy(x => x).ToList();

            countryBox.Items.AddRange(country.ToArray());
            companyBox.Items.AddRange(company.ToArray());
            yearBox.Items.AddRange(year.ToArray());
            rateBox.Items.AddRange(rate.ToArray());
            natoBox.Items.AddRange(nato.ToArray());
            gaBox.Items.AddRange(ga.ToArray());
        }
        private void ParsingNames(object sender)
        {
            CheckBox temp = sender as CheckBox;
            switch (temp.Text)
            {
                case "Country":
                    countryComboBox.Text = "";
                    if (temp.CheckState == CheckState.Checked)
                    {
                        countryComboBox.Enabled = true;
                    }
                    else
                    {
                        countryComboBox.Enabled = false;
                        myCountry = null;
                    }
                    break;
                case "Manufacturer":
                    companyComboBox.Text = "";
                    if (temp.CheckState == CheckState.Checked)
                    {
                        companyComboBox.Enabled = true;
                    }
                    else
                    {
                        companyComboBox.Enabled = false;
                        myCompany = null;
                    }
                    break;
                case "Year":
                    yearComboBox.Text = "";
                    if (temp.CheckState == CheckState.Checked)
                    {
                        yearComboBox.Enabled = true;
                    }
                    else
                    {
                        yearComboBox.Enabled = false;
                        myYear = null;
                    }
                    break;
                case "Rating":
                    rateComboBox.Text = "";
                    if (temp.CheckState == CheckState.Checked)
                    {
                        rateComboBox.Enabled = true;
                    }
                    else
                    {
                        rateComboBox.Enabled = false;
                        myRate = null;
                    }
                    break;
                case "NATO":
                    rateComboBox.Text = "";
                    if (temp.CheckState == CheckState.Checked)
                    {
                        natoComboBox.Enabled = true;
                    }
                    else
                    {
                        natoComboBox.Enabled = false;
                        myNato = null;
                    }
                    break;
                case "GA":
                    rateComboBox.Text = "";
                    if (temp.CheckState == CheckState.Checked)
                    {
                        gaComboBox.Enabled = true;
                    }
                    else
                    {
                        gaComboBox.Enabled = false;
                        myGa = null;
                    }
                    break;

            }
        }
      


        private void Parsing4XML()
        {

            Search myTemplate = new Search();
            myTemplate.company = myCompany;
            myTemplate.country = myCountry;
            myTemplate.year = myYear;
            myTemplate.rate = myRate;
            myTemplate.nato = myNato;
            myTemplate.ga = myGa;

            if (saxButton.Checked)
            {
                List<Search> res;
                IParse parser = new SAX();
                res = parser.AnalyzeFile(myTemplate, path);
                Output(res);
            }
            else if (domButton.Checked)
            {
                IParse parser = new DOM();
                List<Search> res;
                res = parser.AnalyzeFile(myTemplate, path);
                Output(res);

            }
            else if (linqButton.Checked)
            {
                IParse parser = new LinqToXML();
                List<Search> res;
                res = parser.AnalyzeFile(myTemplate, path);
                Output(res);
            }
        }

     

        private void Output(List<Search> res)
        {
            wind.Clear();
            foreach (Search n in res)
            { 

                wind.AppendText("Name: " + n.name + " \n");
                wind.AppendText("Country: " + n.country + " \n");
                wind.AppendText("Company: " + n.company + " \n");
                wind.AppendText("Year: " + n.year + " \n");
                wind.AppendText("Rate: " + n.rate + " \n");
                wind.AppendText("Nato: " + n.nato + " \n");
                wind.AppendText("GA: " + n.ga + " \n");
                wind.AppendText("\n");
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
        #region CHECKCHANGED
        private void countryCheck_CheckedChanged(object sender, EventArgs e)
        {
            ParsingNames(sender);
        }
        private void gaCheck_CheckedChanged(object sender, EventArgs e)
        {
            ParsingNames(sender);
        }
        private void companyCheck_CheckedChanged(object sender, EventArgs e)
        {
            ParsingNames(sender);
        }

        private void yearCheck_CheckedChanged(object sender, EventArgs e)
        {
            ParsingNames(sender);
        }

        private void rateCheck_CheckedChanged(object sender, EventArgs e)
        {
            ParsingNames(sender);
        }
        private void natoCheck_CheckedChanged(object sender, EventArgs e)
        {
            ParsingNames(sender);
        }
        #endregion CHECKCHANGED
        #region INDEXCHANGED
        private void countryComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            myCountry = countryComboBox.Text;
        }
        private void gaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            myGa = gaComboBox.Text;
        }

        private void companyComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            myCompany = companyComboBox.Text;
        }

        private void yearComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            myYear = yearComboBox.Text;
        }

        private void rateComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            myRate = rateComboBox.Text;
        }
        private void natoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion INDEXCHANGED
        private void searchButton_Click(object sender, EventArgs e)
        {
            Parsing4XML();
        }
        #region ToHTML
        private void вHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
           IntoHTML();
        }
        private void IntoHTML()
        {
            XslCompiledTransform xsl = new XslCompiledTransform();
            xsl.Load("C:\\Users\\Deque\\Desktop\\LABA3\\XML_To_HTML\\XML_To_HTML\\XSLTFile1.xslt");
            string input = path;
            string result = @"YourHTML.html";
            xsl.Transform(input, result);
            MessageBox.Show("Успішно!");
        }
        #endregion ToHTML

        
        

        private void допомогаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help form = new Help();
            form.Show();
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        #region openfile
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            open();
        }
        private bool open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml) |*.xml |All files (*.*)|*.*";
            openFileDialog.ShowDialog();
            path = openFileDialog.FileName;
            if (path == "")
            {
                return false;
            }
            buildBox(countryComboBox, companyComboBox, yearComboBox, rateComboBox, natoComboBox, gaComboBox);
            return true;
        }
        #endregion openfile
    }
}







