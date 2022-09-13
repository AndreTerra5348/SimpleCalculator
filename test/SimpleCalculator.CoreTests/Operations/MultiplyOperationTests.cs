using FluentAssertions;
using Xunit;

namespace SimpleCalculator.Core.Operations.Tests;

public class MultiplyOperationTests
{
    [Theory]
    [InlineData(1, 1, 1)]
    public void Execute_ReturnsExpectedValue(double valueA, double valueB, double expectedResult)
    {
        // Arrange
        var sut = new MultiplyOperation();

        // Act
        double result = sut.Execute(valueA, valueB);

        // Assert
        result.Should().Be(expectedResult);

    }
}