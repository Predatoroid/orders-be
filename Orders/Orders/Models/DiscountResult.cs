using Orders.ViewModels;

namespace Orders.Models;

public record DiscountResult(string Name, Money Amount)
{
    public DiscountResultVm ToVm() => new(Name, Amount.ToVm());
}