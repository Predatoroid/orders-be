using Orders.Abstractions;
using Orders.Models;

namespace Orders.Services;

public class PromotionDiscount : IDiscountStrategy
{
    private const decimal DiscountPercentage = 0.1m;
    
    public DiscountResult Apply(Order order)
    {
        var discountAmount = order.Price with { Value = -order.Price.Value * DiscountPercentage };
        return new DiscountResult("Promotion Discount", discountAmount);
    }
}