using Orders.Models;

namespace Orders.Abstractions;

public interface IDiscountStrategy
{
    DiscountResult Apply(Order order);
}