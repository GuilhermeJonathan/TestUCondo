using FluentValidation;
using TestUCondo.Application.Commands.AccountModule.Command;

namespace TestUCondo.Application.Commands.AccountModule.Validations
{
    public class DeleteAccountCommandValidator : AbstractValidator<DeleteAccountCommand>
    {
        public DeleteAccountCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("O Id deve ser maior que zero.");
        }
    }
}