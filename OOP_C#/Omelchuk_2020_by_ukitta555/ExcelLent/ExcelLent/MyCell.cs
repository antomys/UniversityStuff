using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ExcelLent
{
    [Serializable()]
    public class MyCell : DataGridViewTextBoxCell
    {
        
        private string expression = "";
        
        private HashSet<string> variables = new HashSet<string>();

        public HashSet<string> Variables
        {
            get { return variables; }
        }

        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }

        public override object Clone()
        {
            var objToReturn = (MyCell)base.Clone();
            objToReturn.expression = expression;
            return objToReturn;
        }
    }
}
