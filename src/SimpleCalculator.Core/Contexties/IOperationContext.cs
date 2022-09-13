using SimpleCalculator.Core.Models;

namespace SimpleCalculator.Core.Contexties;

public interface IOperationContext
{
    void SetOperation(IOperation operation);
    double Execute(double a, double b);
    bool HasOperator();
    void Clear();
}
