using Orders.Enums;

namespace Orders.Models.DAO;

public record CustomerFieldDao
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Description { get; set; }
    public FieldTypeEnum FieldType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}