using System.Threading.Tasks;
using Core.Domain.Campaign;
using Core.Domain.Product.Dtos;
using Core.WorkTime;

namespace Core.Domain.Product
{
    public class ProductDomainService : IProductDomainService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IWorkTime _workTime;

        public ProductDomainService(IProductRepository productRepository, ICampaignRepository campaignRepository, IWorkTime workTime)
        {
            _productRepository = productRepository;
            _campaignRepository = campaignRepository;
            _workTime = workTime;
        }

        public async Task<ProductOverviewDto> GetOverview(string code)
        {
            var productDto = await _productRepository.GetAndThrow<ProductOverviewDto>(x => x.Code == code);

            var campaign = await _campaignRepository.Get(CampaignSpecifications.ActiveCampaign(code, _workTime.Now()));
            if (campaign != null)
                productDto.DiscountedPrice = productDto.Price - (productDto.Price * campaign.DiscountPercentage() / 100);

            return productDto;
        }

        public async Task<Result<Product>> Add(string code, decimal price, int stock)
        {
            var product = new Product();
            var result = Result.Ok(product);

            result
                .Combine(product.SetCode(code))
                .Combine(product.SetPrice(price))
                .Combine(product.SetStock(stock));

            var isExist = await _productRepository.AnyAsync(x => x.Code == code);
            if (isExist)
                result.AddError(ProductError.CodeIsAlreadyExist);

            if (result.IsSuccess)
                _productRepository.Add(product);

            return result;
        }
    }
}