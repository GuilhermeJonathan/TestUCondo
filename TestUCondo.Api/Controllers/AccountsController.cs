using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestUCondo.Application.Commands.AccountModule.Command;
using TestUCondo.Application.Queries.AccountModule.Query;

namespace TestUCondo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateAccountCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id in URL and body must match.");

            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _mediator.Send(new GetAccountByIdQuery { Id = id });
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var query = new GetAccountsPaginateQuery(page, pageSize, search);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var command = new DeleteAccountCommand(id);
            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
