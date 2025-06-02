using MediatR;
using TestUCondo.Application.CrossCutting.DTO;
using TestUCondo.Application.CrossCutting.DTO.Accounts;
using TestUCondo.Application.Queries.AccountModule.Query;
using TestUCondo.Domain.Entities.Repositories;
using AutoMapper;

namespace TestUCondo.Application.Queries.AccountModule.Handler
{
    public class GetAccountsPaginateQueryHandler : IRequestHandler<GetAccountsPaginateQuery, PaginatedResultDto<AccountDTO>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountsPaginateQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResultDto<AccountDTO>> Handle(GetAccountsPaginateQuery request, CancellationToken cancellationToken)
        {
            var (accounts, totalCount) = await _accountRepository.GetPaginatedAsync(
                request.Page, request.PageSize, request.Search, cancellationToken);

            var accountDtos = accounts.Select(a => _mapper.Map<AccountDTO>(a));
            
            return new PaginatedResultDto<AccountDTO>
            {
                Items = accountDtos,
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }
    }
}