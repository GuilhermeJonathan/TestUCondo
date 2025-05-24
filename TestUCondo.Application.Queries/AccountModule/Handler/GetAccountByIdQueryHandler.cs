using MediatR;
using TestUCondo.Application.CrossCutting.DTO.Accounts;
using TestUCondo.Application.Queries.AccountModule.Query;
using TestUCondo.Domain.Entities.Repositories;
using AutoMapper;

namespace TestUCondo.Application.Queries.AccountModule.Handler
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDTO?>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountByIdQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<AccountDTO?> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAsync(request.Id, cancellationToken);
            if (account == null)
                return null;

            return _mapper.Map<AccountDTO>(account);
        }
    }
}