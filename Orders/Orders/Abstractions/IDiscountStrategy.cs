using Orders.Models;

namespace Orders.Abstractions;

public interface IDiscountStrategy : IService
{
    int Order { get; }
    DiscountResult Apply(Order order);
}