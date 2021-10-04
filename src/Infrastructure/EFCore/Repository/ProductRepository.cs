using Core.Domain.Product;

namespace Infrastructure.EFCore.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}