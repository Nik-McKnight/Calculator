﻿/// <summary>
/// Author: Nik McKnight
/// Date: 4/20/2022
/// 
/// This class represents the Model of the calculator in an MVC architecture.
/// </summary>

using System.Diagnostics;

namespace Utilities
{
    /// <summary>
    /// The calculator model.
    /// </summary>
    public class Calculator
    {
        // The formula that is currently being entered.
        private string tempFormula;

        // A list containing results of previous calculations.
        private List<Result> Results;

        // The most recently calculated result.
        private double value;

        // The second most recently calculated result.
        private double lastValue;

        // The last character entered by the controller.
        char lastEntry;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Calculator()
        {
            tempFormula = "";
            Results = new List<Result>();
        }

        /// <summary>
        /// Adds the given character to the formula string.
        /// </summary>
        /// <param name="entry">The latest character entered by the controller.</param>
        public void AddToFormula(char entry)
        {
            // Automatically adds a * when a number is followed directly by a left
            // parenthesis or a right parenthesis is followed directly by a number.
            try
            {
                if ((Char.IsDigit(lastEntry) & entry == '(') |
                    (lastEntry == ')' & Char.IsDigit(entry)))
                {
                    tempFormula += '*';
                }
            }
            catch
            {

            }

            // Adds the entry to the formula
            tempFormula += entry;
            lastEntry = entry;
        }

        /// <summary>
        /// Deletes the most recently entered character.
        /// </summary>
        public void Backspace()
        {
            tempFormula = tempFormula.Substring(0, tempFormula.Length - 1);
        }

        /// <summary>
        /// Self-Explanatory.
        /// </summary>
        public void ClearFormula()
        {
            tempFormula = "";
        }

        /// <summary>
        /// Converts a positive formula to negative and vice-versa.
        /// </summary>
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

        /// <summary>
        /// Sets the current function to be the denominator of a fraction.
        /// </summary>
        public void Fraction()
        {
            if (tempFormula.Length > 0)
            {
                tempFormula = "1/" + tempFormula;
            }
        }


        /// <summary>
        /// Converts the tempFormula into a formula object and calculates the result.
        /// </summary>
        /// <returns></returns>
        public double Calculate()
        {
            Formula formula;

            try
            {
                try
                {
                    // Keeps track of what the previous result was
                    lastValue = Results[0].GetValue();
                }
                catch
                {
                    // Sets the previous result to 0 if no calculations have been performed yet
                    lastValue = 0;
                }

                if (tempFormula == "")
                {
                    // Returns last result if no formula has been entered.
                    return lastValue;
                }

                else if (!Char.IsDigit(tempFormula[0]) & tempFormula[0] != '(') {
                    // Sets the previous value to be the beginning of the new function
                    // if the new formula was started with an operator other than '('
                    tempFormula = lastValue.ToString() + tempFormula;
                }

                // Creates new Formula object
                formula = new Formula(tempFormula);

                // Checks to see if a decimal value needs to be rounded off.
                value = CheckForRound((double)formula.Evaluate(s => 0));

                // Inserts this result into the list of results.
                Results.Insert(0, new Result(tempFormula, value, 1, 1.0));
                lastValue = value;
            }
            catch
            {
                value = 0;
            }

            tempFormula = "";
            return value;
        }

        /// <summary>
        /// Performs exponential operations on the current function.
        /// </summary>
        /// <param name="exp">The given power.</param>
        /// <param name="isRoot">Represents whether the operation is a root or not.</param>
        /// <returns></returns>
        public double Exponent(int exp, bool isRoot)
        {
            value = Calculate();

            // Performs exponential function
            if (!isRoot)
            {
                value = CheckForRound(Math.Pow(value, exp));
                Results.Insert(0, new Result(Results[0].GetFormula(), value, exp, 1));
            }

            // Performs root function
            else
            {
                value = CheckForRound(Math.Pow(value, 1.0/exp));
                Results.Insert(0, new Result(Results[0].GetFormula(), value, 1, exp));
            }

            lastValue = value;
            return value;
        }

        /// <summary>
        /// Self-Explanatory
        /// </summary>
        /// <returns></returns>
        public List<Result> GetAllResults()
        {
            return Results;
        }

