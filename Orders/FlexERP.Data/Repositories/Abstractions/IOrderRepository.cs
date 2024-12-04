using FlexERP.Data.DAOs;
using FlexERP.Shared.Abstractions;

namespace FlexERP.Data.Repositories.Abstractions;

public interface IOrderRepository : IRepository
{
    Task<OrderDao> GetOrderAsync(int id);
}