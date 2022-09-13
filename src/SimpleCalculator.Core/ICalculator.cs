namespace SimpleCalculator.Core;

public interface ICalculator
{
    double ValueA { get; }
    double ValueB { get; }

    void AssignValue(double value);
    double Calculate();
    void Clear();
}