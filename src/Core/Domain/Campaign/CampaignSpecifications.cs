using System;
using System.Linq.Expressions;

namespace Core.Domain.Campaign
{
    public static class CampaignSpecifications
    {
        public static Expression<Func<Campaign, bool>> ActiveCampaign(string code, DateTime currentDateTime)
        {
            return x => x.ProductCode == code &&
                        x.BeginDate <= currentDateTime && x.EndDate >= currentDateTime &&
                        x.SalesCount < x.TargetSalesCount;
        }
    }
}