/// <summary>
/// Author: Nik McKnight
/// Date: 4/20/2022
/// 
/// This partial class represents the Controller of the calculator in an MVC architecture.
/// </summary>

// TODO Scientific (future release)
// TODO Saved results to memory to use as variables (future release)
// TODO Refactor/Rebuild Formula (future release)
// TODO Add logging (future release)

using System.Diagnostics;
using static Utilities.Calculator;
using Utilities;

namespace GUI
{
    /// <summary>
    /// The calculator controller.
    /// </summary>    
    public partial class StandardCalculator : Form
    {
        // The calculator model associated with this controller.
        private Calculator calculator;

        // Standard button width
        int buttonWidth;

        // Standard button height
        int buttonHeight;

        // Padding between buttons
        int PADDING = 5;

        /// <summary>
        /// GUI constructor
        /// </summary>
        public StandardCalculator()
        {
            calculator = new Calculator();
            InitializeComponent();
            this.Width = 490;
            this.Height = 560;
            resizeElements();
        }


        /// <summary>
        /// Processes input from the keyboard
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData">The key that has been pressed.</param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    ProcessInput('=');
                    break;

                case Keys.Back:
                    ProcessInput('B');
                    break;

                case Keys.OemPeriod:
                    ProcessInput('.');
                    break;

                case Keys.Oemplus:
                    ProcessInput('+');
                    break;

                case Keys.OemMinus:
                    ProcessInput('-');
                    break;

                //TODO Figure out these operators (future release)
                /*case Keys.:
                    ProcessInput('*');
                    break;

                case Keys.:
                    ProcessInput('/');
                    break; 

                case Keys.:
                    ProcessInput('(');
                    break;

                case Keys.:
                    ProcessInput(')');
                    break;*/

                case Keys.N:
                    ProcessInput('N');
                    break;

                case Keys.F:
                    ProcessInput('F');
                    break;

                case Keys.E:
                    ProcessInput('E');
                    break;

                case Keys.R:
                    ProcessInput('R');
                    break;

                case Keys.C:
                    ProcessInput('C');
                    break;

                case Keys.D0:
                    ProcessInput('0');
                    break;

                case Keys.D1:
                    ProcessInput('1');
                    break;

                case Keys.D2:
                    ProcessInput('2');
                    break;

                case Keys.D3:
                    ProcessInput('3');
                    break;

                case Keys.D4:
                    ProcessInput('4');
                    break;

                case Keys.D5:
                    ProcessInput('5');
                    break;

                case Keys.D6:
                    ProcessInput('6');
                    break;

                case Keys.D7:
                    ProcessInput('7');
                    break;

                case Keys.D8:
                    ProcessInput('8');
                    break;

                case Keys.D9:
                    ProcessInput('9');
                    break;

                case Keys.Up:
                    ChangePower(1);
                    break;

                case Keys.Down:
                    ChangePower(-1);
                    break;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
            return true;
        }

