using FlexERP.Orders.Models;
using FlexERP.Orders.ViewModels;
using FlexERP.Shared.Enums;
using FluentAssertions;

namespace Orders.UnitTests.Models;

public class DiscountResultTests
{
    [Fact]
    public void ToVm_ShouldReturnCorrectViewModel()
    {
        // Arrange
        var money = new Money(CurrencyEnum.USD, 100);
        var discountResult = new DiscountResult("Seasonal Discount", money);

        // Act
        var vm = discountResult.ToVm();

        // Assert
        vm.Should().BeEquivalentTo(new DiscountResultVm("Seasonal Discount", new MoneyVm("USD", 100)));
    }

    [Fact]
    public void ToVm_ShouldHandleEmptyName()
    {
        // Arrange
        var money = new Money(CurrencyEnum.EUR, 50);
        var discountResult = new DiscountResult(string.Empty, money);

        // Act
        var vm = discountResult.ToVm();

        // Assert
        vm.Should().BeEquivalentTo(new DiscountResultVm(string.Empty, new MoneyVm("EUR", 50)));
    }

    [Fact]
    public void ToVm_ShouldThrowArgumentOutOfRangeExceptionWithDefaultMoney()
    {
        // Arrange
        var discountResult = new DiscountResult("Default Test", default);

        // Act
        var action = () => discountResult.ToVm();

        // Assert
        action.Should().Throw<ArgumentOutOfRangeException>();
    }
}