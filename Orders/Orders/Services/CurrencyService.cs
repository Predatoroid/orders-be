using Orders.Abstractions;
using Orders.Enums;
using Orders.Models;

namespace Orders.Services;

public class CurrencyService : ICurrencyService
{
    private readonly Dictionary<CurrencyEnum, decimal> _exchangeRates = new()
    {
        { CurrencyEnum.USD, 0.92m },
        { CurrencyEnum.GBP, 1.16m },
        { CurrencyEnum.EUR, 1.0m }
    };

    public Money ConvertToEuro(Money money)
    {
        if (!_exchangeRates.TryGetValue(money.Currency, out var value))
        {
            throw new InvalidOperationException($"{nameof(ConvertToEuro)}: Unsupported currency: {money.Currency}");
        }

        return new Money(CurrencyEnum.EUR, money.Value * value);
    }
}