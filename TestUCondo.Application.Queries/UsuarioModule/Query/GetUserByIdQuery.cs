using MediatR;

namespace TestUCondo.Application.Queries.UsuarioModule.Query
{
    public class GetUserByIdQuery : IRequest<UserDto?>
    {
        public long Id { get; set; }
    }


    public class UserDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
