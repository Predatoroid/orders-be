using Orders.Models;

namespace Orders.Abstractions;

public interface ICurrencyService : IService
{
    Money ConvertToEuro(Money money);
}