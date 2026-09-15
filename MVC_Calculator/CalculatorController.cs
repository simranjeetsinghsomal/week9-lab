using System;

namespace MVC_Calculator
{
    public class CalculatorController
    {
        private CalculatorForm view;
        private CalculatorModel model;

        public CalculatorController(CalculatorForm view, CalculatorModel model)
        {
            this.view = view;
            this.model = model;
        }

        public void Calculate(string operation)
        {
            int number1, number2;
            if (!int.TryParse(view.Number1, out number1) ||
                !int.TryParse(view.Number2, out number2))
            {
                view.ShowResult("Enter two whole numbers.");
                return;
            }

            try
            {
                double result;
                switch (operation)
                {
                    case "Add": result = model.Add(number1, number2); break;
                    case "Subtract": result = model.Subtract(number1, number2); break;
                    case "Multiply": result = model.Multiply(number1, number2); break;
                    case "Divide": result = model.Divide(number1, number2); break;
                    default: view.ShowResult("Unknown operation."); return;
                }
                view.ShowResult("Result: " + result);
            }
            catch (DivideByZeroException)
            {
                view.ShowResult("Cannot divide by zero.");
            }
            catch (OverflowException)
            {
                view.ShowResult("Result is too large.");
            }
        }
    }
}
