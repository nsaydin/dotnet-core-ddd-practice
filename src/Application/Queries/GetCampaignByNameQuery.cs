using Application.Dtos;
using MediatR;

namespace Application.Queries
{
    public class GetCampaignByNameQuery : IRequest<CampaignDto>
    {
        public string Name { get; }

        public GetCampaignByNameQuery(string name)
        {
            Name = name;
        }
    }
}