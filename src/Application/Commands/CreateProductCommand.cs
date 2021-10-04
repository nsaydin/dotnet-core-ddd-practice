using Application.Dtos;
using MediatR;

namespace Application.Commands
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}