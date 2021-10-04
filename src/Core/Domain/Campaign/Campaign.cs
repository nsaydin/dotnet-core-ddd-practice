using System;
using Core.Domain.Entity;

namespace Core.Domain.Campaign
{
    public class Campaign : AggregateRoot
    {
        public string Name { get; set; }

        public string ProductCode { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public int PriceManipulationLimit { get; set; }

        public int TargetSalesCount { get; set; }

        public int SalesCount { get; set; }

        public Result SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Fail(CampaignError.NameCannotBeEmpty);
            Name = name;

            return Result.Ok();
        }

        public Result SetDuration(int duration)
        {
            if (duration <= 0)
                return Result.Fail(CampaignError.DurationShouldBeGreaterThanZero);

            BeginDate = DateTime.Now.Date;
            EndDate = BeginDate.AddHours(duration);

            return Result.Ok();
        }

        public Result SetTargetSalesCount(int targetSalesCount, int stock)
        {
            if (targetSalesCount > stock)
                return Result.Fail(CampaignError.TargetSalesCountShouldBeLessThanProductStock);

            TargetSalesCount = targetSalesCount;

            return Result.Ok();
        }

        public decimal DiscountPercentage()
        {
            if (PriceManipulationLimit <= 0)
                return 0;

            var salesPercentage = SalesCount * 100 / TargetSalesCount;
            var applyPercentage = PriceManipulationLimit * (100 - salesPercentage) / 100;

            return applyPercentage;
        }
    }
}