using MediatR;
using TestUCondo.Application.CrossCutting.DTO;
using TestUCondo.Application.Queries.UsuarioModule.DTO;
using TestUCondo.Application.Queries.UsuarioModule.Query;
using TestUCondo.Domain.Entities.Repositories;

namespace TestUCondo.Application.Queries.UsuarioModule.Handler
{
    public class GetUsersPaginateQueryHandler : IRequestHandler<GetUsersPaginateQuery, PaginatedResultDto<UserQueryDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersPaginateQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PaginatedResultDto<UserQueryDto>> Handle(GetUsersPaginateQuery request, CancellationToken cancellationToken)
        {
            var (users, totalCount) = await _userRepository.GetPaginatedAsync(
                request.Page, request.PageSize, request.Search, cancellationToken);

            var userDtos = users.Select(u => new UserQueryDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            });

            return new PaginatedResultDto<UserQueryDto>
            {
                Items = userDtos,
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }
    }
}