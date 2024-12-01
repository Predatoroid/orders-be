namespace Orders.Models;

public record CustomerFieldValue()
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int CustomFieldId { get; set; }
    public string Value { get; set; }
}