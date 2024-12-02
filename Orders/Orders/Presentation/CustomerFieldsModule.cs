using Orders.Abstractions;
using Orders.Enums;

namespace Orders.Presentation;

public static class CustomerFieldsModule
{
    private const string TagModule = "CustomerFields";

    public static void AddCustomerFieldsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/customers/{customerId}/fields", CreateCustomerField)
            .WithName("CreateCustomerField")
            .WithTags(TagModule);

        app.MapGet("/api/customers/{customerId}/fields/{fieldId}", GetCustomerField)
            .WithName("GetCustomerField")
            .WithTags(TagModule);
        
        app.MapPost("/api/customers/{customerId}/fields/{fieldId}/options", CreateCustomerFieldOption)
            .WithName("CreateCustomerFieldOption")
            .WithTags(TagModule);
        
        app.MapPost("/api/customers/{customerId}/fields/{fieldId}/options/{optionId}/values", CreateCustomerFieldValue)
            .WithName("CreateCustomerFieldValue")
            .WithTags(TagModule);
        
        app.MapPatch("/api/customers/{customerId}/fields/{fieldId}", UpdateCustomerField)
            .WithName("UpdateCustomerField")
            .WithTags(TagModule);
        
        app.MapPatch("/api/customers/{customerId}/fields/{fieldId}/options/{optionId}", UpdateCustomerFieldOption)
            .WithName("UpdateCustomerFieldOption")
            .WithTags(TagModule);
    }

    private static async Task<IResult> CreateCustomerField(int customerId, FieldTypeEnum fieldTypeId, string description, ICustomerFieldService customerFieldService)
    {
        var customerFieldIdResult = await customerFieldService.CreateCustomerFieldAsync(customerId, fieldTypeId, description);
        return customerFieldIdResult.Success ? Results.Created("/api/customers/{customerId}/fields", customerFieldIdResult.Value) : Results.Conflict();
    }

    private static async Task<IResult> GetCustomerField(int id, ICustomerFieldService customerFieldService)
    {
        var customerFieldResult = await customerFieldService.GetCustomerFieldAsync(id);
        return customerFieldResult.Success ? Results.Ok(customerFieldResult.Value) : Results.NotFound();
    }
    
    private static async Task<IResult> CreateCustomerFieldOption(int fieldId, string optionValue, ICustomerFieldService customerFieldService)
    {
        var customerFieldOptionIdResult = await customerFieldService.CreateCustomerFieldOptionAsync(fieldId, optionValue);
        return customerFieldOptionIdResult.Success ? Results.Created("/api/customers/{customerId}/fields/{fieldId}/options", customerFieldOptionIdResult.Value) : Results.Conflict();
    }
    
    private static async Task<IResult> CreateCustomerFieldValue(int fieldId, int fieldOptionId, ICustomerFieldService customerFieldService)
    {
        var customerFieldValueIdResult = await customerFieldService.CreateCustomerFieldValueAsync(fieldId, fieldOptionId);
        return customerFieldValueIdResult.Success ? Results.Created("/api/customers/{customerId}/fields/{fieldId}/options/{optionId}/values", customerFieldValueIdResult.Value) : Results.Conflict();
    }
    
    private static async Task<IResult> UpdateCustomerField(int fieldId, string description, ICustomerFieldService customerFieldService)
    {
        var customerFieldResult = await customerFieldService.UpdateCustomerFieldAsync(fieldId, description);
        return customerFieldResult.Success ? Results.NoContent() : Results.UnprocessableEntity();
    }
    
    private static async Task<IResult> UpdateCustomerFieldOption(int fieldOptionId, string optionValue, ICustomerFieldService customerFieldService)
    {
        var customerFieldValueIdResult = await customerFieldService.UpdateCustomerFieldOptionAsync(fieldOptionId, optionValue);
        return customerFieldValueIdResult.Success ? Results.NoContent() : Results.UnprocessableEntity();
    }
}