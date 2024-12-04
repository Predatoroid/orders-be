using FlexERP.Orders.ViewModels;

namespace FlexERP.Orders.Models;

public record DiscountResult(string Name, Money Amount)
{
    public DiscountResultVm ToVm() => new(Name, Amount.ToVm());
}