using FluentValidation;
using TestUCondo.Application.Commands.UsuarioModule.Command;

namespace TestUCondo.Application.Commands.UsuarioModule.Validations
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O Nome deve ser informado.")
                .MaximumLength(100).WithMessage("O Nome deve possuir no máximo 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("A Email deve ser informada.")
                .EmailAddress().WithMessage("Deve ser um email válido.")
                .MaximumLength(100).WithMessage("O Email deve possuir no máximo 100 caracteres.");
        }
    }
}
