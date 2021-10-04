using System.Threading.Tasks;

namespace Core.Domain.Order
{
    public interface IOrderDomainService
    {
        Task<Result<Order>> Add(string productCode, int quantity);
    }
}