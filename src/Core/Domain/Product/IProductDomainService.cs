using System.Threading.Tasks;
using Core.Domain.Product.Dtos;

namespace Core.Domain.Product
{
    public interface IProductDomainService
    {
        Task<ProductOverviewDto> GetOverview(string code);
        
        Task<Result<Product>> Add(string code, decimal price, int stock);
    }
}