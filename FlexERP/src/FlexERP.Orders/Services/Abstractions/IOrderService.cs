using FlexERP.Orders.ViewModels;
using FlexERP.Shared.Models;

namespace FlexERP.Orders.Services.Abstractions;

public interface IOrderService
{
    Task<ServiceResult<OrderVm>> GetOrderWithDiscounts(int orderId);
}