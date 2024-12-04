using FlexERP.Orders.Models;
using FlexERP.Shared.Abstractions;

namespace FlexERP.Orders.Services.Abstractions;

public interface ICurrencyService : IService
{
    Money ConvertToEuro(Money money);
}