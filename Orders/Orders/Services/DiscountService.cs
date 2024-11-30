using Orders.Abstractions;
using Orders.Models;

namespace Orders.Services;

public class DiscountService : IDiscountService
{
    private readonly IEnumerable<IDiscountStrategy> _strategies = new List<IDiscountStrategy>
    {
        new PriceListDiscount(),
        new PromotionDiscount(),
        new CouponDiscount()
    };

    public IEnumerable<DiscountResult> ApplyDiscounts(Order order)
    {
        var results = new List<DiscountResult>();
        var currentOrder = order;
        
        foreach (var strategy in _strategies)
        {
            var discountResult = strategy.Apply(currentOrder);
            currentOrder = currentOrder with { Price = currentOrder.Price + discountResult.Amount };
            
            results.Add(discountResult);
        }
        
        return results;
    }
}