using System.Threading.Tasks;
using Application.Commands;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class OrdersController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create(CreateOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}