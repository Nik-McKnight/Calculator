// TODO Resize buttons/boxes when window resized
// TODO Scientific
// TODO Saved results to memory to use as variables
// TODO More roots/Exponents
// TODO Parentheses
// TODO Fit to window
// TODO Readme
// TODO Comment on eveything possible
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

        public Form1()
        {
            calculator = new Calculator();
            InitializeComponent();
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                // Allows data to be entered by hitting enter
                case Keys.Enter:
                    Debug.WriteLine("enter");
                    break;



                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
            return true;
        }

        private void ProcessInput(char buttonOrKey)
        {
            Char[] operators;

            // TODO Rest of operators. Fraction and sign-flip may be difficult.
            operators = new char[]{ '.', 'N', 'F', 'E', 'R', 'C', 'B',
                '+', '-', '*', '/', '(', ')' };

            if (char.IsDigit(buttonOrKey) | operators.Contains(buttonOrKey))
            {
                calculator.addToFormula(buttonOrKey);
                FormulaBox.Text = calculator.getTempFormula();
            }

            if (buttonOrKey.Equals('='))
            {
                string result;

                result = calculator.Calculate().ToString();

                FormulaBox.Text = result;

                DisplayResults();
            }

        }

        private void DisplayResults()
        {
            ResultsBox.Clear();
            foreach (Result result in calculator.GetAllResults())
            {
                ResultsBox.Text += (result.GetFormula() + Environment.NewLine + result.GetValue() + Environment.NewLine + Environment.NewLine) ;
            }
        }
    }
}

