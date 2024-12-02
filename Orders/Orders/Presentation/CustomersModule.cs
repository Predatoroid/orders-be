using Orders.Abstractions;

namespace Orders.Presentation;

public static class CustomersModule
{
    private const string TagModule = "Customers";

    public static void AddCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/customers", CreateCustomer)
            .WithName("CreateCustomer")
            .WithTags(TagModule);

        app.MapGet("/api/customers", GetCustomer)
            .WithName("GetCustomer")
            .WithTags(TagModule);
    }
    
    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="name">The name of the customer.</param>
    /// <param name="customerService">The customer service instance.</param>
    /// <returns>The created customer.</returns>
    private static async Task<IResult> CreateCustomer(string name, ICustomerService customerService)
    {
        var customerIdResult = await customerService.CreateCustomerAsync(name);
        return customerIdResult.Success ? Results.Created("/api/customers", customerIdResult.Value) : Results.Conflict();
    }

    /// <summary>
    /// Retrieves a customer by ID.
    /// </summary>
    /// <param name="id">The customer's ID.</param>
    /// <param name="customerService">The customer service instance.</param>
    /// <returns>The customer details.</returns>
    private static async Task<IResult> GetCustomer(int id, ICustomerService customerService)
    {
        var customerResult = await customerService.GetCustomerAsync(id);
        return customerResult.Success ? Results.Ok(customerResult.Value) : Results.NotFound();
    }
}