using FluentValidation;
using MediatR;
using TestUCondo.Application.Commands.UsuarioModule.Command;
using TestUCondo.Application.CrossCutting.DTO;
using TestUCondo.Domain.Entities.Repositories;

namespace TestUCondo.Application.Commands.UsuarioModule.Handler
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResultType>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<DeleteUserCommand> _validator;

        public DeleteUserCommandHandler(IUserRepository userRepository, IValidator<DeleteUserCommand> validator)
        {
            _userRepository = userRepository;
            _validator = validator;
        }

        public async Task<ResultType> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
                return new ResultType { Success = false, Message = "User not found." };

            await _userRepository.DeleteAsync(user, cancellationToken);

            return new ResultType { Success = true, Message = "User deleted successfully." };
        }
    }
}