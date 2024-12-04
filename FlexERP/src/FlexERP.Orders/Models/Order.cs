using FlexERP.Data.DAOs;
using FlexERP.Shared.Enums;

namespace FlexERP.Orders.Models;

public record Order
{
    public int Id { get; }
    private readonly Money _price;

    public Money Price
    {
        get
        {
            if (_price.Equals(default))
            {
                throw new ArgumentException($"Price cannot be empty for order: {Id}");
            }
            return _price;
        }
        init => _price = value;
    }
    
    public Order(int id, Money price)
    {
        Id = id;
        _price = price.Equals(default)
            ? throw new ArgumentException($"Price cannot be empty for order: {id}")
            : price;
    }

    public static Order FromDao(OrderDao dao) => new(dao.Id, new Money((CurrencyEnum)dao.Currency, dao.Value));
}