using System.Threading.Tasks;
using Core.Domain.Product;
using Core.WorkTime;

namespace Core.Domain.Campaign
{
    public class CampaignDomainService : ICampaignDomainService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IProductRepository _productRepository;
        private readonly IWorkTime _workTime;

        public CampaignDomainService(ICampaignRepository campaignRepository, IWorkTime workTime, IProductRepository productRepository)
        {
            _campaignRepository = campaignRepository;
            _workTime = workTime;
            _productRepository = productRepository;
        }

        public async Task<Result<Campaign>> Add(string name, string productCode, int duration, int priceManipulationLimit,
            int targetSalesCount)
        {
            var campaign = new Campaign { ProductCode = productCode, PriceManipulationLimit = priceManipulationLimit };
            var result = Result.Ok(campaign);

            var product = await _productRepository.GetAndThrow(x => x.Code == productCode);

            var hasActiveSameCampaign =
                await _campaignRepository.AnyAsync(CampaignSpecifications.ActiveCampaign(productCode, _workTime.Now()));
            if (hasActiveSameCampaign)
                result.AddError(CampaignError.IsAlreadyExist);
            
            result
                .Combine(campaign.SetName(name))
                .Combine(campaign.SetDuration(duration))
                .Combine(campaign.SetTargetSalesCount(targetSalesCount, product.Stock));


            if (result.IsSuccess)
                _campaignRepository.Add(campaign);

            return result;
        }
    }
}