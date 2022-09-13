using FluentAssertions;
using Xunit;

namespace SimpleCalculator.Core.Operations.Tests;

public class SubtractOperationTests
{
    [Theory]
    [InlineData(1, 1, 0)]
    public void Execute_ReturnsExpectedValue(double valueA, double valueB, double expectedResult)
    {
        // Arrange
        var sut = new SubtractOperation();

        // Act
        double result = sut.Execute(valueA, valueB);

        // Assert
        result.Should().Be(expectedResult);
    }
}