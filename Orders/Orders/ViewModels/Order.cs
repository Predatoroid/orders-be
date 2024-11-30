using Orders.Enums;
using Orders.Models;

namespace Orders.ViewModels;

public record OrderVm(int Id, MoneyVm OriginalPrice, MoneyVm FinalPrice, IEnumerable<DiscountResultVm> Discounts);