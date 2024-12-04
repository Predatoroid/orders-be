using FlexERP.Orders.ViewModels;
using FlexERP.Shared.Enums;

namespace FlexERP.Orders.Models;

public readonly record struct Money
{
    private readonly CurrencyEnum _currency;

    public CurrencyEnum Currency
    {
        get
        {
            if (!Enum.IsDefined(_currency))
            {
                throw new ArgumentOutOfRangeException(_currency.ToString());
            }
            return _currency;
        }
    }
    
    public decimal Value { get; init; }
    
    public Money(CurrencyEnum currency, decimal value)
    {
        Value = value;
        _currency = Enum.IsDefined(currency)
            ? currency
            : throw new ArgumentOutOfRangeException(currency.ToString());
    }
    
    public MoneyVm ToVm() => new(Currency.ToString(), Value);
    
    public override string ToString() => $"{Currency}:{Value}";

    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency)
        {
            throw new ArgumentException("Cannot add with different currencies");
        }
        
        return a with { Value = a.Value + b.Value };    
    }
    
    public static Money operator -(Money a, Money b)
    {
        if (a.Currency != b.Currency)
        {
            throw new ArgumentException("Cannot subtract with different currencies");
        }
        
        return a with { Value = a.Value - b.Value };    
    }
    
    public static Money operator *(Money a, Money b)
    {
        if (a.Currency != b.Currency)
        {
            throw new ArgumentException("Cannot multiply with different currencies");
        }
        
        return a with { Value = a.Value * b.Value };    
    }
    
    public static Money operator /(Money a, Money b)
    {
        if (a.Currency != b.Currency)
        {
            throw new ArgumentException("Cannot divide with different currencies");
        }

        if (decimal.Equals(b.Value, decimal.Zero))
        {
            throw new DivideByZeroException();
        }
        
        return a with { Value = a.Value / b.Value };    
    }
}