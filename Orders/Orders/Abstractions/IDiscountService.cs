using Orders.Models;

namespace Orders.Abstractions;

public interface IDiscountService : IService
{
    IEnumerable<DiscountResult> ApplyDiscounts(Order order);
}