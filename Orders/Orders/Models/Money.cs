using Orders.Enums;
using Orders.ViewModels;

namespace Orders.Models;

public record Money(CurrencyEnum Currency, decimal Value)
{
    public MoneyVm ToVm() => new MoneyVm(Currency.ToString(), Value);
    
    public override string ToString() => $"{Value}:{Currency}";

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
            throw new ArgumentException("Cannot multiply with different currencies");
        }

        if (decimal.Equals(b.Value, decimal.Zero))
        {
            throw new DivideByZeroException();
        }
        
        return a with { Value = a.Value / b.Value };    
    }
}