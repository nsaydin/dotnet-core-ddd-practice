using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Core.Domain.Campaign;
using Core.Domain.Order;
using MediatR;

namespace Application.Queries
{
    public class GetCampaignByNameQueryHandler : IRequestHandler<GetCampaignByNameQuery, CampaignDto>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IOrderRepository _orderRepository;

        public GetCampaignByNameQueryHandler(ICampaignRepository campaignRepository, IOrderRepository orderRepository)
        {
            _campaignRepository = campaignRepository;
            _orderRepository = orderRepository;
        }

        public async Task<CampaignDto> Handle(GetCampaignByNameQuery query, CancellationToken cancellationToken)
        {
            var campaignDto = await _campaignRepository.GetAndThrow<CampaignDto>(x => x.Name == query.Name);

            var campaignOrders = await _orderRepository.GetList<OrderDto>(x => x.CampaignId == campaignDto.Id);
            campaignDto.AverageProductPrice = campaignOrders.Sum(y => y.DiscountedPrice * y.Quantity) / campaignOrders.Sum(x => x.Quantity);

            return campaignDto;
        }
    }
}