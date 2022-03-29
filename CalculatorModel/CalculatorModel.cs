using System.Diagnostics;

namespace Utilities
{
    public class Calculator
    {
        private string tempFormula;
        private List<Result> Results;

        public Calculator()
        {
            tempFormula = "";
            Results = new List<Result>();
        }

        public void addToFormula(char entry)
        {
            tempFormula += entry;
        }

        public double Calculate()
        {
            Formula formula;
            double value;
            double lastValue;

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
                Results.Insert(0, new Result(tempFormula, value));
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
            
            public Result(string formula, double value)
            {
                this.formula = formula.ToString();
                this.value = value;
            }

            public string GetFormula()
            {
                return formula;
            }

            public double GetValue()
            {
                return value;
            }

            public string GetString()
            {
                return (formula.ToString() + "/n" + value + "/n");
            }
        }
    }
}