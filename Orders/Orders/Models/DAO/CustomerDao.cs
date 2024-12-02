namespace Orders.Models.DAO;

public record CustomerDao
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}