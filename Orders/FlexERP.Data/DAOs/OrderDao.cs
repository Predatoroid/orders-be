namespace FlexERP.Data.DAOs;

public record OrderDao()
{
    public int Id { get; set; }
    public short Currency { get; set; }
    public decimal Value { get; set; }
}