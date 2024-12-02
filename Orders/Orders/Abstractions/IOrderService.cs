using Orders.Models;
using Orders.ViewModels;

namespace Orders.Abstractions;

public interface IOrderService : IService
{
    Task<ServiceResult<OrderVm>> GetOrderWithDiscounts(int orderId);
}