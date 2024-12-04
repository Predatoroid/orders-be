namespace FlexERP.Customers.Models;

public record CustomerFieldValue
{
    public int Id { get; set; }
    public int CustomerFieldId { get; set; }
    public int FieldOptionId { get; set; }
    public DateTime CreatedAt { get; set; }
}