using System.Threading.Tasks;
using Core.Domain.Order;
using Core.Domain.Product;
using Core.Event;

namespace Core.Listeners
{
    public class ChangeProductStockEventHandler : IEventHandler<OrderCreated>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeProductStockEventHandler(IProductRepository productRepository, IUnitOfWork unitOfWork,
            IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
        }

        public async Task Handle(IEventContext<OrderCreated> context)
        {
            var domainEvent = context.Message;
            
            var order = await _orderRepository.GetAndThrow(domainEvent.OrderId);
            var product = await _productRepository.GetAndThrow(x => x.Code == order.ProductCode);
            
            product.Sell(order.Quantity);
            _productRepository.Update(product);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}