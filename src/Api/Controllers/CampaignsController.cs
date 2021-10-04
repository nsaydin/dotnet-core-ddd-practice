using System.Threading.Tasks;
using Application.Commands;
using Application.Dtos;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class CampaignsController : BaseController
    {
        [HttpGet("{name}")]
        public async Task<ActionResult<CampaignDto>> GetByName(string name)
        {
            return Ok(await Mediator.Send(new GetCampaignByNameQuery(name)));
        }

        [HttpPost]
        public async Task<ActionResult<CampaignDto>> Create(CreateCampaignCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}