namespace Orders.Models;

public record CustomerFieldHistory()
{
    public int Id { get; set; }
    public int CustomFieldValueId { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public DateTime Timestamp { get; set; }
}