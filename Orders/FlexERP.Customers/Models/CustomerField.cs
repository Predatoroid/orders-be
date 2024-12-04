using FlexERP.Shared.Enums;

namespace FlexERP.Customers.Models;

public record CustomerField
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public FieldTypeEnum FieldTypeId { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}