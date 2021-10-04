using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Mapping;
using Core.Domain.Product;
using MediatR;

namespace Application.Queries
{
    public class GetProductByCodeQueryHandler : IRequestHandler<GetProductByCodeQuery, ProductDto>
    {
        private readonly IProductDomainService _productDomainService;
        private readonly IMapper _mapper;

        public GetProductByCodeQueryHandler(IProductDomainService productDomainService, IMapper mapper)
        {
            _productDomainService = productDomainService;
            _mapper = mapper;
        }


        public async Task<ProductDto> Handle(GetProductByCodeQuery query, CancellationToken cancellationToken)
        {
            var productOverviewDto = await _productDomainService.GetOverview(query.Code);

            return _mapper.Map<ProductDto>(productOverviewDto);
        }
    }
}