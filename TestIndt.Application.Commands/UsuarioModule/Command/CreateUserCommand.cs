using MediatR;
using TestUCondo.Application.CrossCutting.DTO;

namespace TestUCondo.Application.Commands.UsuarioModule.Command
{
    public class CreateUserCommand : IRequest<ResultType>
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
