using FluentAssertions;
using Moq;
using SimpleCalculator.Core.Contexties;
using SimpleCalculator.Core.Operations;
using Xunit;

namespace SimpleCalculator.Core.Tests;

public class CalculatorTests
{
    [Theory]
    [InlineData(5)]
    public void AssignValue_AssignValueA(double valueA)
    {
        // Arrange
        var mockOperationContext = new Mock<IOperationContext>();
        var sut = new Calculator(mockOperationContext.Object);

        // Act
        sut.AssignValue(valueA);

        // Assert
        sut.ValueA.Should().Be(valueA);
    }

    [Theory]
    [InlineData(5)]
    public void AssignValue_AssignValueB(double valueB)
    {
        // Arrange
        var mockOperationContext = new Mock<IOperationContext>();
        var sut = new Calculator(mockOperationContext.Object);
        sut.AssignValue(0);

        // Act
        sut.AssignValue(valueB);

        // Assert
        sut.ValueB.Should().Be(valueB);
    }

    [Theory]
    [InlineData(5)]
    public void Calculate_ReturnsExpectedValue(double expectedValue)
    {
        // Arrange
        var mockOperationContext = new Mock<IOperationContext>();
        mockOperationContext.Setup(x => x.Execute(It.IsAny<double>(), It.IsAny<double>()))
            .Returns(expectedValue);
        mockOperationContext.Setup(x => x.HasOperator())
            .Returns(true);

        var sut = new Calculator(mockOperationContext.Object);

        // Act
        var result = sut.Calculate();

        // Assert
        result.Should().Be(expectedValue);
    }

    [Fact]
    public void Calculate_WithNullOperation_ReturnsDefaultValue()
    {
        // Arrange
        var mockOperationContext = new Mock<IOperationContext>();
        var sut = new Calculator(mockOperationContext.Object);
        
        // Act
        var result = sut.Calculate();

        // Assert
        result.Should().Be(Calculator.DefaultValue);
    }

    [Fact]
    public void Calculate_WithDivideOperation_CallAssignValueTwice_WithZeros_ThrowDivideByZeroException()
    {
        // Arrange
        var operationContext = new OperationContext();
        operationContext.SetOperation(new DivideOperation());
        var sut = new Calculator(operationContext);
        sut.AssignValue(0);
        sut.AssignValue(0);

        // Act
        var act = () => sut.Calculate();

        // Assert
        act.Should()
            .Throw<DivideByZeroException>()
            .WithMessage("Cannot divide by zero");
    }

    [Fact]
    public void Clear_CallContextClearOnce()
    {
        // Arrange
        var mockOperationContext = new Mock<IOperationContext>();

        var sut = new Calculator(mockOperationContext.Object);

        // Act
        sut.Clear();

        // Assert
        mockOperationContext.Verify(x => x.Clear(), Times.Once);
    }

    [Fact]
    public void Clear_SetValueA_AsDefaultValue()
    {
        // Arrange
        var mockOperationContext = new Mock<IOperationContext>();
        var sut = new Calculator(mockOperationContext.Object);
        sut.AssignValue(-1);

        // Act
        sut.Clear();

        // Assert
        sut.ValueA.Should().Be(Calculator.DefaultValue);
    }

    [Fact]
    public void Clear_SetValueB_AsDefaultValue()
    {
        // Arrange
        var mockOperationContext = new Mock<IOperationContext>();
        var sut = new Calculator(mockOperationContext.Object);
        sut.AssignValue(-1);
        sut.AssignValue(-1);

        // Act
        sut.Clear();

        // Assert
        sut.ValueB.Should().Be(Calculator.DefaultValue);
    }
}