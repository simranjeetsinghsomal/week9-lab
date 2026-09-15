using System;
using System.Drawing;
using System.Windows.Forms;

namespace NonMVC_Calculator
{
    public class CalculatorForm : Form
    {
        private TextBox txtNumber1 = new TextBox();
        private TextBox txtNumber2 = new TextBox();
        private Button btnAdd = new Button();
        private Label lblResult = new Label();

        public CalculatorForm()
        {
            Text = "NonMVC Calculator";
            ClientSize = new Size(340, 210);
            var label1 = new Label { Text = "Number 1", Location = new Point(20, 23) };
            var label2 = new Label { Text = "Number 2", Location = new Point(20, 63) };
            txtNumber1.Location = new Point(130, 20);
            txtNumber2.Location = new Point(130, 60);
            btnAdd.Text = "Add";
            btnAdd.Location = new Point(130, 100);
            lblResult.Text = "Result:";
            lblResult.Location = new Point(20, 150);
            lblResult.Size = new Size(300, 40);
            Controls.AddRange(new Control[] { label1, label2, txtNumber1, txtNumber2, btnAdd, lblResult });
            btnAdd.Click += AddClicked;
        }

        private void AddClicked(object sender, EventArgs e)
        {
            int number1, number2;
            if (!int.TryParse(txtNumber1.Text, out number1) ||
                !int.TryParse(txtNumber2.Text, out number2))
            {
                lblResult.Text = "Enter two whole numbers.";
                return;
            }
            try
            {
                int sum = checked(number1 + number2);
                lblResult.Text = "Result: " + sum;
            }
            catch (OverflowException)
            {
                lblResult.Text = "Result is too large.";
            }
        }
    }
}
