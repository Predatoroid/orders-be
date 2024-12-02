namespace Orders.Models.DAO;

public record FieldOption
{
    public int Id { get; set; }
    public int CustomerFieldId { get; set; }
    public string OptionValue { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}