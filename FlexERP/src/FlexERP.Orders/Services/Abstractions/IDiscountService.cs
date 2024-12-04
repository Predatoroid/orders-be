using FlexERP.Orders.Models;

namespace FlexERP.Orders.Services.Abstractions;

public interface IDiscountService
{
    IEnumerable<DiscountResult> ApplyDiscounts(Order order);
}