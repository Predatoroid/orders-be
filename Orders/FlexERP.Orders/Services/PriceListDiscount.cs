using FlexERP.Orders.Models;
using FlexERP.Orders.Services.Abstractions;

namespace FlexERP.Orders.Services;

public class PriceListDiscount : IDiscountStrategy
{
    private const decimal DiscountPercentage = 0.05m;

    public int Order => 1;

    public DiscountResult Apply(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        var discountAmount = order.Price with { Value = -order.Price.Value * DiscountPercentage };
        return new DiscountResult("Price List Discount", discountAmount);
    }
}