// TODO Resize buttons/boxes when window resized
// TODO Scientific
// TODO Saved results to memory to use as variables
// TODO Fit to window
// TODO Readme
// TODO Comment on everything possible
// TODO Fix issue with floating point precision (like on squaring then rooting)
// TODO Refactor/Rebuild Formula
// TODO Unit Testing
// TODO Make shift keys work
// TODO 
// TODO 
// TODO 
// TODO 
// TODO 
// TODO 
// TODO 
// TODO 
// TODO 
// TODO 
// TODO 

using System.Diagnostics;
using static Utilities.Calculator;
using Utilities;

namespace GUI
{
    public partial class Form1 : Form
    {
        private Calculator calculator;
        private bool shift;

        public Form1()
        {
            calculator = new Calculator();
            shift = false;
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                // Allows data to be entered by hitting enter
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

                //TODO Figure out these operators
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
                    if (shift == true)
                    {
                        ProcessInput('(');
                    }
                    else
                    {
                        ProcessInput('9');
                    }
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

        /*private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Shift | e.KeyCode == Keys.ShiftKey | e.KeyCode == Keys.LShiftKey | e.KeyCode == Keys.RShiftKey)
            {
                shift = true;
                Debug.WriteLine(shift);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Shift | e.KeyCode == Keys.ShiftKey | e.KeyCode == Keys.LShiftKey | e.KeyCode == Keys.RShiftKey)
            {
                shift = false;
                Debug.WriteLine(shift);
            }
        }*/

        private void ProcessInput(char buttonOrKey)
        {
            Char[] standardOperators;
            Char[] advancedOperators;

            standardOperators = new char[]{ '.', '+', '-', '*', '/', '(', ')' };

            advancedOperators = new char[] { '=', 'N', 'F', 'E', 'R', 'C', 'B' };

            if (char.IsDigit(buttonOrKey) | standardOperators.Contains(buttonOrKey))
            {
                calculator.AddToFormula(buttonOrKey);
                FormulaBox.Text = calculator.getTempFormula();
            }

            if (advancedOperators.Contains(buttonOrKey))
            {
                string result;

                switch (buttonOrKey)
                {
                    case '=':
                        result = calculator.Calculate().ToString();
                        FormulaBox.Text = result;
                        DisplayResults(result);
                        break;


                    case 'N':
                        calculator.InvertFormula();
                        FormulaBox.Text = calculator.getTempFormula();
                        break;


                    case 'F':
                        calculator.Fraction();
                        FormulaBox.Text = calculator.getTempFormula();
                        break;


                    case 'E':
                        result = calculator.Exponent(Convert.ToInt32(PowerBox.Text), false).ToString();
                        FormulaBox.Text = result;
                        DisplayResults(result);
                        break;


                    case 'R':
                        result = calculator.Exponent(Convert.ToInt32(PowerBox.Text), true).ToString();
                        FormulaBox.Text = result;
                        DisplayResults(result);
                        break;


                    case 'C':
                        calculator.ClearFormula();
                        result = "";
                        DisplayResults(result);
                        break;


                    case 'B':
                        calculator.Backspace();
                        FormulaBox.Text = calculator.getTempFormula();
                        break;
                }
            }
            

        }

        private void DisplayResults(string result)
        {
            ResultsBox.Clear();
            FormulaBox.Text = result;
            foreach (Result r in calculator.GetAllResults())
            {
                ResultsBox.Text += (r.GetFormula() + Environment.NewLine + r.GetValue() + Environment.NewLine + Environment.NewLine) ;
            }
        }

        private void ChangePower(int change)
        {
            int power = Convert.ToInt32(PowerBox.Text) + change;
            PowerBox.Text = power.ToString();
        }

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


