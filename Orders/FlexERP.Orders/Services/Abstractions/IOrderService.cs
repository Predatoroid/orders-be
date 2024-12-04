using FlexERP.Orders.ViewModels;
using FlexERP.Shared.Abstractions;
using FlexERP.Shared.Models;

namespace FlexERP.Orders.Services.Abstractions;

public interface IOrderService : IService
{
    Task<ServiceResult<OrderVm>> GetOrderWithDiscounts(int orderId);
}