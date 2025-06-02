using MediatR;
using TestUCondo.Application.CrossCutting.DTO;

namespace TestUCondo.Application.Commands.CustomerModule.Command
{
    public class CreateCustomerCommand : IRequest<ResultType>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CpfCnpj { get; set; }
        public string Phone { get; set; }
    }
}