using System;

namespace Application.Dtos
{
    public class CampaignDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string ProductCode { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public int PriceManipulationLimit { get; set; }

        public int TargetSalesCount { get; set; }
        public int SalesCount { get; set; }

        public decimal AverageProductPrice { get; set; }
    }
}