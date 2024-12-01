namespace Orders.Models;

public record CustomerFieldOption()
{
    public int Id { get; set; }
    public int CustomFieldId { get; set; }
    public string Value { get; set; }
}