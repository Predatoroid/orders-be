using FlexERP.Customers.Models;
using FlexERP.Customers.Services.Abstractions;
using FlexERP.Data.Repositories.Abstractions;
using FlexERP.Shared.Enums;
using FlexERP.Shared.Models;
using FlexERP.WebApi.Enums;
using Serilog;

namespace FlexERP.Customers.Services;

public class CustomerFieldService : ICustomerFieldService
{
    private readonly ICustomerFieldRepository _customerFieldRepository;
    
    public CustomerFieldService(ICustomerFieldRepository customerFieldRepository)
    {
        _customerFieldRepository = customerFieldRepository;
    }
    
    public async Task<ServiceResult<int>> CreateCustomerFieldAsync(int customerId, FieldTypeEnum fieldTypeId, string description)
    {
        Log.Information("Creating customer field with type {FieldTypeId} and {Description}", fieldTypeId, description);
        
        int customerFieldId;
        try
        {
            customerFieldId = await _customerFieldRepository.CreateCustomerFieldAsync(customerId, fieldTypeId, description);
        }
        catch (Exception)
        {
            Log.Error("Couldn't create customer field with {FieldTypeId} and {Description}", fieldTypeId, description);
            return new ServiceResult<int>(ServiceErrorCode.GenericError);
        }

        Log.Information("Created customer field with id {CustomerFieldId}", customerFieldId);

        return new ServiceResult<int>(customerFieldId);
    }

    public async Task<ServiceResult<CustomerField>> GetCustomerFieldAsync(int customerFieldId)
    {
        Log.Information("Getting customer field with id {Id}", customerFieldId);

        CustomerField customerField;
        try
        {
            var customerFieldDao = await _customerFieldRepository.GetCustomerFieldAsync(customerFieldId);
            customerField = CustomerField.FromDao(customerFieldDao);
        }
        catch (Exception)
        {
            Log.Warning("Couldn't get customer field with {Id}", customerFieldId);
            return new ServiceResult<CustomerField>(ServiceErrorCode.GenericError);
        }

        return new ServiceResult<CustomerField>(customerField);
    }
    
    public async Task<ServiceResult<IEnumerable<CustomerFieldHistory>>> GetCustomerFieldHistoryAsync(int customerFieldId)
    {
        Log.Information("Getting customer field history with id {Id}", customerFieldId);

        IEnumerable<CustomerFieldHistory> customerFieldHistoryRecords;
        try
        {
            var customerFieldHistoryRecordsDao = await _customerFieldRepository.GetCustomerFieldHistoryAsync(customerFieldId);
            customerFieldHistoryRecords = customerFieldHistoryRecordsDao.Select(CustomerFieldHistory.FromDao).ToList();
        }
        catch (Exception)
        {
            Log.Warning("Couldn't get customer field with {Id}", customerFieldId);
            return new ServiceResult<IEnumerable<CustomerFieldHistory>>(ServiceErrorCode.GenericError);
        }

        return new ServiceResult<IEnumerable<CustomerFieldHistory>>(customerFieldHistoryRecords);
    }
    
    public async Task<ServiceResult<int>> CreateCustomerFieldOptionAsync(int customerFieldId, string optionValue)
    {
        int customerFieldOptionId;
        try
        {
            //TODO: add validation for textbox to prevent adding more than one record
            customerFieldOptionId = await _customerFieldRepository.CreateCustomerFieldOptionAsync(customerFieldId, optionValue);
        }
        catch (Exception)
        {
            Log.Error("Couldn't create customer field option with {CustomerFieldId} and {OptionValue}", customerFieldId, optionValue);
            return new ServiceResult<int>(ServiceErrorCode.GenericError);
        }

        return new ServiceResult<int>(customerFieldOptionId);
    }
    
    public async Task<ServiceResult<int>> CreateCustomerFieldValueAsync(int customerFieldId, int fieldOptionId)
    {
        int customerFieldValueId;
        try
        {
            customerFieldValueId = await _customerFieldRepository.CreateCustomerFieldValueAsync(customerFieldId, fieldOptionId);
        }
        catch (Exception)
        {
            Log.Error("Couldn't create customer field value with {CustomerFieldId} and {FieldOptionId}", customerFieldId, fieldOptionId);
            return new ServiceResult<int>(ServiceErrorCode.GenericError);
        }

        return new ServiceResult<int>(customerFieldValueId);
    }

    public async Task<ServiceResult<bool>> UpdateCustomerFieldAsync(int fieldId, string description)
    {
        try
        {
            var previousCustomerField = await _customerFieldRepository.GetCustomerFieldAsync(fieldId);
            await _customerFieldRepository.UpdateCustomerFieldAsync(previousCustomerField.Id, description);
            
            await _customerFieldRepository.CreateCustomerFieldHistoryAsync(previousCustomerField.Id, EntityTypeEnum.CustomerFields, previousCustomerField.Description ?? string.Empty, description);
        }
        catch (Exception)
        {
            Log.Error("Couldn't update customer field value with {CustomerFieldId} and {Description}", fieldId, description);
            return new ServiceResult<bool>(ServiceErrorCode.GenericError);
        }
        
        return new ServiceResult<bool>(true);
    }

    public Task<ServiceResult<CustomerFieldOption>> UpdateCustomerFieldOptionAsync(int fieldOptionId, string optionValue)
    {
        //TODO: Same implementation with UpdateCustomerFieldAsync
        throw new NotImplementedException();
    }
}