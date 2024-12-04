using FlexERP.Orders.Models;
using FlexERP.Shared.Abstractions;

namespace FlexERP.Orders.Services.Abstractions;

public interface IDiscountStrategy : IService
{
    int Order { get; }
    DiscountResult Apply(Order order);
}