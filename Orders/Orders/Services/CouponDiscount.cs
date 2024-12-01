using Orders.Abstractions;
using Orders.Enums;
using Orders.Models;

namespace Orders.Services;

public class CouponDiscount : IDiscountStrategy
{
    private static readonly Money DiscountPrice = new Money(CurrencyEnum.EUR, -10m);

    public int Order => 3; 

    public DiscountResult Apply(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);
        
        return new DiscountResult("Coupon Discount", DiscountPrice);
    }
}