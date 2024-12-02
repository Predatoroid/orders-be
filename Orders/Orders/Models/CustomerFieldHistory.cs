using Orders.Enums;

namespace Orders.Models;

public record CustomerFieldHistory
{
    public int Id { get; set; }
    public int CustomFieldId { get; set; }
    public EntityTypeEnum EntityTypeId { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public DateTime CreatedAt { get; set; }
}