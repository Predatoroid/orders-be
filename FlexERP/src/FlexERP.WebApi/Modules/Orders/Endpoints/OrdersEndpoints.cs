using FlexERP.Orders.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FlexERP.WebApi.Modules.Orders.Endpoints;

public static class OrdersEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/orders")
            .WithTags("Orders");

        group.MapGet("/{orderId:int}", GetOrder)
            .WithName("GetOrder")
            .Produces(200)
            .Produces(404);
    }
    
    private static async Task<IResult> GetOrder(int orderId, [FromServices] IOrderService orderService)
    {
        var orderResult = await orderService.GetOrderWithDiscounts(orderId);
        return orderResult.Success ? Results.Ok(orderResult.Value) : Results.NotFound();
    }
}