        /// <summary>
        /// Self-Explanatory
        /// </summary>
        /// <returns></returns>
        public string GetTempFormula()
        {
            return tempFormula;
        }

        /// <summary>
        /// Rounds the value to the nearest int if it should be an int to compensate
        /// for floating point imprecision. 
        /// e.g. sqrt(2)^2 != 2.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double CheckForRound(double value)
        {
            int nearestInt;
            nearestInt = (int)Math.Round(value);
            if (Math.Abs((double)nearestInt - value) <= 0.0000000001)
            {
                value = nearestInt;
            }
            return value;
        }

        /// <summary>
        /// Represents a result. 
        /// </summary>
        public struct Result
        {
            private string formula;
            private double value;
            private int exp;
            private double root;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="formula">The input formula.</param>
            /// <param name="value">The calculated value of the formula.</param>
            /// <param name="exp">The exponent power.</param>
            /// <param name="root">The root power.</param>
            public Result(string formula, double value, int exp, double root)
            {
                this.formula = formula.ToString();
                this.value = value;
                this.exp = exp;
                this.root = root;
            }

            /// <summary>
            /// Self-Explanatory
            /// </summary>
            /// <returns></returns>
            public string GetFormula()
            {
                string output = "";
                output += formula;
                output = FormatExponent(output, exp, root);
                return output;
            }

            /// <summary>
            /// Self-Explanatory
            /// </summary>
            /// <returns></returns>
            public double GetValue()
            {
                return value;
            }

            /// <summary>
            /// Self-Explanatory
            /// </summary>
            /// <returns></returns>
            public string ToString()
            {
                return (formula.ToString() + "/n" + value + "/n");
            }

            /// <summary>
            /// Formats the string to show the proper exponent.
            /// </summary>
            /// <param name="formula">The input formula.</param>
            /// <param name="exp">The exponent power.</param>
            /// <param name="root">The root power.</param>
            /// <returns></returns>
            public string FormatExponent(string formula, int exp, double root)
            {
                // The exponent of the input formula.
                double oldExp = 1;
                double newExp = exp;
                string output = formula;

                // Checks if  the formula if it has an exponent other than 1.
                if (formula.Contains('^'))
                {
                    int length = output.IndexOf('^');

                    // Determines the old exponent from the input string.
                    oldExp = PowerFromString(output.Substring(length+1));

                    // Removes the exponent formatting and parentheses from the string.
                    output = output.Substring(1, length-2);
                }

                // Calculates the new exponent.
                newExp = oldExp * exp / root;

                // Returns the formula with no extra formatting if the new exponent is 1.
                if (newExp.Equals(1.0))
                {
                    return output;
                }

                // Check for power of 0;
                if (newExp.Equals(0.0))
                {
                    return "(" + output + ")^0";
                }

                // Reformats the formula to show the new exponent.
                else
                {
                    return "(" + output + ")^" + PowerToString(newExp);
                }
            }

            /// <summary>
            /// Converts an integer or fractional power (e.g. 1 or 1/4) to a double (1.0 or 0.25)
            /// </summary>
            /// <param name="powerString">The input power in string format.</param>
            /// <returns>The power as a double.</returns>
            private double PowerFromString(string powerString)
            {
                // Default power
                double power = 1;

                // If the input string is a fraction
                if (powerString.Contains('/'))
                {
                    // Gets the denominator
                    string tempString = powerString.Substring(3, powerString.Length - 4);
                    
                    // Converts that denominator to a double
                    power = 1 / Convert.ToDouble(tempString);
                }

                // If the input string is an integer
                else
                {
                    power = Convert.ToDouble(powerString);
                }

                return power;
            }

            /// <summary>
            /// Converts a double (1.0 or 0.25) to an integer or fractional power.
            /// </summary>
            /// <param name="power">The input power as a double.</param>
            /// <returns>The power as a string.</returns>
            private string PowerToString(double power)
            {
                string powerString = "";

                if (power < 1)
                {
                    // Converts the decimal power to an int
                    int tempPower = (int)(1 / power);

                    // Formats it as a fractional power string
                    powerString = $"(1/{tempPower})";
                }

                else
                {
                    powerString = power.ToString();
                }

                return powerString;
            }
        }
    }
} 