using MediatR;
using TestUCondo.Application.CrossCutting.DTO;

namespace TestUCondo.Application.Commands.AccountModule.Command
{
    public class DeleteAccountCommand : IRequest<ResultType>
    {
        public long Id { get; set; }
        public DeleteAccountCommand(long id)
        {
            Id = id;
        }
    }
}