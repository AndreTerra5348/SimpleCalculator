using SimpleCalculator.Core.Contexties;

namespace SimpleCalculator.Core;
public class Calculator : ICalculator
{
    public const double DefaultValue = 0;

    public double ValueA { get; private set; } = DefaultValue;
    public double ValueB { get; private set; } = DefaultValue;

    bool _isMainValueAssigned = false;

    private readonly IOperationContext _operationContext;

    public Calculator(IOperationContext operationContext)
    {
        _operationContext = operationContext;
    }

    public void AssignValue(double value)
    {
        if (!_isMainValueAssigned)
            ValueA = value;
        else
            ValueB = value;

        _isMainValueAssigned = true;
    }

    public double Calculate()
    {
        return _operationContext.HasOperator() ?
             ValueA = _operationContext.Execute(ValueA, ValueB) : ValueA;
    }

    public void Clear()
    {
        ValueA = DefaultValue;
        ValueB = DefaultValue;
        _isMainValueAssigned = false;
        _operationContext.Clear();
    }
}
