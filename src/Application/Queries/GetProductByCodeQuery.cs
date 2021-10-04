using Application.Dtos;
using MediatR;

namespace Application.Queries
{
    public class GetProductByCodeQuery : IRequest<ProductDto>
    {
        public string Code { get; }
        public GetProductByCodeQuery(string code)
        {
            Code = code;
        }
    }
}