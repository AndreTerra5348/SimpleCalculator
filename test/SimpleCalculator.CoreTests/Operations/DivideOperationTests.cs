using FluentAssertions;
using Xunit;

namespace SimpleCalculator.Core.Operations.Tests;

public class DivideOperationTests
{
    [Theory]
    [InlineData(4, 2, 2)]
    public void Execute_ReturnsExpectedValue(double valueA, double valueB, double expectedResult)
    {
        // Arrange
        var sut = new DivideOperation();

        // Act
        double result = sut.Execute(valueA, valueB);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void Execute_WithZeros_ThrowDivideByZeroException()
    {
        // Arrange
        var sut = new DivideOperation();

        // Act
        Action act = () => sut.Execute(0, 0);

        // Assert
        act.Should()
            .Throw<DivideByZeroException>()
            .WithMessage("Cannot divide by zero");
        
    }
}