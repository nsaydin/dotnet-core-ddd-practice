using System.Threading.Tasks;
using Application.Commands;
using Application.Mapping;
using Core;
using Core.Domain.Campaign;
using Core.Exceptions;
using Moq;
using Xunit;

namespace Application.Tests.Commands
{
    public class CampaignCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ICampaignDomainService> _domainService;
        private readonly Mock<IMapper> _mapper;

        public CampaignCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _domainService = new Mock<ICampaignDomainService>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task should_create_campaign()
        {
            // given
            var command = new CreateCampaignCommand()
            {
                ProductCode = "apple",
                Duration = 1,
                Name = "camp",
                PriceManipulationLimit = 20,
                TargetSalesCount = 2
            };

            var handler = new CreateCampaignCommandHandler(_domainService.Object, _unitOfWork.Object, _mapper.Object);

            _domainService.Setup(x => x.Add(command.Name, command.ProductCode, command.Duration,
                command.PriceManipulationLimit, command.TargetSalesCount)).ReturnsAsync(Result.Ok(new Campaign()));

            // when
            await handler.Handle(command, new System.Threading.CancellationToken());

            // then
            _domainService.Verify(m => m.Add(command.Name, command.ProductCode, command.Duration, command.PriceManipulationLimit,
                command.TargetSalesCount));
            _unitOfWork.Verify(m => m.SaveChangesAsync());
        }

        [Fact]
        public async Task should_throw_exception_when_create_campaign_failed()
        {
            // given
            var command = new CreateCampaignCommand()
            {
                ProductCode = "apple",
                Duration = 1,
                Name = "camp",
                PriceManipulationLimit = 20,
                TargetSalesCount = 2
            };

            var handler = new CreateCampaignCommandHandler(_domainService.Object, _unitOfWork.Object, _mapper.Object);

            _domainService.Setup(x => x.Add(command.Name, command.ProductCode, command.Duration,
                command.PriceManipulationLimit, command.TargetSalesCount)).ReturnsAsync(Result.Fail<Campaign>());

            // when & then
            await Assert.ThrowsAsync<ValidationException>(async () =>
                await handler.Handle(command, new System.Threading.CancellationToken()));
        }
    }
}