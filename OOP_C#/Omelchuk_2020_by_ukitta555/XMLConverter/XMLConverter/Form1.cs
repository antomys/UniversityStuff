using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;

namespace XMLConverter
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            fillDropdowns();
        }

        private void fillDropdowns() 
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\Vlad_2\repos\C#\XMLConverter\XMLConverter\ProgLanguages.xml");
            XmlElement root = doc.DocumentElement;
            if (root != null) 
            {
                XmlNodeList childNodes = root.SelectNodes("ProgLanguage");
                for (int i = 0; i < childNodes.Count; i++) 
                {
                    XmlNode childNode = childNodes.Item(i);
                    checkProperties(childNode);
                }
            }
        }

        private void checkProperties(XmlNode node) 
        {
            checkPropertyKey(LanguageNameComboBox, node, "@LanguageName");
            checkPropertyKey(AuthorsComboBox, node, "@Authors");
            checkPropertyKey(LanguageTypeComboBox, node, "@TypeOfLanguage");
            checkPropertyKey(ReleaseYearComboBox, node, "@ReleaseYear");
            checkPropertyKey(AbstractionLevelComboBox, node, "@AbstractionLevel");
            checkPropertyKey(CommonUsageComboBox, node, "@CommonlyUsedFor");
        }

        private void checkPropertyKey(ComboBox comboBox, XmlNode node, string property) 
        {
            if (!comboBox.Items.Contains(node.SelectSingleNode(property).Value))
            {
                comboBox.Items.Add(node.SelectSingleNode(property).Value);
                //Console.WriteLine(node.SelectSingleNode(property).Value);
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search() 
        {
            ResultRichTextBox.Text = "";
            ProgLanguage searchParams = new ProgLanguage();
            try 
            {

                if (LanguageNameCheckBox.Checked && LanguageNameComboBox.SelectedItem != null)
                    searchParams.LanguageName = LanguageNameComboBox.SelectedItem.ToString();

                if (AuthorsCheckBox.Checked && AuthorsComboBox.SelectedItem != null)
                    searchParams.Authors = AuthorsComboBox.SelectedItem.ToString();

                if (ReleaseYearCheckBox.Checked && ReleaseYearComboBox.SelectedItem != null)
                    searchParams.ReleaseYear = ReleaseYearComboBox.SelectedItem.ToString();

                if (AbstractionLevelCheckBox.Checked && AbstractionLevelComboBox.SelectedItem != null)
                    searchParams.AbstractionLevel = AbstractionLevelComboBox.SelectedItem.ToString();

                if (CommonUsageCheckBox.Checked && CommonUsageComboBox.SelectedItem != null)
                    searchParams.CommonlyUsedFor = CommonUsageComboBox.SelectedItem.ToString();

                if (LanguageTypeCheckBox.Checked && LanguageTypeComboBox.SelectedItem != null)
                    searchParams.TypeOfLanguage = LanguageTypeComboBox.SelectedItem.ToString();
            }
            catch (Exception e) 
            {
                Console.WriteLine("Whoops! You gotta choose something before you go.");
            }

            IStrategyParser parserStrategy = new DOMXMLStrategy();

            if (XMLDOMRadioButton.Checked)
                parserStrategy = new DOMXMLStrategy();
            if (LINQRadioButton.Checked)
                parserStrategy = new XMLtoLINQStrategy();
            if (SAXRadioButton.Checked)
                parserStrategy = new SAXStrategy();

            List<ProgLanguage> result = parserStrategy.Parse(searchParams);
            Console.WriteLine(result.Count);
            foreach (ProgLanguage language in result) 
            {
                ResultRichTextBox.Text += "Language name: " + language.LanguageName + "\n";
                ResultRichTextBox.Text += "Language type: " + language.TypeOfLanguage + "\n";
                ResultRichTextBox.Text += "Authors :" + language.Authors + "\n";
                ResultRichTextBox.Text += "Abstraction level: " + language.AbstractionLevel + "\n";
                ResultRichTextBox.Text += "Release year: " + language.ReleaseYear + "\n";
                ResultRichTextBox.Text += "Common usage: " + language.CommonlyUsedFor + "\n";
                ResultRichTextBox.Text += "\n" + "__________________________________" + "\n" + "\n";
             }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ResultRichTextBox.Clear();
        }

        private void HTMLButton_Click(object sender, EventArgs e)
        {
            Transform();
        }
        private void Transform() 
        {
            XslCompiledTransform xct = new XslCompiledTransform();
            xct.Load(@"C:\Users\Vlad_2\repos\C#\XMLConverter\XMLConverter\XSLtoHTML.xsl");
            string fileXML = @"C:\Users\Vlad_2\repos\C#\XMLConverter\XMLConverter\ProgLanguages.xml";
            string fileHTML = @"C:\Users\Vlad_2\repos\C#\XMLConverter\XMLConverter\ProgLanguages.html";
            xct.Transform(fileXML, fileHTML);
            MessageBox.Show("Transformed your file :)");
        }
    }
}
