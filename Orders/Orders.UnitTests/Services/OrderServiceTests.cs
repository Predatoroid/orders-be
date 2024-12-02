using FluentAssertions;
using Moq;
using Orders.Abstractions;
using Orders.Enums;
using Orders.Models;
using Orders.Services;

namespace Orders.UnitTests.Services;

public class OrderServiceTests
{
    private readonly Mock<ICurrencyService> _currencyServiceMock;
    private readonly Mock<IDiscountService> _discountServiceMock;
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _currencyServiceMock = new Mock<ICurrencyService>();
        _discountServiceMock = new Mock<IDiscountService>();
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _orderService = new OrderService(
            _currencyServiceMock.Object,
            _discountServiceMock.Object,
            _orderRepositoryMock.Object);
    }

    [Fact]
    public async Task GetOrderWithDiscounts_ShouldReturnOrderWithDiscounts_WhenOrderExists()
    {
        // Arrange
        const int orderId = 1;
        var order = new Order(orderId, new Money(CurrencyEnum.USD, 100));

        var convertedPrice = new Money(CurrencyEnum.EUR, 90);
        var discounts = new List<DiscountResult>
        {
            new DiscountResult("Promotion Discount", new Money(CurrencyEnum.EUR, -10)),
            new DiscountResult("Loyalty Discount", new Money(CurrencyEnum.EUR, -5))
        };

        _orderRepositoryMock
            .Setup(repo => repo.GetOrderAsync(orderId))
            .ReturnsAsync(order);

        _currencyServiceMock
            .Setup(service => service.ConvertToEuro(It.IsAny<Money>()))
            .Returns(convertedPrice);

        _discountServiceMock
            .Setup(service => service.ApplyDiscounts(order))
            .Returns(discounts);

        // Act
        var result = await _orderService.GetOrderWithDiscounts(orderId);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.FinalPrice.Value.Should().Be(75); // 90 - 10 - 5
        result.Value.FinalPrice.Currency.Should().Be("EUR");
        result.Value.Discounts.Should().HaveCount(2);

        _orderRepositoryMock.Verify(repo => repo.GetOrderAsync(orderId), Times.Once);
        _currencyServiceMock.Verify(service => service.ConvertToEuro(It.IsAny<Money>()), Times.Once);
        _discountServiceMock.Verify(service => service.ApplyDiscounts(order), Times.Once);
    }

    [Fact]
    public async Task GetOrderWithDiscounts_ShouldReturnError_WhenOrderDoesNotExist()
    {
        // Arrange
        const int orderId = 99;
        _orderRepositoryMock
            .Setup(repo => repo.GetOrderAsync(orderId))
            .ThrowsAsync(new Exception("Order not found"));

        // Act
        var result = await _orderService.GetOrderWithDiscounts(orderId);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Error.Should().Be(ServiceErrorCode.GenericError);

        _orderRepositoryMock.Verify(repo => repo.GetOrderAsync(orderId), Times.Once);
        _currencyServiceMock.Verify(service => service.ConvertToEuro(It.IsAny<Money>()), Times.Never);
        _discountServiceMock.Verify(service => service.ApplyDiscounts(It.IsAny<Order>()), Times.Never);
    }
}
