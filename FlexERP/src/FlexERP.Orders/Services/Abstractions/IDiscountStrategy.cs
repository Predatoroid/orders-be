using FlexERP.Orders.Models;

namespace FlexERP.Orders.Services.Abstractions;

public interface IDiscountStrategy
{
    int Order { get; }
    DiscountResult Apply(Order order);
}