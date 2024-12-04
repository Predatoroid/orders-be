using FlexERP.WebApi.Modules.Customers.Endpoints;
using FlexERP.WebApi.Modules.Orders.Endpoints;

namespace FlexERP.WebApi.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void MapModuleEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapOrderEndpoints();
        app.MapCustomerEndpoints();
        app.MapCustomerFieldsEndpoints();
    }
}