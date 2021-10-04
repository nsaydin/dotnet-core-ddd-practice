using Core.Domain.Campaign;
using Core.Domain.Product;
using Xunit;

namespace Core.Tests.Domain
{
    public class CampaignTests
    {
        [Fact]
        public void should_success_when_set_campaign_name()
        {
            // given 
            const string name = "campaign";
            var campaign = new Campaign();

            // when
            var result = campaign.SetName(name);

            // then
            Assert.True(result.IsSuccess);
            Assert.Equal(name, campaign.Name);
        }

        [Fact]
        public void should_fail_when_set_campaign_name_with_empty()
        {
            // given 
            const string code = "";
            var product = new Product();

            // when
            var result = product.SetCode(code);

            // then
            Assert.False(result.IsSuccess);
            Assert.Contains(result.Errors, x => x == ProductError.CodeCannotBeEmpty);
        }
        
        [Fact]
        public void should_success_when_campaign_calculate_discount_percentage()
        {
            // given 
            const string name = "campaign";
            var campaign = new Campaign
            {
                PriceManipulationLimit = 30,
                TargetSalesCount = 10,
                SalesCount = 5
            };

            // when
            var percentage = campaign.DiscountPercentage();

            // then
            Assert.Equal(15, percentage);
        }
        
        [Fact]
        public void should_success_when_campaign_calculate_discount_percentage_scenario()
        {
            // given 
            const string name = "campaign";
            var campaign = new Campaign
            {
                PriceManipulationLimit = 30,
                TargetSalesCount = 10,
                SalesCount = 2
            };

            // when
            var percentage = campaign.DiscountPercentage();

            // then
            Assert.Equal(24, percentage);
        }

    }
}