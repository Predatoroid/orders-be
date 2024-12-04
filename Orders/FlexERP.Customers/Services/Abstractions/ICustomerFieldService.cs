using FlexERP.Customers.Models;
using FlexERP.Shared.Abstractions;
using FlexERP.Shared.Enums;
using FlexERP.Shared.Models;

namespace FlexERP.Customers.Services.Abstractions;

public interface ICustomerFieldService : IService
{
    Task<ServiceResult<int>> CreateCustomerFieldAsync(int customerId, FieldTypeEnum fieldTypeId, string description);
    Task<ServiceResult<CustomerField>> GetCustomerFieldAsync(int customerFieldId);
    Task<ServiceResult<int>> CreateCustomerFieldOptionAsync(int customerFieldId, string optionValue);
    Task<ServiceResult<int>> CreateCustomerFieldValueAsync(int customerFieldId, int fieldOptionId);
    Task<ServiceResult<bool>> UpdateCustomerFieldAsync(int fieldId, string description);
    Task<ServiceResult<CustomerFieldOption>> UpdateCustomerFieldOptionAsync(int fieldOptionId, string optionValue);
}