using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestUCondo.Application.Commands.AccountModule.Command;

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
    }
}
