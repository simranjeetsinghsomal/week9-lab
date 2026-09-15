namespace MVC_Calculator
{
    public class CalculatorModel
    {
        public int Add(int num1, int num2) { return checked(num1 + num2); }
        public int Subtract(int num1, int num2) { return checked(num1 - num2); }
        public int Multiply(int num1, int num2) { return checked(num1 * num2); }
        public double Divide(int num1, int num2)
        {
            if (num2 == 0)
                throw new System.DivideByZeroException();
            return (double)num1 / num2;
        }
    }
}
