using Orders.Abstractions;
using Orders.Models;

namespace Orders.Services;

public class PromotionDiscount : IDiscountStrategy
{
    private const decimal DiscountPercentage = 0.1m;

    public int Order => 2;

    public DiscountResult Apply(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);
        
        var discountAmount = order.Price with { Value = -order.Price.Value * DiscountPercentage };
        return new DiscountResult("Promotion Discount", discountAmount);
    }
}