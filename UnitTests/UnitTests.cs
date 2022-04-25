using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;
using static Utilities.Calculator;

namespace UnitTests
{
    [TestClass]
    public class CalculatorModelTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            Calculator calculator = new Calculator();
            Assert.AreEqual("", calculator.GetTempFormula());
            Assert.AreEqual(0, calculator.GetAllResults().Count);
        }

        [TestMethod]
        public void TestAddToFormula()
        {
            Calculator calculator = new Calculator();
            string temp = "5+5*5";

            foreach (char c in temp)
            {
                calculator.AddToFormula(c);
            }

            Assert.AreEqual(temp, calculator.GetTempFormula());
        }

        [TestMethod]
        public void TestBackspace()
        {
            Calculator calculator = new Calculator();
            string temp = "5+5*5";

            foreach (char c in temp)
            {
                calculator.AddToFormula(c);
            }

            calculator.Backspace();

            Assert.AreEqual("5+5*", calculator.GetTempFormula());
        }

        [TestMethod]
        public void TestClearFormula()
        {
            Calculator calculator = new Calculator();
            string temp = "5+5*5";

            foreach (char c in temp)
            {
                calculator.AddToFormula(c);
            }

            calculator.ClearFormula();

            Assert.AreEqual("", calculator.GetTempFormula());
        }

        [TestMethod]
        public void TestInvertFormula()
        {
            Calculator calculator = new Calculator();
            string temp = "5+5*5";

            foreach (char c in temp)
            {
                calculator.AddToFormula(c);
            }

            calculator.InvertFormula();
            Assert.AreEqual("-" + temp, calculator.GetTempFormula());
            calculator.InvertFormula();
            Assert.AreEqual(temp, calculator.GetTempFormula());
        }

        [TestMethod]
        public void TestFraction()
        {
            Calculator calculator = new Calculator();
            string temp = "5+5*5";

            foreach (char c in temp)
            {
                calculator.AddToFormula(c);
            }

            calculator.Fraction();

            Assert.AreEqual("1/" + temp, calculator.GetTempFormula());
        }


        [TestMethod]
        public void TestCalculate()
        {
            Calculator calculator = new Calculator();
            string temp = "5+5*5";

            foreach (char c in temp)
            {
                calculator.AddToFormula(c);
            }

            Assert.AreEqual(30.0, calculator.Calculate());
        }

        [TestMethod]
        public void TestCalculateExponent()
        {
            Calculator calculator = new Calculator();
            string temp = "10";

            foreach (char c in temp)
            {
                calculator.AddToFormula(c);
            }

            Assert.AreEqual(100.0, calculator.Exponent(2, false));
            Assert.AreEqual(1000000.0, calculator.Exponent(3, false));
        }

        [TestMethod]
        public void TestCalculateRoot()
        {
            Calculator calculator = new Calculator();
            string temp = "1000000";

            foreach (char c in temp)
            {
                calculator.AddToFormula(c);
            }

            Assert.AreEqual(100.0, calculator.Exponent(3, true));
            Assert.AreEqual(10.0, calculator.Exponent(2, true));
        }

        [TestMethod]
        public void TestCheckForRound()
        {
            Calculator calculator = new Calculator();

            Assert.AreEqual(4.99, calculator.CheckForRound(4.99));
            Assert.AreEqual(5.0, calculator.CheckForRound(4.99999999999));
        }

        [TestMethod]
        public void TestStress()
        {
            Calculator calculator = new Calculator();

            for (int i = 1; i <= 100; i++)
            {
                StressHelper(calculator, i);
                Assert.AreEqual((double)i, calculator.GetAllResults()[0].GetValue());
            }

            Assert.AreEqual(100, calculator.GetAllResults().Count);
        }

        public void StressHelper(Calculator c, int i)
        {
            c.AddToFormula('1');
            for (int j = 2; j <= i; j++)
            {
                c.AddToFormula('+');
                c.AddToFormula('1');
            }
            c.Calculate();
        }
    }

    [TestClass]
    public class ResultTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            Result result = new Result("5+5*5", 30.0, 1, 1.0);
            Assert.AreEqual("5+5*5", result.GetFormula());
            Assert.AreEqual(30.0, result.GetValue());
        }

        [TestMethod]
        public void TestFormatExponent()
        {
            string temp = "30";
            Result result = new Result("5+5*5", 30.0, 1, 1.0);

            temp = result.FormatExponent(temp, 2, 1.0);
            Assert.AreEqual("(30)^2", temp);

            temp = result.FormatExponent(temp, 3, 1.0);
            Assert.AreEqual("(30)^6", temp);

            temp = result.FormatExponent(temp, 1, 4.0);
            Assert.AreEqual("(30)^1.5", temp);

            temp = result.FormatExponent(temp, 1, 1.5);
            Assert.AreEqual("30", temp);

            temp = result.FormatExponent(temp, 1, 2.0);
            Assert.AreEqual("(30)^(1/2)", temp);

            temp = result.FormatExponent(temp, 1, 4.0);
            Assert.AreEqual("(30)^(1/8)", temp);

            temp = result.FormatExponent(temp, 8, 1.0);
            Assert.AreEqual("30", temp);

            temp = result.FormatExponent(temp, 0, 1.0);
            Assert.AreEqual("(30)^0", temp);
        }
    }
}