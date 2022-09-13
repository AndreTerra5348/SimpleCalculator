using SimpleCalculator.Core.Models;

namespace SimpleCalculator.Core.Operations;

public class SubtractOperation : IOperation
{
    public double Execute(double a, double b)
    {
        return a - b;
    }
}
