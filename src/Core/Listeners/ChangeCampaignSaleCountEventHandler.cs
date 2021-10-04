using System.Threading.Tasks;
using Core.Domain.Campaign;
using Core.Domain.Order;
using Core.Event;

namespace Core.Listeners
{
    public class CampaignAppliedEventHandler : IEventHandler<CampaignApplied>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CampaignAppliedEventHandler(IUnitOfWork unitOfWork, ICampaignRepository campaignRepository, IOrderRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _campaignRepository = campaignRepository;
            _orderRepository = orderRepository;
        }

        public async Task Handle(IEventContext<CampaignApplied> context)
        {
            var domainEvent = context.Message;

            var order = await _orderRepository.GetAndThrow(domainEvent.OrderId);
            var campaign = await _campaignRepository.GetAndThrow(domainEvent.CampaignId);

            campaign.SalesCount += order.Quantity;
            _campaignRepository.Update(campaign);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}