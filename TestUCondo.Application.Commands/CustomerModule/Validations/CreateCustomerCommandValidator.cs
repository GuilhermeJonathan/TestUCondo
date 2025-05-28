using FluentValidation;
using TestUCondo.Application.Commands.CustomerModule.Command;

namespace TestUCondo.Application.Commands.CustomerModule.Validations
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome deve ser informado.")
                .MaximumLength(100).WithMessage("O nome deve possuir no máximo 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email deve ser informado.")
                .EmailAddress().WithMessage("Deve ser um email válido.")
                .MaximumLength(100).WithMessage("O email deve possuir no máximo 100 caracteres.");

            RuleFor(x => x.CpfCnpj)
                .NotEmpty().WithMessage("O CPF/CNPJ deve ser informado.")
                .Must(IsCpfCnpjValid).WithMessage("CPF/CNPJ inválido.")
                .MaximumLength(18).WithMessage("O CPF/CNPJ deve possuir no máximo 18 caracteres.");
        }

        private bool IsCpfCnpjValid(string cpfCnpj)
        {
            if (string.IsNullOrWhiteSpace(cpfCnpj))
                return false;

            var digits = new string(cpfCnpj.Where(char.IsDigit).ToArray());
            return digits.Length == 11 || digits.Length == 14;            
        }
    }
}