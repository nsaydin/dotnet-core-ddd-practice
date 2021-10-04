using System;
using System.Threading.Tasks;
using Core.Domain.Campaign;
using Core.Domain.Product;
using Core.Tests.Extensions;
using Core.WorkTime;
using Moq;
using Xunit;

namespace Core.Tests.DomainService
{
    public class CampaignDomainServiceTests
    {
        private readonly Mock<ICampaignRepository> _campaignRepository;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly CampaignDomainService _domainService;

        public CampaignDomainServiceTests()
        {
            _productRepository = new Mock<IProductRepository>();
            _campaignRepository = new Mock<ICampaignRepository>();
            var workTime = new Mock<IWorkTime>();

            _domainService = new CampaignDomainService(_campaignRepository.Object, workTime.Object, _productRepository.Object);
        }

        [Fact]
        public async Task should_success_when_add_campaign()
        {
            // given
            const string name = "campaign";
            const string productCode = "apple";
            const int duration = 1;
            const int priceManipulationLimit = 30;
            const int targetSalesCount = 2;

            var product = new Product { };
            product.SetCode(productCode);
            product.SetStock(100);

            _productRepository.Setup(x => x.GetAndThrow(MyIt.IsAnyExp<Product>())).ReturnsAsync(product);
            _campaignRepository.Setup(m => m.AnyAsync(MyIt.IsAnyExp<Campaign>())).ReturnsAsync(false);

            // when
            var result = await _domainService.Add(name, productCode, duration, priceManipulationLimit, targetSalesCount);

            // then
            Assert.True(result.IsSuccess);
            Assert.Equal(name, result.Value.Name);
            Assert.Equal(productCode, result.Value.ProductCode);
            Assert.Equal(DateTime.Now.Date, result.Value.BeginDate);
            Assert.Equal(DateTime.Now.Date.AddHours(duration), result.Value.EndDate);
            Assert.Equal(priceManipulationLimit, result.Value.PriceManipulationLimit);
            Assert.Equal(targetSalesCount, result.Value.TargetSalesCount);

            _campaignRepository.Verify(x => x.Add(result.Value));
        }

        [Fact]
        public async Task should_fail_when_create_campaign_with_same_campaign()
        {
            // given
            const string name = "campaign";
            const string productCode = "apple";
            const int duration = 1;
            const int priceManipulationLimit = 30;
            const int targetSalesCount = 2;

            var product = new Product { };
            product.SetCode(productCode);
            product.SetStock(100);

            _productRepository.Setup(x => x.GetAndThrow(MyIt.IsAnyExp<Product>())).ReturnsAsync(product);
            _campaignRepository.Setup(m => m.AnyAsync(MyIt.IsAnyExp<Campaign>())).ReturnsAsync(true);

            // when
            var result = await _domainService.Add(name, productCode, duration, priceManipulationLimit, targetSalesCount);

            // then
            Assert.False(result.IsSuccess);
            Assert.Contains(result.Errors, x => x == CampaignError.IsAlreadyExist);

            _campaignRepository.Verify(x => x.Add(result.Value), Times.Never);
        }
    }
}