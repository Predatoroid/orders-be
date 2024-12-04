using FlexERP.Orders.Models;
using FlexERP.Orders.Services.Abstractions;

namespace FlexERP.Orders.Services;

public class DiscountService : IDiscountService
{
    private readonly IEnumerable<IDiscountStrategy> _strategies;
    
    public DiscountService(IEnumerable<IDiscountStrategy> strategies)
    {
        _strategies = strategies.OrderBy(strategy => strategy.Order);
    }

    public IEnumerable<DiscountResult> ApplyDiscounts(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);
        
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