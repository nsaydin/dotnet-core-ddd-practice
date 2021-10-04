using Application.Dtos;
using MediatR;

namespace Application.Commands
{
    public class CreateOrderCommand : IRequest<OrderDto>
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
    }
}