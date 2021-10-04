using System.Threading.Tasks;
using Application.Commands;
using Application.Mapping;
using Core;
using Core.Domain.Order;
using Core.Exceptions;
using Moq;
using Xunit;

namespace Application.Tests.Commands
{
    public class OrderCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IOrderDomainService> _domainService;
        private readonly Mock<IMapper> _mapper;

        public OrderCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _domainService = new Mock<IOrderDomainService>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task should_create_order()
        {
            // given
            var command = new CreateOrderCommand()
            {
                Quantity = 1,
                ProductCode = "apple"
            };

            var handler = new CreateOrderCommandHandler(_domainService.Object, _unitOfWork.Object, _mapper.Object);

            _domainService.Setup(x => x.Add(command.ProductCode, command.Quantity)).ReturnsAsync(Result.Ok(new Order()));

            // when
            await handler.Handle(command, new System.Threading.CancellationToken());

            // then
            _domainService.Verify(m => m.Add(command.ProductCode, command.Quantity));
            _unitOfWork.Verify(m => m.SaveChangesAsync());
        }

        [Fact]
        public async Task should_throw_exception_when_create_order_failed()
        {
            // given
            var command = new CreateOrderCommand()
            {
                Quantity = 1,
                ProductCode = "apple"
            };

            var handler = new CreateOrderCommandHandler(_domainService.Object, _unitOfWork.Object, _mapper.Object);

            _domainService.Setup(x => x.Add(command.ProductCode, command.Quantity)).ReturnsAsync(Result.Fail<Order>());

            // when & then
            await Assert.ThrowsAsync<ValidationException>(async () =>
                await handler.Handle(command, new System.Threading.CancellationToken()));
        }
    }
}