        /// <summary>
        /// Determines what should be done after a given button click or key press,
        /// </summary>
        /// <param name="buttonOrKey">A char that represents the button click or key press</param>
        private void ProcessInput(char buttonOrKey)
        {
            // Basic arithmetic operators (+, -, etc.)
            Char[] standardOperators;

            // Arithmetic and non-arithmetic operators that required
            // a little more work to implement (=, invert, backspace, etc.)
            Char[] advancedOperators;

            standardOperators = new char[]{ '.', '+', '-', '*', '/', '(', ')' };
            advancedOperators = new char[] { '=', 'N', 'F', 'E', 'R', 'C', 'B' };

            // Checks if the input is a number or a basic operator
            if (char.IsDigit(buttonOrKey) | standardOperators.Contains(buttonOrKey))
            {
                // Adds it directly to the formula string
                calculator.AddToFormula(buttonOrKey);

                // Gets the formula string from the calculator object and shows it on the GUI
                FormulaBox.Text = calculator.GetTempFormula();
            }

            // Checks if the input is an advanced operator
            if (advancedOperators.Contains(buttonOrKey))
            {
                string result;

                switch (buttonOrKey)
                {
                    // Calculates Formula (= button)
                    case '=':
                        result = calculator.Calculate().ToString();
                        FormulaBox.Text = result;
                        DisplayResults(result);
                        break;

                    // Inverts sign of formula (+/- button)
                    case 'N':
                        calculator.InvertFormula();
                        FormulaBox.Text = calculator.GetTempFormula();
                        break;

                    // Sets the current formula to be the denominator of a fraction (1/x button)
                    case 'F':
                        calculator.Fraction();
                        FormulaBox.Text = calculator.GetTempFormula();
                        break;

                    // Calculates the current formula to the n power given in the power box. (x^n button)
                    case 'E':
                        result = calculator.Exponent(Convert.ToInt32(PowerBox.Text), false).ToString();
                        FormulaBox.Text = result;
                        DisplayResults(result);
                        break;

                    // Calculates the n root (given in the power box) of the current formula (root x button)
                    case 'R':
                        result = calculator.Exponent(Convert.ToInt32(PowerBox.Text), true).ToString();
                        FormulaBox.Text = result;
                        DisplayResults(result);
                        break;

                    // Deletes the current formula (C Button)
                    case 'C':
                        calculator.ClearFormula();
                        result = "";
                        DisplayResults(result);
                        break;

                    // Deletes the most recent character entered (backspace Button)
                    // TODO Modify this so it undoes the last entrey, rather than simply deleting the last entered char
                    case 'B':
                        calculator.Backspace();
                        FormulaBox.Text = calculator.GetTempFormula();
                        break;
                }
            }
        }

        /// <summary>
        /// Displays the latest result in the formula box and all results in the results box.
        /// </summary>
        /// <param name="result">The latest result of a calculation.</param>
        private void DisplayResults(string result)
        {
            ResultsBox.Clear();
            FormulaBox.Text = result;
            foreach (Result r in calculator.GetAllResults())
            {
                ResultsBox.Text += (r.GetFormula() + Environment.NewLine + r.GetValue() + Environment.NewLine + Environment.NewLine) ;
            }
        }

        /// <summary>
        /// Updates the power to use in the exponential and root calculations.
        /// </summary>
        /// <param name="change">Positive or negative integer.</param>
        private void ChangePower(int change)
        {
            int power = Convert.ToInt32(PowerBox.Text) + change;
            PowerBox.Text = power.ToString();
        }

