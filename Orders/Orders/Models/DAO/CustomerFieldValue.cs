namespace Orders.Models.DAO;

public record CustomerFieldValue
{
    public int Id { get; set; }
    public int CustomerFieldId { get; set; }
    public int? FieldOptionId { get; set; }
    public string FieldValue { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}