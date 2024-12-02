using Orders.Abstractions;
using Orders.Enums;
using Orders.Models;
using Serilog;

namespace Orders.Services;

public class CustomerFieldService : ICustomerFieldService
{
    private readonly ICustomerFieldRepository _customerFieldRepository;
    
    public CustomerFieldService(ICustomerFieldRepository customerFieldRepository)
    {
        _customerFieldRepository = customerFieldRepository;
    }
    
    public async Task<int> CreateCustomerFieldAsync(FieldTypeEnum fieldTypeId, string description)
    {
        Log.Information("Creating customer field with type {FieldTypeId} and {Description}", fieldTypeId, description);

        int? customerFieldId;
        try
        {
            customerFieldId = await _customerFieldRepository.CreateCustomerFieldAsync(fieldTypeId, description);
        }
        catch (Exception _)
        {
            Log.Error("Couldn't create customer field with {FieldTypeId} and {Description}", fieldTypeId, description);
            throw;
        }

        if (!customerFieldId.HasValue)
        {
            throw new Exception($"Couldn't create customer field with type:{fieldTypeId} and description:{description}");
        }

        Log.Information("Created customer field with id {CustomerFieldId}", customerFieldId.Value);

        return customerFieldId.Value;
    }
}