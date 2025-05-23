using MediatR;
using TestUCondo.Application.CrossCutting.DTO;
using TestUCondo.Application.Queries.UsuarioModule.DTO;

namespace TestUCondo.Application.Queries.UsuarioModule.Query
{
    public class GetUsersPaginateQuery : IRequest<PaginatedResultDto<UserQueryDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }

        public GetUsersPaginateQuery(int page, int pageSize, string? search = null)
        {
            Page = page;
            PageSize = pageSize;
            Search = search;
        }
    }
}