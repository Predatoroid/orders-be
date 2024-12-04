using FlexERP.Orders.Models;
using FlexERP.Orders.Services.Abstractions;
using FlexERP.Shared.Enums;

namespace FlexERP.Orders.Services;

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