        /// <summary>
        /// Resizes all elements on the form when the window is resized.
        /// 
        /// TODO There has to be an easier way to do this (future release?)
        /// </summary>
        private void resizeElements()
        {
            // Top Row
            buttonWidth = (this.Width - 8 * PADDING) / 7;
            buttonHeight = (this.Height - 8 * PADDING) / 8;

            DownButton.Left = PADDING;
            DownButton.Top = 7 * PADDING;
            DownButton.Width = buttonWidth;

            PowerBox.Left = DownButton.Right + PADDING;
            PowerBox.Top = 7 * PADDING;
            PowerBox.Width = buttonWidth;

            PowerLabel.Left = (PowerBox.Right + PowerBox.Left) / 2 - 20;
            PowerLabel.Top = 2 * PADDING;

            UpButton.Left = PowerBox.Right + PADDING;
            UpButton.Top = 7 * PADDING;
            UpButton.Width = buttonWidth;

            // Formula Box
            FormulaBox.Left = PADDING;
            FormulaBox.Top = UpButton.Bottom + PADDING;
            FormulaBox.Width = (7 * buttonWidth) + (3 * PADDING);
            FormulaBox.Height = 25;

            // Second Row
            LeftParButton.Left = PADDING;
            LeftParButton.Top = FormulaBox.Bottom + PADDING;
            LeftParButton.Width = buttonWidth;
            LeftParButton.Height = buttonHeight;

            RightParButton.Left = LeftParButton.Right + PADDING;
            RightParButton.Top = FormulaBox.Bottom + PADDING;
            RightParButton.Width = buttonWidth;
            RightParButton.Height = buttonHeight;

            ClearButton.Left = RightParButton.Right + PADDING;
            ClearButton.Top = FormulaBox.Bottom + PADDING;
            ClearButton.Width = buttonWidth;
            ClearButton.Height = buttonHeight;

            BackButton.Left = ClearButton.Right + PADDING;
            BackButton.Top = FormulaBox.Bottom + PADDING;
            BackButton.Width = buttonWidth;
            BackButton.Height = buttonHeight;

            // Third Row
            FracButton.Left = PADDING;
            FracButton.Top = LeftParButton.Bottom + PADDING;
            FracButton.Width = buttonWidth;
            FracButton.Height = buttonHeight;

            ExpButton.Left = FracButton.Right + PADDING;
            ExpButton.Top = LeftParButton.Bottom + PADDING;
            ExpButton.Width = buttonWidth;
            ExpButton.Height = buttonHeight;

            RootButton.Left = ExpButton.Right + PADDING;
            RootButton.Top = LeftParButton.Bottom + PADDING;
            RootButton.Width = buttonWidth;
            RootButton.Height = buttonHeight;

            DivButton.Left = RootButton.Right + PADDING;
            DivButton.Top = LeftParButton.Bottom + PADDING;
            DivButton.Width = buttonWidth;
            DivButton.Height = buttonHeight;

            // Fourth Row
            SevenButton.Left = PADDING;
            SevenButton.Top = FracButton.Bottom + PADDING;
            SevenButton.Width = buttonWidth;
            SevenButton.Height = buttonHeight;

            EightButton.Left = SevenButton.Right + PADDING;
            EightButton.Top = FracButton.Bottom + PADDING;
            EightButton.Width = buttonWidth;
            EightButton.Height = buttonHeight;

            NineButton.Left = EightButton.Right + PADDING;
            NineButton.Top = FracButton.Bottom + PADDING;
            NineButton.Width = buttonWidth;
            NineButton.Height = buttonHeight;

            MultButton.Left = NineButton.Right + PADDING;
            MultButton.Top = FracButton.Bottom + PADDING;
            MultButton.Width = buttonWidth;
            MultButton.Height = buttonHeight;

            // Fifth Row
            FourButton.Left = PADDING;
            FourButton.Top = MultButton.Bottom + PADDING;
            FourButton.Width = buttonWidth;
            FourButton.Height = buttonHeight;

            FiveButton.Left = FourButton.Right + PADDING;
            FiveButton.Top = MultButton.Bottom + PADDING;
            FiveButton.Width = buttonWidth;
            FiveButton.Height = buttonHeight;

            SixButton.Left = FiveButton.Right + PADDING;
            SixButton.Top = MultButton.Bottom + PADDING;
            SixButton.Width = buttonWidth;
            SixButton.Height = buttonHeight;

            SubButton.Left = SixButton.Right + PADDING;
            SubButton.Top = MultButton.Bottom + PADDING;
            SubButton.Width = buttonWidth;
            SubButton.Height = buttonHeight;

            // Sixth Row
            OneButton.Left = PADDING;
            OneButton.Top = SubButton.Bottom + PADDING;
            OneButton.Width = buttonWidth;
            OneButton.Height = buttonHeight;

            TwoButton.Left = OneButton.Right + PADDING;
            TwoButton.Top = SubButton.Bottom + PADDING;
            TwoButton.Width = buttonWidth;
            TwoButton.Height = buttonHeight;

            ThreeButton.Left = TwoButton.Right + PADDING;
            ThreeButton.Top = SubButton.Bottom + PADDING;
            ThreeButton.Width = buttonWidth;
            ThreeButton.Height = buttonHeight;

            AddButton.Left = ThreeButton.Right + PADDING;
            AddButton.Top = SubButton.Bottom + PADDING;
            AddButton.Width = buttonWidth;
            AddButton.Height = buttonHeight;

            // Seven Row
            PosNegButton.Left = PADDING;
            PosNegButton.Top = AddButton.Bottom + PADDING;
            PosNegButton.Width = buttonWidth;
            PosNegButton.Height = buttonHeight;

            ZeroButton.Left = PosNegButton.Right + PADDING;
            ZeroButton.Top = AddButton.Bottom + PADDING;
            ZeroButton.Width = buttonWidth;
            ZeroButton.Height = buttonHeight;

            DecButton.Left = ZeroButton.Right + PADDING;
            DecButton.Top = AddButton.Bottom + PADDING;
            DecButton.Width = buttonWidth;
            DecButton.Height = buttonHeight;

            EqButton.Left = DecButton.Right + PADDING;
            EqButton.Top = AddButton.Bottom + PADDING;
            EqButton.Width = buttonWidth;
            EqButton.Height = buttonHeight;

            // Results Box
            ResultsBox.Left = BackButton.Right + PADDING;
            ResultsBox.Top = FormulaBox.Bottom + PADDING;
            ResultsBox.Width = FormulaBox.Right - ResultsBox.Left;
            ResultsBox.Height = EqButton.Bottom - BackButton.Top;
        }

