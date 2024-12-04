using FlexERP.Customers.Services.Abstractions;
using FlexERP.Shared.Enums;
using FlexERP.WebApi.Modules.Customers.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FlexERP.WebApi.Modules.Customers.Endpoints;

public static class CustomersEndpoints
{
    public static void MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/customers");

        group.MapPost("/", CreateCustomer)
            .WithName("CreateCustomer")
            .Produces(201)
            .Produces(400);

        group.MapGet("/{customerId:int}", GetCustomer)
            .WithName("GetCustomer")
            .Produces(200)
            .Produces(404);
    }
    
    public static void MapCustomerFieldsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/customers/{customerId:int}/fields")
            .WithTags("CustomerFields");

        group.MapPost("/", CreateCustomerField)
            .WithName("CreateCustomerField");

        group.MapGet("/{fieldId:int}", GetCustomerField)
            .WithName("GetCustomerField");

        group.MapPost("/{fieldId:int}/options", CreateCustomerFieldOption)
            .WithName("CreateCustomerFieldOption");

        group.MapPost("/{fieldId:int}/options/{optionId}/values", CreateCustomerFieldValue)
            .WithName("CreateCustomerFieldValue");

        group.MapPatch("/{fieldId:int}", UpdateCustomerField)
            .WithName("UpdateCustomerField");

        group.MapPatch("/{fieldId:int}/options/{optionId}", UpdateCustomerFieldOption)
            .WithName("UpdateCustomerFieldOption");
    }

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customerDto">The customer details</param>
    /// <param name="customerService">The customer service instance.</param>
    /// <returns>The created customer.</returns>
    private static async Task<IResult> CreateCustomer(CustomerDto customerDto, [FromServices] ICustomerService customerService)
    {
        var customerIdResult = await customerService.CreateCustomerAsync(customerDto?.Name ?? string.Empty);
        return customerIdResult.Success ? Results.Created("/api/customers", customerIdResult.Value) : Results.Conflict();
    }

    /// <summary>
    /// Retrieves a customer by ID.
    /// </summary>
    /// <param name="id">The customer's ID.</param>
    /// <param name="customerService">The customer service instance.</param>
    /// <returns>The customer details.</returns>
    private static async Task<IResult> GetCustomer(int id, [FromServices] ICustomerService customerService)
    {
        var customerResult = await customerService.GetCustomerAsync(id);
        return customerResult.Success ? Results.Ok(customerResult.Value) : Results.NotFound();
    }

    private static async Task<IResult> CreateCustomerField(int customerId, FieldTypeEnum fieldTypeId, string description, [FromServices] ICustomerFieldService customerFieldService)
    {
        var customerFieldIdResult = await customerFieldService.CreateCustomerFieldAsync(customerId, fieldTypeId, description);
        return customerFieldIdResult.Success ? Results.Created("/api/customers/{customerId}/fields", customerFieldIdResult.Value) : Results.Conflict();
    }

    private static async Task<IResult> GetCustomerField(int id, [FromServices] ICustomerFieldService customerFieldService)
    {
        var customerFieldResult = await customerFieldService.GetCustomerFieldAsync(id);
        return customerFieldResult.Success ? Results.Ok(customerFieldResult.Value) : Results.NotFound();
    }
    
    private static async Task<IResult> CreateCustomerFieldOption(int fieldId, string optionValue, [FromServices] ICustomerFieldService customerFieldService)
    {
        var customerFieldOptionIdResult = await customerFieldService.CreateCustomerFieldOptionAsync(fieldId, optionValue);
        return customerFieldOptionIdResult.Success ? Results.Created("/api/customers/{customerId}/fields/{fieldId}/options", customerFieldOptionIdResult.Value) : Results.Conflict();
    }
    
    private static async Task<IResult> CreateCustomerFieldValue(int fieldId, int fieldOptionId, [FromServices] ICustomerFieldService customerFieldService)
    {
        var customerFieldValueIdResult = await customerFieldService.CreateCustomerFieldValueAsync(fieldId, fieldOptionId);
        return customerFieldValueIdResult.Success ? Results.Created("/api/customers/{customerId}/fields/{fieldId}/options/{optionId}/values", customerFieldValueIdResult.Value) : Results.Conflict();
    }
    
    private static async Task<IResult> UpdateCustomerField(int fieldId, string description, [FromServices] ICustomerFieldService customerFieldService)
    {
        var customerFieldResult = await customerFieldService.UpdateCustomerFieldAsync(fieldId, description);
        return customerFieldResult.Success ? Results.NoContent() : Results.UnprocessableEntity();
    }
    
    private static async Task<IResult> UpdateCustomerFieldOption(int fieldOptionId, string optionValue, [FromServices] ICustomerFieldService customerFieldService)
    {
        var customerFieldValueIdResult = await customerFieldService.UpdateCustomerFieldOptionAsync(fieldOptionId, optionValue);
        return customerFieldValueIdResult.Success ? Results.NoContent() : Results.UnprocessableEntity();
    }
}