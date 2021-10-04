using System.Threading.Tasks;
using Application.Commands;
using Application.Mapping;
using Core;
using Core.Domain.Product;
using Core.Exceptions;
using Moq;
using Xunit;

namespace Application.Tests.Commands
{
    public class ProductCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IProductDomainService> _domainService;
        private readonly Mock<IMapper> _mapper;

        public ProductCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _domainService = new Mock<IProductDomainService>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task should_create_product()
        {
            // given
            var command = new CreateProductCommand()
            {
                Code = "AB",
                Price = 100,
                Stock = 10
            };

            var handler = new CreateProductCommandHandler(_domainService.Object, _unitOfWork.Object, _mapper.Object);

            _domainService.Setup(x => x.Add(command.Code, command.Price, command.Stock)).ReturnsAsync(Result.Ok(new Product()));

            // when
            await handler.Handle(command, new System.Threading.CancellationToken());

            // then
            _domainService.Verify(m => m.Add(command.Code, command.Price, command.Stock));
            _unitOfWork.Verify(m => m.SaveChangesAsync());
        }

        [Fact]
        public async Task should_throw_exception_when_create_product_failed()
        {
            // given
            var command = new CreateProductCommand()
            {
                Code = "AB",
                Price = 100,
                Stock = 10
            };

            var handler = new CreateProductCommandHandler(_domainService.Object, _unitOfWork.Object, _mapper.Object);

            _domainService.Setup(x => x.Add(command.Code, command.Price, command.Stock)).ReturnsAsync(Result.Fail<Product>());

            // when & then
            await Assert.ThrowsAsync<ValidationException>(async () =>
                await handler.Handle(command, new System.Threading.CancellationToken()));
        }
    }
}