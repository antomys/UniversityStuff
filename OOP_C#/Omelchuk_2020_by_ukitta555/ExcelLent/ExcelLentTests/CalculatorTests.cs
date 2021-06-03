using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExcelLent;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelLent.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void EvaluateTestExponent()
        {
            Assert.AreEqual(Calculator.Evaluate("0 ^ 0"), 1);
            Assert.AreEqual(Calculator.Evaluate("1 ^ 10"), 1);
            Assert.AreEqual(Calculator.Evaluate("10 ^ 1"), 10);
            Assert.AreEqual(Calculator.Evaluate("10 ^ 2"), 100);
            Assert.AreEqual(Calculator.Evaluate("10 ^ 0"), 1);
        }

        [TestMethod()]
        public void EvaluateTestMMin()
        {
            Assert.AreEqual(Calculator.Evaluate("mmin(12, 10, 5)"), 5);
            Assert.AreEqual(Calculator.Evaluate("mmin(1.2, 10, 3)"), 1.2);
            Assert.AreEqual(Calculator.Evaluate("mmin(1, 1, 1)"), 1);
            Assert.AreEqual(Calculator.Evaluate("mmin(-1, 1, 0)"), -1);
            Assert.AreEqual(Calculator.Evaluate("mmin(-(12^3), +120123, 8000000000.123123,-23)"), -1728);
        }
        [TestMethod()]
        public void EvaluateTestMod()
        {
            Assert.AreEqual(Calculator.Evaluate("12 mod 2"), 0);
            Assert.AreEqual(Calculator.Evaluate("13 mod 2"), 1);
            Assert.IsTrue(Math.Abs(Calculator.Evaluate("13.1 mod 2") - 1.1) < 0.1);
            Assert.AreEqual(Calculator.Evaluate("0 mod 2"), 0);
            Assert.AreEqual(Calculator.Evaluate("13 mod -2"), 1);
        }
    }
}