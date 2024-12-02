namespace Orders.Models;

public record CustomerFieldOption()
{
    public int Id { get; set; }
    public int CustomFieldId { get; set; }
    public string OptionValue { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}