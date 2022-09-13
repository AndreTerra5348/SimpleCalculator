using SimpleCalculator.Core.Models;

namespace SimpleCalculator.Core.Contexties;

public class OperationContext : IOperationContext
{
    private IOperation _operation;

    public void Clear()
    {
        _operation = null;
    }

    public double Execute(double a, double b)
    {
        return _operation.Execute(a, b);
    }

    public bool HasOperator()
    {
        return _operation is not null;
    }

    public void SetOperation(IOperation operation)
    {
        _operation = operation;
    }
}
