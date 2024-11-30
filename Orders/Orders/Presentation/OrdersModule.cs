using Orders.Abstractions;
using Orders.Enums;
using Orders.Models;
using Orders.ViewModels;
using Serilog;

namespace Orders.Presentation;

public static class OrdersModule
{
    public static void AddOrdersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/discounts/{orderId}", async (int orderId, IDiscountService discountService, ICurrencyService currencyService) =>
            {
                Log.Information("Processing order {OrderId}", orderId);

                // Simulate fetching order
                var order = new Order
                {
                    Id = orderId,
                    Price = new Money(CurrencyEnum.EUR, 340m)
                };
        
                // Convert to EUR
                var originalPrice = new Money(order.Price.Currency, order.Price.Value);
                var priceInEuro = currencyService.ConvertToEuro(originalPrice);

                Log.Information("Converted {OriginalPrice} to {PriceInEuro}", originalPrice, priceInEuro);

                // Apply discounts
                var discounts = discountService.ApplyDiscounts(order).ToList();
        
                // Calculate final price
                var totalDiscount = discounts.Sum(d => d.Amount.Value);
                var finalPrice = new Money(CurrencyEnum.EUR, priceInEuro.Value + totalDiscount);
        
                var orderVm = new OrderVm(
                    order.Id,
                    new MoneyVm(order.Price.Currency.ToString(), order.Price.Value),
                    finalPrice.ToVm(),
                    discounts.Select(x => x.ToVm()).ToList()
                );

                Log.Information("Discounts applied for order {OrderId}", orderId);

                return Results.Ok(orderVm);
            })
            .WithName("GetDiscounts");
    } 
}