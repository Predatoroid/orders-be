using Orders.Abstractions;
using Orders.Enums;

namespace Orders.Presentation;

public static class CustomerFieldsModule
{
    private const string TagModule = "CustomerFields";

    public static void AddCustomerFieldsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/customers/{customerId}/fields/{fieldTypeId}", CreateCustomerField)
            .WithName("CreateCustomerField")
            .WithTags(TagModule);

        // app.MapGet("/api/customers", GetCustomer)
        //     .WithName("GetCustomer")
        //     .WithTags(TagModule);
    }

    private static async Task<IResult> CreateCustomerField(FieldTypeEnum fieldTypeId, string description, ICustomerFieldService customerFieldService)
    {
        return Results.Ok(await customerFieldService.CreateCustomerFieldAsync(fieldTypeId, description));
    }

    // /// <summary>
    // /// Retrieves a customer by ID.
    // /// </summary>
    // /// <param name="id">The customer's ID.</param>
    // /// <param name="customerService">The customer service instance.</param>
    // /// <returns>The customer details.</returns>
    // private static async Task<IResult> GetCustomer(int id, ICustomerService customerService)
    // {
    //     return Results.Ok(await customerService.GetCustomerAsync(id));
    // }
}