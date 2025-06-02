using MediatR;
using TestUCondo.Application.CrossCutting.DTO;

namespace TestUCondo.Application.Commands.UsuarioModule.Command
{
    public class DeleteUserCommand : IRequest<ResultType>
    {
        public long Id { get; set; }
        public DeleteUserCommand(long id)
        {
            Id = id;
        }
    }
}