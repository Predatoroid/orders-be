using Orders.Abstractions;

namespace Orders.Presentation;

public static class OrdersModule
{
    private const string TagModule = "Orders";
    
    public static void AddOrdersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/orders/{orderId}/discounts", GetOrderWithDiscounts)
            .WithName("GetDiscounts")
            .WithTags(TagModule);
    }

    private static async Task<IResult> GetOrderWithDiscounts(int orderId, IOrderService orderService)
    {
        var orderResult = await orderService.GetOrderWithDiscounts(orderId);
        return orderResult.Success ? Results.Ok(orderResult.Value) : Results.NotFound();
    }
}