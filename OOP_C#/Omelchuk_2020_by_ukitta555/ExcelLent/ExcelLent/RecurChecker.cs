using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ExcelLent
{
    class RecurChecker
    {
        public static bool Check(MyCell currentCell, string currentVar, HashSet<string> vars)
        {
            try
            {
                /*
                MessageBox.Show("inside recur checker");
                MessageBox.Show("current var:" + currentVar);
                string variablesInVars = "";
                foreach (string var in vars)
                {
                    variablesInVars += var + " ";
                }
                MessageBox.Show(variablesInVars);
                */

                HashSet<string> tmp = new HashSet<string>(vars);
                tmp.IntersectWith(currentCell.Variables);
                if (tmp.Count != 0) return true;
                vars.Add(currentVar);
                HashSet<string> cellsToGo = currentCell.Variables;
                bool f = false;
                foreach (string cellName in cellsToGo)
                {
                    string letters = "";
                    int i = 0;
                    while (Char.IsLetter(cellName[i]))
                    {
                        letters += cellName[i];
                        i++;
                    }
                    int numbers = Convert.ToInt32(cellName.Substring(i));
                    if (!f)
                    {
                        f = f || Check((MyCell)Program.form1.getDataGridView().Rows[numbers].Cells[letters], cellName, vars);
                    }
                }
                return f;
            }
            catch (StackOverflowException e)
            {
                MessageBox.Show("Stack overflow! Recursion use I see indeed.");
                return true;
            }
        }
    }
}
