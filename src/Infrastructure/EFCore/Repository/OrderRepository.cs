using Core.Domain.Order;

namespace Infrastructure.EFCore.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}