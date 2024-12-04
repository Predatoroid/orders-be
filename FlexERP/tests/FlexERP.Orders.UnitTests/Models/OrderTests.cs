using FlexERP.Orders.Models;
using FlexERP.Shared.Enums;
using FluentAssertions;

namespace FlexERP.Orders.UnitTests.Models;

public class OrderTests
{
    [Fact]
    public void Constructor_ShouldSetProperties_WhenValidValuesProvided()
    {
        // Arrange
        var id = 1;
        var price = new Money(CurrencyEnum.USD, 100m);

        // Act
        var order = new Order(id, price);

        // Assert
        order.Id.Should().Be(id);
        order.Price.Should().Be(price);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenPriceIsDefault()
    {
        // Arrange
        var id = 1;
        var defaultPrice = default(Money);

        // Act
        Action act = () => new Order(id, defaultPrice);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Price cannot be empty for order: 1");
    }

    [Fact]
    public void PropertyPriceGetter_ShouldReturnPrice_WhenValidOrder()
    {
        // Arrange
        var order = new Order(3, new Money(CurrencyEnum.EUR, 50m));

        // Act
        var price = order.Price;

        // Assert
        price.Should().BeEquivalentTo(new Money(CurrencyEnum.EUR, 50m));
    }
}