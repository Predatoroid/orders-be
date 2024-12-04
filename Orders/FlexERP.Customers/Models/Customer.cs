namespace FlexERP.Customers.Models;

public record Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}