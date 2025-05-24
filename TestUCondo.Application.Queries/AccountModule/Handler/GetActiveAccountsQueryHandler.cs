using MediatR;
using TestUCondo.Application.CrossCutting.DTO.Accounts;
using TestUCondo.Application.Queries.AccountModule.Query;
using TestUCondo.Domain.Entities.Repositories;
using AutoMapper;

namespace TestUCondo.Application.Queries.AccountModule.Handler
{
    public class GetActiveAccountsQueryHandler : IRequestHandler<GetActiveAccountsQuery, List<AccountResumeDTO>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetActiveAccountsQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<List<AccountResumeDTO>> Handle(GetActiveAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.GetListAsync(a => a.Ativo, cancellationToken);
            return accounts.Select(a => _mapper.Map<AccountResumeDTO>(a)).ToList();
        }
    }
}