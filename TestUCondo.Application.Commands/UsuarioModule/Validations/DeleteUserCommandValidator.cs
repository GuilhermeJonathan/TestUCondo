using FluentValidation;
using TestUCondo.Application.Commands.UsuarioModule.Command;

namespace TestUCondo.Application.Commands.UsuarioModule.Validations
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("User Id is required.");
        }
    }
}