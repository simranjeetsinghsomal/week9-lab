using System.Drawing;
using System.Windows.Forms;

namespace MVC_Calculator
{
    public class CalculatorForm : Form
    {
        private TextBox txtNumber1 = new TextBox();
        private TextBox txtNumber2 = new TextBox();
        private Label lblResult = new Label();
        private CalculatorController controller;

        public string Number1 { get { return txtNumber1.Text; } }
        public string Number2 { get { return txtNumber2.Text; } }

        public CalculatorForm()
        {
            Text = "MVC Calculator";
            ClientSize = new Size(400, 210);
            controller = new CalculatorController(this, new CalculatorModel());
            var label1 = new Label { Text = "Number 1", Location = new Point(20, 23) };
            var label2 = new Label { Text = "Number 2", Location = new Point(20, 63) };
            txtNumber1.Location = new Point(130, 20);
            txtNumber2.Location = new Point(130, 60);
            lblResult.Text = "Result:";
            lblResult.Location = new Point(20, 150);
            lblResult.Size = new Size(360, 40);
            Controls.AddRange(new Control[] { label1, label2, txtNumber1, txtNumber2, lblResult });
            AddOperationButton("Add", 20);
            AddOperationButton("Subtract", 110);
            AddOperationButton("Multiply", 200);
            AddOperationButton("Divide", 290);
        }

        private void AddOperationButton(string operation, int left)
        {
            var button = new Button();
            button.Text = operation;
            button.Location = new Point(left, 100);
            button.Size = new Size(80, 28);
            button.Click += (sender, e) => controller.Calculate(operation);
            Controls.Add(button);
        }

        public void ShowResult(string result)
        {
            lblResult.Text = result;
        }
    }
}
