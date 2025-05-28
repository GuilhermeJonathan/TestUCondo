using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestUCondo.Application.Commands.CustomerModule.Command;
using TestUCondo.Application.Queries.CustomerModule.Query;

namespace TestUCondo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers(
            [FromQuery] string? name,
            [FromQuery] string? email,
            [FromQuery] string? cpfCnpj,
            [FromQuery] int? offset = 1,
            [FromQuery] int? limit = 100)
        {
            var query = new GetCustomersQuery(name, email, cpfCnpj, offset, limit);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery(id));
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);            
        }
    }
}