        /// <summary>
        /// Event handler for the window being resized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StandardCalculator_Resize(object sender, EventArgs e)
        {
            resizeElements();
        }

        // Event handlers for button clicks are below

        private void ZeroButton_Click(object sender, EventArgs e)
        {
            ProcessInput('0');
        }

        private void OneButton_Click(object sender, EventArgs e)
        {
            ProcessInput('1');
        }

        private void TwoButton_Click(object sender, EventArgs e)
        {
            ProcessInput('2');
        }

        private void ThreeButton_Click(object sender, EventArgs e)
        {
            ProcessInput('3');
        }

        private void FourButton_Click(object sender, EventArgs e)
        {
            ProcessInput('4');
        }

        private void FiveButton_Click(object sender, EventArgs e)
        {
            ProcessInput('5');
        }

        private void SixButton_Click(object sender, EventArgs e)
        {
            ProcessInput('6');
        }

        private void SevenButton_Click(object sender, EventArgs e)
        {
            ProcessInput('7');
        }

        private void EightButton_Click(object sender, EventArgs e)
        {
            ProcessInput('8');
        }

        private void NineButton_Click(object sender, EventArgs e)
        {
            ProcessInput('9');
        }

        private void PosNegButton_Click(object sender, EventArgs e)
        {
            ProcessInput('N');
        }

        private void DecButton_Click(object sender, EventArgs e)
        {
            ProcessInput('.');
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            ProcessInput('+');
        }

        private void SubButton_Click(object sender, EventArgs e)
        {
            ProcessInput('-');
        }

        private void MultButton_Click(object sender, EventArgs e)
        {
            ProcessInput('*');
        }

        private void DivButton_Click(object sender, EventArgs e)
        {
            ProcessInput('/');
        }

        private void EqButton_Click(object sender, EventArgs e)
        {
            ProcessInput('=');
        }

        private void FracButton_Click(object sender, EventArgs e)
        {
            ProcessInput('F');
        }

        private void ExpButton_Click(object sender, EventArgs e)
        {
            ProcessInput('E');
        }

        private void RootButton_Click(object sender, EventArgs e)
        {
            ProcessInput('R');
        }

        private void LeftParButton_Click(object sender, EventArgs e)
        {
            ProcessInput('(');
        }

        private void RightParButton_Click(object sender, EventArgs e)
        {
            ProcessInput(')');
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ProcessInput('C');
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            ProcessInput('B');
        }

        private void UpButton_Click(object sender, EventArgs e)
        {
            ChangePower(1);
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            ChangePower(-1);
        }
    }
}


