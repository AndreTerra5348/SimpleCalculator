using SimpleCalculator.Core.Models;

namespace SimpleCalculator.Core.Operations;
public class DivideOperation : IOperation
{
    public double Execute(double a, double b)
    {
        if (b == 0)
            throw new DivideByZeroException("Cannot divide by zero");
        return a / b;
    }
}


