using FlexERP.Orders.Models;
using FlexERP.Orders.Services;
using FlexERP.Shared.Enums;
using FluentAssertions;

namespace FlexERP.Orders.UnitTests.Services;

public class CouponDiscountTests
{
    [Fact]
    public void Apply_ShouldReturnCorrectDiscountResult_WithValidOrder()
    {
        // Arrange
        var couponDiscount = new CouponDiscount();
        var order = new Order(1, new Money(CurrencyEnum.EUR, 100m));

        // Act
        var result = couponDiscount.Apply(order);

        // Assert
        result.Should().BeEquivalentTo(
            new DiscountResult("Coupon Discount", new Money(CurrencyEnum.EUR, -10m))
        );
    }

    [Fact]
    public void Apply_ShouldThrowArgumentNullExceptionWithNullOrder()
    {
        // Arrange
        var couponDiscount = new CouponDiscount();

        // Act
        var action = () => couponDiscount.Apply(null!);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }
}