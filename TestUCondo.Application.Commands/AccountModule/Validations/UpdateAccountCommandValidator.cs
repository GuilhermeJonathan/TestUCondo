using FluentValidation;
using TestUCondo.Application.Commands.AccountModule.Command;

namespace TestUCondo.Application.Commands.AccountModule.Validations
{
    public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id é obrigatório.");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Descrição é obrigatória.")
                .MaximumLength(100).WithMessage("Descrição deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Tipo)
                .IsInEnum().WithMessage("Tipo de conta inválido.");

            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("Código é obrigatório.");

            RuleFor(x => x.AceitaLancamento)
                .NotNull().WithMessage("Aceita Lançamento é obrigatório.");
        }
    }
}