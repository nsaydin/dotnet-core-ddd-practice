using System.Threading.Tasks;

namespace Core.Domain.Campaign
{
    public interface ICampaignDomainService
    {
        Task<Result<Campaign>> Add(string name, string productCode, int duration, int priceManipulationLimit, int targetSalesCount);
    }
}