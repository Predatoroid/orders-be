using FlexERP.Shared.Abstractions;
using FlexERP.Shared.Enums;
using FlexERP.WebApi.Enums;

namespace FlexERP.Data.Repositories.Abstractions;

public interface ICustomerFieldRepository : IRepository
{
    Task<int> CreateCustomerFieldAsync(int customerId, FieldTypeEnum fieldTypeId, string description);
    Task<CustomerField> GetCustomerFieldAsync(int id);
    Task<int> CreateCustomerFieldOptionAsync(int customerFieldId, string optionValue);
    Task<int> CreateCustomerFieldValueAsync(int customerFieldId, int fieldOptionId);
    Task UpdateCustomerFieldAsync(int customerFieldId, string description);
    Task UpdateCustomerFieldOptionAsync(int fieldOptionId, string optionValue);
    Task<int> CreateCustomerFieldHistoryAsync(int entityId, EntityTypeEnum entityTypeId, string oldValue, string newValue);
}