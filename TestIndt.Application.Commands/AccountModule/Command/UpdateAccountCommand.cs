using MediatR;
using TestUCondo.Application.CrossCutting.DTO;
using TestUCondo.Application.CrossCutting.Enum;

namespace TestUCondo.Application.Commands.AccountModule.Command
{
    public class UpdateAccountCommand : IRequest<ResultType>
    {
        public long Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public bool AceitaLancamento { get; set; }
        public AccountTypeEnum Tipo { get; set; }
        public long? Pai { get; set; }
    }
}