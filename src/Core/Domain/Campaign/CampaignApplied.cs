using System;
using Core.Event;

namespace Core.Domain.Campaign
{
    public class CampaignApplied : IEvent
    {
        public Guid OrderId { get; set; }
        public Guid CampaignId { get; set; }

        public CampaignApplied()
        {
        }

        public CampaignApplied(Guid orderId, Guid campaignId)
        {
            OrderId = orderId;
            CampaignId = campaignId;
        }
    }
}