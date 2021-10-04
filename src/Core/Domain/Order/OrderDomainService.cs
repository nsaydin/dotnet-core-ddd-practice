using System.Threading.Tasks;
using Core.Domain.Campaign;
using Core.Domain.Product;
using Core.WorkTime;

namespace Core.Domain.Order
{
    public class OrderDomainService : IOrderDomainService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductDomainService _productDomainService;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IWorkTime _workTime;

        public OrderDomainService(IOrderRepository orderRepository, IProductDomainService productDomainService,
            ICampaignRepository campaignRepository, IWorkTime workTime)
        {
            _orderRepository = orderRepository;
            _productDomainService = productDomainService;
            _campaignRepository = campaignRepository;
            _workTime = workTime;
        }

        public async Task<Result<Order>> Add(string productCode, int quantity)
        {
            var order = new Order();
            var result = Result.Ok(order);

            var product = await _productDomainService.GetOverview(productCode);

            //todo : nice to have quantity should be less than target sales count
            if (product.Stock <= 0 || product.Stock < quantity)
                result.AddError(OrderError.NotEnoughStock);

            var campaign = await _campaignRepository.Get(CampaignSpecifications.ActiveCampaign(productCode, _workTime.Now()));
            if (campaign != null)
                result.Combine(order.ApplyCampaign(campaign.Id, product.DiscountedPrice));

            result
                .Combine(order.SetCode(productCode))
                .Combine(order.SetPrice(product.Price))
                .Combine(order.SetQuantity(quantity));

            if (result.IsSuccess)
            {
                order.Create();
                _orderRepository.Add(order);
            }

            return result;
        }
    }
}