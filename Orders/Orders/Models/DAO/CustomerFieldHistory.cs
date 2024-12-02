namespace Orders.Models.DAO;

public record CustomerFieldHistory
{
    public int Id { get; set; }
    public int CustomerFieldId { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public DateTime CreatedAt { get; set; }
}