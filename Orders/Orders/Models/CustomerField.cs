using Orders.Enums;

namespace Orders.Models;

public record CustomerField
{
    public int Id { get; set; }
    public string Name { get; set; }
    public FieldTypeEnum FieldType { get; set; }
}