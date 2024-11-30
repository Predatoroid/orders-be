using System.Data;
using Orders.Models;

namespace Orders.Repositories;

public class OrderRepository
{
    // private readonly IDbConnection _dbConnection;
    //
    // public OrderRepository(IDbConnection dbConnection)
    // {
    //     _dbConnection = dbConnection;
    // }
    //
    // public Order GetOrderById(int orderId)
    // {
    //     string query = "SELECT * FROM Orders WHERE Id = @Id";
    //     return _dbConnection.QuerySingleOrDefault<Order>(query, new { Id = orderId });
    // }
}