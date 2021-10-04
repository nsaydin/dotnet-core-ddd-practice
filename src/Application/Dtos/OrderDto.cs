using System;

namespace Application.Dtos
{
    public class OrderDto
    {
        public string ProductCode { get; set; }

        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }

        public int Quantity { get; set; }

        public Guid? CampaignId { get; set; }
    }
}