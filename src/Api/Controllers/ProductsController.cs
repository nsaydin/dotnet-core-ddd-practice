using System.Threading.Tasks;
using Application.Commands;
using Application.Dtos;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ProductsController : BaseController
    {
        [HttpGet("{code}")]
        public async Task<ActionResult<ProductDto>> GetByCode(string code)
        {
            return Ok(await Mediator.Send(new GetProductByCodeQuery(code)));
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(CreateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}