using FlexERP.Orders.Models;
using FlexERP.Orders.Services;
using FlexERP.Shared.Enums;
using FluentAssertions;
using FlexERP.WebApi.Enums;

namespace Orders.UnitTests.Services;

public class PriceListDiscountTests
{
    [Fact]
    public void Apply_ShouldReturnCorrectDiscountResult_WithValidOrder()
    {
        // Arrange
        var price = new Money(CurrencyEnum.EUR, 100m);
        var order = new Order(1, price);
        var priceListDiscount = new PriceListDiscount();

        // Act
        var result = priceListDiscount.Apply(order);

        // Assert
        result.Should().BeEquivalentTo(
            new DiscountResult("Price List Discount", new Money(CurrencyEnum.EUR, -5m))
        );
    }

    [Fact]
    public void Apply_ShouldHandleOrderWithZeroPrice()
    {
        // Arrange
        var price = new Money(CurrencyEnum.EUR, 0m);
        var order = new Order(1, price);
        var priceListDiscount = new PriceListDiscount();

        // Act
        var result = priceListDiscount.Apply(order);

        // Assert
        result.Should().BeEquivalentTo(
            new DiscountResult("Price List Discount", new Money(CurrencyEnum.EUR, 0m))
        );
    }

    [Fact]
    public void Apply_ShouldHandleOrderWithNegativePrice()
    {
        // Arrange
        var price = new Money(CurrencyEnum.EUR, -50m);
        var order = new Order(1, price);
        var priceListDiscount = new PriceListDiscount();

        // Act
        var result = priceListDiscount.Apply(order);

        // Assert
        result.Should().BeEquivalentTo(
            new DiscountResult("Price List Discount", new Money(CurrencyEnum.EUR, 2.5m))
        );
    }

    [Fact]
    public void Apply_ShouldThrowArgumentNullExceptionWithNullOrder()
    {
        // Arrange
        var priceListDiscount = new PriceListDiscount();

        // Act
        var action = () => priceListDiscount.Apply(null);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }
}