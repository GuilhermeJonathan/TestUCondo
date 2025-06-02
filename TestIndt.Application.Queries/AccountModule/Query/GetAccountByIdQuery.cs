using MediatR;
using TestUCondo.Application.CrossCutting.DTO.Accounts;

namespace TestUCondo.Application.Queries.AccountModule.Query
{
    public class GetAccountByIdQuery : IRequest<AccountDTO?>
    {
        public long Id { get; set; }
    }
}