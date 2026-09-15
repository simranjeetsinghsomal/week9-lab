using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

class CheckCalculators
{
    static int passed;

    static void Check(Form form, string first, string second, string operation, string expected)
    {
        TextBox input1 = (TextBox)form.GetType().GetField("txtNumber1", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(form);
        TextBox input2 = (TextBox)form.GetType().GetField("txtNumber2", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(form);
        Label result = (Label)form.GetType().GetField("lblResult", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(form);
        input1.Text = first;
        input2.Text = second;
        Button selected = null;
        foreach (Control control in form.Controls)
            if (control is Button && control.Text == operation) selected = (Button)control;
        if (selected == null) throw new Exception("Missing button: " + operation);
        selected.PerformClick();
        Application.DoEvents();
        if (result.Text != expected)
            throw new Exception(form.Text + ": expected " + expected + ", got " + result.Text);
        Console.WriteLine("PASS " + form.Text + ": " + first + " " + operation + " " + second + " => " + result.Text);
        passed++;
    }

    [STAThread]
    static int Main()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Application.EnableVisualStyles();
        try
        {
            using (var form = new NonMVC_Calculator.CalculatorForm())
            {
                form.ShowInTaskbar = false;
                form.Opacity = 0;
                form.Show();
                Application.DoEvents();
                Check(form, "8", "3", "Add", "Result: 11");
                Check(form, "-5", "2", "Add", "Result: -3");
                Check(form, "abc", "2", "Add", "Enter two whole numbers.");
                Check(form, "", "2", "Add", "Enter two whole numbers.");
                Check(form, "2147483647", "1", "Add", "Result is too large.");
                form.Close();
            }
            using (var form = new MVC_Calculator.CalculatorForm())
            {
                form.ShowInTaskbar = false;
                form.Opacity = 0;
                form.Show();
                Application.DoEvents();
                Check(form, "8", "3", "Add", "Result: 11");
                Check(form, "8", "3", "Subtract", "Result: 5");
                Check(form, "8", "3", "Multiply", "Result: 24");
                Check(form, "8", "2", "Divide", "Result: 4");
                Check(form, "7", "2", "Divide", "Result: 3.5");
                Check(form, "-5", "2", "Add", "Result: -3");
                Check(form, "3", "8", "Subtract", "Result: -5");
                Check(form, "-5", "2", "Multiply", "Result: -10");
                Check(form, "-7", "2", "Divide", "Result: -3.5");
                Check(form, "0", "5", "Divide", "Result: 0");
                Check(form, "8", "0", "Divide", "Cannot divide by zero.");
                Check(form, "abc", "2", "Add", "Enter two whole numbers.");
                Check(form, "", "2", "Subtract", "Enter two whole numbers.");
                Check(form, "2.5", "2", "Multiply", "Enter two whole numbers.");
                Check(form, "2147483647", "1", "Add", "Result is too large.");
                Check(form, "-2147483648", "1", "Subtract", "Result is too large.");
                Check(form, "2147483647", "2", "Multiply", "Result is too large.");
                Check(form, "4", "2", "Add", "Result: 6");
                form.Close();
            }
            Console.WriteLine(passed + " checks passed. Both forms were opened invisibly and their buttons were exercised with PerformClick.");
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("FAIL " + ex);
            return 1;
        }
    }
}
