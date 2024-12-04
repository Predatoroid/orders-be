using FlexERP.Data.DAOs;

namespace FlexERP.Data.Repositories.Abstractions;

public interface IOrderRepository
{
    Task<OrderDao> GetOrderAsync(int id);
}