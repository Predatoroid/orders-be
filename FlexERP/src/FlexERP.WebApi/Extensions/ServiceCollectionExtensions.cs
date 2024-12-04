using FlexERP.Customers.Services;
using FlexERP.Customers.Services.Abstractions;
using FlexERP.Data.Repositories;
using FlexERP.Data.Repositories.Abstractions;
using FlexERP.Orders.Services;
using FlexERP.Orders.Services.Abstractions;
using FlexERP.WebApi.Repositories;

namespace FlexERP.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<IDiscountService, DiscountService>();
        services.AddScoped<IDiscountStrategy, PriceListDiscount>();
        services.AddScoped<IDiscountStrategy, PromotionDiscount>();
        services.AddScoped<IDiscountStrategy, CouponDiscount>();
        
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ICustomerFieldService, CustomerFieldService>();
        
        return services;
    }
    
    public static IServiceCollection RegisterSingletons(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerFieldRepository, CustomerFieldRepository>();
        
        return services;
    }
}