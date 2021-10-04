using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Mapping;
using Core;
using Core.Domain.Order;
using MediatR;

namespace Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IOrderDomainService _orderDomainService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IOrderDomainService orderDomainService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _orderDomainService = orderDomainService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<OrderDto> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var result = await _orderDomainService.Add(command.ProductCode, command.Quantity);
            result.ValidateAndThrow();

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderDto>(result.Value);
        }
    }
}