using FlexERP.Orders.Models;
using FlexERP.Shared.Abstractions;

namespace FlexERP.Orders.Services.Abstractions;

public interface IDiscountService : IService
{
    IEnumerable<DiscountResult> ApplyDiscounts(Order order);
}