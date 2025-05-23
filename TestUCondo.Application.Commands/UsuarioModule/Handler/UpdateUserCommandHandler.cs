using FluentValidation;
using MediatR;
using TestUCondo.Application.Commands.UsuarioModule.Command;
using TestUCondo.Application.CrossCutting.DTO;
using TestUCondo.Domain.Entities.Repositories;

namespace TestUCondo.Application.Commands.UsuarioModule.Handler
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResultType>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UpdateUserCommand> _validator;

        public UpdateUserCommandHandler(IUserRepository userRepository, IValidator<UpdateUserCommand> validator)
        {
            _userRepository = userRepository;
            _validator = validator;
        }

        public async Task<ResultType> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
                return new ResultType { Success = false, Message = "User not found." };

            user.Name = request.Name;
            user.Email = request.Email;

            await _userRepository.UpdateAsync(user);

            return new ResultType { Success = true, Message = "User updated succesfull.", Data = user };
        }
    }
}