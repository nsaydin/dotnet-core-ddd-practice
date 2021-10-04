using System;
using System.Threading.Tasks;
using Core.Domain.Campaign;
using Core.Domain.Order;
using Core.Domain.Product;
using Core.Domain.Product.Dtos;
using Core.Tests.Extensions;
using Core.WorkTime;
using Moq;
using Xunit;

namespace Core.Tests.DomainService
{
    public class OrderDomainServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Mock<ICampaignRepository> _campaignRepository;
        private readonly Mock<IProductDomainService> _productDomainService;
        private readonly OrderDomainService _domainService;

        public OrderDomainServiceTests()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _productDomainService = new Mock<IProductDomainService>();
            _campaignRepository = new Mock<ICampaignRepository>();
            var workTime = new Mock<IWorkTime>();

            _domainService = new OrderDomainService(_orderRepository.Object, _productDomainService.Object, _campaignRepository.Object,
                workTime.Object);
        }

        [Fact]
        public async Task should_success_when_add_order()
        {
            // given
            const string productCode = "apple";
            const int quantity = 1;

            var product = new ProductOverviewDto { Code = productCode, Stock = 100, Price = 100 };

            _productDomainService.Setup(x => x.GetOverview(productCode)).ReturnsAsync(product);
            _campaignRepository.Setup(m => m.Get(MyIt.IsAnyExp<Campaign>())).ReturnsAsync((Campaign)null);

            // when
            var result = await _domainService.Add(productCode, quantity);

            // then
            Assert.True(result.IsSuccess);
            Assert.Equal(product.Price, result.Value.Price);
            Assert.Equal(product.DiscountedPrice, result.Value.DiscountedPrice);
            Assert.Equal(productCode, result.Value.ProductCode);
            Assert.Equal(quantity, result.Value.Quantity);
            Assert.Contains(result.Value.DomainEvents, x => x.GetType() == typeof(OrderCreated));

            _orderRepository.Verify(x => x.Add(result.Value));
        }

        [Fact]
        public async Task should_fail_when_add_order_with_quantity_greater_than_stock()
        {
            // given
            const string productCode = "apple";
            const int quantity = 20;

            var product = new ProductOverviewDto { Code = productCode, Stock = 10 };

            _productDomainService.Setup(x => x.GetOverview(productCode)).ReturnsAsync(product);
            _campaignRepository.Setup(m => m.Get(MyIt.IsAnyExp<Campaign>())).ReturnsAsync((Campaign)null);

            // when
            var result = await _domainService.Add(productCode, quantity);

            // then
            Assert.False(result.IsSuccess);
            Assert.Contains(result.Errors, x => x == OrderError.NotEnoughStock);

            _orderRepository.Verify(x => x.Add(result.Value), Times.Never);
        }

        [Fact]
        public async Task should_fail_when_add_order_with_no_stock()
        {
            // given
            const string productCode = "apple";
            const int quantity = 1;

            var product = new ProductOverviewDto { Code = productCode, Stock = 0 };

            _productDomainService.Setup(x => x.GetOverview(productCode)).ReturnsAsync(product);
            _campaignRepository.Setup(m => m.Get(MyIt.IsAnyExp<Campaign>())).ReturnsAsync((Campaign)null);

            // when
            var result = await _domainService.Add(productCode, quantity);

            // then
            Assert.False(result.IsSuccess);
            Assert.Contains(result.Errors, x => x == OrderError.NotEnoughStock);

            _orderRepository.Verify(x => x.Add(result.Value), Times.Never);
        }

        [Fact]
        public async Task should_success_when_add_order_with_active_campaign()
        {
            // given
            const string productCode = "apple";
            const int quantity = 1;

            var product = new ProductOverviewDto { Code = productCode, Stock = 100, Price = 100 };
            var campaign = new Campaign
            {
                Id = Guid.NewGuid(),
                PriceManipulationLimit = 30,
                SalesCount = 0,
                TargetSalesCount = 10
            };

            _productDomainService.Setup(x => x.GetOverview(productCode)).ReturnsAsync(product);
            _campaignRepository.Setup(m => m.Get(MyIt.IsAnyExp<Campaign>())).ReturnsAsync(campaign);

            // when
            var result = await _domainService.Add(productCode, quantity);

            // then
            Assert.True(result.IsSuccess);
            Assert.Equal(productCode, result.Value.ProductCode);
            Assert.Equal(quantity, result.Value.Quantity);
            Assert.Equal(campaign.Id, result.Value.CampaignId);
            Assert.Contains(result.Value.DomainEvents, x => x.GetType() == typeof(OrderCreated));
            Assert.Contains(result.Value.DomainEvents, x => x.GetType() == typeof(CampaignApplied));

            _orderRepository.Verify(x => x.Add(result.Value));
        }
    }
}