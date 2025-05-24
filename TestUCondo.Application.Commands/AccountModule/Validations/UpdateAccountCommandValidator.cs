using FluentValidation;
using TestUCondo.Application.Commands.AccountModule.Command;

namespace TestUCondo.Application.Commands.AccountModule.Validations
{
    public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id � obrigat�rio.");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Descri��o � obrigat�ria.")
                .MaximumLength(100).WithMessage("Descri��o deve ter no m�ximo 100 caracteres.");

            RuleFor(x => x.Tipo)
                .IsInEnum().WithMessage("Tipo de conta inv�lido.");

            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("C�digo � obrigat�rio.");

            RuleFor(x => x.AceitaLancamento)
                .NotNull().WithMessage("Aceita Lan�amento � obrigat�rio.");
        }
    }
}