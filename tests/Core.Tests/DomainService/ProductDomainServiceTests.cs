using System.Threading.Tasks;
using Core.Domain.Campaign;
using Core.Domain.Product;
using Core.Domain.Product.Dtos;
using Core.Tests.Extensions;
using Core.WorkTime;
using Moq;
using Xunit;

namespace Core.Tests.DomainService
{
    public class ProductDomainServiceTests
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<ICampaignRepository> _campaignRepository;
        private readonly ProductDomainService _domainService;

        public ProductDomainServiceTests()
        {
            _productRepository = new Mock<IProductRepository>();
            _campaignRepository = new Mock<ICampaignRepository>();
            var workTime = new Mock<IWorkTime>();

            _domainService = new ProductDomainService(_productRepository.Object, _campaignRepository.Object, workTime.Object);
        }

        [Fact]
        public async Task should_success_when_add_product()
        {
            // given
            const string code = "apple";
            const int price = 250;
            const int stock = 10;

            _productRepository.Setup(m => m.AnyAsync(MyIt.IsAnyExp<Product>())).ReturnsAsync(false);

            // when
            var result = await _domainService.Add(code, price, stock);

            // then
            Assert.True(result.IsSuccess);
            Assert.Equal(code, result.Value.Code);
            Assert.Equal(price, result.Value.Price);
            Assert.Equal(stock, result.Value.Stock);

            _productRepository.Verify(x => x.Add(result.Value));
        }

        [Fact]
        public async Task should_fail_when_add_product_with_exist_code()
        {
            // given
            const string code = "apple";
            const int price = 250;
            const int stock = 10;

            _productRepository.Setup(m => m.AnyAsync(MyIt.IsAnyExp<Product>())).ReturnsAsync(true);

            // when
            var result = await _domainService.Add(code, price, stock);

            // then
            Assert.False(result.IsSuccess);
            Assert.Contains(result.Errors, x => x == ProductError.CodeIsAlreadyExist);

            _productRepository.Verify(x => x.Add(result.Value), Times.Never);
        }

        [Fact]
        public async Task should_get_when_get_product_overview_without_campaign()
        {
            // given
            const string code = "apple";
            var product = new ProductOverviewDto { Price = 100, DiscountedPrice = 100 };

            _productRepository.Setup(x => x.GetAndThrow<ProductOverviewDto>(MyIt.IsAnyExp<Product>())).ReturnsAsync(product);
            _campaignRepository.Setup(x => x.Get(MyIt.IsAnyExp<Campaign>())).ReturnsAsync((Campaign)null);

            // when
            var productOverviewDto = await _domainService.GetOverview(code);

            // then
            Assert.Equal(productOverviewDto.Price, product.Price);
            Assert.Equal(productOverviewDto.Price, product.DiscountedPrice);
        }

        [Fact]
        public async Task should_get_when_get_product_overview_with_campaign()
        {
            // given
            const string code = "apple";
            var product = new ProductOverviewDto { Price = 100, DiscountedPrice = 100 };
            var campaign = new Campaign
            {
                PriceManipulationLimit = 30,
                SalesCount = 0,
                TargetSalesCount = 10
            };

            _productRepository.Setup(x => x.GetAndThrow<ProductOverviewDto>(MyIt.IsAnyExp<Product>())).ReturnsAsync(product);
            _campaignRepository.Setup(x => x.Get(MyIt.IsAnyExp<Campaign>())).ReturnsAsync(campaign);

            // when
            var productOverviewDto = await _domainService.GetOverview(code);

            // then
            Assert.Equal(productOverviewDto.Price, product.Price);
            Assert.Equal(70, productOverviewDto.DiscountedPrice);
        }
    }
}