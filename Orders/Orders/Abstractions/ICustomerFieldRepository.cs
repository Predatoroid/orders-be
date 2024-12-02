using Orders.Enums;

namespace Orders.Abstractions;

public interface ICustomerFieldRepository
{
    Task<int> CreateCustomerFieldAsync(FieldTypeEnum fieldTypeId, string description);
}