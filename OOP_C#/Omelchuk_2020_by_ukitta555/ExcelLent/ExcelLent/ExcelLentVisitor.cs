using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

namespace ExcelLent
{
    class ExcelLentVisitor : ExpressionGrammarBaseVisitor<double>
    {
        public override double VisitCompileUnit(ExpressionGrammarParser.CompileUnitContext context)
        {
            return Visit(context.expression());
        }

        //IdentifierExpression
        

        public override double VisitParenthesizedExpression(ExpressionGrammarParser.ParenthesizedExpressionContext context)
        {
            return Visit(context.expression());
        }

        public override double VisitExponentExpression(ExpressionGrammarParser.ExponentExpressionContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            Debug.WriteLine("{0} ^ {1}", left, right);
            return System.Math.Pow(left, right);
        }

        public override double VisitDivExpression(ExpressionGrammarParser.DivExpressionContext context)
        { 
            try
            {
                var left = WalkLeft(context);
                var right = WalkRight(context);

                if (right == 0) throw new Exception("Divide by 0");

                Debug.WriteLine("{0} div {1}", left, right);

                return (left - (left % right)) / right;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return double.PositiveInfinity;
        }

        public override double VisitModExpression(ExpressionGrammarParser.ModExpressionContext context)
        {
            try
            {
                var left = WalkLeft(context);
                var right = WalkRight(context);
                if (right == 0) throw new Exception("Divide by 0");

                Debug.WriteLine("{0} mod {1}", left, right);
                return left % right;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return double.PositiveInfinity;
        }
        public override double VisitNumberExpression(ExpressionGrammarParser.NumberExpressionContext context)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

            var result = Convert.ToDouble(context.GetText());
            Debug.WriteLine(result);

            return result;
        }

        public override double VisitMinusExpression(ExpressionGrammarParser.MinusExpressionContext context)
        { 
            var expression = WalkLeft(context);
            return -1 * expression;
        }

        public override double VisitPlusExpression([NotNull] ExpressionGrammarParser.PlusExpressionContext context)
        {
            var expression = WalkLeft(context);
            return expression;
        }

        public override double VisitMMinExpression([NotNull] ExpressionGrammarParser.MMinExpressionContext context)
        {
            double min = Double.PositiveInfinity;
            var result = context.GetText();
            string[] expressions = result.Split(",");
            for (int i = 0; i < expressions.Length; i++)
            {
                var expression = Visit(context.GetRuleContext<ExpressionGrammarParser.ExpressionContext>(i));
                if (min < expression)
                {
                    min = expression;
                }
            }
            
            return min;
        }

        public override double VisitMMaxExpression([NotNull] ExpressionGrammarParser.MMaxExpressionContext context)
        {
            double max = Double.NegativeInfinity;
            var result = context.GetText();
            string[] expressions = result.Split(",");
            for (int i = 0; i < expressions.Length; i++)
            {
                var expression = Visit(context.GetRuleContext<ExpressionGrammarParser.ExpressionContext>(i));
                if (max < expression)
                {
                    max = expression;
                }
            }

            return max;
        }

        public override double VisitIdentifierExpression(ExpressionGrammarParser.IdentifierExpressionContext context)
        {
            var result = context.GetText();
            string letters = "";
            int i = 0;
            while (Char.IsLetter(result[i]))
            {
                letters += result[i];
                i++;
            }
            int numbers = Convert.ToInt32(result.Substring(i));

            // get DataGridView
            var dgv = Program.form1.getDataGridView();

            // update variable dependencies - send selected cells to the ones that they use
            int currentRow = Program.form1.CurrentRow; // get row of cell for which we calculate the expression
            int currentCol = Program.form1.CurrentColumn; // get col of cell for which we calculate the expression
          //  MessageBox.Show("Current row for which we calculate the expression:" + dgv.Columns[currentCol].Name + dgv.Rows[currentRow].HeaderCell.Value);
            MyCell cell = (MyCell)dgv.Rows[currentRow].Cells[currentCol];// get cell
            // get cell name in Excel terms
            string colName = dgv.Columns[cell.ColumnIndex].HeaderText;
            string rowName = cell.RowIndex.ToString();

           // MessageBox.Show("TRY to add variable dependency:" + (colName + rowName) + " to " + (letters + numbers.ToString()));
            //check to prevent adding redundant cells to hashsets (A-B-C type of connection mustn't create A-C link)
            if (((MyCell)(dgv.Rows[currentRow].Cells[currentCol])).Expression.Contains(letters + numbers))
            {
            //    MessageBox.Show("added variable dependency:" + (colName + rowName) + " to " + (letters + numbers.ToString()));
                // add it as a cell to visit while checking recursion
                ((MyCell)(dgv.Rows[numbers].Cells[letters])).Variables.Add(colName + rowName);
            }
            try
            {
                /*if (((letters + numbers.ToString()) != (colName + rowName)) 
                    && !(RecurChecker.Check((MyCell)dgv.Rows[numbers].Cells[letters], letters + numbers, new HashSet<string>())) 
                   )
                */
                if (!(RecurChecker.Check((MyCell)dgv.Rows[numbers].Cells[letters], letters + numbers, new HashSet<string>())))
                {
               //     MessageBox.Show("Recur checker OK");
               //     MessageBox.Show("Expression: " + ((MyCell)dgv.Rows[numbers].Cells[letters]).Expression);
                    Program.form1.CurrentRow = numbers;
                    int tmp;
                    Program.form1.getColNameToColIndex().TryGetValue(letters, out tmp);
                    Program.form1.CurrentColumn = tmp;
                    return Convert.ToDouble(Calculator.Evaluate(((MyCell)dgv.Rows[numbers].Cells[letters]).Expression));
                }
                else
                {
                    throw new Exception("Cycles detected!");
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Cycles!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        private double WalkLeft(ExpressionGrammarParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<ExpressionGrammarParser.ExpressionContext>(0));
        }

        private double WalkRight(ExpressionGrammarParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<ExpressionGrammarParser.ExpressionContext>(1));
        }
    }
}

