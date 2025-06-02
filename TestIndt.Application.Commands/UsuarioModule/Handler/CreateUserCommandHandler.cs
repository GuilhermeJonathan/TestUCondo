using FluentValidation;
using MediatR;
using TestUCondo.Application.Commands.UsuarioModule.Command;
using TestUCondo.Application.CrossCutting.DTO;
using TestUCondo.Domain.Entities;
using TestUCondo.Domain.Entities.Repositories;

namespace TestUCondo.Application.Commands.UsuarioModule.Handler
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResultType>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<CreateUserCommand> _validator;

        public CreateUserCommandHandler(IUserRepository userRepository, IValidator<CreateUserCommand> validator)
        {
            _userRepository = userRepository;
            _validator = validator;
        }

        public async Task<ResultType> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {                
                throw new ValidationException(validationResult.Errors);
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,              
            };

            await _userRepository.AddAsync(user, cancellationToken);

            return new ResultType { Success = true, Data = user };
        }
    }
}
