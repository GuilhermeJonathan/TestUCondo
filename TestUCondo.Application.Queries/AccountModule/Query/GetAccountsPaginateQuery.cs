using MediatR;
using TestUCondo.Application.CrossCutting.DTO;
using TestUCondo.Application.CrossCutting.DTO.Accounts;

namespace TestUCondo.Application.Queries.AccountModule.Query
{
    public class GetAccountsPaginateQuery : IRequest<PaginatedResultDto<AccountDTO>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }

        public GetAccountsPaginateQuery(int page, int pageSize, string? search = null)
        {
            Page = page;
            PageSize = pageSize;
            Search = search;
        }
    }
}