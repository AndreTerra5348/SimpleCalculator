using SimpleCalculator.Core.Models;

namespace SimpleCalculator.Core.Operations;

public class MultiplyOperation : IOperation
{
    public double Execute(double a, double b)
    {
        return a * b;
    }
}