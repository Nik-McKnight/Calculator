using System.Diagnostics;

namespace Utilities
{
    public class Calculator
    {
        private string tempFormula;
        private List<Result> Results;
        private double value;
        private double lastValue;

        public Calculator()
        {
            tempFormula = "";
            Results = new List<Result>();
        }

        public void AddToFormula(char entry)
        {
            tempFormula += entry;
        }

        public void ClearFormula()
        {
            tempFormula = "";
        }

        public void InvertFormula()
        {
            try
            {
                if (tempFormula[0] == '-')
                {
                    tempFormula = tempFormula.Substring(1);
                }
                else
                {
                    tempFormula = "-" + tempFormula;
                }
            }
            catch
            {

            }
        }

        public double Exponent(int exp, bool isRoot)
        {
            value = Calculate();

            if (!isRoot)
            {
                value = Math.Pow(value, exp);
                Results.Insert(0, new Result(Results[0].GetFormula(), value, exp, 1));
            }

            else
            {
                value = Math.Pow(value, 1.0/exp);
                Results.Insert(0, new Result(Results[0].GetFormula(), value, 1, exp));
            }

            lastValue = value;
            return value;
        }

        public double Calculate()
        {
            Formula formula;

            try
            {
                try
                {
                    lastValue = Results[0].GetValue();
                }
                catch
                {
                    lastValue = 0;
                }

                if (tempFormula == "")
                {
                    return lastValue;
                    Debug.WriteLine("hi");
                }

                else if (!Char.IsDigit(tempFormula[0])) {
                    tempFormula = lastValue.ToString() + tempFormula;
                }

                formula = new Formula(tempFormula);
                value = (double)formula.Evaluate(s => 0);
                Results.Insert(0, new Result(tempFormula, value, 1, 1.0));
                lastValue = value;
            }
            catch (Exception ex)
            {
                value = 0;
            }

            tempFormula = "";

            return value;
        }

        public List<Result> GetAllResults()
        {
            return Results;
        }

        public string getTempFormula()
        {
            return tempFormula;
        }

        public struct Result
        {
            private string formula;
            private double value;
            private int exp;
            private double root;
            
            public Result(string formula, double value, int exp, double root)
            {
                this.formula = formula.ToString();
                this.value = value;
                this.exp = exp;
                this.root = root;
            }

            //TODO Clean up this method
            public string GetFormula()
            {
                string output = "";
                output += formula;
                output = FormatExponent(output, exp, root);
                return output;
            }

            public double GetValue()
            {
                return value;
            }

            public string GetString()
            {
                return (formula.ToString() + "/n" + value + "/n");
            }

            //TODO Refactor to eliminate root?
            private string FormatExponent(string formula, int exp, double root)
            {
                double oldExp = 1;
                string output = formula;
                if (formula.Contains('^'))
                {
                    int length = output.IndexOf('^');
                    oldExp = Convert.ToDouble(output.Substring(length+1));
                    output = output.Substring(1, length-2);
                }
                if ((oldExp * exp / root).Equals(1.0))
                {
                    return output;
                }
                else
                {
                    return "(" + output + ")^" + (oldExp * exp / root).ToString();
                }
            }
        }
    }
}