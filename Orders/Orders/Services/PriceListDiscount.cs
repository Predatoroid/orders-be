using Orders.Abstractions;
using Orders.Models;

namespace Orders.Services;

public class PriceListDiscount : IDiscountStrategy
{
    private const decimal DiscountPercentage = 0.05m;

    public DiscountResult Apply(Order order)
    {
        var discountAmount = order.Price with { Value = -order.Price.Value * DiscountPercentage };
        return new DiscountResult("Price List Discount", discountAmount);
    }
}