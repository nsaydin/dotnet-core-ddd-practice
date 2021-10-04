using System;
using System.Threading.Tasks;
using Application.Commands;
using Application.Dtos;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class TimesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<CampaignDto>> Get()
        {
            return Ok(await Mediator.Send(new GetTimeQuery()));
        }

        [HttpPost("increase")]
        public async Task<ActionResult<DateTime>> Increase(IncreaseTimeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}