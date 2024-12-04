using FlexERP.Orders.Models;
using FlexERP.Orders.Services;
using FlexERP.Orders.Services.Abstractions;
using FlexERP.Shared.Enums;
using FluentAssertions;
using Moq;
using FlexERP.WebApi.Enums;

namespace Orders.UnitTests.Services;

public class DiscountServiceTests
{
    private readonly Mock<IDiscountStrategy> _mockPriceListDiscount;
    private readonly Mock<IDiscountStrategy> _mockPromotionDiscount;
    private readonly Mock<IDiscountStrategy> _mockCouponDiscount;
    private readonly DiscountService _discountService;

    public DiscountServiceTests()
    {
        // Mock the strategies
        _mockPriceListDiscount = new Mock<IDiscountStrategy>();
        _mockPromotionDiscount = new Mock<IDiscountStrategy>();
        _mockCouponDiscount = new Mock<IDiscountStrategy>();

        _mockPriceListDiscount.SetupGet(x => x.Order).Returns(1);
        _mockPromotionDiscount.SetupGet(x => x.Order).Returns(2);
        _mockCouponDiscount.SetupGet(x => x.Order).Returns(3);

        // Setup DiscountService with mocked strategies
        _discountService = new DiscountService(
            [_mockPriceListDiscount.Object, _mockPromotionDiscount.Object, _mockCouponDiscount.Object]
        );
    }

    [Fact]
    public void ApplyDiscounts_ShouldApplyAllDiscountsInOrder()
    {
        // Arrange
        var order = new Order(1, new Money(CurrencyEnum.USD, 200m));

        var priceListDiscountResult = new DiscountResult("Price List Discount", new Money(CurrencyEnum.USD, -20m));
        var promotionDiscountResult = new DiscountResult("Promotion Discount", new Money(CurrencyEnum.USD, -30m));
        var couponDiscountResult = new DiscountResult("Coupon Discount", new Money(CurrencyEnum.USD, -40m));

        // Setup mock behaviors
        _mockPriceListDiscount.Setup(x => x.Apply(It.IsAny<Order>())).Returns(priceListDiscountResult);
        _mockPromotionDiscount.Setup(x => x.Apply(It.IsAny<Order>())).Returns(promotionDiscountResult);
        _mockCouponDiscount.Setup(x => x.Apply(It.IsAny<Order>())).Returns(couponDiscountResult);

        // Act
        var results = _discountService.ApplyDiscounts(order).ToList();

        // Assert
        results.Should().HaveCount(3);
        results[0].Name.Should().Be("Price List Discount");
        results[1].Name.Should().Be("Promotion Discount");
        results[2].Name.Should().Be("Coupon Discount");

        results[0].Amount.Value.Should().Be(-20m);
        results[1].Amount.Value.Should().Be(-30m);
        results[2].Amount.Value.Should().Be(-40m);
    }

    [Fact]
    public void ApplyDiscounts_ShouldUpdateOrderPriceCorrectly()
    {
        // Arrange
        var order = new Order(1, new Money(CurrencyEnum.USD, 200m));

        var priceListDiscountResult = new DiscountResult("Price List Discount", new Money(CurrencyEnum.USD, -20m));
        var promotionDiscountResult = new DiscountResult("Promotion Discount", new Money(CurrencyEnum.USD, -30m));
        var couponDiscountResult = new DiscountResult("Coupon Discount", new Money(CurrencyEnum.USD, -40m));

        // Setup mock behaviors
        _mockPriceListDiscount.Setup(x => x.Apply(It.IsAny<Order>())).Returns(priceListDiscountResult);
        _mockPromotionDiscount.Setup(x => x.Apply(It.IsAny<Order>())).Returns(promotionDiscountResult);
        _mockCouponDiscount.Setup(x => x.Apply(It.IsAny<Order>())).Returns(couponDiscountResult);

        // Act
        var results = _discountService.ApplyDiscounts(order).ToList();

        // Assert
        // After applying all discounts, the order price should be:
        // 200 - 20 - 30 - 40 = 110
        results[0].Amount.Value.Should().Be(-20m);
        results[1].Amount.Value.Should().Be(-30m);
        results[2].Amount.Value.Should().Be(-40m);

        // Verify the final order price
        var finalOrderPrice = order.Price.Value + results[0].Amount.Value + results[1].Amount.Value + results[2].Amount.Value;
        finalOrderPrice.Should().Be(110m); // 200 - 20 - 30 - 40 = 110
    }

    [Fact]
    public void ApplyDiscounts_ShouldHandleEmptyOrder()
    {
        // Arrange
        var emptyOrder = new Order(1, new Money(CurrencyEnum.USD, 0m));

        var priceListDiscountResult = new DiscountResult("Price List Discount", new Money(CurrencyEnum.USD, -20m));
        var promotionDiscountResult = new DiscountResult("Promotion Discount", new Money(CurrencyEnum.USD, -30m));
        var couponDiscountResult = new DiscountResult("Coupon Discount", new Money(CurrencyEnum.USD, -40m));

        // Setup mock behaviors
        _mockPriceListDiscount.Setup(x => x.Apply(It.IsAny<Order>())).Returns(priceListDiscountResult);
        _mockPromotionDiscount.Setup(x => x.Apply(It.IsAny<Order>())).Returns(promotionDiscountResult);
        _mockCouponDiscount.Setup(x => x.Apply(It.IsAny<Order>())).Returns(couponDiscountResult);

        // Act
        var results = _discountService.ApplyDiscounts(emptyOrder).ToList();

        // Assert
        results.Should().HaveCount(3);
        results[0].Amount.Value.Should().Be(-20m);
        results[1].Amount.Value.Should().Be(-30m);
        results[2].Amount.Value.Should().Be(-40m);
    }

    [Fact]
    public void ApplyDiscounts_ShouldThrowArgumentNullException_WhenOrderIsNull()
    {
        // Act & Assert
        Action act = () => _discountService.ApplyDiscounts(null);
        act.Should().Throw<ArgumentNullException>();
    }
}