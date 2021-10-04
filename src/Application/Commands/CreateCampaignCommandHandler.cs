using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Mapping;
using Core;
using Core.Domain.Campaign;
using MediatR;

namespace Application.Commands
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, CampaignDto>
    {
        private readonly ICampaignDomainService _campaignDomainService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCampaignCommandHandler(ICampaignDomainService campaignDomainService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _campaignDomainService = campaignDomainService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<CampaignDto> Handle(CreateCampaignCommand command, CancellationToken cancellationToken)
        {
            var result = await _campaignDomainService.Add(command.Name, command.ProductCode, command.Duration,
                command.PriceManipulationLimit, command.TargetSalesCount);
            result.ValidateAndThrow();

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CampaignDto>(result.Value);
        }
    }
}