using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExcelLent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ExcelLent.Tests
{
    [TestClass()]
    public class Form1Tests
    {
        [TestMethod()]
        public void Form1Test()
        {
            Form1 form = new Form1();

            // Check column naming
            Assert.AreEqual(form.getDataGridView().Columns[0].Name, "A");
            Assert.AreEqual(form.getDataGridView().Columns[3].Name, "D");
            Assert.AreEqual(form.getDataGridView().Columns[25].Name, "Z");
            Assert.AreEqual(form.getDataGridView().Columns[26].Name, "AA");
            // Check row naming
            Assert.AreEqual(form.getDataGridView().Rows[0].HeaderCell.Value, "0");
            Assert.AreEqual(form.getDataGridView().Rows[4].HeaderCell.Value, "4");
        }

        [TestMethod()]
        public void getDataGridViewTest()
        {
            Form1 form = new Form1();
            Assert.IsInstanceOfType(form.getDataGridView(), typeof(DataGridView), "wrong type!");
        }
    }
}