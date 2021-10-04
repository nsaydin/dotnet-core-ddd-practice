using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Mapping;
using Core;
using Core.Domain.Product;
using MediatR;

namespace Application.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductDomainService _productDomainService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductDomainService productDomainService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productDomainService = productDomainService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await _productDomainService.Add(command.Code, command.Price, command.Stock);
            result.ValidateAndThrow();

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProductDto>(result.Value);
        }
    }
}