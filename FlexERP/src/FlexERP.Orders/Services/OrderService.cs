using FlexERP.Data.Repositories.Abstractions;
using FlexERP.Orders.Models;
using FlexERP.Orders.Services.Abstractions;
using FlexERP.Orders.ViewModels;
using FlexERP.Shared.Enums;
using FlexERP.Shared.Models;
using FlexERP.WebApi.Enums;
using Serilog;

namespace FlexERP.Orders.Services;

public class OrderService : IOrderService
{
    private readonly ICurrencyService _currencyService;
    private readonly IDiscountService _discountService;
    private readonly IOrderRepository _orderRepository;
    
    public OrderService(ICurrencyService currencyService, 
        IDiscountService discountService,
        IOrderRepository orderRepository)
    {
        _currencyService = currencyService;
        _discountService = discountService;
        _orderRepository = orderRepository;
    }
    public async Task<ServiceResult<OrderVm>> GetOrderWithDiscounts(int orderId)
    {
        Log.Information("Processing order {OrderId}", orderId);
        OrderVm orderVm;
        
        try
        {
            // Simulate fetching order
            var orderDao = await _orderRepository.GetOrderAsync(orderId);
            var order = Order.FromDao(orderDao);
                
            // Convert to EUR
            var originalPrice = new Money(order.Price.Currency, order.Price.Value);
            var priceInEuro = _currencyService.ConvertToEuro(originalPrice);

            Log.Information("Converted {OriginalPrice} to {PriceInEuro}", originalPrice, priceInEuro);

            // Apply discounts
            var discounts = _discountService.ApplyDiscounts(order).ToList();
        
            // Calculate final price
            var totalDiscount = discounts.Sum(d => d.Amount.Value);
            var finalPrice = new Money(CurrencyEnum.EUR, priceInEuro.Value + totalDiscount);
        
            orderVm = new OrderVm(
                order.Id,
                new MoneyVm(order.Price.Currency.ToString(), order.Price.Value),
                finalPrice.ToVm(),
                discounts.Select(x => x.ToVm()).ToList()
            );
        }
        catch (Exception)
        {
            Log.Error("Couldn't get order with discounts of {OrderId}", orderId);
            return new ServiceResult<OrderVm>(ServiceErrorCode.GenericError);
        }
        
        Log.Information("Discounts applied for order {OrderId}", orderId);

        return new ServiceResult<OrderVm>(orderVm);
    }
}