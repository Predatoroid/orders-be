using FlexERP.Data.DAOs;

namespace FlexERP.Customers.Models;

public record Customer
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }

    public static Customer FromDao(CustomerDao customerDao) =>
        new()
        {
            Id = customerDao.Id,
            Name = customerDao.Name,
            CreatedAt = customerDao.CreatedAt
        };
}