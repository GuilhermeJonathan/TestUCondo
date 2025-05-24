using MediatR;
using TestUCondo.Application.CrossCutting.DTO.Accounts;

namespace TestUCondo.Application.Queries.AccountModule.Query
{
    public class GetActiveAccountsQuery : IRequest<List<AccountResumeDTO>>
    {
    }
}