using Orders.Enums;

namespace Orders.Abstractions;

public interface ICustomerFieldService : IService
{
    Task<int> CreateCustomerFieldAsync(FieldTypeEnum fieldTypeId, string description);
}