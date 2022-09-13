using FluentAssertions;
using Moq;
using SimpleCalculator.Core.Models;
using Xunit;

namespace SimpleCalculator.Core.Contexties.Tests;

public class OperationContextTests
{
    [Theory]
    [InlineData(5)]
    public void Execute_ReturnsExpectedValue(double expectedValue)
    {
        // Arrange
        var mockOperation = new Mock<IOperation>();
        mockOperation.Setup(x => x.Execute(It.IsAny<double>(), It.IsAny<double>()))
            .Returns(expectedValue);

        var sut = new OperationContext();
        sut.SetOperation(mockOperation.Object);

        // Act
        var result = sut.Execute(1, 1);

        // Assert
        result.Should().Be(expectedValue);
    }

    [Fact]
    public void HasOperator_WithoutOperation_ReturnsFalse()
    {
        // Arrange
        var sut = new OperationContext();

        // Act
        var result = sut.HasOperator();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void HasOperator_WithOperation_ReturnsTrue()
    {
        // Arrange
        var mockOperation = new Mock<IOperation>();
        var sut = new OperationContext();
        sut.SetOperation(mockOperation.Object);

        // Act
        var result = sut.HasOperator();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Clear_CallHasOperator_ReturnsFalse()
    {
        // Arrange
        var mockOperation = new Mock<IOperation>();
        var sut = new OperationContext();
        sut.SetOperation(mockOperation.Object);
        sut.Clear();

        // Act
        var result = sut.HasOperator();

        // Assert
        result.Should().BeFalse();
    }
}