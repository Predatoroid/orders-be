using FlexERP.Orders.Models;

namespace FlexERP.Orders.Services.Abstractions;

public interface ICurrencyService
{
    Money ConvertToEuro(Money money);
}