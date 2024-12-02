using Orders.Models;

namespace Orders.Abstractions;

public interface IOrderRepository
{
    Task<Order> GetOrderAsync(int id);
}