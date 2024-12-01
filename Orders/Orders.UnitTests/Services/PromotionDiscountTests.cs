using FluentAssertions;
using Orders.Enums;
using Orders.Models;
using Orders.Services;

namespace Orders.UnitTests.Services;

public class PromotionDiscountTests
{
    [Fact]
    public void Apply_ShouldReturnCorrectDiscount_WhenOrderIsValid()
    {
        // Arrange
        var order = new Order(1, new Money(CurrencyEnum.USD, 200m));  // Assuming Order constructor takes (id, money)
        var promotionDiscount = new PromotionDiscount();

        // Act
        var result = promotionDiscount.Apply(order);

        // Assert
        result.Should().NotBeNull();
        result.Amount.Currency.Should().Be(CurrencyEnum.USD);
        result.Amount.Value.Should().Be(-20m);  // 200 * 0.1 = 20, so the discount is -20
        result.Name.Should().Be("Promotion Discount");
    }

    [Fact]
    public void Apply_ShouldThrowArgumentNullException_WhenOrderIsNull()
    {
        // Arrange
        var promotionDiscount = new PromotionDiscount();

        // Act
        Action act = () => promotionDiscount.Apply(null);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'order')");
    }

    [Fact]
    public void Apply_ShouldReturnDiscountAmountAsNegative_WhenOrderHasPositivePrice()
    {
        // Arrange
        var order = new Order(2, new Money(CurrencyEnum.GBP, 150m));  // GBP as currency
        var promotionDiscount = new PromotionDiscount();

        // Act
        var result = promotionDiscount.Apply(order);

        // Assert
        result.Amount.Value.Should().Be(-15m);  // 150 * 0.1 = 15, so the discount is -15
    }

    [Fact]
    public void Apply_ShouldReturnZeroDiscount_WhenOrderHasZeroPrice()
    {
        // Arrange
        var order = new Order(3, new Money(CurrencyEnum.USD, 0m));  // Price is 0
        var promotionDiscount = new PromotionDiscount();

        // Act
        var result = promotionDiscount.Apply(order);

        // Assert
        result.Amount.Value.Should().Be(0m);  // No discount if price is zero
    }

    [Fact]
    public void Apply_ShouldReturnNegativeDiscount_WhenOrderHasNegativePrice()
    {
        // Arrange
        var order = new Order(4, new Money(CurrencyEnum.EUR, -100m));  // Negative price
        var promotionDiscount = new PromotionDiscount();

        // Act
        var result = promotionDiscount.Apply(order);

        // Assert
        result.Amount.Value.Should().Be(10m);  // -100 * 0.1 = -10, so the discount is 10 (positive)
    }
}