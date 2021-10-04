using System;
using Core.Domain.Campaign;
using Core.Domain.Entity;

namespace Core.Domain.Order
{
    public class Order : AggregateRoot
    {
        public string ProductCode { get; private set; }

        public decimal Price { get; private set; }
        public decimal? DiscountedPrice { get; private set; }

        public int Quantity { get; private set; }

        public Guid? CampaignId { get; private set; }


        public Result SetCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return Result.Fail(OrderError.CodeCannotBeEmpty);
            ProductCode = code;

            return Result.Ok();
        }

        public Result SetPrice(decimal price)
        {
            if (price <= 0)
                return Result.Fail(OrderError.PriceShouldBeGreaterThanOrEqualToZero);
            Price = price;

            return Result.Ok();
        }

        public Result SetQuantity(int quantity)
        {
            if (quantity <= 0)
                return Result.Fail(OrderError.QuantityShouldBeGreaterThanOrEqualToZero);
            Quantity = quantity;

            return Result.Ok();
        }

        public Result ApplyCampaign(Guid campaignId, decimal? discountedPrice)
        {
            CampaignId = campaignId;
            AddDomainEvent(new CampaignApplied(Id, campaignId));

            if (discountedPrice <= 0)
                return Result.Fail(OrderError.QuantityShouldBeGreaterThanOrEqualToZero);
            DiscountedPrice = discountedPrice;

            return Result.Ok();
        }

        public void Create()
        {
            AddDomainEvent(new OrderCreated(Id));
        }
    }
}