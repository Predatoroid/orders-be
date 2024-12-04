using FlexERP.Orders.Models;
using FlexERP.Orders.ViewModels;
using FlexERP.Shared.Enums;
using FluentAssertions;

namespace FlexERP.Orders.UnitTests.Models;

public class MoneyTests
{
    [Fact]
    public void Constructor_ShouldInitializeCorrectly_WhenValidCurrencyAndValueProvided()
    {
        // Arrange
        var validCurrency = CurrencyEnum.USD;
        var validValue = 100m;

        // Act
        var money = new Money(validCurrency, validValue);

        // Assert
        money.Currency.Should().Be(validCurrency);
        money.Value.Should().Be(validValue);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentOutOfRangeException_WhenCurrencyIsInvalid()
    {
        // Arrange
        var invalidCurrency = (CurrencyEnum)999;
        var validValue = 50m;

        // Act
        Action act = () => new Money(invalidCurrency, validValue);

        // Assert
        act.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("*999*");
    }

    [Fact]
    public void Property_Currency_ShouldThrowArgumentOutOfRangeException_WhenAccessedWithInvalidCurrency()
    {
        // Arrange
        var money = new Money
        {
            Value = 5
        };

        // Act
        Action act = () => _ = money.Currency;

        // Assert
        act.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("*0*");
    }

    [Fact]
    public void Property_Value_ShouldBeSetCorrectly_WhenValidValueIsProvided()
    {
        // Arrange
        var currency = CurrencyEnum.EUR;
        var value = 200m;

        // Act
        var money = new Money(currency, value);

        // Assert
        money.Value.Should().Be(value);
    }

    [Fact]
    public void Constructor_ShouldAllowNegativeValues()
    {
        // Arrange
        var currency = CurrencyEnum.GBP;
        var negativeValue = -50m;

        // Act
        var money = new Money(currency, negativeValue);

        // Assert
        money.Currency.Should().Be(currency);
        money.Value.Should().Be(negativeValue);
    }
    
    [Fact]
    public void ToVm_ShouldReturnCorrectViewModel()
    {
        // Arrange
        var money = new Money(CurrencyEnum.USD, 100);

        // Act
        var vm = money.ToVm();

        // Assert
        vm.Should().BeEquivalentTo(new MoneyVm("USD", 100));
    }

    [Fact]
    public void ToString_ShouldReturnFormattedString()
    {
        // Arrange
        var money = new Money(CurrencyEnum.EUR, 50);

        // Act
        var result = money.ToString();

        // Assert
        result.Should().Be("EUR:50");
    }

    [Fact]
    public void AdditionOperator_ShouldAddValues_WhenCurrenciesAreSame()
    {
        // Arrange
        var money1 = new Money(CurrencyEnum.USD, 50);
        var money2 = new Money(CurrencyEnum.USD, 30);

        // Act
        var result = money1 + money2;

        // Assert
        result.Should().Be(new Money(CurrencyEnum.USD, 80));
    }

    [Fact]
    public void AdditionOperator_ShouldThrow_WhenCurrenciesAreDifferent()
    {
        // Arrange
        var money1 = new Money(CurrencyEnum.USD, 50);
        var money2 = new Money(CurrencyEnum.EUR, 30);

        // Act
        Action act = () => _ = money1 + money2;

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Cannot add with different currencies");
    }

    [Fact]
    public void SubtractionOperator_ShouldSubtractValues_WhenCurrenciesAreSame()
    {
        // Arrange
        var money1 = new Money(CurrencyEnum.USD, 50);
        var money2 = new Money(CurrencyEnum.USD, 30);

        // Act
        var result = money1 - money2;

        // Assert
        result.Should().Be(new Money(CurrencyEnum.USD, 20));
    }

    [Fact]
    public void SubtractionOperator_ShouldThrow_WhenCurrenciesAreDifferent()
    {
        // Arrange
        var money1 = new Money(CurrencyEnum.USD, 50);
        var money2 = new Money(CurrencyEnum.EUR, 30);

        // Act
        Action act = () => _ = money1 - money2;

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Cannot subtract with different currencies");
    }

    [Fact]
    public void MultiplicationOperator_ShouldMultiplyValues_WhenCurrenciesAreSame()
    {
        // Arrange
        var money1 = new Money(CurrencyEnum.USD, 10);
        var money2 = new Money(CurrencyEnum.USD, 5);

        // Act
        var result = money1 * money2;

        // Assert
        result.Should().Be(new Money(CurrencyEnum.USD, 50));
    }

    [Fact]
    public void MultiplicationOperator_ShouldThrow_WhenCurrenciesAreDifferent()
    {
        // Arrange
        var money1 = new Money(CurrencyEnum.USD, 10);
        var money2 = new Money(CurrencyEnum.EUR, 5);

        // Act
        Action act = () => _ = money1 * money2;

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Cannot multiply with different currencies");
    }

    [Fact]
    public void DivisionOperator_ShouldDivideValues_WhenCurrenciesAreSame()
    {
        // Arrange
        var money1 = new Money(CurrencyEnum.USD, 10);
        var money2 = new Money(CurrencyEnum.USD, 2);

        // Act
        var result = money1 / money2;

        // Assert
        result.Should().Be(new Money(CurrencyEnum.USD, 5));
    }

    [Fact]
    public void DivisionOperator_ShouldThrow_WhenCurrenciesAreDifferent()
    {
        // Arrange
        var money1 = new Money(CurrencyEnum.USD, 10);
        var money2 = new Money(CurrencyEnum.EUR, 2);

        // Act
        Action act = () => _ = money1 / money2;

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Cannot divide with different currencies");
    }

    [Fact]
    public void DivisionOperator_ShouldThrow_WhenDividingByZero()
    {
        // Arrange
        var money1 = new Money(CurrencyEnum.USD, 10);
        var money2 = new Money(CurrencyEnum.USD, 0);

        // Act
        Action act = () => _ = money1 / money2;

        // Assert
        act.Should().Throw<DivideByZeroException>();
    }
}